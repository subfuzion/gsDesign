using System.Windows.Controls;

namespace Subfuzion.Silverlight.UI.Charting.OneParameter
{
	using ViewModels;

	public partial class OneParameterPlotControl : UserControl
	{
		public OneParameterPlotControl()
		{
			InitializeComponent();
			Loaded += (sender, args) =>
			{
				var viewModel = DataContext as SpendingFunctionViewModel;
				if (viewModel != null)
				{
					viewModel.UpdateLine();
				}
			};
		}
	}
}
