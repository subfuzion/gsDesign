namespace Subfuzion.Silverlight.UI.Charting
{
	using System;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Input;
	using System.Windows.Media;
	using System.Windows.Shapes;

	[TemplatePart(Name = "PART_plotCanvas", Type = typeof (Canvas))]
	public class InteractivePlot : Control
	{
		public static readonly string PlotCanvasPart = "PART_plotCanvas";

		private readonly Shape DefaultControlPoint = new Ellipse
		{Fill = new SolidColorBrush(Colors.Red), Width = 12, Height = 12};

		private bool isDragging;

		public InteractivePlot()
		{
			DefaultStyleKey = typeof (InteractivePlot);

			ControlPointPlotPrecision = 3;
			MaximumPlotCoordinate = new Point(1.0, 1.0);
		}

		private Canvas PlotCanvas { get; set; }

		#region IsControlPointVisible Property

		public static DependencyProperty IsControlPointVisibleProperty = DependencyProperty.Register(
			"IsControlPointVisible",
			typeof (bool),
			typeof (InteractivePlot),
			new PropertyMetadata(IsControlPointVisibleChangedHandler));

		public bool IsControlPointVisible
		{
			get { return (bool) GetValue(IsControlPointVisibleProperty); }
			set { SetValue(IsControlPointVisibleProperty, value); }
		}

		private static void IsControlPointVisibleChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
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

		#region PropertyName Property

		#region ControlPointPlotPrecision Property

		public static DependencyProperty ControlPointPlotPrecisionProperty = DependencyProperty.Register(
			"ControlPointPlotPrecision",
			typeof (int),
			typeof (InteractivePlot),
			new PropertyMetadata(ControlPointPlotPrecisionChangedHandler));

		public int ControlPointPlotPrecision
		{
			get { return (int) GetValue(ControlPointPlotPrecisionProperty); }
			set { SetValue(ControlPointPlotPrecisionProperty, value); }
		}

		private static void ControlPointPlotPrecisionChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnControlPointPlotPrecisionChanged((int) args.NewValue, (int) args.OldValue);
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

		#region ControlPointPosition Property

		public static DependencyProperty ControlPointPositionProperty = DependencyProperty.Register(
			"ControlPointPosition",
			typeof (Point),
			typeof (InteractivePlot),
			new PropertyMetadata(ControlPointPositionChangedHandler));

		public Point ControlPointPosition
		{
			get { return (Point) GetValue(ControlPointPositionProperty); }
			set { SetValue(ControlPointPositionProperty, value); }
		}

		private static void ControlPointPositionChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
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
			UpdateControlPointStateDisplay();

			Point logPoint = PhysicalToLogicalCoordinates(newValue);
			ControlPointPlotX = logPoint.X;
			ControlPointPlotY = logPoint.Y;
		}

		#endregion // ControlPointPosition Property

		#region ControlPointPlotX Property

		public static DependencyProperty ControlPointPlotXProperty = DependencyProperty.Register(
			"ControlPointPlotX",
			typeof (double),
			typeof (InteractivePlot),
			new PropertyMetadata(ControlPointPlotXChangedHandler));

		public double ControlPointPlotX
		{
			get { return (double) GetValue(ControlPointPlotXProperty); }
			set { SetValue(ControlPointPlotXProperty, value); }
		}

		private static void ControlPointPlotXChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnControlPointPlotXChanged((double) args.NewValue, (double) args.OldValue);
			}
		}

		protected virtual void OnControlPointPlotXChanged(double newValue, double oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnControlPointPlotXChanged(newValue);
		}

		protected virtual void OnControlPointPlotXChanged(double newValue)
		{
			// add handler code
			Point physPoint = LogicalToPhysicalCoordinates(new Point(newValue, ControlPointPlotY));
			if (Math.Abs(physPoint.X - ControlPointPosition.X) > double.Epsilon)
				ControlPointPosition = physPoint;
		}

		#endregion ControlPointPlotX Property

		#region ControlPointPlotY Property

		public static DependencyProperty ControlPointPlotYProperty = DependencyProperty.Register(
			"ControlPointPlotY",
			typeof (double),
			typeof (InteractivePlot),
			new PropertyMetadata(ControlPointPlotYChangedHandler));

		public double ControlPointPlotY
		{
			get { return (double) GetValue(ControlPointPlotYProperty); }
			set { SetValue(ControlPointPlotYProperty, value); }
		}

		private static void ControlPointPlotYChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnControlPointPlotYChanged((double) args.NewValue, (double) args.OldValue);
			}
		}

		protected virtual void OnControlPointPlotYChanged(double newValue, double oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnControlPointPlotYChanged(newValue);
		}

		protected virtual void OnControlPointPlotYChanged(double newValue)
		{
			// add handler code
			Point physPoint = LogicalToPhysicalCoordinates(new Point(ControlPointPlotX, newValue));
			if (Math.Abs(physPoint.Y - ControlPointPosition.Y) > double.Epsilon)
				ControlPointPosition = physPoint;
		}

		#endregion ControlPointPlotY Property

		#region ControlPoint Property

		public static DependencyProperty ControlPointProperty = DependencyProperty.Register(
			"ControlPoint",
			typeof (Shape),
			typeof (InteractivePlot),
			new PropertyMetadata(ControlPointChangedHandler));

		public Shape ControlPoint
		{
			get { return (Shape) GetValue(ControlPointProperty); }
			set { SetValue(ControlPointProperty, value); }
		}

		private static void ControlPointChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnControlPointChanged((Shape) args.NewValue, (Shape) args.OldValue);
			}
		}

		protected virtual void OnControlPointChanged(Shape newValue, Shape oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			if (PlotCanvas != null && PlotCanvas.Children.Contains(oldValue))
			{
				RemoveControlPointHandlers(oldValue);
				PlotCanvas.Children.Remove(oldValue);
			}

			OnControlPointChanged(newValue);
		}

		protected virtual void OnControlPointChanged(Shape newValue)
		{
			// add handler code
			AddControlPointHandlers(newValue);
			newValue.Visibility = Visibility.Collapsed;
			if (PlotCanvas != null) PlotCanvas.Children.Add(newValue);
			UpdateControlPointStateDisplay();
		}

		#endregion // ControlPoint Property

		#region ControlPointHover Property

		public static DependencyProperty ControlPointHoverProperty = DependencyProperty.Register(
			"ControlPointHover",
			typeof (Shape),
			typeof (InteractivePlot),
			new PropertyMetadata(ControlPointHoverChangedHandler));

		public Shape ControlPointHover
		{
			get { return (Shape) GetValue(ControlPointHoverProperty); }
			set { SetValue(ControlPointHoverProperty, value); }
		}

		private static void ControlPointHoverChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnControlPointHoverChanged((Shape) args.NewValue, (Shape) args.OldValue);
			}
		}

		protected virtual void OnControlPointHoverChanged(Shape newValue, Shape oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			if (PlotCanvas != null && PlotCanvas.Children.Contains(oldValue))
			{
				RemoveControlPointHandlers(oldValue);
				PlotCanvas.Children.Remove(oldValue);
			}

			OnControlPointHoverChanged(newValue);
		}

		protected virtual void OnControlPointHoverChanged(Shape newValue)
		{
			// add handler code
			AddControlPointHandlers(newValue);
			newValue.Visibility = Visibility.Collapsed;
			if (PlotCanvas != null) PlotCanvas.Children.Add(newValue);
			UpdateControlPointStateDisplay();
		}

		#endregion ControlPointHover Property

		#region ControlPointDrag Property

		public static DependencyProperty ControlPointDragProperty = DependencyProperty.Register(
			"ControlPointDrag",
			typeof (Shape),
			typeof (InteractivePlot),
			new PropertyMetadata(ControlPointDragChangedHandler));

		public Shape ControlPointDrag
		{
			get { return (Shape) GetValue(ControlPointDragProperty); }
			set { SetValue(ControlPointDragProperty, value); }
		}

		private static void ControlPointDragChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnControlPointDragChanged((Shape) args.NewValue, (Shape) args.OldValue);
			}
		}

		protected virtual void OnControlPointDragChanged(Shape newValue, Shape oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			if (PlotCanvas != null && PlotCanvas.Children.Contains(oldValue))
			{
				RemoveControlPointHandlers(oldValue);
				PlotCanvas.Children.Remove(oldValue);
			}

			OnControlPointDragChanged(newValue);
		}

		protected virtual void OnControlPointDragChanged(Shape newValue)
		{
			// add handler code
			AddControlPointHandlers(newValue);
			newValue.Visibility = Visibility.Collapsed;
			if (PlotCanvas != null) PlotCanvas.Children.Add(newValue);
			UpdateControlPointStateDisplay();
		}

		#endregion ControlPointDrag Property

		#region ControlPointVisibility Property

		public static DependencyProperty ControlPointVisibilityProperty = DependencyProperty.Register(
			"ControlPointVisibility",
			typeof (Visibility),
			typeof (InteractivePlot),
			new PropertyMetadata(ControlPointVisibilityChangedHandler));

		public Visibility ControlPointVisibility
		{
			get { return (Visibility) GetValue(ControlPointVisibilityProperty); }
			set { SetValue(ControlPointVisibilityProperty, value); }
		}

		private static void ControlPointVisibilityChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnControlPointVisibilityChanged((Visibility) args.NewValue, (Visibility) args.OldValue);
			}
		}

		protected virtual void OnControlPointVisibilityChanged(Visibility newValue, Visibility oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnControlPointVisibilityChanged(newValue);
		}

		protected virtual void OnControlPointVisibilityChanged(Visibility newValue)
		{
			// add handler code
			UpdateControlPointStateDisplay();
		}

		#endregion ControlPointVisibility Property

		#region ControlPointState Property

		public static DependencyProperty ControlPointStateProperty = DependencyProperty.Register(
			"ControlPointState",
			typeof (ControlPointState),
			typeof (InteractivePlot),
			new PropertyMetadata(ControlPointStateChangedHandler));

		public ControlPointState ControlPointState
		{
			get { return (ControlPointState) GetValue(ControlPointStateProperty); }
			set { SetValue(ControlPointStateProperty, value); }
		}

		private static void ControlPointStateChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnControlPointStateChanged((ControlPointState) args.NewValue, (ControlPointState) args.OldValue);
			}
		}

		protected virtual void OnControlPointStateChanged(ControlPointState newValue, ControlPointState oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnControlPointStateChanged(newValue);
		}

		protected virtual void OnControlPointStateChanged(ControlPointState newValue)
		{
			// add handler code
			UpdateControlPointStateDisplay();
		}

		#endregion ControlPointState Property

		private Shape ActiveControlPoint
		{
			get
			{
				switch (ControlPointState)
				{
					case ControlPointState.Hover:
						return CurrentControlPointHover;

					case ControlPointState.Drag:
						return CurrentControlPointDrag;
				}

				return CurrentControlPoint;
			}
		}

		private Shape CurrentControlPoint
		{
			get
			{
				if (ControlPoint == null)
				{
					ControlPoint = DefaultControlPoint;
				}

				return ControlPoint;
			}
		}

		private Shape CurrentControlPointHover
		{
			get { return ControlPointHover ?? CurrentControlPoint; }
		}

		private Shape CurrentControlPointDrag
		{
			get { return ControlPointDrag ?? CurrentControlPointHover; }
		}

		private void UpdateControlPointStateDisplay()
		{
			switch (ControlPointState)
			{
				case ControlPointState.Normal:
					SetPosition(CurrentControlPoint, ControlPointPosition);
					CurrentControlPoint.Visibility = ControlPointVisibility;

					if (ControlPointHover != null) ControlPointHover.Visibility = Visibility.Collapsed;
					if (ControlPointDrag != null) ControlPointDrag.Visibility = Visibility.Collapsed;
					break;

				case ControlPointState.Hover:
					if (ControlPointHover != null)
					{
						SetPosition(ControlPointHover, ControlPointPosition);
						ControlPointHover.Visibility = ControlPointVisibility;

						if (ControlPoint != null) ControlPoint.Visibility = Visibility.Collapsed;
						if (ControlPointDrag != null) ControlPointDrag.Visibility = Visibility.Collapsed;
					}
					else
					{
						SetPosition(ControlPoint, ControlPointPosition);
						ControlPoint.Visibility = ControlPointVisibility;
						if (ControlPointDrag != null) ControlPointDrag.Visibility = Visibility.Collapsed;
					}
					break;

				case ControlPointState.Drag:
					if (ControlPointDrag != null)
					{
						SetPosition(ControlPointDrag, ControlPointPosition);
						ControlPointDrag.Visibility = ControlPointVisibility;

						if (ControlPoint != null) ControlPoint.Visibility = Visibility.Collapsed;
						if (ControlPointHover != null) ControlPointHover.Visibility = Visibility.Collapsed;
					}
					else
					{
						SetPosition(ControlPoint, ControlPointPosition);
						ControlPoint.Visibility = ControlPointVisibility;
						if (ControlPointHover != null) ControlPointHover.Visibility = Visibility.Collapsed;
					}
					break;
			}
		}

		private static void SetPosition(DependencyObject element, Point p)
		{
			SetPosition(element, p.X, p.Y);
		}

		private static void SetPosition(DependencyObject element, double x, double y)
		{
			element.SetValue(Canvas.LeftProperty, x);
			element.SetValue(Canvas.TopProperty, y);
		}


		private void AddControlPointHandlers(Shape shape)
		{
			shape.MouseEnter += ControlPointOnMouseEnter;
			shape.MouseMove += HandleOnMouseMove;
			shape.MouseLeave += ControlPointOnMouseLeave;
			shape.MouseLeftButtonDown += ControlPointOnMouseLeftButtonDown;
			shape.MouseLeftButtonUp += ControlPointOnMouseLeftButtonUp;
		}

		private void RemoveControlPointHandlers(Shape shape)
		{
			shape.MouseEnter -= ControlPointOnMouseEnter;
			shape.MouseMove -= HandleOnMouseMove;
			shape.MouseLeave -= ControlPointOnMouseLeave;
			shape.MouseLeftButtonDown -= ControlPointOnMouseLeftButtonDown;
			shape.MouseLeftButtonUp -= ControlPointOnMouseLeftButtonUp;
		}

		private void ControlPointOnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
		{
			//if (ControlPointState != ControlPointState.Drag)
			if(!isDragging)
				ControlPointState = ControlPointState.Hover;
		}

		private void ControlPointOnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
		{
			//if (ControlPointState != ControlPointState.Drag)
			if (!isDragging)
				ControlPointState = ControlPointState.Normal;
		}

		private void ControlPointOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			isDragging = true;
			ControlPointState = ControlPointState.Drag;
			ActiveControlPoint.CaptureMouse();
		}

		private void ControlPointOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			isDragging = false;
			ControlPointState = ControlPointState.Hover;
		}

		#endregion

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			//SizeChanged += (sender, args) => OnSizeChanged(args.NewSize);

			PlotCanvas = GetTemplateChild(PlotCanvasPart) as Canvas;
			if (PlotCanvas != null)
			{
				PlotCanvas.SizeChanged += (sender, args) => OnSizeChanged(args.NewSize);
				ClipToBounds(ActualWidth, ActualHeight);

				//PlotCanvas.MouseEnter += (sender, args) => PlotCanvas.MouseMove += HandleOnMouseMove;
				//PlotCanvas.MouseLeave += (sender, args) => PlotCanvas.MouseMove -= HandleOnMouseMove;
				PlotCanvas.MouseLeftButtonUp += (sender, args) =>
				{
					isDragging = false;
					ControlPointState = ControlPointState.Normal;
				};


				if (ControlPoint != null) PlotCanvas.Children.Add(ControlPoint);
				if (ControlPointHover != null) PlotCanvas.Children.Add(ControlPointHover);
				if (ControlPointDrag != null) PlotCanvas.Children.Add(ControlPointDrag);

				UpdateControlPointStateDisplay();
			}
		}

		private bool InBounds(Point p, FrameworkElement element)
		{
			bool inBounds = (p.X >= 0 && p.X < element.ActualWidth && p.Y >= 0 && p.Y < element.ActualHeight) ? true : false;
			return inBounds;
		}

		private void HandleOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
		{
			if (isDragging)
			{
				Point p = mouseEventArgs.GetPosition(PlotCanvas);

				if (p.X < 0) p.X = 0;
				if (p.X > ActualWidth - 1) p.X = ActualWidth - 1;
				if (p.Y < 0) p.Y = 0;
				if (p.Y > ActualHeight - 1) p.Y = ActualHeight - 1;
				ControlPointPosition = p;
			}
		}

		private void OnSizeChanged(Size size)
		{
			//Width = size.Width;
			//Height = size.Height;
			ClipToBounds(size.Width, size.Height);
		}

		private void ClipToBounds(Size size)
		{
			ClipToBounds(size.Width, size.Height);
		}

		private void ClipToBounds(double width, double height)
		{
			if (PlotCanvas != null)
			{
				PlotCanvas.Clip = new RectangleGeometry {Rect = new Rect(0, 0, width, height)};
			}
		}

		public Point LogicalToPhysicalCoordinates(Point p)
		{
			double logWidth = MaximumPlotCoordinate.X - MinimumPlotCoordinate.X;
			double logHeight = MaximumPlotCoordinate.Y - MinimumPlotCoordinate.Y;

			double x = Math.Round((p.X/logWidth)*(ActualWidth - 1), ControlPointPlotPrecision);
			double y = Math.Round((p.Y/logHeight)*(ActualHeight - 1), ControlPointPlotPrecision);

			var physPoint = new Point(x, y);
			return physPoint;
		}

		public Point PhysicalToLogicalCoordinates(Point p)
		{
			double logWidth = MaximumPlotCoordinate.X - MinimumPlotCoordinate.X;
			double logHeight = MaximumPlotCoordinate.Y - MinimumPlotCoordinate.Y;

			double logX = Math.Round((p.X/(ActualWidth - 1))*logWidth, ControlPointPlotPrecision);
			double logY = Math.Round((p.Y/(ActualHeight - 1))*logHeight, ControlPointPlotPrecision);

			var logPoint = new Point(logX, logY);
			return logPoint;
		}

		#region MinimumPlotCoordinate Property

		public static DependencyProperty MinimumPlotCoordinateProperty = DependencyProperty.Register(
			"MinimumPlotCoordinate",
			typeof (Point),
			typeof (InteractivePlot),
			new PropertyMetadata(MinimumPlotCoordinateChangedHandler));

		public Point MinimumPlotCoordinate
		{
			get { return (Point) GetValue(MinimumPlotCoordinateProperty); }
			set { SetValue(MinimumPlotCoordinateProperty, value); }
		}

		private static void MinimumPlotCoordinateChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnMinimumPlotCoordinateChanged((Point) args.NewValue, (Point) args.OldValue);
			}
		}

		protected virtual void OnMinimumPlotCoordinateChanged(Point newValue, Point oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnMinimumPlotCoordinateChanged(newValue);
		}

		protected virtual void OnMinimumPlotCoordinateChanged(Point newValue)
		{
			// add handler code
		}

		#endregion MinimumPlotCoordinate Property

		#region MaximumPlotCoordinate Property

		public static DependencyProperty MaximumPlotCoordinateProperty = DependencyProperty.Register(
			"MaximumPlotCoordinate",
			typeof (Point),
			typeof (InteractivePlot),
			new PropertyMetadata(MaximumPlotCoordinateChangedHandler));

		public Point MaximumPlotCoordinate
		{
			get { return (Point) GetValue(MaximumPlotCoordinateProperty); }
			set { SetValue(MaximumPlotCoordinateProperty, value); }
		}

		private static void MaximumPlotCoordinateChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as InteractivePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnMaximumPlotCoordinateChanged((Point) args.NewValue, (Point) args.OldValue);
			}
		}

		protected virtual void OnMaximumPlotCoordinateChanged(Point newValue, Point oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnMaximumPlotCoordinateChanged(newValue);
		}

		protected virtual void OnMaximumPlotCoordinateChanged(Point newValue)
		{
			// add handler code
		}

		#endregion MaximumPlotCoordinate Property
	}
}