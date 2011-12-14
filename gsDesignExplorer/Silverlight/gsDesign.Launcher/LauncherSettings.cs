namespace gsDesign.Launcher
{
	using Properties;

	public static class LauncherSettings
	{
		private static Settings Settings
		{
			get { return Settings.Default; }
		}

		public static bool ShowConsoleOutput
		{
			get { return Settings.ShowConsoleOutput; }
			set
			{
				Settings.ShowConsoleOutput = value;
				Settings.Save();
			}
		}

		public static string RservePath
		{
			get { return Settings.RservePath; }
			set
			{
				Settings.RservePath = value;
				Settings.Save();
			}
		}

		public static string ExplorerPath
		{
			get { return Settings.ExplorerPath; }
			set
			{
				Settings.ExplorerPath = value;
				Settings.Save();
			}
		}
	}
}