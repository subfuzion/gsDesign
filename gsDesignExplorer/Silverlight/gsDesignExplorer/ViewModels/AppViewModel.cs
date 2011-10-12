namespace gsDesign.Explorer.ViewModels
{
	using System.Collections.Generic;
	using System.Windows;
	using Design;
	using Models;
	using Subfuzion.Helpers;
	using Subfuzion.R.Rserve;

	public partial class AppViewModel : ViewModelBase
	{
		#region Fields

		private readonly GSDesignApplication _gsDesign;

		private ViewMode _currentViewMode = (ViewMode) (-1);

		private Visibility _analysisPanelVisibility;
		private Visibility _designPanelVisibility;
		private Visibility _simulationPanelVisibility;
		private Visibility _testPanelVisibility;

		private Visibility _afterRunExecutedVisibility;
		private Visibility _beforeRunExecutedVisibility;

		private string _outputText = "Welcome to gsDesign Explorer";

		#endregion

		public AppViewModel()
		{
			_gsDesign = new GSDesignApplication();

			CurrentViewMode = ViewMode.Design;

			BeforeRunExecutedVisibility = Visibility.Visible;
			AfterRunExecutedVisibility = Visibility.Collapsed;

			RserveClient = new RserveClient();

			InitCommands();
			InitHandlers();
		}

		public GSDesignApplication GSDesign
		{
			get { return _gsDesign; }
		}

		public void InitHandlers()
		{
			GSDesign.PropertyChanged += (sender, propertyChangedEventArgs) =>
			    {
			    	switch (propertyChangedEventArgs.PropertyName)
			    	{
			    		case "Designs":
							RaisePropertyChanged("Designs");
			    			break;

						case "CurrentDesign":
							RaisePropertyChanged("CurrentDesign");
			    			break;
			    	}
			    };
		}

		public RserveClient RserveClient { get; private set; }

		#region View mode (Design, Analysis, Simulation...) management

		public IEnumerable<string> ViewModes
		{
			get { return EnumHelper.GetNames<ViewMode>(); }
		}

		public ViewMode CurrentViewMode
		{
			get { return _currentViewMode; }
			set
			{
				if (_currentViewMode != value)
				{
					_currentViewMode = value;

					switch (value)
					{
						case ViewMode.Design:
							AnalysisPanelVisibility = Visibility.Collapsed;
							SimulationPanelVisibility = Visibility.Collapsed;
							TestPanelVisibility = Visibility.Collapsed;
							DesignPanelVisibility = Visibility.Visible;
							break;
						case ViewMode.Analysis:
							DesignPanelVisibility = Visibility.Collapsed;
							SimulationPanelVisibility = Visibility.Collapsed;
							TestPanelVisibility = Visibility.Collapsed;
							AnalysisPanelVisibility = Visibility.Visible;
							break;
						case ViewMode.Simulation:
							DesignPanelVisibility = Visibility.Collapsed;
							AnalysisPanelVisibility = Visibility.Collapsed;
							TestPanelVisibility = Visibility.Collapsed;
							SimulationPanelVisibility = Visibility.Visible;
							break;
						case ViewMode.Test:
							DesignPanelVisibility = Visibility.Collapsed;
							AnalysisPanelVisibility = Visibility.Collapsed;
							SimulationPanelVisibility = Visibility.Collapsed;
							TestPanelVisibility = Visibility.Visible;
							break;
					}

					RaisePropertyChanged("CurrentViewMode");
				}
			}
		}

		public Visibility DesignPanelVisibility
		{
			get { return _designPanelVisibility; }
			set
			{
				if (_designPanelVisibility != value)
				{
					_designPanelVisibility = value;
					RaisePropertyChanged("DesignPanelVisibility");
				}
			}
		}

		public Visibility AnalysisPanelVisibility
		{
			get { return _analysisPanelVisibility; }
			set
			{
				if (_analysisPanelVisibility != value)
				{
					_analysisPanelVisibility = value;
					RaisePropertyChanged("AnalysisPanelVisibility");
				}
			}
		}

		public Visibility SimulationPanelVisibility
		{
			get { return _simulationPanelVisibility; }
			set
			{
				if (_simulationPanelVisibility != value)
				{
					_simulationPanelVisibility = value;
					RaisePropertyChanged("SimulationPanelVisibility");
				}
			}
		}

		public Visibility TestPanelVisibility
		{
			get { return _testPanelVisibility; }
			set
			{
				if (_testPanelVisibility != value)
				{
					_testPanelVisibility = value;
					RaisePropertyChanged("TestPanelVisibility");
				}
			}
		}

		public Visibility BeforeRunExecutedVisibility
		{
			get { return _beforeRunExecutedVisibility; }
			set
			{
				if (_beforeRunExecutedVisibility != value)
				{
					_beforeRunExecutedVisibility = value;
					RaisePropertyChanged("BeforeRunExecutedVisibility");
				}
			}
		}

		public Visibility AfterRunExecutedVisibility
		{
			get { return _afterRunExecutedVisibility; }
			set
			{
				if (_afterRunExecutedVisibility != value)
				{
					_afterRunExecutedVisibility = value;
					RaisePropertyChanged("AfterRunExecutedVisibility");
				}
			}
		}

		#endregion

		public string OutputText
		{
			get { return _outputText; }
			set
			{
				if (_outputText != value)
				{
					_outputText = value;
					RaisePropertyChanged("OutputText");
				}
			}
		}




		//#region Design property

		//private Design _design;

		//public Design Design
		//{
		//    get { return _design; }

		//    set
		//    {
		//        if (_design != value)
		//        {
		//            RemoveDesignHandlers();
		//            _design = value;
		//            AddDesignHandlers();
		//            Update();
		//        }
		//    }
		//}

		//#endregion // Design




		//public readonly DesignViewModel DesignViewModel = new DesignViewModel();


		#region Designs property

		public DesignCollection Designs
		{
			get { return GSDesign.Designs; }
		}

		#endregion // Designs

		#region CurrentDesign property

		public Models.Design CurrentDesign
		{
			get { return GSDesign.CurrentDesign; }
			set { GSDesign.CurrentDesign = value; }
		}

		#endregion // CurrentDesign



	}
}