namespace Subfuzion.R.Rserve
{
	using System;
	using System.Diagnostics;
	using System.Net;
	using System.Net.Sockets;
	using System.Windows;
	using Helpers;
	using Protocol;

	public class RserveConnection : NotifyPropertyChangedBase
	{
		#region Fields

		private static readonly int DefaultBufferSize = 1*1024*1024;
		private readonly byte[] _messageBuffer = new byte[DefaultBufferSize];

		private int _pending;
		private Socket _socket;

		#endregion

		#region Properties

		private ConnectionState _connectionState;

		private bool _isBusy;
		private Action<object, object[]> _logger;

		private ProtocolSettings _protocolSettings;

		public ConnectionState ConnectionState
		{
			get { return _connectionState; }
			private set
			{
				if (_connectionState != value)
				{
					_connectionState = value;
					NotifyPropertyChanged("ConnectionState");
				}
			}
		}

		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				if (_isBusy != value)
				{
					_isBusy = value;
					NotifyPropertyChanged("IsBusy");
				}
			}
		}

		/// <summary>
		/// From the Rserve handshake
		/// </summary>
		/// <see cref="OnReceiveServerHandshake"/>
		public ProtocolSettings ProtocolSettings
		{
			get { return _protocolSettings; }
			private set
			{
				if (_protocolSettings != value)
				{
					_protocolSettings = value;
					NotifyPropertyChanged("ProtocolSettings");
				}
			}
		}

		public Action<object, object[]> Logger
		{
			get
			{
				if (_logger == null)
				{
					_logger = (msg, args) => Console.WriteLine(msg.ToString(), args);
				}

				return _logger;
			}
		}

		[Conditional("DEBUG")]
		private void Log(object msg)
		{
			Log(msg, null);
		}

		[Conditional("DEBUG")]
		private void Log(object msg, params object[] args)
		{
			if (Logger != null)
			{
				Logger("[RserveConnection] " + msg, args);
			}
		}

		#endregion

		private void OnSocketAsyncCompleted(object sender, SocketAsyncEventArgs socketAsyncEventArgs)
		{
			var callContext = socketAsyncEventArgs.UserToken as CallContext;
			if (callContext == null)
			{
				Log("CallContext can't be null when handling OnSocketAsyncCompleted event");
				throw new Exception("CallContext can't be null when handling OnSocketAsyncCompleted event");
			}

			var lastOperation = socketAsyncEventArgs.LastOperation;
			if (lastOperation != callContext.Operation)
			{
				var msg = string.Format("Operation mismatched. The reported last operation ({0}) does not match expected ({1})",
					lastOperation, callContext.Operation);
				Log(msg);
				throw new Exception(msg);
			}

			//if (socketAsyncEventArgs.BytesTransferred == 0)
			//{
			//    Log("Server disconnected (not expected), socket error: " + socketAsyncEventArgs.SocketError);
			//    Disconnect();
			//    ReturnError(callContext, ErrorCode.ConnectionBroken);
			//    return;
			//}

			if (socketAsyncEventArgs.SocketError != SocketError.Success)
			{
				Log("socket error: " + socketAsyncEventArgs.SocketError);
				Disconnect();
				ReturnError(callContext, ErrorCode.Success);
				return;
			}

			switch (lastOperation)
			{
				case SocketAsyncOperation.Connect:
					HandleConnectCompleted(socketAsyncEventArgs);
					break;

				case SocketAsyncOperation.Send:
					HandleSendCompleted(socketAsyncEventArgs);
					break;

				case SocketAsyncOperation.Receive:
					HandleReceiveCompleted(socketAsyncEventArgs);
					break;
			}
		}

		public void Disconnect(Action<ConnectionState, SocketError> callback = null)
		{
			try
			{
				if (_socket != null && _socket.Connected)
				{
					_socket.Close();
				}
			}
			catch (Exception e)
			{
				Log("Caught exception while attempting to disconnect: " + e);
			}
			finally
			{
				_socket = null;
				ConnectionState = ConnectionState.Disconnected;
				if (callback != null)
				{
					callback(ConnectionState, SocketError.NotConnected);
				}
			}
		}

		public void Connect(Action<ConnectionState, SocketError> callback = null)
		{
			const int port = 6311; // 4502 if not elevated trust

			Log("Connecting to Rserve on port {0}", port);

			if (ConnectionState == ConnectionState.Connected)
			{
				Log("Can't connect, already connected. Disconnect first");
				throw new Exception("Already connected. Disconnect first");
			}

			ConnectionState = ConnectionState.Connecting;

			// can't run on different thread and will be null anyway when launched from file system:
			// var endPoint = new DnsEndPoint(Application.Current.Host.Source.DnsSafeHost, 4502);
			var endPoint = new DnsEndPoint("localhost", port);

			_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
			          {
			          	// disable nagle's algorithm
			          	// http://msdn.microsoft.com/en-us/library/system.net.sockets.socket.nodelay.aspx
			          	NoDelay = true,
			          };

			var socketAsyncEventArgs = new SocketAsyncEventArgs
			           {
			           	RemoteEndPoint = endPoint,
			           	UserToken = new CallContext { Operation = SocketAsyncOperation.Connect, ConnectionAction = callback },
			           };

			socketAsyncEventArgs.Completed += OnSocketAsyncCompleted;
			if (!_socket.ConnectAsync(socketAsyncEventArgs))
			{
				OnSocketAsyncCompleted(_socket, socketAsyncEventArgs);
			}
		}

		private void HandleConnectCompleted(SocketAsyncEventArgs socketAsyncEventArgs)
		{
			if (socketAsyncEventArgs.SocketError != SocketError.Success)
			{
				Log("Connect completed unsuccessfully. SocketError={0}", socketAsyncEventArgs.SocketError);

				var callContext = socketAsyncEventArgs.UserToken as CallContext;
				if (callContext != null)
				{
					if (callContext.ConnectionAction != null)
					{
						callContext.ConnectionAction(ConnectionState.Disconnected, socketAsyncEventArgs.SocketError);
					}
				}

				return;
			}

			Log("Connection made, listening for Rserve handshake");

			var handshakeBuffer = new byte[32];
			socketAsyncEventArgs.SetBuffer(handshakeBuffer, 0, handshakeBuffer.Length);

			// the handshake gets a special handler
			socketAsyncEventArgs.Completed -= OnSocketAsyncCompleted;
			socketAsyncEventArgs.Completed += OnReceiveServerHandshake;

			//_socket.SendBufferSize = DefaultBufferSize;
			//_socket.ReceiveBufferSize = DefaultBufferSize;

			if (!_socket.ReceiveAsync(socketAsyncEventArgs))
			{
				OnReceiveServerHandshake(_socket, socketAsyncEventArgs);
			}
		}

		private void OnReceiveServerHandshake(object sender, SocketAsyncEventArgs socketAsyncEventArgs)
		{
			Log("Processing handshake");

			try
			{
				// restore the normal handler
				socketAsyncEventArgs.Completed -= OnReceiveServerHandshake;
				socketAsyncEventArgs.Completed += OnSocketAsyncCompleted;

				if (socketAsyncEventArgs.BytesTransferred == 0) // || socketAsyncEventArgs.SocketError != SocketError.Success)
				{
					// server disconnected -- SHOULD NOT SEE THIS ON A HANDSHAKE
					Log("Failed to receive Rserve handshake (Rserve disconnected). Resetting socket.");
					Disconnect();
					throw new Exception("Rserve disconnected during handshake");
				}

				ProtocolSettings = ProtocolSettings.Parse(socketAsyncEventArgs.Buffer);
				ConnectionState = socketAsyncEventArgs.SocketError == SocketError.Success
									? ConnectionState.Connected
									: ConnectionState.Disconnected;

				var callContext = socketAsyncEventArgs.UserToken as CallContext;
				if (callContext != null)
				{
					var callback = callContext.ConnectionAction as Action<ConnectionState, SocketError>;
					if (callback != null)
					{
						callback(ConnectionState, socketAsyncEventArgs.SocketError);
					}
				}
			}
			catch (Exception e)
			{
				Log(e);
			}
			finally
			{
				IsBusy = false;
			}
		}

		public void SendRequest(
			Request request,
			Action<Response, object> completed,
			Action<ErrorCode, object> error,
			object context)
		{
			try
			{
				if (ConnectionState != ConnectionState.Connected)
				{
					Log("Can't send request (not connected)");
					throw new Exception("Not connected");
				}

				if (IsBusy)
				{
					Log("Connection is busy (still waiting for response to last request");
					throw new Exception("Connection is busy (still waiting for reponse to last request");
				}

				IsBusy = true;

				var socketAsyncEventArgs = new SocketAsyncEventArgs
				                           {
				                           	UserToken = new CallContext
				                           	            {
				                           	            	CompletedAction = completed,
				                           	            	ErrorAction = error,
				                           	            	Request = request,
				                           	            	UserContext = context,
				                           	            	Operation = SocketAsyncOperation.Send,
				                           	            }
				                           };

				socketAsyncEventArgs.Completed += OnSocketAsyncCompleted;

				byte[] buffer = request.ToEncodedBytes();
				socketAsyncEventArgs.SetBuffer(buffer, 0, buffer.Length);

				if (!_socket.SendAsync(socketAsyncEventArgs))
				{
					OnSocketAsyncCompleted(_socket, socketAsyncEventArgs);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private void HandleSendCompleted(SocketAsyncEventArgs socketAsyncEventArgs)
		{
			Log("Send request completed");

			Listen(socketAsyncEventArgs);
		}

		private void Listen(SocketAsyncEventArgs socketAsyncEventArgs)
		{
			Log("Listening for response");

			var callContext = socketAsyncEventArgs.UserToken as CallContext;
			if (callContext == null)
			{
				throw new Exception("Listen: CallContext is null");
			}

			callContext.Operation = SocketAsyncOperation.Receive;

			var buffer = new byte[DefaultBufferSize];
			socketAsyncEventArgs.SetBuffer(buffer, 0, DefaultBufferSize);

			if (!_socket.ReceiveAsync(socketAsyncEventArgs))
			{
				OnSocketAsyncCompleted(_socket, socketAsyncEventArgs);
			}
		}

		private void HandleReceiveCompleted(SocketAsyncEventArgs socketAsyncEventArgs)
		{
			Log("Received response, parsing it");

			var callContext = socketAsyncEventArgs.UserToken as CallContext;
			if (callContext == null)
			{
				// this should never happen if invoked via OnSocketAsyncCompleted since it checks
				throw new Exception("HandleReceivedCompleted: CallContext is null");
			}

			if (socketAsyncEventArgs.Buffer[0] == 0)
			{
				for (int i = 1; i < 32; i++)
				{
					if (socketAsyncEventArgs.Buffer[i] != 0) break;
				}

				if (!_socket.ReceiveAsync(socketAsyncEventArgs))
				{
					OnSocketAsyncCompleted(_socket, socketAsyncEventArgs);
				}

				return;
			}


			var response = new Response(callContext.Request, socketAsyncEventArgs.Buffer);

			try
			{
				if (response.Payload.PayloadCode == PayloadCode.Empty)
				{
					IsBusy = false;
					SendRequest(callContext.Request, callContext.CompletedAction, callContext.ErrorAction, callContext);
					return;
				}

				if (response.Payload.PayloadCode == PayloadCode.Rexpression)
				{
					var rexp = Rexpression.FromBytes(response.Payload.Content);
				}
				else
				{
					
				}
			}
			catch (Exception e)
			{
				Log(e);
			}

			ReturnResult(callContext, response);
			IsBusy = false;
		}

		private void ReturnResult(CallContext context, Response response)
		{
			if (context != null)
			{
				if (context.CompletedAction != null)
				{
					// Deployment.Current.Dispatcher.BeginInvoke(() => context.CompletedAction(response, context.UserContext));
					context.CompletedAction(response, context.UserContext);
				}
			}
		}

		private void ReturnError(CallContext context, ErrorCode errorCode)
		{
			if (context != null)
			{
				if (context.ErrorAction != null)
				{
					// Deployment.Current.Dispatcher.BeginInvoke(() => context.ErrorAction(errorCode, context.UserContext));
					context.ErrorAction(errorCode, context.UserContext);
				}
			}
		}

	}
}
