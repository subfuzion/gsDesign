namespace gsDesign.Design.SpendingFunctions
{
	/// <summary>
	/// Base class for X-ParameterSpendingFunctions; instances are the properties of the
	/// SpendingFunction class. Instances of the SpendingFunction class are the
	/// LowerSpendingFunction and UpperSpendingFunction properties of the
	/// SpendingFunctionParameters class.
	/// </summary>
	public class ParameterSpendingFunctionBase
	{
		public ParameterSpendingFunctionBase(DesignParameters designParameters, SpendingFunctionBounds bounds)
		{
			DesignParameters = designParameters;
			SpendingFunctionBounds = bounds;
		}

		public DesignParameters DesignParameters { get; private set; }

		public SpendingFunctionBounds SpendingFunctionBounds { get; set; }
	}
}