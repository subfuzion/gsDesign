namespace gsDesign.Explorer.Views.Design.SpendingFunctions.OneParameter
{
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Controls.DataVisualization;
	using System.Windows.Controls.DataVisualization.Charting;
	using System.Windows.Input;
	using System.Windows.Media;
	using Subfuzion.Helpers.UI;
	using ViewModels.Design.SpendingFunctions.OneParameter;

	public partial class OneParameterView : UserControl
	{
		private static readonly Brush NormalBrushFill = new SolidColorBrush(Colors.Orange);
		private static readonly Brush NormalBrushStroke = new SolidColorBrush(Colors.Black);
		private static readonly Brush HoverBrushFill = new SolidColorBrush(Colors.White);
		private static readonly Brush HoverBrushStroke = new SolidColorBrush(Colors.Black);
		private static readonly Brush DragBrushFill = new SolidColorBrush(Colors.White);
		private static readonly Brush DragBrushStroke = new SolidColorBrush(Colors.Red);

		private bool isDragging;
		private Point lastPosition;

		public OneParameterView()
		{
			InitializeComponent();

			Loaded += OneParameterView_Loaded;
		}

		private string Output
		{
			get { return output.Text; }
			set { output.Text = value; }
		}

		private void OneParameterView_Loaded(object sender, RoutedEventArgs e)
		{
			SetControlPointNormal();

			InitializeHandlers();

	
			//var points = lineSeries.Points;
			//int i = 0;
			//foreach (var point in points)
			//{
			//    AppendOutput(string.Format("point[{0}] x={1}, y={2}", i++, point.X, point.Y));
			//}


			var element = (Series) chart.Series[0];
			overlay.Width = element.ActualWidth;
			overlay.Height = element.ActualHeight;
		}

		private void InitializeHandlers()
		{
			var sfViewModel = DataContext as OneParameterSpendingFunctionViewModel;
			if (sfViewModel != null)
			{
				sfViewModel.PropertyChanged += (sender, args) =>
				{
					if (args.PropertyName == "PlotData")
					{
						Output = string.Empty;
						for (var i = 0; i < sfViewModel.PlotData.Count; i++)
						{
							var plotItem = sfViewModel.PlotData[i];

							var text = string.Format("{0} -> ({1}, {2})", i, plotItem.X, plotItem.Y);
							AppendOutput(text);
						}

						//UpdateIntercept();
					}
					else if (args.PropertyName == "Intercept")
					{
						UpdateIntercept(sender as OneParameterSpendingFunctionViewModel);
					}
				};
			}


			Chart element = chart;
			// var element = (Series) chart.Series[0];
			element.MouseEnter += (sender, args) => element.MouseMove += HandleOnMouseMove;
			element.MouseLeave += (sender, args) => element.MouseMove -= HandleOnMouseMove;
			element.MouseLeftButtonUp += ElementOnMouseLeftButtonUp;

			controlPoint.MouseEnter += (sender, args) => SetControlPointHover();
			controlPoint.MouseLeave += (sender, args) => SetControlPointNormal();
			controlPoint.MouseLeftButtonDown += ControlPointOnMouseLeftButtonDown;
			controlPoint.MouseLeftButtonUp += ControlPointOnMouseLeftButtonUp;
		}

		private void ElementOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			isDragging = false;
			SetControlPointNormal();
		}

		private void ControlPointOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			isDragging = false;
			SetControlPointNormal();
		}

		private void ControlPointOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			isDragging = true;
			SetControlPointDrag();
			lastPosition = mouseButtonEventArgs.GetPosition((Series) chart.Series[0]);
			chart.CaptureMouse();
		}

		private bool InBounds(Point p, FrameworkElement element)
		{
			bool inBounds = (p.X >= 0 && p.X < element.ActualWidth && p.Y >= 0 && p.Y < element.ActualHeight) ? true : false;
			return inBounds;
		}

		private void HandleOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
		{
			var series = (Series) chart.Series[0];

			var xAxis = (CategoryAxis) chart.Axes[0];
			var yAxis = (LinearAxis) chart.Axes[1];

			var xval = xAxis.GetCategoryAtPosition(new UnitValue(100, Unit.Pixels));

			var x = xAxis.GetPlotAreaCoordinate(0.5.ToString());
			var y = yAxis.GetPlotAreaCoordinate(0.5);

			//var lineSeries = sender as LineSeries;

			if (InBounds(mouseEventArgs.GetPosition(series), series))
			{
				ShowCoordinate(mouseEventArgs.GetPosition(series));

				if (isDragging)
				{
					SetControlPointDrag();

					var p = mouseEventArgs.GetPosition(series);
					controlPoint.SetValue(Canvas.LeftProperty, p.X);
					controlPoint.SetValue(Canvas.TopProperty, p.Y);

					UpdatePlotData(p);
				}
			}
		}

		private void AppendOutput(string text)
		{
			Output = string.Format("{0}\n{1}", Output, text);
		}

		private void ShowCoordinate(Point p)
		{
			Output = string.Format("({0}, {1})", p.X, p.Y);
		}

		private void SetControlPointNormal()
		{
			controlPoint.Fill = NormalBrushFill;
			controlPoint.Stroke = NormalBrushStroke;
		}

		private void SetControlPointHover()
		{
			controlPoint.Fill = HoverBrushFill;
			controlPoint.Stroke = HoverBrushStroke;
		}

		private void SetControlPointDrag()
		{
			controlPoint.Stroke = DragBrushFill;
			controlPoint.Stroke = DragBrushStroke;
		}

		private void overlay_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (lineSeries != null)
			{
				var parent = lineSeries.Parent;
			}
		}

		private CoordinateTransformer _coordinateTransformer;

		private void UpdateIntercept(OneParameterSpendingFunctionViewModel viewModel)
		{
			var series = (LineSeries)chart.Series[0];
			var width = series.ActualWidth;
			var height = series.ActualHeight;

			var x = viewModel.Intercept.X * width;
			var y = viewModel.Intercept.Y * height;

			if (_coordinateTransformer == null)
			{
				_coordinateTransformer = new CoordinateTransformer(chart, series);
			}

			var intercept = _coordinateTransformer.TransformToContainerCoordinates(new Point(x, y));

			y = -(y - height);

			controlPoint.SetValue(Canvas.LeftProperty, x);
			controlPoint.SetValue(Canvas.TopProperty, y);
		}

		private void UpdatePlotData(Point p)
		{
			// ViewModels.UpdatePlotData(p.X, p.Y);
		}
	}

}