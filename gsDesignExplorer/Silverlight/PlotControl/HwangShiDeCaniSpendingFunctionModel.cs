namespace Subfuzion.Silverlight.UI.Charting
{
	public class HwangShiDeCaniSpendingFunctionModel : OneParameterSpendingFunction
	{
		public HwangShiDeCaniSpendingFunctionModel()
		{
			SpendingFunction = OneParameterSpendingFunctions.HwangShihDeCaniFunction;
			InverseSpendingFunction = OneParameterSpendingFunctions.HwangShihDeCaniFunctionInverse;
			ParameterSpendingFunction = OneParameterSpendingFunctions.HwangShihDeCaniFunctionSpendingParameter;
	
			SpendingFunctionParameterMaximum = 40.0;
			SpendingFunctionParameterMinimum = -40.0;
			SpendingFunctionParameter = -8.0;

			InterimSpendingParameterMaximum = 0.025;
			InterimSpendingParameterMinimum = 0.001;
			InterimSpendingParameter = 0.024;

			TimingMaximum = 1.0;
			TimingMinimum = 0.0;
			Timing = 0.5;
		}
	}
}
