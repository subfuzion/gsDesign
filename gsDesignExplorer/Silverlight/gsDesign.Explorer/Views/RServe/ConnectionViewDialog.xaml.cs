namespace gsDesign.Explorer.Views.RServe
{
	using System.IO;
	using System.Windows;
	using System.Windows.Controls;
	using ViewModels.RServe;

	public partial class ConnectionViewDialog : ChildWindow
	{
		public ConnectionViewDialog()
		{
			InitializeComponent();
		}

		public ViewModel ViewModel
		{
			get { return (ViewModel)Resources["ViewModel"]; }
		}

		private void OKButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}

		private void SetRservePathButton_Click(object sender, RoutedEventArgs e)
		{
			var path = string.IsNullOrEmpty(ViewModel.RservePath) ? "." : Path.GetFullPath(Path.GetDirectoryName(ViewModel.RservePath));

			var dlg = new OpenFileDialog
			{
				InitialDirectory = path,
				Filter = "Executable files (.exe)|*.exe",
			};

			bool? result = dlg.ShowDialog();

			if (result == true)
			{
				ViewModel.RservePath = dlg.File.FullName;
				ViewModel.ToggleRserveRunning();
			}
		}
	}
}