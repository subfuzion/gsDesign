namespace gsDesign.Explorer.ViewModels.Design.SpendingFunctions
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

		private SpendingFunctionBounds _spendingFunctionBounds = SpendingFunctionBounds.UpperSpending;

		public SpendingFunctionBounds SpendingFunctionBounds
		{
			get { return _spendingFunctionBounds; }

			set
			{
				if (_spendingFunctionBounds != value)
				{
					_spendingFunctionBounds = value;
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
				?? (_upperSpendingFunction = new SpendingFunctionViewModel(Model.UpperSpendingFunction));
			}

			set
			{
				if (_upperSpendingFunction != value)
				{
					_upperSpendingFunction = value;
					RaisePropertyChanged("UpperSpendingFunction");
				}
			}
		}

		#endregion // UpperSpendingFunction

	}
}
