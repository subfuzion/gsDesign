namespace gsDesign.Design.SpendingFunctions
{
	using OneParameter;
	using ParameterFree;

	public class SpendingFunction
	{
		public SpendingFunction(DesignParameters designParameters)
		{
			DesignParameters = designParameters;
		}

		protected DesignParameters DesignParameters { get; private set; }

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
					?? (_parameterFreeSpendingFunction = new ParameterFreeSpendingFunction(DesignParameters)
					{
						LanDeMetsApproximation = LanDeMetsApproximation.OBrienFleming
					});
			}

			set { _parameterFreeSpendingFunction = value; }
		}

		#endregion // ParameterFreeSpendingFunction

		#region OneParameterSpendingFunction property

		private OneParameterSpendingFunction _oneParameterSpendingFunction;

		public OneParameterSpendingFunction OneParameterSpendingFunction
		{
			get
			{
				return _oneParameterSpendingFunction
					?? (_oneParameterSpendingFunction = new OneParameterSpendingFunction(DesignParameters)
					{
						OneParameterFamily = OneParameterFamily.HwangShihDeCani,
					});
			}

			set { _oneParameterSpendingFunction = value; }
		}

		#endregion // OneParameterSpendingFunction

		#endregion
	}
}