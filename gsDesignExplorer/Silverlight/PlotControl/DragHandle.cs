namespace Subfuzion.Silverlight.UI.Charting
{
	using System;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Input;

	[TemplatePart(Name = "PART_DragHandle", Type = typeof(Canvas))]
	public class DragHandle : Control
	{
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

			MouseEnter += OnMouseEnter;
			MouseLeave += OnMouseLeave;
			MouseLeftButtonDown += OnMouseLeftButtonDown;
			MouseLeftButtonUp += OnMouseLeftButtonUp;
		}

		private void OnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
		{
			Log("OnMouseEnter");
		}

		private void OnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
		{
			Log("OnMouseLeave");
		}

		private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			Log("OnMouseLeftButtonDown");
		}

		private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			Log("OnMouseLeftButtonUp");
		}
	}
}
