namespace gsDesign.Explorer.ViewModels
{
	using System;
	using System.Windows.Input;

	public class DelegateCommand : ICommand
	{
		private readonly Action<object> _action;
		private readonly Func<object, bool> _canExecute;
		private bool _canExecuteCached;

		public DelegateCommand(Action<object> action, Func<object,bool> canExecute = null)
		{
			_action = action;
			_canExecute = canExecute;
		}

		#region ICommand Members

		public event EventHandler CanExecuteChanged;

		public virtual bool CanExecute(object parameter)
		{
			bool canExecute = _canExecute == null || _canExecute(parameter);

			// if the current value does not match the previously cached value,
			// then fire CanExecuteChanged
			if (_canExecuteCached != canExecute)
			{
				_canExecuteCached = canExecute;
				RaiseCanExecuteChanged();
			}

			return canExecute;
		}

		public virtual void Execute(object parameter)
		{
			if (_action != null) _action(parameter);
		}

		#endregion

		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged(this, EventArgs.Empty);
		}
	}
}