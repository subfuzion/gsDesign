namespace Subfuzion.R.Rserve
{
	using System;
	using System.Net.Sockets;
	using Protocol;

	public class CallContext
	{
		/// <summary>
		/// Used to correlating and ordering the requests and responses
		/// (overflow is ok)
		/// </summary>
		public long ID { get; set; }

		/// <summary>
		/// The operation of the call context should match SocketAsyncEventArgs.LastOperation on the callback
		/// </summary>
		public SocketAsyncOperation Operation { get; set; }

		public Action<ConnectionState, SocketError> ConnectionAction { get; set; }

		/// <summary>
		/// The Rserve request, if there was one)
		/// </summary>
		public Request Request { get; set; }

		/// <summary>
		/// The Rserve response (if there was an Rserve request)
		/// </summary>
		public Action<Response, object> CompletedAction { get; set; }

		public Action<ErrorCode, object> ErrorAction { get; set; }

		/// <summary>
		/// Provided by the caller, if desired
		/// </summary>
		public object UserContext { get; set; }
	}
}
