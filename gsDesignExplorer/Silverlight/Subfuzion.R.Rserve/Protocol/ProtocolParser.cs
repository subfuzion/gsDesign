namespace Subfuzion.R.Rserve.Protocol
{
	using System;
	using System.Text;

	public class ProtocolParser
	{
		public static ProtocolSettings ParseHandshake(byte[] bytes)
		{
			if (bytes == null || bytes.Length < 32)
			{
				throw new ArgumentException("Expected 32 bytes for Rserve identification");
			}

			var protocol = new ProtocolSettings
			{
				Signature = Encoding.UTF8.GetString(bytes, 0, 4),
				Version = Encoding.UTF8.GetString(bytes, 4, 4),
				Name = Encoding.UTF8.GetString(bytes, 8, 4)
			};

			for (int i = 12; i < 32; i += 4)
			{
				string attribute = Encoding.UTF8.GetString(bytes, i, 4);

				// don't bother parsing for R version; shouldn't see this
				// attribute any more (versions are too high to be represented
				// now in "R" attribute's remaining 3 bytes)
				// if (attribute.StartsWith("R"))
				// {
				//     _rVersion = attribute;
				// }

				if (attribute.StartsWith("AR"))
				{
					protocol.IsAuthorizationRequired = true;
					if (attribute.EndsWith("pt")) protocol.PasswordEncryption = PasswordEncryption.PlainText;
					else if (attribute.EndsWith("uc")) protocol.PasswordEncryption = PasswordEncryption.UnixCrypt;
				}

				if (attribute.StartsWith("K"))
				{
					protocol.PasswordEncryptionKey = attribute.Substring(1, 3);
				}
			}

			return protocol;
		}

		public static ResponseHeader ParseResponseHeader(byte[] bytes)
		{
			if (bytes == null || bytes.Length < ResponseHeader.ResponseHeaderSize)
			{
				throw new ArgumentException("Invalid response header");
			}

			var responseCode = BitConverter.ToInt32(bytes, 0);
			if ((responseCode & 0x010000) != 0x010000)
			{
				throw new ArgumentException("Invalid response header");
			}

			var responseHeader = new ResponseHeader
			{
				CommandCode = (CommandCode)responseCode,
				PayloadSize = BitConverter.ToInt32(bytes, 4),
				PayloadOffset = BitConverter.ToInt32(bytes, 8),
				PayloadSize2 = BitConverter.ToInt32(bytes, 12)
			};

			return responseHeader;
		}

		public static Response ParseResponse(Request request, byte[] responseBytes)
		{
			var response = new Response();

			response.RawBytes = responseBytes;

			response.Request = request;

			var header = ProtocolHeader.CreateResponseHeader(responseBytes);

			response.Header = header;

			if (header.IsOk && header.PayloadSize > 0)
			{
				var offset = ProtocolHeader.HeaderSize + header.PayloadOffset;
				response.Payload = ParsePayload(responseBytes, offset);
			}
			else
			{
				response.Payload = new Payload();
			}

			return response;
		}

		public static Payload ParsePayload(byte[] rawBytes, int offset)
		{
			// get the data type (PayloadCode) of the payload content
			// (it will be either Rexpression or ByteStream)
			var transportCode = (PayloadCode)rawBytes[offset];

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

		public static Rexpression ParseRexpression(byte[] bytes)
		{
			var offset = 0;
			return ParseRexpression(bytes, ref offset);
		}

		public static Rexpression ParseRexpression(byte[] bytes, ref int offset)
		{
			if (bytes == null || (bytes.Length - offset) < Rexpression.RexpressionHeaderSize)
			{
				throw new ArgumentException("Invalid Rexpression header");
			}

			try
			{
				Rexpression rexp = null;
				Rexpression attr = null;

				var type = (RexpressionType)(bytes[offset] & 0x3F);
				var hasAttribute = (bytes[offset] & (byte)RexpressionType.HasAttribute) == (byte)RexpressionType.HasAttribute;

				// TODO: handle long data
				var lengthBytes = new byte[4];
				Array.Copy(bytes, offset + 1, lengthBytes, 0, Rexpression.DataSizeHeaderSize);
				var length = BitConverter.ToInt32(lengthBytes, 0);

				offset += Rexpression.RexpressionHeaderSize;
				var delta = 0;

				if (hasAttribute)
				{
					delta = offset;
					attr = ParseRexpression(bytes, ref offset);
					delta = offset - delta;
				}

				length -= delta;

				var data = new byte[length];
				Array.Copy(bytes, offset, data, 0, length);

				offset += length;

				rexp = new Rexpression(type, data) { HasAttribute = hasAttribute, Attribute = attr};

				return rexp;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}
}