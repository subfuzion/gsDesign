namespace Subfuzion.R.Rserve.Protocol
{
	using Helpers;

	public class Response
	{
		public byte[] RawBytes { get; set; }

		/// <summary>
		/// Gets true if the response is OK
		/// </summary>
		public bool IsOk
		{
			get { return Header.IsOk; }
		}

		/// <summary>
		/// Gets true if the response has an error
		/// </summary>
		public bool IsError
		{
			get { return Header.IsError; }
		}

		/// <summary>
		/// Gets the error (status) code
		/// </summary>
		public ErrorCode ErrorCode
		{
			get { return Header.ErrorCode; }
		}

		/// <summary>
		/// Gets the raw error value
		/// </summary>
		public int ErrorValue
		{
			get { return Header.ErrorValue; }
		}

		/// <summary>
		/// Gets the associated request
		/// </summary>
		public Request Request { get; internal set; }

		/// <summary>
		/// Gets the response header
		/// </summary>
		public ResponseHeader Header { get; internal set; }

		/// <summary>
		/// Gets the response payload
		/// </summary>
		public Payload Payload { get; internal set; }

		public override string ToString()
		{
			return string.Format("(Response) ErrorCode:{0} for Command:{1} Payload:{2}, Raw:{3}", ErrorCode, Request.CommandCode, Payload, RawBytes.GetUTF8String());
		}
	}
}