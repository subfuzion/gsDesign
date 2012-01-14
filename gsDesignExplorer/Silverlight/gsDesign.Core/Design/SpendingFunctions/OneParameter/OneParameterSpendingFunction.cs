namespace gsDesign.Design.SpendingFunctions.OneParameter
{
	using System;

	public class OneParameterSpendingFunction
	{
		public OneParameterSpendingFunction(DesignParameters designParameters)
		{
			DesignParameters = designParameters;
		}

		protected DesignParameters DesignParameters { get; private set; }

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

		public double AlphaSpending
		{
			get { return DesignParameters.ErrorPowerTimingParameters.Alpha; }
			set { DesignParameters.ErrorPowerTimingParameters.Alpha = value; }
		}

		private double _timing = 0.5;
		public double Timing
		{
			get { return _timing; }
			set { _timing = value; }
		}

		public OneParameterFamily OneParameterFamily { get; set; }

		// Gamma
		private double _hwangShihDeCani = 4.0;
		public double HwangShihDeCani
		{
			get { return _hwangShihDeCani; }
			set { _hwangShihDeCani = value; }
		}

		// Rho
		private double _power = 0;
		public double Power
		{
			get { return _power; }
			set { _power = value; }
		}

		// Nu
		private double _exponential = 0;
		public double Exponential
		{
			get { return _exponential; }
			set { _exponential = value; }
		}
	}
}