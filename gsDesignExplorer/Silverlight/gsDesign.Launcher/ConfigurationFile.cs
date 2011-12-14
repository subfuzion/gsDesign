namespace gsDesign.Launcher
{
	using System.IO;
	using System.Xml.Serialization;

	public class ConfigurationFile
	{
		private static readonly string ConfigurationFileName = "settings.xml";

		private static ConfigurationFile _instance;

		public static ConfigurationFile Settings
		{
			get
			{
				if (_instance == null)
				{
					if (File.Exists(ConfigurationFileName))
					{
						var serializer = new XmlSerializer(typeof (ConfigurationFile));
						var fileStream = new FileStream(ConfigurationFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
						_instance = (ConfigurationFile) serializer.Deserialize(fileStream);
					}
					else
					{
						_instance = new ConfigurationFile();
					}
				}

				return _instance;
			}
		}

		public string RservePath { get; set; }

		public string PolicyServerPath { get; set; }

		public void Save()
		{
			var serializer = new XmlSerializer(typeof (ConfigurationFile));
			var fileStream = new FileStream(ConfigurationFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
			serializer.Serialize(fileStream, this);
		}
	}
}