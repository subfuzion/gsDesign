namespace gsDesign.Explorer.Models.Rserve.Protocol
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
			ContentLength = BitConverter.ToInt32(bytes, 4);
			ContentOffset = BitConverter.ToInt32(bytes, 8);
			ContentLength2 = BitConverter.ToInt32(bytes, 12);
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
	}
}
