namespace Subfuzion.Silverlight.UI.Charting
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Windows;
	using gsDesign.Design.SpendingFunctions.OneParameter;

	public class SpendingFunctionViewModel : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged(string property)
		{
			var handlers = PropertyChanged;
			if (handlers != null)
			{
				handlers(this, new PropertyChangedEventArgs(property));
			}
		}

		#endregion

		private readonly Dictionary<OneParameterFamily, OneParameterSpendingFunction> _spendingFunctions = new Dictionary<OneParameterFamily, OneParameterSpendingFunction>(); 

		public SpendingFunctionViewModel()
		{
			var hsdsf = new HwangShiDeCaniSpendingFunctionModel();
			_spendingFunctions.Add(OneParameterFamily.HwangShihDeCani, hsdsf);

			var psf = new PowerSpendingFunctionModel();
			_spendingFunctions.Add(OneParameterFamily.Power, psf);

			var esf = new ExponentialSpendingFunctionModel();
			_spendingFunctions.Add(OneParameterFamily.Exponential, esf);
		}

		private void NotifyParameterUpdates()
		{
			NotifyPropertyChanged("SpendingFunctionParameterMaximum");
			NotifyPropertyChanged("SpendingFunctionParameterMinimum");
			NotifyPropertyChanged("SpendingFunctionParameter");

			NotifyPropertyChanged("InterimSpendingParameterMaximum");
			NotifyPropertyChanged("InterimSpendingParameterMinimum");
			NotifyPropertyChanged("InterimSpendingParameter");

			NotifyPropertyChanged("TimingMaximum");
			NotifyPropertyChanged("TimingMinimum");
			NotifyPropertyChanged("Timing");

			NotifyPropertyChanged("Coordinates");
		}

		#region CurrentSpendingFunction property

		private OneParameterFamily _currentSpendingFunctionFamily = OneParameterFamily.HwangShihDeCani;

		/// <summary>
		/// Gets or sets the CurrentSpendingFunctionFamily property.
		/// </summary>
		public OneParameterFamily CurrentSpendingFunctionFamily
		{
			get { return _currentSpendingFunctionFamily; }

			set
			{
				if (_currentSpendingFunctionFamily != value)
				{
					_currentSpendingFunctionFamily = value;

					foreach (var oneParameterSpendingFunction in _spendingFunctions.Values)
					{
						oneParameterSpendingFunction.Timing = TimingParameter;
						oneParameterSpendingFunction.InterimSpendingParameter = InterimSpendingParameter;
					}

					CurrentPlotFunction.Update();

					NotifyPropertyChanged("CurrentSpendingFunctionFamily");
					NotifyPropertyChanged("CurrentPlotFunction");
					NotifyParameterUpdates();
				}
			}
		}

		#endregion

		#region PlotConstraint property

		private PlotConstraint _plotConstraint = PlotConstraint.MoveLineWithPoint;

		/// <summary>
		/// Gets or sets the PlotConstraint property.
		/// </summary>
		public PlotConstraint PlotConstraint
		{
			get { return _plotConstraint; }

			set
			{
				if (_plotConstraint != value)
				{
					_plotConstraint = value;
					NotifyPropertyChanged("PlotConstraint");

					foreach (var oneParameterSpendingFunction in _spendingFunctions.Values)
					{
						oneParameterSpendingFunction.PlotConstraint = value;
					}
				}
			}
		}

		#endregion

		#region Alpha property

		private double _alpha = 0.025;

		/// <summary>
		/// Gets or sets the Alpha property.
		/// </summary>
		public double Alpha
		{
			get { return _alpha; }

			set
			{
				if (Math.Abs(_alpha - value) > double.Epsilon)
				{
					_alpha = value;
					NotifyPropertyChanged("Alpha");
				}
			}
		}

		#region AlphaMinimum property

		public double AlphaMinimum
		{
			get { return 0.0; }
		}

		#endregion

		#region AlphaMaximum property

		public double AlphaMaximum
		{
			get { return 1.0; }
		}

		#endregion


		#endregion

		#region CurrentPlotFunction property

		/// <summary>
		/// Gets or sets the CurrentPlotFunction property.
		/// </summary>
		public OneParameterSpendingFunction CurrentPlotFunction
		{
			get { return _spendingFunctions[CurrentSpendingFunctionFamily]; }
		}

		#endregion

		#region Coordinates property

		/// <summary>
		/// Gets or sets the Coordinates property.
		/// </summary>
		public ObservableCollection<Point> Coordinates
		{
			get { return CurrentPlotFunction.Coordinates; }
		}

		#endregion

		#region SpendingFunctionParameter property

		/// <summary>
		/// Gets or sets the SpendingFunctionParameter property.
		/// </summary>
		public double SpendingFunctionParameter
		{
			get { return CurrentPlotFunction.SpendingFunctionParameter; }

			set
			{
				if (Math.Abs(CurrentPlotFunction.SpendingFunctionParameter - value) > double.Epsilon)
				{
					CurrentPlotFunction.SpendingFunctionParameter = value;
					NotifyParameterUpdates();
					CurrentPlotFunction.Update();
				}
			}
		}

		public double SpendingFunctionParameterMinimum
		{
			get { return CurrentPlotFunction.SpendingFunctionParameterMinimum; }
		}

		public double SpendingFunctionParameterMaximum
		{
			get { return CurrentPlotFunction.SpendingFunctionParameterMaximum; }
		}

		#endregion

		#region InterimSpendingParameter property

		/// <summary>
		/// Gets or sets the InterimSpendingParameter property.
		/// </summary>
		public double InterimSpendingParameter
		{
			get { return CurrentPlotFunction.InterimSpendingParameter; }

			set
			{
				if (Math.Abs(CurrentPlotFunction.InterimSpendingParameter - value) > double.Epsilon)
				{
					CurrentPlotFunction.InterimSpendingParameter = value;
					NotifyParameterUpdates();
					CurrentPlotFunction.Update();
				}
			}
		}

		public double InterimSpendingParameterMinimum
		{
			get { return CurrentPlotFunction.InterimSpendingParameterMinimum; }
		}

		public double InterimSpendingParameterMaximum
		{
			get { return CurrentPlotFunction.InterimSpendingParameterMaximum; }
		}

		#endregion

		#region TimingParameter property

		/// <summary>
		/// Gets or sets the TimingParameter property.
		/// </summary>
		public double TimingParameter
		{
			get { return CurrentPlotFunction.Timing; }

			set
			{
				if (Math.Abs(CurrentPlotFunction.Timing - value) > double.Epsilon)
				{
					CurrentPlotFunction.Timing = value;
					NotifyParameterUpdates();
					CurrentPlotFunction.Update();
				}
			}
		}

		public double TimingParameterMinimum
		{
			get { return CurrentPlotFunction.TimingMinimum; }
		}

		public double TimingParameterMaximum
		{
			get { return CurrentPlotFunction.TimingMaximum; }
		}

		#endregion

		#region Logging

		#region LogOutput property

		private string _logOutput;

		/// <summary>
		/// Gets or sets the LogOutput property.
		/// </summary>
		public string LogOutput
		{
			get { return _logOutput ?? (_logOutput = string.Empty); }

			set
			{
				if (_logOutput != value)
				{
					_logOutput = value;
					NotifyPropertyChanged("LogOutput");
				}
			}
		}

		protected void Log(string function, string message = "", params object[] args)
		{
			var log = string.Format("[{0}.{1}] {2}", GetType().Name, function, string.Format(message, args));
			LogOutput = LogOutput + "\n" + log;
		}

		#endregion

		#endregion

	}
}
