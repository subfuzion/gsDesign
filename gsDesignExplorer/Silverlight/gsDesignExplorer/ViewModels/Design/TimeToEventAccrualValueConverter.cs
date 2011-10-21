namespace gsDesign.Explorer.ViewModels.Design
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows.Data;
	using Models;

	public class TimeToEventAccrualValueConverter : IValueConverter
	{
		private const string Uniform = "Uniform";
		private const string Exponential = "Exponential";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is TimeToEventAccrual == false)
			{
				throw new NotImplementedException();
			}

			if (targetType.Equals(typeof(IEnumerable)))
			{
				var values = new List<string>
				             {
				             	Uniform,
								Exponential,
				             };

				return values;
			}

			if (targetType.Equals(typeof(object)) || targetType.Equals(typeof(string)))
			{
				var binomialTesting = (TimeToEventAccrual)value;
				switch (binomialTesting)
				{
					case TimeToEventAccrual.Uniform:
						return Uniform;

					case TimeToEventAccrual.Exponential:
						return Exponential;
				}

				return binomialTesting.ToString();
			}

			throw new NotImplementedException("Unhandled targetType: " + targetType);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return null;
			}

			switch (value.ToString())
			{
				case Uniform:
					return TimeToEventAccrual.Uniform;

				case Exponential:
					return TimeToEventAccrual.Exponential;
			}

			return (TimeToEventAccrual)Enum.Parse(typeof(TimeToEventAccrual), value.ToString(), true);
		}
	}
}
