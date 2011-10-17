namespace Subfuzion.Helpers
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Windows;

	public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged, INotifyDataErrorInfo
	{
		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region INotifyPropertyChanged implementation

		protected void RaisePropertyChanged(string property)
		{
			try
			{
				if (PropertyChanged != null)
				{
					Deployment.Current.Dispatcher.BeginInvoke(() =>
					                                          {
					                                          	// the double check is necessary in case anything has changed by the time this is invoked on the UI thread
					                                          	if (PropertyChanged != null)
					                                          	{
					                                          		PropertyChanged(this, new PropertyChangedEventArgs(property));
					                                          	}
					                                          });
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		#endregion

		#region INotifyDataErrorInfo

		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

		public IEnumerable GetErrors(string property)
		{
			if (_errors == null) return null;

			if (string.IsNullOrEmpty(property))
			{
				// return the whole error collection
				return _errors.Values;
			}

			if (_errors.ContainsKey(property))
			{
				return _errors[property];
			}

			return null;
		}

		public bool HasErrors
		{
			get { return _errors != null && _errors.Count > 0; }
		}

		#endregion

		#region INotifyDataErrorInfo implementation

		private Dictionary<string, List<string>> _errors;

		protected void RaiseErrorsChanged(string property)
		{
			try
			{
				if (ErrorsChanged != null)
				{
					Deployment.Current.Dispatcher.BeginInvoke(() =>
					{
						// the double check is necessary in case anything has changed by the time this is invoked on the UI thread
						if (ErrorsChanged != null)
						{
							ErrorsChanged(this, new DataErrorsChangedEventArgs(property));
						}
					});
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		protected void AddError(string property, string error)
		{
			if (_errors == null) _errors = new Dictionary<string, List<string>>();

			if (_errors.ContainsKey(property))
			{
				_errors[property].Add(error);
			}
			else
			{
				_errors.Add(property, new List<string> { error });
			}

			RaiseErrorsChanged(property);
		}

		protected void ClearErrors(string property = null)
		{
			if (_errors == null || _errors.Count == 0) return;

			if (property != null)
			{
				_errors.Remove(property);
				RaiseErrorsChanged(property);
			}
			else
			{
				foreach (var error in _errors)
				{
					ClearErrors(error.Key);
				}

				_errors = null;
			}
		}

		protected void SetErrors(string property, List<string> errors)
		{
			if (_errors == null) _errors = new Dictionary<string, List<string>>();

			_errors.Remove(property);
			_errors.Add(property, errors);
			RaiseErrorsChanged(property);
		}

		#endregion


	}
}