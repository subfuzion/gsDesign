namespace Subfuzion.Silverlight.UI.Charting.ValueConverters
{
	using System;
	using System.Globalization;
	using System.Windows.Data;
	using gsDesign.Design.SpendingFunctions;

	// http://www.codeproject.com/Articles/81960/Binding-RadioButtons-to-an-Enum-in-Silverlight
	public class SpendingFunctionLowerBoundSpendingRadioButtonValueConverter : IValueConverter
	{
		private const string BetaSpending = "Lower Bounds - Beta Spending";
		private const string H0Spending = "Lower Bounds H0 Spending";

		#region Implementation of IValueConverter

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			if (value.GetType() != typeof(SpendingFunctionLowerBoundSpending)) throw new ArgumentException();

			if (targetType == typeof(object) || targetType == typeof(string))
			{
				var plotConstraint = (SpendingFunctionLowerBoundSpending)Enum.Parse(typeof(SpendingFunctionLowerBoundSpending), parameter.ToString(), true);

				switch (plotConstraint)
				{
					case SpendingFunctionLowerBoundSpending.BetaSpending:
						return BetaSpending;

					case SpendingFunctionLowerBoundSpending.H0Spending:
						return H0Spending;

					default:
						return plotConstraint.ToString();
				}
			}

			if (targetType == typeof(bool?))
			{
				return value.ToString() == parameter.ToString();
			}

			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			if (targetType != typeof(SpendingFunctionLowerBoundSpending)) throw new ArgumentException();

			Type valueType = value.GetType();

			if (valueType == typeof(bool))
			{
				return (bool)value ? Enum.Parse(targetType, parameter.ToString(), true) : null;
			}

			throw new NotImplementedException();
		}

		#endregion
	}
}