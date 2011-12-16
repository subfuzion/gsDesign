namespace gsDesign.Explorer.Models.Design.SpendingFunctions
{
	public class SpendingFunctionParameters
	{
		#region SpendingFunctionTestingParameters property

		private SpendingFunctionTestingParameters _spendingFunctionTestingParameters;

		public SpendingFunctionTestingParameters SpendingFunctionTestingParameters
		{
			get
			{
				return _spendingFunctionTestingParameters
				       ?? (_spendingFunctionTestingParameters = new SpendingFunctionTestingParameters
																{
																	SpendingFunctionTestType =
																	SpendingFunctionTestType.TwoSidedWithFutility
																});
			}

			set { _spendingFunctionTestingParameters = value; }
		}

		#endregion // SpendingFunctionTestingParameters

		#region UpperSpendingFunction property

		private SpendingFunction _upperSpendingFunction;

		public SpendingFunction UpperSpendingFunction
		{
			get { return _upperSpendingFunction ?? (_upperSpendingFunction = new SpendingFunction()); }

			set { _upperSpendingFunction = value; }
		}

		#endregion // UpperSpendingFunction

		#region LowerSpendingFunction property

		private SpendingFunction _lowerSpendingFunction;

		public SpendingFunction LowerSpendingFunction
		{
			get { return _lowerSpendingFunction ?? (_lowerSpendingFunction = new SpendingFunction()); }

			set { _lowerSpendingFunction = value; }
		}

		#endregion // LowerSpendingFunction
	}
}