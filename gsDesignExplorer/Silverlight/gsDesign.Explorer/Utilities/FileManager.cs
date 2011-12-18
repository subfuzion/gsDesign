namespace gsDesign.Explorer.Utilities
{
	using System;
	using System.IO;

	public static class FileManager
	{
		public static string GetTempPath()
		{
			return Path.GetTempPath();
		}

		public static string GetTempFileName()
		{
			return Path.GetTempFileName();
		}
		
		public static void EnsureLocalDataFolderExists()
		{
			var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			path = Path.Combine(path, "gsDesign");
			if (!File.Exists(path))
			{
				File.Create(path);
			}
		}

		public static string CreateTempFile(string contents)
		{
			var path = GetTempFileName();
			using (var writer = new StreamWriter(path))
			{
				writer.Write(contents);
				writer.Close();
			}

			return path;
		}
	}
}