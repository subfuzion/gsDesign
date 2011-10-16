namespace Subfuzion.Helpers
{
	using System;
	using System.ComponentModel;
	using System.Windows;

	public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

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
	}
}