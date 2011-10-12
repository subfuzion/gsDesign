namespace gsDesign.Explorer.Models
{
	using System;
	using System.Globalization;
	using System.Windows.Data;

	public enum EptSpacing
	{
		Equal,
		Unequal,
	}

	public class EptSpacingValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is EptSpacing == false)
			{
				throw new NotImplementedException();
			}

			var eptSpacing = (EptSpacing) value;

			if (targetType.Equals(typeof(object)))
			{
				return eptSpacing.ToString();
			}

			throw new NotImplementedException("Unhandled targetType: " + targetType);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (EptSpacing) Enum.Parse(typeof (EptSpacing), value.ToString(), true);
		}
	}
}
