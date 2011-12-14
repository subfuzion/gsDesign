namespace gsDesign.Explorer.Services
{
	using System;
	using System.Configuration;
	using System.IO;
	using System.Reflection;
	using System.ServiceModel;
	using Subfuzion.Silverlight.Tcp;

	public class ServiceManager
	{
		public static readonly string DefaultPolicyFileKey = "DefaultPolicyFile";

		private static readonly ServiceManager _instance = new ServiceManager();
		private ServiceHost _rServiceHost;
		private SocketPolicyServer _policyServer;

		public static ServiceManager Instance
		{
			get { return _instance; }
		}

		// NOTE must set at command prompt: netsh http add urlacl url=http://+:4503/ user=DOMAIN\user
		public void StartServices()
		{
			try
			{
				StartPolicyServer();

				Console.WriteLine("Starting RService");

				// configure using code
				/*
				var baseAddress = new Uri("http://localhost:4503/");

				var rServiceUri = new Uri(baseAddress, "RService/");
				_rServiceHost = new ServiceHost(typeof(RService), rServiceUri);
				// _rServiceHost = new ServiceHost(typeof(RService));

				var smb = new ServiceMetadataBehavior
				          {HttpGetEnabled = true, MetadataExporter = {PolicyVersion = PolicyVersion.Policy15}};
				_rServiceHost.Description.Behaviors.Add(smb);

				var binding = new BasicHttpBinding();
				_rServiceHost.AddServiceEndpoint(typeof (IRService), binding, "");
				
				*/

				// configure using app.config
				_rServiceHost = new ServiceHost(typeof (RService));
				_rServiceHost.Open(TimeSpan.FromSeconds(2));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private void StartPolicyServer()
		{
			try
			{
				string policyFile = ConfigurationManager.AppSettings[DefaultPolicyFileKey];

				string currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? ".";
				policyFile = Path.Combine(currentDir, policyFile);
				_policyServer = new SocketPolicyServer(policyFile);
				_policyServer.Start();

				Console.WriteLine("Started Silverlight socket policy server on port 943. Press <Enter> to exit.");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void StopServices()
		{
			try
			{
				Console.WriteLine("Stopping RService");
				_rServiceHost.Close(TimeSpan.FromSeconds(1));

				_policyServer.Stop();
				Console.WriteLine("Stopped Silverlight socket policy server");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}