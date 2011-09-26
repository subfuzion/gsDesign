using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Subfuzion.Silverlight.Tcp
{
	public class SocketPolicyServer
	{
		public static readonly IPAddress DefaultIPAddress = IPAddress.Any;

		// Silverlight requires port 943
		public static readonly int PolicyServerPort = 943;

		private ServerContext _serverContext;

		private Policy _policy;

		private TcpListener _listener;
		private bool _isStopped = true;

		public SocketPolicyServer(string policyFileName)
			: this(DefaultIPAddress, PolicyServerPort, policyFileName)
		{
		}

		// caller should catch file exceptions
		public SocketPolicyServer(IPAddress ipAddress, int port, string policyFileName)
		{
			LoadPolicyFile(policyFileName);
		}

		// caller should catch socket exceptions
		public void Start()
		{
			try
			{
				if (_isStopped)
				{
					_serverContext = new ServerContext {IPAddress = IPAddress.Any, Port = PolicyServerPort};

					_listener = new TcpListener(_serverContext.IPAddress, _serverContext.Port);
					_listener.Start();

					// This call returns immediately; waiting for a client to connect happens on a separate thread
					ListenForNewConnection();

					_isStopped = false;
					Console.WriteLine("start success");
				}
			}
			catch (SocketException e)
			{
				ServerException.ThrowServerException(e, _serverContext);
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
			_policy = new Policy(policyFileName);
			Console.WriteLine("loaded policy file: " + _policy.ToString());
		}
	}
}
