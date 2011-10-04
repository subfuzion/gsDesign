namespace Subfuzion.R.Rserve.Protocol
{
	using System;

	public class RequestHeader : ProtocolHeader
	{
		public RequestHeader(CommandCode commandCode, int contentLength, int contentOffset = 0, int contentLength2 = 0)
		{
			Code = (int)commandCode;
			ContentLength = contentLength;
			ContentOffset = contentOffset;
			ContentLength2 = contentLength2;
		}

		public CommandCode CommandCode
		{
			get { return (CommandCode) Code; }
		}

		public void CopyTo(byte[] dest, int offset = 0)
		{
			BitConverter.GetBytes(Code).CopyTo(dest, offset);
			BitConverter.GetBytes(ContentLength).CopyTo(dest, offset + 4);
			BitConverter.GetBytes(ContentOffset).CopyTo(dest, offset + 8);
			BitConverter.GetBytes(ContentLength2).CopyTo(dest, offset + 12);
		}
	}
}
