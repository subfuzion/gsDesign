using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace gsDesign.LauncherGUI.ViewModels
{
	public enum RunState
	{
		Stopped,
		Running,
	}

	public class RunStateToBrushValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType.Equals(typeof(Brush)) && value is RunState)
			{
				return (RunState)value == RunState.Running ? Brushes.Green : Brushes.Red;
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
				return (RunState)value == RunState.Running ? "Stop" : "Start";
			}

			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
