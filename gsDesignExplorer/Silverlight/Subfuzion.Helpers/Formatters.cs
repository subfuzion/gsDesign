namespace Subfuzion.Helpers
{
	using System;
	using System.Text;
	using System.Threading;

	public static class Formatters
	{
		// http://stackoverflow.com/questions/1546113/double-to-string-conversion-without-scientific-notation
		// - Paul Sasik
		public static string ToLongString(this double input)
		{
			string str = input.ToString().ToUpper();

			// if string representation was collapsed from scientific notation, just return it:
			if (!str.Contains("E")) return str;

			string sep = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
			char decSeparator = sep.ToCharArray()[0];

			string[] exponentParts = str.Split('E');
			string[] decimalParts = exponentParts[0].Split(decSeparator);

			// fix missing decimal point:
			if (decimalParts.Length == 1) decimalParts = new string[] { exponentParts[0], "0" };

			int exponentValue = int.Parse(exponentParts[1]);

			string newNumber = decimalParts[0] + decimalParts[1];

			string result;

			if (exponentValue > 0)
			{
				result =
					newNumber +
					GetZeros(exponentValue - decimalParts[1].Length);
			}
			else // negative exponent
			{
				result =
					"0" +
					decSeparator +
					GetZeros(exponentValue + decimalParts[0].Length) +
					newNumber;

				result = result.TrimEnd('0');
			}

			return result;
		}

		private static string GetZeros(int zeroCount)
		{
			if (zeroCount < 0)
				zeroCount = Math.Abs(zeroCount);

			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < zeroCount; i++) sb.Append("0");

			return sb.ToString();
		}
	}
}
