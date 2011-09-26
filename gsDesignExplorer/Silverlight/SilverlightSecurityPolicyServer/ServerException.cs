using System;
using System.Net.Sockets;
using System.Runtime.Serialization;

namespace Subfuzion.Silverlight.Tcp
{
	public class ServerException : Exception
	{
		public ServerException()
		{
		}

		public ServerException(string message) : base(message)
		{
		}

		public ServerException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public static void ThrowServerException(SocketException e, ServerContext serverContext = null)
		{
			var ipAddress = serverContext != null ? serverContext.IPAddress.ToString() : "unknown";
			var port = serverContext != null ? serverContext.Port.ToString() : "-";

			if (ipAddress.Equals("0.0.0.0")) ipAddress = "any";

			if (e.Message.StartsWith("Only one usage of each socket address"))
				throw new ServerException(string.Format("Can't bind to socket on port {0} for IP addresses ({1}) because it is already in use", port, ipAddress), e);

			throw e;
		}
	}
}
