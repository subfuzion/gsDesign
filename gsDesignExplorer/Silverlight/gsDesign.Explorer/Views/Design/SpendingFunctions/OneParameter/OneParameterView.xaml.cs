namespace gsDesign.Explorer.Views.Design.SpendingFunctions.OneParameter
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Controls.DataVisualization.Charting;
	using System.Windows.Input;
	using System.Windows.Shapes;

	public class SampleDataItem
	{
		public string StepName { get; set; }
		public double Value { get; set; }
	}

	public partial class OneParameterView : UserControl
	{
		public OneParameterView()
		{
			InitializeComponent();
			InitializeData();

			Loaded += OneParameterView_Loaded;
		}

		private void InitializeHandlers()
		{
			//chart.MouseMove += HandleOnMouseMove;
			var element = chart;
			// var element = (Series) chart.Series[0];
			//var element = lineSeries;
			element.MouseEnter += (sender, args) => element.MouseMove += HandleOnMouseMove;
			element.MouseLeave += (sender, args) => element.MouseMove -= HandleOnMouseMove;

		}

		void OneParameterView_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
//			InitializeHandlers();

			//var points = lineSeries.Points;
			//int i = 0;
			//foreach (var point in points)
			//{
			//    AppendOutput(string.Format("point[{0}] x={1}, y={2}", i++, point.X, point.Y));
			//}
		}

		private bool InBounds(Point p, FrameworkElement element)
		{
			var inBounds = (p.X >= 0 && p.X < element.ActualWidth && p.Y >= 0 && p.Y < element.ActualHeight) ? true : false;
			return inBounds;
		}

		private void HandleOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
		{
			//var element = (Series) chart.Series[0];
			var element = sender as LineSeries;
			if (InBounds(mouseEventArgs.GetPosition(element), element))
			{
				ShowCoordinate(mouseEventArgs.GetPosition(element));
			}
		}

		private void InitializeData()
		{
			var data = new List<SampleDataItem>();

			for (int i = 0; i < 8; i++)
			{
				data.Add(new SampleDataItem
				{
					StepName = i.ToString(CultureInfo.InvariantCulture),
					Value = i * i,
				});
			}

			DataContext = data;
		}

		private void AppendOutput(string text)
		{
			Output = string.Format("{0}\n{1}", Output, text);
		}

		private string Output
		{
			get { return output.Text; }
			set { output.Text = value; }
		}

		private void ShowCoordinate(Point p)
		{
			Output = string.Format("({0}, {1})", p.X, p.Y);
		}
	}
}