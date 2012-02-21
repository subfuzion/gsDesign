namespace Subfuzion.Silverlight.UI.Charting
{
	using System;

	public delegate double FunctionOfTwoVariables(double x, double[] y);

	// John D. Cook
	// http://www.codeproject.com/Articles/79541/Three-Methods-for-Root-finding-in-C
	internal class RootFinding
	{
		private const int maxIterations = 50;

		public static double Brent
			(
			FunctionOfTwoVariables f,
			double left,
			double right,
			double tolerance = 1e-6,
			double target = 0.0,
			double[] y = null
			)
		{
			// extra info that callers may not always want
			int iterationsUsed;
			double errorEstimate;

			return Brent(f, left, right, tolerance, target, out iterationsUsed, out errorEstimate, y);
		}

		public static double Brent
			(FunctionOfTwoVariables g, double left, double right, double tolerance, double target, out int iterationsUsed,
				out double errorEstimate, double[] y)
		{
			if (tolerance <= 0.0)
			{
				string msg = string.Format("Tolerance must be positive. Recieved {0}.", tolerance);
				throw new ArgumentOutOfRangeException(msg);
			}

			errorEstimate = double.MaxValue;

			// Standardize the problem.  To solve g(x) = target,
			// solve f(x) = 0 where f(x) = g(x) - target.
			FunctionOfTwoVariables f = delegate(double x, double[] y2) { return g(x, y2) - target; };

			// Implementation and notation based on Chapter 4 in
			// "Algorithms for Minimization without Derivatives"
			// by Richard Brent.

			double c, d, e, fa, fb, fc, tol, m, p, q, r, s;

			// set up aliases to match Brent's notation
			double a = left;
			double b = right;
			double t = tolerance;
			iterationsUsed = 0;

			fa = f(a, y);
			fb = f(b, y);

			if (fa * fb > 0.0)
			{
				string str = "Invalid starting bracket. Function must be above target on one end and below target on other end.";
				string msg = string.Format("{0} Target: {1}. f(left) = {2}. f(right) = {3}", str, target, fa + target, fb + target);
				throw new ArgumentException(msg);
			}

		label_int:
			c = a;
			fc = fa;
			d = e = b - a;
		label_ext:
			if (Math.Abs(fc) < Math.Abs(fb))
			{
				a = b;
				b = c;
				c = a;
				fa = fb;
				fb = fc;
				fc = fa;
			}

			iterationsUsed++;

			double machine_epsilon = Math.Pow(2.0, -53);
			tol = 2.0 * machine_epsilon * Math.Abs(b) + t;

			errorEstimate = m = 0.5 * (c - b);
			if (Math.Abs(m) > tol && fb != 0.0) // exact comparison with 0 is OK here
			{
				// See if bisection is forced
				if (Math.Abs(e) < tol || Math.Abs(fa) <= Math.Abs(fb))
				{
					d = e = m;
				}
				else
				{
					s = fb / fa;
					if (a == c)
					{
						// linear interpolation
						p = 2.0 * m * s;
						q = 1.0 - s;
					}
					else
					{
						// Inverse quadratic interpolation
						q = fa / fc;
						r = fb / fc;
						p = s * (2.0 * m * q * (q - r) - (b - a) * (r - 1.0));
						q = (q - 1.0) * (r - 1.0) * (s - 1.0);
					}
					if (p > 0.0)
						q = -q;
					else
						p = -p;
					s = e;
					e = d;
					if (2.0 * p < 3.0 * m * q - Math.Abs(tol * q) && p < Math.Abs(0.5 * s * q))
						d = p / q;
					else
						d = e = m;
				}
				a = b;
				fa = fb;
				if (Math.Abs(d) > tol)
					b += d;
				else if (m > 0.0)
					b += tol;
				else
					b -= tol;
				if (iterationsUsed == maxIterations)
					return b;

				fb = f(b, y);
				if ((fb > 0.0 && fc > 0.0) || (fb <= 0.0 && fc <= 0.0))
					goto label_int;
				else
					goto label_ext;
			}
			else
				return b;
		}
	}
}