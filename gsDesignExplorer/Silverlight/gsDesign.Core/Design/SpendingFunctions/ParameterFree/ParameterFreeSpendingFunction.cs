namespace gsDesign.Design.SpendingFunctions.ParameterFree
{
	public class ParameterFreeSpendingFunction
	{
		public ParameterFreeSpendingFunction(DesignParameters designParameters)
		{
			DesignParameters = designParameters;
		}

		protected DesignParameters DesignParameters { get; private set; }

		public LanDeMetsApproximation LanDeMetsApproximation { get; set; }
	}
}
