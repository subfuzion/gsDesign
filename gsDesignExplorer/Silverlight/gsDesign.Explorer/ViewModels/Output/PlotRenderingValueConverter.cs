namespace gsDesign.Explorer.ViewModels.Output
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows.Data;
	using Models.Output;

	public class PlotRenderingValueConverter : IValueConverter
	{
		private const string BasicQuality = "Basic (high-speed)";
		private const string HighQuality = "High Quality (slow-speed)";

		#region Implementation of IValueConverter

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			if (!value.GetType().Equals(typeof(PlotRendering))) throw new ArgumentException();

			if (targetType.Equals(typeof(int))) return (int)(PlotRendering)value;

			if (targetType.Equals(typeof(object)) || targetType.Equals(typeof(string)))
			{
				var s = (PlotRendering)value;
				switch (s)
				{
					case PlotRendering.BasicQuality:
						return BasicQuality;

					case PlotRendering.HighQuality:
						return HighQuality;

					default:
						return s.ToString();
				}
			}

			if (targetType.Equals(typeof(IEnumerable)))
			{
				var values = new List<string>
				{
					BasicQuality,
					HighQuality,
				};

				return values;
			}

			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			if (!targetType.Equals(typeof(PlotRendering))) throw new ArgumentException();

			Type valueType = value.GetType();

			if (valueType.Equals(typeof(int))) return (PlotRendering)value;

			if (valueType.Equals(typeof(string)) || valueType.Equals(typeof(object)))
			{
				var s = (string)value;

				switch (s)
				{
					case BasicQuality:
						return PlotRendering.BasicQuality;

					case HighQuality:
						return PlotRendering.HighQuality;

					default:
						return (PlotRendering)Enum.Parse(typeof(PlotRendering), (string)value, true);
				}
			}

			throw new NotImplementedException();
		}

		#endregion
	}
}