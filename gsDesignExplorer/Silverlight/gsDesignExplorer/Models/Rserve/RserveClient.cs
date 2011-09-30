namespace gsDesign.Explorer.Models.Rserve
{
	using System;
	using System.Net;
	using System.Net.Sockets;
	using Helpers;

	public class RserveClient : NotifyPropertyChangedBase
	{
		private ConnectionState _connectionState;
		private Socket _socket;

		public ConnectionState ConnectionState
		{
			get { return _connectionState; }
			set
			{
				if (_connectionState != value)
				{
					_connectionState = value;
					RaisePropertyChanged("ConnectionState");
				}
			}
		}

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

			_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			var args = new SocketAsyncEventArgs {UserToken = _socket, RemoteEndPoint = endPoint};

			args.Completed += (sender, socketAsyncEventArgs) =>
			                  	{
			                  		ConnectionState = socketAsyncEventArgs.SocketError == SocketError.Success
			                  		                  	? ConnectionState.Connected
			                  		                  	: ConnectionState.Disconnected;
			                  	};

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
	}
}