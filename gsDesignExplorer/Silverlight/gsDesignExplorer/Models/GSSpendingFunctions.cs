namespace gsDesign.Explorer.Models
{
	public class GSSpendingFunctions
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

		#region SpendingFunctionType property

		private SpendingFunctionType _spendingFunctionType;

		public SpendingFunctionType SpendingFunctionType
		{
			get { return _spendingFunctionType; }

			set
			{
				_spendingFunctionType = value;
			}
		}

		#endregion // SpendingFunctionType

		#region SpendingFunctionTestTypeCode property

		public int SpendingFunctionTestTypeCode
		{
			get
			{
				if (SpendingFunctionTestType == SpendingFunctionTestType.OneSided)
					return 1;

				if (SpendingFunctionTestType == SpendingFunctionTestType.TwoSidedSymmetric)
					return 2;

				if (SpendingFunctionTestType == SpendingFunctionTestType.TwoSidedWithFutility)
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

		#region SpendingFunctionTestType property

		private SpendingFunctionTestType _spendingFunctionTestType = SpendingFunctionTestType.TwoSidedWithFutility;

		public SpendingFunctionTestType SpendingFunctionTestType
		{
			get { return _spendingFunctionTestType; }

			set
			{
				_spendingFunctionTestType = value;
			}
		}

		#endregion // SpendingFunctionTestType

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
