using System.Windows.Controls;

namespace gsDesign.Explorer.Views.Test
{
	using System.Windows.Input;
	using ViewModels.Test;

	public partial class TestView : UserControl
	{
		public TestView()
		{
			InitializeComponent();
		}

		TestViewModel ViewModel
		{
			get
			{
				return (TestViewModel)Resources["ViewModel"];
			}
		}

		private void LayoutRoot_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.F5)
			{
				ViewModel.RunCommand.Execute();
			}
		}

		private void inputText_TextChanged(object sender, TextChangedEventArgs e)
		{
			ViewModel.Input = inputText.Text;
		}
	}
}
