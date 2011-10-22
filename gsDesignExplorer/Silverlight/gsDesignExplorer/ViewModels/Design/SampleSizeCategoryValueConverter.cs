namespace gsDesign.Explorer.ViewModels.Design
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows.Data;
	using Models;

	public class SampleSizeCategoryValueConverter : IValueConverter
	{
		private const string UserInput = "User Input";
		private const string Binomial = "Binomial";
		private const string TimeToEvent = "Time to Event";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			if (!value.GetType().Equals(typeof(SampleSizeCategory))) throw new ArgumentException();

			if (targetType.Equals(typeof(int))) return (int)(SampleSizeCategory)value;

			if (targetType.Equals(typeof(object)) || targetType.Equals(typeof(string)))
			{
				var s = (SampleSizeCategory)value;
				switch (s)
				{
					case SampleSizeCategory.UserInput:
						return UserInput;

					case SampleSizeCategory.Binomial:
						return Binomial;

					case SampleSizeCategory.TimeToEvent:
						return TimeToEvent;

					default:
						return s.ToString();
				}
			}

			if (targetType.Equals(typeof(IEnumerable)))
			{
				var values = new List<string>
				             {
				             	UserInput,
				             	Binomial,
								TimeToEvent,
				             };

				return values;
			}

			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			if (!targetType.Equals(typeof(SampleSizeCategory))) throw new ArgumentException();

			var valueType = value.GetType();

			if (valueType.Equals(typeof(int))) return (SampleSizeCategory)value;

			if (valueType.Equals(typeof(string)) || valueType.Equals(typeof(object)))
			{
				var s = (string)value;

				switch (s)
				{
					case UserInput:
						return SampleSizeCategory.UserInput;

					case Binomial:
						return SampleSizeCategory.Binomial;

					case TimeToEvent:
						return SampleSizeCategory.TimeToEvent;

					default:
						return
							(SampleSizeCategory)
							Enum.Parse(typeof(SampleSizeCategory), (string)value, true);
				}
			}

			throw new NotImplementedException();
		}
	}
}