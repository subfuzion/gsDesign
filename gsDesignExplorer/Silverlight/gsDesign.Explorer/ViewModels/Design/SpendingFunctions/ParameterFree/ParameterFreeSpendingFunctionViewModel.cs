namespace gsDesign.Explorer.ViewModels.Design.SpendingFunctions.ParameterFree
{
	using System.ComponentModel.DataAnnotations;
	using gsDesign.Design.SpendingFunctions.ParameterFree;

	public class ParameterFreeSpendingFunctionViewModel : ViewModelBase
	{
		private readonly ParameterFreeSpendingFunction _parameterFreeSpendingFunction;

		public ParameterFreeSpendingFunctionViewModel(ParameterFreeSpendingFunction parameterFreeSpendingFunction)
		{
			_parameterFreeSpendingFunction = parameterFreeSpendingFunction;
		}

		private ParameterFreeSpendingFunction Model
		{
			get { return _parameterFreeSpendingFunction; }
		}

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
					NotifyPropertyChanged("LanDeMetsApproximation");
				}
			}
		}

		#endregion // LanDeMetsApproximation
	}
}