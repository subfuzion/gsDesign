namespace gsDesign.Explorer.Models.Rserve.Protocol
{
	using System;

	public class Response
	{
		private readonly ResponseHeader _header;
		private REXP _rexp;

		public Response(Request request, byte[] responseBytes)
		{
			_header = ProtocolHeader.CreateResponseHeader(responseBytes);
			Content = new byte[responseBytes.Length - 16];
			Array.Copy(responseBytes, 16, Content, 0, Content.Length);
			_rexp = REXP.FromBytes(Content);
		}

		public bool IsOk
		{
			get { return _header.IsOk; }
		}

		public bool IsError
		{
			get { return _header.IsError; }
		}

		public int ErrorCode
		{
			get { return _header.ErrorCode; }
		}

		/// <summary>
		/// The raw data
		/// </summary>
		public byte[] Content { get; set; }

		public override string ToString()
		{
			return ErrorCode.ToString();
		}
	}
}