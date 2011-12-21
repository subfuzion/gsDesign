namespace gsDesign.Tests
{
	using System;
	using System.Net.Sockets;
	using System.Threading;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Subfuzion.R.Rserve;
	using Subfuzion.R.Rserve.Protocol;

	[TestClass]
	public class RserveTests
	{
		public static readonly TimeSpan DefaultTimeOut = TimeSpan.FromSeconds(5);
		//public static readonly TimeSpan DefaultTimeOut = TimeSpan.FromMinutes(1);

		private static RserveConnection NewConnection()
		{
			var waitHandle = new AutoResetEvent(false);
			var connection = new RserveConnection();

			connection.Connect((connectionState_, socketError_) => waitHandle.Set());
			waitHandle.WaitOne(DefaultTimeOut);

			if (connection.ConnectionState != ConnectionState.Connected)
			{
				throw new Exception("Connection failure");
			}

			return connection;
		}

		//[TestMethod]
		//public void Connect()
		//{
		//    var waitHandle = new AutoResetEvent(false);
		//    ConnectionState connectionState = ConnectionState.Disconnected;
		//    SocketError socketError = SocketError.SocketError;

		//    var connection = new RserveConnection();

		//    connection.Connect((connectionState_, socketError_) =>
		//    {
		//        connectionState = connectionState_;
		//        socketError = socketError_;
		//        waitHandle.Set();
		//    });

		//    waitHandle.WaitOne(DefaultTimeOut);

		//    Assert.IsTrue(connectionState == ConnectionState.Connected);
		//    Assert.IsTrue(connectionState == connection.ConnectionState);
		//    Assert.IsTrue(socketError == SocketError.Success);

		//    connection.Disconnect();
		//}

		//[TestMethod]
		//public void Disconnect()
		//{
		//    var waitHandle = new AutoResetEvent(false);

		//    var conn = NewConnection();
		//    conn.Disconnect((connectionState, socketError) => waitHandle.Set());
		//    waitHandle.WaitOne(DefaultTimeOut);

		//    Assert.IsTrue(conn.ConnectionState == ConnectionState.Disconnected);
		//}

		//[TestMethod]
		//public void VerifyProtocol()
		//{
		//    var conn = NewConnection();
		//    var ps = conn.ProtocolSettings;

		//    Assert.IsTrue(ps.Name == "QAP1");
		//    Assert.IsTrue(ps.Signature == "Rsrv");
		//    Assert.IsTrue(ps.Version == "0103");

		//    conn.Disconnect();
		//}

		[TestMethod]
		public void TestCommand1()
		{
			var waitHandle = new AutoResetEvent(false);

			var conn = NewConnection();

			var input = "1+1";
			//var input = "capture.output(1+1)";

			var request = Request.Eval(input);
			Response response = null;
			ErrorCode errorCode = ErrorCode.Success;

			var count = 0;
			for (var i = 0; i < 200; i++)
			{
				conn.SendRequest(
					request,

					(response_, o) =>
					{
						response = response_;
						waitHandle.Set();
					},

					(errorCode_, o) =>
					{
						errorCode = errorCode_;
						waitHandle.Set();
					},

					null);

				waitHandle.WaitOne(DefaultTimeOut);

				//Assert.IsNotNull(response);
				//Assert.IsTrue(errorCode == ErrorCode.Success);

				//Assert.IsTrue(response.Payload.PayloadCode == PayloadCode.Rexpression);

				//var rexp = Rexpression.FromBytes(response.Payload.Content);

				try
				{
					if (response.Payload.PayloadCode == PayloadCode.Rexpression)
					{
						count++;
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}


				//while (conn.IsBusy)
				//{
				//    Thread.Sleep(100);
				//}

				Thread.Sleep(50);
			}


			
			conn.Disconnect();
		}
	}
}