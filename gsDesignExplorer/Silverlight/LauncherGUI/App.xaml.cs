using System.Windows;
using gsDesign.LauncherGUI.ViewModels;

namespace gsDesign.LauncherGUI
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static ViewModel ViewModel
		{
			get { return (ViewModel)Current.Resources["ViewModel"]; }
		}
	}
}
