namespace gsDesign.Explorer.Models.Design.SpendingFunctions
{
	public class SpendingFunctionParameters
	{
		#region SpendingFunctionBounds property

		private SpendingFunctionBounds _spendingFunctionBounds;

		public SpendingFunctionBounds SpendingFunctionBounds
		{
			get { return _spendingFunctionBounds; }

			set
			{
				_spendingFunctionBounds = value;
			}
		}

		#endregion // SpendingFunctionBounds

		#region SpendingFunctionCategory property

		private SpendingFunctionCategory _spendingFunctionCategory;

		public SpendingFunctionCategory SpendingFunctionCategory
		{
			get { return _spendingFunctionCategory; }

			set
			{
				_spendingFunctionCategory = value;
			}
		}

		#endregion // SpendingFunctionCategory

		#region SpendingFunctionTestTypeCode property

		public int SpendingFunctionTestTypeCode
		{
			get
			{
				if (SpendingFunctionTestCategory == SpendingFunctionTestCategory.OneSided)
					return 1;

				if (SpendingFunctionTestCategory == SpendingFunctionTestCategory.TwoSidedSymmetric)
					return 2;

				if (SpendingFunctionTestCategory == SpendingFunctionTestCategory.TwoSidedWithFutility)
				{
					if (SpendingFunctionLowerBoundSpending == SpendingFunctionLowerBoundSpending.BetaSpending)
					{
						if (SpendingFunctionLowerBoundTesting == SpendingFunctionLowerBoundTesting.Binding)
							return 3;

						if (SpendingFunctionLowerBoundTesting == SpendingFunctionLowerBoundTesting.NonBinding)
							return 4;
					}

					if (SpendingFunctionLowerBoundSpending == SpendingFunctionLowerBoundSpending.H0Spending)
					{
						if (SpendingFunctionLowerBoundTesting == SpendingFunctionLowerBoundTesting.Binding)
							return 5;

						if (SpendingFunctionLowerBoundTesting == SpendingFunctionLowerBoundTesting.NonBinding)
							return 6;
					}
				}

				return -1;
			}
		}

		#endregion // SpendingFunctionTestTypeCode

		#region SpendingFunctionTestCategory property

		private SpendingFunctionTestCategory _spendingFunctionTestCategory = SpendingFunctionTestCategory.TwoSidedWithFutility;

		public SpendingFunctionTestCategory SpendingFunctionTestCategory
		{
			get { return _spendingFunctionTestCategory; }

			set
			{
				_spendingFunctionTestCategory = value;
			}
		}

		#endregion // SpendingFunctionTestCategory

		#region SpendingFunctionLowerBoundSpending property

		private SpendingFunctionLowerBoundSpending _spendingFunctionLowerBoundSpending = SpendingFunctionLowerBoundSpending.BetaSpending;

		public SpendingFunctionLowerBoundSpending SpendingFunctionLowerBoundSpending
		{
			get { return _spendingFunctionLowerBoundSpending; }

			set
			{
				_spendingFunctionLowerBoundSpending = value;
			}
		}

		#endregion // SpendingFunctionLowerBoundSpending

		#region SpendingFunctionLowerBoundTesting property

		private SpendingFunctionLowerBoundTesting _spendingFunctionLowerBoundTesting = SpendingFunctionLowerBoundTesting.NonBinding;

		public SpendingFunctionLowerBoundTesting SpendingFunctionLowerBoundTesting
		{
			get { return _spendingFunctionLowerBoundTesting; }

			set
			{
				_spendingFunctionLowerBoundTesting = value;
			}
		}

		#endregion // SpendingFunctionLowerBoundTesting

	}
}
