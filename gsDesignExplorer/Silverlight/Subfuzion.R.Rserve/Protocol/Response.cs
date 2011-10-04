namespace Subfuzion.R.Rserve.Protocol
{
	using System;
	using System.Text;

	public class Response
	{
		private readonly ResponseHeader _header;
		private REXP _rexp;

		public Response(Request request, byte[] responseBytes)
		{
			_header = ProtocolHeader.CreateResponseHeader(responseBytes);

			if (_header.IsOk && _header.ContentLength > 0)
			{
				// get the data type (DataTransferCode) of the content
				// it will be either Sexp or ByteStream

				var offset = ProtocolHeader.HeaderLength + _header.ContentOffset;

				var dataType = (DataTransportCode) responseBytes[offset];
				var lengthBytes = new byte[4];
				Array.Copy(responseBytes, offset + 1, lengthBytes, 0, 3);
				var length = BitConverter.ToInt32(lengthBytes, 0);

				Content = new byte[length];
				Array.Copy(responseBytes, offset + 4, Content, 0, Content.Length);

				if (dataType == DataTransportCode.Sexp)
				{
					_rexp = REXP.FromBytes(Content);
					var s = Encoding.UTF8.GetString(_rexp.Data, 0, _rexp.DataLength);
				}
			}
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