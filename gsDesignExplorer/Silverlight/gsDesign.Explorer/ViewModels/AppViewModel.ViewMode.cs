namespace gsDesign.Explorer.ViewModels
{
	using System.Collections.Generic;
	using System.Windows;
	using Subfuzion.Helpers;

	public partial class AppViewModel
	{
		private void InitViewMode()
		{
			CurrentViewMode = ViewMode.Design;
			BeforeRunExecutedVisibility = Visibility.Visible;
			AfterRunExecutedVisibility = Visibility.Collapsed;
		}

		public IEnumerable<string> ViewModes
		{
			get { return EnumHelper.GetNames<ViewMode>(); }
		}

		private ViewMode _currentViewMode = (ViewMode)(-1);
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

					NotifyPropertyChanged("CurrentViewMode");
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
					NotifyPropertyChanged("DesignPanelVisibility");
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
					NotifyPropertyChanged("AnalysisPanelVisibility");
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
					NotifyPropertyChanged("SimulationPanelVisibility");
				}
			}
		}

		private Visibility _testPanelVisibility;
		public Visibility TestPanelVisibility
		{
			get { return _testPanelVisibility; }
			set
			{
				if (_testPanelVisibility != value)
				{
					_testPanelVisibility = value;
					NotifyPropertyChanged("TestPanelVisibility");
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
					NotifyPropertyChanged("BeforeRunExecutedVisibility");
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
					NotifyPropertyChanged("AfterRunExecutedVisibility");
				}
			}
		}
	}
}
