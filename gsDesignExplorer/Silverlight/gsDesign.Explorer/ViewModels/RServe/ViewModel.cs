namespace gsDesign.Explorer.ViewModels.RServe
{
	using System;
	using System.IO;
	using ViewModels;

	public class ViewModel : ViewModelBase
	{
		public static readonly string RserveFileName = "Rserve.exe";

		private RunState _rserveRunState;

		public ViewModel()
		{
			_rserveRunState = IsValidRservePath ? RunState.Stopped : RunState.Invalid;
		}

		private Launcher _launcher;
		public Launcher Launcher
		{
			get { return _launcher ?? (_launcher = new Launcher()); }
		}

		public bool IsSystemConfigurationValid
		{
			get { return IsRserveButtonEnabled; }
		}

		public SystemState SystemState
		{
			get
			{
				if (!IsSystemConfigurationValid)
					return SystemState.Invalid;

				if (RserveRunState == RunState.Running)
					return SystemState.Running;

				return SystemState.Stopped;
			}
		}

		public bool IsConsoleOutputEnabled
		{
			get { return LauncherSettings.ShowConsoleOutput; }
			set
			{
				if (LauncherSettings.ShowConsoleOutput != value)
				{
					LauncherSettings.ShowConsoleOutput = value;
					NotifyPropertyChanged("IsConsoleOutputEnabled");
				}
			}
		}

		#region Rserve

		public RunState RserveRunState
		{
			get { return _rserveRunState; }

			set
			{
				if (_rserveRunState != value)
				{
					_rserveRunState = value;
					NotifyPropertyChanged("RserveRunState");
					NotifyPropertyChanged("IsRserveButtonEnabled");
					NotifyPropertyChanged("IsRservePathButtonEnabled");
					NotifyPropertyChanged("IsExplorerButtonEnabled");
					NotifyPropertyChanged("SystemState");
				}
			}
		}

		public string RservePath
		{
			get { return LauncherSettings.RservePath; }

			set
			{
				if (LauncherSettings.RservePath != value && IsValidRservePathString(value))
				{
					LauncherSettings.RservePath = value;
					NotifyPropertyChanged("RservePath");
					RserveRunState = RunState.Stopped;
				}
			}
		}

		public bool IsValidRservePathString(string path)
		{
			return path != null && File.Exists(path) /* && RservePath.EndsWith(RserveFileName) && */ ;
		}

		public bool IsValidRservePath
		{
			get { return IsValidRservePathString(RservePath); }
		}

		public bool IsRserveButtonEnabled
		{
			get { return CanStartRserve || RserveRunState == RunState.Running; }
		}

		public bool IsRservePathButtonEnabled
		{
			get { return RserveRunState != RunState.Running; }
		}

		public bool CanStartRserve
		{
			get { return RserveRunState == RunState.Stopped && IsValidRservePath; }
		}

		public bool CanStopRserve
		{
			get { return RserveRunState == RunState.Running; }
		}

		public void StartRserve()
		{
			if (CanStartRserve)
			{
				try
				{
					Launcher.StartRserve(RservePath, IsConsoleOutputEnabled);
					RserveRunState = RunState.Running;
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		public void StopRserve()
		{
			if (CanStopRserve)
			{
				try
				{
					Launcher.StopRserve();
					RserveRunState = RunState.Stopped;
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		public void ToggleRserveRunning()
		{
			if (RserveRunState == RunState.Stopped)
			{
				StartRserve();
			}
			else
			{
				StopRserve();
			}
		}

		#endregion
	}
}