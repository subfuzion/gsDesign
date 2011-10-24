namespace gsDesign.Explorer.Models.Design.SpendingFunctions
{
	public class SpendingFunctionParameters
	{
		#region CurrentSpendingFunctionBounds property

		private SpendingFunctionBounds _currentSpendingFunctionBounds = SpendingFunctionBounds.UpperSpending;

		public SpendingFunctionBounds CurrentSpendingFunctionBounds
		{
			get { return _currentSpendingFunctionBounds; }

			set
			{
				_currentSpendingFunctionBounds = value;
			}
		}

		#endregion // CurrentSpendingFunctionBounds

		#region LowerSpendingFunction property

		private SpendingFunction _lowerSpendingFunction;

		public SpendingFunction LowerSpendingFunction
		{
			get
			{
				return _lowerSpendingFunction ?? (_lowerSpendingFunction = new SpendingFunction
				{
					SpendingFunctionTestingParameters =
					{
						SpendingFunctionTestCategory = SpendingFunctionTestCategory.TwoSidedWithFutility
					}
				});
			}

			set { _lowerSpendingFunction = value; }
		}

		#endregion // LowerSpendingFunction

		#region UpperSpendingFunction property

		private SpendingFunction _upperSpendingFunction;

		public SpendingFunction UpperSpendingFunction
		{
			get
			{
				return _upperSpendingFunction
				       ?? (_upperSpendingFunction = new SpendingFunction());
			}

			set { _upperSpendingFunction = value; }
		}

		#endregion // UpperSpendingFunction
	}
}