using System;
using System.Windows;
using Microsoft.Win32;

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

		private void SetRservePathButton_Click(object sender, RoutedEventArgs e)
		{
			var dlg = new OpenFileDialog
			          	{
			          		FileName = "Rserve.exe",
							DefaultExt = ".exe",
							Filter = "Executable files (.exe)|*.exe"
			          	};

			bool? result = dlg.ShowDialog();

			if (result == true)
			{
				App.ViewModel.RservePath = dlg.FileName;
			}
		}
	}
}
