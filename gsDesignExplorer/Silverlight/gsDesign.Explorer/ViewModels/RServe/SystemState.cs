namespace gsDesign.Explorer.ViewModels.RServe
{
	using System;
	using System.Globalization;
	using System.Windows.Data;
	using System.Windows.Media;

	public enum SystemState
	{
		Invalid,
		Stopped,
		Running,
	}

	public class SystemStateToBrushValueConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType.Equals(typeof (Brush)) && value is SystemState)
			{
				switch ((SystemState) value)
				{
					case SystemState.Invalid:
						return Brushes.Red;

					case SystemState.Stopped:
						return Brushes.LightGray;

					case SystemState.Running:
						return Brushes.Green;
				}
			}

			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}

	public class SystemStateToTransitionActionValueConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is SystemState)
			{
				switch ((SystemState) value)
				{
					case SystemState.Invalid:
						return "Not configured";

					case SystemState.Stopped:
						return "Start system";

					case SystemState.Running:
						return "Stop system";
				}
			}

			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}