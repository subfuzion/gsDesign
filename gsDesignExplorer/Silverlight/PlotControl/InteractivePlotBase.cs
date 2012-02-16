namespace Subfuzion.Silverlight.UI.Charting
{
	using System;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;

	public class InteractivePlotBase : Control
	{
		public static readonly string PlotCanvasPart = "PART_plotCanvas";

		public override void OnApplyTemplate()
		{
			Log("OnApplyTemplate");

			base.OnApplyTemplate();

			SizeChanged += (sender, args) => OnSizeChanged(args.NewSize);
		}

		#region Dependency Properties

		#region PlotCanvas

		public static DependencyProperty PlotCanvasProperty = DependencyProperty.Register(
			"PlotCanvas",
			typeof (Canvas),
			typeof (InteractivePlot),
			new PropertyMetadata(PlotCanvasChangedHandler));

		public Canvas PlotCanvas
		{
			get { return (Canvas) GetValue(PlotCanvasProperty); }
			set { SetValue(PlotCanvasProperty, value); }
		}

		private static void PlotCanvasChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnPlotCanvasChanged((Canvas) args.NewValue, (Canvas) args.OldValue);
			}
		}

		protected virtual void OnPlotCanvasChanged(Canvas newValue, Canvas oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			if (oldValue != null)
			{
				oldValue.SizeChanged -= PlotCanvasOnSizeChanged;
			}

			OnPlotCanvasChanged(newValue);
		}

		protected virtual void OnPlotCanvasChanged(Canvas newValue)
		{
			// add handler code
			PlotWidth = newValue != null ? newValue.ActualWidth : 0;
			PlotHeight = newValue != null ? newValue.ActualHeight : 0;

			PlotCanvas.SizeChanged += PlotCanvasOnSizeChanged;
		}

		#endregion

		#region PlotWidth

		public static DependencyProperty PlotWidthProperty = DependencyProperty.Register(
			"PlotWidth",
			typeof (double),
			typeof (InteractivePlot),
			new PropertyMetadata(PlotWidthChangedHandler));

		public double PlotWidth
		{
			get { return (double) GetValue(PlotWidthProperty); }
			set { SetValue(PlotWidthProperty, value); }
		}

		private static void PlotWidthChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnPlotWidthChanged((double) args.NewValue, (double) args.OldValue);
			}
		}

		protected virtual void OnPlotWidthChanged(double newValue, double oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnPlotWidthChanged(newValue);
		}

		protected virtual void OnPlotWidthChanged(double newValue)
		{
			// add handler code
		}

		#endregion

		#region PlotHeight

		public static DependencyProperty PlotHeightProperty = DependencyProperty.Register(
			"PlotHeight",
			typeof (double),
			typeof (InteractivePlot),
			new PropertyMetadata(PlotHeightChangedHandler));

		public double PlotHeight
		{
			get { return (double) GetValue(PlotHeightProperty); }
			set { SetValue(PlotHeightProperty, value); }
		}

		private static void PlotHeightChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnPlotHeightChanged((double) args.NewValue, (double) args.OldValue);
			}
		}

		protected virtual void OnPlotHeightChanged(double newValue, double oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnPlotHeightChanged(newValue);
		}

		protected virtual void OnPlotHeightChanged(double newValue)
		{
			// add handler code
		}

		#endregion

		#region MinimumLogicalCoordinate

		public static DependencyProperty MinimumLogicalCoordinateProperty = DependencyProperty.Register(
			"MinimumLogicalCoordinate",
			typeof (Point),
			typeof (InteractivePlot),
			new PropertyMetadata(MinimumLogicalCoordinateChangedHandler));

		public Point MinimumLogicalCoordinate
		{
			get { return (Point) GetValue(MinimumLogicalCoordinateProperty); }
			set { SetValue(MinimumLogicalCoordinateProperty, value); }
		}

		private static void MinimumLogicalCoordinateChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnMinimumLogicalCoordinateChanged((Point) args.NewValue, (Point) args.OldValue);
			}
		}

		protected virtual void OnMinimumLogicalCoordinateChanged(Point newValue, Point oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnMinimumLogicalCoordinateChanged(newValue);
		}

		protected virtual void OnMinimumLogicalCoordinateChanged(Point newValue)
		{
			// add handler code
		}

		#endregion

		#region MaximumLogicalCoordinate

		public static readonly Point DefaultMaximumLogicalCoordinate = new Point(1.0, 1.0);

		public static DependencyProperty MaximumLogicalCoordinateProperty = DependencyProperty.Register(
			"MaximumLogicalCoordinate",
			typeof (Point),
			typeof (InteractivePlot),
			new PropertyMetadata(DefaultMaximumLogicalCoordinate, MaximumLogicalCoordinateChangedHandler));

		public Point MaximumLogicalCoordinate
		{
			get { return (Point) GetValue(MaximumLogicalCoordinateProperty); }
			set { SetValue(MaximumLogicalCoordinateProperty, value); }
		}

		private static void MaximumLogicalCoordinateChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnMaximumLogicalCoordinateChanged((Point) args.NewValue, (Point) args.OldValue);
			}
		}

		protected virtual void OnMaximumLogicalCoordinateChanged(Point newValue, Point oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnMaximumLogicalCoordinateChanged(newValue);
		}

		protected virtual void OnMaximumLogicalCoordinateChanged(Point newValue)
		{
			// add handler code
		}

		#region PlotPrecision Property

		public static readonly int DefaultPlotPrecision = 3;

		public static DependencyProperty ControlPointPlotPrecisionProperty = DependencyProperty.Register(
			"ControlPointPlotPrecision",
			typeof(int),
			typeof(InteractivePlot),
			new PropertyMetadata(DefaultPlotPrecision, ControlPointPlotPrecisionChangedHandler));

		public int ControlPointPlotPrecision
		{
			get { return (int)GetValue(ControlPointPlotPrecisionProperty); }
			set { SetValue(ControlPointPlotPrecisionProperty, value); }
		}

		private static void ControlPointPlotPrecisionChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnControlPointPlotPrecisionChanged((int)args.NewValue, (int)args.OldValue);
			}
		}

		protected virtual void OnControlPointPlotPrecisionChanged(int newValue, int oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnControlPointPlotPrecisionChanged(newValue);
		}

		protected virtual void OnControlPointPlotPrecisionChanged(int newValue)
		{
			// add handler code
		}

		#endregion ControlPointPlotPrecision Property

		#endregion

		#endregion

		#region Handlers

		protected void PlotCanvasOnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
		{
			//OnSizeChanged(sizeChangedEventArgs.NewSize);
		}

		#endregion

		#region Overrides

		protected virtual void OnSizeChanged(Size newSize)
		{
			Log("OnSizeChanged", "physical size: ({0}, {1})", newSize.Width, newSize.Height);
			ClipToBounds(newSize.Width, newSize.Height);

			PlotWidth = newSize.Width;
			PlotHeight = newSize.Height;

			UpdatePlotDisplay();
		}

		public virtual void UpdatePlotDisplay()
		{
		}

		#endregion

		#region Helpers

		protected static void SetPosition(DependencyObject element, Point p)
		{
			SetPosition(element, p.X, p.Y);
		}

		private static void SetPosition(DependencyObject element, double x, double y)
		{
			element.SetValue(Canvas.LeftProperty, x);
			element.SetValue(Canvas.TopProperty, y);
		}

		private static bool InBounds(Point p, FrameworkElement element)
		{
			return p.X >= 0 && p.X < element.ActualWidth && p.Y >= 0 && p.Y < element.ActualHeight;
		}

		#endregion

		#region Implementation

		private void ClipToBounds(Size size)
		{
			ClipToBounds(size.Width, size.Height);
		}

		protected void ClipToBounds(double width, double height)
		{
			if (PlotCanvas != null)
			{
				PlotCanvas.Clip = new RectangleGeometry { Rect = new Rect(0, 0, width, height) };
			}
		}

		#endregion

		#region Public Methods

		public Point LogicalToPhysicalCoordinates(Point p)
		{
			double logWidth = MaximumLogicalCoordinate.X - MinimumLogicalCoordinate.X;
			double logHeight = MaximumLogicalCoordinate.Y - MinimumLogicalCoordinate.Y;

			double x = Math.Round((p.X/logWidth)*(ActualWidth - 1), ControlPointPlotPrecision);
			double y = Math.Round((p.Y/logHeight)*(ActualHeight - 1), ControlPointPlotPrecision);

			//double x = Math.Round((p.X / logWidth) * (PlotWidth - 1), ControlPointPlotPrecision);
			//double y = Math.Round((p.Y / logHeight) * (PlotHeight - 1), ControlPointPlotPrecision);

			var physPoint = new Point(x, y);
			return physPoint;
		}

		public Point PhysicalToLogicalCoordinates(Point p)
		{
			double logWidth = MaximumLogicalCoordinate.X - MinimumLogicalCoordinate.X;
			double logHeight = MaximumLogicalCoordinate.Y - MinimumLogicalCoordinate.Y;

			double logX = Math.Round((p.X/(ActualWidth - 1))*logWidth, ControlPointPlotPrecision);
			double logY = Math.Round((p.Y/(ActualHeight - 1))*logHeight, ControlPointPlotPrecision);

			var logPoint = new Point(logX, logY);
			return logPoint;
		}

		#endregion

		#region Debugging Support

		#region Logger property

		public Action<string> Logger
		{
			get { return (Action<string>) GetValue(LoggerProperty); }
			set { SetValue(LoggerProperty, value); }
		}

		public static DependencyProperty LoggerProperty = DependencyProperty.Register(
			"Logger",
			typeof (Action<string>),
			typeof (InteractivePlot),
			new PropertyMetadata(LoggerChangedHandler));

		private static void LoggerChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnLoggerChanged((Action<string>) args.NewValue, (Action<string>) args.OldValue);
			}
		}

		protected virtual void OnLoggerChanged(Action<string> newValue, Action<string> oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnLoggerChanged(newValue);
		}

		protected virtual void OnLoggerChanged(Action<string> newValue)
		{
			// add handler code
		}

		#endregion

		protected void Log(string function, string message = "", params object[] args)
		{
			if (Logger != null)
			{
				// var log = string.Format("[{0}] {1}", GetType().Name, string.Format(message, args));
				var log = string.Format("[{0}] {1}", function, string.Format(message, args));
				Logger(log);
			}
		}

		#endregion

	}
}