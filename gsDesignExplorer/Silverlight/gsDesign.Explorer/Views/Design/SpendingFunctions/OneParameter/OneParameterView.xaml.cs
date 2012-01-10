namespace gsDesign.Explorer.Views.Design.SpendingFunctions.OneParameter
{
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Controls.DataVisualization.Charting;
	using System.Windows.Input;
	using System.Windows.Media;

	public class SampleDataItem
	{
		public string StepName { get; set; }
		public double Value { get; set; }
	}

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
			InitializeData();

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
			var element = (Series) chart.Series[0];
			// var element = sender as LineSeries;
			if (InBounds(mouseEventArgs.GetPosition(element), element))
			{
				ShowCoordinate(mouseEventArgs.GetPosition(element));

				if (isDragging)
				{
					SetControlPointDrag();

					Point p = mouseEventArgs.GetPosition(element);
					controlPoint.SetValue(Canvas.LeftProperty, p.X);
					controlPoint.SetValue(Canvas.TopProperty, p.Y);
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

		private void InitializeData()
		{
			var data = new List<SampleDataItem>();

			for (int i = 0; i < 8; i++)
			{
				data.Add(new SampleDataItem
				{
					StepName = i.ToString(CultureInfo.InvariantCulture),
					Value = i*i,
				});
			}

			DataContext = data;
		}
	}
}