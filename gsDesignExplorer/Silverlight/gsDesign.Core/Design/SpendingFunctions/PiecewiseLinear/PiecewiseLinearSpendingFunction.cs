namespace gsDesign.Design.SpendingFunctions.PiecewiseLinear
{
	/// <summary>
	/// An instance of this class is a property of SpendingFunction (which is either the
	/// LowerSpendingFunction or UpperSpendingFunction property of SpendingFunctionParameters)
	/// </summary>
	public class PiecewiseLinearSpendingFunction : ParameterSpendingFunctionBase
	{
		public PiecewiseLinearSpendingFunction(DesignParameters designParameters, SpendingFunctionBounds bounds) : base(designParameters, bounds)
		{
		}
	}
}