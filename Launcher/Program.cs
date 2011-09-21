namespace Launcher
{
	using System;
	using System.Diagnostics;
	using System.IO;

	internal class Program
	{
		private Process mongooseProcess;
		private Process rserveProcess;

		private static void Main(string[] args)
		{
			var launcher = new Program();

			launcher.StartRserve();
			launcher.StartMongoose();

			launcher.Print("Press <enter> to quit");
			Console.ReadLine();

			launcher.mongooseProcess.Kill();
			launcher.rserveProcess.Kill();
		}

		private void StartRserve()
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\thirdparty\rserve\inst\Rserve.exe");

			var processStartInfo = new ProcessStartInfo {FileName = pathname, Arguments = "--RS-port 4502", CreateNoWindow = true};

			rserveProcess = Process.Start(processStartInfo);

			Print("started rserve on port {0}", 4502.ToString());
		}

		private void StartMongoose()
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\thirdparty\mongoose\mongoose.exe");

			var processStartInfo = new ProcessStartInfo {FileName = pathname, CreateNoWindow = true};

			mongooseProcess = Process.Start(processStartInfo);

			Print("started mongoose on port {0}", 8080.ToString());
		}

		private void Print(string s, params string[] args)
		{
			Console.Out.WriteLine(string.Format(s, args));
		}
	}
}