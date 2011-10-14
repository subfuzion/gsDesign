namespace Subfuzion.Helpers.UI
{
	using System;
	using System.Windows;
	using System.Windows.Controls;

	public class FlowPanel : Panel
	{
		protected override Size MeasureOverride(Size availableSize)
		{
			var compositeSize = new Size(0, 0);

			foreach (var child in Children)
			{
				child.Measure(availableSize);
				compositeSize.Width = Math.Max(compositeSize.Width, child.DesiredSize.Width);
				compositeSize.Height += Math.Max(compositeSize.Height, child.DesiredSize.Height);
			}

			return compositeSize;
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			var y = 0.0;


			foreach (var child in Children)
			{
				var r = new Rect(0, y, this.ActualWidth, child.DesiredSize.Height);
				child.Arrange(r);
				y += child.DesiredSize.Height;
			}

			return base.ArrangeOverride(finalSize);
		}
	}
}