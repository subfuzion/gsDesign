namespace gsDesign.Explorer.ViewModels.Design.SpendingFunctions.OneParameter
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows.Data;
	using gsDesign.Design.SpendingFunctions.OneParameter;

	public class OneParameterFamilyValueConverter : IValueConverter
	{
		private const string HwangShihDeCani = "Hwang-Shih-DeCani";
		private const string Power = "Power";
		private const string Exponential = "Exponential";

		#region Implementation of IValueConverter

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			if (value.GetType() != typeof (OneParameterFamily)) throw new ArgumentException();

			if (targetType == typeof (int)) return (int) (OneParameterFamily) value;

			if (targetType == typeof (object) || targetType == typeof (string))
			{
				var s = (OneParameterFamily) value;
				switch (s)
				{
					case OneParameterFamily.HwangShihDeCani:
						return HwangShihDeCani;

					case OneParameterFamily.Power:
						return Power;

					case OneParameterFamily.Exponential:
						return Exponential;

					default:
						return s.ToString();
				}
			}

			if (targetType == typeof (IEnumerable))
			{
				var values = new List<string>
				{
					HwangShihDeCani,
					Power,
					Exponential,
				};

				return values;
			}

			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			if (targetType != typeof (OneParameterFamily)) throw new ArgumentException();

			Type valueType = value.GetType();

			if (valueType == typeof (int)) return (OneParameterFamily) value;

			if (valueType == typeof (string) || valueType == typeof (object))
			{
				var s = (string) value;

				switch (s)
				{
					case HwangShihDeCani:
						return OneParameterFamily.HwangShihDeCani;

					case Power:
						return OneParameterFamily.Power;

					case Exponential:
						return OneParameterFamily.Exponential;

					default:
						return (OneParameterFamily) Enum.Parse(typeof (OneParameterFamily), (string) value, true);
				}
			}

			throw new NotImplementedException();
		}

		#endregion
	}
}