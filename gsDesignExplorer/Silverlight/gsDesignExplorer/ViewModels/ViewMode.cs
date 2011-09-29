namespace gsDesign.Explorer.ViewModels
{
	using System;
	using System.Globalization;
	using System.Windows.Data;

	public enum ViewMode
	{
		Design,
		Analysis,
		Simulation,
		Test,
	}

	public class ViewModeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is ViewMode)
			{
				if (targetType.Equals(typeof(string)))
				{
					return ((ViewMode) value).ToString();
				}

				if (targetType.Equals(typeof(int)))
				{
					return (int) ((ViewMode) value);
				}
			}

			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType.Equals(typeof(ViewMode)))
			{
				if (value is string)
				{
					return Enum.Parse(typeof(ViewMode), (string)value, true);
				}

				if (value is int)
				{
					try
					{
						return (ViewMode)value;
					}
					catch (Exception e)
					{
						return ViewMode.Design;
					}
				}
			}

			throw new NotImplementedException();
		}
	}

}