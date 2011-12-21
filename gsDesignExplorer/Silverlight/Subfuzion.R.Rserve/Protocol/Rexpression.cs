// ReSharper disable InconsistentNaming

namespace Subfuzion.R.Rserve.Protocol
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	public class Rexpression
	{
		static readonly int TypeHeaderSize = 1;
		static readonly int DataSizeHeaderSize = 3;
		static readonly int RexpressionHeaderSize = TypeHeaderSize + DataSizeHeaderSize;

		private readonly RexpressionType _rexpressionType;
		private readonly byte[] _data;

		public Rexpression(RexpressionType rexpressionType, byte[] data)
		{
			_rexpressionType = rexpressionType;
			_data = data;
		}

		public RexpressionType RexpressionType
		{
			get { return _rexpressionType; }
		}

		internal byte[] Data
		{
			get { return _data; }
		}

		internal int DataSize
		{
			get { return _data != null ? _data.Length : 0; }
		}

		public byte[] ToEncodedBytes()
		{
			var contents = new byte[RexpressionHeaderSize + DataSize];
			contents[0] = (byte)RexpressionType;
			Array.Copy(BitConverter.GetBytes(DataSize), 0, contents, 1, 3);
			Array.Copy(Data, 0, contents, RexpressionHeaderSize, DataSize);

			return contents;
		}

		public static Rexpression FromBytes(byte[] bytes)
		{
			if (bytes == null || bytes.Length < RexpressionHeaderSize)
			{
				throw new ArgumentException();
			}

			try
			{
				var type = (RexpressionType) bytes[0];

				var lengthBytes = new byte[4];
				Array.Copy(bytes, 1, lengthBytes, 0, DataSizeHeaderSize);
				var length = BitConverter.ToInt32(lengthBytes, 0);

				var data = new byte[length];
				Array.Copy(bytes, RexpressionHeaderSize, data, 0, length);

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

			return new Rexpression(RexpressionType.StringVector, bytes);
		}

		#region Conversions

		#region String List

		public bool IsStringList
		{
			get { return RexpressionType == RexpressionType.StringArray || RexpressionType == RexpressionType.StringVector; }
		}

		public List<string> ToStringList()
		{
			if (!IsStringList)
			{
				throw new InvalidOperationException("expression is not a StringArray or StringVector");
			}

			var list = new List<string>();
			var totalStringCount = 0;

			// count how many strings in data
			for (int i = 0; i < DataSize; i++)
			{
				if (Data[i] == 0) totalStringCount++;
			}

			var sb = new StringBuilder();
			for (int i = 0, count = 0; count < totalStringCount; i++)
			{
				if (Data[i] == 0)
				{
					list.Add(sb.ToString());
					sb.Clear();
					count++;
					continue;
				}

				sb.Append((char) Data[i]);
			}

			return list;
		}

		public override string ToString()
		{
			return string.Join("\n", ToStringList());
		}

		#endregion

		#region Double List

		public bool IsDoubleList
		{
			get { return RexpressionType == RexpressionType.DoubleArray; }
		}

		public List<double> ToDoubleList()
		{
			if (!IsDoubleList)
			{
				throw new InvalidOperationException("expression is not a DoubleArray");
			}

			var list = new List<double>();
			var totalDoubleCount = DataSize/sizeof (double);

			for (int offset = 0; offset < DataSize; offset += sizeof(double))
			{
				list.Add(BitConverter.ToDouble(Data, offset));
			}

			return list;
		}

		#endregion

		#endregion

	}
}

// ReSharper restore InconsistentNaming
