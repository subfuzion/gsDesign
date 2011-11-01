namespace gsDesign.LauncherGUI.Services
{
	using System;
	using System.ServiceModel;
	using System.ServiceModel.Description;

	public class ServiceManager
	{
		private ServiceHost _rServiceHost;

		private static ServiceManager _instance = new ServiceManager();

		public static ServiceManager Instance { get { return _instance; } }

		public ServiceManager()
		{
		}

		public void StartServices()
		{
			try
			{
				Console.WriteLine("Starting RService");

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

				_rServiceHost = new ServiceHost(typeof(RService));
				_rServiceHost.Open(TimeSpan.FromSeconds(2));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public void StopServices()
		{
			try
			{
				Console.WriteLine("Stopping RService");
				_rServiceHost.Close(TimeSpan.FromSeconds(1));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

	}
}
