namespace Subfuzion.Silverlight.UI.Charting
{
	using System;
	using System.ComponentModel;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;

	public class LinePlotBase : Control, INotifyPropertyChanged
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

		public static DependencyProperty PlotSurfaceProperty = DependencyProperty.Register(
			"PlotSurface",
			typeof (Canvas),
			typeof (LinePlot),
			new PropertyMetadata(PlotSurfaceChangedHandler));

		public Canvas PlotSurface
		{
			get { return (Canvas) GetValue(PlotSurfaceProperty); }
			set { SetValue(PlotSurfaceProperty, value); }
		}

		private static void PlotSurfaceChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as LinePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnPlotSurfaceChanged((Canvas) args.NewValue, (Canvas) args.OldValue);
			}
		}

		protected virtual void OnPlotSurfaceChanged(Canvas newValue, Canvas oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			if (oldValue != null)
			{
				oldValue.SizeChanged -= OnPlotSurfaceSizeChanged;
			}

			OnPlotSurfaceChanged(newValue);
		}

		protected virtual void OnPlotSurfaceChanged(Canvas newValue)
		{
			// add handler code
			PhysicalWidth = newValue != null ? newValue.ActualWidth : 0;
			PhysicalHeight = newValue != null ? newValue.ActualHeight : 0;

			PlotSurface.SizeChanged += OnPlotSurfaceSizeChanged;
		}

		#endregion

		#region PhysicalWidth

		public static DependencyProperty PhysicalWidthProperty = DependencyProperty.Register(
			"PhysicalWidth",
			typeof (double),
			typeof (LinePlot),
			new PropertyMetadata(PhysicalWidthChangedHandler));

		public double PhysicalWidth
		{
			get { return (double) GetValue(PhysicalWidthProperty); }
			set { SetValue(PhysicalWidthProperty, value); }
		}

		private static void PhysicalWidthChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as LinePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnPhysicalWidthChanged((double) args.NewValue, (double) args.OldValue);
			}
		}

		protected virtual void OnPhysicalWidthChanged(double newValue, double oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnPhysicalWidthChanged(newValue);
		}

		protected virtual void OnPhysicalWidthChanged(double newValue)
		{
		}

		#endregion

		#region PhysicalHeight

		public static DependencyProperty PhysicalHeightProperty = DependencyProperty.Register(
			"PhysicalHeight",
			typeof (double),
			typeof (LinePlot),
			new PropertyMetadata(PhysicalHeightChangedHandler));

		public double PhysicalHeight
		{
			get { return (double) GetValue(PhysicalHeightProperty); }
			set { SetValue(PhysicalHeightProperty, value); }
		}

		private static void PhysicalHeightChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as LinePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnPhysicalHeightChanged((double) args.NewValue, (double) args.OldValue);
			}
		}

		protected virtual void OnPhysicalHeightChanged(double newValue, double oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnPhysicalHeightChanged(newValue);
		}

		protected virtual void OnPhysicalHeightChanged(double newValue)
		{
		}

		#endregion

		#region MaximumPhysicalCoordinate property

		private double _maximumPhysicalCoordinate;

		/// <summary>
		/// Gets the MaximumPhysicalCoordinate property.
		/// </summary>
		public Point MaximumPhysicalCoordinate
		{
			get { return LogicalToPhysicalCoordinates(MaximumLogicalCoordinate);}
		}

		#endregion

		#region MinimumLogicalCoordinate

		public static readonly Point DefaultMinimumLogicalCoordinate = new Point(0.0, 0.0);

		public static DependencyProperty MinimumLogicalCoordinateProperty = DependencyProperty.Register(
			"MinimumLogicalCoordinate",
			typeof (Point),
			typeof (LinePlot),
			new PropertyMetadata(DefaultMinimumLogicalCoordinate, MinimumLogicalCoordinateChangedHandler));

		public Point MinimumLogicalCoordinate
		{
			get { return (Point) GetValue(MinimumLogicalCoordinateProperty); }
			set { SetValue(MinimumLogicalCoordinateProperty, value); }
		}

		private static void MinimumLogicalCoordinateChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as LinePlot;
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
			typeof (LinePlot),
			new PropertyMetadata(DefaultMaximumLogicalCoordinate, MaximumLogicalCoordinateChangedHandler));

		public Point MaximumLogicalCoordinate
		{
			get { return (Point) GetValue(MaximumLogicalCoordinateProperty); }
			set { SetValue(MaximumLogicalCoordinateProperty, value); }
		}

		private static void MaximumLogicalCoordinateChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as LinePlot;
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

		public static DependencyProperty PlotPrecisionProperty = DependencyProperty.Register(
			"PlotPrecision",
			typeof(int),
			typeof(LinePlot),
			new PropertyMetadata(DefaultPlotPrecision, PlotPrecisionChangedHandler));

		public int PlotPrecision
		{
			get { return (int)GetValue(PlotPrecisionProperty); }
			set { SetValue(PlotPrecisionProperty, value); }
		}

		private static void PlotPrecisionChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as LinePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnPlotPrecisionChanged((int)args.NewValue, (int)args.OldValue);
			}
		}

		protected virtual void OnPlotPrecisionChanged(int newValue, int oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnPlotPrecisionChanged(newValue);
		}

		protected virtual void OnPlotPrecisionChanged(int newValue)
		{
			// add handler code
		}

		#endregion ControlPointPlotPrecision Property

		#endregion

		#endregion

		#region Handlers

		protected void OnPlotSurfaceSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
		{
			OnPlotSurfaceSizeChanged(sizeChangedEventArgs.NewSize);
		}

		#endregion

		#region Overridable methods

		protected virtual void OnSizeChanged(Size newSize)
		{
			Log("OnSizeChanged", "physical size: ({0}, {1})", newSize.Width, newSize.Height);

			ClipToBounds(newSize.Width, newSize.Height);

			PhysicalWidth = newSize.Width;
			PhysicalHeight = newSize.Height;
			NotifyPropertyChanged("MaximumPhysicalCoordinate");

			UpdatePlotDisplay();
		}

		protected virtual void OnPlotSurfaceSizeChanged(Size newSize)
		{
			Log("OnPlotSurfaceSizeChanged", "physical coordinates: ({0}, {1})", newSize.Width, newSize.Height);
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
			if (double.IsNaN(x) || double.IsNaN(y) || double.IsInfinity(x) || double.IsInfinity(y) || double.IsNegativeInfinity(x) || double.IsNegativeInfinity(y)) return;

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
			if (PlotSurface != null)
			{
				PlotSurface.Clip = new RectangleGeometry { Rect = new Rect(0, 0, width, height) };
			}
		}

		#endregion

		#region Public Methods

		public Point LogicalToPhysicalCoordinates(Point p)
		{
			double logWidth = MaximumLogicalCoordinate.X - MinimumLogicalCoordinate.X;
			double logHeight = MaximumLogicalCoordinate.Y - MinimumLogicalCoordinate.Y;

			var width = PhysicalWidth; // ActualWidth;
			var height = PhysicalHeight; // ActualHeight;

			double x = Math.Round((p.X / logWidth) * (width - 1), PlotPrecision);
			double y = Math.Round((p.Y / logHeight) * (height - 1), PlotPrecision);

			var physPoint = new Point(x, y);
			return physPoint;
		}

		public Point PhysicalToLogicalCoordinates(Point p)
		{
			double logWidth = MaximumLogicalCoordinate.X - MinimumLogicalCoordinate.X;
			double logHeight = MaximumLogicalCoordinate.Y - MinimumLogicalCoordinate.Y;

			var width = PhysicalWidth; // ActualWidth;
			var height = PhysicalHeight; // ActualHeight;

			double logX = Math.Round((p.X / (width - 1)) * logWidth, PlotPrecision);
			double logY = Math.Round((p.Y / (height - 1)) * logHeight, PlotPrecision);

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
			typeof (LinePlot),
			new PropertyMetadata(LoggerChangedHandler));

		private static void LoggerChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as LinePlot;
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
			LogOutput = string.Format("[{0}.{1}] {2}", GetType().Name, function, string.Format(message, args));
			if (Logger != null)
			{
				Logger(LogOutput);
			}
		}

		#region LogOutput property

		public string LogOutput
		{
			get { return (string) GetValue(LogOutputProperty); }
			set { SetValue(LogOutputProperty, value); }
		}

		public static DependencyProperty LogOutputProperty = DependencyProperty.Register(
			"LogOutput",
			typeof (string),
			typeof (LinePlot),
			new PropertyMetadata(LogOutputChangedHandler));

		private static void LogOutputChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var linePlot = dependencyObject as LinePlot;
			if (linePlot != null)
			{
				linePlot.OnLogOutputChanged((string) args.NewValue, (string) args.OldValue);
			}
		}

		protected virtual void OnLogOutputChanged(string newValue, string oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnLogOutputChanged(newValue);
		}

		protected virtual void OnLogOutputChanged(string newValue)
		{
			// add handler code
		}

		#endregion



		#endregion

		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged(string propertyName)
		{
			var handlers = PropertyChanged;
			if (handlers != null)
			{
				handlers(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}