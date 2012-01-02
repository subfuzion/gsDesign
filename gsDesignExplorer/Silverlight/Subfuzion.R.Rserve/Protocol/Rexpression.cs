// ReSharper disable InconsistentNaming

namespace Subfuzion.R.Rserve.Protocol
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using Helpers;

	public class Rexpression
	{
		internal static readonly int TypeHeaderSize = 1;
		internal static readonly int DataSizeHeaderSize = 3;
		internal static readonly int RexpressionHeaderSize = TypeHeaderSize + DataSizeHeaderSize;

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

		public bool HasAttribute { get; internal set; }

		public Rexpression Attribute { get; internal set; }

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

		//public static Rexpression FromBytes(byte[] bytes)
		//{
		//    if (bytes == null || bytes.Length < RexpressionHeaderSize)
		//    {
		//        throw new ArgumentException();
		//    }

		//    try
		//    {
		//        var type = (RexpressionType) (bytes[0] & 0x3F);
		//        var hasAttribute = (bytes[0] & (byte) RexpressionType.HasAttribute) == (byte) RexpressionType.HasAttribute;

		//        // TODO: handle long data
		//        var lengthBytes = new byte[4];
		//        Array.Copy(bytes, 1, lengthBytes, 0, DataSizeHeaderSize);
		//        var length = BitConverter.ToInt32(lengthBytes, 0);

		//        var data = new byte[length];
		//        Array.Copy(bytes, RexpressionHeaderSize, data, 0, length);

		//        return new Rexpression(type, data) {HasAttribute = hasAttribute};
		//    }
		//    catch (Exception e)
		//    {
		//        Console.WriteLine(e);
		//        throw;
		//    }
		//}

		public static Rexpression FromString(string s)
		{
			// Ensure server is using UTF8. See http://www.rforge.net/Rserve/doc.html "String encoding directive"
			var utf8 = Encoding.UTF8.GetBytes(s);
			var bytes = new byte[s.Length + 1];
			utf8.CopyTo(bytes, 0);
			bytes[s.Length] = 0;

			return new Rexpression(RexpressionType.StringVector, bytes);
		}

		public override string ToString()
		{
			// return string.Join("\n", ToStringList());
			return ToFormattedString().Trim();
		}

		public string ToFormattedString(string separator = "\n", bool recursive = true)
		{
			var sb = new StringBuilder();

			if (IsVector)
			{
				var list = ToVector();

				foreach (var r in list)
				{
					sb.Append(r.ToFormattedString()).Append(separator);
				}
			}

			if (IsStringList)
			{
				var list = ToStringList();

				foreach (var s in list)
				{
					sb.Append(s).Append(separator);
				}
			}

			if (IsDoubleList)
			{
				var list = ToDoubleList();

				foreach (var d in list)
				{
					sb.Append(d).Append(separator);
				}
			}

			if (IsBoolArray)
			{
				var list = ToBoolArray();

				foreach (var b in list)
				{
					sb.Append(b).Append(separator);
				}
			}

			if (IsListTags)
			{
				var list = ToListTags();

				foreach (var tag in list)
				{
					sb.Append(tag.Name.ToFormattedString()).Append('=').Append(tag.Value.ToFormattedString()).Append('\n');
				}
			}

			if (IsSymbolName)
			{
				sb.Append(ToSymbolName()).Append(separator);
			}

			return sb.ToString();
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

		#endregion

		#region Vector

		public bool IsVector
		{
			get { return RexpressionType == RexpressionType.Vector || RexpressionType == RexpressionType.ExpressionVector; }
		}

		public List<Rexpression> ToVector()
		{
			if (!IsVector)
			{
				throw new InvalidOperationException("expression is not a vector");
			}

			var list = new List<Rexpression>();

			var offset = 0;
			var size = DataSize;

			while (offset < size)
			{
				var rexp = ProtocolParser.ParseRexpression(Data, ref offset);
				list.Add(rexp);
			}

			return list;
		}

		#endregion

		#region Bool Array

		public bool IsBoolArray
		{
			get { return RexpressionType == RexpressionType.BoolArray; }
		}

		public List<bool> ToBoolArray()
		{
			if (!IsBoolArray)
			{
				throw new InvalidOperationException("expression is not a bool array");
			}

			var list = new List<bool>();
			var count = DataSize/sizeof (byte);

			for (int offset = 0; offset < DataSize; offset += sizeof(byte))
			{
				list.Add(BitConverter.ToBoolean(Data, offset));
			}

			return list;
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

		#region Integer List

		public bool IsIntegerList
		{
			get { return RexpressionType == RexpressionType.IntegerArray; }
		}

		public List<int> ToIntegerList()
		{
			if (!IsIntegerList)
			{
				throw new InvalidOperationException("expression is not an IntegerArray");
			}

			var list = new List<int>();
			var totalIntegerCount = DataSize / sizeof(int);

			for (int offset = 0; offset < DataSize; offset += sizeof(int))
			{
				list.Add(BitConverter.ToInt32(Data, offset));
			}

			return list;
		}

		#endregion

		#region List Tag

		public bool IsListTags
		{
			get { return RexpressionType == RexpressionType.ListTags; }
		}

		public List<Tag> ToListTags()
		{
			if (!IsListTags)
			{
				throw new InvalidOperationException("expression is not a list of tags");
			}

			var list = new List<Tag>();

			var offset = 0;
			var size = DataSize;

			while (offset < size)
			{
				var name = ProtocolParser.ParseRexpression(Data, ref offset);
				var value = ProtocolParser.ParseRexpression(Data, ref offset);
				list.Add(new Tag { Name = name, Value = value });
			}

			return list;
			
		}

		#endregion

		#region Symbol Name

		public bool IsSymbolName
		{
			get { return RexpressionType == RexpressionType.SymbolName; }
		}

		public string ToSymbolName()
		{
			if (!IsSymbolName)
			{
				throw new InvalidOperationException("expression is not a symbol name");
			}

			return Data.GetUTF8String();
		}

		#endregion


		#endregion

	}
}

// ReSharper restore InconsistentNaming
