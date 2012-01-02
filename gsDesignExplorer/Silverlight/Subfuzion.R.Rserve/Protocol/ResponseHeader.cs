namespace Subfuzion.R.Rserve.Protocol
{
	public class ResponseHeader : ProtocolHeader
	{
		internal static readonly int ResponseHeaderSize = 16;

		public bool IsOk
		{
			//get { return ((int)CommandCode & (int)CommandCode.ResponseOk) == (int)CommandCode.ResponseOk; }
			get { return ((int) CommandCode & 15) == 1; }
		}

		public bool IsError
		{
			//get { return ((int)CommandCode & (int)CommandCode.ResponseError) == (int)CommandCode.ResponseError; }
			get { return ((int) CommandCode & 15) == 2; }
		}

		public ErrorCode ErrorCode
		{
			get { return (ErrorCode)ErrorValue; }
		}

		public int ErrorValue
		{
			get { return (((int)CommandCode >> 24) & 0x7F); }
		}

		public override string ToString()
		{
			return string.Format("IsOk:{0} IsError:{1} Code:{2} ErrorCode:{3} PayloadSize:{4} PayloadOffset:{5} PayloadSize2:{6}",
				IsOk, IsError, CommandCode, ErrorCode, PayloadSize, PayloadOffset, PayloadSize2);
		}
	}
}
