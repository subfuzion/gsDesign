using System;
using System.Configuration;

namespace Subfuzion.Silverlight.Tcp
{
	class Program
	{
		public static readonly string DefaultPolicyFileKey = "DefaultPolicyFile";

		// No attempt is made to catch exceptions here. Any exceptions will be written to the
		// console and the process will exit.
		static void Main(string[] args)
		{
			var policyFile = args.Length > 0 ? args[0] : ConfigurationManager.AppSettings[DefaultPolicyFileKey];

			var policyServer = new SocketPolicyServer(policyFile);
			policyServer.Start();

			Console.WriteLine("Started Silverlight socket policy server on port 943. Press <Enter> to stop.");
			Console.ReadLine();

			policyServer.Stop();
			Console.WriteLine("Stopped Silverlight socket policy server");
		}
	}
}
