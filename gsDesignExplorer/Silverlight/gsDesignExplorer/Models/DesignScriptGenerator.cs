namespace gsDesign.Explorer.Models
{
	using System;
	using System.IO;

	public class DesignScriptGenerator
	{
		private StringWriter _writer;

		public string GenerateScript(GSDesign design)
		{
			if (design == null) return null;

			Design = design;

			GeneratedTimestamp = DateTime.Now;

			_writer = new StringWriter();

			AppendHeader();
			AppendK();
			AppendTestType();
			AppendAlpha();
			AppendBeta();
			AppendNFix();

			var script = _writer.ToString();
			_writer.Close();
			_writer = null;
			return script;
		}

		private StringWriter Writer
		{
			get { return _writer; }
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

		private void AppendComment(string comment, params string[] args)
		{
			Writer.WriteLine("# {0}", string.Format(comment, args));
		}

		private void AppendAssignment(object property, object value)
		{
			Writer.WriteLine("{0} <- {1}", property, value);
		}

		private void AppendHeader()
		{
			AppendComment("This R script was created via an export of a group sequential design");
			AppendComment("developed in gsDesign Explorer version 2.0 on {0}", GeneratedTimestamp.ToString());
			Writer.WriteLine();
			Writer.WriteLine("###");
			AppendComment("Design      : {0}", Design.Name);
			AppendComment("Description : {0}", Design.Description);
			Writer.WriteLine("###");
			Writer.WriteLine();
		}

		private void AppendK()
		{
			AppendAssignment("k", Design.Ept.K);
		}

		private void AppendTestType()
		{
			AppendAssignment("test.type", Design.SpendingFunctions.TestTypeCode);
		}
	
		private void AppendAlpha()
		{
			AppendAssignment("alpha", Math.Round(Design.Ept.Alpha, 6));
		}

		private void AppendBeta()
		{
			AppendAssignment("beta", Math.Round(Design.Ept.Beta, 6));
		}

		private void AppendNFix()
		{
			AppendAssignment("n.fix", Design.SampleSize.NFix);
		}
	}
}
