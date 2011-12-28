namespace Subfuzion.R.Rserve.Protocol
{
	using Helpers;

	public class Response
	{
		public Response(Request request, byte[] responseBytes)
		{
			RawBytes = responseBytes;

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

		public byte[] RawBytes { get; private set; }

		public bool IsOk
		{
			get { return Header.IsOk; }
		}

		public bool IsError
		{
			get { return Header.IsError; }
		}

		public ErrorCode ErrorCode
		{
			get { return Header.ErrorCode; }
		}

		public int ErrorValue
		{
			get { return Header.ErrorValue; }
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
			return string.Format("(Response) ErrorCode:{0} for Command:{1} Payload:{2}, Raw:{3}", ErrorCode, Request.CommandCode, Payload, RawBytes.GetUTF8String());
		}
	}
}