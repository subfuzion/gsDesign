namespace PlotControl
{
	using System;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Data;
	using Subfuzion.Silverlight.UI.Charting;

	public partial class HwangShiDeCaniSpendingFunctionView : UserControl
	{
		private PowerPlotFunction _plotFunction;

		public HwangShiDeCaniSpendingFunctionView()
		{
			InitializeComponent();

			plot.Logger = msg =>
			{
				log.Text += msg + "\n";
				scrollView.ScrollToBottom();
				log.SelectionStart = log.Text.Length;
			};

			Loaded += OnLoaded;
		}

		/// <summary>
		/// Provides application-wide access to the view model
		/// </summary>
		public SpendingFunctionViewModel ViewModel
		{
			get { return (SpendingFunctionViewModel)Resources["spendingFunctionViewModel"]; }
		}

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			//_plotFunction = new HwangShihDeCaniPlotFunction
			//{
			//    InterimSpendingParameter = 0.025,
			//    InterimSpendingParameterMaximum = 0.025,
			//    InterimSpendingParameterMinimum = 0.0,
			//    SpendingFunctionParameter = -8.0,
			//    SpendingFunctionParameterMaximum = 40.0,
			//    SpendingFunctionParameterMinimum = -40.0,
			//    Timing = 0.5,
			//    TimingMaximum = 1.0,
			//    TimingMinimum = 0.0,
			//};

			//_plotFunction = new OneParameterSpendingFunction
			//{
			//    SpendingFunction = OneParameterSpendingFunctions.HwangShihDeCaniFunction,
			//    InverseSpendingFunction = OneParameterSpendingFunctions.HwangShihDeCaniFunctionInverse,
			//    ParameterSpendingFunction = OneParameterSpendingFunctions.HwangShihDeCaniFunctionSpendingParameter,
			//    InterimSpendingParameter = 0.025,
			//    InterimSpendingParameterMaximum = 0.025,
			//    InterimSpendingParameterMinimum = 0.0,
			//    SpendingFunctionParameter = -8.0,
			//    SpendingFunctionParameterMaximum = 40.0,
			//    SpendingFunctionParameterMinimum = -40.0,
			//    Timing = 0.5,
			//    TimingMaximum = 1.0,
			//    TimingMinimum = 0.0,
			//};

			_plotFunction = new PowerPlotFunction
			{
				SpendingFunctionParameter = 4,
				SpendingFunctionParameterMaximum = 15.0,
				SpendingFunctionParameterMinimum = 0.001,
				InterimSpendingParameter = 0.025,
				InterimSpendingParameterMaximum = 0.025,
				InterimSpendingParameterMinimum = 0.0,
				Timing = 0.5,
				TimingMaximum = 1.0,
				TimingMinimum = 0.0,
			};

			interimSpending.DataContext = _plotFunction;
			timing.DataContext = _plotFunction;
			spendingFunctionParameter.DataContext = _plotFunction;

			_plotFunction.Update();

			plot.Coordinates = _plotFunction.Coordinates;

			gammaSlider.Minimum = _plotFunction.SpendingFunctionParameterMinimum;
			gammaSlider.Maximum = _plotFunction.SpendingFunctionParameterMaximum;
			//gammaSlider.Value = _plotFunction.SpendingFunctionParameter;
			//gammaTextBox.Text = _plotFunction.SpendingFunctionParameter.ToString(CultureInfo.InvariantCulture);

			interimSpendingSlider.Minimum = _plotFunction.InterimSpendingParameterMinimum;
			interimSpendingSlider.Maximum = _plotFunction.InterimSpendingParameterMaximum;
			// interimSpendingSlider.Value = _plotFunction.InterimSpendingParameter;
			// interimSpendingTextBox.Text = _plotFunction.InterimSpendingParameter.ToString(CultureInfo.InvariantCulture);

			timingSlider.Minimum = _plotFunction.TimingMinimum;
			timingSlider.Maximum = _plotFunction.TimingMaximum;
			//timingSlider.Value = _plotFunction.Timing;
			//timingTextBox.Text = _plotFunction.Timing.ToString(CultureInfo.InvariantCulture);

			RegisterForNotification("ControlPointPhysicalPosition", plot, (o, args) =>
			{
			    var point = plot.PhysicalToLogicalCoordinates((Point)args.NewValue);
			    //_plotFunction.Update(point.X, point.Y);
			    _plotFunction.Timing = point.X;
			    _plotFunction.InterimSpendingParameter = point.Y;
			});
		}

		private void gammaSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			try
			{
			    _plotFunction.SpendingFunctionParameter = e.NewValue;
			    _plotFunction.Update();
			    plot.UpdatePlotDisplay();
			}
			catch (Exception exception)
			{
			    Console.WriteLine(exception);
			}
		}

		private void interimSpendingSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			_plotFunction.InterimSpendingParameter = e.NewValue;
			_plotFunction.Update();
			plot.UpdatePlotDisplay();

			plot.ControlPointPlotY = e.NewValue;
			//interimSpendingTextBox.Text = _plotFunction.InterimSpendingParameter.ToString(CultureInfo.InvariantCulture);
			plot.ControlPointPlotX = _plotFunction.Timing;
		}

		private void timingSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			// move point along the line

			double x = e.NewValue;
			_plotFunction.Timing = x;
			_plotFunction.Update();


			plot.UpdatePlotDisplay();

			plot.ControlPointPlotX = e.NewValue;
			//timingTextBox.Text = _plotFunction.Timing.ToString(CultureInfo.InvariantCulture);

			plot.ControlPointPlotY = _plotFunction.InterimSpendingParameter;
		}

		private void moveLineRadioButton_Checked(object sender, RoutedEventArgs e)
		{
			if (_plotFunction != null)
			    _plotFunction.PlotConstraint = PlotConstraint.MoveLineWithPoint;
		}

		private void movePointRadioButton_Checked(object sender, RoutedEventArgs e)
		{
			if (_plotFunction != null)
			    _plotFunction.PlotConstraint = PlotConstraint.MovePointAlongLine;
		}

		// Listen for change of the dependency property  
		public void RegisterForNotification(string propertyName, FrameworkElement element, PropertyChangedCallback callback)
		{
			// bind to dependency property  
			var b = new Binding(propertyName) {Source = element};
			var prop = DependencyProperty.RegisterAttached(
			    "NotifyAttached" + propertyName,
			    typeof (object),
			    typeof (UserControl),
			    new PropertyMetadata(callback));

			element.SetBinding(prop, b);
		}
	}
}