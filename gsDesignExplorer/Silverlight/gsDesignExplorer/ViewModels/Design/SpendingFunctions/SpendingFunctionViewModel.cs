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
