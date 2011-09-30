namespace gsDesign.Explorer.Helpers
{
	using System.ComponentModel;
	using System.Windows;

	public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void RaisePropertyChanged(string property)
		{
			if (PropertyChanged != null)
			{
				Deployment.Current.Dispatcher.BeginInvoke(() => PropertyChanged(this, new PropertyChangedEventArgs(property)));
			}
		}
	}
}