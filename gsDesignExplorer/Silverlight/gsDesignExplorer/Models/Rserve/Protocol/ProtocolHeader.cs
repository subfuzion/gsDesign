namespace gsDesign.Explorer.Models.Rserve.Protocol
{
	public abstract class ProtocolHeader
	{
		public static RequestHeader CreateRequestHeader(Command command, int contentLength, int contentOffset = 0, int contentLength2 = 0)
		{
			return new RequestHeader(command, contentLength, contentOffset, contentLength2);
		}

		public static ResponseHeader CreateResponseHeader(byte[] bytes)
		{
			return new ResponseHeader(bytes);
		}

		public static readonly int HeaderLength = 16;

		public int Code { get; set; }

		public int ContentLength { get; set; }

		public int ContentOffset { get; set; }

		public int ContentLength2 { get; set; }

	}
}
