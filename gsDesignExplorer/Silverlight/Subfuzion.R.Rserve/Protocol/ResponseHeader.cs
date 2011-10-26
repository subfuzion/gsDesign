namespace Subfuzion.R.Rserve.Protocol
{
	using System;

	public class ResponseHeader : ProtocolHeader
	{
		public ResponseHeader(byte[] bytes)
		{
			if (bytes == null || bytes.Length < 16)
			{
				throw new ArgumentException("Not a valid response header");
			}

			Code = BitConverter.ToInt32(bytes, 0);
			PayloadSize = BitConverter.ToInt32(bytes, 4);
			PayloadOffset = BitConverter.ToInt32(bytes, 8);
			PayloadSize2 = BitConverter.ToInt32(bytes, 12);
		}

		public bool IsOk
		{
			get { return (Code & 0x01FFFF) == 0x010001; }
		}

		public bool IsError
		{
			get { return (Code & 0x01FFFF) == 0x010002; }
		}

		public ErrorCode ErrorCode
		{
			get { return (ErrorCode)ErrorValue; }
		}

		public int ErrorValue
		{
			get { return ((Code >> 24) & 0x7F); }
		}

		public override string ToString()
		{
			return string.Format("IsOk:{0} IsError:{1} Code:{2} ErrorCode:{3} PayloadSize:{4} PayloadOffset:{5} PayloadSize2:{6}",
				IsOk, IsError, Code, ErrorCode, PayloadSize, PayloadOffset, PayloadSize2);
		}
	}
}
