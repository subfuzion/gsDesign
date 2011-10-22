namespace gsDesign.Explorer.ViewModels.Design
{
	using System;
	using System.Globalization;
	using System.Windows.Data;
	using Models.Design.ErrorPowerTiming;

	public class IntervalSpacingValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is IntervalSpacing == false)
			{
				throw new NotImplementedException();
			}

			var eptSpacing = (IntervalSpacing) value;

			if (targetType.Equals(typeof(object)))
			{
				return eptSpacing.ToString();
			}

			throw new NotImplementedException("Unhandled targetType: " + targetType);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (IntervalSpacing) Enum.Parse(typeof (IntervalSpacing), value.ToString(), true);
		}
	}
}