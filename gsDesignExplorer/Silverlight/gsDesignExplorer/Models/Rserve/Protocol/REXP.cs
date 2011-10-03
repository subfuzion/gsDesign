// ReSharper disable InconsistentNaming

namespace gsDesign.Explorer.Models.Rserve.Protocol
{
	using System;
	using System.Text;

	public class REXP
	{
		static readonly int ExpressionTypeLengthBytes = 1;
		static readonly int DataLengthBytes = 3;
		static readonly int REXPHeaderLengthBytes = ExpressionTypeLengthBytes + DataLengthBytes;

		private readonly ExpressionType _expressionType;
		private readonly byte[] _data;

		public REXP(ExpressionType expressionType, byte[] data)
		{
			_expressionType = expressionType;
			_data = data;
		}

		public ExpressionType ExpressionType
		{
			get { return _expressionType; }
		}

		public byte[] Data
		{
			get { return _data; }
		}

		public int DataLength
		{
			get { return _data != null ? _data.Length : 0; }
		}

		public byte[] ToEncodedBytes()
		{
			var contents = new byte[REXPHeaderLengthBytes + DataLength];
			contents[0] = (byte)ExpressionType;
			Array.Copy(BitConverter.GetBytes(DataLength), 0, contents, 1, 3);
			Array.Copy(Data, 0, contents, REXPHeaderLengthBytes, DataLength);

			return contents;
		}

		public static REXP FromBytes(byte[] bytes)
		{
			if (bytes == null || bytes.Length < REXPHeaderLengthBytes)
			{
				throw new ArgumentException("Can't convert bytes to REXP");
			}

			try
			{
				var type = (ExpressionType) bytes[0];

				var lengthBytes = new byte[4];
				Array.Copy(bytes, 1, lengthBytes, 0, DataLengthBytes);
				var length = BitConverter.ToInt32(lengthBytes, 0);

				var data = new byte[length];
				Array.Copy(bytes, REXPHeaderLengthBytes, data, 0, length);

				return new REXP(type, data);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public static REXP FromString(string s)
		{
			// Ensure server is using UTF8. See http://www.rforge.net/Rserve/doc.html "String encoding directive"
			var utf8 = Encoding.UTF8.GetBytes(s);
			var bytes = new byte[s.Length + 1];
			utf8.CopyTo(bytes, 0);
			bytes[s.Length] = 0;

			return new REXP(ExpressionType.StringVector, bytes);
		}
	}
}

// ReSharper restore InconsistentNaming
