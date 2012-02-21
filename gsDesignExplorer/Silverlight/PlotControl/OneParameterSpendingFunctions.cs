
namespace Subfuzion.Silverlight.UI.Charting
{
	using System;

	public static class OneParameterSpendingFunctions
	{
		#region Hwang-Shih-DeCani

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

			return -Math.Log(1 - y*(1 - Math.Exp(-sfValue))/alpha)/sfValue;
		}

		// 
		public static double HwangShihDeCaniFunctionSpendingParameter(double alpha, double y, double timing)
		{
			//			return RootFinding(HwangShihDeCaniFunction(alpha), -2, 1.5);

			return 0;
		}

		//double root = RootFinding(
		//    new FunctionOfOneVariable(f), // function to find root of, cast as delegate
		//    1.0,                          // left end of bracket
		//    5.0,                          // right end of bracket 
		//    1e-10,                        // tolerance 
		//    0.2,                          // target 
		//    out iterationsUsed,           // number of steps the algorithm used
		//    out errorEstimate             // estimate of the error in the result
		//);

		#endregion

		#region Power

		// y = alpha * (t^rho)
		public static double PowerFunction(double alpha, double timing, double sfValue)
		{
			try
			{
				return alpha * Math.Pow(timing, sfValue);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return 0.0;
		}

		// Inverse: t = exp( (ln(y) - ln(alpha)) / rho)
		public static double PowerFunctionInverse(double alpha, double y, double sfValue)
		{
			try
			{
				//if (sfValue < SpendingFunctionParameterMinimum)
				//{
				//    sfValue = SpendingFunctionParameterMinimum;
				//}

				return Math.Exp((Math.Log(y) - Math.Log(alpha)) / sfValue);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return 0.0;
		}

		// rho = (ln(y)-ln(alpha))/ln(t)
		public static double PowerFunctionSpendingParameter(double alpha, double y, double timing)
		{
			try
			{
				return (Math.Log(y) - Math.Log(alpha)) / Math.Log(timing);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return 0.0;
		}

		#endregion

		#region Exponential

		#endregion
	}

}
