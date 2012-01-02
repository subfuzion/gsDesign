namespace Subfuzion.R.Rserve.Protocol
{
	using System;

	public class RequestHeader : ProtocolHeader
	{
		public RequestHeader(CommandCode commandCode, int payloadSize, int payloadOffset = 0, int payloadLength2 = 0)
		{
			CommandCode = commandCode;
			PayloadSize = payloadSize;
			PayloadOffset = payloadOffset;
			PayloadSize2 = payloadLength2;
		}

		public void CopyToRequest(byte[] requestBytes, int offset = 0)
		{
			BitConverter.GetBytes((int)CommandCode).CopyTo(requestBytes, offset);
			BitConverter.GetBytes(PayloadSize).CopyTo(requestBytes, offset + 4);
			BitConverter.GetBytes(PayloadOffset).CopyTo(requestBytes, offset + 8);
			BitConverter.GetBytes(PayloadSize2).CopyTo(requestBytes, offset + 12);
		}

		public override string ToString()
		{
			return string.Format("RequestHeader -> CommandCode:{0} PayloadSize:{1} PayloadOffset:{2} PayloadSize2:{3}",
				CommandCode, PayloadSize, PayloadOffset, PayloadSize2);
		}
	}
}
