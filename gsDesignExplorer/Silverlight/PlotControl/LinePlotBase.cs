namespace Subfuzion.Silverlight.UI.Charting
{
	using System;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Input;
	using System.Windows.Media;
	using System.Windows.Shapes;

	public delegate bool CanDragTo(LinePlotBase linePlot, ref Point p);

	public class LinePlotBase : Control, INotifyPropertyChanged
	{
		public static readonly string PlotCanvasPart = "PART_plotCanvas";

		private bool isDraggingA;
		private bool isDraggingB;

		public override void OnApplyTemplate()
		{
			Log("OnApplyTemplate");

			base.OnApplyTemplate();

			if (DesignerProperties.GetIsInDesignMode(this)) return;

			SizeChanged += (sender, args) => OnSizeChanged(args.NewSize);

			PlotSurface = GetTemplateChild(PlotCanvasPart) as Canvas;
			if (PlotSurface != null)
			{
				// Ensure all the children are added

				if (Polyline != null) PlotSurface.Children.Add(Polyline);

				if (DragHandleA != null && !PlotSurface.Children.Contains(DragHandleA))
				{
					DragHandleA.Logger = Log;
					PlotSurface.Children.Add(DragHandleA);
					DragHandleA.MouseLeftButtonDown += DragHandleAOnMouseLeftButtonDown;
					DragHandleA.MouseLeftButtonUp += DragHandleAOnMouseLeftButtonUp;
				}

				if (DragHandleB != null && !PlotSurface.Children.Contains(DragHandleB))
				{
					DragHandleB.Logger = Log;
					PlotSurface.Children.Add(DragHandleB);
					DragHandleB.MouseLeftButtonDown += DragHandleBOnMouseLeftButtonDown;
					DragHandleB.MouseLeftButtonUp += DragHandleBOnMouseLeftButtonUp;
				}

//				UpdatePlotDisplay();
//				UpdateControlPointStateDisplay();
			}
		}

		#region Dependency Properties

		#region PlotSurface

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

		#region Coordinates

		public static DependencyProperty CoordinatesProperty = DependencyProperty.Register(
			"Coordinates",
			typeof(ObservableCollection<Point>),
			typeof(LinePlot),
			new PropertyMetadata(CoordinatesChangedHandler));

		public ObservableCollection<Point> Coordinates
		{
			get { return (ObservableCollection<Point>)GetValue(CoordinatesProperty); }
			set { SetValue(CoordinatesProperty, value); }
		}

		private static void CoordinatesChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as LinePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnCoordinatesChanged((ObservableCollection<Point>)args.NewValue,
					(ObservableCollection<Point>)args.OldValue);
			}
		}

		protected virtual void OnCoordinatesChanged(ObservableCollection<Point> newCoordinates,
			ObservableCollection<Point> oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			if (newCoordinates != oldValue)
			{
				newCoordinates.CollectionChanged += (sender, args) => OnCoordinatesChanged(newCoordinates);
			}
			OnCoordinatesChanged(newCoordinates);
		}

		protected virtual void OnCoordinatesChanged(ObservableCollection<Point> newCoordinates)
		{
			// add handler code
			if (PlotSurface == null || newCoordinates == null || newCoordinates.Count <= 0) return;

			MinimumLogicalCoordinate = newCoordinates[0];
			MaximumLogicalCoordinate = newCoordinates[newCoordinates.Count - 1];

			Polyline.Points = new PointCollection();
			foreach (Point point in newCoordinates)
			{
				Point vertex = LogicalToPhysicalCoordinates(point);
				Polyline.Points.Add(vertex);
			}
		}

		#endregion

		#region Polyline

		public static DependencyProperty PolylineProperty = DependencyProperty.Register(
			"Polyline",
			typeof(Polyline),
			typeof(LinePlot),
			new PropertyMetadata(new Polyline { Stroke = new SolidColorBrush(Colors.Black), StrokeThickness = 1 },
				PolylineChangedHandler));

		public Polyline Polyline
		{
			get { return (Polyline)GetValue(PolylineProperty); }
			set { SetValue(PolylineProperty, value); }
		}

		private static void PolylineChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as LinePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnPolylineChanged((Polyline)args.NewValue, (Polyline)args.OldValue);
			}
		}

		protected virtual void OnPolylineChanged(Polyline newValue, Polyline oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnPolylineChanged(newValue);
		}

		protected virtual void OnPolylineChanged(Polyline newValue)
		{
			// add handler code
		}

		#endregion

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
			OnCoordinatesChanged(Coordinates);
		}

		#endregion

		#region Helpers

		protected static void SetPhysicalPosition(DependencyObject element, Point p)
		{
			SetPhysicalPosition(element, p.X, p.Y);
		}

		protected static void SetPhysicalPosition(DependencyObject element, double x, double y)
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

		#region Handlers

		private void OnPlotSurfaceSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
		{
			OnPlotSurfaceSizeChanged(sizeChangedEventArgs.NewSize);
		}

		#endregion

		protected void ClipToBounds(Size size)
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
			LogMessage = string.Format("[{0}.{1}] {2}", GetType().Name, function, string.Format(message, args));
			if (Logger != null)
			{
				Logger(LogMessage);
			}
		}

		#region LogMessage property

		public string LogMessage
		{
			get { return (string) GetValue(LogMessageProperty); }
			set { SetValue(LogMessageProperty, value); }
		}

		public static DependencyProperty LogMessageProperty = DependencyProperty.Register(
			"LogMessage",
			typeof (string),
			typeof (LinePlot),
			new PropertyMetadata(LogMessageChangedHandler));

		private static void LogMessageChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var linePlot = dependencyObject as LinePlot;
			if (linePlot != null)
			{
				linePlot.OnLogMessageChanged((string) args.NewValue, (string) args.OldValue);
			}
		}

		protected virtual void OnLogMessageChanged(string newValue, string oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnLogMessageChanged(newValue);
		}

		protected virtual void OnLogMessageChanged(string newValue)
		{
			// add handler code
		}

		#endregion

		#endregion

		#region Drag Handles

		#region DragHandleA property

		public DragHandle DragHandleA
		{
			get { return (DragHandle)GetValue(DragHandleAProperty); }
			set { SetValue(DragHandleAProperty, value); }
		}

		public static DependencyProperty DragHandleAProperty = DependencyProperty.Register(
			"DragHandleA",
			typeof(DragHandle),
			typeof(LinePlot),
			new PropertyMetadata(DragHandleAChangedHandler));

		private static void DragHandleAChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var linePlot = dependencyObject as LinePlot;
			if (linePlot != null)
			{
				linePlot.OnDragHandleAChanged((DragHandle)args.NewValue, (DragHandle)args.OldValue);
			}
		}

		protected virtual void OnDragHandleAChanged(DragHandle newValue, DragHandle oldValue)
		{
			if (oldValue != null)
			{
				oldValue.Logger = null;

				oldValue.MouseLeftButtonDown -= DragHandleAOnMouseLeftButtonDown;
				oldValue.MouseLeftButtonUp -= DragHandleAOnMouseLeftButtonUp;
				oldValue.MouseMove -= HandleDraggingDragHandleA;

				if (PlotSurface != null && PlotSurface.Children.Contains(oldValue))
				{
					//RemoveControlPointHandlers(oldValue);
					PlotSurface.Children.Remove(oldValue);
				}
			}

			OnDragHandleAChanged(newValue);
		}

		protected virtual void OnDragHandleAChanged(DragHandle newValue)
		{
			if (newValue != null)
			{
				newValue.Logger = Log;

				newValue.MouseLeftButtonDown += DragHandleAOnMouseLeftButtonDown;
				newValue.MouseLeftButtonUp += DragHandleAOnMouseLeftButtonUp;
				newValue.MouseMove += HandleDraggingDragHandleA;

				//AddControlPointHandlers(newValue);
				//newValue.Visibility = Visibility.Collapsed;
				if (PlotSurface != null) PlotSurface.Children.Add(newValue);
				//UpdateControlPointStateDisplay();
			}
		}

		#endregion

		#region DragHandleB property

		public DragHandle DragHandleB
		{
			get { return (DragHandle)GetValue(DragHandleBProperty); }
			set { SetValue(DragHandleBProperty, value); }
		}

		public static DependencyProperty DragHandleBProperty = DependencyProperty.Register(
			"DragHandleB",
			typeof(DragHandle),
			typeof(LinePlot),
			new PropertyMetadata(DragHandleBChangedHandler));

		private static void DragHandleBChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var linePlot = dependencyObject as LinePlot;
			if (linePlot != null)
			{
				linePlot.OnDragHandleBChanged((DragHandle)args.NewValue, (DragHandle)args.OldValue);
			}
		}

		protected virtual void OnDragHandleBChanged(DragHandle newValue, DragHandle oldValue)
		{
			if (oldValue != null)
			{
				oldValue.Logger = null;

				oldValue.MouseLeftButtonDown -= DragHandleBOnMouseLeftButtonDown;
				oldValue.MouseLeftButtonUp -= DragHandleBOnMouseLeftButtonUp;
				oldValue.MouseMove -= HandleDraggingDragHandleB;

				if (PlotSurface != null && PlotSurface.Children.Contains(oldValue))
				{
					//RemoveControlPointHandlers(oldValue);
					PlotSurface.Children.Remove(oldValue);
				}
			}

			OnDragHandleBChanged(newValue);
		}

		protected virtual void OnDragHandleBChanged(DragHandle newValue)
		{
			if (newValue != null)
			{
				newValue.Logger = Log;

				newValue.MouseLeftButtonDown += DragHandleBOnMouseLeftButtonDown;
				newValue.MouseLeftButtonUp += DragHandleBOnMouseLeftButtonUp;
				newValue.MouseMove += HandleDraggingDragHandleB;

				//AddControlPointHandlers(newValue);
				//newValue.Visibility = Visibility.Collapsed;
				if (PlotSurface != null) PlotSurface.Children.Add(newValue);
				//UpdateControlPointStateDisplay();
			}
		}

		#endregion

		private void DragHandleAOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			isDraggingA = true;
			MoveToTop(DragHandleA);
			DragHandleA.CaptureMouse();
		}

		private void DragHandleAOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			DragHandleA.ReleaseMouseCapture();
			isDraggingA = false;
		}

		private void DragHandleBOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			isDraggingB = true;
			MoveToTop(DragHandleB);
			DragHandleB.CaptureMouse();
		}

		private void DragHandleBOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			DragHandleB.ReleaseMouseCapture();
			isDraggingB = false;
		}

		private void HandleDraggingDragHandleA(object sender, MouseEventArgs mouseEventArgs)
		{
			if (isDraggingA)
			{
				Point p = mouseEventArgs.GetPosition(PlotSurface);

				if (p.X < 0) p.X = 0;
				if (p.X > ActualWidth - 1) p.X = ActualWidth - 1;
				if (p.Y < 0) p.Y = 0;
				if (p.Y > ActualHeight - 1) p.Y = ActualHeight - 1;

				if (DragHandleB != null)
				{
					var xlimit = (double) DragHandleB.GetValue(Canvas.LeftProperty);
					if (p.X > xlimit) p.X = xlimit;

					var ylimit = (double) DragHandleB.GetValue(Canvas.TopProperty);
					if (p.Y > ylimit) p.Y = ylimit;
				}

				//ControlPointPhysicalPosition = p;
				if (CanDragHandleA != null && !CanDragHandleA(this, ref p))
				{
					return;
				}

				SetPhysicalPosition(DragHandleA, p);
			}
		}

		private void HandleDraggingDragHandleB(object sender, MouseEventArgs mouseEventArgs)
		{
			if (isDraggingB)
			{
				Point p = mouseEventArgs.GetPosition(PlotSurface);

				if (p.X < 0) p.X = 0;
				if (p.X > ActualWidth - 1) p.X = ActualWidth - 1;
				if (p.Y < 0) p.Y = 0;
				if (p.Y > ActualHeight - 1) p.Y = ActualHeight - 1;

				if (DragHandleA != null)
				{
					var xlimit = (double)DragHandleA.GetValue(Canvas.LeftProperty);
					if (p.X < xlimit) p.X = xlimit;

					var ylimit = (double)DragHandleA.GetValue(Canvas.TopProperty);
					if (p.Y < ylimit) p.Y = ylimit;
				}

				//ControlPointPhysicalPosition = p;
				if (CanDragHandleB != null && !CanDragHandleB(this, ref p))
				{
					return;
				}

				SetPhysicalPosition(DragHandleB, p);
			}
		}

		#region CanDragHandleA property

		public CanDragTo CanDragHandleA
		{
			get { return (CanDragTo) GetValue(CanDragHandleAProperty); }
			set { SetValue(CanDragHandleAProperty, value); }
		}

		public static DependencyProperty CanDragHandleAProperty = DependencyProperty.Register(
			"CanDragHandleA",
			typeof (CanDragTo),
			typeof (LinePlot),
			new PropertyMetadata(CanDragHandleAChangedHandler));

		private static void CanDragHandleAChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var linePlot = dependencyObject as LinePlot;
			if (linePlot != null)
			{
				linePlot.OnCanDragHandleAChanged((CanDragTo) args.NewValue, (CanDragTo) args.OldValue);
			}
		}

		protected virtual void OnCanDragHandleAChanged(CanDragTo newValue, CanDragTo oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnCanDragHandleAChanged(newValue);
		}

		protected virtual void OnCanDragHandleAChanged(CanDragTo newValue)
		{
			// add handler code
		}

		#endregion

		#region CanDragHandleB property

		public CanDragTo CanDragHandleB
		{
			get { return (CanDragTo) GetValue(CanDragHandleBProperty); }
			set { SetValue(CanDragHandleBProperty, value); }
		}

		public static DependencyProperty CanDragHandleBProperty = DependencyProperty.Register(
			"CanDragHandleB",
			typeof (CanDragTo),
			typeof (LinePlot),
			new PropertyMetadata(CanDragHandleBChangedHandler));

		private static void CanDragHandleBChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var linePlot = dependencyObject as LinePlot;
			if (linePlot != null)
			{
				linePlot.OnCanDragHandleBChanged((CanDragTo) args.NewValue, (CanDragTo) args.OldValue);
			}
		}

		protected virtual void OnCanDragHandleBChanged(CanDragTo newValue, CanDragTo oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnCanDragHandleBChanged(newValue);
		}

		protected virtual void OnCanDragHandleBChanged(CanDragTo newValue)
		{
			// add handler code
		}

		#endregion

		#region DragHandleAPostion property

		public Point DragHandleAPostion
		{
			get { return (Point) GetValue(DragHandleAPostionProperty); }
			set { SetValue(DragHandleAPostionProperty, value); }
		}

		public static DependencyProperty DragHandleAPostionProperty = DependencyProperty.Register(
			"DragHandleAPostion",
			typeof (Point),
			typeof (LinePlot),
			new PropertyMetadata(DragHandleAPostionChangedHandler));

		private static void DragHandleAPostionChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var linePlot = dependencyObject as LinePlot;
			if (linePlot != null)
			{
				linePlot.OnDragHandleAPostionChanged((Point) args.NewValue, (Point) args.OldValue);
			}
		}

		protected virtual void OnDragHandleAPostionChanged(Point newValue, Point oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnDragHandleAPostionChanged(newValue);
		}

		protected virtual void OnDragHandleAPostionChanged(Point newValue)
		{
			DragHandleA.SetValue(Canvas.LeftProperty, newValue.X);
			DragHandleA.SetValue(Canvas.TopProperty, newValue.Y);
		}

		#endregion

		#region DragHandleBPostion property

		public Point DragHandleBPostion
		{
			get { return (Point) GetValue(DragHandleBPostionProperty); }
			set { SetValue(DragHandleBPostionProperty, value); }
		}

		public static DependencyProperty DragHandleBPostionProperty = DependencyProperty.Register(
			"DragHandleBPostion",
			typeof (Point),
			typeof (LinePlot),
			new PropertyMetadata(DragHandleBPostionChangedHandler));

		private static void DragHandleBPostionChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var linePlot = dependencyObject as LinePlot;
			if (linePlot != null)
			{
				linePlot.OnDragHandleBPostionChanged((Point) args.NewValue, (Point) args.OldValue);
			}
		}

		protected virtual void OnDragHandleBPostionChanged(Point newValue, Point oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnDragHandleBPostionChanged(newValue);
		}

		protected virtual void OnDragHandleBPostionChanged(Point newValue)
		{
			DragHandleB.SetValue(Canvas.LeftProperty, newValue.X);
			DragHandleB.SetValue(Canvas.TopProperty, newValue.Y);
		}

		#endregion



		#endregion

		private void MoveToTop(UIElement element)
		{
			if (element != null && PlotSurface != null && PlotSurface.Children.Contains(element))
			{
				PlotSurface.Children.Remove(element);
				PlotSurface.Children.Add(element);
			}
		}

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