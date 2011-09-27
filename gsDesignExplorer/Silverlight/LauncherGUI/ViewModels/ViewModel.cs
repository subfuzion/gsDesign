using System.IO;

namespace gsDesign.LauncherGUI.ViewModels
{
	public class ViewModel : ViewModelBase
	{
		public static readonly string RserveFileName = "Rserve.exe";

		private RunState _rserveRunState;
		private string _rservePath;

		public RunState RserveRunState
		{
			get
			{
				return _rserveRunState;
			}

			set
			{
				if (_rserveRunState != value)
				{
					_rserveRunState = value;
					RaisePropertyChanged("RserveRunState");
					RaisePropertyChanged("IsRserveButtonEnabled");
				}
			}
		}

		public string RservePath
		{
			get
			{
				return _rservePath;
			}

			set
			{
				if (_rservePath != value)
				{
					_rservePath = value;
					RaisePropertyChanged("RservePath");
					RaisePropertyChanged("IsRserveButtonEnabled");
				}
			}
		}

		public bool IsRservePathValid
		{
			get { return RservePath != null && RservePath.EndsWith(RserveFileName) && File.Exists(RservePath); }
		}

		public bool IsRserveButtonEnabled
		{
			get
			{
				return IsRservePathValid || RserveRunState == RunState.Running;
			}
		}
	}
}
