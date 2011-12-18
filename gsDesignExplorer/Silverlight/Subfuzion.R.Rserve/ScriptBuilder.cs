namespace Subfuzion.R.Rserve
{
	using System.Text;

	public class ScriptBuilder
	{
		private StringBuilder _script;


		public static string ReplaceBackWithForwardSlashes(string text)
		{
			return text.Replace('\\', '/');
		}

	}
}