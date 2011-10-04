using System.Windows.Controls;

namespace gsDesign.Explorer.Views.Test
{
	using Subfuzion.R.Rserve.Protocol;

	public partial class TestView : UserControl
	{
		public TestView()
		{
			InitializeComponent();
		}

		private void executeButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(inputText.Text))
			{
				var request = Request.Eval(inputText.Text);
				App.AppViewModel.RserveClient.SendRequest(request, OnResponse);
			}
		}

		private void OnResponse(Response response)
		{
			
		}
	}
}
