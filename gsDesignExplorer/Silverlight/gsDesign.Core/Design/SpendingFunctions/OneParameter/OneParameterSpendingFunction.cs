namespace gsDesign.Design.SpendingFunctions.OneParameter
{
	using System;

	/// <summary>
	/// An instance of this class is a property of SpendingFunction (which is either the
	/// LowerSpendingFunction or UpperSpendingFunction property of SpendingFunctionParameters)
	/// </summary>
	public class OneParameterSpendingFunction : ParameterSpendingFunctionBase
	{
		public OneParameterSpendingFunction(DesignParameters designParameters, SpendingFunctionBounds bounds)
			: base(designParameters, bounds)
		{
		}

		public double SpendingFunctionValue
		{
			get
			{
				switch (OneParameterFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return HwangShihDeCani;

					case OneParameterFamily.Power:
						return Power;

					case OneParameterFamily.Exponential:
						return Exponential;

					default:
						throw new Exception(string.Format("Unsupported enum value for OneParameterFamily: {0}", OneParameterFamily));
				}
			}
		}

		#region InterimSpending property

		public double InterimSpending
		{
			get
			{
				if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
				{
					return AlphaInterimSpending;
				}

				// otherwise its LowerSpending
				switch (DesignParameters.SpendingFunctionParameters.SpendingFunctionTestingParameters.SpendingFunctionLowerBoundSpending)
				{
					case SpendingFunctionLowerBoundSpending.BetaSpending:
						return BetaInterimSpending;

					case SpendingFunctionLowerBoundSpending.H0Spending:
						return H0InterimSpending;
				}

				throw new Exception("Not a supported configuration for determining InterimSpending");
			}

			set
			{
				if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
				{
					AlphaInterimSpending = value;
					return;
				}

				// otherwise its LowerSpending
				switch (DesignParameters.SpendingFunctionParameters.SpendingFunctionTestingParameters.SpendingFunctionLowerBoundSpending)
				{
					case SpendingFunctionLowerBoundSpending.BetaSpending:
						BetaInterimSpending = value;
						return;

					case SpendingFunctionLowerBoundSpending.H0Spending:
						H0InterimSpending = value;
						return;
				}

				throw new Exception("Not a supported configuration for setting InterimSpending");
			}
		}

		public double InterimSpendingDefault
		{
			get
			{
				if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
				{
					return 0.025;
				}

				// otherwise its LowerSpending
				switch (DesignParameters.SpendingFunctionParameters.SpendingFunctionTestingParameters.SpendingFunctionLowerBoundSpending)
				{
					case SpendingFunctionLowerBoundSpending.BetaSpending:
						return 0.1;

					case SpendingFunctionLowerBoundSpending.H0Spending:
						return 0.975;
				}

				throw new Exception("Not a supported configuration for determining InterimSpendingDefault");
			}
		}

		public double InterimSpendingMinimum
		{
			get { return 0.0; }
		}

		public double InterimSpendingMaximum
		{
			get
			{
				if (SpendingFunctionBounds == SpendingFunctionBounds.UpperSpending)
				{
					return Alpha;
				}

				// otherwise its LowerSpending
				switch (DesignParameters.SpendingFunctionParameters.SpendingFunctionTestingParameters.SpendingFunctionLowerBoundSpending)
				{
					case SpendingFunctionLowerBoundSpending.BetaSpending:
						return Beta;

					case SpendingFunctionLowerBoundSpending.H0Spending:
						return 1 - Alpha;
				}

				throw new Exception("Not a supported configuration for determining InterimSpendingMaximum");
			}
		}

		public double InterimSpendingIncrement
		{
			get { return 0.05; }
		}

		public int InterimSpendingPrecision
		{
			get { return 5; }
		}

		#endregion // InterimSpending

		public double Alpha
		{
			get { return DesignParameters.ErrorPowerTimingParameters.Alpha; }
			set { DesignParameters.ErrorPowerTimingParameters.Alpha = value; }
		}

		public double Beta
		{
			get { return DesignParameters.ErrorPowerTimingParameters.Beta; }
			set { DesignParameters.ErrorPowerTimingParameters.Beta = value; }
		}

		private double _timing = 0.5;
		public double Timing
		{
			get { return _timing; }
			set { _timing = value; }
		}

		public OneParameterFamily OneParameterFamily { get; set; }

		// Gamma
		private double _hwangShihDeCani = -8.0;
		public double HwangShihDeCani
		{
			get { return _hwangShihDeCani; }
			set { _hwangShihDeCani = value; }
		}

		// Rho
		private double _power = 4.0;
		public double Power
		{
			get { return _power; }
			set { _power = value; }
		}

		// Nu
		private double _exponential = 0.75;
		public double Exponential
		{
			get { return _exponential; }
			set { _exponential = value; }
		}

		private double _alphaInterimSpending = 0.025;
		public double AlphaInterimSpending
		{
			get { return _alphaInterimSpending; }
			set { _alphaInterimSpending = value; }
		}

		private double _betaInterimSpending = 0.1;
		public double BetaInterimSpending
		{
			get { return _betaInterimSpending; }
			set { _betaInterimSpending = value; }
		}

		private double _h0InterimSpending = 0.975;
		public double H0InterimSpending
		{
			get { return _h0InterimSpending; }
			set { _h0InterimSpending = value; }
		}
	}
}