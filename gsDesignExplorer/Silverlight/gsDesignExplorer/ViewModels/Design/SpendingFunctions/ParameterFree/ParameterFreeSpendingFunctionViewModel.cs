namespace gsDesign.Explorer.ViewModels.Design.SpendingFunctions.ParameterFree
{
	using System.ComponentModel.DataAnnotations;
	using Models.Design.SpendingFunctions.ParameterFree;

	public class ParameterFreeSpendingFunctionViewModel : ViewModelBase
	{
		private ParameterFreeSpendingFunction _parameterFreeSpendingFunction;

		public ParameterFreeSpendingFunctionViewModel(ParameterFreeSpendingFunction parameterFreeSpendingFunction)
		{
			_parameterFreeSpendingFunction = parameterFreeSpendingFunction;
		}

		ParameterFreeSpendingFunction Model { get { return _parameterFreeSpendingFunction; } }

		#region LanDeMetsApproximation property

		 [Display(Name = "Lan-DeMets Approximation",
			Description = "Select O'Brien Fleming or Pocock design")]
		public LanDeMetsApproximation LanDeMetsApproximation
		{
			get { return Model.LanDeMetsApproximation; }

			set
			{
				if (Model.LanDeMetsApproximation != value)
				{
					Model.LanDeMetsApproximation = value;
					RaisePropertyChanged("LanDeMetsApproximation");
				}
			}
		}

		#endregion // LanDeMetsApproximation

	}
}
