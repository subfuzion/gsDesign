namespace gsDesign.Explorer.Views
{
	using System.Windows;
	using System.Windows.Controls;

	public partial class ToolBar : UserControl
	{
		public ToolBar()
		{
			InitializeComponent();
		}

		private void runButton_Click(object sender, RoutedEventArgs e)
		{
			App.AppViewModel.RunDesign();
		}
	}
}