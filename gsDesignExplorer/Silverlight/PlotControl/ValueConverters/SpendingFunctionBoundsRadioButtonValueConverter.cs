namespace Subfuzion.Silverlight.UI.Charting.ValueConverters
{
	using System;
	using System.Globalization;
	using System.Windows.Data;
	using gsDesign.Design.SpendingFunctions;

	// http://www.codeproject.com/Articles/81960/Binding-RadioButtons-to-an-Enum-in-Silverlight
	public class SpendingFunctionBoundsRadioButtonValueConverter : IValueConverter
	{
		private const string LowerSpending = "Lower Spending";
		private const string UpperSpending = "Upper Spending";

		#region Implementation of IValueConverter

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			if (value.GetType() != typeof(SpendingFunctionBounds)) throw new ArgumentException();

			if (targetType == typeof(object) || targetType == typeof(string))
			{
				var plotConstraint = (SpendingFunctionBounds)Enum.Parse(typeof(SpendingFunctionBounds), parameter.ToString(), true);

				switch (plotConstraint)
				{
					case SpendingFunctionBounds.LowerSpending:
						return LowerSpending;

					case SpendingFunctionBounds.UpperSpending:
						return UpperSpending;

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

			if (targetType != typeof(SpendingFunctionBounds)) throw new ArgumentException();

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