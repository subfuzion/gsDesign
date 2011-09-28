using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace gsDesign.LauncherGUI.ViewModels
{
	public enum RunState
	{
		Invalid,
		Stopped,
		Running,
	}

	public class RunStateToBrushValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType.Equals(typeof(Brush)) && value is RunState)
			{
				switch ((RunState)value)
				{
					case RunState.Invalid:
						return Brushes.Red;

					case RunState.Stopped:
						return Brushes.LightGray;

					case RunState.Running:
						return Brushes.Green;
				}
			}

			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class RunStateToTransitionActionValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is RunState)
			{
				switch ((RunState)value)
				{
					case RunState.Invalid:
						return "Unavailable";

					case RunState.Stopped:
						return "Start";

					case RunState.Running:
						return "Stop";
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
