namespace gsDesign.Explorer.ViewModels.Design.SpendingFunctions
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Windows.Data;
	using Models.Design.SpendingFunctions;

	public class SpendingFunctionLowerBoundTestingValueConverter : IValueConverter
	{
		private const string Binding = "Binding";
		private const string NonBinding = "Non-binding";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			if (!value.GetType().Equals(typeof(SpendingFunctionLowerBoundTesting))) throw new ArgumentException();

			if (targetType.Equals(typeof(int))) return (int)(SpendingFunctionLowerBoundTesting)value;

			if (targetType.Equals(typeof(object)) || targetType.Equals(typeof(string)))
			{
				var s = (SpendingFunctionLowerBoundTesting)value;
				switch (s)
				{
					case SpendingFunctionLowerBoundTesting.Binding:
						return Binding;

					case SpendingFunctionLowerBoundTesting.NonBinding:
						return NonBinding;

					default:
						return s.ToString();
				}
			}

			if (targetType.Equals(typeof(IEnumerable)))
			{
				var values = new List<string>
				             {
				             	Binding,
				             	NonBinding,
				             };

				return values;
			}

			throw new NotImplementedException();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			if (!targetType.Equals(typeof(SpendingFunctionLowerBoundTesting))) throw new ArgumentException();

			var valueType = value.GetType();

			if (valueType.Equals(typeof(int))) return (SpendingFunctionLowerBoundTesting)value;

			if (valueType.Equals(typeof(string)) || valueType.Equals(typeof(object)))
			{
				var s = (string)value;

				switch (s)
				{
					case Binding:
						return SpendingFunctionLowerBoundTesting.Binding;

					case NonBinding:
						return SpendingFunctionLowerBoundTesting.NonBinding;

					default:
						return
							(SpendingFunctionLowerBoundTesting)
							Enum.Parse(typeof(SpendingFunctionLowerBoundTesting), (string)value, true);
				}
			}

			throw new NotImplementedException();
		}
	}
}
