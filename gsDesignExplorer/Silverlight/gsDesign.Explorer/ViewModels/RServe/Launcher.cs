namespace gsDesign.Explorer.ViewModels.RServe
{
	using System;
	using Subfuzion.Helpers.Win32;

	public class Launcher
	{
		//private Process rserveProcess;

		public void StartRserve(string pathname, bool showConsoleOutput = false)
		{
			// string pathname = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\thirdparty\rserve\inst\Rserve.exe");

			try
			{
				//var processStartInfo = new ProcessStartInfo
				//{
				//    FileName = pathname,
				//    Arguments = "--RS-port 4502",
				//    CreateNoWindow = true,
				//    UseShellExecute = showConsoleOutput,
				//};

				//rserveProcess = Process.Start(processStartInfo);



				ShellHelper.ShellExecute(IntPtr.Zero, ShellVerbs.OpenFile, pathname, null, null, ShowCommands.Hide);

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
				//rserveProcess.Kill();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void StopProcesses()
		{
			StopRserve();
		}

		private void Print(string s, params object[] args)
		{
			Console.Out.WriteLine(string.Format(s, args));
		}
	}
}
