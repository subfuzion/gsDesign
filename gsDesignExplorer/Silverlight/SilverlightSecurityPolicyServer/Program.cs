namespace Subfuzion.Silverlight.Tcp
{
	using System;
	using System.Configuration;
	using System.IO;
	using System.Reflection;

	internal class Program
	{
		public static readonly string DefaultPolicyFileKey = "DefaultPolicyFile";

		private static void Main(string[] args)
		{
			try
			{
				string policyFile = args.Length > 0 ? args[0] : ConfigurationManager.AppSettings[DefaultPolicyFileKey];

				string currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? ".";
				policyFile = Path.Combine(currentDir, policyFile);

				var policyServer = new SocketPolicyServer(policyFile);
				policyServer.Start();

				Console.WriteLine("Started Silverlight socket policy server on port 943. Press <Enter> to exit.");
				Console.ReadLine();

				policyServer.Stop();
				Console.WriteLine("Stopped Silverlight socket policy server");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				Console.ReadLine();
			}
		}
	}
}