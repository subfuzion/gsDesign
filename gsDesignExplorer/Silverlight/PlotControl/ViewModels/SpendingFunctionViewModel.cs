namespace Subfuzion.Silverlight.UI.Charting.ViewModels
{
	using System;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Windows;
	using Models;
	using gsDesign.Design.SpendingFunctions;
	using gsDesign.Design.SpendingFunctions.OneParameter;

	/// <summary>
	/// Solve for y
	/// </summary>
	/// <param name="yMax"></param>
	/// <param name="x">timing</param>
	/// <param name="spendingParameter"></param>
	/// <returns></returns>
	public delegate double SpendingFunction(double yMax, double x, double spendingParameter);

	/// <summary>
	/// Solve for x
	/// </summary>
	/// <param name="yMax"></param>
	/// <param name="y">interim spending</param>
	/// <param name="spendingParameter"></param>
	/// <returns></returns>
	public delegate double InverseSpendingFunction(double yMax, double y, double spendingParameter);

	/// <summary>
	/// Solve for spending parameter
	/// </summary>
	/// <param name="yMax"></param>
	/// <param name="y">interim spending</param>
	/// <param name="x">timing</param>
	/// <returns></returns>
	public delegate double FittingSpendingFunction(double yMax, double y, double x);

	public class SpendingFunctionViewModel : INotifyPropertyChanged
	{
		/// <summary>
		/// Initialize the supported spending functions
		/// </summary>
		public SpendingFunctionViewModel()
		{
			var coordinates = new ObservableCollection<Point>();
			for (int i = 0; i < 30; i++)
			{
				coordinates.Add(new Point(0, 0));
			}

			Coordinates = coordinates;
		}

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
			// Log("NotifyParameterUpdates");

			NotifyPropertyChanged("SpendingParameter");
			NotifyPropertyChanged("SpendingParameterMaximum");
			NotifyPropertyChanged("SpendingParameterMinimum");
			NotifyPropertyChanged("SpendingParameterControlMaximum");
			NotifyPropertyChanged("SpendingParameterControlMinimum");
			NotifyPropertyChanged("SpendingParameterIncrement");
			NotifyPropertyChanged("SpendingParameterPrecision");
			NotifyPropertyChanged("SpendingParameterMaximumString");
			NotifyPropertyChanged("SpendingParameterMinimumString");

			NotifyPropertyChanged("TimingParameterMaximum");
			NotifyPropertyChanged("TimingParameterMinimum");
			NotifyPropertyChanged("TimingParameter");

			NotifyPropertyChanged("InterimSpendingParameter");
			NotifyPropertyChanged("InterimSpendingParameterMaximum");
			NotifyPropertyChanged("InterimSpendingParameterMinimum");
			NotifyPropertyChanged("InterimSpendingParameterControlMaximum");
			NotifyPropertyChanged("InterimSpendingParameterControlMinimum");
		}

		private void NotifyCoordinatesUpdate()
		{
			NotifyPropertyChanged("TimingParameter");
			NotifyPropertyChanged("InterimSpendingParameter");
			NotifyPropertyChanged("Coordinates");
		}

		#endregion

		#region Logging

		private string _logHistory;
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

		protected void Log(string function, string message = "", params object[] args)
		{
			if (string.IsNullOrWhiteSpace(message) && string.IsNullOrWhiteSpace(function)) return;

			string log = string.Format("[{0}.{1}] {2}", GetType().Name, function, string.Format(message, args));
			LogHistory = string.IsNullOrWhiteSpace(LogHistory) ? log : LogHistory + "\n" + log;
		}

		#endregion

		#region Plotting Support

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
					NotifyCoordinatesUpdate();
				}
			}
		}

		#endregion Coordinates

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
					UpdateLine();
					NotifyPropertyChanged("CurrentSpendingFunctionFamily");
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

					UpdateLine();

					NotifyPropertyChanged("LowerBoundsSpending");
					NotifyPropertyChanged("InterimSpendingParameterMaximum");
				}
			}
		}

		public bool LowerBoundsSpendingIsEnabled
		{
			get { return SpendingFunctionBounds == SpendingFunctionBounds.LowerSpending; }
		}

		#endregion

		#region Alpha property

		private double _alpha = 0.025;

		/// <summary>
		/// Gets or sets alpha.
		/// 
		/// This will also update:
		///   * interim spending (y)
		///   * interim spending max (ymax)
		/// </summary>
		public double Alpha
		{
			get { return _alpha*100; }

			set
			{
				if (value >= Beta) value = Beta - 0.1;
				value /= 100;
				_alpha = value;
				NotifyPropertyChanged("Alpha");
				NotifyPropertyChanged("InterimSpendingParameterMaximum");
				NotifyPropertyChanged("InterimSpendingParameter");
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

		/// <summary>
		/// Gets or sets beta.
		/// 
		/// This will also update:
		///   * interim spending (y)
		///   * interim spending max (ymax)
		/// </summary>
		public double Beta
		{
			get { return 100 - (100*_beta); }

			set
			{
				if (value <= Alpha) value = Alpha + 0.1;
				value = (100 - value)/100;
				_beta = value;
				NotifyPropertyChanged("Beta");
				NotifyPropertyChanged("InterimSpendingParameterMaximum");
				NotifyPropertyChanged("InterimSpendingParameter");
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

		private double _gammaLower = -1.0;
		private double _gammaUpper = -8.0;
		private double _nuLower = 0.3;
		private double _nuUpper = 0.75;
		private double _rhoLower = 2.0;
		private double _rhoUpper = 4.0;

		/// <summary>
		/// Gets or sets the spending parameter.
		/// 
		/// - When adjusting the value, keep x (Timing) and solve for y (InterimSpending)
		///   using the spending function.
		/// 
		/// - Adjusting this value will also update:
		///   * interim spending value (y)
		///   * plot coordinates
		/// </summary>
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
				SetSpendingParameter(value);
				double y = ComputeInterimSpendingParameter();
				SetInterimSpendingParameter(y);
				UpdateLine();
				NotifyParameterUpdates();
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

		public string SpendingParameterMinimumString
		{
			get { return SpendingParameterMinimum.ToString(string.Format("F{0}", SpendingParameterPrecision)); }
		}

		public double SpendingParameterControlMinimum
		{
			get
			{
				switch (CurrentSpendingFunctionFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return SpendingParameterMinimum + 0.01;

					case OneParameterFamily.Power:
						return SpendingParameterMinimum + 0.001;

					case OneParameterFamily.Exponential:
						return SpendingParameterMinimum + 0.001;
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

		public string SpendingParameterMaximumString
		{
			get { return SpendingParameterMaximum.ToString(string.Format("F{0}", SpendingParameterPrecision)); }
		}

		public double SpendingParameterControlMaximum
		{
			get
			{
				switch (CurrentSpendingFunctionFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return SpendingParameterMaximum - 0.01;

					case OneParameterFamily.Power:
						return SpendingParameterMaximum - 0.001;

					case OneParameterFamily.Exponential:
						return SpendingParameterMaximum - 0.001;
				}

				return 1.0;
			}
		}

		public double SpendingParameterIncrement
		{
			get
			{
				switch (CurrentSpendingFunctionFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return 0.01;

					case OneParameterFamily.Power:
						return 0.001;

					case OneParameterFamily.Exponential:
						return 0.001;
				}

				return 0.001;
			}
		}

		public int SpendingParameterPrecision
		{
			get
			{
				switch (CurrentSpendingFunctionFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return 2;

					case OneParameterFamily.Power:
						return 3;

					case OneParameterFamily.Exponential:
						return 3;
				}

				return 3;
			}
		}

		public void SetSpendingParameter(double value)
		{
			if (value < SpendingParameterControlMinimum) value = SpendingParameterControlMinimum;
			if (value > SpendingParameterControlMaximum) value = SpendingParameterControlMaximum;

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
		}

		#endregion

		#region TimingParameter property

		private double _timingParameter;

		/// <summary>
		/// Gets or sets the timing (x) parameter.
		/// 
		/// - To update the line (move line with point), keep y (interim spending) and solve
		///   for the spending parameter using the fitting function
		/// 
		/// - To move the point along the line, keep the spending parameter and solve
		///   for y (interim spending) using the inverse spending function
		/// 
		/// - Adjusting this value will also update:
		///   (move line with point) =>
		///     * spending parameter
		///     * plot coordinates
		///   (move point along line) =>
		///     * interim spending (y)
		/// </summary>
		public double TimingParameter
		{
			get { return _timingParameter; }

			set
			{
				SetTimingParameter(value);

				if (PlotUpdateMode == PlotUpdateMode.MoveLineWithPoint)
				{
					// will trigger update notifications
					SpendingParameter = ComputeSpendingParameter();
				}
				else
				{
					double y = ComputeInterimSpendingParameter();
					SetInterimSpendingParameter(y);
					NotifyParameterUpdates();
				}
			}
		}

		public void UpdateCoordinate(double timing, double interimSpending)
		{
			if (PlotUpdateMode == PlotUpdateMode.MoveLineWithPoint)
			{
				SetTimingParameter(timing);
				SetInterimSpendingParameter(interimSpending);
				// will trigger update notifications
				SpendingParameter = ComputeSpendingParameter();
			}
			else
			{
				TimingParameter = timing;
			}
		}

		public double TimingParameterMinimum
		{
			get { return 0.0; }
		}

		public double TimingParameterControlMinimum
		{
			get { return 0.001; }
		}

		public double TimingParameterMaximum
		{
			get { return 1.0; }
		}

		public double TimingParameterControlMaximum
		{
			get { return 0.999; }
		}

		public double TimingParameterIncrement
		{
			get { return 0.010; }
		}

		public int TimingParameterPrecision
		{
			get { return 3; }
		}

		public void SetTimingParameter(double value)
		{
			_timingParameter = value;
		}

		#endregion

		#region InterimSpendingParameter property

		private double? _interimSpendingLowerBeta;
		private double? _interimSpendingLowerH0;
		private double? _interimSpendingUpper;

		/// <summary>
		/// Gets or sets the interim spending (y) parameter.
		/// 
		/// - To update the line (move line with point), keep x (Timing) and solve
		///   for the spending parameter using the fitting function
		/// 
		/// - To move the point along the line, keep the spending parameter and solve
		///   for x (Timing) using the inverse spending function
		/// 
		/// - Adjusting this value will also update:
		///   (move line with point) =>
		///     * spending parameter
		///     * plot coordinates
		///   (move point along line) =>
		///     * timing (x)
		/// </summary>
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
				SetInterimSpendingParameter(value);

				if (PlotUpdateMode == PlotUpdateMode.MoveLineWithPoint)
				{
					// will trigger update notifications
					SpendingParameter = ComputeSpendingParameter();
				}
				else
				{
					double x = ComputeTimingParameter();
					SetTimingParameter(x);
					NotifyParameterUpdates();
				}
			}
		}

		public double InterimSpendingParameterIncrement
		{
			get { return 0.001; }
		}

		public double InterimSpendingParameterPrecision
		{
			get { return 5; }
		}

		public double InterimSpendingParameterMinimum
		{
			get { return 0.0; }
		}

		public double InterimSpendingParameterControlMinimum
		{
			get { return InterimSpendingParameterMinimum + 0.00001; }
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

		public double InterimSpendingParameterControlMaximum
		{
			get { return InterimSpendingParameterMaximum - 0.00001; }
		}

		public void SetInterimSpendingParameter(double value)
		{
			if (value < InterimSpendingParameterControlMinimum) value = InterimSpendingParameterControlMinimum;
			if (value > InterimSpendingParameterControlMaximum) value = InterimSpendingParameterControlMaximum;

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
		}

		#endregion

		#endregion

		#region Methods

		/// <summary>
		/// Solve for y (interim spending)
		/// </summary>
		public SpendingFunction SpendingFunction
		{
			get
			{
				switch (CurrentSpendingFunctionFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return OneParameterSpendingFunctions.HwangShihDeCaniFunction;
					case OneParameterFamily.Power:
						return OneParameterSpendingFunctions.PowerFunction;
					case OneParameterFamily.Exponential:
						return OneParameterSpendingFunctions.ExponentialFunction;
				}

				throw new Exception("Unsupported");
			}
		}

		/// <summary>
		/// Solve for x (timing)
		/// </summary>
		public InverseSpendingFunction InverseSpendingFunction
		{
			get
			{
				switch (CurrentSpendingFunctionFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return OneParameterSpendingFunctions.HwangShihDeCaniFunctionInverse;
					case OneParameterFamily.Power:
						return OneParameterSpendingFunctions.PowerFunctionInverse;
					case OneParameterFamily.Exponential:
						return OneParameterSpendingFunctions.ExponentialFunctionInverse;
				}

				throw new Exception("Unsupported");
			}
		}

		/// <summary>
		/// Solve for spending parameter
		/// </summary>
		public FittingSpendingFunction FittingSpendingFunction
		{
			get
			{
				switch (CurrentSpendingFunctionFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return OneParameterSpendingFunctions.HwangShihDeCaniFunctionSpendingParameter;
					case OneParameterFamily.Power:
						return OneParameterSpendingFunctions.PowerFunctionSpendingParameter;
					case OneParameterFamily.Exponential:
						return OneParameterSpendingFunctions.ExponentialFunctionSpendingParameter;
				}

				throw new Exception("Unsupported");
			}
		}

		/// <summary>
		/// Solve for y (interim spending)
		/// </summary>
		public double ComputeInterimSpendingParameter()
		{
			double alpha = InterimSpendingParameterControlMaximum;
			double x = TimingParameter;
			double spendingParameter = SpendingParameter;

			double y = SpendingFunction(alpha, x, spendingParameter);
			return y;
		}

		/// <summary>
		/// Solve for x (timing)
		/// </summary>
		public double ComputeTimingParameter()
		{
			double alpha = InterimSpendingParameterControlMaximum;
			double y = InterimSpendingParameter;
			double spendingParameter = SpendingParameter;

			double x = InverseSpendingFunction(alpha, y, spendingParameter);
			return x;
		}

		/// <summary>
		/// Solve for spending parameter
		/// </summary>
		public double ComputeSpendingParameter()
		{
			double alpha = InterimSpendingParameterControlMaximum;
			double y = InterimSpendingParameter;
			double x = TimingParameter;

			double spendingParameter = FittingSpendingFunction(alpha, y, x);
			return spendingParameter;
		}

		public void UpdateAll()
		{
			NotifyParameterUpdates();
			UpdateLine();
		}

		public void UpdateLine()
		{
			int incrementCount = Coordinates.Count;
			int intervalCount = incrementCount - 1;

			double alpha = InterimSpendingParameterMaximum;
			double xMin = TimingParameterMinimum;
			double xMax = TimingParameterMaximum;

			Coordinates[0] = new Point(xMin, SpendingFunction(alpha, xMin, SpendingParameter));
			Coordinates[Coordinates.Count - 1] = new Point(xMax, SpendingFunction(alpha, xMax, SpendingParameter));

			// compute x-axis interval
			double increment = (xMax - xMin)/intervalCount;

			// compute y values for x values between min and max
			for (int i = 1; i < Coordinates.Count - 1; i++)
			{
				// timing = x
				double x = Coordinates[i - 1].X + increment;

				// y is a function of alpha, timing, & spending value
				double y = SpendingFunction(alpha, x, SpendingParameter);
				Coordinates[i] = new Point(x, y);
			}

			NotifyCoordinatesUpdate();

			if (LinePlotControl != null)
			{
				LinePlotControl.UpdatePlotDisplay();
			}
		}

		#region LinePlotControl property

		private LinePlot _linePlotControl;

		public LinePlot LinePlotControl
		{
			get { return _linePlotControl; }

			set
			{
				if (_linePlotControl != value)
				{
					_linePlotControl = value;
					NotifyPropertyChanged("LinePlotControl");
				}
			}
		}

		#endregion

		#endregion
	}
}