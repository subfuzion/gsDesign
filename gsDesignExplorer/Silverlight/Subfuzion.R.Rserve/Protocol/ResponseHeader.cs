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
			get { return (Code & 15) == 1; }
		}

		public bool IsError
		{
			get { return (Code & 15) == 2; }
		}

		public int ErrorCode
		{
			get { return ((Code >> 24) & 127); }
		}

		public override string ToString()
		{
			return string.Format("ResponseHeader -> IsOk:{0} IsError:{1} ErrorCode:{2} PayloadSize:{3} PayloadOffset:{4} PayloadSize2:{5}",
				IsOk, IsError, (CommandCode)Code, PayloadSize, PayloadOffset, PayloadSize2);
		}
	}
}
