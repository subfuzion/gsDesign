namespace Subfuzion.Silverlight.UI.Charting.Views
{
	using System.Windows.Controls;

	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();

			//plot.Logger = msg =>
			//{
			//    log.Text += msg + "\n";
			//    scrollView.ScrollToBottom();
			//    log.SelectionStart = log.Text.Length;
			//};

			log.TextChanged += (sender, args) =>
			{
				scrollView.ScrollToBottom();
				log.SelectionStart = log.Text.Length;
			};
		}
	}
}