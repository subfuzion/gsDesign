using System.Windows;
using System.Windows.Controls;

namespace gsDesign.Explorer.Views
{
	public partial class DesignExplorerView : UserControl
	{
		public DesignExplorerView()
		{
			InitializeComponent();
		}

		// for a better solution see
		// http://dnchannel.blogspot.com/2010/01/silverlight-3-auto-select-text-in.html
		private void DescriptionTextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			((TextBox)sender).SelectAll();
		}
	}
}
