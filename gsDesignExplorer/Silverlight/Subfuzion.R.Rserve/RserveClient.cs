namespace Subfuzion.R.Rserve
{
	using System;
	using System.Net;
	using System.Net.Sockets;
	using System.Windows;
	using Helpers;
	using Protocol;

	/*
		Rserve communication is done over any reliable connection-oriented
		protocol (usually TCP/IP or local sockets). After the connection is
		established, the server sends 32 bytes of ID-string defining the
		capabilities of the server. Each attribute of the ID-string is 4 bytes
		long and is meant to be user-readable (i.e. don't use special characters),
		and it's a good idea to make "\r\n\r\n" the last attribute

		the ID string must be of the form:

		[0] "Rsrv" - R-server ID signature
		[4] "0100" - version of the R server
		[8] "QAP1" - protocol used for communication (here Quad Attributes Packets v1)
		[12] any additional attributes follow. \r\n<space> and '-' are ignored.

		optional attributes
		(in any order; it is legitimate to put dummy attributes, like "----" or
		"    " between attributes):

		"R151" - version of R (here 1.5.1)
		"ARpt" - authorization required (here "pt"=plain text, "uc"=unix crypt,
				 "m5"=MD5)
				 connection will be closed if the first packet is not CMD_login.
				 if more AR.. methods are specified, then client is free to
				 use the one he supports (usually the most secure)
		"K***" - key if encoded authentification is challenged (*** is the key)
				 for unix crypt the first two letters of the key are the salt
				 required by the server
	*/

	public class RserveClient : NotifyPropertyChangedBase
	{
		#region Fields

		private static readonly int DefaultBufferSize = 8*1024*1024; // 8 MB
		private readonly byte[] _messageBuffer = new byte[DefaultBufferSize];
		private ConnectionState _connectionState;

		private ProtocolSettings _protocolSettings;
		private Socket _socket;

		private int _pending;
		private bool _isBusy;

		#endregion

		#region Properties

		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				if (_isBusy != value)
				{
					_isBusy = value;
					RaisePropertyChanged("IsBusy");
				}
			}
		}

		public ConnectionState ConnectionState
		{
			get { return _connectionState; }
			private set
			{
				if (_connectionState != value)
				{
					_connectionState = value;
					RaisePropertyChanged("ConnectionState");
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
					RaisePropertyChanged("ProtocolSettings");
				}
			}
		}

		#endregion

		public void ToggleConnect()
		{
			if (ConnectionState == ConnectionState.Connected)
			{
				Disconnect();
			}
			else
			{
				Connect();
			}
		}

		public void Connect()
		{
			Disconnect();

			// can't run on different thread and will be null anyway when launched from file system:
			// var endPoint = new DnsEndPoint(Application.Current.Host.Source.DnsSafeHost, 4502);
			var endPoint = new DnsEndPoint("localhost", 4502);

			_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
			          	{
			          		// disable nagle's algorithm
			          		// http://msdn.microsoft.com/en-us/library/system.net.sockets.socket.nodelay.aspx
			          		NoDelay = true,
			          	};

			var args = new SocketAsyncEventArgs
			           	{
			           		RemoteEndPoint = endPoint
			           	};

			args.Completed += OnConnectAsyncCompleted;

			_socket.ConnectAsync(args);
		}

		public void Disconnect()
		{
			try
			{
				if (_socket != null && _socket.Connected)
				{
					_socket.Close();
					_socket = null;
				}
			}
			catch
			{
				// might want to log this
			}
			finally
			{
				ConnectionState = ConnectionState.Disconnected;
			}
		}

		private void OnConnectAsyncCompleted(object sender, SocketAsyncEventArgs socketAsyncEventArgs)
		{
			ConnectionState = socketAsyncEventArgs.SocketError == SocketError.Success
			                  	? ConnectionState.Connected
			                  	: ConnectionState.Disconnected;

			if (ConnectionState == ConnectionState.Connected)
			{
				var handshakeBuffer = new byte[32];
				socketAsyncEventArgs.SetBuffer(handshakeBuffer, 0, handshakeBuffer.Length);
				socketAsyncEventArgs.Completed -= OnConnectAsyncCompleted;
				socketAsyncEventArgs.Completed += OnReceiveServerHandshake;

				//_socket.SendBufferSize = DefaultBufferSize;
				//_socket.ReceiveBufferSize = DefaultBufferSize;
				_socket.ReceiveAsync(socketAsyncEventArgs);
			}
		}

		private void OnReceiveServerHandshake(object sender, SocketAsyncEventArgs socketAsyncEventArgs)
		{
			try
			{
				if (socketAsyncEventArgs.BytesTransferred == 0) // || socketAsyncEventArgs.SocketError != SocketError.Success)
				{
					// server disconnected
					Disconnect();
				}

				ProtocolSettings = ProtocolSettings.Parse(socketAsyncEventArgs.Buffer);

				socketAsyncEventArgs.Completed -= OnReceiveServerHandshake;

				IsBusy = false;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public void SendFileRequest()
		{
		}

		public void SendRequest(
			Request request,
			Action<Response, object> completed,
			Action<ErrorCode, object> error,
			object context)
		{
			try
			{
				if (IsBusy || ConnectionState != ConnectionState.Connected) return;
				IsBusy = true;

				//var args = new SocketAsyncEventArgs
				//            {
				//				  UserToken = new RequestContext {Request = request},
				//                BufferList = new List<ArraySegment<byte>>
				//                                {
				//                                    new ArraySegment<byte>(request.ToEncodedBytes()),
				//                                },
				//            };

				//socketAsyncEventArgs.UserToken = new RequestContext
				//{
				//    CompletedAction = completed,
				//    ErrorAction = error,
				//    Context = context,
				//    Request = request,
				//};

				var socketAsyncEventArgs = new SocketAsyncEventArgs
							{
								UserToken = new RequestContext
												{
													CompletedAction = completed,
													ErrorAction = error,
													Context = context,
													Request = request,
												},
							};

				byte[] buffer = request.ToEncodedBytes();
				socketAsyncEventArgs.SetBuffer(buffer, 0, buffer.Length);

				socketAsyncEventArgs.Completed += OnSocketAsyncCompleted;
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

		private void OnSocketAsyncCompleted(object sender, SocketAsyncEventArgs socketAsyncEventArgs)
		{
			SocketAsyncOperation lastOperation = socketAsyncEventArgs.LastOperation;

			switch (lastOperation)
			{
				case SocketAsyncOperation.None:
					// what is this?
					break;

				case SocketAsyncOperation.Send:
					try
					{
						socketAsyncEventArgs.Completed -= OnConnectAsyncCompleted;
						socketAsyncEventArgs.Completed -= OnReceiveCompleted;
						// var args = new SocketAsyncEventArgs {UserToken = socketAsyncEventArgs.UserToken};
						socketAsyncEventArgs.SetBuffer(new byte[DefaultBufferSize], 0, DefaultBufferSize);
						socketAsyncEventArgs.Completed += OnReceiveCompleted;
						if (!_socket.ReceiveAsync(socketAsyncEventArgs))
						{
							OnReceiveCompleted(_socket, socketAsyncEventArgs);
						}
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
					break;

				case SocketAsyncOperation.Receive:
					OnReceiveCompleted(_socket, socketAsyncEventArgs);
					break;
			}
		}

		private void OnReceiveCompleted(object sender, SocketAsyncEventArgs socketAsyncEventArgs)
		{
			var context = (RequestContext)socketAsyncEventArgs.UserToken;

			try
			{
				if (socketAsyncEventArgs.BytesTransferred == 0) // || socketAsyncEventArgs.SocketError != SocketError.Success)
				{
					// server disconnected
					Disconnect();
					ReturnError(context, ErrorCode.ConnectionBroken);
					return;
				}

				if (socketAsyncEventArgs.SocketError != SocketError.Success)
				{
					Console.WriteLine("socket error");
					ReturnError(context, ErrorCode.Unknown);
					return;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}


			if (socketAsyncEventArgs.Buffer[0] == 0)
			{
				for (int i = 1; i < 32; i++)
				{
					if (socketAsyncEventArgs.Buffer[i] != 0) break;
				}

				if (!_socket.ReceiveAsync(socketAsyncEventArgs))
				{
					OnReceiveCompleted(_socket, socketAsyncEventArgs);
				}
				return;
			}

			if (context != null)
			{
				// TODO need to handle when Rserve is going to send more data than the buffer size
				var response = new Response(context.Request, socketAsyncEventArgs.Buffer);
				ReturnResult(context, response);
				IsBusy = false;
			}
		}

		private void ReturnResult(RequestContext context, Response response)
		{
			if (context != null)
			{
				if (context.CompletedAction != null)
				{
					Deployment.Current.Dispatcher.BeginInvoke(() => context.CompletedAction(response, context.Context));
				}
			}
		}

		private void ReturnError(RequestContext context, ErrorCode errorCode)
		{
			if (context != null)
			{
				if (context.ErrorAction != null)
				{
					Deployment.Current.Dispatcher.BeginInvoke(() => context.ErrorAction(errorCode, context.Context));
				}
			}
		}

	}

	public class RequestContext
	{
		public Action<Response, object> CompletedAction { get; set; }
		public Action<ErrorCode, object> ErrorAction { get; set; }
		public object Context { get; set; }
		public Request Request { get; set; }
	}

	public class FileRequestContext
	{
		
	}
}