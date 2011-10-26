namespace gsDesign.Explorer.ViewModels.Design.SpendingFunctions
{
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using Models;
	using Models.Design.SpendingFunctions;
	using Subfuzion.Helpers;

	public class SpendingFunctions : NotifyPropertyChangedBase
	{
		#region Initialization

		private DesignParameters _designParameters;

		public SpendingFunctions(DesignParameters designParameters)
		{
			_designParameters = designParameters;
		}

		private SpendingFunctionParameters Model
		{
			get { return _designParameters.SpendingFunctionParameters; }
		}

		#endregion

		#region Testing parameters

		#region SpendingFunctionTestType property

		[Display(Name = "Test Type",
		   Description = "Lower spending test type")]
		public SpendingFunctionTestType SpendingFunctionTestType
		{
			get { return Model.SpendingFunctionTestingParameters.SpendingFunctionTestType; }

			set
			{
				if (Model.SpendingFunctionTestingParameters.SpendingFunctionTestType != value)
				{
					Model.SpendingFunctionTestingParameters.SpendingFunctionTestType = value;
					RaisePropertyChanged("SpendingFunctionTestType");
					RaisePropertyChanged("IsLowerSpendingTabEnabled");
					RaisePropertyChanged("SpendingFunctionBounds");
				}
			}
		}

		#endregion // SpendingFunctionTestType

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

		#region IsLowerSpendingTabEnabled property

		public bool IsLowerSpendingTabEnabled
		{
			get { return Model.SpendingFunctionTestingParameters.SpendingFunctionTestType == SpendingFunctionTestType.TwoSidedWithFutility; }
		}

		#endregion // IsLowerSpendingTabEnabled

		#region SpendingFunctionBounds property

		private SpendingFunctionBounds _currentBounds = SpendingFunctionBounds.UpperSpending;

		public SpendingFunctionBounds SpendingFunctionBounds
		{
			get { return IsLowerSpendingTabEnabled ? _currentBounds : (_currentBounds = SpendingFunctionBounds.UpperSpending); }

			set
			{
				if (_currentBounds != value)
				{
					_currentBounds = value;
					RaisePropertyChanged("SpendingFunctionBounds");
				}
			}
		}

		#endregion // SpendingFunctionBounds

		#region LowerSpendingFunction property

		private SpendingFunctionViewModel _lowerSpendingFunction;

		 [Display(Name = "Lower Spending",
			Description = "Lower spending function")]
		public SpendingFunctionViewModel LowerSpendingFunction
		{
			get { return _lowerSpendingFunction
				?? (_lowerSpendingFunction = new SpendingFunctionViewModel(Model.LowerSpendingFunction)); }

			set
			{
				if (_lowerSpendingFunction != value)
				{
					_lowerSpendingFunction = value;
					RaisePropertyChanged("LowerSpendingFunction");
				}
			}
		}

		#endregion // LowerSpendingFunction

		#region UpperSpendingFunction property

		private SpendingFunctionViewModel _upperSpendingFunction;

		[Display(Name = "Uppper Spending",
		   Description = "Uppper spending function")]
		public SpendingFunctionViewModel UpperSpendingFunction
		{
			get { return _upperSpendingFunction
				?? (UpperSpendingFunction = new SpendingFunctionViewModel(Model.UpperSpendingFunction));
			}

			set
			{
				if (_upperSpendingFunction != value)
				{
					_upperSpendingFunction = value;
					RaisePropertyChanged("UpperSpendingFunction");
					RaisePropertyChanged("IsLowerSpendingTabEnabled");
				}
			}
		}

		#endregion // UpperSpendingFunction
	}
}
