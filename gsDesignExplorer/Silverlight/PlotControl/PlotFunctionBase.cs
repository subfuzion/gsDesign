namespace Subfuzion.Silverlight.UI.Charting
{
	using System;
	using System.Collections.ObjectModel;
	using System.Collections.Specialized;
	using System.ComponentModel;
	using System.Windows;

	public abstract class PlotFunctionBase : INotifyPropertyChanged, INotifyCollectionChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public event NotifyCollectionChangedEventHandler CollectionChanged;

		public abstract void Update();

		#region Coordinates property

		private ObservableCollection<Point> _coordinates;

		/// <summary>
		/// Gets or sets the Coordinates property.
		/// </summary>
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

		#region MinimumX property

		private double _minimumX = 0.0;

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
					Update();
				}
			}
		}

		#endregion MinimumX

		#region MaximumX property

		private double _maximumX = 1.0;

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
					Update();
				}
			}
		}

		#endregion MaximumX

		protected void NotifyPropertyChanged(string property)
		{
			var handlers = PropertyChanged;
			if (handlers != null)
			{
				handlers(this, new PropertyChangedEventArgs(property));
			}
		}
	}
}