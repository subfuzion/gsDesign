namespace gsDesign.Explorer.ViewModels.Design
{
	using System.ComponentModel.DataAnnotations;
	using Models;
	using Subfuzion.Helpers;

	public class SpendingFunctions : NotifyPropertyChangedBase
	{
		private GSDesign _design;

		public SpendingFunctions(GSDesign design)
		{
			_design = design;
		}

		private GSSpendingFunctions Model
		{
			get { return _design.SpendingFunctions; }
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

		#region SpendingFunctionType property

		private SpendingFunctionType _spendingFunctionType;

		public SpendingFunctionType SpendingFunctionType
		{
			get { return Model.SpendingFunctionType; }

			set
			{
				if (Model.SpendingFunctionType != value)
				{
					Model.SpendingFunctionType = value;
					RaisePropertyChanged("SpendingFunctionType");
				}
			}
		}

		#endregion // SpendingFunctionType

		#region SpendingFunctionTestType property

		 [Display(Name = "Test Type",
			Description = "Select lower spending test type")]
		public SpendingFunctionTestType SpendingFunctionTestType
		{
			get { return Model.SpendingFunctionTestType; }

			set
			{
				if (Model.SpendingFunctionTestType != value)
				{
					Model.SpendingFunctionTestType = value;
					RaisePropertyChanged("SpendingFunctionTestType");
				}
			}
		}

		#endregion // SpendingFunctionTestType

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
