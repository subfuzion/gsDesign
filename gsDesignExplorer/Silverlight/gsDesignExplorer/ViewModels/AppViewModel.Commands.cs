namespace gsDesign.Explorer.ViewModels
{
	using System.Threading;
	using System.Windows;

	public partial class AppViewModel
	{
		// Called by constructor
		private void InitCommands()
		{
			RunDesignCommand = new DelegateCommand { ExecuteAction = RunDesign, CompletedAction = RunDesignCompleted, Async = true };
			ConnectCommand = new DelegateCommand {ExecuteAction = Connect, Async = true};
			ToggleConnectCommand = new DelegateCommand { ExecuteAction = ToggleConnect, Async = true };
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
