namespace Subfuzion.Silverlight.UI.Charting
{
	using System;
	using System.Collections.ObjectModel;
	using System.Windows;

	public delegate double SpendingFunction(double alpha, double timing, double sfValue);

	public delegate double InverseSpendingFunction(double alpha, double y, double sfValue);

	public delegate double ParameterSpendingFunction(double alpha, double y, double sfValue);

	public class OneParameterSpendingFunction : PlotFunction
	{
		public OneParameterSpendingFunction()
		{
			PlotUpdateMode = PlotUpdateMode.MoveLineWithPoint;

			var coordinates = new ObservableCollection<Point>();
			for (int i = 0; i < 30; i++)
			{
				coordinates.Add(new Point(0, 0));
			}

			Coordinates = coordinates;
		}

		public override void Update()
		{
			var incrementCount = Coordinates.Count;
			var intervalCount = incrementCount - 1;

			var alpha = InterimSpendingParameterMaximum; // InterimSpendingParameter;
			var xMin = TimingMinimum;
			var xMax = TimingMaximum;

			Coordinates[0] = new Point(xMin, SpendingFunction(alpha, xMin, SpendingFunctionParameter));
			Coordinates[Coordinates.Count - 1] = new Point(xMax, SpendingFunction(alpha, xMax, SpendingFunctionParameter));

			// compute x-axis interval
			var increment = (xMax - xMin) / intervalCount;

			// compute y values for x values between min and max
			for (var i = 1; i < Coordinates.Count - 1; i++)
			{
				// timing = x
				var x = Coordinates[i - 1].X + increment;

				// y is a function of alpha, timing, & spending value
				var y = SpendingFunction(alpha, x, SpendingFunctionParameter);
				Coordinates[i] = new Point(x, y);
			}

			base.Update();
		}

		public void Update(double x, double y)
		{
			_timing = x;
			_interimSpendingParameter = y;

			NotifyPropertyChanged("Timing");
			NotifyPropertyChanged("InterimSpendingParameter");

			//Update();
		}

		public SpendingFunction SpendingFunction { get; set; }

		public InverseSpendingFunction InverseSpendingFunction { get; set; }

		public ParameterSpendingFunction ParameterSpendingFunction { get; set; }

		#region PlotConstraint property

		private PlotUpdateMode _plotUpdateMode = PlotUpdateMode.MovePointAlongLine;

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
					_plotUpdateMode = value;
					NotifyPropertyChanged("PlotUpdateMode");
				}
			}
		}

		#endregion


		#region SpendingFunctionParameter property

		private double _spendingFunctionParameter;

		/// <summary>
		/// Gets or sets the SpendingFunctionValue property.
		/// </summary>
		public double SpendingFunctionParameter
		{
			get { return _spendingFunctionParameter; }

			set
			{
				//if (value < SpendingFunctionParameterMinimum) value = SpendingFunctionParameterMinimum;
				//if (value > SpendingFunctionParameterMaximum) value = SpendingFunctionParameterMaximum;

				if (Math.Abs(_spendingFunctionParameter - value) > double.Epsilon)
				{
					try
					{
						_spendingFunctionParameter = value;
						NotifyPropertyChanged("SpendingFunctionParameter");

						var x = Timing;
						var alpha = InterimSpendingParameterMaximum;
						var y = SpendingFunction(alpha, x, _spendingFunctionParameter);

						// InterimSpendingParameter = y;
						_interimSpendingParameter = y;
						NotifyPropertyChanged("InterimSpendingParameter");
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
				}
			}
		}

		#region SpendingFunctionParameterMinimum property

		private double _spendingFunctionParameterMinimum;

		/// <summary>
		/// Gets or sets the SpendingFunctionParameterMinimum property.
		/// </summary>
		public double SpendingFunctionParameterMinimum
		{
			get { return _spendingFunctionParameterMinimum; }

			set
			{
				if (Math.Abs(_spendingFunctionParameterMinimum - value) > double.Epsilon)
				{
					_spendingFunctionParameterMinimum = value;
					NotifyPropertyChanged("SpendingFunctionParameterMinimum");
				}
			}
		}

		#endregion

		#region SpendingFunctionParameterMaximum property

		private double _spendingFunctionParameterMaximum;

		/// <summary>
		/// Gets or sets the SpendingFunctionParameterMaximum property.
		/// </summary>
		public double SpendingFunctionParameterMaximum
		{
			get { return _spendingFunctionParameterMaximum; }

			set
			{
				if (Math.Abs(_spendingFunctionParameterMaximum - value) > double.Epsilon)
				{
					_spendingFunctionParameterMaximum = value;
					NotifyPropertyChanged("SpendingFunctionParameterMaximum");
				}
			}
		}

		#endregion

		#endregion

		#region InterimSpendingParameter property

		private double _interimSpendingParameter;

		/// <summary>
		/// Gets or sets the InterimSpendingParameter property.
		/// </summary>
		public double InterimSpendingParameter
		{
			get { return _interimSpendingParameter; }

			set
			{
				//if (value < InterimSpendingParameterMinimum) value = InterimSpendingParameterMinimum;
				//if (value > InterimSpendingParameterMaximum) value = InterimSpendingParameterMaximum;

				if (Math.Abs(_interimSpendingParameter - value) > double.Epsilon)
				{
					_interimSpendingParameter = value;
					NotifyPropertyChanged("InterimSpendingParameter");

					if (PlotUpdateMode == PlotUpdateMode.MovePointAlongLine)
					{
						var y = InterimSpendingParameter;
						var alpha = InterimSpendingParameterMaximum;

						var x = InverseSpendingFunction(alpha, y, SpendingFunctionParameter);

						Timing = x;
					}
					else // PlotConstraint.MoveLineWithPoint
					{
						// compute rho
						var rho = ParameterSpendingFunction(InterimSpendingParameterMaximum, InterimSpendingParameter, Timing);
						SpendingFunctionParameter = rho;

						// update coordinates
						Update();
					}
				}
			}
		}

		#region InterimSpendingParameterMinimum property

		private double _interimSpendingParameterMinimum;

		/// <summary>
		/// Gets or sets the InterimSpendingParameterMinimum property.
		/// </summary>
		public double InterimSpendingParameterMinimum
		{
			get { return _interimSpendingParameterMinimum; }

			set
			{
				if (Math.Abs(_interimSpendingParameterMinimum - value) > double.Epsilon)
				{
					_interimSpendingParameterMinimum = value;
					NotifyPropertyChanged("InterimSpendingParameterMinimum");
				}
			}
		}

		#endregion

		#region InterimSpendingParameterMaximum property

		private double _interimSpendingParameterMaximum;

		/// <summary>
		/// Gets or sets the AlphaParameterMaximum property.
		/// </summary>
		public double InterimSpendingParameterMaximum
		{
			get { return _interimSpendingParameterMaximum; }

			set
			{
				if (Math.Abs(_interimSpendingParameterMaximum - value) > double.Epsilon)
				{
					_interimSpendingParameterMaximum = value;
					NotifyPropertyChanged("InterimSpendingParameterMaximum");
				}
			}
		}

		#endregion

		#endregion

		#region Timing property

		private double _timing;

		/// <summary>
		/// Gets or sets the Timing property.
		/// </summary>
		public double Timing
		{
			get { return _timing; }

			set
			{
				//if (value < TimingMinimum) value = TimingMinimum;
				//if (value > TimingMaximum) value = TimingMaximum;

				if (Math.Abs(_timing - value) > double.Epsilon)
				{
					_timing = value;
					NotifyPropertyChanged("Timing");

					if (PlotUpdateMode == PlotUpdateMode.MovePointAlongLine)
					{
						var x = Timing;
						var alpha = InterimSpendingParameterMaximum;

						var y = SpendingFunction(alpha, x, SpendingFunctionParameter);

						InterimSpendingParameter = y;
					}
					else // PlotConstraint.MoveLineWithPoint
					{
						// compute rho
						var rho = ParameterSpendingFunction(InterimSpendingParameterMaximum, InterimSpendingParameter, Timing);
						SpendingFunctionParameter = rho;

						// update coordinates
						Update();
					}
				}
			}
		}

		#region TimingMinimum property

		private double _timingMinimum;

		/// <summary>
		/// Gets or sets the TimingMinimum property.
		/// </summary>
		public double TimingMinimum
		{
			get { return _timingMinimum; }

			set
			{
				if (Math.Abs(_timingMinimum - value) > double.Epsilon)
				{
					_timingMinimum = value;
					NotifyPropertyChanged("TimingMinimum");
				}
			}
		}

		#endregion

		#region TimingMaximum property

		private double _timingMaximum;

		/// <summary>
		/// Gets or sets the TimingMaximum property.
		/// </summary>
		public double TimingMaximum
		{
			get { return _timingMaximum; }

			set
			{
				if (Math.Abs(_timingMaximum - value) > double.Epsilon)
				{
					_timingMaximum = value;
					NotifyPropertyChanged("TimingMaximum");
				}
			}
		}

		#endregion

		#endregion
	}
}