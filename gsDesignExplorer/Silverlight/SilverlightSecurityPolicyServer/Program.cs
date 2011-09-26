using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Reflection;

namespace Subfuzion.Silverlight.Tcp
{
	class Program
	{
		public static readonly string DefaultPolicyFileKey = "DefaultPolicyFile";

		static void Main(string[] args)
		{
			try
			{
				var policyFile = args.Length > 0 ? args[0] : ConfigurationManager.AppSettings[DefaultPolicyFileKey];

				var currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? ".";
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
