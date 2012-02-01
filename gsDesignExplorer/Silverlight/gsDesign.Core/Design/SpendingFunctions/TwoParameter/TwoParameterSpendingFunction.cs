namespace gsDesign.Design.SpendingFunctions.TwoParameter
{
	using System;

	/// <summary>
	/// An instance of this class is a property of SpendingFunction (which is either the
	/// LowerSpendingFunction or UpperSpendingFunction property of SpendingFunctionParameters)
	/// </summary>
	public class TwoParameterSpendingFunction : ParameterSpendingFunctionBase
	{
		public TwoParameterSpendingFunction(DesignParameters designParameters, SpendingFunctionBounds bounds) : base(designParameters, bounds)
		{
		}

		#region TwoParameterFamily property

		private TwoParameterFamily _twoParameterFamily;

		public TwoParameterFamily TwoParameterFamily
		{
			get { return _twoParameterFamily; }

			set
			{
				_twoParameterFamily = value;
			}
		}

		#endregion // TwoParameterFamily


		private Func<double, double, double, double> PlotFunction
		{
			get
			{
				switch (TwoParameterFamily)
				{
					case TwoParameterFamily.Cauchy:
						return Cauchy;

					case TwoParameterFamily.ExtremeValue:
						return Cauchy;

					case TwoParameterFamily.ExtremeValue2:
						return Cauchy;

					case TwoParameterFamily.Logistic:
						return Cauchy;

					case TwoParameterFamily.Normal:
						return Cauchy;


					default:
						throw new Exception(string.Format("Unsupported enum value for TwoParameterFamily: {0}", TwoParameterFamily));
				}
			}
		}

		private double Cauchy(double alpha, double timing, double sfValue)
		{
			return alpha * (1 - Math.Exp(-sfValue * timing)) / (1 - Math.Exp(-sfValue));
		}

		private double ExtremeValue(double alpha, double timing, double sfValue)
		{
			return alpha * Math.Pow(timing, sfValue);
		}

		private double ExtremeValue2(double alpha, double timing, double sfValue)
		{
			return Math.Pow(alpha, (Math.Pow(timing, -sfValue)));
		}

		private double Logistic(double alpha, double timing, double sfValue)
		{
			return Math.Pow(alpha, (Math.Pow(timing, -sfValue)));
		}

		private double Normal(double alpha, double timing, double sfValue)
		{
			return Math.Pow(alpha, (Math.Pow(timing, -sfValue)));
		}
	}
}