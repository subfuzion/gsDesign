namespace gsDesign.Explorer.ViewModels.Design
{
	using System.ComponentModel.DataAnnotations;
	using Models;
	using Models.Design.SpendingFunctions;
	using Subfuzion.Helpers;

	public class SpendingFunctions : NotifyPropertyChangedBase
	{
		private DesignParameters _designParameters;

		public SpendingFunctions(DesignParameters designParameters)
		{
			_designParameters = designParameters;
		}

		private SpendingFunctionParameters Model
		{
			get { return _designParameters.SpendingFunctionParameters; }
		}

		#region SpendingFunctionBounds property

		public SpendingFunctionBounds SpendingFunctionBounds
		{
			get { return Model.SpendingFunctionBounds; }

			set
			{
				if (Model.SpendingFunctionBounds != value)
				{
					Model.SpendingFunctionBounds = value;
					RaisePropertyChanged("SpendingFunctionBounds");
				}
			}
		}

		#endregion // SpendingFunctionBounds

		#region SpendingFunctionCategory property

		private SpendingFunctionCategory _spendingFunctionCategory;

		public SpendingFunctionCategory SpendingFunctionCategory
		{
			get { return Model.SpendingFunctionCategory; }

			set
			{
				if (Model.SpendingFunctionCategory != value)
				{
					Model.SpendingFunctionCategory = value;
					RaisePropertyChanged("SpendingFunctionCategory");
				}
			}
		}

		#endregion // SpendingFunctionCategory

		#region SpendingFunctionTestCategory property

		 [Display(Name = "Test Type",
			Description = "Select lower spending test type")]
		public SpendingFunctionTestCategory SpendingFunctionTestCategory
		{
			get { return Model.SpendingFunctionTestCategory; }

			set
			{
				if (Model.SpendingFunctionTestCategory != value)
				{
					Model.SpendingFunctionTestCategory = value;
					RaisePropertyChanged("SpendingFunctionTestCategory");
				}
			}
		}

		#endregion // SpendingFunctionTestCategory

		#region SpendingFunctionLowerBoundSpending property

		 [Display(Name = "Lower Bound Spending",
			Description = "Select lower bound spending for 2-sided futility test type")]
		public SpendingFunctionLowerBoundSpending SpendingFunctionLowerBoundSpending
		{
			get { return Model.SpendingFunctionLowerBoundSpending; }

			set
			{
				if (Model.SpendingFunctionLowerBoundSpending != value)
				{
					Model.SpendingFunctionLowerBoundSpending = value;
					RaisePropertyChanged("SpendingFunctionLowerBoundSpending");
				}
			}
		}

		#endregion // SpendingFunctionLowerBoundSpending

		#region SpendingFunctionLowerBoundTesting property

		 [Display(Name = "Lower Bound Testing",
			Description = "Select lower bound binding or non-binding testing")]
		public SpendingFunctionLowerBoundTesting SpendingFunctionLowerBoundTesting
		{
			get { return Model.SpendingFunctionLowerBoundTesting; }

			set
			{
				if (Model.SpendingFunctionLowerBoundTesting != value)
				{
					Model.SpendingFunctionLowerBoundTesting = value;
					RaisePropertyChanged("SpendingFunctionLowerBoundTesting");
				}
			}
		}

		#endregion // SpendingFunctionLowerBoundTesting




	}
}
