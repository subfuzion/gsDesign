namespace gsDesign.Explorer.ViewModels.Design
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows.Data;
	using Models;
	using Models.Design.SampleSize;

	public class TimeToEventHypothesisValueConverter : IValueConverter
	{
		private const string RiskRatio = "Risk Ratio";
		private const string RiskDifference = "Risk Difference";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is TimeToEventHypothesis == false)
			{
				throw new NotImplementedException();
			}

			if (targetType.Equals(typeof(IEnumerable)))
			{
				var values = new List<string>
				             {
				             	RiskRatio,
								RiskDifference,
				             };

				return values;
			}

			if (targetType.Equals(typeof(object)) || targetType.Equals(typeof(string)))
			{
				var binomialTesting = (TimeToEventHypothesis)value;
				switch (binomialTesting)
				{
					case TimeToEventHypothesis.RiskRatio:
						return RiskRatio;

					case TimeToEventHypothesis.RiskDifference:
						return RiskDifference;
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
				case RiskRatio:
					return TimeToEventHypothesis.RiskRatio;

				case RiskDifference:
					return TimeToEventHypothesis.RiskDifference;
			}

			return (TimeToEventHypothesis)Enum.Parse(typeof(TimeToEventHypothesis), value.ToString(), true);
		}
	}
}
