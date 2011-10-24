namespace gsDesign.Explorer.ViewModels.Design.SpendingFunctions
{
	using System.ComponentModel.DataAnnotations;
	using Models.Design.SpendingFunctions;
	using ParameterFree;

	public class SpendingFunctionViewModel : ViewModelBase
	{
		private SpendingFunction _spendingFunction;

		public SpendingFunctionViewModel(SpendingFunction spendingFunction)
		{
			_spendingFunction = spendingFunction;
		}

		public SpendingFunction Model { get { return _spendingFunction; } }

		#region Testing parameters

		#region IsEnabledSpendingFunctionTestCategory property

		private bool _isEnabledSpendingFunctionTestCategory = true;

		public bool IsEnabledSpendingFunctionTestCategory
		{
			get { return _isEnabledSpendingFunctionTestCategory; }

			set
			{
				if (_isEnabledSpendingFunctionTestCategory != value)
				{
					_isEnabledSpendingFunctionTestCategory = value;
					RaisePropertyChanged("IsEnabledSpendingFunctionTestCategory");
				}
			}
		}

		#endregion // IsEnabledSpendingFunctionTestCategory

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

		#region Spending function parameters

		#region SpendingFunctionParameterCategory property

		public SpendingFunctionParameterCategory SpendingFunctionParameterCategory
		{
			get { return Model.SpendingFunctionParameterCategory; }

			set
			{
				if (Model.SpendingFunctionParameterCategory != value)
				{
					Model.SpendingFunctionParameterCategory = value;
					RaisePropertyChanged("SpendingFunctionParameterCategory");
				}
			}
		}

		#endregion // SpendingFunctionParameterCategory

		#region ParameterFreeSpendingFunctionViewModel property

		private ParameterFreeSpendingFunctionViewModel _parameterFreeSpendingFunctionSpendingViewModel;

		public ParameterFreeSpendingFunctionViewModel ParameterFreeSpendingFunctionViewModel
		{
			get { return _parameterFreeSpendingFunctionSpendingViewModel
				?? (ParameterFreeSpendingFunctionViewModel = new ParameterFreeSpendingFunctionViewModel(Model.ParameterFreeSpendingFunction)); }

			set
			{
				if (_parameterFreeSpendingFunctionSpendingViewModel != value)
				{
					_parameterFreeSpendingFunctionSpendingViewModel = value;
					RaisePropertyChanged("ParameterFreeSpendingFunctionViewModel");
				}
			}
		}

		#endregion // ParameterFreeSpendingFunctionViewModel

		#endregion
	}
}
