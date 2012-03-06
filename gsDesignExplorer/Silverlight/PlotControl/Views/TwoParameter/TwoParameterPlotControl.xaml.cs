namespace Subfuzion.Silverlight.UI.Charting.Views.TwoParameter
{
	using System.Windows.Controls;
	using System.Windows;
	using System.Windows.Data;
	using ViewModels;

	public partial class TwoParameterPlotControl : UserControl
	{
		public TwoParameterPlotControl()
		{
			InitializeComponent();
			Loaded += (sender, args) =>
			{
				var viewModel = DataContext as TwoParameterSpendingFunctionViewModel;
				if (viewModel != null)
				{
					viewModel.UpdateAll();
					RegisterForNotification("ControlPointPhysicalPosition", plot, (o, args_) =>
					{
						var point = plot.PhysicalToLogicalCoordinates((Point)args_.NewValue);
						//_plotFunction.Update(point.X, point.Y);
						viewModel.UpdateCoordinate(point.X, point.Y);
						//viewModel.TimingParameter = point.X;
						//viewModel.InterimSpendingParameter = point.Y;
					});
				}
			};
		}

		// Listen for change of the dependency property  
		public void RegisterForNotification(string propertyName, FrameworkElement element, PropertyChangedCallback callback)
		{
			// bind to dependency property  
			var b = new Binding(propertyName) { Source = element };
			var prop = DependencyProperty.RegisterAttached(
				"NotifyAttached" + propertyName,
				typeof(object),
				typeof(UserControl),
				new PropertyMetadata(callback));

			element.SetBinding(prop, b);
		}
	}
}
