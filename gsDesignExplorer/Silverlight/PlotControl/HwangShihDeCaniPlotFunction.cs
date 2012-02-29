namespace Subfuzion.Silverlight.UI.Charting
{
	using System;
	using System.Collections.ObjectModel;
	using System.Windows;

	public class HwangShihDeCaniPlotFunction : PlotFunction
	{
		public static double HwangShihDeCaniFunction(double alpha, double timing, double sfValue)
		{
			if (Math.Abs(sfValue - 0) < double.Epsilon)
			{
				return alpha*timing;
			}

			return alpha*(1 - Math.Exp(-sfValue*timing))/(1 - Math.Exp(-sfValue));
		}

		// t = -log(1 - y * (1 - exp(-gamma)) / alpha) / gamma
		public static double HwangShihDeCaniFunctionInverse(double alpha, double y, double sfValue)
		{
			//if (sfValue == 0)
			//{
			//    return 
			//}

			return -Math.Log(1 - y*(1 - Math.Exp(-sfValue)) / alpha) / sfValue;
		}

		// 
		public static double HwangShihDeCaniFunctionSpendingParameter(double alpha, double y, double timing)
		{
			if (Math.Abs(timing - 0) < double.Epsilon) timing = 0.5;
			if (Math.Abs(y - 0) < double.Epsilon) y = 0.001;
			if (Math.Abs(y - 0.025) < double.Epsilon) y = 0.001;



			var par = new[] { alpha, timing };

			const double tolerance = 1e-7;

			var target = y;

			return RootFinding.Brent(RootFindingHSD, -40, 40, tolerance, target, par);
		}

		private static double RootFindingHSD(double x, double[] y)
		{
			double alpha = y[0];
			double timing = y[1];
			double sfValue = x;

			return HwangShihDeCaniFunction(alpha, timing, sfValue);
		}

		public HwangShihDeCaniPlotFunction()
		{
			PlotUpdateMode = PlotUpdateMode.MoveLineWithPoint;

			var coordinates = new ObservableCollection<Point>();
			for (int i = 0; i < 30; i++)
			{
				coordinates.Add(new Point(0,0));
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

			Coordinates[0] = new Point(xMin, HwangShihDeCaniFunction(alpha, xMin, SpendingFunctionParameter));
			Coordinates[Coordinates.Count - 1] = new Point(xMax, HwangShihDeCaniFunction(alpha, xMax, SpendingFunctionParameter));

			// compute x-axis interval
			var increment = (xMax - xMin) / intervalCount;

			// compute y values for x values between min and max
			for (var i = 1; i < Coordinates.Count - 1; i++)
			{
				// timing = x
				var x = Coordinates[i - 1].X + increment;

				// y is a function of alpha, timing, & spending value
				var y = HwangShihDeCaniFunction(alpha, x, SpendingFunctionParameter);
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

		#region PlotConstraint property

		private PlotUpdateMode _plotUpdateMode;

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
					NotifyPropertyChanged("PlotConstraint");
				}
			}
		}

		#endregion


		#region SpendingFunctionParameter property

		private double _spendingFunctionParameter = -8.0;

		/// <summary>
		/// Gets or sets the SpendingFunctionValue property.
		/// </summary>
		public double SpendingFunctionParameter
		{
			get { return _spendingFunctionParameter; }

			set
			{
				if (value < SpendingFunctionParameterMinimum) value = SpendingFunctionParameterMinimum;
				if (value > SpendingFunctionParameterMaximum) value = SpendingFunctionParameterMaximum;

				if (Math.Abs(_spendingFunctionParameter - value) > double.Epsilon)
				{
					try
					{
						_spendingFunctionParameter = value;

						var x = Timing;
						var alpha = InterimSpendingParameterMaximum;
						var y = HwangShihDeCaniFunction(alpha, x, _spendingFunctionParameter);

						InterimSpendingParameter = y;

						NotifyPropertyChanged("SpendingFunctionParameter");
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
				}
			}
		}

		#region SpendingFunctionParameterMinimum property

		private double _spendingFunctionParameterMinimum = 0.001;

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

		private double _spendingFunctionParameterMaximum = 10.0;

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

		#region Timing property

		private double _timing = 0.5;

		/// <summary>
		/// Gets or sets the Timing property.
		/// </summary>
		public double Timing
		{
			get { return _timing; }

			set
			{
				if (value < TimingMinimum) value = TimingMinimum;
				if (value > TimingMaximum) value = TimingMaximum;

				if (Math.Abs(_timing - value) > double.Epsilon)
				{
					_timing = value;
					NotifyPropertyChanged("Timing");

					if (PlotUpdateMode == PlotUpdateMode.MovePointAlongLine)
					{
						var x = Timing;
						var alpha = InterimSpendingParameterMaximum;

						var y = HwangShihDeCaniFunction(alpha, x, SpendingFunctionParameter);

						InterimSpendingParameter = y;
					}
					else // PlotConstraint.MoveLineWithPoint
					{
						// compute rho
						var rho = HwangShihDeCaniFunctionSpendingParameter(InterimSpendingParameterMaximum, InterimSpendingParameter, Timing);
						SpendingFunctionParameter = rho;

						// update coordinates
						Update();
					}
				}
			}
		}

		#region TimingMinimum property

		private double _timingMinimum = 0.0;

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

		private double _timingMaximum = 1.0;

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

		#region InterimSpendingParameter property

		private double _interimSpendingParameter = 0.024;

		/// <summary>
		/// Gets or sets the InterimSpendingParameter property.
		/// </summary>
		public double InterimSpendingParameter
		{
			get { return _interimSpendingParameter; }

			set
			{
				if (value < InterimSpendingParameterMinimum) value = InterimSpendingParameterMinimum;
				if (value > InterimSpendingParameterMaximum) value = InterimSpendingParameterMaximum;

				if (Math.Abs(_interimSpendingParameter - value) > double.Epsilon)
				{
					_interimSpendingParameter = value;
					NotifyPropertyChanged("InterimSpendingParameter");

					if (PlotUpdateMode == PlotUpdateMode.MovePointAlongLine)
					{
						var y = InterimSpendingParameter;
						var alpha = InterimSpendingParameterMaximum;

						var x = HwangShihDeCaniFunctionInverse(alpha, y, SpendingFunctionParameter);

						Timing = x;
					}
					else // PlotConstraint.MoveLineWithPoint
					{
						// compute rho
						var rho = HwangShihDeCaniFunctionSpendingParameter(InterimSpendingParameterMaximum, InterimSpendingParameter, Timing);
						SpendingFunctionParameter = rho;

						// update coordinates
						Update();
					}
				}
			}
		}

		#region InterimSpendingParameterMinimum property

		private double _interimSpendingParameterMinimum = 0.1;

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

		private double _interimSpendingParameterMaximum = 0.025;

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
	}
}