namespace gsDesign.Explorer.ViewModels.Design
{
	using System;
	using System.Globalization;
	using Models;

	public class SampleSizeTypeValueConverter
	{
		private const string UserInput = "User Input";
		private const string Binomial = "Binomial";
		private const string TimeToEvent = "Time to Event";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is SampleSizeType == false)
			{
				throw new NotImplementedException();
			}

			if (targetType.Equals(typeof (object)))
			{
				var testType = (SampleSizeType)value;
				switch (testType)
				{
					case SampleSizeType.UserInput:
						return UserInput;

					case SampleSizeType.Binomial:
						return Binomial;

					case SampleSizeType.TimeToEvent:
						return TimeToEvent;
				}

				return testType.ToString();
			}

			throw new NotImplementedException("Unhandled targetType: " + targetType);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			switch (value.ToString())
			{
				case UserInput:
					return SampleSizeType.UserInput;

				case Binomial:
					return SampleSizeType.Binomial;

				case TimeToEvent:
					return SampleSizeType.TimeToEvent;
			}

			return (SampleSizeType)Enum.Parse(typeof(SampleSizeType), value.ToString(), true);
		}
	}
}