namespace gsDesign.Design.SpendingFunctions.ParameterFree
{
	/// <summary>
	/// An instance of this class is a property of SpendingFunction (which is either the
	/// LowerSpendingFunction or UpperSpendingFunction property of SpendingFunctionParameters)
	/// </summary>
	public class ParameterFreeSpendingFunction : ParameterSpendingFunctionBase
	{
		public ParameterFreeSpendingFunction(DesignParameters designParameters, SpendingFunctionBounds bounds) : base(designParameters, bounds)
		{
		}

		public LanDeMetsApproximation LanDeMetsApproximation { get; set; }
	}
}
