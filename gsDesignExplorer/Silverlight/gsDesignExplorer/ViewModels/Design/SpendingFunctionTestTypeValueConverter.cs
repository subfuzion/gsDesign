namespace gsDesign.Explorer.ViewModels.Design
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows.Data;
	using Models;

	public class SpendingFunctionTestTypeValueConverter : IValueConverter
	{
		private const string OneSided = "1-sided";
		private const string TwoSidedSymmetric = "2-sided symmetric";
		private const string TwoSidedWithFutility = "2-sided with futility";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;
			
			if (!value.GetType().Equals(typeof(SpendingFunctionTestType))) throw new ArgumentException();

			if (targetType.Equals(typeof(int))) return (int)(SpendingFunctionTestType)value;

			if (targetType.Equals(typeof(object)) || targetType.Equals(typeof(string)))
			{
				var s = (SpendingFunctionTestType)value;
				switch (s)
				{
					case SpendingFunctionTestType.OneSided:
						return OneSided;

					case SpendingFunctionTestType.TwoSidedSymmetric:
						return TwoSidedSymmetric;

					case SpendingFunctionTestType.TwoSidedWithFutility:
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
			
			if (!targetType.Equals(typeof(SpendingFunctionTestType))) throw new ArgumentException();

			var valueType = value.GetType();

			if (valueType.Equals(typeof(int))) return (SpendingFunctionTestType)value;

			if (valueType.Equals(typeof(string)) || valueType.Equals(typeof(object)))
			{
				var s = (string)value;

				switch (s)
				{
					case OneSided:
						return SpendingFunctionTestType.OneSided;

					case TwoSidedSymmetric:
						return SpendingFunctionTestType.TwoSidedSymmetric;

					case TwoSidedWithFutility:
						return SpendingFunctionTestType.TwoSidedWithFutility;

					default:
						return (SpendingFunctionTestType)Enum.Parse(typeof(SpendingFunctionTestType), (string)value, true);
				}
			}

			throw new NotImplementedException();
		}
	}
}