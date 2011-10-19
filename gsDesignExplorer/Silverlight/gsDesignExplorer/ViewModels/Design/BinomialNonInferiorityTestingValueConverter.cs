namespace gsDesign.Explorer.ViewModels.Design
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows.Data;
	using Models;

	public class BinomialNonInferiorityTestingValueConverter : IValueConverter
	{
		private const string Superiority = "Superiority";
		private const string SuperiorityWithMargin = "Non-inferiority - superiority with margin";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is BinomialNonInferiorityTesting == false)
			{
				throw new NotImplementedException();
			}

			if (targetType.Equals(typeof(IEnumerable)))
			{
				var values = new List<string>
				             {
				             	Superiority,
								SuperiorityWithMargin,
				             };

				return values;
			}

			if (targetType.Equals(typeof(object)) || targetType.Equals(typeof(string)))
			{
				var binomialTesting = (BinomialNonInferiorityTesting)value;
				switch (binomialTesting)
				{
					case BinomialNonInferiorityTesting.Superiority:
						return Superiority;

					case BinomialNonInferiorityTesting.SuperiorityWithMargin:
						return SuperiorityWithMargin;
				}

				return binomialTesting.ToString();
			}

			throw new NotImplementedException("Unhandled targetType: " + targetType);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return null;
			}

			switch (value.ToString())
			{
				case Superiority:
					return BinomialNonInferiorityTesting.Superiority;

				case SuperiorityWithMargin:
					return BinomialNonInferiorityTesting.SuperiorityWithMargin;
			}

			return (BinomialNonInferiorityTesting)Enum.Parse(typeof(BinomialNonInferiorityTesting), value.ToString(), true);
		}
	}
}
