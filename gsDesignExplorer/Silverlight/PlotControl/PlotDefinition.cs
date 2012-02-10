namespace Subfuzion.Silverlight.UI.Charting
{
	using System;
	using System.Collections.ObjectModel;
	using System.Collections.Specialized;
	using System.ComponentModel;
	using System.Windows;

	public delegate double[] InputFunction(double minimum, double maximum);
	public delegate double PlotFunction(double x);

	public class PlotParameters : INotifyPropertyChanged
	{
		// default function: y = f(x) => x
		public static PlotFunction DefaultPlotFunction = x => x;

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(string property)
		{
			var handlers = PropertyChanged;
			if (handlers != null)
			{
				handlers(this, new PropertyChangedEventArgs(property));
			}
		}


		#region IntervalCount property

		private int _intervalCount = 1;

		/// <summary>
		/// Gets or sets the IntervalCount property.
		/// </summary>
		// [Display(Name = "IntervalCount",
		//	Description = "")]
		public int IntervalCount
		{
			get { return _intervalCount; }

			set
			{
				if (_intervalCount != value && value >= 1)
				{
					_intervalCount = value;
					NotifyPropertyChanged("IntervalCount");
				}
			}
		}

		#endregion IntervalCount

		public double[] DefaultInputFunction(double minimum, double maximum)
		{
			var incrementCount = IntervalCount + 1;

			var data = new double[incrementCount];

			data[0] = minimum;
			data[data.Length - 1] = maximum;

			var increment = (maximum - minimum)/IntervalCount;

			for (var i = 1; i < data.Length - 1; i++)
			{
				data[i] = data[i - 1] + increment;
			}

			return data;
		}

		#region InputFunction property

		private InputFunction _inputFunction;

		/// <summary>
		/// Gets or sets the InputFunction property.
		/// </summary>
		// [Display(Name = "InputFunction",
		//	Description = "")]
		public InputFunction InputFunction
		{
			get { return _inputFunction ?? DefaultInputFunction; }

			set
			{
				if (_inputFunction != value)
				{
					_inputFunction = value;
					NotifyPropertyChanged("InputFunction");
				}
			}
		}

		#endregion InputFunction


		#region Origin property

		private Point _origin;

		/// <summary>
		/// Gets or sets the Origin property.
		/// </summary>
		// [Display(Name = "Origin",
		//	Description = "")]
		public Point Origin
		{
			get { return _origin; }

			set
			{
				if (Math.Abs(_origin.X - value.X) > double.Epsilon
					&& Math.Abs(_origin.Y - value.Y) > double.Epsilon)
				{
					_origin = value;
					NotifyPropertyChanged("Origin");
				}
			}
		}

		#endregion Origin


		#region MinimumX property

		private double _minimumX;

		/// <summary>
		/// Gets or sets the MinimumX property.
		/// </summary>
		public double MinimumX
		{
			get { return _minimumX; }

			set
			{
				if (Math.Abs(_minimumX - value) > double.Epsilon)
				{
					_minimumX = value;
					NotifyPropertyChanged("MinimumX");
					ComputeCoordinates();
				}
			}
		}

		#endregion MinimumX

		#region MaximumX property

		private double _maximumX;

		/// <summary>
		/// Gets or sets the MaximumX property.
		/// </summary>
		public double MaximumX
		{
			get { return _maximumX; }

			set
			{
				if (Math.Abs(_maximumX - value) > double.Epsilon)
				{
					_maximumX = value;
					NotifyPropertyChanged("MaximumX");
					ComputeCoordinates();
				}
			}
		}

		#endregion MaximumX

		#region MinimumY property

		private double _minimumY;

		/// <summary>
		/// Gets or sets the MinimumY property.
		/// </summary>
		public double MinimumY
		{
			get { return _minimumY; }

			private set
			{
				if (Math.Abs(_minimumY - value) > double.Epsilon)
				{
					_minimumY = value;
					NotifyPropertyChanged("MinimumY");
				}
			}
		}

		#endregion MinimumY

		#region MaximumY property

		private double _maximumY;

		/// <summary>
		/// Gets or sets the MaximumY property.
		/// </summary>
		public double MaximumY
		{
			get { return _maximumY; }

			private set
			{
				if (Math.Abs(_maximumY - value) > double.Epsilon)
				{
					_maximumY = value;
					NotifyPropertyChanged("MaximumY");
				}
			}
		}

		#endregion MaximumY

		#region PlotFunction property

		private PlotFunction _plotFunction;

		/// <summary>
		/// Gets or sets the PlotFunction property.
		/// </summary>
		public PlotFunction PlotFunction
		{
			get { return _plotFunction ?? DefaultPlotFunction; }

			private set
			{
				if (_plotFunction != value)
				{
					_plotFunction = value;
					NotifyPropertyChanged("PlotFunction");
				}
			}
		}

		#endregion PlotFunction

		#region Data property

		private ObservableCollection<double> _data;

		/// <summary>
		/// Gets or sets the Data property.
		/// </summary>
		// [Display(Name = "Data",
		//	Description = "")]
		public ObservableCollection<double> Data
		{
			get { return _data; }

			set
			{
				if (_data != value)
				{
					if (_data != null) _data.CollectionChanged -= DataOnCollectionChanged;
					_data = value;
					_data.CollectionChanged += DataOnCollectionChanged;
					NotifyPropertyChanged("Data");
					ComputeCoordinates();
				}
			}
		}

		private void DataOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
		{
			ComputeCoordinates();
		}

		#endregion Data

		#region Coordinates property

		private ObservableCollection<Point> _coordinates;

		/// <summary>
		/// Gets or sets the Coordinates property.
		/// </summary>
		// [Display(Name = "Coordinates",
		//	Description = "")]
		public ObservableCollection<Point> Coordinates
		{
			get { return _coordinates; }

			set
			{
				if (_coordinates != value)
				{
					_coordinates = value;
					NotifyPropertyChanged("Coordinates");
				}
			}
		}

		#endregion Coordinates

		#region Implementation

		private void ComputeCoordinates()
		{
			if (Data == null || Data.Count == 0)
			{
				Coordinates = null;
			}


			MinimumX = Data[0];


			NotifyPropertyChanged("MinimumX");
			NotifyPropertyChanged("MaximumX");
			NotifyPropertyChanged("MinimumY");
			NotifyPropertyChanged("MaximumY");
			NotifyPropertyChanged("Coordinates");
		}

		#endregion

	}
}