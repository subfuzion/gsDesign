namespace gsDesign.Explorer.ViewModels
{
	using System.Threading;
	using System.Windows;
	using Subfuzion.Helpers;

	public partial class AppViewModel
	{
		// Called by constructor
		private void InitCommands()
		{
			NewDesignCommand = new DelegateCommand {ExecuteAction = NewDesign};
			RunDesignCommand = new DelegateCommand { ExecuteAction = RunDesign, CompletedAction = RunDesignCompleted, Async = true };
			ConnectCommand = new DelegateCommand {ExecuteAction = Connect, Async = true};
			ToggleConnectCommand = new DelegateCommand { ExecuteAction = ToggleConnect, Async = true };
		}

		public DelegateCommand NewDesignCommand { get; set; }

		private void NewDesign(object parameter = null)
		{
			CreateDesign();
		}

		public DelegateCommand RunDesignCommand { get; private set; }

		private void RunDesign(object parameter = null)
		{
			Thread.Sleep(2000);
		}

		private void RunDesignCompleted(object parameter = null)
		{
			BeforeRunExecutedVisibility = Visibility.Collapsed;
			AfterRunExecutedVisibility = Visibility.Visible;
		}

		public DelegateCommand ConnectCommand { get; private set; }

		private void Connect(object parameter = null)
		{
			RserveClient.Connect();
		}

		public DelegateCommand ToggleConnectCommand { get; private set; }

		private void ToggleConnect(object parameter = null)
		{
			RserveClient.ToggleConnect();
		}
	}
}
