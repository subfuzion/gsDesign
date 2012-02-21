namespace Subfuzion.Silverlight.UI.Charting
{
	using System;
	using System.Globalization;
	using System.Windows.Data;

	// http://www.codeproject.com/Articles/81960/Binding-RadioButtons-to-an-Enum-in-Silverlight
	public class PlotConstraintValueConverter : IValueConverter
	{
		private const string MoveLineWithPoint = "Move line with point";
		private const string MovePointAlongLine = "Move point along line";

		#region Implementation of IValueConverter

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			if (value.GetType() != typeof (PlotConstraint)) throw new ArgumentException();

			if (targetType == typeof (object) || targetType == typeof (string))
			{
				var plotConstraint = (PlotConstraint) Enum.Parse(typeof (PlotConstraint), parameter.ToString(), true);

				switch (plotConstraint)
				{
					case PlotConstraint.MoveLineWithPoint:
						return MoveLineWithPoint;

					case PlotConstraint.MovePointAlongLine:
						return MovePointAlongLine;

					default:
						return plotConstraint.ToString();
				}
			}

			if (targetType == typeof (bool?))
			{
				return value.ToString() == parameter.ToString();
			}

			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			if (targetType != typeof (PlotConstraint)) throw new ArgumentException();

			Type valueType = value.GetType();

			if (valueType == typeof (bool))
			{
				return (bool) value ? Enum.Parse(targetType, parameter.ToString(), true) : null;
			}

			throw new NotImplementedException();
		}

		#endregion
	}
}