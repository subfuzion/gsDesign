namespace Subfuzion.Helpers
{
	using System;
	using System.IO.IsolatedStorage;

	public static class Settings
	{
		public static T Read<T>(string name, T defaultValue = default(T))
		{
			var settings = IsolatedStorageSettings.ApplicationSettings;
			if (settings == null)
			{
				throw new Exception("Unable to read from isolated storage");
			}

			T value;
			settings.TryGetValue(name, out value);
			return value;
		}

		public static void Write<T>(string name, T value)
		{
			var settings = IsolatedStorageSettings.ApplicationSettings;
			if (settings == null)
			{
				throw new Exception("Unable to write to isolated storage");
			}

			if (settings.Contains(name))
			{
				settings[name] = value;
			}
			else
			{
				settings.Add(name, value);
			}

			settings.Save();
		}
	}
}