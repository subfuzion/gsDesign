namespace gsDesign.Explorer.ViewModels
{
	using System;
	using System.ComponentModel;

	public class AsyncDelegateCommand : DelegateCommand
	{
		private readonly Action<object> _completedAction;
		private readonly Action<Exception> _errorAction;
		private readonly Action<object, ProgressChangedEventArgs> _progressAction;
		private BackgroundWorker _worker;

		public AsyncDelegateCommand(
			Action<object> action,
			Func<object, bool> canExecute = null,
			Action<object> completed = null,
			Action<object, ProgressChangedEventArgs> progress = null,
			Action<Exception> error = null
			)
			: base(action, canExecute)
		{
			_completedAction = completed;
			_progressAction = progress;
			_errorAction = error;
		}


		public override void Execute(object parameter)
		{
			// shouldn't execute at all if CanExecute is queried properly first
			if (IsBusy()) return;

			_worker = new BackgroundWorker();
			_worker.DoWork += (sender, doWorkEventArgs) =>
			                  	{
			                  		// uncomment for WPF
			                  		// CommandManager.InvalidateRequerySuggested();
			                  		base.Execute(parameter);
			                  	};

			_worker.ProgressChanged += (sender, progressChangedEventArgs) =>
			                           	{
			                           		if (_progressAction != null)
			                           		{
			                           			_progressAction(sender, progressChangedEventArgs);
			                           		}
			                           	};

			_worker.RunWorkerCompleted += (sender, runWorkerCompletedEventArgs) =>
			                              	{
			                              		if (_completedAction != null)
			                              		{
			                              			if (runWorkerCompletedEventArgs.Error != null)
			                              			{
			                              				_errorAction(runWorkerCompletedEventArgs.Error);
			                              			}
			                              			else
			                              			{
			                              				_completedAction(runWorkerCompletedEventArgs.Result);
			                              			}
			                              		}

			                              		_worker = null;
			                              	};

			_worker.RunWorkerAsync(parameter);
		}

		public override bool CanExecute(object parameter)
		{
			return base.CanExecute(parameter) && !IsBusy();
		}

		public void Cancel()
		{
			if (_worker != null && _worker.IsBusy && _worker.WorkerSupportsCancellation) _worker.CancelAsync();
		}

		public bool IsBusy()
		{
			return _worker != null && _worker.IsBusy;
		}
	}
}