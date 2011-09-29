namespace gsDesign.Explorer.Helpers
{
	using System;
	using System.Linq;
	using System.Reflection;

	public static class EnumHelper
	{
		public static T[] GetValues<T>() where T : struct, IConvertible
		{
			Type type = typeof (T);
			if (!type.IsEnum)
				throw new ArgumentException("Type '" + type.Name + "' is not an enum");

			return type.GetFields(BindingFlags.Public | BindingFlags.Static)
				.Where(field => field.IsLiteral)
				.Select(field => (T) field.GetValue(null))
				.ToArray();
		}

		public static string[] GetNames<T>() where T : struct, IConvertible
		{
			Type type = typeof (T);
			if (!type.IsEnum)
				throw new ArgumentException("Type '" + type.Name + "' is not an enum");

			return type.GetFields(BindingFlags.Public | BindingFlags.Static)
				.Where(field => field.IsLiteral)
				.Select(field => field.Name)
				.ToArray();
		}
	}
}