namespace gsDesign.Explorer.Models
{
	using System;
	using System.IO;
	using System.Text;

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

			Writer.WriteLine("### still in work ###");
			AppendNFix();
			AppendAssignment("FixedDesignSampleSize", Design.SampleSize.FixedDesignSampleSize);
			AppendAssignment("RandomizationRatio", Design.SampleSize.RandomizationRatio);
			AppendAssignment("ControlEventRate", Design.SampleSize.ControlEventRate);
			AppendAssignment("NonInferiorityTesting", Design.SampleSize.NonInferiorityTesting.ToString());
			AppendAssignment("Delta", Design.SampleSize.Delta);
			Writer.WriteLine("###");
			AppendAssignment("timing", "[TBD]");
			AppendAssignment("sfu", "[TBD]");
			AppendAssignment("sfupar", "[TBD]");
			AppendAssignment("sfl", "[TBD]");
			AppendAssignment("sflpar", "[TBD]");
			AppendAssignment("endpoint", "[TBD]");
			Writer.WriteLine("###");

			Writer.WriteLine();
			AppendGSDesignFunction();
			Writer.WriteLine();
			AppendPlot();

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

		private void AppendGSDesignFunction()
		{
			var sb = new StringBuilder();
			sb.Append("gsDesign(k=k, test.type=test.type, alpha=alpha, beta=beta, n.fix=n.fix\n")
				.Append("  timing=timing, sfu=sfu, sfupar=sfupar, sfl=sfl, sflpar=sflpar, endpoint=endpoint)");

			AppendAssignment(Design.Name, sb.ToString());

			Writer.WriteLine();

			AppendAssignment("fixedDesign", "list(events = 1, sampleSize = 0)");
		}

		private void AppendPlot()
		{
			AppendComment("Boundaries Plot");
			Writer.WriteLine("plot({0}", Design.Name);
			Writer.WriteLine("  plottype=1,");
			Writer.WriteLine("  base=TRUE,");
			Writer.WriteLine("  col=c(\"black\", \"black\"),");
			Writer.WriteLine("  lwd=c(1, 1),");
			Writer.WriteLine("  lty=c(\"solid\", \"solid\"),");
			Writer.WriteLine("  dgt=c(2, 2))");
		}
	}
}
