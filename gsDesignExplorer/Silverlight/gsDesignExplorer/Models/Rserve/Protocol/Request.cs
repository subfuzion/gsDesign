namespace gsDesign.Explorer.Models.Rserve.Protocol
{
	using System;

	public class Request
	{
		public Command Command { get; set; }

		/// <summary>
		/// The raw data
		/// </summary>
		public byte[] Content { get; set; }

		public override string ToString()
		{
			return Command.ToString();
		}

		/// <summary>
		/// TODO: currently only handles arrays with 2^32 elements
		/// (maximum size of array index)
		/// </summary>
		/// <returns></returns>
		public byte[] ToBytes()
		{
			var command = Command;
			var data = Content;
			var length = data != null ? data.Length : 0;

			var bytes = new byte[16];

			BitConverter.GetBytes((int)command).CopyTo(bytes, 0);
			BitConverter.GetBytes(length).CopyTo(bytes, 4);
			BitConverter.GetBytes(0).CopyTo(bytes, 8);
			BitConverter.GetBytes(0).CopyTo(bytes, 12);

			return bytes;
		}

		public static Request ShutdownCommandMessage()
		{
			return new Request { Command = Command.Shutdown };
		}

		public static Request CtrlShutdownCommandMessage()
		{
			return new Request {Command = Command.CtrlShutdown};
		}
	}
}