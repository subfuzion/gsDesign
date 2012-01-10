namespace gsDesign.Explorer.ViewModels.Design.SpendingFunctions.OneParameter
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using gsDesign.Design.SpendingFunctions.OneParameter;

	public class OneParameterSpendingFunctionViewModel : ViewModelBase
	{
		private readonly OneParameterSpendingFunction _oneParameterSpendingFunction;

		public OneParameterSpendingFunctionViewModel(OneParameterSpendingFunction oneParameterSpendingFunction)
		{
			_oneParameterSpendingFunction = oneParameterSpendingFunction;
		}

		private OneParameterSpendingFunction Model
		{
			get { return _oneParameterSpendingFunction; }
		}

		#region Spending Function

		#region OneParameterFamily property

		[Display(Name = "One Parameter Spending Function Family",
			Description = "Select Hwang-Shih-DeCani, Power, or Exponential design")]
		public OneParameterFamily OneParameterFamily
		{
			get { return Model.OneParameterFamily; }

			set
			{
				if (Model.OneParameterFamily != value)
				{
					Model.OneParameterFamily = value;
					NotifyPropertyChanged("OneParameterFamily");
					NotifyPropertyChanged("SpendingFunctionMinimum");
					NotifyPropertyChanged("SpendingFunctionMaximum");
					NotifyPropertyChanged("SpendingFunctionIncrement");
					NotifyPropertyChanged("SpendingFunctionPrecision");
				}
			}
		}

		#endregion // OneParameterFamily

		#region SpendingFunctionValue property

		[Display(Name = "One Parameter Spending Function",
			Description = "One Parameter Spending Function value")]
		public double SpendingFunctionValue
		{
			get
			{
				switch (OneParameterFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return Model.HwangShihDeCani;

					case OneParameterFamily.Power:
						return Model.Power;

					case OneParameterFamily.Exponential:
						return Model.Exponential;

					default:
						throw new Exception(string.Format("Unsupported enum value for OneParameterFamily: {0}", OneParameterFamily));
				}
			}

			set
			{
				switch (OneParameterFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						if (Math.Abs(Model.HwangShihDeCani - value) > double.Epsilon)
						{
							Model.HwangShihDeCani = value;
							NotifyPropertyChanged("SpendingFunctionValue");
						}
						return;

					case OneParameterFamily.Power:
						if (Math.Abs(Model.Power - value) > double.Epsilon)
						{
							Model.Power = value;
							NotifyPropertyChanged("SpendingFunctionValue");
						}
						return;

					case OneParameterFamily.Exponential:
						if (Math.Abs(Model.Exponential - value) > double.Epsilon)
						{
							Model.Exponential = value;
							NotifyPropertyChanged("SpendingFunctionValue");
						}
						return;

					default:
						throw new Exception(string.Format("Unsupported enum value for OneParameterFamily: {0}", OneParameterFamily));
				}
			}
		}

		#endregion // SpendingFunctionValue

		public double SpendingFunctionMinimum
		{
			get
			{
				switch (OneParameterFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return -40.0;

					case OneParameterFamily.Power:
						return 0.001;

					case OneParameterFamily.Exponential:
						return 0.001;

					default:
						throw new Exception(string.Format("Unsupported enum value for OneParameterFamily: {0}", OneParameterFamily));
				}
			}
		}

		public double SpendingFunctionMaximum
		{
			get
			{
				switch (OneParameterFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return 40.0;

					case OneParameterFamily.Power:
						return 15.0;

					case OneParameterFamily.Exponential:
						return 1.5;

					default:
						throw new Exception(string.Format("Unsupported enum value for OneParameterFamily: {0}", OneParameterFamily));
				}
			}
		}

		public double SpendingFunctionIncrement
		{
			get
			{
				switch (OneParameterFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return 1.0;

					case OneParameterFamily.Power:
						return 1.0;

					case OneParameterFamily.Exponential:
						return 0.1;

					default:
						throw new Exception(string.Format("Unsupported enum value for OneParameterFamily: {0}", OneParameterFamily));
				}
			}
		}

		public int SpendingFunctionPrecision
		{
			get
			{
				switch (OneParameterFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return 1;

					case OneParameterFamily.Power:
						return 3;

					case OneParameterFamily.Exponential:
						return 3;

					default:
						throw new Exception(string.Format("Unsupported enum value for OneParameterFamily: {0}", OneParameterFamily));
				}
			}
		}

		#endregion // Spending Function

		#region Timing property

		 [Display(Name = "Timing",
			Description = "Timing value")]
		public double Timing
		{
			get { return Model.Timing; }

			set
			{
				if (Math.Abs(Model.Timing - value) > double.Epsilon)
				{
					Model.Timing = value;
					NotifyPropertyChanged("Timing");
				}
			}
		}

		#endregion // Timing

		public double TimingMinimum
		{
			get { return 0.001; }
		}

		public double TimingMaximum
		{
			get { return 0.999; }
		}

		public double TimingIncrement
		{
			get { return 0.1; }
		}

		public int TimingPrecision
		{
			get { return 3; }
		}
	}
}