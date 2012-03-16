﻿namespace Subfuzion.Silverlight.UI.Charting
{
	using System;
	using System.Collections.ObjectModel;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Input;
	using System.Windows.Media;
	using System.Windows.Shapes;

	[TemplatePart(Name = "PART_plotCanvas", Type = typeof (Canvas))]
	public class LinePlot : LinePlotBase
	{
		private readonly Shape DefaultControlPoint = new Ellipse {Fill = new SolidColorBrush(Colors.Red), Width = 12, Height = 12};

		private bool isDragging;

		public LinePlot()
		{
			DefaultStyleKey = typeof (LinePlot);
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if (DesignerProperties.GetIsInDesignMode(this)) return;

			//PlotSurface = GetTemplateChild(PlotCanvasPart) as Canvas;
			if (PlotSurface != null)
			{
				if (ControlPoint != null && !PlotSurface.Children.Contains(ControlPoint))
				{
					PlotSurface.Children.Add(ControlPoint);
				}

				if (ControlPointHover != null && !PlotSurface.Children.Contains(ControlPointHover))
				{
					PlotSurface.Children.Add(ControlPointHover);
				}

				if (ControlPointDrag != null && !PlotSurface.Children.Contains(ControlPointDrag))
				{
					PlotSurface.Children.Add(ControlPointDrag);
				}

				UpdatePlotDisplay();
				UpdateControlPointStateDisplay();
			}
		}

		#region Dependency Properties

		#region Control Point

		#region Control Point Shapes

		#region ControlPoint Property

		public static DependencyProperty ControlPointProperty = DependencyProperty.Register(
			"ControlPoint",
			typeof(Shape),
			typeof(LinePlot),
			new PropertyMetadata(ControlPointChangedHandler));

		public Shape ControlPoint
		{
			get { return (Shape)GetValue(ControlPointProperty); }
			set { SetValue(ControlPointProperty, value); }
		}

		private static void ControlPointChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as LinePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnControlPointChanged((Shape)args.NewValue, (Shape)args.OldValue);
			}
		}

		protected virtual void OnControlPointChanged(Shape newValue, Shape oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			if (PlotSurface != null && PlotSurface.Children.Contains(oldValue))
			{
				RemoveControlPointHandlers(oldValue);
				PlotSurface.Children.Remove(oldValue);
			}

			OnControlPointChanged(newValue);
		}

		protected virtual void OnControlPointChanged(Shape newValue)
		{
			// add handler code
			AddControlPointHandlers(newValue);
			newValue.Visibility = Visibility.Collapsed;
			if (PlotSurface != null) PlotSurface.Children.Add(newValue);
			UpdateControlPointStateDisplay();
		}

		#endregion // ControlPoint Property

		#region ControlPointHover Property

		public static DependencyProperty ControlPointHoverProperty = DependencyProperty.Register(
			"ControlPointHover",
			typeof(Shape),
			typeof(LinePlot),
			new PropertyMetadata(ControlPointHoverChangedHandler));

		public Shape ControlPointHover
		{
			get { return (Shape)GetValue(ControlPointHoverProperty); }
			set { SetValue(ControlPointHoverProperty, value); }
		}

		private static void ControlPointHoverChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as LinePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnControlPointHoverChanged((Shape)args.NewValue, (Shape)args.OldValue);
			}
		}

		protected virtual void OnControlPointHoverChanged(Shape newValue, Shape oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			if (PlotSurface != null && PlotSurface.Children.Contains(oldValue))
			{
				RemoveControlPointHandlers(oldValue);
				PlotSurface.Children.Remove(oldValue);
			}

			OnControlPointHoverChanged(newValue);
		}

		protected virtual void OnControlPointHoverChanged(Shape newValue)
		{
			// add handler code
			AddControlPointHandlers(newValue);
			newValue.Visibility = Visibility.Collapsed;
			if (PlotSurface != null) PlotSurface.Children.Add(newValue);
			UpdateControlPointStateDisplay();
		}

		#endregion ControlPointHover Property

		#region ControlPointDrag Property

		public static DependencyProperty ControlPointDragProperty = DependencyProperty.Register(
			"ControlPointDrag",
			typeof(Shape),
			typeof(LinePlot),
			new PropertyMetadata(ControlPointDragChangedHandler));

		public Shape ControlPointDrag
		{
			get { return (Shape)GetValue(ControlPointDragProperty); }
			set { SetValue(ControlPointDragProperty, value); }
		}

		private static void ControlPointDragChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as LinePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnControlPointDragChanged((Shape)args.NewValue, (Shape)args.OldValue);
			}
		}

		protected virtual void OnControlPointDragChanged(Shape newValue, Shape oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			if (PlotSurface != null && PlotSurface.Children.Contains(oldValue))
			{
				RemoveControlPointHandlers(oldValue);
				PlotSurface.Children.Remove(oldValue);
			}

			OnControlPointDragChanged(newValue);
		}

		protected virtual void OnControlPointDragChanged(Shape newValue)
		{
			// add handler code
			AddControlPointHandlers(newValue);
			newValue.Visibility = Visibility.Collapsed;
			if (PlotSurface != null) PlotSurface.Children.Add(newValue);
			UpdateControlPointStateDisplay();
		}

		#endregion ControlPointDrag Property

		#endregion

		#region ControlPointVisibility Property

		public static DependencyProperty ControlPointVisibilityProperty = DependencyProperty.Register(
			"ControlPointVisibility",
			typeof (Visibility),
			typeof (LinePlot),
			new PropertyMetadata(ControlPointVisibilityChangedHandler));

		public Visibility ControlPointVisibility
		{
			get { return (Visibility) GetValue(ControlPointVisibilityProperty); }
			set { SetValue(ControlPointVisibilityProperty, value); }
		}

		private static void ControlPointVisibilityChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as LinePlot;
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

		#region IsControlPointVisible Property

		public static DependencyProperty IsControlPointVisibleProperty = DependencyProperty.Register(
			"IsControlPointVisible",
			typeof (bool),
			typeof (LinePlot),
			new PropertyMetadata(IsControlPointVisibleChangedHandler));

		public bool IsControlPointVisible
		{
			get { return (bool) GetValue(IsControlPointVisibleProperty); }
			set { SetValue(IsControlPointVisibleProperty, value); }
		}

		private static void IsControlPointVisibleChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as LinePlot;
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

		public static DependencyProperty ControlPointPhysicalPositionProperty = DependencyProperty.Register(
			"ControlPointPosition",
			typeof (Point),
			typeof (LinePlot),
			new PropertyMetadata(ControlPointPhysicalPositionChangedHandler));

		public Point ControlPointPhysicalPosition
		{
			get { return (Point) GetValue(ControlPointPhysicalPositionProperty); }
			set { SetValue(ControlPointPhysicalPositionProperty, value); }
		}

		private static void ControlPointPhysicalPositionChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as LinePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnControlPointPhysicalPositionChanged((Point) args.NewValue, (Point) args.OldValue);
			}
		}

		protected virtual void OnControlPointPhysicalPositionChanged(Point newValue, Point oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnControlPointPhysicalPositionChanged(newValue);
		}

		protected virtual void OnControlPointPhysicalPositionChanged(Point newValue)
		{
			// add handler code
			UpdateControlPointStateDisplay();

			//Point logPoint = PhysicalToLogicalCoordinates(newValue);
			//ControlPointPlotX = logPoint.X;
			//ControlPointPlotY = logPoint.Y;
		}

		#endregion // ControlPointPhysicalPosition Property

		#region ControlPointPlotX Property

		public static DependencyProperty ControlPointPlotXProperty = DependencyProperty.Register(
			"ControlPointPlotX",
			typeof (double),
			typeof (LinePlot),
			new PropertyMetadata(ControlPointPlotXChangedHandler));

		public double ControlPointPlotX
		{
			get { return (double) GetValue(ControlPointPlotXProperty); }
			set { SetValue(ControlPointPlotXProperty, value); }
		}

		private static void ControlPointPlotXChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as LinePlot;
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
			if (Math.Abs(physPoint.X - ControlPointPhysicalPosition.X) > double.Epsilon)
				ControlPointPhysicalPosition = physPoint;
		}

		#endregion ControlPointPlotX Property

		#region ControlPointPlotY Property

		public static DependencyProperty ControlPointPlotYProperty = DependencyProperty.Register(
			"ControlPointPlotY",
			typeof (double),
			typeof (LinePlot),
			new PropertyMetadata(ControlPointPlotYChangedHandler));

		public double ControlPointPlotY
		{
			get { return (double) GetValue(ControlPointPlotYProperty); }
			set { SetValue(ControlPointPlotYProperty, value); }
		}

		private static void ControlPointPlotYChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as LinePlot;
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
			if (Math.Abs(physPoint.Y - ControlPointPhysicalPosition.Y) > double.Epsilon)
				ControlPointPhysicalPosition = physPoint;
		}

		#endregion ControlPointPlotY Property

		#region Control point state management and display

		#region ControlPointState Property

		public static DependencyProperty ControlPointStateProperty = DependencyProperty.Register(
			"ControlPointState",
			typeof (ControlState),
			typeof (LinePlot),
			new PropertyMetadata(ControlPointStateChangedHandler));

		public ControlState ControlPointState
		{
			get { return (ControlState) GetValue(ControlPointStateProperty); }
			set { SetValue(ControlPointStateProperty, value); }
		}

		private static void ControlPointStateChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as LinePlot;
			if (interactivePlot != null)
			{
				interactivePlot.Log("ControlPointStateChangedHandler", "new value: {0} (old value: {1})",
					(ControlState) args.NewValue, (ControlState) args.OldValue);
				interactivePlot.OnControlPointStateChanged((ControlState) args.NewValue, (ControlState) args.OldValue);
			}
		}

		protected virtual void OnControlPointStateChanged(ControlState newValue, ControlState oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnControlPointStateChanged(newValue);
		}

		protected virtual void OnControlPointStateChanged(ControlState newValue)
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
					case ControlState.Hover:
						return CurrentControlPointHover;

					case ControlState.Drag:
						return CurrentControlPointDrag;
				}

				return CurrentControlPoint;
			}
		}

		private Shape CurrentControlPoint
		{
			get { return ControlPoint ?? (ControlPoint = DefaultControlPoint); }
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
			var controlPointState = ControlPointState;
			var currentControlPoint = CurrentControlPoint;

			switch (controlPointState)
			{
				case ControlState.Normal:
					SetPhysicalPosition(currentControlPoint, ControlPointPhysicalPosition);
					currentControlPoint.Visibility = ControlPointVisibility;

					if (ControlPointHover != null) ControlPointHover.Visibility = Visibility.Collapsed;
					if (ControlPointDrag != null) ControlPointDrag.Visibility = Visibility.Collapsed;
					break;

				case ControlState.Hover:
					if (ControlPointHover != null)
					{
						SetPhysicalPosition(ControlPointHover, ControlPointPhysicalPosition);
						ControlPointHover.Visibility = ControlPointVisibility;

						if (ControlPoint != null) ControlPoint.Visibility = Visibility.Collapsed;
						if (ControlPointDrag != null) ControlPointDrag.Visibility = Visibility.Collapsed;
					}
					else
					{
						SetPhysicalPosition(ControlPoint, ControlPointPhysicalPosition);
						ControlPoint.Visibility = ControlPointVisibility;
						if (ControlPointDrag != null) ControlPointDrag.Visibility = Visibility.Collapsed;
					}
					break;

				case ControlState.Drag:
					if (ControlPointDrag != null)
					{
						SetPhysicalPosition(ControlPointDrag, ControlPointPhysicalPosition);
						ControlPointDrag.Visibility = ControlPointVisibility;

						if (ControlPoint != null) ControlPoint.Visibility = Visibility.Collapsed;
						if (ControlPointHover != null) ControlPointHover.Visibility = Visibility.Collapsed;
					}
					else
					{
						SetPhysicalPosition(ControlPoint, ControlPointPhysicalPosition);
						ControlPoint.Visibility = ControlPointVisibility;
						if (ControlPointHover != null) ControlPointHover.Visibility = Visibility.Collapsed;
					}
					break;
			}
		}

		#endregion

		#region Control point handlers

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
			if (!isDragging)
				ControlPointState = ControlState.Hover;
		}

		private void ControlPointOnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
		{
			//if (ControlPointState != ControlPointState.Drag)
			if (!isDragging)
				ControlPointState = ControlState.Normal;
		}

		private void ControlPointOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			isDragging = true;
			ControlPointState = ControlState.Drag;
			ActiveControlPoint.CaptureMouse();
		}

		private void ControlPointOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			isDragging = false;
			ControlPointState = ControlState.Hover;
		}

		#endregion

		#endregion

		#endregion

		#region Implementation

		#region Handlers

		private void HandleOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
		{
			if (isDragging)
			{
				Point p = mouseEventArgs.GetPosition(PlotSurface);

				if (p.X < 0) p.X = 0;
				if (p.X > ActualWidth - 1) p.X = ActualWidth - 1;
				if (p.Y < 0) p.Y = 0;
				if (p.Y > ActualHeight - 1) p.Y = ActualHeight - 1;
				ControlPointPhysicalPosition = p;
			}
			//else if (ControlPointState != ControlPointState.Normal && )
		}

		#endregion

		protected virtual void OnCoordinatesChanged(ObservableCollection<Point> newCoordinates)
		{
			base.OnCoordinatesChanged(newCoordinates);
			UpdateControlPointStateDisplay();
		}

		public override void UpdatePlotDisplay()
		{
			base.UpdatePlotDisplay();

			// HACK: ensure control point is remapped to new physical location
			OnControlPointPlotXChanged(ControlPointPlotX);
			OnControlPointPlotYChanged(ControlPointPlotY);

			var point = LogicalToPhysicalCoordinates(new Point(ControlPointPlotX, ControlPointPlotY));

			DragHandleALogicalX = ControlPointPlotX;
			DragHandleALogicalY = ControlPointPlotY;
		}

		#endregion
	}

}