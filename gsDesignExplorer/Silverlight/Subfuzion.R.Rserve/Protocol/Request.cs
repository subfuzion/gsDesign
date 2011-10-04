namespace Subfuzion.R.Rserve.Protocol
{
	using System;

	public class Request
	{
		private Request(CommandCode commandCode, Payload payload = null)
		{
			CommandCode = commandCode;
			Payload = payload;
		}

		public CommandCode CommandCode { get; set; }

		public Payload Payload { get; set; }

		public int PayloadSize
		{
			get { return Payload != null ? Payload.PayloadSize : 0; }
		}

		public int RequestSize { get { return ProtocolHeader.HeaderSize + PayloadSize; } }

		public override string ToString()
		{
			return string.Format("(Request) CommandCode:{0}, PayloadCode:{1}, PayloadSize:{2}",
				CommandCode.ToString(),
				Payload != null ? Payload.PayloadCode : PayloadCode.Empty,
				PayloadSize);
		}

		/// <summary>
		/// TODO: currently only handles arrays with 2^32 elements
		/// (maximum size of array index)
		/// </summary>
		/// <returns></returns>
		public byte[] ToEncodedBytes()
		{
			var bytes = new byte[RequestSize];

			var header = ProtocolHeader.CreateRequestHeader(CommandCode, PayloadSize);

			// copy the header
			header.CopyToRequest(bytes);
			
			// copy the content data
			if (Payload != null)
			{
				Array.Copy(Payload.ToEncodedBytes(), 0, bytes, 16, PayloadSize);
			}

			return bytes;
		}

		#region Factory methods

		public static Request Eval(string s)
		{
			return new Request(CommandCode.Eval, Payload.FromString(s));
		}

		public static Request Shutdown()
		{
			return new Request(CommandCode.Shutdown);
		}

		public static Request CtrlShutdown()
		{
			return new Request(CommandCode.CtrlShutdown);
		}

		#endregion
	}
}