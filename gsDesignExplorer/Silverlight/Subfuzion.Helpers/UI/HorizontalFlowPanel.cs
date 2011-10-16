namespace Subfuzion.Helpers.UI
{
	using System;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Controls.Primitives;

	public class HorizontalFlowPanel : Panel
	{
		public static readonly DependencyProperty OrientationProperty =
			DependencyProperty.Register("Orientation", typeof (Orientation), typeof (HorizontalFlowPanel),
			                            new PropertyMetadata(Orientation.Vertical, OnOrientationChanged));

		public HorizontalFlowPanel()
		{
			if (Parent is ScrollViewer)
			{
				Loaded += (sender, routedEventArgs) =>
				          {
				          	var padding = new Thickness();

				          	var parent = (FrameworkElement) Parent;
				          	while (parent != null && parent is ScrollViewer)
				          	{
				          		padding.Left += ((ScrollViewer) parent).Padding.Left;
				          		padding.Top += ((ScrollViewer) parent).Padding.Top;
				          		padding.Right += ((ScrollViewer) parent).Padding.Right;
				          		padding.Bottom += ((ScrollViewer) parent).Padding.Bottom;
				          		parent = (FrameworkElement) parent.Parent;
				          	}

				          	if (parent != null)
				          	{
				          		Width = parent.ActualWidth;
				          		Height = parent.ActualHeight;

				          		parent.SizeChanged += (s, e) =>
				          		                      {
				          		                      	if (Orientation == Orientation.Vertical)
				          		                      	{
				          		                      		Width = e.NewSize.Width - (padding.Left + padding.Right);

				          		                      		if (Parent is ScrollViewer)
				          		                      		{
				          		                      			if (((ScrollViewer) Parent).ComputedVerticalScrollBarVisibility ==
				          		                      			    Visibility.Visible)
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
		}

		public Orientation Orientation
		{
			get { return (Orientation) GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}

		private static void OnOrientationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			((HorizontalFlowPanel) sender).UpdateLayout();
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			var compositeSize = new Size(0, 0);

			foreach (FrameworkElement child in Children)
			{
				child.Measure(availableSize);

				if (Orientation == Orientation.Vertical)
				{
					compositeSize.Width = Math.Max(compositeSize.Width, child.DesiredSize.Width);
					compositeSize.Height += child.DesiredSize.Height;
				}
				else if (Orientation == Orientation.Horizontal)
				{
					compositeSize.Width += double.IsNaN(child.Width) ? child.DesiredSize.Width : child.Width;
					compositeSize.Height = Math.Max(compositeSize.Height, double.IsNaN(child.Height) ? child.DesiredSize.Height : child.Height);
				}
			}

			return compositeSize;
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			double x = 0.0;
			double y = 0.0;

			if (Orientation == Orientation.Vertical)
			{
				foreach (UIElement child in Children)
				{
					var r = new Rect(0, y, finalSize.Width, child.DesiredSize.Height);
					child.Arrange(r);
					y += child.DesiredSize.Height;
				}
			}
			else if (Orientation == Orientation.Horizontal)
			{
				var actualAvailableWidth = finalSize.Width;

				var fixedWidthCount = 0;
				var fixedWidthTotal = 0.0;

				foreach (FrameworkElement child in Children)
				{
					if (!double.IsNaN(child.Width))
					{
						fixedWidthCount++;
						fixedWidthTotal += child.Width;
					}
				}

				var totalRemainingWidth = finalSize.Width - fixedWidthTotal;
				var remainingWidthCount = Children.Count - fixedWidthCount;
				var remainingWidth = totalRemainingWidth/remainingWidthCount;

				foreach (FrameworkElement child in Children)
				{
					var width = double.IsNaN(child.Width) ? remainingWidth : child.DesiredSize.Width;
					var r = new Rect(x, 0, width, finalSize.Height);
					child.Arrange(r);
					x += width;
					var rect = LayoutInformation.GetLayoutSlot(child);
				}
			}

			return base.ArrangeOverride(finalSize);
		}
	}
}