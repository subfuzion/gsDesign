namespace Subfuzion.Silverlight.UI.Charting
{
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;
	using System.Windows.Shapes;

	[TemplatePart(Name = "PART_plotCanvas", Type = typeof(Canvas))]
	public class InteractivePlot : Control
	{
		public static readonly string PlotCanvasPart = "PART_plotCanvas";

		public InteractivePlot()
		{
			DefaultStyleKey = typeof (InteractivePlot);
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			PlotCanvas = GetTemplateChild(PlotCanvasPart) as Canvas;

			if (PlotCanvas != null)
			{
				var ellipse = new Ellipse();
				ellipse.Fill = new SolidColorBrush(Colors.Red);

			}
		}

		private Canvas PlotCanvas { get; set; }

		#region Control Point

		#region IsControlPointVisible Property

		public bool IsControlPointVisible
		{
			get { return (bool) GetValue(IsControlPointVisibleProperty); }
			set { SetValue(IsControlPointVisibleProperty, value); }
		}

		public static DependencyProperty IsControlPointVisibleProperty = DependencyProperty.Register(
			"IsControlPointVisible",
			typeof (bool),
			typeof (InteractivePlot),
			new PropertyMetadata(IsControlPointVisibleChangedHandler));

		private static void IsControlPointVisibleChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnIsControlPointVisibleChanged((bool) args.NewValue, (bool) args.OldValue);
			}
		}

		protected virtual void OnIsControlPointVisibleChanged(bool newValue, bool oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnIsControlPointVisibleChanged(newValue);
		}

		protected virtual void OnIsControlPointVisibleChanged(bool newValue)
		{
			// add handler code
		}

		#endregion // IsControlPointVisible Property

		#region ControlPointPosition Property

		public Point ControlPointPosition
		{
			get { return (Point) GetValue(ControlPointPositionProperty); }
			set { SetValue(ControlPointPositionProperty, value); }
		}

		public static DependencyProperty ControlPointPositionProperty = DependencyProperty.Register(
			"ControlPointPosition",
			typeof (Point),
			typeof (InteractivePlot),
			new PropertyMetadata(ControlPointPositionChangedHandler));

		private static void ControlPointPositionChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnControlPointPositionChanged((Point) args.NewValue, (Point) args.OldValue);
			}
		}

		protected virtual void OnControlPointPositionChanged(Point newValue, Point oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnControlPointPositionChanged(newValue);
		}

		protected virtual void OnControlPointPositionChanged(Point newValue)
		{
			// add handler code
		}

		#endregion // ControlPointPosition Property

		#region ControlPointShape Property

		public Shape ControlPointShape
		{
			get { return (Shape) GetValue(ControlPointShapeProperty); }
			set { SetValue(ControlPointShapeProperty, value); }
		}

		public static DependencyProperty ControlPointShapeProperty = DependencyProperty.Register(
			"ControlPointShape",
			typeof (Shape),
			typeof (InteractivePlot),
			new PropertyMetadata(ControlPointShapeChangedHandler));

		private static void ControlPointShapeChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnControlPointShapeChanged((Shape) args.NewValue, (Shape) args.OldValue);
			}
		}

		protected virtual void OnControlPointShapeChanged(Shape newValue, Shape oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnControlPointShapeChanged(newValue);
		}

		protected virtual void OnControlPointShapeChanged(Shape newValue)
		{
			// add handler code
		}

		#endregion // ControlPointShape Property




		#endregion

	}
}