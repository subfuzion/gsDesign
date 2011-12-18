namespace gsDesign.Explorer.ViewModels.Design.SpendingFunctions
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows.Data;
	using gsDesign.Design.SpendingFunctions;

	public class SpendingFunctionBoundsValueConverter : IValueConverter
	{
		private const string LowerSpending = "Lower Spending";
		private const string UpperSpending = "Upper Spending";

		#region Implementation of IValueConverter

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null || !value.GetType().Equals(typeof(SpendingFunctionBounds))) throw new ArgumentException();

			if (targetType.Equals(typeof(int))) return (int) (SpendingFunctionBounds) value;

			if (targetType.Equals(typeof(object)) || targetType.Equals(typeof(string))) return ((SpendingFunctionBounds) value).ToString();

			if (targetType.Equals(typeof(IEnumerable)))
			{
				var values = new List<string>
				             {
				             	LowerSpending,
				             	UpperSpending,
				             };

				return values;
			}

			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null || !targetType.Equals(typeof(SpendingFunctionBounds))) throw new ArgumentException();

			var valueType = value.GetType();

			if (valueType.Equals(typeof(int))) return (SpendingFunctionBounds) value;

			if (valueType.Equals(typeof(string)) || valueType.Equals(typeof(object)))
			{
				var s = (string)value;

				switch (s)
				{
					case LowerSpending:
						return SpendingFunctionBounds.LowerSpending;

					case UpperSpending:
						return SpendingFunctionBounds.UpperSpending;

					default:
						return (SpendingFunctionBounds)Enum.Parse(typeof(SpendingFunctionBounds), (string)value, true);
				}
			}

			throw new NotImplementedException();
		}

		#endregion
	}
}