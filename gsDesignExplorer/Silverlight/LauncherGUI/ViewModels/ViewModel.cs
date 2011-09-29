using System;
using System.IO;
using gsDesign.LauncherGUI.Models;

namespace gsDesign.LauncherGUI.ViewModels
{
	public class ViewModel : ViewModelBase
	{
		public static readonly string RserveFileName = "Rserve.exe";

		private readonly AppModel _appModel = new AppModel();

		private RunState _explorerRunState;
		private RunState _policyServerRunState;
		private RunState _rserveRunState;

		public ViewModel()
		{
			_rserveRunState = IsValidRservePath ? RunState.Stopped : RunState.Invalid;
			_policyServerRunState = IsValidPolicyServerPath ? RunState.Stopped : RunState.Invalid;
			_explorerRunState = IsValidExplorerPath ? RunState.Stopped : RunState.Invalid;
		}

		private AppModel Model
		{
			get { return _appModel; }
		}

		public bool IsSystemConfigurationValid
		{
			get { return IsRserveButtonEnabled && IsPolicyServerButtonEnabled && IsValidExplorerPath; }
		}

		public SystemState SystemState
		{
			get
			{
				if (!IsSystemConfigurationValid)
					return SystemState.Invalid;

				if (RserveRunState == RunState.Running && PolicyServerRunState == RunState.Running)
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
					RaisePropertyChanged("IsConsoleOutputEnabled");
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
					RaisePropertyChanged("RserveRunState");
					RaisePropertyChanged("IsRserveButtonEnabled");
					RaisePropertyChanged("IsRservePathButtonEnabled");
					RaisePropertyChanged("IsExplorerButtonEnabled");
					RaisePropertyChanged("SystemState");
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
					RaisePropertyChanged("RservePath");
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
					Model.Launcher.StartRserve(RservePath, IsConsoleOutputEnabled);
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
					Model.Launcher.StopRserve();
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

		#region Silverlight Policy Server

		public RunState PolicyServerRunState
		{
			get { return _policyServerRunState; }

			set
			{
				if (_policyServerRunState != value)
				{
					_policyServerRunState = value;
					RaisePropertyChanged("PolicyServerRunState");
					RaisePropertyChanged("IsPolicyServerButtonEnabled");
					RaisePropertyChanged("IsPolicyServerPathButtonEnabled");
					RaisePropertyChanged("IsExplorerButtonEnabled");
					RaisePropertyChanged("SystemState");
				}
			}
		}

		public string PolicyServerPath
		{
			get { return LauncherSettings.PolicyServerPath; }

			set
			{
				if (LauncherSettings.PolicyServerPath != value && IsValidPolicyServerPathString(value))
				{
					LauncherSettings.PolicyServerPath = value;
					RaisePropertyChanged("PolicyServerPath");
					PolicyServerRunState = RunState.Stopped;
				}
			}
		}

		public bool IsValidPolicyServerPathString(string path)
		{
			return path != null && File.Exists(path);
		}

		public bool IsValidPolicyServerPath
		{
			get { return IsValidPolicyServerPathString(PolicyServerPath); }
		}

		public bool IsPolicyServerButtonEnabled
		{
			get { return CanStartPolicyServer || PolicyServerRunState == RunState.Running; }
		}

		public bool IsPolicyServerPathButtonEnabled
		{
			get { return PolicyServerRunState != RunState.Running; }
		}

		public bool CanStartPolicyServer
		{
			get { return PolicyServerRunState == RunState.Stopped && IsValidPolicyServerPath; }
		}

		public bool CanStopPolicyServer
		{
			get { return PolicyServerRunState == RunState.Running; }
		}

		public void StartPolicyServer()
		{
			if (CanStartPolicyServer)
			{
				try
				{
					Model.Launcher.StartSilverlightPolicyServer(PolicyServerPath, IsConsoleOutputEnabled);
					PolicyServerRunState = RunState.Running;
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		public void StopPolicyServer()
		{
			if (CanStopPolicyServer)
			{
				try
				{
					Model.Launcher.StopSilverlightPolicyServer();
					PolicyServerRunState = RunState.Stopped;
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		public void TogglePolicyServerRunning()
		{
			if (PolicyServerRunState == RunState.Stopped)
			{
				StartPolicyServer();
			}
			else
			{
				StopPolicyServer();
			}
		}

		#endregion

		#region gsDesign Explorer

		public RunState ExplorerRunState
		{
			get { return _explorerRunState; }

			set
			{
				if (_explorerRunState != value)
				{
					_explorerRunState = value;
					RaisePropertyChanged("ExplorerRunState");
					RaisePropertyChanged("IsExplorerButtonEnabled");
					RaisePropertyChanged("IsExplorerPathButtonEnabled");
					RaisePropertyChanged("SystemState");
				}
			}
		}

		public string ExplorerPath
		{
			get { return LauncherSettings.ExplorerPath; }

			set
			{
				if (LauncherSettings.ExplorerPath != value && IsValidExplorerPathString(value))
				{
					LauncherSettings.ExplorerPath = value;
					RaisePropertyChanged("ExplorerPath");
					ExplorerRunState = RunState.Stopped;
				}
			}
		}

		public bool IsValidExplorerPathString(string path)
		{
			return path != null && File.Exists(path);
		}

		public bool IsValidExplorerPath
		{
			get { return IsValidExplorerPathString(ExplorerPath); }
		}

		public bool IsExplorerButtonEnabled
		{
			get { return CanStartExplorer; }
		}

		public bool IsExplorerPathButtonEnabled
		{
			get { return ExplorerRunState != RunState.Running; }
		}

		public bool CanStartExplorer
		{
			get { return IsValidExplorerPath && RserveRunState == RunState.Running && PolicyServerRunState == RunState.Running; }
		}

		public void OpenExplorer()
		{
			if (CanStartExplorer)
			{
				try
				{
					Model.Launcher.LaunchExplorer(ExplorerPath);
					ExplorerRunState = RunState.Running;
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		#endregion
	}
}