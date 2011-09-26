using System;
using System.Net.Sockets;

namespace Subfuzion.Silverlight.Tcp
{
	public class ClientConnection
	{
		// The request string sent by the Silverlight plugin for a policy file
		public static readonly string PolicyRequestString = "<policy-file-request/>";

		private readonly TcpClient _client;
		private readonly Policy _policy;

		public ClientConnection(TcpClient client, Policy policy)
		{
			_client = client;
			_policy = policy;
		}

		public void HandleRequest()
		{
			var s = _client.GetStream();

			// Reads the policy request string, but doesn't actually check
			// (this method returns a policy file for every request and then closes the connnection).
			var buffer = new byte[PolicyRequestString.Length];
			_client.ReceiveTimeout = 5000;
			s.Read(buffer, 0, buffer.Length);

			s.Write(_policy.Bytes, 0, _policy.Length);
			_client.Close();
			Console.WriteLine("served policy file in response to request");
		}
	}
}
