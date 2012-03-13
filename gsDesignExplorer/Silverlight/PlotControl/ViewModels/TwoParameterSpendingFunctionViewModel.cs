namespace Subfuzion.Silverlight.UI.Charting.ViewModels
{
	using System;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Windows;
	using Models;
	using gsDesign.Design.SpendingFunctions;
	using gsDesign.Design.SpendingFunctions.OneParameter;

	public class TwoParameterSpendingFunctionViewModel : INotifyPropertyChanged
	{
		/// <summary>
		/// Initialize the supported spending functions
		/// </summary>
		public TwoParameterSpendingFunctionViewModel()
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
					Log("NotifyPropertyChanged exception", "{0}\n * {1}", property, e.ToString());
				}
			}
		}

		private void NotifyParameterUpdates()
		{
			// Log("NotifyParameterUpdates");

			NotifyParameterAUpdates();
			NotifyParameterBUpdates();
		}

		private void NotifyParameterAUpdates()
		{
			// Log("NotifyParameterUpdates");

			NotifyPropertyChanged("SpendingParameterA");
			NotifyPropertyChanged("SpendingParameterAMaximum");
			NotifyPropertyChanged("SpendingParameterAMinimum");
			NotifyPropertyChanged("SpendingParameterAControlMaximum");
			NotifyPropertyChanged("SpendingParameterAControlMinimum");
			NotifyPropertyChanged("SpendingParameterAIncrement");
			NotifyPropertyChanged("SpendingParameterAPrecision");
			NotifyPropertyChanged("SpendingParameterAMaximumString");
			NotifyPropertyChanged("SpendingParameterAMinimumString");

			NotifyPropertyChanged("TimingParameterAMaximum");
			NotifyPropertyChanged("TimingParameterAMinimum");
			NotifyPropertyChanged("TimingParameterA");

			NotifyPropertyChanged("InterimSpendingParameterA");
			NotifyPropertyChanged("InterimSpendingParameterAMaximum");
			NotifyPropertyChanged("InterimSpendingParameterAMinimum");
			NotifyPropertyChanged("InterimSpendingParameterAControlMaximum");
			NotifyPropertyChanged("InterimSpendingParameterAControlMinimum");
		}

		private void NotifyParameterBUpdates()
		{
			// Log("NotifyParameterUpdates");

			NotifyPropertyChanged("SpendingParameterB");
			NotifyPropertyChanged("SpendingParameterBMaximum");
			NotifyPropertyChanged("SpendingParameterBMinimum");
			NotifyPropertyChanged("SpendingParameterBControlMaximum");
			NotifyPropertyChanged("SpendingParameterBControlMinimum");
			NotifyPropertyChanged("SpendingParameterBIncrement");
			NotifyPropertyChanged("SpendingParameterBPrecision");
			NotifyPropertyChanged("SpendingParameterBMaximumString");
			NotifyPropertyChanged("SpendingParameterBMinimumString");

			NotifyPropertyChanged("TimingParameterBMaximum");
			NotifyPropertyChanged("TimingParameterBMinimum");
			NotifyPropertyChanged("TimingParameterB");

			NotifyPropertyChanged("InterimSpendingParameterB");
			NotifyPropertyChanged("InterimSpendingParameterBMaximum");
			NotifyPropertyChanged("InterimSpendingParameterBMinimum");
			NotifyPropertyChanged("InterimSpendingParameterBControlMaximum");
			NotifyPropertyChanged("InterimSpendingParameterBControlMinimum");
		}

		private void NotifyCoordinatesUpdate()
		{
			NotifyPropertyChanged("TimingParameterA");
			NotifyPropertyChanged("InterimSpendingParameterA");
			NotifyPropertyChanged("TimingParameterB");
			NotifyPropertyChanged("InterimSpendingParameterB");
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

					NotifyPropertyChanged("SpendingParameterAMaximum");
					NotifyPropertyChanged("SpendingParameterAMinimum");
					NotifyPropertyChanged("SpendingParameterA");

					NotifyPropertyChanged("InterimSpendingParameterAMaximum");
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
					NotifyPropertyChanged("InterimSpendingParameterAMaximum");
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
			get { return _alpha * 100; }

			set
			{
				if (value >= Beta) value = Beta - 0.1;
				value /= 100;
				_alpha = value;
				NotifyPropertyChanged("Alpha");
				NotifyPropertyChanged("InterimSpendingParameterAMaximum");
				NotifyPropertyChanged("InterimSpendingParameterA");
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
			get { return 100 - (100 * _beta); }

			set
			{
				if (value <= Alpha) value = Alpha + 0.1;
				value = (100 - value) / 100;
				_beta = value;
				NotifyPropertyChanged("Beta");
				NotifyPropertyChanged("InterimSpendingParameterAMaximum");
				NotifyPropertyChanged("InterimSpendingParameterA");
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

		#region Spending Function A parameters

		#region SpendingParameterA property

		private double _gammaLowerParameterA = -1.0;
		private double _gammaUpperParamterA = -8.0;
		private double _nuLowerParameterA = 0.3;
		private double _nuUpperParameterA = 0.75;
		private double _rhoLowerParameterA = 2.0;
		private double _rhoUpperParameterA = 4.0;

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
		public double SpendingParameterA
		{
			get
			{
				switch (CurrentSpendingFunctionFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending
							? _gammaUpperParamterA
							: _gammaLowerParameterA;

					case OneParameterFamily.Power:
						return SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending
							? _rhoUpperParameterA
							: _rhoLowerParameterA;

					case OneParameterFamily.Exponential:
						return SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending
							? _nuUpperParameterA
							: _nuLowerParameterA;
				}

				return 0.0;
			}

			set
			{
				if (SetSpendingParameterA(value))
				{
					double y = ComputeInterimSpendingParameterA();
					SetInterimSpendingParameterA(y);
					UpdateLine();
					NotifyParameterUpdates();
				}
			}
		}

		public bool SetSpendingParameterA(double value)
		{
			if (double.IsNaN(value))
			{
				return false;
			}

			if (value < SpendingParameterAControlMinimum)
			{
				value = SpendingParameterAControlMinimum;
			}

			if (value > SpendingParameterAControlMaximum)
			{
				value = SpendingParameterAControlMaximum;
			}

			switch (CurrentSpendingFunctionFamily)
			{
				case OneParameterFamily.HwangShihDeCani:
					if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
						_gammaUpperParamterA = value;
					else
						_gammaLowerParameterA = value;
					break;

				case OneParameterFamily.Power:
					if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
						_rhoUpperParameterA = value;
					else
						_rhoLowerParameterA = value;
					break;

				case OneParameterFamily.Exponential:
					if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
						_nuUpperParameterA = value;
					else
						_nuLowerParameterA = value;
					break;
			}

			return true;
		}

		public double SpendingParameterAMinimum
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

		public string SpendingParameterAMinimumString
		{
			get { return SpendingParameterAMinimum.ToString(string.Format("F{0}", SpendingParameterAPrecision)); }
		}

		public double SpendingParameterAControlMinimum
		{
			get
			{
				switch (CurrentSpendingFunctionFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return SpendingParameterAMinimum + 0.01;

					case OneParameterFamily.Power:
						return SpendingParameterAMinimum + 0.050;

					case OneParameterFamily.Exponential:
						return SpendingParameterAMinimum + 0.001;
				}

				return 0.0;
			}
		}

		public double SpendingParameterAMaximum
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

		public string SpendingParameterAMaximumString
		{
			get { return SpendingParameterAMaximum.ToString(string.Format("F{0}", SpendingParameterAPrecision)); }
		}

		public double SpendingParameterAControlMaximum
		{
			get
			{
				switch (CurrentSpendingFunctionFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return SpendingParameterAMaximum - 0.01;

					case OneParameterFamily.Power:
						return SpendingParameterAMaximum - 0.050;

					case OneParameterFamily.Exponential:
						return SpendingParameterAMaximum - 0.001;
				}

				return 1.0;
			}
		}

		public double SpendingParameterAIncrement
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

		public int SpendingParameterAPrecision
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

		#endregion

		#region TimingParameterA property

		private double _timingParameterA;

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
		public double TimingParameterA
		{
			get { return _timingParameterA; }

			set
			{
				if (SetTimingParameterA(value))
				{
					if (PlotUpdateMode == PlotUpdateMode.MoveLineWithPoint)
					{
						// will trigger update notifications
						SpendingParameterA = ComputeSpendingParameterA(SpendingParameterA);
					}
					else
					{
						double y = ComputeInterimSpendingParameterA();
						SetInterimSpendingParameterA(y);
						NotifyParameterUpdates();
					}
				}
			}
		}

		public bool SetTimingParameterA(double value)
		{
			if (double.IsNaN(value))
			{
				return false;
			}

			if (value < TimingParameterAControlMinimum)
			{
				value = TimingParameterAControlMinimum;
			}

			if (value > TimingParameterAControlMaximum)
			{
				value = TimingParameterAControlMaximum;
			}

			_timingParameterA = value;

			return true;
		}

		public double TimingParameterAMinimum
		{
			get { return 0.0; }
		}

		public double TimingParameterAControlMinimum
		{
			get { return 0.05; }
		}

		public double TimingParameterAMaximum
		{
			get { return 1.0; }
		}

		public double TimingParameterAControlMaximum
		{
			get { return 0.95; }
		}

		public double TimingParameterAIncrement
		{
			get { return 0.010; }
		}

		public int TimingParameterAPrecision
		{
			get { return 3; }
		}

		#endregion

		#region InterimSpendingParameterA property

		private double? _interimSpendingLowerBetaParameterA;
		private double? _interimSpendingLowerH0ParameterA;
		private double? _interimSpendingUpperParameterA;

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
		public double InterimSpendingParameterA
		{
			get
			{
				if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
				{
					if (!_interimSpendingUpperParameterA.HasValue) _interimSpendingUpperParameterA = InterimSpendingParameterAMaximum;
					return _interimSpendingUpperParameterA.Value;
				}

				if (LowerBoundsSpending == SpendingFunctionLowerBoundSpending.BetaSpending)
				{
					if (!_interimSpendingLowerBetaParameterA.HasValue) _interimSpendingLowerBetaParameterA = InterimSpendingParameterAMaximum;
					return _interimSpendingLowerBetaParameterA.Value;
				}

				// else
				if (!_interimSpendingLowerH0ParameterA.HasValue) _interimSpendingLowerH0ParameterA = InterimSpendingParameterAMaximum;
				return _interimSpendingLowerH0ParameterA.Value;
			}

			set
			{
				if (PlotUpdateMode == PlotUpdateMode.MoveLineWithPoint)
				{
					var spendingParameter = FittingSpendingFunction(InterimSpendingParameterAMaximum, value, TimingParameterA,
						SpendingParameterA);

					if (spendingParameter <= SpendingParameterAControlMaximum)
					{
						if (SetInterimSpendingParameterA(value))
						{
							// will trigger update notifications
							//SpendingParameterA = spendingParameter;
							SetSpendingParameterA(spendingParameter);
							UpdateLine();
						}
					}
				}
				else
				{
					double x = ComputeTimingParameterA();
					SetTimingParameterA(x);
				}

				NotifyParameterUpdates();
			}
		}

		public bool SetInterimSpendingParameterA(double value)
		{
			if (double.IsNaN(value))
			{
				return false;
			}

			if (value < InterimSpendingParameterAControlMinimum)
			{
				value = InterimSpendingParameterAControlMinimum;
			}

			if (value > InterimSpendingParameterAControlMaximum)
			{
				value = InterimSpendingParameterAControlMaximum;
			}

			if (CurrentSpendingFunctionFamily == OneParameterFamily.HwangShihDeCani)
			{
				var y = NormalSpendingFunction(InterimSpendingParameterAMaximum, TimingParameterA, SpendingParameterAMaximum);
				if (y < value)
				{
					value = y;
				}

				y = NormalSpendingFunction(InterimSpendingParameterAMaximum, TimingParameterA, SpendingParameterAMinimum);
				if (y > value)
				{
					value = y;
				}
			}
			else
			{
				var y = NormalSpendingFunction(InterimSpendingParameterAMaximum, TimingParameterA, SpendingParameterAMaximum);
				if (y > value)
				{
					value = y;
				}

				y = NormalSpendingFunction(InterimSpendingParameterAMaximum, TimingParameterA, SpendingParameterAMinimum);
				if (y < value)
				{
					value = y;
				}
			}

			if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
			{
				_interimSpendingUpperParameterA = value;
			}
			else if (LowerBoundsSpending == SpendingFunctionLowerBoundSpending.BetaSpending)
			{
				_interimSpendingLowerBetaParameterA = value;
			}
			else
			{
				_interimSpendingLowerH0ParameterA = value;
			}

			return true;
		}

		public double InterimSpendingParameterAIncrement
		{
			get { return 0.001; }
		}

		public double InterimSpendingParameterAPrecision
		{
			get { return 5; }
		}

		public double InterimSpendingParameterAMinimum
		{
			get { return 0.0; }
		}

		public double InterimSpendingParameterAControlMinimum
		{
			get
			{
				if (CurrentSpendingFunctionFamily == OneParameterFamily.HwangShihDeCani)
				{
					return NormalSpendingFunction(InterimSpendingParameterAMaximum, TimingParameterA, SpendingParameterAMinimum);
				}
				else
				{
					return InterimSpendingParameterAMinimum + 0.00001;
				}
			}
		}

		public double InterimSpendingParameterAMaximum
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

		public double InterimSpendingParameterAControlMaximum
		{
			get
			{
				if (CurrentSpendingFunctionFamily == OneParameterFamily.HwangShihDeCani)
				{
					return NormalSpendingFunction(InterimSpendingParameterAMaximum, TimingParameterA, SpendingParameterAMaximum);
				}
				else
				{
					return InterimSpendingParameterAMaximum; //  -0.00001;
				}
			}
		}

		#endregion

		#endregion

		#region Spending Function B parameters

		#region SpendingParameterB property

		private double _gammaLowerParameterB = -1.0;
		private double _gammaUpperParameterB = -8.0;
		private double _nuLowerParameterB = 0.3;
		private double _nuUpperParameterB = 0.75;
		private double _rhoLowerParameterB = 2.0;
		private double _rhoUpperParameterB = 4.0;

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
		public double SpendingParameterB
		{
			get
			{
				switch (CurrentSpendingFunctionFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending
							? _gammaUpperParameterB
							: _gammaLowerParameterB;

					case OneParameterFamily.Power:
						return SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending
							? _rhoUpperParameterB
							: _rhoLowerParameterB;

					case OneParameterFamily.Exponential:
						return SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending
							? _nuUpperParameterB
							: _nuLowerParameterB;
				}

				return 0.0;
			}

			set
			{
				if (SetSpendingParameterB(value))
				{
					double y = ComputeInterimSpendingParameterB();
					SetInterimSpendingParameterB(y);
					UpdateLine();
					NotifyParameterUpdates();
				}
			}
		}

		public bool SetSpendingParameterB(double value)
		{
			if (double.IsNaN(value))
			{
				return false;
			}

			if (value < SpendingParameterBControlMinimum)
			{
				value = SpendingParameterBControlMinimum;
			}

			if (value > SpendingParameterBControlMaximum)
			{
				value = SpendingParameterBControlMaximum;
			}

			switch (CurrentSpendingFunctionFamily)
			{
				case OneParameterFamily.HwangShihDeCani:
					if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
						_gammaUpperParameterB = value;
					else
						_gammaLowerParameterB = value;
					break;

				case OneParameterFamily.Power:
					if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
						_rhoUpperParameterB = value;
					else
						_rhoLowerParameterB = value;
					break;

				case OneParameterFamily.Exponential:
					if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
						_nuUpperParameterB = value;
					else
						_nuLowerParameterB = value;
					break;
			}

			return true;
		}

		public double SpendingParameterBMinimum
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

		public string SpendingParameterBMinimumString
		{
			get { return SpendingParameterBMinimum.ToString(string.Format("F{0}", SpendingParameterBPrecision)); }
		}

		public double SpendingParameterBControlMinimum
		{
			get
			{
				switch (CurrentSpendingFunctionFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return SpendingParameterBMinimum + 0.01;

					case OneParameterFamily.Power:
						return SpendingParameterBMinimum + 0.050;

					case OneParameterFamily.Exponential:
						return SpendingParameterBMinimum + 0.001;
				}

				return 0.0;
			}
		}

		public double SpendingParameterBMaximum
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

		public string SpendingParameterBMaximumString
		{
			get { return SpendingParameterBMaximum.ToString(string.Format("F{0}", SpendingParameterBPrecision)); }
		}

		public double SpendingParameterBControlMaximum
		{
			get
			{
				switch (CurrentSpendingFunctionFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return SpendingParameterBMaximum - 0.01;

					case OneParameterFamily.Power:
						return SpendingParameterBMaximum - 0.050;

					case OneParameterFamily.Exponential:
						return SpendingParameterBMaximum - 0.001;
				}

				return 1.0;
			}
		}

		public double SpendingParameterBIncrement
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

		public int SpendingParameterBPrecision
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

		#endregion

		#region TimingParameterB property

		private double _timingParameterParameterB;

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
		public double TimingParameterB
		{
			get { return _timingParameterParameterB; }

			set
			{
				if (SetTimingParameterB(value))
				{
					if (PlotUpdateMode == PlotUpdateMode.MoveLineWithPoint)
					{
						// will trigger update notifications
						SpendingParameterB = ComputeSpendingParameterB(SpendingParameterB);
					}
					else
					{
						double y = ComputeInterimSpendingParameterB();
						SetInterimSpendingParameterB(y);
						NotifyParameterUpdates();
					}
				}
			}
		}

		public bool SetTimingParameterB(double value)
		{
			if (double.IsNaN(value))
			{
				return false;
			}

			if (value < TimingParameterBControlMinimum)
			{
				value = TimingParameterBControlMinimum;
			}

			if (value > TimingParameterBControlMaximum)
			{
				value = TimingParameterBControlMaximum;
			}

			_timingParameterParameterB = value;

			return true;
		}

		public double TimingParameterBMinimum
		{
			get { return 0.0; }
		}

		public double TimingParameterBControlMinimum
		{
			get { return 0.05; }
		}

		public double TimingParameterBMaximum
		{
			get { return 1.0; }
		}

		public double TimingParameterBControlMaximum
		{
			get { return 0.95; }
		}

		public double TimingParameterBIncrement
		{
			get { return 0.010; }
		}

		public int TimingParameterBPrecision
		{
			get { return 3; }
		}

		#endregion

		#region InterimSpendingParameterB property

		private double? _interimSpendingLowerBetaParameterB;
		private double? _interimSpendingLowerH0ParameterB;
		private double? _interimSpendingUpperParameterB;

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
		public double InterimSpendingParameterB
		{
			get
			{
				if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
				{
					if (!_interimSpendingUpperParameterB.HasValue) _interimSpendingUpperParameterB = InterimSpendingParameterBMaximum;
					return _interimSpendingUpperParameterB.Value;
				}

				if (LowerBoundsSpending == SpendingFunctionLowerBoundSpending.BetaSpending)
				{
					if (!_interimSpendingLowerBetaParameterB.HasValue) _interimSpendingLowerBetaParameterB = InterimSpendingParameterBMaximum;
					return _interimSpendingLowerBetaParameterB.Value;
				}

				// else
				if (!_interimSpendingLowerH0ParameterB.HasValue) _interimSpendingLowerH0ParameterB = InterimSpendingParameterBMaximum;
				return _interimSpendingLowerH0ParameterB.Value;
			}

			set
			{
				if (PlotUpdateMode == PlotUpdateMode.MoveLineWithPoint)
				{
					var spendingParameter = FittingSpendingFunction(InterimSpendingParameterBMaximum, value, TimingParameterB,
						SpendingParameterB);

					if (spendingParameter <= SpendingParameterBControlMaximum)
					{
						if (SetInterimSpendingParameterB(value))
						{
							// will trigger update notifications
							//SpendingParameterB = spendingParameter;
							SetSpendingParameterB(spendingParameter);
							UpdateLine();
						}
					}
				}
				else
				{
					double x = ComputeTimingParameterB();
					SetTimingParameterB(x);
				}

				NotifyParameterUpdates();
			}
		}

		public bool SetInterimSpendingParameterB(double value)
		{
			if (double.IsNaN(value))
			{
				return false;
			}

			if (value < InterimSpendingParameterBControlMinimum)
			{
				value = InterimSpendingParameterBControlMinimum;
			}

			if (value > InterimSpendingParameterBControlMaximum)
			{
				value = InterimSpendingParameterBControlMaximum;
			}

			if (CurrentSpendingFunctionFamily == OneParameterFamily.HwangShihDeCani)
			{
				var y = NormalSpendingFunction(InterimSpendingParameterBMaximum, TimingParameterB, SpendingParameterBMaximum);
				if (y < value)
				{
					value = y;
				}

				y = NormalSpendingFunction(InterimSpendingParameterBMaximum, TimingParameterB, SpendingParameterBMinimum);
				if (y > value)
				{
					value = y;
				}
			}
			else
			{
				var y = NormalSpendingFunction(InterimSpendingParameterBMaximum, TimingParameterB, SpendingParameterBMaximum);
				if (y > value)
				{
					value = y;
				}

				y = NormalSpendingFunction(InterimSpendingParameterBMaximum, TimingParameterB, SpendingParameterBMinimum);
				if (y < value)
				{
					value = y;
				}
			}

			if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
			{
				_interimSpendingUpperParameterB = value;
			}
			else if (LowerBoundsSpending == SpendingFunctionLowerBoundSpending.BetaSpending)
			{
				_interimSpendingLowerBetaParameterB = value;
			}
			else
			{
				_interimSpendingLowerH0ParameterB = value;
			}

			return true;
		}

		public double InterimSpendingParameterBIncrement
		{
			get { return 0.001; }
		}

		public double InterimSpendingParameterBPrecision
		{
			get { return 5; }
		}

		public double InterimSpendingParameterBMinimum
		{
			get { return 0.0; }
		}

		public double InterimSpendingParameterBControlMinimum
		{
			get
			{
				if (CurrentSpendingFunctionFamily == OneParameterFamily.HwangShihDeCani)
				{
					return NormalSpendingFunction(InterimSpendingParameterBMaximum, TimingParameterB, SpendingParameterBMinimum);
				}
				else
				{
					return InterimSpendingParameterBMinimum + 0.00001;
				}
			}
		}

		public double InterimSpendingParameterBMaximum
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

		public double InterimSpendingParameterBControlMaximum
		{
			get
			{
				if (CurrentSpendingFunctionFamily == OneParameterFamily.HwangShihDeCani)
				{
					return NormalSpendingFunction(InterimSpendingParameterBMaximum, TimingParameterB, SpendingParameterBMaximum);
				}
				else
				{
					return InterimSpendingParameterBMaximum; //  -0.00001;
				}
			}
		}

		#endregion

		#endregion

		#endregion

		#region Methods

		/// <summary>
		/// Solve for y (interim spending)
		/// </summary>
		public NormalSpendingFunction NormalSpendingFunction
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
						return OneParameterSpendingFunctions.HwangShihDeCaniInverseFunction;
					case OneParameterFamily.Power:
						return OneParameterSpendingFunctions.PowerInverseFunction;
					case OneParameterFamily.Exponential:
						return OneParameterSpendingFunctions.ExponentialInverseFunction;
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
						return OneParameterSpendingFunctions.HwangShihDeCaniFittingFunction;
					case OneParameterFamily.Power:
						return OneParameterSpendingFunctions.PowerFittingFunction;
					case OneParameterFamily.Exponential:
						return OneParameterSpendingFunctions.ExponentialFittingFunction;
				}

				throw new Exception("Unsupported");
			}
		}

		/// <summary>
		/// Solve for y (interim spending)
		/// </summary>
		public double ComputeInterimSpendingParameterA()
		{
			double alpha = InterimSpendingParameterAControlMaximum;
			double x = TimingParameterA;
			double spendingParameter = SpendingParameterA;

			double y = NormalSpendingFunction(alpha, x, spendingParameter);
			return y;
		}

		/// <summary>
		/// Solve for y (interim spending)
		/// </summary>
		public double ComputeInterimSpendingParameterB()
		{
			double alpha = InterimSpendingParameterBControlMaximum;
			double x = TimingParameterB;
			double spendingParameter = SpendingParameterB;

			double y = NormalSpendingFunction(alpha, x, spendingParameter);
			return y;
		}

		/// <summary>
		/// Solve for x (timing)
		/// </summary>
		public double ComputeTimingParameterA()
		{
			double alpha = InterimSpendingParameterAControlMaximum;
			double y = InterimSpendingParameterA;
			double spendingParameter = SpendingParameterA;

			double x = InverseSpendingFunction(alpha, y, spendingParameter);
			return x;
		}

		/// <summary>
		/// Solve for x (timing)
		/// </summary>
		public double ComputeTimingParameterB()
		{
			double alpha = InterimSpendingParameterBControlMaximum;
			double y = InterimSpendingParameterB;
			double spendingParameter = SpendingParameterB;

			double x = InverseSpendingFunction(alpha, y, spendingParameter);
			return x;
		}

		/// <summary>
		/// Solve for spending parameter
		/// </summary>
		public double ComputeSpendingParameterA(double defaultSpendingParameterA)
		{
			double alpha = InterimSpendingParameterAControlMaximum;
			double y = InterimSpendingParameterA;
			double x = TimingParameterA;

			double spendingParameter = FittingSpendingFunction(alpha, y, x, defaultSpendingParameterA);
			return spendingParameter;
		}

		/// <summary>
		/// Solve for spending parameter
		/// </summary>
		public double ComputeSpendingParameterB(double defaultSpendingParameterB)
		{
			double alpha = InterimSpendingParameterBControlMaximum;
			double y = InterimSpendingParameterB;
			double x = TimingParameterB;

			double spendingParameter = FittingSpendingFunction(alpha, y, x, defaultSpendingParameterB);
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

			double alpha = InterimSpendingParameterAMaximum;
			double xMin = TimingParameterAMinimum;
			double xMax = TimingParameterAMaximum;

			Coordinates[0] = new Point(xMin, NormalSpendingFunction(alpha, xMin, SpendingParameterA));
			Coordinates[Coordinates.Count - 1] = new Point(xMax, NormalSpendingFunction(alpha, xMax, SpendingParameterA));

			// compute x-axis interval
			double increment = (xMax - xMin) / intervalCount;

			// compute y values for x values between min and max
			for (int i = 1; i < Coordinates.Count - 1; i++)
			{
				// timing = x
				double x = Coordinates[i - 1].X + increment;

				// y is a function of alpha, timing, & spending value
				double y = NormalSpendingFunction(alpha, x, SpendingParameterA);
				Coordinates[i] = new Point(x, y);
			}

			NotifyCoordinatesUpdate();
		}

		public void UpdateCoordinate(double timing, double interimSpending)
		{
			if (PlotUpdateMode == PlotUpdateMode.MoveLineWithPoint)
			{
				SetTimingParameterA(timing);

				SetInterimSpendingParameterA(interimSpending);

				// will trigger update notifications
				SpendingParameterA = ComputeSpendingParameterA(SpendingParameterA);
				// OR
				var spendingParameter = ComputeSpendingParameterA(SpendingParameterA);
				SetSpendingParameterA(spendingParameter);
				UpdateLine();
				NotifyParameterUpdates();
			}
			else
			{
				TimingParameterA = timing;
			}
		}

		#endregion

		#region CanDragHandleA property

		public CanDragTo CanDragHandleA
		{
			get
			{
				return (LinePlotBase linePlot, ref Point p) =>
				{
					return true;
				};
			}
		}

		#endregion

		#region CanDragHandleB property

		public CanDragTo CanDragHandleB
		{
			get
			{
				return (LinePlotBase linePlot, ref Point p) =>
				{
					return true;
				};
			}
		}

		#endregion


	}
}