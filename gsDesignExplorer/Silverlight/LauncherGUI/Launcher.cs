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
		private Process explorer;

		public void StartRserve(string pathname, bool showConsoleOutput = false)
		{
			// string pathname = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\thirdparty\rserve\inst\Rserve.exe");

			try
			{
				var processStartInfo = new ProcessStartInfo
				{
					FileName = pathname,
					Arguments = "--RS-port 4502",
					CreateNoWindow = true,
					UseShellExecute = showConsoleOutput,
				};

				rserveProcess = Process.Start(processStartInfo);

				Print("started rserve on port {0}", 4502.ToString());
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void StopRserve()
		{
			try
			{
				rserveProcess.Kill();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void StartMongoose(bool showConsoleOutput = false)
		{
			try
			{
				string pathname = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\thirdparty\mongoose\mongoose.exe");

				var processStartInfo = new ProcessStartInfo
				{
					FileName = pathname,
					CreateNoWindow = true,
					UseShellExecute = showConsoleOutput,
				};

				mongooseProcess = Process.Start(processStartInfo);

				Print("started mongoose on port {0}", 8080.ToString());
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void StopMongoose()
		{
			try
			{
				mongooseProcess.Kill();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void StartSilverlightPolicyServer(string pathname = null, bool showConsoleOutput = false)
		{
			// pathname = pathname ?? Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\SilverlightSecurityPolicyServer\bin\Debug\SilverlightPolicyServer.exe");

			try
			{
				var processStartInfo = new ProcessStartInfo
				{
					FileName = pathname,
					CreateNoWindow = true,
					UseShellExecute = showConsoleOutput,
				};

				policyServer = Process.Start(processStartInfo);

				Print("started Silverlight policy server on port {0}", 943);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void StopSilverlightPolicyServer()
		{
			try
			{
				policyServer.Kill();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void LaunchExplorer(string pathname)
		{
			// pathname = pathname ?? Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\SilverlightSecurityPolicyServer\bin\Debug\SilverlightPolicyServer.exe");

			try
			{
				var processStartInfo = new ProcessStartInfo
				{
					FileName = pathname,
				};

				explorer = Process.Start(processStartInfo);

				Print("launched gsDesign Explorer");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void StopProcesses()
		{
			StopSilverlightPolicyServer();
			StopRserve();
		}

		private void Print(string s, params object[] args)
		{
			Console.Out.WriteLine(string.Format(s, args));
		}
	}
}
