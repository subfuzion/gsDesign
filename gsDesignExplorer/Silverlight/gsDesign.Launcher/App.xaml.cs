namespace gsDesign.Launcher
{
	using System.Windows;
	using ViewModels;

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static ViewModel ViewModel
		{
			get { return (ViewModel) Current.Resources["ViewModel"]; }
		}
	}
}