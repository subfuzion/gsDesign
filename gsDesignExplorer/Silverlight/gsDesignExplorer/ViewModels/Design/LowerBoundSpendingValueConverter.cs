namespace gsDesign.Explorer.ViewModels.Design
{
	using System;
	using System.Globalization;
	using Models;

	public class LowerBoundSpendingValueConverter
	{
		private const string BetaSpending = "Beta spending";
		private const string H0Spending = "H0 spending";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is LowerBoundSpending == false)
			{
				throw new NotImplementedException();
			}

			if (targetType.Equals(typeof(object)))
			{
				var lowerBoundSpending = (LowerBoundSpending)value;
				switch (lowerBoundSpending)
				{
					case LowerBoundSpending.BetaSpending:
						return BetaSpending;

					case LowerBoundSpending.H0Spending:
						return H0Spending;
				}

				return lowerBoundSpending.ToString();
			}

			throw new NotImplementedException("Unhandled targetType: " + targetType);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			switch (value.ToString())
			{
				case BetaSpending:
					return LowerBoundSpending.BetaSpending;

				case H0Spending:
					return LowerBoundSpending.H0Spending;
			}

			return (LowerBoundSpending)Enum.Parse(typeof(LowerBoundSpending), value.ToString(), true);
		}
	}
}
