namespace gsDesign.Explorer.ViewModels.Design
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows.Data;
	using Models;

	public class SpendingFunctionTypeValueConverter : IValueConverter
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
			
			if (!value.GetType().Equals(typeof(SpendingFunctionType))) throw new ArgumentException();

			if (targetType.Equals(typeof(int))) return (int)(SpendingFunctionType)value;

			if (targetType.Equals(typeof(object)) || targetType.Equals(typeof(string)))
			{
				var s = (SpendingFunctionType)value;
				switch (s)
				{
					case SpendingFunctionType.ParameterFree:
						return ParameterFree;

					case SpendingFunctionType.OneParameter:
						return OneParameter;

					case SpendingFunctionType.TwoParameter:
						return TwoParameter;

					case SpendingFunctionType.ThreeParameter:
						return ThreeParameter;

					case SpendingFunctionType.PiecewiseLinear:
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
			
			if (!targetType.Equals(typeof(SpendingFunctionType))) throw new ArgumentException();

			var valueType = value.GetType();

			if (valueType.Equals(typeof(int))) return (SpendingFunctionType)value;

			if (valueType.Equals(typeof(string)) || valueType.Equals(typeof(object)))
			{
				var s = (string) value;

				switch (s)
				{
					case ParameterFree:
						return SpendingFunctionType.ParameterFree;

					case OneParameter:
						return SpendingFunctionType.OneParameter;

					case TwoParameter:
						return SpendingFunctionType.TwoParameter;

					case ThreeParameter:
						return SpendingFunctionType.ThreeParameter;

					case PiecewiseLinear:
						return SpendingFunctionType.PiecewiseLinear;

					default:
						return (SpendingFunctionType)Enum.Parse(typeof(SpendingFunctionType), (string)value, true);
				}
			}

			throw new NotImplementedException();
		}

		#endregion
	}
}