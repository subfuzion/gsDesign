namespace gsDesign.Explorer.ViewModels
{
	using System.Threading;
	using System.Windows;
	using RService;
	using Subfuzion.Helpers;

	public partial class AppViewModel
	{
		// Called by constructor
		private void InitCommands()
		{
			ConnectCommand = new DelegateCommand { ExecuteAction = Connect, Async = true };
			ToggleConnectCommand = new DelegateCommand { ExecuteAction = ToggleConnect, Async = true };

			NewDesignCommand = new DelegateCommand { ExecuteAction = NewDesign };
			RunDesignCommand = new DelegateCommand { ExecuteAction = RunDesign, CompletedAction = RunDesignCompleted, Async = true };
			ResetDesignCommand = new DelegateCommand { ExecuteAction = ResetDesign };
		}

		#region Rserve commands

		#region Connect command

		public DelegateCommand ConnectCommand { get; private set; }

		private void Connect(object parameter = null)
		{
			RserveClient.Connect();
			RaisePropertyChanged("RserveClient");
		}

		#endregion

		#region Toggle connect

		public DelegateCommand ToggleConnectCommand { get; private set; }

		private void ToggleConnect(object parameter = null)
		{
			RserveClient.ToggleConnect();
			RaisePropertyChanged("RserveClient");
		}

		#endregion

		#endregion // Rserve commands

		#region Design commands

		#region New design command

		public DelegateCommand NewDesignCommand { get; set; }

		private void NewDesign(object parameter = null)
		{
			CreateDesign();
		}

		#endregion

		#region Run design command

		public DelegateCommand RunDesignCommand { get; private set; }

		private void RunDesign(object parameter = null)
		{
			// Thread.Sleep(2000);

			var rService = new RServiceClient();
			rService.DoWorkCompleted += rService_DoWorkCompleted;
			rService.DoWorkAsync();
		}

		void rService_DoWorkCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			
		}

		private void RunDesignCompleted(object parameter = null)
		{
			BeforeRunExecutedVisibility = Visibility.Collapsed;
			AfterRunExecutedVisibility = Visibility.Visible;
		}

		#endregion

		#region Reset design command

		public DelegateCommand ResetDesignCommand { get; private set; }

		public void ResetDesign(object parameter = null)
		{
			CurrentDesign.Reset();
		}

		#endregion





		#endregion // Design commands
	}
}
