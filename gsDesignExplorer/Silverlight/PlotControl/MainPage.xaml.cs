namespace PlotControl
{
	using System.Globalization;
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

		private HwangShihDeCaniPlotFunction _hsdPlotFunction;

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			_hsdPlotFunction = new HwangShihDeCaniPlotFunction
			{
				InterimSpendingParameter = 0.025,
				InterimSpendingParameterMaximum = 0.025,
				InterimSpendingParameterMinimum = 0.0,
				SpendingFunctionParameter = -8.0,
				SpendingFunctionParameterMaximum = 40.0,
				SpendingFunctionParameterMinimum = -40.0,
				Timing = 0.5,
				TimingMaximum = 1.0,
				TimingMinimum = 0.0,
			};

			_hsdPlotFunction.Update();

			plot.Coordinates = _hsdPlotFunction.Coordinates;

			gammaSlider.Minimum = _hsdPlotFunction.SpendingFunctionParameterMinimum;
			gammaSlider.Maximum = _hsdPlotFunction.SpendingFunctionParameterMaximum;
			gammaSlider.Value = _hsdPlotFunction.SpendingFunctionParameter;
			gammaTextBox.Text = _hsdPlotFunction.SpendingFunctionParameter.ToString(CultureInfo.InvariantCulture);

			interimSpendingSlider.Minimum = _hsdPlotFunction.InterimSpendingParameterMinimum;
			interimSpendingSlider.Maximum = _hsdPlotFunction.InterimSpendingParameterMaximum;
			interimSpendingSlider.Value = _hsdPlotFunction.InterimSpendingParameter;
			interimSpendingTextBox.Text = _hsdPlotFunction.InterimSpendingParameter.ToString(CultureInfo.InvariantCulture);

			timingSlider.Minimum = _hsdPlotFunction.TimingMinimum;
			timingSlider.Maximum = _hsdPlotFunction.TimingMaximum;
			timingSlider.Value = _hsdPlotFunction.Timing;
			timingTextBox.Text = _hsdPlotFunction.Timing.ToString(CultureInfo.InvariantCulture);
		}

		private double PlotFunction(double x)
		{
			return x*x;
		}

		private void gammaSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			_hsdPlotFunction.SpendingFunctionParameter = e.NewValue;
			_hsdPlotFunction.Update();
			plot.UpdatePlotDisplay();

			gammaTextBox.Text = _hsdPlotFunction.SpendingFunctionParameter.ToString(CultureInfo.InvariantCulture);
		}

		private void interimSpendingSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			_hsdPlotFunction.InterimSpendingParameter = e.NewValue;
			_hsdPlotFunction.Update();
			plot.UpdatePlotDisplay();

			plot.ControlPointPlotY = e.NewValue;
			interimSpendingTextBox.Text = _hsdPlotFunction.InterimSpendingParameter.ToString(CultureInfo.InvariantCulture);
		}

		private void timingSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			_hsdPlotFunction.Timing = e.NewValue;
			_hsdPlotFunction.Update();
			plot.UpdatePlotDisplay();

			plot.ControlPointPlotX = e.NewValue;
			timingTextBox.Text = _hsdPlotFunction.Timing.ToString(CultureInfo.InvariantCulture);
		}
	}
}