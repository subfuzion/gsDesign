namespace gsDesign.Explorer.Views.RServe
{
	using System.Windows;
	using System.Windows.Controls;

	public partial class ConnectionView : UserControl
	{
		public ConnectionView()
		{
			InitializeComponent();
		}

		private void StartRserveButton_Click(object sender, RoutedEventArgs e)
		{
			//App.ViewModel.ToggleRserveRunning();
		}

		private void SetRservePathButton_Click(object sender, RoutedEventArgs e)
		{
			//var dlg = new OpenFileDialog
			//{
			//    FileName = App.ViewModel.RservePath,
			//    DefaultExt = ".exe",
			//    Filter = "Executable files (.exe)|*.exe"
			//};

			//bool? result = dlg.ShowDialog();

			//if (result == true)
			//{
			//    App.ViewModel.RservePath = dlg.FileName;
			//}
		}

		private void StartExplorerButton_Click(object sender, RoutedEventArgs e)
		{
			//App.ViewModel.OpenExplorer();
		}

		private void SetExplorerPathButton_Click(object sender, RoutedEventArgs e)
		{
			//var dlg = new OpenFileDialog
			//{
			//    FileName = App.ViewModel.ExplorerPath,
			//    DefaultExt = ".html",
			//    Filter = "HTML files (.html)|*.html"
			//};

			//bool? result = dlg.ShowDialog();

			//if (result == true)
			//{
			//    App.ViewModel.ExplorerPath = dlg.FileName;
			//}
		}

		private void SystemButton_Click(object sender, RoutedEventArgs e)
		{
			StartSystem();
		}

		private void StartSystem()
		{
			//App.ViewModel.ToggleRserveRunning();
			//App.ViewModel.OpenExplorer();
		}

		private void StopSystem()
		{
			//			App.ViewModel.StopPolicyServer();
//			App.ViewModel.StopRserve();
		}
	}
}