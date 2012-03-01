namespace Subfuzion.Silverlight.UI.Charting.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Windows;
	using Models;
	using gsDesign.Design.SpendingFunctions;
	using gsDesign.Design.SpendingFunctions.OneParameter;
	using OneParameterSpendingFunction = Charting.OneParameterSpendingFunction;

	public class SpendingFunctionViewModel : INotifyPropertyChanged
	{
		#region Notification Support

		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged(string property)
		{
			PropertyChangedEventHandler handlers = PropertyChanged;
			if (handlers != null)
			{
				try
				{
					handlers(this, new PropertyChangedEventArgs(property));
				}
				catch (Exception e)
				{
					Log("NotifyPropertyChanged exception", e.Message);
				}
			}
		}

		private void NotifyParameterUpdates()
		{
			Log("NotifyParameterUpdates");

			NotifyPropertyChanged("SpendingParameter");
			NotifyPropertyChanged("SpendingParameterMaximum");
			NotifyPropertyChanged("SpendingParameterMinimum");

			NotifyPropertyChanged("InterimSpendingParameter");
			NotifyPropertyChanged("InterimSpendingParameterMaximum");
			NotifyPropertyChanged("InterimSpendingParameterMinimum");

			NotifyPropertyChanged("TimingParameterMaximum");
			NotifyPropertyChanged("TimingParameterMinimum");
			NotifyPropertyChanged("TimingParameter");
		}

		private void NotifyCoordinatesUpdate()
		{
			NotifyPropertyChanged("Coordinates");
		}

		#endregion

		private readonly Dictionary<OneParameterFamily, OneParameterSpendingFunction> _spendingFunctions =
			new Dictionary<OneParameterFamily, OneParameterSpendingFunction>();

		/// <summary>
		/// Initialize the supported spending functions
		/// </summary>
		public SpendingFunctionViewModel()
		{
			var hsdsf = new HwangShiDeCaniSpendingFunctionModel();
			_spendingFunctions.Add(OneParameterFamily.HwangShihDeCani, hsdsf);

			var psf = new PowerSpendingFunctionModel();
			_spendingFunctions.Add(OneParameterFamily.Power, psf);

			var esf = new ExponentialSpendingFunctionModel();
			_spendingFunctions.Add(OneParameterFamily.Exponential, esf);
		}

		#region Plotting Support

		#region CurrentPlotFunction property

		/// <summary>
		/// Gets or sets the CurrentPlotFunction property.
		/// </summary>
		public OneParameterSpendingFunction CurrentPlotFunction
		{
			get { return _spendingFunctions[CurrentSpendingFunctionFamily]; }
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

		#endregion

		#region Spending Function constraints

		#region CurrentSpendingFunction property

		private OneParameterFamily _currentSpendingFunctionFamily = OneParameterFamily.HwangShihDeCani;

		/// <summary>
		/// Gets or sets the CurrentSpendingFunctionFamily property.
		/// </summary>
		public OneParameterFamily CurrentSpendingFunctionFamily
		{
			get { return _currentSpendingFunctionFamily; }

			set
			{
				if (_currentSpendingFunctionFamily != value)
				{
					Log("CurrentSpendingFunctionFamily", "{0}", value.ToString());

					_currentSpendingFunctionFamily = value;

					foreach (OneParameterSpendingFunction oneParameterSpendingFunction in _spendingFunctions.Values)
					{
						oneParameterSpendingFunction.PlotUpdateMode = PlotUpdateMode;
						oneParameterSpendingFunction.Timing = TimingParameter;
						oneParameterSpendingFunction.InterimSpendingParameter = InterimSpendingParameter;
					}

					CurrentPlotFunction.Update();

					NotifyPropertyChanged("CurrentSpendingFunctionFamily");
					NotifyPropertyChanged("CurrentPlotFunction");
					NotifyParameterUpdates();
				}
			}
		}

		#endregion

		#region PlotUpdateMode property

		private PlotUpdateMode _plotUpdateMode = PlotUpdateMode.MoveLineWithPoint;

		/// <summary>
		/// Gets or sets the PlotConstraint property.
		/// </summary>
		public PlotUpdateMode PlotUpdateMode
		{
			get { return _plotUpdateMode; }

			set
			{
				if (_plotUpdateMode != value)
				{
					Log("PlotUpdateMode", "{0}", value.ToString());

					_plotUpdateMode = value;
					NotifyPropertyChanged("PlotUpdateMode");

					foreach (OneParameterSpendingFunction oneParameterSpendingFunction in _spendingFunctions.Values)
					{
						oneParameterSpendingFunction.PlotUpdateMode = value;
					}
				}
			}
		}

		#endregion

		#region SpendingFunctionBounds property

		private SpendingFunctionBounds _spendingFunctionBounds;

		public SpendingFunctionBounds SpendingFunctionBounds
		{
			get { return _spendingFunctionBounds; }

			set
			{
				if (_spendingFunctionBounds != value)
				{
					Log("SpendingFunctionBounds", "{0}", value.ToString());

					_spendingFunctionBounds = value;
					NotifyPropertyChanged("SpendingFunctionBounds");

					NotifyPropertyChanged("SpendingParameterMaximum");
					NotifyPropertyChanged("SpendingParameterMinimum");
					NotifyPropertyChanged("SpendingParameter");

					NotifyPropertyChanged("InterimSpendingParameterMaximum");
					NotifyPropertyChanged("LowerBoundsSpendingIsEnabled");
				}
			}
		}

		#endregion

		#region LowerBoundsSpending property

		private SpendingFunctionLowerBoundSpending _lowerBoundsSpending;

		public SpendingFunctionLowerBoundSpending LowerBoundsSpending
		{
			get { return _lowerBoundsSpending; }

			set
			{
				if (_lowerBoundsSpending != value)
				{
					Log("LowerBoundsSpending", "{0}", value.ToString());

					_lowerBoundsSpending = value;
					NotifyPropertyChanged("LowerBoundsSpending");
					NotifyPropertyChanged("InterimSpendingParameterMaximum");
				}
			}
		}

		private bool _lowerBoundsSpendingIsEnabled;

		public bool LowerBoundsSpendingIsEnabled
		{
			get { return SpendingFunctionBounds == SpendingFunctionBounds.LowerSpending; }
		}

		#endregion

		#region Alpha property

		private double _alpha = 0.025;

		public double Alpha
		{
			get { return _alpha * 100; }

			set
			{
				if (value >= Beta) value = Beta - 0.1;
				value /= 100;
				_alpha = value;
				NotifyPropertyChanged("Alpha");
			}
		}

		public double AlphaMinimum
		{
			get { return 0.0; }
		}

		public double AlphaMaximum
		{
			get { return 99.9; }
		}

		#endregion

		#region Beta property

		private double _beta = 0.1;

		public double Beta
		{
			get { return 100 - (100 * _beta); }

			set
			{
				if (value <= Alpha) value = Alpha + 0.1;
				value = (100 - value) / 100;
				_beta = value;
				NotifyPropertyChanged("Beta");
			}
		}

		public double BetaMinimum
		{
			get { return 0.0; }
		}

		public double BetaMaximum
		{
			get { return 100.0; }
		}

		#endregion

		#endregion

		#region Spending Function input parameters

		#region SpendingParameter property

		private double _gammaUpper = -8.0;
		private double _gammaLower = -1.0;
		private double _rhoUpper = 4.0;
		private double _rhoLower = 2.0;
		private double _nuUpper = 0.75;
		private double _nuLower = 0.3;

		public double SpendingParameter
		{
			get
			{
				switch (CurrentSpendingFunctionFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending
							? _gammaUpper
							: _gammaLower;

					case OneParameterFamily.Power:
						return SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending
							? _rhoUpper
							: _rhoLower;

					case OneParameterFamily.Exponential:
						return SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending
							? _nuUpper
							: _nuLower;
				}

				return 0.0;
			}

			set
			{
				switch (CurrentSpendingFunctionFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
							_gammaUpper = value;
						else
							_gammaLower = value;
						break;

					case OneParameterFamily.Power:
						if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
							_rhoUpper = value;
						else
							_rhoLower = value;
						break;

					case OneParameterFamily.Exponential:
						if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
							_nuUpper = value;
						else
							_nuLower = value;
						break;
				}


				if (Math.Abs(CurrentPlotFunction.SpendingFunctionParameter - value) > double.Epsilon)
				{
					CurrentPlotFunction.SpendingFunctionParameter = value;
					NotifyParameterUpdates();
					CurrentPlotFunction.Update();
				}
			}
		}

		public double SpendingParameterMinimum
		{
			get
			{
				switch (CurrentSpendingFunctionFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return -40.0;

					case OneParameterFamily.Power:
						return 0.0;

					case OneParameterFamily.Exponential:
						return 0.0;

				}

				return 0.0;
			}
		}

		public double SpendingParameterMaximum
		{
			get
			{
				switch (CurrentSpendingFunctionFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return 40.0;

					case OneParameterFamily.Power:
						return 15.0;

					case OneParameterFamily.Exponential:
						return 1.5;

				}

				return 1.0;
			}
		}

		public double SpendingParameterIncrement
		{
			get { return 0.001; }
		}

		public int SpendingParameterPrecision
		{
			get { return 5; }
		}

		#endregion

		#region InterimSpendingParameter property

		private double? _interimSpendingUpper;
		private double? _interimSpendingLowerBeta;
		private double? _interimSpendingLowerH0;

		public double InterimSpendingParameter
		{
			get
			{
				if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
				{
					if (!_interimSpendingUpper.HasValue) _interimSpendingUpper = InterimSpendingParameterMaximum;
					return _interimSpendingUpper.Value;
				}

				if (LowerBoundsSpending == SpendingFunctionLowerBoundSpending.BetaSpending)
				{
					if (!_interimSpendingLowerBeta.HasValue) _interimSpendingLowerBeta = InterimSpendingParameterMaximum;
					return _interimSpendingLowerBeta.Value;
				}

				// else
				if (!_interimSpendingLowerH0.HasValue) _interimSpendingLowerH0 = InterimSpendingParameterMaximum;
				return _interimSpendingLowerH0.Value;
			}

			set
			{
				if (value < InterimSpendingParameterMinimum) value = InterimSpendingParameterMinimum;
				if (value > InterimSpendingParameterMaximum) value = InterimSpendingParameterMaximum;

				if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
				{
					_interimSpendingUpper = value;
				}
				else if (LowerBoundsSpending == SpendingFunctionLowerBoundSpending.BetaSpending)
				{
					_interimSpendingLowerBeta = value;
				}
				else
				{
					_interimSpendingLowerH0 = value;
				}


				if (Math.Abs(CurrentPlotFunction.InterimSpendingParameter - value) > double.Epsilon)
				{
					CurrentPlotFunction.InterimSpendingParameter = value;
					NotifyParameterUpdates();
					CurrentPlotFunction.Update();
				}
			}
		}

		public double InterimSpendingParameterMinimum
		{
			get { return 0.0; }
		}

		public double InterimSpendingParameterMaximum
		{
			get
			{
				if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
				{
					return _alpha;
				}

				if (LowerBoundsSpending == SpendingFunctionLowerBoundSpending.BetaSpending)
				{
					return _beta;
				}

				// else LowerBoundsSpending == SpendingFunctionLowerBoundSpending.H0Spending
				return 1 - _alpha;
			}
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
					NotifyParameterUpdates();
					CurrentPlotFunction.Update();
				}
			}
		}

		public double TimingParameterMinimum
		{
			get { return 0.0; }
		}

		public double TimingParameterMaximum
		{
			get { return 1.0; }
		}

		#endregion

		#endregion

		#region Logging

		protected void Log(string function, string message = "", params object[] args)
		{
			if (string.IsNullOrWhiteSpace(message) && string.IsNullOrWhiteSpace(function)) return;

			string log = string.Format("[{0}.{1}] {2}", GetType().Name, function, string.Format(message, args));
			LogHistory = string.IsNullOrWhiteSpace(LogHistory) ? log : LogHistory + "\n" + log;
		}

		#region LogMessage property

		private string _logMessage;

		/// <summary>
		/// Gets or sets the LogMessage property.
		/// </summary>
		public string LogMessage
		{
			get { return _logMessage; }

			set
			{
				if (_logMessage != value)
				{
					_logMessage = value;
					NotifyPropertyChanged("LogMessage");
					Log(_logMessage);
				}
			}
		}

		#endregion

		#region LogHistory property

		private string _logHistory;

		/// <summary>
		/// Gets or sets the LogHistory property.
		/// </summary>
		// [Display(Name = "LogHistory",
		//	Description = "")]
		public string LogHistory
		{
			get { return _logHistory; }

			set
			{
				// use for float comparison:
				// if (Math.Abs(_logHistory - value) > float.Epsilon)

				// use for double comparison:
				// if (Math.Abs(_logHistory - value) > double.Epsilon)

				if (_logHistory != value)
				{
					_logHistory = value;
					NotifyPropertyChanged("LogHistory");
				}
			}
		}

		#endregion

		#endregion
	}
}