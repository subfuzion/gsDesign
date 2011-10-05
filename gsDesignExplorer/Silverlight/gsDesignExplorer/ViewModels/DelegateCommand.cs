namespace gsDesign.Explorer.ViewModels
{
	using System;
	using System.ComponentModel;
	using System.Windows;
	using System.Windows.Input;
	using Subfuzion.Helpers;

	public class DelegateCommand : NotifyPropertyChangedBase, ICommand
	{
		#region Fields

		// Commands are enabled by default
		private bool _canExecute = true;

		private BackgroundWorker _worker;

		#endregion

		#region Initialization Properties

		public Action<Exception> ErrorAction;

		private Func<object, bool> _canExecuteFunc;
		public bool Async { get; set; }

		public Func<object, bool> CanExecuteFunc
		{
			get { return _canExecuteFunc; }
			set
			{
				if (_canExecuteFunc != value)
				{
					_canExecuteFunc = value;
					Requery();
				}
			}
		}

		public Action<object> ExecuteAction { get; set; }

		public Action<object> CompletedAction { get; set; }

		public Action<object, ProgressChangedEventArgs> ProgressAction { get; set; }

		#endregion

		#region Properties

		// IsEnabled will be set to false while
		// a command is busy executing
		public bool IsEnabled
		{
			get { return _canExecute; }
			set
			{
				if (_canExecute != value)
				{
					_canExecute = value;
					RaisePropertyChanged("IsEnabled");
				}
			}
		}

		public bool IsWorkerBusy
		{
			get { return _worker != null && _worker.IsBusy; }
		}

		#endregion

		#region ICommand Members

		public event EventHandler CanExecuteChanged;

		public void Requery()
		{
			CanExecute();
		}

		public virtual bool CanExecute(object parameter = null)
		{
			// can't execute if busy
			bool canExecute = !IsWorkerBusy;

			// if not busy, then also invoke CanExecuteFunc
			if (canExecute && CanExecuteFunc != null)
			{
				canExecute = CanExecuteFunc(parameter);
			}

			// if the current value does not match the _canExecute state,
			// then fire CanExecuteChanged
			// (recursion warning: DON'T invoke through getter since it invokes this method)
			// (setting through setter is ok to ensure property changed events are raised)
			if (_canExecute != canExecute)
			{
				_canExecute = canExecute;
				RaiseCanExecuteChanged();
				RaisePropertyChanged("IsEnabled");
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

		#endregion

		private void ExecuteSync(object parameter)
		{
			if (ExecuteAction != null)
			{
//				IsEnabled = false;

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

//				IsEnabled = true;
			}
		}

		private void ExecuteAsync(object parameter)
		{
			_worker = new BackgroundWorker();
			_worker.DoWork += (sender, doWorkEventArgs) =>
			                  	{
			                  		// uncomment for WPF
			                  		// CommandManager.InvalidateRequerySuggested();
//				IsEnabled = false;
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
//				IsEnabled = true;
			                              	};

			_worker.RunWorkerAsync(parameter);
		}

		public void Cancel()
		{
			if (_worker != null && _worker.IsBusy && _worker.WorkerSupportsCancellation) _worker.CancelAsync();
		}

		protected void RaiseCanExecuteChanged()
		{
			if (CanExecuteChanged != null)
			{
				Deployment.Current.Dispatcher.BeginInvoke(() => CanExecuteChanged(this, EventArgs.Empty));
			}
		}
	}
}