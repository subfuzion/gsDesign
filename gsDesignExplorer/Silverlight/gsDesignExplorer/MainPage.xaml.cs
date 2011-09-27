using System.Windows;
using System.Windows.Controls;

namespace gsDesign.Explorer
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void runButton_Click(object sender, RoutedEventArgs e)
		{
			App.ViewModel.RunDesign();
		}
	}
}