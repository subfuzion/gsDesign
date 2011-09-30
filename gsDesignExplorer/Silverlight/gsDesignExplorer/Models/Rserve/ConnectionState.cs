namespace gsDesign.Explorer.Models.Rserve
{
	using System;
	using System.Globalization;
	using System.Windows.Data;
	using System.Windows.Media;

	public enum ConnectionState
	{
		Disconnected,
		Connected,
	}

	public class ConnectionStateValueConverter : IValueConverter
	{
		private static readonly SolidColorBrush Red = new SolidColorBrush(Colors.Red);
		private static readonly SolidColorBrush Green = new SolidColorBrush(Colors.Green);

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType.Equals(typeof(Brush)) && value is ConnectionState)
			{
				switch ((ConnectionState)value)
				{
					case ConnectionState.Disconnected:
						return Red;

					case ConnectionState.Connected:
						return Green;
				}
			}

			if (value is ConnectionState)
			{
				switch ((ConnectionState)value)
				{
					case ConnectionState.Connected:
						return "Disconnect";

					case ConnectionState.Disconnected:
						return "Connect";
				}
			}

			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
