namespace gsDesign.Design.SpendingFunctions.ThreeParameter
{
	/// <summary>
	/// An instance of this class is a property of SpendingFunction (which is either the
	/// LowerSpendingFunction or UpperSpendingFunction property of SpendingFunctionParameters)
	/// </summary>
	public class ThreeParameterSpendingFunction : ParameterSpendingFunctionBase
	{
		public ThreeParameterSpendingFunction(DesignParameters designParameters, SpendingFunctionBounds bounds) : base(designParameters, bounds)
		{
		}
	}
}