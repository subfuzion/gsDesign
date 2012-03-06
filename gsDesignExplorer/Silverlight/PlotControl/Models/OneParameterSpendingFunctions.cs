
namespace Subfuzion.Silverlight.UI.Charting.Models
{
	using System;

	/// <summary>
	/// Solve for y
	/// </summary>
	/// <param name="yMax"></param>
	/// <param name="x">timing</param>
	/// <param name="spendingParameter"></param>
	/// <returns></returns>
	public delegate double NormalSpendingFunction(double yMax, double x, double spendingParameter);

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
	/// <param name="defaultSpendingParameter">Expected value to return if an error occurs</param>
	/// <returns></returns>
	public delegate double FittingSpendingFunction(double yMax, double y, double x, double defaultSpendingParameter);

	public static class OneParameterSpendingFunctions
	{
		#region Hwang-Shih-DeCani

		// y = alpha * (1-exp(-gamma * t)) / (1 - exp(-gamma)) 
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
		public static double HwangShihDeCaniInverseFunction(double alpha, double y, double sfValue)
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

		// uses a variation of John Cook's root finding function
		public static double HwangShihDeCaniFittingFunction(double alpha, double y, double timing, double defaultSpendingParameter)
		{
			try
			{
				// Passing valid values is now the responsibility of the caller
				// (want only one place to specify valid values (in case we update them)
				// and the caller currently has exhaustive validation checks. If we update
				// (or suppress) these validation checks, don't want to have to update range
				// values in multiple locations)
				// if (timing < 0.001) timing = 0.001;
				// if (y < 0.00001) y = 0.00001;

				var par = new[] { alpha, timing };

				const double tolerance = 1e-7;

				var target = y;

				return RootFinding.Brent(RootFindingHSD, -40, 40, tolerance, target, par);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return defaultSpendingParameter;
			}
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
		public static double PowerInverseFunction(double alpha, double y, double sfValue)
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
		public static double PowerFittingFunction(double alpha, double y, double timing, double defaultSpendingParameter)
		{
			try
			{
				return (Math.Log(y) - Math.Log(alpha)) / Math.Log(timing);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return defaultSpendingParameter;
			}
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
		public static double ExponentialInverseFunction(double alpha, double y, double sfValue)
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
		public static double ExponentialFittingFunction(double alpha, double y, double timing, double defaultSpendingParameter)
		{
			try
			{
				return (Math.Log(-Math.Log(alpha)) - Math.Log(-Math.Log(y))) / Math.Log(timing);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return defaultSpendingParameter;
			}
		}

		#endregion
	}

}
