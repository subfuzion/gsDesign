namespace gsDesign.Tests
{
	using System;
	using System.Net.Sockets;
	using System.Threading;
	using System.Threading.Tasks;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Subfuzion.R.Rserve;

	[TestClass]
	public class RserveTests
	{
		public static readonly TimeSpan DefaultTimeOut = TimeSpan.FromSeconds(5);

		[TestMethod]
		public void Connect()
		{
			var waitHandle = new AutoResetEvent(false);
			ConnectionState connectionState = ConnectionState.Disconnected;
			SocketError socketError = SocketError.SocketError;

			var client = new RserveClient();

			client.Connect((connectionState_, socketError_) =>
			{
				connectionState = connectionState_;
				socketError = socketError_;
				waitHandle.Set();
			});

			waitHandle.WaitOne(DefaultTimeOut);

			client.Disconnect();

			Assert.IsTrue(connectionState == ConnectionState.Connected);
			Assert.IsTrue(socketError == SocketError.Success);
		}

		[TestMethod]
		public void Disconnect()
		{
			var waitHandle = new AutoResetEvent(false);
			ConnectionState connectionState = ConnectionState.Disconnected;
			SocketError socketError = SocketError.SocketError;

			var client = new RserveClient();

			client.Connect((connectionState_, socketError_) =>
			{
				connectionState = connectionState_;
				socketError = socketError_;
				waitHandle.Set();
			});

			waitHandle.WaitOne(DefaultTimeOut);
			Assert.IsTrue(connectionState == ConnectionState.Connected);
			Assert.IsTrue(socketError == SocketError.Success);

			client.Disconnect((connectionState_, socketError_) =>
			{
				connectionState = connectionState_;
				socketError = socketError_;
				waitHandle.Set();
			});

			waitHandle.WaitOne(DefaultTimeOut);
			Assert.IsTrue(connectionState == ConnectionState.Disconnected);
			Assert.IsTrue(socketError == SocketError.NotConnected);
		}

		[TestMethod]
		public void TestScript()
		{
			
		}
	}
}