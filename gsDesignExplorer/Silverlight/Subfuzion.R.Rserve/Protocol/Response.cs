namespace Subfuzion.R.Rserve.Protocol
{
	using System;
	using Helpers;

	public class Response
	{
		private static byte[] _previousBuffer1 = new byte[1024];
		private static string _previousBuffer1str;

		private static byte[] _previousBuffer2 = new byte[1024];
		private static string _previousBuffer2str;

		private static byte[] _previousBuffer3 = new byte[1024];
		private static string _previousBuffer3str;

		public Response(Request request, byte[] responseBytes)
		{
			Request = request;

			Header = ProtocolHeader.CreateResponseHeader(responseBytes);

			if (Header.IsOk && Header.PayloadSize > 0)
			{
				var offset = ProtocolHeader.HeaderSize + Header.PayloadOffset;
				Payload = Payload.FromEncodedBytes(responseBytes, offset);
			}
			else
			{
				var previous3 = _previousBuffer3.GetUTF8String();
				var previous2 = _previousBuffer2.GetUTF8String();
				var previous1 = _previousBuffer1.GetUTF8String();
				var current = responseBytes.GetUTF8String();
				Payload = new Payload();
			}

			Array.Copy(_previousBuffer2, _previousBuffer3, 1024);
			_previousBuffer3str = _previousBuffer3.GetUTF8String();

			Array.Copy(_previousBuffer1, _previousBuffer2, 1024);
			_previousBuffer2str = _previousBuffer2.GetUTF8String();

			Array.Copy(responseBytes, _previousBuffer1, 1024);
			_previousBuffer1str = _previousBuffer1.GetUTF8String();
		}

		public bool IsOk
		{
			get { return Header.IsOk; }
		}

		public bool IsError
		{
			get { return Header.IsError; }
		}

		public ErrorCode ErrorCode
		{
			get { return Header.ErrorCode; }
		}

		public int ErrorValue
		{
			get { return Header.ErrorValue; }
		}

		/// <summary>
		/// The associated request
		/// </summary>
		public Request Request { get; private set; }

		/// <summary>
		/// The response header
		/// </summary>
		public ResponseHeader Header { get; private set; }

		/// <summary>
		/// The response payload
		/// </summary>
		public Payload Payload { get; private set; }

		public override string ToString()
		{
			return string.Format("(Response) ErrorCode:{0} for Command:{1} Payload:{2}", ErrorCode, Request.CommandCode, Payload);
		}
	}
}