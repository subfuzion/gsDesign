namespace gsDesign.Explorer.Controls
{
	using System.Threading;
	using System.Windows.Controls;

	public class PercentUpDown : NumericUpDown
	{
		protected override string FormatValue()
		{
			string value;

			switch (DecimalPlaces)
			{
				case 1:
					value = string.Format("{0:F1}", Value);
					break;

				case 2:
					value = string.Format("{0:F2}", Value);
					break;

				case 3:
					value = string.Format("{0:F3}", Value);
					break;

				case 4:
					value = string.Format("{0:F4}", Value);
					break;

				default:
					value = string.Format("{0}", Value);
					break;
			}


			return string.Format("{0} {1}",
				value,
				Thread.CurrentThread.CurrentCulture.NumberFormat.PercentSymbol);
		}

		protected override double ParseValue(string value)
		{
			return base.ParseValue(
				value.Replace(" " + Thread.CurrentThread.CurrentCulture.NumberFormat.PercentSymbol, string.Empty));
		}
	}
}