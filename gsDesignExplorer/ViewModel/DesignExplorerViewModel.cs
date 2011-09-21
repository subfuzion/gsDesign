namespace gsDesignExplorer.ViewModel
{
	using System;
	using System.Windows;

	public class DesignExplorerViewModel : ViewModelBase
	{
		public DesignExplorerViewModel()
		{
			CurrentViewMode = 0;

			DesignPanelVisibility = Visibility.Visible;
			AnalysisPanelVisibility = Visibility.Collapsed;
			SimulationPanelVisibility = Visibility.Collapsed;

			BeforeRunExecutedVisibility = Visibility.Visible;
			AfterRunExecutedVisibility = Visibility.Collapsed;
		}

		public string[] ViewModes
		{
			get { return new[] {"Design", "Analysis", "Simulation"}; }
		}

		private int _currentViewMode;
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
							DesignPanelVisibility = Visibility.Visible;
							break;
						case 1:
							DesignPanelVisibility = Visibility.Collapsed;
							SimulationPanelVisibility = Visibility.Collapsed;
							AnalysisPanelVisibility = Visibility.Visible;
							break;
						case 2:
							DesignPanelVisibility = Visibility.Collapsed;
							AnalysisPanelVisibility = Visibility.Collapsed;
							SimulationPanelVisibility = Visibility.Visible;
							break;
					}
				}
			}
		}

		private ViewMode _viewMode;
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

		private Visibility _designPanelVisibility;
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

		private Visibility _analysisPanelVisibility;
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

		private Visibility _simulationPanelVisibility;
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

		private Visibility _beforeRunExecutedVisibility;
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

		private Visibility _afterRunExecutedVisibility;
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

		public void RunDesign()
		{
			BeforeRunExecutedVisibility = Visibility.Collapsed;
			AfterRunExecutedVisibility = Visibility.Visible;
		}
	}
}
