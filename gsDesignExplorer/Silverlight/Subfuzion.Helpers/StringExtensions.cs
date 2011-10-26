namespace Subfuzion.Helpers
{
	using System.Text;

	public static class StringExtensions
	{
		public static byte[] GetBytes(this string s)
		{
			if (string.IsNullOrEmpty(s)) return new byte[] { 0 };

			var utf8 = Encoding.UTF8.GetBytes(s);
			var bytes = new byte[s.Length + 1];
			utf8.CopyTo(bytes, 0);
			bytes[s.Length] = 0;
			return bytes;
		}

		public static string GetUTF8String(this byte[] bytes, int offset = 0, int count = -1)
		{
			return bytes == null ? null : Encoding.UTF8.GetString(bytes, offset, count == -1 ? bytes.Length : count);
		}
	}
}
