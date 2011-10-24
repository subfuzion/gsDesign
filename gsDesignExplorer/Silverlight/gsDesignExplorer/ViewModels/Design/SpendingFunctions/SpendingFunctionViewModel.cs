namespace gsDesign.Explorer.ViewModels.Design.SpendingFunctions
{
	using System.ComponentModel.DataAnnotations;
	using Models.Design.SpendingFunctions;

	public class SpendingFunctionViewModel : ViewModelBase
	{
		private SpendingFunction _spendingFunction;

		public SpendingFunctionViewModel(SpendingFunction spendingFunction)
		{
			_spendingFunction = spendingFunction;
		}

		public SpendingFunction Model { get { return _spendingFunction; } }

		#region Testing parameters

		#region SpendingFunctionTestCategory property

		[Display(Name = "Test Type",
		   Description = "Lower spending test type")]
		public SpendingFunctionTestCategory SpendingFunctionTestCategory
		{
			get { return Model.SpendingFunctionTestingParameters.SpendingFunctionTestCategory; }

			set
			{
				if (Model.SpendingFunctionTestingParameters.SpendingFunctionTestCategory != value)
				{
					Model.SpendingFunctionTestingParameters.SpendingFunctionTestCategory = value;
					RaisePropertyChanged("SpendingFunctionTestCategory");
				}
			}
		}

		#endregion // SpendingFunctionTestCategory

		#region SpendingFunctionLowerBoundSpending property

		[Display(Name = "Lower Bound Spending",
		   Description = "Lower bound spending for 2-sided futility test type")]
		public SpendingFunctionLowerBoundSpending SpendingFunctionLowerBoundSpending
		{
			get { return Model.SpendingFunctionTestingParameters.SpendingFunctionLowerBoundSpending; }

			set
			{
				if (Model.SpendingFunctionTestingParameters.SpendingFunctionLowerBoundSpending != value)
				{
					Model.SpendingFunctionTestingParameters.SpendingFunctionLowerBoundSpending = value;
					RaisePropertyChanged("SpendingFunctionLowerBoundSpending");
				}
			}
		}

		#endregion // SpendingFunctionLowerBoundSpending

		#region SpendingFunctionLowerBoundTesting property

		[Display(Name = "Lower Bound Testing",
		   Description = "Binding or non-binding testing")]
		public SpendingFunctionLowerBoundTesting SpendingFunctionLowerBoundTesting
		{
			get { return Model.SpendingFunctionTestingParameters.SpendingFunctionLowerBoundTesting; }

			set
			{
				if (Model.SpendingFunctionTestingParameters.SpendingFunctionLowerBoundTesting != value)
				{
					Model.SpendingFunctionTestingParameters.SpendingFunctionLowerBoundTesting = value;
					RaisePropertyChanged("SpendingFunctionLowerBoundTesting");
				}
			}
		}

		#endregion // SpendingFunctionLowerBoundTesting

		#endregion // Testing parameters


	}
}
