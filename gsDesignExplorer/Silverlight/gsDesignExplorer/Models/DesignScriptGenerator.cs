namespace gsDesign.Explorer.Models
{
	using System;
	using System.IO;

	public class DesignScriptGenerator
	{
		public string GenerateScript(GSDesign design)
		{
			if (design == null) return null;

			Design = design;

			GeneratedTimestamp = DateTime.Now;

			var writer = new StringWriter();

			AppendHeader(writer);

			return writer.ToString();
		}

		private GSDesign Design { get; set; }

		#region GeneratedTimestamp property

		private DateTime? _generatedTimestamp;

		public DateTime GeneratedTimestamp
		{
			get { return _generatedTimestamp.GetValueOrDefault(); }

			set
			{
				_generatedTimestamp = value;
			}
		}

		#endregion // GeneratedTimestamp

		private void AppendHeader(TextWriter writer)
		{
			AppendComment(writer, "This R script was created via an export of a group sequential design");
			AppendComment(writer, "developed in gsDesign Explorer version 2.0 on {0}", GeneratedTimestamp.ToString());
			writer.WriteLine();
			writer.WriteLine("###");
			AppendComment(writer, "Design      : {0}", Design.Name);
			AppendComment(writer, "Description : {0}", Design.Description);
			writer.WriteLine("###");
			writer.WriteLine();
		}

		private void AppendComment(TextWriter writer, string comment, params string[] args)
		{
			writer.WriteLine("# {0}", string.Format(comment, args));
		}
	}
}
