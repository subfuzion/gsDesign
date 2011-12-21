namespace gsDesign.Explorer.ViewModels
{
	using System.Net.Sockets;
	using System.Windows;
	using System.Windows.Controls;
	using Subfuzion.Helpers;
	using Subfuzion.R.Rserve;
	using Views.RServe;

	public partial class AppViewModel
	{
		// Called by constructor
		private void InitCommands()
		{
			ConnectCommand = new DelegateCommand { ExecuteAction = Connect, Async = true };
			ToggleConnectCommand = new DelegateCommand { ExecuteAction = ToggleConnect, Async = true };

			NewDesignCommand = new DelegateCommand { ExecuteAction = NewDesign };
			RunDesignCommand = new DelegateCommand { ExecuteAction = RunDesign, CompletedAction = RunDesignCompleted, Async = false, CanExecuteFunc = CanRunDesign, };
			ResetDesignCommand = new DelegateCommand { ExecuteAction = ResetDesign };
		}

		#region Rserve commands

		#region Connect command

		public DelegateCommand ConnectCommand { get; private set; }

		public void Connect(object parameter = null)
		{
			RserveClient.Connect(HandleConnectionResult);
			NotifyPropertyChanged("RserveClient");
		}

		#endregion

		#region Toggle connect

		public DelegateCommand ToggleConnectCommand { get; private set; }

		public void ToggleConnect(object parameter = null)
		{
			RserveClient.ToggleConnect(HandleConnectionResult);
			NotifyPropertyChanged("RserveClient");
		}

		private void HandleConnectionResult(ConnectionState connectionState, SocketError socketError)
		{
			switch (socketError)
			{
				case SocketError.AccessDenied:
					// not running as trusted application
					break;

				case SocketError.ConnectionRefused:
					// need to launch RServe
					ShowConnectionDialog();
					break;

				case SocketError.NotConnected:
					// not an error. just means disconnected.
					break;

				default:
					// log this
					break;
			}
		}

		private ChildWindow _connectionDialog;
		private void ShowConnectionDialog()
		{
			Deployment.Current.Dispatcher.BeginInvoke(() =>
			{
				_connectionDialog = new ConnectionViewDialog();

				_connectionDialog.Closed += (sender, e) =>
				{

				};

				_connectionDialog.Show();
			});
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
			RunDesign();
		}

		private bool CanRunDesign(object parameter = null)
		{
			return RserveClient.ConnectionState == ConnectionState.Connected && !RserveClient.IsBusy;
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
