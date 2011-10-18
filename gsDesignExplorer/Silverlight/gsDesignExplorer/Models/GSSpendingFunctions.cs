namespace gsDesign.Explorer.Models
{
	public class GSSpendingFunctions
	{
		public GSSpendingFunctions()
		{
			TestType = TestType.TwoSidedWithFutility;
			LowerBoundSpending = LowerBoundSpending.BetaSpending;
			LowerBoundTesting = LowerBoundTesting.NonBinding;
		}

		#region TestTypeDefinition property

		public int TestTypeCode
		{
			get
			{
				if (TestType == TestType.OneSided)
					return 1;

				if (TestType == TestType.TwoSidedSymmetric)
					return 2;

				if (TestType == TestType.TwoSidedWithFutility)
				{
					if (LowerBoundSpending == LowerBoundSpending.BetaSpending)
					{
						if (LowerBoundTesting == LowerBoundTesting.Binding)
							return 3;

						if (LowerBoundTesting == LowerBoundTesting.NonBinding)
							return 4;
					}

					if (LowerBoundSpending == LowerBoundSpending.H0Spending)
					{
						if (LowerBoundTesting == LowerBoundTesting.Binding)
							return 5;

						if (LowerBoundTesting == LowerBoundTesting.NonBinding)
							return 6;
					}
				}

				return -1;
			}
		}

		#endregion // TestTypeDefinition

		#region TestType property

		private TestType _testType;

		public TestType TestType
		{
			get { return _testType; }

			set
			{
				_testType = value;
			}
		}

		#endregion // TestType

		#region LowerBoundSpending property

		private LowerBoundSpending _lowerBoundSpending;

		public LowerBoundSpending LowerBoundSpending
		{
			get { return _lowerBoundSpending; }

			set
			{
				_lowerBoundSpending = value;
			}
		}

		#endregion // LowerBoundSpending

		#region LowerBoundTesting property

		private LowerBoundTesting _lowerBoundTesting;

		public LowerBoundTesting LowerBoundTesting
		{
			get { return _lowerBoundTesting; }

			set
			{
				_lowerBoundTesting = value;
			}
		}

		#endregion // LowerBoundTesting

	}
}
