
namespace Subfuzion.Silverlight.UI.Charting.Models
{
	using System;

	public static class OneParameterSpendingFunctions
	{
		#region Hwang-Shih-DeCani

		public static double HwangShihDeCaniFunction(double alpha, double timing, double sfValue)
		{
			try
			{
				if (Math.Abs(sfValue - 0) < double.Epsilon)
				{
					return alpha*timing;
				}

				return alpha*(1 - Math.Exp(-sfValue*timing))/(1 - Math.Exp(-sfValue));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return 0.0;
		}

		// t = -log(1 - y * (1 - exp(-gamma)) / alpha) / gamma
		public static double HwangShihDeCaniFunctionInverse(double alpha, double y, double sfValue)
		{
			try
			{
				//if (sfValue == 0)
				//{
				//    return 
				//}

				return -Math.Log(1 - y*(1 - Math.Exp(-sfValue))/alpha)/sfValue;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return 0.0;
		}

		// 
		public static double HwangShihDeCaniFunctionSpendingParameter(double alpha, double y, double timing)
		{
			try
			{
				//if (Math.Abs(timing - 0) < double.Epsilon) timing = 0.5;
				//if (Math.Abs(y - 0) < double.Epsilon) y = 0.001;
				//if (Math.Abs(y - 0.025) < double.Epsilon) y = 0.001;


				if (timing < 0.001) timing = 0.001;
				if (y < 0.00001) y = 0.00001;


				var par = new[] { alpha, timing };

				const double tolerance = 1e-7;

				var target = y;

				return RootFinding.Brent(RootFindingHSD, -40, 40, tolerance, target, par);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return y;
			}

			return 0.0;
		}

		private static double RootFindingHSD(double x, double[] y)
		{
			double alpha = y[0];
			double timing = y[1];
			double sfValue = x;

			return HwangShihDeCaniFunction(alpha, timing, sfValue);
		}

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

		// y=alpha^(t^(-nu)) 
		public static double ExponentialFunction(double alpha, double timing, double sfValue)
		{
			try
			{
				return Math.Pow(alpha, Math.Pow(timing, -sfValue));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return 0.0;
		}

		// Inverse: t=exp((-ln(-ln y) - ln(-ln(alpha))) / nu) 
		public static double ExponentialFunctionInverse(double alpha, double y, double sfValue)
		{
			try
			{
				//if (sfValue < SpendingFunctionParameterMinimum)
				//{
				//    sfValue = SpendingFunctionParameterMinimum;
				//}

				return Math.Exp((-Math.Log(-Math.Log(y)) - Math.Log(-Math.Log(alpha))) / sfValue);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return 0.0;
		}

		// nu = (ln(-ln(alpha))-ln(-ln(y)))/ln(t)
		public static double ExponentialFunctionSpendingParameter(double alpha, double y, double timing)
		{
			try
			{
				return (Math.Log(-Math.Log(alpha)) - Math.Log(-Math.Log(y))) / Math.Log(timing);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return 0.0;
		}

		#endregion
	}

}
