namespace Subfuzion.Silverlight.UI.Charting
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Windows;

	public class SpendingFunctionViewModel : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged(string property)
		{
			var handlers = PropertyChanged;
			if (handlers != null)
			{
				handlers(this, new PropertyChangedEventArgs(property));
			}
		}

		#endregion

		private Dictionary<OneParameterFamily, OneParameterSpendingFunction> _spendingFunctions = new Dictionary<OneParameterFamily, OneParameterSpendingFunction>(); 

		public SpendingFunctionViewModel()
		{
			var sf = new OneParameterSpendingFunction
			{
				SpendingFunction = OneParameterSpendingFunctions.HwangShihDeCaniFunction,
				InverseSpendingFunction = OneParameterSpendingFunctions.HwangShihDeCaniFunctionInverse,
				ParameterSpendingFunction = OneParameterSpendingFunctions.HwangShihDeCaniFunctionSpendingParameter,
				InterimSpendingParameter = 0.025,
				InterimSpendingParameterMaximum = 0.025,
				InterimSpendingParameterMinimum = 0.0,
				SpendingFunctionParameter = -8.0,
				SpendingFunctionParameterMaximum = 40.0,
				SpendingFunctionParameterMinimum = -40.0,
				Timing = 0.5,
				TimingMaximum = 1.0,
				TimingMinimum = 0.0,
			};

			_spendingFunctions.Add(OneParameterFamily.HwangShihDeCani, sf);

			sf = new OneParameterSpendingFunction
			{
				SpendingFunction = OneParameterSpendingFunctions.PowerFunction,
				InverseSpendingFunction = OneParameterSpendingFunctions.PowerFunctionInverse,
				ParameterSpendingFunction = OneParameterSpendingFunctions.PowerFunctionSpendingParameter,
				SpendingFunctionParameter = 4,
				SpendingFunctionParameterMaximum = 15.0,
				SpendingFunctionParameterMinimum = 0.001,
				InterimSpendingParameter = 0.025,
				InterimSpendingParameterMaximum = 0.025,
				InterimSpendingParameterMinimum = 0.0,
				Timing = 0.5,
				TimingMaximum = 1.0,
				TimingMinimum = 0.0,
			};

			_spendingFunctions.Add(OneParameterFamily.Power, sf);

			sf = new OneParameterSpendingFunction
			{
				SpendingFunction = OneParameterSpendingFunctions.PowerFunction,
				InverseSpendingFunction = OneParameterSpendingFunctions.PowerFunctionInverse,
				ParameterSpendingFunction = OneParameterSpendingFunctions.PowerFunctionSpendingParameter,
				SpendingFunctionParameter = 4,
				SpendingFunctionParameterMaximum = 15.0,
				SpendingFunctionParameterMinimum = 0.001,
				InterimSpendingParameter = 0.025,
				InterimSpendingParameterMaximum = 0.025,
				InterimSpendingParameterMinimum = 0.0,
				Timing = 0.5,
				TimingMaximum = 1.0,
				TimingMinimum = 0.0,
			};

			_spendingFunctions.Add(OneParameterFamily.Exponential, sf);
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
					NotifyPropertyChanged("CurrentPlotFunction");
					NotifyPropertyChanged("Coordinates");
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

		/// <summary>
		/// Gets or sets the CurrentPlotFunction property.
		/// </summary>
		public OneParameterSpendingFunction CurrentPlotFunction
		{
			get { return _spendingFunctions[CurrentSpendingFunction]; }
		}

		#endregion

		#region Coordinates property

		/// <summary>
		/// Gets or sets the Coordinates property.
		/// </summary>
		public ObservableCollection<Point> Coordinates
		{
			get { return CurrentPlotFunction.Coordinates; }
		}

		#endregion

		#region SpendingFunctionParameter property

		/// <summary>
		/// Gets or sets the SpendingFunctionParameter property.
		/// </summary>
		public double SpendingFunctionParameter
		{
			get { return CurrentPlotFunction.SpendingFunctionParameter; }

			set
			{
				if (Math.Abs(CurrentPlotFunction.SpendingFunctionParameter - value) > double.Epsilon)
				{
					CurrentPlotFunction.SpendingFunctionParameter = value;
					NotifyPropertyChanged("SpendingFunctionParameter");
				}
			}
		}

		public double SpendingFunctionParameterMinimum
		{
			get { return CurrentPlotFunction.SpendingFunctionParameterMinimum; }
		}

		public double SpendingFunctionParameterMaximum
		{
			get { return CurrentPlotFunction.SpendingFunctionParameterMaximum; }
		}

		#endregion

		#region InterimSpendingParameter property

		/// <summary>
		/// Gets or sets the InterimSpendingParameter property.
		/// </summary>
		public double InterimSpendingParameter
		{
			get { return CurrentPlotFunction.InterimSpendingParameter; }

			set
			{
				if (Math.Abs(CurrentPlotFunction.InterimSpendingParameter - value) > double.Epsilon)
				{
					CurrentPlotFunction.InterimSpendingParameter = value;
					NotifyPropertyChanged("InterimSpendingParameter");
				}
			}
		}

		public double InterimSpendingParameterMinimum
		{
			get { return CurrentPlotFunction.InterimSpendingParameterMinimum; }
		}

		public double InterimSpendingParameterMaximum
		{
			get { return CurrentPlotFunction.InterimSpendingParameterMaximum; }
		}

		#endregion

		#region TimingParameter property

		/// <summary>
		/// Gets or sets the TimingParameter property.
		/// </summary>
		public double TimingParameter
		{
			get { return CurrentPlotFunction.Timing; }

			set
			{
				if (Math.Abs(CurrentPlotFunction.Timing - value) > double.Epsilon)
				{
					CurrentPlotFunction.Timing = value;
					NotifyPropertyChanged("TimingParameter");
				}
			}
		}

		public double TimingParameterMinimum
		{
			get { return CurrentPlotFunction.TimingMinimum; }
		}

		public double TimingParameterMaximum
		{
			get { return CurrentPlotFunction.TimingMaximum; }
		}

		#endregion
	}
}
