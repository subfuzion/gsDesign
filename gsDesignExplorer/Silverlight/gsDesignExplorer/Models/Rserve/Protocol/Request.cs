namespace gsDesign.Explorer.Models.Rserve.Protocol
{
	using System;
	using System.Text;

	public class Request
	{
		public Request()
		{
		}

		public Request(Command command, byte[] content = null)
		{
			Command = command;
			Content = content;
		}

		public Command Command { get; set; }

		/// <summary>
		/// The raw data
		/// </summary>
		public byte[] Content { get; set; }

		public int ContentLength
		{
			get { return Content != null ? Content.Length : 0; }
		}

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
			var bytes = new byte[ProtocolHeader.HeaderLength + ContentLength];

			var header = ProtocolHeader.CreateRequestHeader(Command, ContentLength);

			// copy the header
			header.CopyTo(bytes);
			
			// copy the content data
			if (Content != null)
			{
				Array.Copy(Content, 0, bytes, 16, ContentLength);
			}

			return bytes;
		}

		#region Factory methods

		public static Request Eval(string s)
		{
			var utf8 = Encoding.UTF8.GetBytes(s);
			var bytes = new byte[s.Length + 1];
			utf8.CopyTo(bytes, 0);
			bytes[s.Length] = 0;

			var type = DataTransportType.String;
			var length = bytes.Length;

			var contents = new byte[4 + length];
			contents[0] = (byte)type;
			Array.Copy(BitConverter.GetBytes(length), 0, contents, 1, 3);
			Array.Copy(bytes, 0, contents, 4, bytes.Length);


			//var rexp = REXP.FromString(s).ToEncodedBytes();

			//var contents = new byte[4 + rexp.Length];
			//contents[0] = (byte)DataTransportType.Sexp;
			//Array.Copy(BitConverter.GetBytes(rexp.Length), 0, contents, 1, 3);
			//Array.Copy(rexp, 0, contents, 4, rexp.Length);

			return new Request(Command.Eval)
			       	{
						Content = contents,
			       	};
		}

		public static Request Shutdown()
		{
			return new Request {Command = Command.Shutdown};
		}

		public static Request CtrlShutdown()
		{
			return new Request {Command = Command.CtrlShutdown};
		}

		#endregion
	}
}