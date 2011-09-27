using System;
using System.Diagnostics;
using System.IO;

namespace gsDesign.LauncherGUI
{
	public class Launcher
	{
		private Process mongooseProcess;
		private Process rserveProcess;
		private Process policyServer;

		public void StartRserve()
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\thirdparty\rserve\inst\Rserve.exe");

			var processStartInfo = new ProcessStartInfo { FileName = pathname, Arguments = "--RS-port 4502", CreateNoWindow = true };

			rserveProcess = Process.Start(processStartInfo);

			Print("started rserve on port {0}", 4502.ToString());
		}

		public void StartMongoose()
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\thirdparty\mongoose\mongoose.exe");

			var processStartInfo = new ProcessStartInfo { FileName = pathname, CreateNoWindow = true };

			mongooseProcess = Process.Start(processStartInfo);

			Print("started mongoose on port {0}", 8080.ToString());
		}

		public void StartSilverlightPolicyServer()
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\SilverlightSecurityPolicyServer\bin\Debug\SilverlightPolicyServer.exe");

			var processStartInfo = new ProcessStartInfo { FileName = pathname, CreateNoWindow = true };

			policyServer = Process.Start(processStartInfo);

			Print("started Silverlight policy server on port {0}", 943.ToString());
		}

		private void Print(string s, params string[] args)
		{
			Console.Out.WriteLine(string.Format(s, args));
		}
	}
}
