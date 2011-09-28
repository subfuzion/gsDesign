using System;
using System.Windows;
using Microsoft.Win32;
using gsDesign.LauncherGUI.ViewModels;

namespace gsDesign.LauncherGUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
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
	}
}
