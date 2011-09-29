namespace gsDesign.Explorer.ViewModels
{
	using System.Threading;
	using System.Windows;
	using System.Windows.Input;
	using Helpers;

	public class AppViewModel : ViewModelBase
	{
		#region Fields

		private ViewMode _currentViewMode;

		private Visibility _analysisPanelVisibility;
		private Visibility _designPanelVisibility;
		private Visibility _simulationPanelVisibility;
		private Visibility _testPanelVisibility;

		private Visibility _afterRunExecutedVisibility;
		private Visibility _beforeRunExecutedVisibility;

		#endregion

		public AppViewModel()
		{
			CurrentViewMode = 0;

			DesignPanelVisibility = Visibility.Visible;
			AnalysisPanelVisibility = Visibility.Collapsed;
			SimulationPanelVisibility = Visibility.Collapsed;
			TestPanelVisibility = Visibility.Collapsed;

			BeforeRunExecutedVisibility = Visibility.Visible;
			AfterRunExecutedVisibility = Visibility.Collapsed;

			// commands
			RunDesignCommand = new DelegateCommand {ExecuteAction = RunDesign, CompletedAction = RunDesignCompleted, Async = true};
		}

		#region View mode (Design, Analysis, Simulation...) management

		public string[] ViewModes
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

		#region Commands

		public ICommand RunDesignCommand { get; private set; }

		public void RunDesign(object parameter = null)
		{
			Thread.Sleep(2000);
		}

		public void RunDesignCompleted(object parameter = null)
		{
			BeforeRunExecutedVisibility = Visibility.Collapsed;
			AfterRunExecutedVisibility = Visibility.Visible;
		}

		#endregion
	}
}