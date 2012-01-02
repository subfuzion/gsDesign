namespace gsDesign.Tests
{
	using System;
	using System.Net.Sockets;
	using System.Text;
	using System.Threading;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Subfuzion.R.Rserve;
	using Subfuzion.R.Rserve.Protocol;

	[TestClass]
	public class RserveTests
	{
		public static readonly TimeSpan DefaultTimeOut = TimeSpan.FromSeconds(30);
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
		public void TestCommand()
		{
			var waitHandle = new AutoResetEvent(false);

			var conn = NewConnection();

			var template = "0 + {0}";
			//var input = "capture.output(1+1)";

			var count = 0;
			var max = 1000;
			var sb = new StringBuilder();

			for (var i = 0; i < max; i++)
			{
				var input = string.Format(template, i);
				var request = Request.Eval(input);
				Response response = null;
				ErrorCode errorCode = ErrorCode.Success;

				conn.SendRequest(
					request,

					(response_, o) =>
					{
						response = response_;
						if (response == null)
						{
							throw new Exception("This should never happen (and this exception won't get caught either)");
						}

						waitHandle.Set();
					},

					(errorCode_, o) =>
					{
						errorCode = errorCode_;
						waitHandle.Set();
					},

					null);

				waitHandle.WaitOne(DefaultTimeOut);
				if (response == null)
				{
					// timed out
					i--;
					continue;
				}

				//Assert.IsNotNull(response);
				//Assert.IsTrue(errorCode == ErrorCode.Success);

				//Assert.IsTrue(response.Payload.PayloadCode == PayloadCode.Rexpression);

				//var rexp = Rexpression.FromBytes(response.Payload.Content);

				try
				{
					var payloadCode = response.Payload.PayloadCode;
					if (payloadCode == PayloadCode.Rexpression)
					{
						count++;

						var rexp = ProtocolParser.ParseRexpression(response.Payload.Content);
						if (rexp.IsDoubleList)
						{
							var list = rexp.ToDoubleList();

							foreach (var d in list)
							{
								sb.Append(string.Format("{0}, ", d));
							}
						}
						else if (rexp.IsIntegerList)
						{
							var list = rexp.ToIntegerList();

							foreach (var n in list)
							{
								sb.Append(string.Format("{0}, ", n));
							}
						}
					}
					else if (payloadCode == PayloadCode.Integer)
					{
						var resp = ProtocolParser.ParseResponse(response.Request, response.RawBytes);
						throw new Exception("PayloadCode.Integer");
					}
					else
					{
						throw new Exception("Invalid response");
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

				//Thread.Sleep(50);
			}

			var s = sb.ToString();

			Assert.AreEqual(count, max);

			var elements = s.Split(new string[] { ", " }, StringSplitOptions.None);

			for (int i = 0; i < max; i++)
			{
				var n = double.Parse(elements[i]);
				Assert.AreEqual(n, i);
			}

			conn.Disconnect();
		}
	}
}