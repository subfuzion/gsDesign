namespace Subfuzion.Silverlight.UI.Charting
{
	using System;
	using System.Collections.ObjectModel;
	using System.ComponentModel;

	public class SpendingFunctionViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged(string property)
		{
			var handlers = PropertyChanged;
			if (handlers != null)
			{
				handlers(this, new PropertyChangedEventArgs(property));
			}
		}

		#region CurrentSpendingFunction property

		private OneParameterFamily _currentSpendingFunction;

		/// <summary>
		/// Gets or sets the CurrentSpendingFunction property.
		/// </summary>
		public OneParameterFamily CurrentSpendingFunction
		{
			get { return _currentSpendingFunction; }

			set
			{
				if (_currentSpendingFunction != value)
				{
					_currentSpendingFunction = value;
					NotifyPropertyChanged("CurrentSpendingFunction");
				}
			}
		}

		#endregion

		#region PlotConstraint property

		private PlotConstraint _plotConstraint = PlotConstraint.MoveLineWithPoint;

		/// <summary>
		/// Gets or sets the PlotConstraint property.
		/// </summary>
		public PlotConstraint PlotConstraint
		{
			get { return _plotConstraint; }

			set
			{
				if (_plotConstraint != value)
				{
					_plotConstraint = value;
					NotifyPropertyChanged("PlotConstraint");
				}
			}
		}

		#endregion

		#region Alpha property

		private double _alpha = 0.025;

		/// <summary>
		/// Gets or sets the Alpha property.
		/// </summary>
		public double Alpha
		{
			get { return _alpha; }

			set
			{
				if (Math.Abs(_alpha - value) > double.Epsilon)
				{
					_alpha = value;
					NotifyPropertyChanged("Alpha");
				}
			}
		}

		#region AlphaMinimum property

		public double AlphaMinimum
		{
			get { return 0.0; }
		}

		#endregion

		#region AlphaMaximum property

		public double AlphaMaximum
		{
			get { return 1.0; }
		}

		#endregion


		#endregion

		#region CurrentPlotFunction property

		private PlotFunction _currentPlotFunction;

		/// <summary>
		/// Gets or sets the CurrentPlotFunction property.
		/// </summary>
		public PlotFunction CurrentPlotFunction
		{
			get { return _currentPlotFunction; }

			set
			{
				if (_currentPlotFunction != value)
				{
					_currentPlotFunction = value;
					NotifyPropertyChanged("CurrentPlotFunction");
				}
			}
		}

		#endregion


	}
}
