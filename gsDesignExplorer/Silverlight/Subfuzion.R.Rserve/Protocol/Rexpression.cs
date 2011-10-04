// ReSharper disable InconsistentNaming

namespace Subfuzion.R.Rserve.Protocol
{
	using System;
	using System.Text;

	public class Rexpression
	{
		static readonly int ExpressionTypeLengthBytes = 1;
		static readonly int DataLengthBytes = 3;
		static readonly int REXPHeaderLengthBytes = ExpressionTypeLengthBytes + DataLengthBytes;

		private readonly ExpressionCode _expressionCode;
		private readonly byte[] _data;

		public Rexpression(ExpressionCode expressionCode, byte[] data)
		{
			_expressionCode = expressionCode;
			_data = data;
		}

		public ExpressionCode ExpressionCode
		{
			get { return _expressionCode; }
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
			contents[0] = (byte)ExpressionCode;
			Array.Copy(BitConverter.GetBytes(DataLength), 0, contents, 1, 3);
			Array.Copy(Data, 0, contents, REXPHeaderLengthBytes, DataLength);

			return contents;
		}

		public static Rexpression FromBytes(byte[] bytes)
		{
			if (bytes == null || bytes.Length < REXPHeaderLengthBytes)
			{
				throw new ArgumentException("Can't convert bytes to Rexpression");
			}

			try
			{
				ExpressionCode type = (ExpressionCode) bytes[0];

				var lengthBytes = new byte[4];
				Array.Copy(bytes, 1, lengthBytes, 0, DataLengthBytes);
				var length = BitConverter.ToInt32(lengthBytes, 0);

				var data = new byte[length];
				Array.Copy(bytes, REXPHeaderLengthBytes, data, 0, length);

				return new Rexpression(type, data);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public static Rexpression FromString(string s)
		{
			// Ensure server is using UTF8. See http://www.rforge.net/Rserve/doc.html "String encoding directive"
			var utf8 = Encoding.UTF8.GetBytes(s);
			var bytes = new byte[s.Length + 1];
			utf8.CopyTo(bytes, 0);
			bytes[s.Length] = 0;

			return new Rexpression(ExpressionCode.StringVector, bytes);
		}
	}


}

// ReSharper restore InconsistentNaming
