namespace gsDesign.Explorer.ViewModels
{
	using System.Threading;
	using System.Windows;
	using System.Windows.Input;

	public partial class AppViewModel
	{
		// Called by constructor
		private void InitCommands()
		{
			RunDesignCommand = new DelegateCommand { ExecuteAction = RunDesign, CompletedAction = RunDesignCompleted, Async = true };
		}

		public ICommand RunDesignCommand { get; private set; }

		private void RunDesign(object parameter = null)
		{
			Thread.Sleep(2000);
		}

		private void RunDesignCompleted(object parameter = null)
		{
			BeforeRunExecutedVisibility = Visibility.Collapsed;
			AfterRunExecutedVisibility = Visibility.Visible;
		}


	}
}
