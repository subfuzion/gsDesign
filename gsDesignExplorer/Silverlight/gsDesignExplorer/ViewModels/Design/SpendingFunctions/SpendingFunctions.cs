namespace gsDesign.Explorer.ViewModels.Design.SpendingFunctions
{
	using System.ComponentModel;
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
			get { return Model.CurrentSpendingFunctionBounds; }

			set
			{
				if (Model.CurrentSpendingFunctionBounds != value)
				{
					Model.CurrentSpendingFunctionBounds = value;
					RaisePropertyChanged("SpendingFunctionBounds");
				}
			}
		}

		#endregion // SpendingFunctionBounds

		#region IsLowerSpendingTabEnabled property

		public bool IsLowerSpendingTabEnabled
		{
			get { return UpperSpendingFunction.SpendingFunctionTestCategory == SpendingFunctionTestCategory.TwoSidedWithFutility; }
		}

		#endregion // IsLowerSpendingTabEnabled

		#region LowerSpendingFunction property

		private SpendingFunctionViewModel _lowerSpendingFunction;

		 [Display(Name = "Lower Spending",
			Description = "Lower spending function")]
		public SpendingFunctionViewModel LowerSpendingFunction
		{
			get { return _lowerSpendingFunction
				?? (_lowerSpendingFunction =
					new SpendingFunctionViewModel(Model.LowerSpendingFunction)
					{
						IsEnabledSpendingFunctionTestCategory = false
					}); }

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
					if (_upperSpendingFunction != null)
					{
						_upperSpendingFunction.PropertyChanged -= OnUpperSpendingFunctionPropertyChanged;
					}

					_upperSpendingFunction = value;
					_upperSpendingFunction.PropertyChanged += OnUpperSpendingFunctionPropertyChanged;

					RaisePropertyChanged("UpperSpendingFunction");
					RaisePropertyChanged("IsLowerSpendingTabEnabled");
				}
			}
		}

		#endregion // UpperSpendingFunction

		private void OnUpperSpendingFunctionPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName.Equals("SpendingFunctionTestCategory"))
			{
				// Need this to ensure lower spending tab is disabled unless test type is 2-sided with futility
				RaisePropertyChanged("IsLowerSpendingTabEnabled");
			}
		}
	}
}
