namespace Subfuzion.Silverlight.UI.Charting
{
	using System;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Input;
	using System.Windows.Shapes;

	[TemplatePart(Name = "PART_Canvas", Type = typeof(Canvas))]
	public class DragHandle : Control
	{
		public static readonly string CanvasPart = "PART_Canvas";

		private bool isDragging;

		public DragHandle()
		{
			DefaultStyleKey = typeof(DragHandle);
		}

		#region Debugging Support

		#region Logger property

		public Action<string,string,object[]> Logger
		{
			get { return (Action<string,string,object[]>) GetValue(LoggerProperty); }
			set { SetValue(LoggerProperty, value); }
		}

		public static DependencyProperty LoggerProperty = DependencyProperty.Register(
			"Logger",
			typeof (Action<string,string,object[]>),
			typeof (DragHandle),
			new PropertyMetadata(LoggerChangedHandler));

		private static void LoggerChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var dragHandle = dependencyObject as DragHandle;
			if (dragHandle != null)
			{
				dragHandle.OnLoggerChanged((Action<string,string,object[]>) args.NewValue, (Action<string,string,object[]>) args.OldValue);
			}
		}

		protected virtual void OnLoggerChanged(Action<string,string,object[]> newValue, Action<string,string,object[]> oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnLoggerChanged(newValue);
		}

		protected virtual void OnLoggerChanged(Action<string,string,object[]> newValue)
		{
			// add handler code
		}

		#endregion


		protected void Log(string function, string message = "", params object[] args)
		{
			if (Logger != null)
			{
				Logger(function, message, args);
			}
		}

		#region LogMessage property

		public string LogMessage
		{
			get { return (string)GetValue(LogMessageProperty); }
			set { SetValue(LogMessageProperty, value); }
		}

		public static DependencyProperty LogMessageProperty = DependencyProperty.Register(
			"LogMessage",
			typeof(string),
			typeof(DragHandle),
			new PropertyMetadata(LogMessageChangedHandler));

		private static void LogMessageChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var dragHandle = dependencyObject as DragHandle;
			if (dragHandle != null)
			{
				dragHandle.OnLogMessageChanged((string)args.NewValue, (string)args.OldValue);
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

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if (DesignerProperties.GetIsInDesignMode(this)) return;

			Canvas = GetTemplateChild(CanvasPart) as Canvas;
			if (Canvas == null) return;

			if (EnabledShape != null && !Canvas.Children.Contains(EnabledShape))
			{
				Canvas.Children.Clear();
				Canvas.Children.Add(EnabledShape);
			}
			else
			{
				// the control template provides a default shape
				EnabledShape = Canvas.Children[0] as Shape;
			}

			if (HoverShape != null && !Canvas.Children.Contains(HoverShape))
			{
				Canvas.Children.Add(HoverShape);
			}

			if (DragShape != null && !Canvas.Children.Contains(DragShape))
			{
				Canvas.Children.Add(DragShape);
			}

			ShowControlState(ControlState.Normal);
		}


		private Canvas Canvas { get; set; }

		protected override void OnMouseEnter(MouseEventArgs e)
		{
			//Log("OnMouseEnter");
			base.OnMouseEnter(e);
			ControlState = ControlState.Hover;
		}

		protected override void OnMouseLeave(MouseEventArgs e)
		{
			//Log("OnMouseLeave");
			base.OnMouseLeave(e);
			ControlState = ControlState.Normal;
		}

		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			//Log("OnMouseLeftButtonDown");
			base.OnMouseLeftButtonDown(e);
			ControlState = ControlState.Drag;
		}

		protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
		{
			//Log("OnMouseLeftButtonUp");
			base.OnMouseLeftButtonUp(e);
			ControlState = ControlState.Hover;
		}

		#region Dependency Properties

		#region EnabledShape property

		public Shape EnabledShape
		{
			get { return (Shape) GetValue(EnabledShapeProperty); }
			set { SetValue(EnabledShapeProperty, value); }
		}

		public static DependencyProperty EnabledShapeProperty = DependencyProperty.Register(
			"EnabledShape",
			typeof (Shape),
			typeof (DragHandle),
			new PropertyMetadata(EnabledShapeChangedHandler));

		private static void EnabledShapeChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var dragHandle = dependencyObject as DragHandle;
			if (dragHandle != null)
			{
				dragHandle.OnEnabledShapeChanged((Shape) args.NewValue, (Shape) args.OldValue);
			}
		}

		protected virtual void OnEnabledShapeChanged(Shape newValue, Shape oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			if (oldValue != null) Canvas.Children.Remove(oldValue);
			OnEnabledShapeChanged(newValue);
		}

		protected virtual void OnEnabledShapeChanged(Shape newValue)
		{
			// add handler code
			if (Canvas != null && newValue != null && !Canvas.Children.Contains(newValue))
			{
				Canvas.Children.Add(newValue);
			}
		}

		#endregion

		#region HoverShape property

		public Shape HoverShape
		{
			get { return (Shape) GetValue(HoverShapeProperty); }
			set { SetValue(HoverShapeProperty, value); }
		}

		public static DependencyProperty HoverShapeProperty = DependencyProperty.Register(
			"HoverShape",
			typeof (Shape),
			typeof (DragHandle),
			new PropertyMetadata(HoverShapeChangedHandler));

		private static void HoverShapeChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var dragHandle = dependencyObject as DragHandle;
			if (dragHandle != null)
			{
				dragHandle.OnHoverShapeChanged((Shape) args.NewValue, (Shape) args.OldValue);
			}
		}

		protected virtual void OnHoverShapeChanged(Shape newValue, Shape oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			if (oldValue != null) Canvas.Children.Remove(oldValue);
			OnHoverShapeChanged(newValue);
		}

		protected virtual void OnHoverShapeChanged(Shape newValue)
		{
			// add handler code
			if (Canvas != null && newValue != null && !Canvas.Children.Contains(newValue))
			{
				Canvas.Children.Add(newValue);
			}
		}

		#endregion

		#region DragShape property

		public Shape DragShape
		{
			get { return (Shape) GetValue(DragShapeProperty); }
			set { SetValue(DragShapeProperty, value); }
		}

		public static DependencyProperty DragShapeProperty = DependencyProperty.Register(
			"DragShape",
			typeof (Shape),
			typeof (DragHandle),
			new PropertyMetadata(DragShapeChangedHandler));

		private static void DragShapeChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var dragHandle = dependencyObject as DragHandle;
			if (dragHandle != null)
			{
				dragHandle.OnDragShapeChanged((Shape) args.NewValue, (Shape) args.OldValue);
			}
		}

		protected virtual void OnDragShapeChanged(Shape newValue, Shape oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			if (oldValue != null) Canvas.Children.Remove(oldValue);
			OnDragShapeChanged(newValue);
		}

		protected virtual void OnDragShapeChanged(Shape newValue)
		{
			// add handler code
			if (Canvas != null && newValue != null && !Canvas.Children.Contains(newValue))
			{
				Canvas.Children.Add(newValue);
			}
		}

		#endregion

		#region ControlState property

		public ControlState ControlState
		{
			get { return (ControlState) GetValue(ControlStateProperty); }
			set { SetValue(ControlStateProperty, value); }
		}

		public static DependencyProperty ControlStateProperty = DependencyProperty.Register(
			"ControlState",
			typeof (ControlState),
			typeof (DragHandle),
			new PropertyMetadata(ControlState.Normal, ControlStateChangedHandler));

		private static void ControlStateChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var dragHandle = dependencyObject as DragHandle;
			if (dragHandle != null)
			{
				dragHandle.OnControlStateChanged((ControlState) args.NewValue, (ControlState) args.OldValue);
			}
		}

		protected virtual void OnControlStateChanged(ControlState newValue, ControlState oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnControlStateChanged(newValue);
		}

		protected virtual void OnControlStateChanged(ControlState newValue)
		{
			// add handler code
			ShowControlState(newValue);
		}

		#endregion

		#endregion

		private void ShowControlState(ControlState controlState)
		{
			switch (controlState)
			{
				case ControlState.Normal:
					Show(EnabledShape);
					Hide(HoverShape);
					Hide(DragShape);
					break;

				case ControlState.Hover:
					if (HoverShape != null)
					{
						Show(HoverShape);
						Hide(EnabledShape);
						Hide(DragShape);
					}
					else
					{
						ShowControlState(ControlState.Normal);
					}
					break;

				case ControlState.Drag:
					if (DragShape != null)
					{
						Show(DragShape);
						Hide(EnabledShape);
						Hide(HoverShape);
					}
					else
					{
						ShowControlState(ControlState.Hover);
					}
					break;
			}
		}

		private void Show(UIElement element)
		{
			if (element != null)
			{
				element.Visibility = Visibility.Visible;
			}
		}

		private void Hide(UIElement element)
		{
			if (element != null)
			{
				element.Visibility = Visibility.Collapsed;
			}
		}
	}
}
