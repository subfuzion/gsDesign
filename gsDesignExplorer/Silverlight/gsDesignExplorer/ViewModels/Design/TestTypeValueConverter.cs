namespace gsDesign.Explorer.ViewModels.Design
{
	using System;
	using System.Globalization;
	using Models;

	public class TestTypeValueConverter
	{
		private const string OneSided = "1-sided";
		private const string TwoSidedSymmetric = "2-sided symmetric";
		private const string TwoSidedWithFutility = "2-sided with futility";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is TestType == false)
			{
				throw new NotImplementedException();
			}

			if (targetType.Equals(typeof (object)))
			{
				var testType = (TestType)value;
				switch (testType)
				{
					case TestType.OneSided:
						return OneSided;

					case TestType.TwoSidedSymmetric:
						return TwoSidedSymmetric;

					case TestType.TwoSidedWithFutility:
						return TwoSidedWithFutility;
				}

				return testType.ToString();
			}

			throw new NotImplementedException("Unhandled targetType: " + targetType);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			switch (value.ToString())
			{
				case OneSided:
					return TestType.OneSided;

				case TwoSidedSymmetric:
					return TestType.TwoSidedSymmetric;

				case TwoSidedWithFutility:
					return TestType.TwoSidedWithFutility;
			}

			return (TestType)Enum.Parse(typeof(TestType), value.ToString(), true);
		}
	}
}