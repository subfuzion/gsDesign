namespace gsDesign.Explorer.ViewModels.Design.SpendingFunctions.ParameterFree
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows.Data;
	using gsDesign.Design.SpendingFunctions.ParameterFree;

	public class LanDeMetsApproximationValueConverter : IValueConverter
	{
		private const string OBrienFleming = "O'Brien-Fleming";
		private const string Pocock = "Pocock";

		#region Implementation of IValueConverter

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			if (!value.GetType().Equals(typeof(LanDeMetsApproximation))) throw new ArgumentException();

			if (targetType.Equals(typeof(int))) return (int)(LanDeMetsApproximation)value;

			if (targetType.Equals(typeof(object)) || targetType.Equals(typeof(string)))
			{
				var s = (LanDeMetsApproximation)value;
				switch (s)
				{
					case LanDeMetsApproximation.OBrienFleming:
						return OBrienFleming;

					case LanDeMetsApproximation.Pocock:
						return Pocock;

					default:
						return s.ToString();
				}
			}

			if (targetType.Equals(typeof(IEnumerable)))
			{
				var values = new List<string>
				             {
				             	OBrienFleming,
				             	Pocock,
				             };

				return values;
			}

			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			if (!targetType.Equals(typeof(LanDeMetsApproximation))) throw new ArgumentException();

			var valueType = value.GetType();

			if (valueType.Equals(typeof(int))) return (LanDeMetsApproximation)value;

			if (valueType.Equals(typeof(string)) || valueType.Equals(typeof(object)))
			{
				var s = (string)value;

				switch (s)
				{
					case OBrienFleming:
						return LanDeMetsApproximation.OBrienFleming;

					case Pocock:
						return LanDeMetsApproximation.Pocock;

					default:
						return (LanDeMetsApproximation)Enum.Parse(typeof(LanDeMetsApproximation), (string)value, true);
				}
			}

			throw new NotImplementedException();
		}

		#endregion
	}
}