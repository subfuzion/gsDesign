namespace gsDesign.Explorer.ViewModels
{
	using System;
	using System.ComponentModel;
	using System.Windows;
	using System.Windows.Input;

	public class DelegateCommand : ICommand, INotifyPropertyChanged
	{
		#region Fields

		private bool _isEnabled = true;

		private BackgroundWorker _worker;

		#endregion

		#region Initialization Properties

		public bool Async { get; set; }

		public Func<object, bool> CanExecuteFunc { get; set; } 

		public Action<object> ExecuteAction { get; set; }

		public Action<object> CompletedAction { get; set; }

		public Action<object, ProgressChangedEventArgs> ProgressAction { get; set; }

		public Action<Exception> ErrorAction;

		#endregion

		#region Properties

		// IsEnabled will be set to false while
		// a command is busy executing
		public bool IsEnabled
		{
			get
			{
				return _isEnabled = CanExecute();
			}

			set
			{
				if (_isEnabled != value)
				{
					_isEnabled = value;
					RaisePropertyChanged("IsEnabled");
					RaiseCanExecuteChanged();
				}
			}
		}

		public bool IsWorkerBusy()
		{
			return _worker != null && _worker.IsBusy;
		}

		#endregion

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		protected void RaisePropertyChanged(string property)
		{
			if (PropertyChanged != null)
			{
				Deployment.Current.Dispatcher.BeginInvoke(() => PropertyChanged(this, new PropertyChangedEventArgs(property)));
			}
		}

		#endregion

		#region ICommand Members

		public event EventHandler CanExecuteChanged;

		public virtual bool CanExecute(object parameter = null)
		{
			// can't execute if busy
			bool canExecute = !IsWorkerBusy();

			// if not busy, then also invoke CanExecuteFunc
			if (canExecute && CanExecuteFunc != null)
			{
				canExecute = CanExecuteFunc(parameter);
			}

			// if the current value does not match the _isEnabled state,
			// then fire CanExecuteChanged
			// (recursion warning: DON'T invoke through getter since it invokes this method)
			// (setting through setter is ok to ensure property changed events are raised)
			if (_isEnabled != canExecute)
			{
				IsEnabled = canExecute;
			}

			return canExecute;
		}

		public virtual void Execute(object parameter = null)
		{
			if (CanExecute(parameter) == false) return;

			if (Async)
			{
				ExecuteAsync(parameter);
			}
			else
			{
				ExecuteSync(parameter);
				if (CompletedAction != null)
				{
					CompletedAction(parameter);
				}
			}
		}

		private void ExecuteSync(object parameter)
		{
			if (ExecuteAction != null)
			{
				IsEnabled = false;

				try
				{
					ExecuteAction(parameter);
				}
				catch (Exception e)
				{
					if (ErrorAction != null)
					{
						ErrorAction(e);
					}
				}

				IsEnabled = true;
			}
		}

		private void ExecuteAsync(object parameter)
		{
			_worker = new BackgroundWorker();
			_worker.DoWork += (sender, doWorkEventArgs) =>
			{
				// uncomment for WPF
				// CommandManager.InvalidateRequerySuggested();
				IsEnabled = false;
				ExecuteSync(parameter);
			};

			_worker.ProgressChanged += (sender, progressChangedEventArgs) =>
			{
				if (ProgressAction != null)
				{
					ProgressAction(sender, progressChangedEventArgs);
				}
			};

			_worker.RunWorkerCompleted += (sender, runWorkerCompletedEventArgs) =>
			{
				if (runWorkerCompletedEventArgs.Error == null && CompletedAction != null)
				{
					CompletedAction(runWorkerCompletedEventArgs.Result);
				}

				if (runWorkerCompletedEventArgs.Error != null && ErrorAction != null)
				{
					ErrorAction(runWorkerCompletedEventArgs.Error);
				}

				_worker = null;
				IsEnabled = true;
			};

			_worker.RunWorkerAsync(parameter);
		}

		#endregion

		public void Cancel()
		{
			if (_worker != null && _worker.IsBusy && _worker.WorkerSupportsCancellation) _worker.CancelAsync();
		}

		protected void RaiseCanExecuteChanged()
		{
			Deployment.Current.Dispatcher.BeginInvoke(() => CanExecuteChanged(this, EventArgs.Empty));
		}

	}
}