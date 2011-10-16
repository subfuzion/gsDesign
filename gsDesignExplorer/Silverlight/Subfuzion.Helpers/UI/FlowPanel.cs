namespace Subfuzion.Helpers.UI
{
	using System;
	using System.Windows;
	using System.Windows.Controls;

	public class FlowPanel : Panel
	{
		public static readonly DependencyProperty OrientationProperty =
			DependencyProperty.Register("Orientation", typeof (Orientation), typeof (FlowPanel), new PropertyMetadata(Orientation.Vertical, OnOrientationChanged));

		public Orientation Orientation
		{
			get { return (Orientation) GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}

		static void OnOrientationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			((FlowPanel)sender).UpdateLayout();
		}

		public FlowPanel()
		{
			Loaded += (sender, routedEventArgs) =>
			          	{
			          		var padding = new Thickness();

							var parent = (FrameworkElement)Parent;
							while (parent != null && parent is ScrollViewer)
							{
								padding.Left += ((ScrollViewer) parent).Padding.Left;
								padding.Top += ((ScrollViewer)parent).Padding.Top;
								padding.Right += ((ScrollViewer)parent).Padding.Right;
								padding.Bottom += ((ScrollViewer)parent).Padding.Bottom;
								parent = (FrameworkElement)parent.Parent;
							}

							if (parent != null)
							{
								Width = parent.ActualWidth;
								Height= parent.ActualHeight;

								parent.SizeChanged += (s, e) =>
								{
									if (Orientation == Orientation.Vertical)
									{
										Width = e.NewSize.Width - (padding.Left + padding.Right);

										if (Parent is ScrollViewer)
										{
											if (((ScrollViewer)Parent).ComputedVerticalScrollBarVisibility == Visibility.Visible)
											{
												Width -= 18;
											}
										}
									}
									else if (Orientation == Orientation.Horizontal)
									{
										Height = e.NewSize.Height - (padding.Top + padding.Bottom);

										if (Parent is ScrollViewer)
										{
											if (((ScrollViewer)Parent).ComputedHorizontalScrollBarVisibility == Visibility.Visible)
											{
												Height -= 18;
											}
										}
									}
								};
							}
						};
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			var compositeSize = new Size(0, 0);

			foreach (var child in Children)
			{
				child.Measure(availableSize);

				if (Orientation == Orientation.Vertical)
				{
					compositeSize.Width = Math.Max(compositeSize.Width, child.DesiredSize.Width);
					compositeSize.Height += child.DesiredSize.Height;
				}
				else if (Orientation == Orientation.Horizontal)
				{
					compositeSize.Width += child.DesiredSize.Width;
					compositeSize.Height = Math.Max(compositeSize.Height, child.DesiredSize.Height);
				}
			}

			return compositeSize;
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			var x = 0.0;
			var y = 0.0;

			if (Orientation == Orientation.Vertical)
			{
				foreach (var child in Children)
				{
					var r = new Rect(0, y, finalSize.Width, child.DesiredSize.Height);
					child.Arrange(r);
					y += child.DesiredSize.Height;
				}
			}
			else if (Orientation == Orientation.Horizontal)
			{
				foreach (var child in Children)
				{
					var r = new Rect(x, 0, child.DesiredSize.Width, finalSize.Height);
					child.Arrange(r);
					x += child.DesiredSize.Width;
				}
			}

			return base.ArrangeOverride(finalSize);
		}
	}
}