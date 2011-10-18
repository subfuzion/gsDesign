namespace gsDesign.Explorer.ViewModels.Design
{
	using System;
	using System.Globalization;
	using Models;

	public class LowerBoundTestingValueConverter
	{
		private const string Binding = "Binding";
		private const string NonBinding = "Non-binding";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is TestType == false)
			{
				throw new NotImplementedException();
			}

			if (targetType.Equals(typeof(object)))
			{
				var lowerBoundSpending = (LowerBoundTesting)value;
				switch (lowerBoundSpending)
				{
					case LowerBoundTesting.Binding:
						return Binding;

					case LowerBoundTesting.NonBinding:
						return NonBinding;
				}

				return lowerBoundSpending.ToString();
			}

			throw new NotImplementedException("Unhandled targetType: " + targetType);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			switch (value.ToString())
			{
				case Binding:
					return LowerBoundTesting.Binding;

				case NonBinding:
					return LowerBoundTesting.NonBinding;
			}

			return (LowerBoundTesting)Enum.Parse(typeof(LowerBoundTesting), value.ToString(), true);
		}
	}
}
