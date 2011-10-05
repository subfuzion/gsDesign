namespace Subfuzion.R.Rserve.Protocol
{
	public class Response
	{
		public Response(Request request, byte[] responseBytes)
		{
			Request = request;

			Header = ProtocolHeader.CreateResponseHeader(responseBytes);

			if (Header.IsOk && Header.PayloadSize > 0)
			{
				var offset = ProtocolHeader.HeaderSize + Header.PayloadOffset;
				Payload = Payload.FromEncodedBytes(responseBytes, offset);
			}
			else
			{
				Payload = new Payload();
			}
		}

		public bool IsOk
		{
			get { return Header.IsOk; }
		}

		public bool IsError
		{
			get { return Header.IsError; }
		}

		public int ErrorCode
		{
			get { return Header.ErrorCode; }
		}

		/// <summary>
		/// The associated request
		/// </summary>
		public Request Request { get; private set; }

		/// <summary>
		/// The response header
		/// </summary>
		public ResponseHeader Header { get; private set; }

		/// <summary>
		/// The response payload
		/// </summary>
		public Payload Payload { get; private set; }

		public override string ToString()
		{
			return string.Format("(Response) ErrorCode:{0} for Command:{1} Payload:{2}", ErrorCode, Request.CommandCode, Payload);
		}
	}
}