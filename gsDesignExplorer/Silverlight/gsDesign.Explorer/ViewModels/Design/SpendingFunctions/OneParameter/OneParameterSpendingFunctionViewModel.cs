namespace gsDesign.Explorer.ViewModels.Design.SpendingFunctions.OneParameter
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Globalization;
	using System.Windows;
	using Subfuzion.Helpers;
	using gsDesign.Design.SpendingFunctions.OneParameter;

	public class OneParameterSpendingFunctionViewModel : ViewModelBase
	{
		private readonly OneParameterSpendingFunction _oneParameterSpendingFunction;

		public OneParameterSpendingFunctionViewModel(OneParameterSpendingFunction oneParameterSpendingFunction)
		{
			_oneParameterSpendingFunction = oneParameterSpendingFunction;

			App.AppViewModel.CurrentDesign.ErrorPowerTiming.PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName == "Error")
				{
					UpdatePlotData();
				}
			};
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
					SuppressPlotDataNotifications = true;

					Model.OneParameterFamily = value;
					NotifyPropertyChanged("OneParameterFamily");
					NotifyPropertyChanged("SpendingFunctionValue");
					NotifyPropertyChanged("SpendingFunctionMinimum");
					NotifyPropertyChanged("SpendingFunctionMaximum");
					NotifyPropertyChanged("SpendingFunctionIncrement");
					NotifyPropertyChanged("SpendingFunctionPrecision");
					NotifyPropertyChanged("SpendingFunctionSymbol");

					SuppressPlotDataNotifications = false;
					UpdatePlotData();
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
							UpdatePlotData();
						}
						return;

					case OneParameterFamily.Power:
						if (Math.Abs(Model.Power - value) > double.Epsilon)
						{
							Model.Power = value;
							NotifyPropertyChanged("SpendingFunctionValue");
							UpdatePlotData();
						}
						return;

					case OneParameterFamily.Exponential:
						if (Math.Abs(Model.Exponential - value) > double.Epsilon)
						{
							Model.Exponential = value;
							NotifyPropertyChanged("SpendingFunctionValue");
							UpdatePlotData();
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

		public string SpendingFunctionSymbol
		{
			get
			{
				switch (OneParameterFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return Symbols.Gamma;

					case OneParameterFamily.Power:
						return Symbols.Rho;

					case OneParameterFamily.Exponential:
						return Symbols.Nu;

					default:
						throw new Exception(string.Format("Unsupported enum value for OneParameterFamily: {0}", OneParameterFamily));
				}
			}
		}

		#endregion // Spending Function

		#region Public Properties

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
					UpdatePlotData();
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
			get { return 0.01; }
		}

		public int TimingPrecision
		{
			get { return 3; }
		}

		#endregion

		#region PlotData property

		private List<PlotItem> _plotData;

		public List<PlotItem> PlotData
		{
			get
			{
				if (_plotData == null)
				{
					InitializePlotData();
				}

				return _plotData;
			}

			set
			{
				if (_plotData != value)
				{
					_plotData = value;
					NotifyPropertyChanged("PlotData");
				}
			}
		}

		#endregion // PlotData

		#region PlotFunction property

		private void InitializePlotData()
		{
			var data = new List<PlotItem>();

			for (int i = 0; i < 20; i++)
			{
				data.Add(new PlotItem
				{
					X = i.ToString(CultureInfo.InvariantCulture),
					Y = 0,
				});
			}

			PlotData = data;

			UpdatePlotData();
		}

		private void UpdatePlotData()
		{
			var func = PlotFunction;
			var sfValue = SpendingFunctionValue;
			var alpha = Model.AlphaSpending;

			for (var i = 0; i < PlotData.Count; i++)
			{
				var t = ((double) i)/(PlotData.Count - 1);

				var item = PlotData[i];
				item.X = t.ToString(CultureInfo.InvariantCulture);
				item.Y = func(alpha, t, sfValue);
			}

			NotifyPlotDataChanged();
		}

		private Func<double, double, double, double> PlotFunction
		{
			get
			{
				switch (OneParameterFamily)
				{
					case OneParameterFamily.HwangShihDeCani:
						return HwangShihDeCaniFunction;

					case OneParameterFamily.Power:
						return PowerFunction;

					case OneParameterFamily.Exponential:
						return ExponentialFunction;

					default:
						throw new Exception(string.Format("Unsupported enum value for OneParameterFamily: {0}", OneParameterFamily));
				}
			}
		}

		private double HwangShihDeCaniFunction(double alpha, double timing, double sfValue)
		{
			return alpha * (1 - Math.Exp(-sfValue * timing)) / (1 - Math.Exp(-sfValue));
		}

		private double PowerFunction(double alpha, double timing, double sfValue)
		{
			return alpha * Math.Pow(timing, sfValue);
		}

		private double ExponentialFunction(double alpha, double timing, double sfValue)
		{
			return Math.Pow(alpha, (Math.Pow(timing, -sfValue)));
		}

		private double GetY(int step)
		{
			double x = step * ((double)step / PlotData.Count);
			return 0;

		}

		private bool SuppressPlotDataNotifications { get; set; }

		private void NotifyPlotDataChanged()
		{
			if (!SuppressPlotDataNotifications)
			{
				NotifyPropertyChanged("PlotData");
				NotifyPropertyChanged("Intercept");
			}
		}
		#endregion // PlotFunction

		#region Intercept property

		private Point _intercept;

		public Point Intercept
		{
			get
			{
				var func = PlotFunction;
				var sfValue = SpendingFunctionValue;
				var alpha = Model.AlphaSpending;

				var x = Timing;
				var y = func(alpha, x, sfValue);

				_intercept.X = x;
				_intercept.Y = y;

				return _intercept;
			}

			set
			{
				if (_intercept != value)
				{
					_intercept = value;
					NotifyPropertyChanged("Intercept");
				}
			}
		}

		#endregion // Intercept

	}

	public class PlotItem : ViewModelBase
	{
		private string _x;
		public string X
		{
			get { return _x; }
			set
			{
				_x = value;
				NotifyPropertyChanged("X");
			}
		}

		private double _y;
		public double Y
		{
			get { return _y; }
			set
			{
				_y = value;
				NotifyPropertyChanged("Y");
			}
		}
	}

}