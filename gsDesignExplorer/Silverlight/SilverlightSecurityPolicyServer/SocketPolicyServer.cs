using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Subfuzion.Silverlight.Tcp
{
	public class SocketPolicyServer
	{
		// Silverlight requires port 943
		public static readonly int PolicyServerPort = 943;

		private string _policyFileName;
		private byte[] _policy;

		private TcpListener _listener;
		private bool _isStopped = true;

		// caller should catch file exceptions
		public SocketPolicyServer(string policyFileName)
		{
			LoadPolicyFile(policyFileName);
		}

		// caller should catch socket exceptions
		public void Start()
		{
			if (_isStopped)
			{
				_listener = new TcpListener(IPAddress.Any, PolicyServerPort);
				_listener.Start();

				// This call returns immediately; waiting for a client to connect happens on a separate thread
				ListenForNewConnection();

				_isStopped = false;
				Console.WriteLine("start success");
			}
		}

		private void ListenForNewConnection()
		{
			_listener.BeginAcceptTcpClient(asyncResult =>
			                               	{
												if (_isStopped) return;

												// while we handle this request, continue listening for
												// another request on a separate thread
												ListenForNewConnection();

												// create a ClientConnection object to handle this request
												var client = _listener.EndAcceptTcpClient(asyncResult);
												var policyConnection = new ClientConnection(client, _policy);
												policyConnection.HandleRequest();
											}, null);
		}

		// caller should catch socket exceptions
		public void Stop()
		{
			_isStopped = true;
			_listener.Stop();
			Console.WriteLine("stop success");
		}

		// caller should catch file exceptions
		private void LoadPolicyFile(string policyFileName)
		{
			if (_policyFileName != policyFileName)
			{
				var policyStream = new FileStream(policyFileName, FileMode.Open);
				var policy = new byte[policyStream.Length];
				policyStream.Read(policy, 0, policy.Length);
				policyStream.Close();

				// successfully loaded new policy, so save information
				_policyFileName = policyFileName;
				_policy = policy;

				Console.WriteLine("loaded policy file: " + policyFileName);
			}
		}
	}
}
