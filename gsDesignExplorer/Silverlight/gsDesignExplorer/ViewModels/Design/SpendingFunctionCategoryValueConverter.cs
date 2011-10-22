namespace gsDesign.Explorer.ViewModels.Design
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows.Data;
	using Models;
	using Models.Design.SpendingFunctions;

	public class SpendingFunctionCategoryValueConverter : IValueConverter
	{
		private const string ParameterFree = "Parameter Free";
		private const string OneParameter = "1-Parameter";
		private const string TwoParameter = "2-Parameter";
		private const string ThreeParameter = "3-Parameter";
		private const string PiecewiseLinear = "Piecewise Linear";

		#region Implementation of IValueConverter

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;
			
			if (!value.GetType().Equals(typeof(SpendingFunctionCategory))) throw new ArgumentException();

			if (targetType.Equals(typeof(int))) return (int)(SpendingFunctionCategory)value;

			if (targetType.Equals(typeof(object)) || targetType.Equals(typeof(string)))
			{
				var s = (SpendingFunctionCategory)value;
				switch (s)
				{
					case SpendingFunctionCategory.ParameterFree:
						return ParameterFree;

					case SpendingFunctionCategory.OneParameter:
						return OneParameter;

					case SpendingFunctionCategory.TwoParameter:
						return TwoParameter;

					case SpendingFunctionCategory.ThreeParameter:
						return ThreeParameter;

					case SpendingFunctionCategory.PiecewiseLinear:
						return PiecewiseLinear;

				}

				return s.ToString();
			}

			if (targetType.Equals(typeof(IEnumerable)))
			{
				var values = new List<string>
				             {
				             	ParameterFree,
				             	OneParameter,
				             	TwoParameter,
				             	ThreeParameter,
				             	PiecewiseLinear,
				             };

				return values;
			}

			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;
			
			if (!targetType.Equals(typeof(SpendingFunctionCategory))) throw new ArgumentException();

			var valueType = value.GetType();

			if (valueType.Equals(typeof(int))) return (SpendingFunctionCategory)value;

			if (valueType.Equals(typeof(string)) || valueType.Equals(typeof(object)))
			{
				var s = (string) value;

				switch (s)
				{
					case ParameterFree:
						return SpendingFunctionCategory.ParameterFree;

					case OneParameter:
						return SpendingFunctionCategory.OneParameter;

					case TwoParameter:
						return SpendingFunctionCategory.TwoParameter;

					case ThreeParameter:
						return SpendingFunctionCategory.ThreeParameter;

					case PiecewiseLinear:
						return SpendingFunctionCategory.PiecewiseLinear;

					default:
						return (SpendingFunctionCategory)Enum.Parse(typeof(SpendingFunctionCategory), (string)value, true);
				}
			}

			throw new NotImplementedException();
		}

		#endregion
	}
}