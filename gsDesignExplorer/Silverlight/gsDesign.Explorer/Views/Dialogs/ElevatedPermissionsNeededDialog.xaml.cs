namespace gsDesign.Explorer.Views.Dialogs
{
	using System.Windows;
	using System.Windows.Controls;

	public partial class ElevatedPermissionsNeededDialog : ChildWindow
	{
		public ElevatedPermissionsNeededDialog()
		{
			InitializeComponent();
		}

		private void OKButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}
	}
}