namespace Subfuzion.Helpers.UI
{
	using System.Windows;
	using System.Windows.Media;

	public class CoordinateTransformer
	{
		private readonly GeneralTransform _childToContainerTransform;
		private readonly GeneralTransform _containerToChildTransform;

		public CoordinateTransformer(UIElement container, UIElement child)
		{
			Container = container;
			Child = child;

			_childToContainerTransform = child.TransformToVisual(container);
			_containerToChildTransform = container.TransformToVisual(child);
		}

		public UIElement Container { get; private set; }

		public UIElement Child { get; private set; }

		public Point TransformToContainerCoordinates(Point p)
		{
			return _childToContainerTransform.Transform(p);
		}

		public Rect TransformToContainerCoordinates(Rect r)
		{
			return _childToContainerTransform.TransformBounds(r);
		}

		public Point TransformToChildCoordinates(Point p)
		{
			return _containerToChildTransform.Transform(p);
		}

		public Rect TransformToChildCoordinates(Rect r)
		{
			return _containerToChildTransform.TransformBounds(r);
		}
	}
}