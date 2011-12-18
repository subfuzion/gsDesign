namespace gsDesign.Explorer.ViewModels.Output
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows.Data;
	using gsDesign.Output;

	public class PlotTypeValueConverter : IValueConverter
	{
		private const string Boundaries = "Boundaries";
		private const string Power = "Power";
		private const string TreatmentEffect = "Treatment Effect";
		private const string ConditionalPower = "Conditional Power";
		private const string SpendingFunction = "Spending Function";
		private const string ExpectedSampleSize = "Expected Sample Size";
		private const string BValues = "B-Values";

		#region Implementation of IValueConverter

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			if (!value.GetType().Equals(typeof (PlotType))) throw new ArgumentException();

			if (targetType.Equals(typeof (int))) return (int) (PlotType) value;

			if (targetType.Equals(typeof (object)) || targetType.Equals(typeof (string)))
			{
				var s = (PlotType) value;
				switch (s)
				{
					case PlotType.Boundaries:
						return Boundaries;

					case PlotType.Power:
						return Power;

					case PlotType.TreatmentEffect:
						return TreatmentEffect;

					case PlotType.ConditionalPower:
						return ConditionalPower;

					case PlotType.SpendingFunction:
						return SpendingFunction;

					case PlotType.ExpectedSampleSize:
						return ExpectedSampleSize;

					case PlotType.BValues:
						return BValues;

					default:
						return s.ToString();
				}
			}

			if (targetType.Equals(typeof (IEnumerable)))
			{
				var values = new List<string>
				{
					Boundaries,
					Power,
					TreatmentEffect,
					ConditionalPower,
					SpendingFunction,
					ExpectedSampleSize,
					BValues,
				};

				return values;
			}

			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			if (!targetType.Equals(typeof (PlotType))) throw new ArgumentException();

			Type valueType = value.GetType();

			if (valueType.Equals(typeof (int))) return (PlotType) value;

			if (valueType.Equals(typeof (string)) || valueType.Equals(typeof (object)))
			{
				var s = (string) value;

				switch (s)
				{
					case Boundaries:
						return PlotType.Boundaries;

					case Power:
						return PlotType.Power;

					case TreatmentEffect:
						return PlotType.TreatmentEffect;

					case ConditionalPower:
						return PlotType.ConditionalPower;

					case SpendingFunction:
						return PlotType.SpendingFunction;

					case ExpectedSampleSize:
						return PlotType.ExpectedSampleSize;

					case BValues:
						return PlotType.BValues;

					default:
						return (PlotType) Enum.Parse(typeof (PlotType), (string) value, true);
				}
			}

			throw new NotImplementedException();
		}

		#endregion
	}
}