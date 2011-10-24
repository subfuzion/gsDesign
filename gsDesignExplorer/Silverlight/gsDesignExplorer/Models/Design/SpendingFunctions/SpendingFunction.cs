namespace gsDesign.Explorer.Models.Design.SpendingFunctions
{
	using ParameterFree;

	public class SpendingFunction
	{
		#region SpendingFunctionTestingParameters property

		private SpendingFunctionTestingParameters _spendingFunctionTestingParameters;

		public SpendingFunctionTestingParameters SpendingFunctionTestingParameters
		{
			get
			{
				return _spendingFunctionTestingParameters
				       ?? (_spendingFunctionTestingParameters = new SpendingFunctionTestingParameters());
			}

			set { _spendingFunctionTestingParameters = value; }
		}

		#endregion // SpendingFunctionTestingParameters

		#region Spending function parameters

		#region SpendingFunctionParameterCategory property

		private SpendingFunctionParameterCategory _spendingFunctionParameterCategory =
			SpendingFunctionParameterCategory.ParameterFree;

		public SpendingFunctionParameterCategory SpendingFunctionParameterCategory
		{
			get { return _spendingFunctionParameterCategory; }

			set { _spendingFunctionParameterCategory = value; }
		}

		#endregion // SpendingFunctionParameterCategory

		#region ParameterFreeSpendingFunction property

		private ParameterFreeSpendingFunction _parameterFreeSpendingFunction;

		public ParameterFreeSpendingFunction ParameterFreeSpendingFunction
		{
			get
			{
				return _parameterFreeSpendingFunction
				       ?? (_parameterFreeSpendingFunction = new ParameterFreeSpendingFunction
				                                            {
				                                            	LanDeMetsApproximation = LanDeMetsApproximation.OBrienFleming
				                                            });
			}

			set { _parameterFreeSpendingFunction = value; }
		}

		#endregion // ParameterFreeSpendingFunction

		#endregion
	}
}