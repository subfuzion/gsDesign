namespace Subfuzion.R.Rserve.Protocol
{
	using System;
	using System.Linq;
	using Helpers;

	public class Payload
	{
		public Payload() : this(PayloadCode.Empty, null)
		{
		}

		public Payload(PayloadCode payloadCode, byte[] content)
		{
			PayloadCode = payloadCode;
			Content = content;
		}

		public PayloadCode PayloadCode { get; private set; }

		public byte[] Content { get; private set; }

		public int ContentSize
		{
			get { return Content != null ? Content.Length : 0; }
		}

		public int PayloadSize
		{
			get { return 4 + ContentSize; }
		}

		public override string ToString()
		{
			return string.Format("(Payload) PayloadCode:{0}, ContentSize:{1}", PayloadCode, ContentSize);
		}

		public byte[] ToEncodedBytes()
		{
			var payload = new byte[PayloadSize];

			// set the data transport type
			payload[0] = (byte) PayloadCode;

			// set the length
			Array.Copy(BitConverter.GetBytes(ContentSize), 0, payload, 1, 3);

			// set the content
			if (Content != null)
			{
				Array.Copy(Content, 0, payload, 4, ContentSize);
			}

			return payload;
		}

		public static Payload FromEncodedBytes(byte[] rawBytes, int offset)
		{
			// get the data type (PayloadCode) of the payload content
			// (it will be either Rexpression or ByteStream)
			var transportCode = (PayloadCode) rawBytes[offset];

			var length = 0;

			if (transportCode != PayloadCode.Empty && Enum.IsDefined(typeof(PayloadCode), transportCode))
			{
				var lengthBytes = new byte[4];
				Array.Copy(rawBytes, offset + 1, lengthBytes, 0, 3);
				length = BitConverter.ToInt32(lengthBytes, 0);
			}

			var content = new byte[length];
			Array.Copy(rawBytes, offset + 4, content, 0, content.Length);

			return new Payload(transportCode, content);
		}

		public static Payload FromInteger(int value)
		{
			return new Payload(PayloadCode.Integer, BitConverter.GetBytes(value));
		}

		public static Payload FromCharacter(char value)
		{
			return new Payload(PayloadCode.Character, BitConverter.GetBytes(value));
		}


		public static Payload FromDouble(double value)
		{
			return new Payload(PayloadCode.Double, BitConverter.GetBytes(value));
		}

		public static Payload FromString(string value)
		{
			return new Payload(PayloadCode.String, value.GetBytes());
		}

		public static Payload ByteStream(byte[] bytes)
		{
			return new Payload(PayloadCode.ByteStream, bytes);
		}

		public static Payload FromRexpression(Rexpression value)
		{
			return new Payload(PayloadCode.Rexpression, value.ToEncodedBytes());
		}

		public static Payload FromArray(Rexpression[] objects)
		{
			if (objects == null || objects.Length == 0)
			{
				return new Payload(PayloadCode.Array, null);
			}

			// size of each rexpression is 1 byte for the type + data length
			int size = objects.Sum(rexpression => 1 + rexpression.DataSize);

			var bytes = new byte[size];

			for (int i = 0, offset = 0; i < objects.Length; i++)
			{
				var rexpBytes = objects[i].ToEncodedBytes();
				Array.Copy(rexpBytes, 0, bytes, offset, rexpBytes.Length);
				offset += rexpBytes.Length;
			}

			return new Payload(PayloadCode.Array, bytes);
		}
	}
}