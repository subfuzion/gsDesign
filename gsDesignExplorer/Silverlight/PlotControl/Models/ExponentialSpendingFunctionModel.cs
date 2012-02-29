namespace Subfuzion.Silverlight.UI.Charting.Models
{
	public class ExponentialSpendingFunctionModel : OneParameterSpendingFunction
	{
		public ExponentialSpendingFunctionModel()
		{
			SpendingFunction = OneParameterSpendingFunctions.ExponentialFunction;
			InverseSpendingFunction = OneParameterSpendingFunctions.ExponentialFunctionInverse;
			ParameterSpendingFunction = OneParameterSpendingFunctions.ExponentialFunctionSpendingParameter;

			SpendingFunctionParameterMaximum = 1.5;
			SpendingFunctionParameterMinimum = 0.001;
			SpendingFunctionParameter = 0.75;

			InterimSpendingParameterMaximum = 0.025;
			InterimSpendingParameterMinimum = 0.001;
			InterimSpendingParameter = 0.024;

			TimingMaximum = 1.0;
			TimingMinimum = 0.0;
			Timing = 0.5;
		}
	}
}
