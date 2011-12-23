namespace gsDesign.Explorer.ViewModels
{
	using RServe;
	using Subfuzion.R.Rserve;

	public partial class AppViewModel : ViewModelBase
	{
		public AppViewModel()
		{
			RserveClient = new RserveConnection();
			CreateDesign();

			InitHandlers();
			InitViewMode();
			InitCommands();
		}

		private void InitHandlers()
		{
			RserveClient.PropertyChanged += (sender, propertyChangedEventArgs) =>
			{
				switch (propertyChangedEventArgs.PropertyName)
				{
					case "ConnectionState":
						RunDesignCommand.Requery();
						break;
				}
			};
		}

		private string _outputText = "Welcome to gsDesign Explorer";
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
		/// <summary>
		/// For launching Rserve (currently not being used)
		/// </summary>
		public Launcher Launcher
		{
			get { return _launcher ?? (_launcher = new Launcher()); }
		}
	}
}
