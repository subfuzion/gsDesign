namespace PlotControl
{
	using System.Windows;
	using System.Windows.Controls;
	using Subfuzion.Silverlight.UI.Charting;

	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();

			Loaded += OnLoaded;
		}

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			var plotDef = new PlotDefinition
			{
				MaximumX = 1.0,
				IntervalCount = 10,
				PlotFunction = PlotFunction,
			};

			plot.PlotDefinition = plotDef;
		}

		private double PlotFunction(double x)
		{
			return x*x;
		}
	}
}