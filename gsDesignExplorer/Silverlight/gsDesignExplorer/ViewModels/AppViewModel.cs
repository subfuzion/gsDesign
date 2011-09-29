namespace gsDesign.Explorer.ViewModels
{
	using System;
	using System.Threading;
	using System.Windows;
	using System.Windows.Input;

	public class AppViewModel : ViewModelBase
	{
		#region Commands

		public ICommand RunDesignCommand { get; private set; }

		#endregion

		private Visibility _afterRunExecutedVisibility;
		private Visibility _analysisPanelVisibility;
		private Visibility _beforeRunExecutedVisibility;
		private int _currentViewMode;
		private Visibility _designPanelVisibility;
		private Visibility _simulationPanelVisibility;
		private Visibility _testPanelVisibility;
		private ViewMode _viewMode;

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

		public string[] ViewModes
		{
			get { return new[] {"Design", "Analysis", "Simulation", "Test"}; }
		}

		public int CurrentViewMode
		{
			get { return _currentViewMode; }
			set
			{
				if (_currentViewMode != value)
				{
					_currentViewMode = value;
					RaisePropertyChanged("CurrentViewMode");

					switch (value)
					{
						case 0:
							AnalysisPanelVisibility = Visibility.Collapsed;
							SimulationPanelVisibility = Visibility.Collapsed;
							TestPanelVisibility = Visibility.Collapsed;
							DesignPanelVisibility = Visibility.Visible;
							break;
						case 1:
							DesignPanelVisibility = Visibility.Collapsed;
							SimulationPanelVisibility = Visibility.Collapsed;
							TestPanelVisibility = Visibility.Collapsed;
							AnalysisPanelVisibility = Visibility.Visible;
							break;
						case 2:
							DesignPanelVisibility = Visibility.Collapsed;
							AnalysisPanelVisibility = Visibility.Collapsed;
							TestPanelVisibility = Visibility.Collapsed;
							SimulationPanelVisibility = Visibility.Visible;
							break;
						case 3:
							DesignPanelVisibility = Visibility.Collapsed;
							AnalysisPanelVisibility = Visibility.Collapsed;
							SimulationPanelVisibility = Visibility.Collapsed;
							TestPanelVisibility = Visibility.Visible;
							break;
					}
				}
			}
		}

		public string ViewMode
		{
			get { return _viewMode.ToString(); }
			set
			{
				if (_viewMode.ToString() != value)
				{
					_viewMode = (ViewMode) Enum.Parse(typeof (ViewMode), value, true);
					RaisePropertyChanged("ViewMode");
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

		public void RunDesign(object parameter = null)
		{
			Thread.Sleep(3000);
		}

		public void RunDesignCompleted(object parameter = null)
		{
			BeforeRunExecutedVisibility = Visibility.Collapsed;
			AfterRunExecutedVisibility = Visibility.Visible;
		}
	}
}