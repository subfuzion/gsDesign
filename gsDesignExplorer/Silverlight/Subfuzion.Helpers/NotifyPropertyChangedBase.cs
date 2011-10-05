namespace Subfuzion.Helpers
{
	using System;
	using System.ComponentModel;
	using System.Windows;

	public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void RaisePropertyChanged(string property)
		{
			try
			{
				if (PropertyChanged != null)
				{
					Deployment.Current.Dispatcher.BeginInvoke(() => PropertyChanged(this, new PropertyChangedEventArgs(property)));
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}