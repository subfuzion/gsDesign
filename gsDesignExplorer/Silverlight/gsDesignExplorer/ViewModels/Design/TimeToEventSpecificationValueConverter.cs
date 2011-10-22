using System.Collections.Generic;

namespace gsDesign.Explorer.ViewModels.Design
{
	using System;
	using System.Collections;
	using System.Globalization;
	using System.Windows.Data;
	using Models.Design.SampleSize;

	public class TimeToEventSpecificationValueConverter : IValueConverter
	{
		private const string EventRate = "Event Rate";
		private const string MedianTime = "Median Time";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is TimeToEventSpecification == false)
			{
				throw new NotImplementedException();
			}

			if (targetType.Equals(typeof(IEnumerable)))
			{
				var values = new List<string>
				             {
				             	EventRate,
								MedianTime,
				             };

				return values;
			}

			if (targetType.Equals(typeof(object)) || targetType.Equals(typeof(string)))
			{
				var specification = (TimeToEventSpecification)value;
				switch (specification)
				{
					case TimeToEventSpecification.EventRate:
						return EventRate;

					case TimeToEventSpecification.MedianTime:
						return MedianTime;
				}

				return specification.ToString();
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
				case EventRate:
					return TimeToEventSpecification.EventRate;

				case MedianTime:
					return TimeToEventSpecification.MedianTime;
			}

			return (TimeToEventSpecification)Enum.Parse(typeof(TimeToEventSpecification), value.ToString(), true);
		}
	}
}
