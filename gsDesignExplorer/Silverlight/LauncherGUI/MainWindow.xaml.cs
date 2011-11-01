using System.Windows;
using Microsoft.Win32;

namespace gsDesign.LauncherGUI
{
	using Services;

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			ServiceManager.Instance.StartServices();
		}

		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			ServiceManager.Instance.StopServices();
			StopSystem();
			base.OnClosing(e);
		}

		protected override void OnClosed(System.EventArgs e)
		{
			base.OnClosed(e);
		}

		private void StartRserveButton_Click(object sender, RoutedEventArgs e)
		{
			App.ViewModel.ToggleRserveRunning();
		}

		private void SetRservePathButton_Click(object sender, RoutedEventArgs e)
		{
			var dlg = new OpenFileDialog
			          	{
			          		FileName = App.ViewModel.RservePath,
							DefaultExt = ".exe",
							Filter = "Executable files (.exe)|*.exe"
			          	};

			bool? result = dlg.ShowDialog();

			if (result == true)
			{
				App.ViewModel.RservePath = dlg.FileName;
			}
		}

		private void StartPolicyServerButton_Click(object sender, RoutedEventArgs e)
		{
			App.ViewModel.TogglePolicyServerRunning();
		}

		private void SetPolicyServerPathButton_Click(object sender, RoutedEventArgs e)
		{
			var dlg = new OpenFileDialog
			{
				FileName = App.ViewModel.PolicyServerPath,
				DefaultExt = ".exe",
				Filter = "Executable files (.exe)|*.exe"
			};

			bool? result = dlg.ShowDialog();

			if (result == true)
			{
				App.ViewModel.PolicyServerPath = dlg.FileName;
			}
		}

		private void StartExplorerButton_Click(object sender, RoutedEventArgs e)
		{
			App.ViewModel.OpenExplorer();
		}

		private void SetExplorerPathButton_Click(object sender, RoutedEventArgs e)
		{
			var dlg = new OpenFileDialog
			{
				FileName = App.ViewModel.ExplorerPath,
				DefaultExt = ".html",
				Filter = "HTML files (.html)|*.html"
			};

			bool? result = dlg.ShowDialog();

			if (result == true)
			{
				App.ViewModel.ExplorerPath = dlg.FileName;
			}
		}

		private void SystemButton_Click(object sender, RoutedEventArgs e)
		{
			StartSystem();
		}

		private void StartSystem()
		{
			App.ViewModel.ToggleRserveRunning();
			App.ViewModel.TogglePolicyServerRunning();
			App.ViewModel.OpenExplorer();
		}

		private void StopSystem()
		{
			App.ViewModel.StopPolicyServer();
			App.ViewModel.StopRserve();
		}
	}
}
