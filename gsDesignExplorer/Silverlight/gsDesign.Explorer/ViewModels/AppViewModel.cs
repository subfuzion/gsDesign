namespace gsDesign.Explorer.ViewModels
{
	using RServe;
	using Subfuzion.R.Rserve;

	public partial class AppViewModel : ViewModelBase
	{
		#region Fields

		private string _outputText = "Welcome to gsDesign Explorer";

		#endregion

		public AppViewModel()
		{
			RserveClient = new RserveClient();
			CreateDesign();

			InitViewMode();
			InitCommands();
		}

		public RserveClient RserveClient { get; private set; }

		public string OutputText
		{
			get { return _outputText; }
			set
			{
				if (_outputText != value)
				{
					_outputText = value;
					NotifyPropertyChanged("OutputText");
				}
			}
		}

		private Launcher _launcher;
		public Launcher Launcher
		{
			get { return _launcher ?? (_launcher = new Launcher()); }
		}
	}
}
