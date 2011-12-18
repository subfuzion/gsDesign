namespace gsDesign.Explorer.ViewModels.Design.SpendingFunctions
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows.Data;
	using gsDesign.Design.SpendingFunctions;

	public class SpendingFunctionLowerBoundSpendingValueConverter : IValueConverter
	{
		private const string BetaSpending = "Beta spending";
		private const string H0Spending = "H0 spending";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;
			
			if (!value.GetType().Equals(typeof(SpendingFunctionLowerBoundSpending))) throw new ArgumentException();

			if (targetType.Equals(typeof(int))) return (int)(SpendingFunctionLowerBoundSpending)value;

			if (targetType.Equals(typeof(object)) || targetType.Equals(typeof(string)))
			{
				var s = (SpendingFunctionLowerBoundSpending)value;
				switch (s)
				{
					case SpendingFunctionLowerBoundSpending.BetaSpending:
						return BetaSpending;

					case SpendingFunctionLowerBoundSpending.H0Spending:
						return H0Spending;

					default:
						return s.ToString();
				}
			}

			if (targetType.Equals(typeof(IEnumerable)))
			{
				var values = new List<string>
				             {
				             	BetaSpending,
				             	H0Spending,
				             };

				return values;
			}

			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			if (!targetType.Equals(typeof (SpendingFunctionLowerBoundSpending))) throw new ArgumentException();

			var valueType = value.GetType();

			if (valueType.Equals(typeof (int))) return (SpendingFunctionLowerBoundSpending) value;

			if (valueType.Equals(typeof (string)) || valueType.Equals(typeof (object)))
			{
				var s = (string) value;

				switch (s)
				{
					case BetaSpending:
						return SpendingFunctionLowerBoundSpending.BetaSpending;

					case H0Spending:
						return SpendingFunctionLowerBoundSpending.H0Spending;

					default:
						return
							(SpendingFunctionLowerBoundSpending)
							Enum.Parse(typeof (SpendingFunctionLowerBoundSpending), (string) value, true);
				}
			}

			throw new NotImplementedException();
		}
	}
}
