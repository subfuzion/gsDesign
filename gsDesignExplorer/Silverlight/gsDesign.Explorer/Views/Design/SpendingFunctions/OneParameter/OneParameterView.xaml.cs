namespace gsDesign.Explorer.Views.Design.SpendingFunctions.OneParameter
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows.Controls;
	using System.Windows.Input;

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

			chart.MouseEnter += (sender, args) => chart.MouseMove += ChartOnMouseMove;
			chart.MouseLeave += (sender, args) => chart.MouseMove -= ChartOnMouseMove;

			Loaded += new System.Windows.RoutedEventHandler(OneParameterView_Loaded);
		}

		void OneParameterView_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			//var points = lineSeries.Points;
			//int i = 0;
			//foreach (var point in points)
			//{
			//    AppendOutput(string.Format("point[{0}] x={1}, y={2}", i++, point.X, point.Y));
			//}
		}

		private void ChartOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
		{
			
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
	}
}