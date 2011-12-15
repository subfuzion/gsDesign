namespace gsDesign.Explorer.ViewModels.RServe
{
	using Subfuzion.Helpers;

	public static class LauncherSettings
	{
		public static bool ShowConsoleOutput
		{
			get { return Settings.Read<bool>("ShowConsoleOutput"); }
			set { Settings.Write("ShowConsoleOutput", value); }
		}

		public static string RservePath
		{
			get { return Settings.Read<string>("RservePath"); }
			set { Settings.Write("RservePath", value); }
		}

		public static string ExplorerPath
		{
			get { return Settings.Read<string>("ExplorerPath"); }
			set { Settings.Write("ExplorerPath", value); }
		}
	}
}