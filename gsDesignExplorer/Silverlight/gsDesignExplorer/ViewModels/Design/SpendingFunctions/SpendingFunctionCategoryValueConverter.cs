namespace gsDesign.Explorer.ViewModels.Design.SpendingFunctions
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows.Data;
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
			
			if (!value.GetType().Equals(typeof(SpendingFunctionParameterCategory))) throw new ArgumentException();

			if (targetType.Equals(typeof(int))) return (int)(SpendingFunctionParameterCategory)value;

			if (targetType.Equals(typeof(object)) || targetType.Equals(typeof(string)))
			{
				var s = (SpendingFunctionParameterCategory)value;
				switch (s)
				{
					case SpendingFunctionParameterCategory.ParameterFree:
						return ParameterFree;

					case SpendingFunctionParameterCategory.OneParameter:
						return OneParameter;

					case SpendingFunctionParameterCategory.TwoParameter:
						return TwoParameter;

					case SpendingFunctionParameterCategory.ThreeParameter:
						return ThreeParameter;

					case SpendingFunctionParameterCategory.PiecewiseLinear:
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
			
			if (!targetType.Equals(typeof(SpendingFunctionParameterCategory))) throw new ArgumentException();

			var valueType = value.GetType();

			if (valueType.Equals(typeof(int))) return (SpendingFunctionParameterCategory)value;

			if (valueType.Equals(typeof(string)) || valueType.Equals(typeof(object)))
			{
				var s = (string) value;

				switch (s)
				{
					case ParameterFree:
						return SpendingFunctionParameterCategory.ParameterFree;

					case OneParameter:
						return SpendingFunctionParameterCategory.OneParameter;

					case TwoParameter:
						return SpendingFunctionParameterCategory.TwoParameter;

					case ThreeParameter:
						return SpendingFunctionParameterCategory.ThreeParameter;

					case PiecewiseLinear:
						return SpendingFunctionParameterCategory.PiecewiseLinear;

					default:
						return (SpendingFunctionParameterCategory)Enum.Parse(typeof(SpendingFunctionParameterCategory), (string)value, true);
				}
			}

			throw new NotImplementedException();
		}

		#endregion
	}
}