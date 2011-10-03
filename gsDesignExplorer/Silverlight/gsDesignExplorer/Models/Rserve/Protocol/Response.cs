namespace gsDesign.Explorer.Models.Rserve.Protocol
{
	using System;

	public class Response
	{
		private readonly int _responseCode;

		public Response(Request request, byte[] responseBytes)
		{
			_responseCode = BitConverter.ToInt32(responseBytes, 0);
		}

		public bool IsOk
		{
			get { return (_responseCode & 15) == 1; }
		}

		public bool IsError
		{
			get { return (_responseCode & 15) == 2; }
		}

		public ErrorCode ErrorCode { get; set; }

		/// <summary>
		/// The raw data
		/// </summary>
		public byte[] Content { get; set; }

		public override string ToString()
		{
			return ErrorCode.ToString();
		}

		public byte[] ToBytes()
		{
			var errorCode = ErrorCode;
			var data = Content;
			var length = data != null ? data.Length : 0;

			var bytes = new byte[16];

			//BitConverter.GetBytes((int)command).CopyTo(bytes, 0);
			//BitConverter.GetBytes(length).CopyTo(bytes, 4);
			//BitConverter.GetBytes(0).CopyTo(bytes, 8);
			//BitConverter.GetBytes(0).CopyTo(bytes, 12);

			return bytes;
		}

		//public static Response ShutdownCommandMessage()
		//{
		//    return new Response { Command = Command.CtrlShutdown };
		//}
	}
}