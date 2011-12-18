namespace gsDesign.Design.SpendingFunctions
{
	using ParameterFree;

	public class SpendingFunction
	{
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