namespace gsDesign.Explorer.Models.Rserve
{
	using System;
	using System.Collections.Generic;
	using System.Net;
	using System.Net.Sockets;
	using Helpers;
	using Protocol;

	public class RserveClient : NotifyPropertyChangedBase
	{
		#region Fields

		private static readonly int DefaultBufferSize = 8*1024*1024; // 8 MB
		private readonly byte[] _messageBuffer = new byte[DefaultBufferSize];
		private ConnectionState _connectionState;

		private RserveProtocolSettings _rserveProtocolSettings;
		private Socket _socket;

		#endregion

		#region Properties

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

		public RserveProtocolSettings ProtocolSettings
		{
			get { return _rserveProtocolSettings; }
			private set
			{
				if (_rserveProtocolSettings != value)
				{
					_rserveProtocolSettings = value;
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

			// can't run on different thread and will be null anyway when launched from file system
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
			catch (Exception e)
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

				ProtocolSettings = RserveProtocolSettings.Parse(socketAsyncEventArgs.Buffer);

				socketAsyncEventArgs.Completed -= OnReceiveServerHandshake;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public void SendRequest(Request message)
		{
			try
			{
				//var args = new SocketAsyncEventArgs
				//            {
				//                BufferList = new List<ArraySegment<byte>>
				//                                {
				//                                    new ArraySegment<byte>(message.ToBytes()),
				//                                },
				//            };

				var args = new SocketAsyncEventArgs
				           	{
				           		UserToken = message
				           	};


				var buffer = message.ToBytes();
				args.SetBuffer(buffer, 0, buffer.Length);

				args.Completed += OnSocketAsyncCompleted;
				_socket.SendAsync(args);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private void OnSocketAsyncCompleted(object sender, SocketAsyncEventArgs socketAsyncEventArgs)
		{
			var lastOperation = socketAsyncEventArgs.LastOperation;

			switch (lastOperation)
			{
				case SocketAsyncOperation.None:
					// what is this?
					break;

				case SocketAsyncOperation.Send:
					try
					{
						socketAsyncEventArgs.Completed -= OnConnectAsyncCompleted;
						var args = new SocketAsyncEventArgs();
						args.SetBuffer(new byte[1024], 0, 1024);
						args.Completed += OnReceiveCompleted;
						_socket.ReceiveAsync(args);
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
					break;

				case SocketAsyncOperation.Receive:
					break;
			}
		}

		private void OnReceiveCompleted(object sender, SocketAsyncEventArgs socketAsyncEventArgs)
		{
			try
			{
				if (socketAsyncEventArgs.BytesTransferred == 0) // || socketAsyncEventArgs.SocketError != SocketError.Success)
				{
					// server disconnected
					Disconnect();
				}

				if (socketAsyncEventArgs.SocketError != SocketError.Success)
				{
					Console.WriteLine("socket error on response to: " + socketAsyncEventArgs.UserToken);
					return;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			var request = (Request) socketAsyncEventArgs.UserToken;
			var response = new Response(request, socketAsyncEventArgs.Buffer);
		}
	}
}