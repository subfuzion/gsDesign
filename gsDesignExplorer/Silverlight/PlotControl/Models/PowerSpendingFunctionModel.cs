namespace Subfuzion.Silverlight.UI.Charting.Models
{
	public class PowerSpendingFunctionModel : OneParameterSpendingFunction
	{
		public PowerSpendingFunctionModel()
		{
			SpendingFunction = OneParameterSpendingFunctions.PowerFunction;
			InverseSpendingFunction = OneParameterSpendingFunctions.PowerFunctionInverse;
			ParameterSpendingFunction = OneParameterSpendingFunctions.PowerFunctionSpendingParameter;

			SpendingFunctionParameterMaximum = 15.0;
			SpendingFunctionParameterMinimum = 0.001;
			SpendingFunctionParameter = 4;

			InterimSpendingParameterMaximum = 0.025;
			InterimSpendingParameterMinimum = 0.001;
			InterimSpendingParameter = 0.024;

			TimingMaximum = 1.0;
			TimingMinimum = 0.0;
			Timing = 0.5;
		}
	}
}
