namespace Subfuzion.Silverlight.UI.Charting
{
	using System.Collections.ObjectModel;
	using System.Windows;
	using System.Windows.Media;
	using System.Windows.Shapes;

	public class LinePlotRenderBase : LinePlotBase
	{
		#region PlotFunction property

		public PlotFunction PlotFunction
		{
			get { return (PlotFunction) GetValue(PlotFunctionProperty); }
			set { SetValue(PlotFunctionProperty, value); }
		}

		public static DependencyProperty PlotFunctionProperty = DependencyProperty.Register(
			"PlotFunction",
			typeof (PlotFunction),
			typeof (LinePlot),
			new PropertyMetadata(PlotFunctionChangedHandler));

		private static void PlotFunctionChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var linePlot = dependencyObject as LinePlot;
			if (linePlot != null)
			{
				linePlot.OnPlotFunctionChanged((PlotFunction) args.NewValue, (PlotFunction) args.OldValue);
			}
		}

		protected virtual void OnPlotFunctionChanged(PlotFunction newValue, PlotFunction oldValue)
		{
			// handle property changed here if the old value is important; otherwise, just pass on new value
			OnPlotFunctionChanged(newValue);
		}

		protected virtual void OnPlotFunctionChanged(PlotFunction newValue)
		{
			// add handler code
			if (newValue != null)
			{
				Coordinates = newValue.Coordinates;
			}
		}

		#endregion


		#region Coordinates

		public static DependencyProperty CoordinatesProperty = DependencyProperty.Register(
			"Coordinates",
			typeof (ObservableCollection<Point>),
			typeof (LinePlot),
			new PropertyMetadata(CoordinatesChangedHandler));

		public ObservableCollection<Point> Coordinates
		{
			get { return (ObservableCollection<Point>) GetValue(CoordinatesProperty); }
			set { SetValue(CoordinatesProperty, value); }
		}

		private static void CoordinatesChangedHandler(DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs args)
		{
			var interactivePlot = dependencyObject as LinePlot;
			if (interactivePlot != null)
			{
				interactivePlot.OnCoordinatesChanged((ObservableCollection<Point>) args.NewValue,
					(ObservableCollection<Point>) args.OldValue);
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
			typeof (Polyline),
			typeof (LinePlot),
			new PropertyMetadata(new Polyline {Stroke = new SolidColorBrush(Colors.Black), StrokeThickness = 1},
				PolylineChangedHandler));

		public Polyline Polyline
		{
			get { return (Polyline) GetValue(PolylineProperty); }
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

		#region Overridable methods

		public override void UpdatePlotDisplay()
		{
			OnCoordinatesChanged(Coordinates);
		}

		#endregion
	}
}