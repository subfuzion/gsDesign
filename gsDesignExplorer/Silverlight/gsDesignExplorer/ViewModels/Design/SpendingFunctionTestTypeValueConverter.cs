namespace gsDesign.Explorer.ViewModels.Design
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows.Data;
	using Models;
	using Models.Design.SpendingFunctions;

	public class SpendingFunctionTestTypeValueConverter : IValueConverter
	{
		private const string OneSided = "1-sided";
		private const string TwoSidedSymmetric = "2-sided symmetric";
		private const string TwoSidedWithFutility = "2-sided with futility";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;
			
			if (!value.GetType().Equals(typeof(SpendingFunctionTestCategory))) throw new ArgumentException();

			if (targetType.Equals(typeof(int))) return (int)(SpendingFunctionTestCategory)value;

			if (targetType.Equals(typeof(object)) || targetType.Equals(typeof(string)))
			{
				var s = (SpendingFunctionTestCategory)value;
				switch (s)
				{
					case SpendingFunctionTestCategory.OneSided:
						return OneSided;

					case SpendingFunctionTestCategory.TwoSidedSymmetric:
						return TwoSidedSymmetric;

					case SpendingFunctionTestCategory.TwoSidedWithFutility:
						return TwoSidedWithFutility;

				}

				return s.ToString();
			}

			if (targetType.Equals(typeof(IEnumerable)))
			{
				var values = new List<string>
				             {
				             	OneSided,
				             	TwoSidedSymmetric,
				             	TwoSidedWithFutility,
				             };

				return values;
			}

			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;
			
			if (!targetType.Equals(typeof(SpendingFunctionTestCategory))) throw new ArgumentException();

			var valueType = value.GetType();

			if (valueType.Equals(typeof(int))) return (SpendingFunctionTestCategory)value;

			if (valueType.Equals(typeof(string)) || valueType.Equals(typeof(object)))
			{
				var s = (string)value;

				switch (s)
				{
					case OneSided:
						return SpendingFunctionTestCategory.OneSided;

					case TwoSidedSymmetric:
						return SpendingFunctionTestCategory.TwoSidedSymmetric;

					case TwoSidedWithFutility:
						return SpendingFunctionTestCategory.TwoSidedWithFutility;

					default:
						return (SpendingFunctionTestCategory)Enum.Parse(typeof(SpendingFunctionTestCategory), (string)value, true);
				}
			}

			throw new NotImplementedException();
		}
	}
}