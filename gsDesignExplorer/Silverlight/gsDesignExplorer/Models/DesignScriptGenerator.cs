namespace gsDesign.Explorer.Models
{
	using System;
	using System.IO;
	using System.Text;
	using Design.SampleSize;
	using Design.SpendingFunctions;

	public class DesignScriptGenerator
	{
		private StringWriter _writer;

		public string GenerateScript(DesignParameters designParameters)
		{
			if (designParameters == null) return null;

			DesignParameters = designParameters;

			GeneratedTimestamp = DateTime.Now;

			_writer = new StringWriter();

			AppendHeader();
			AppendK();
			AppendTestType();
			AppendAlpha();
			AppendBeta();
			AppendBinomial();
			AppendTTE();
			AppendNFix();
			AppendTiming();
			AppendSfu();
			AppendSfupar();
			AppendSfl();
			AppendSflpar();
			AppendEndpoint();

			//Writer.WriteLine();
			//Writer.WriteLine("##################################################");
			//Writer.WriteLine("### (still in work)");
			//Writer.WriteLine("# =================================");
			//Writer.WriteLine("# Sample Size tab");
			//Writer.WriteLine("# ---------------------------------");
			//Writer.WriteLine("(Active tab: {0})", DesignParameters.SampleSizeParameters.SampleSizeCategory);
			//Writer.WriteLine("# =================================");
			//Writer.WriteLine("# Sample Size - User Input tab");
			//Writer.WriteLine("# ---------------------------------");
			//AppendAssignment("FixedDesignSampleSize", DesignParameters.SampleSizeParameters.FixedDesignSampleSize);
			//Writer.WriteLine("# =================================");
			//Writer.WriteLine("# Sample Size - Binomial tab");
			//Writer.WriteLine("# ---------------------------------");
			//AppendAssignment("Randomization Ratio", DesignParameters.SampleSizeParameters.BinomialRandomizationRatio);
			//AppendAssignment("Control", DesignParameters.SampleSizeParameters.BinomialControlEventRate);
			//AppendAssignment("Experimental", DesignParameters.SampleSizeParameters.BinomialExperimentalEventRate);
			//AppendAssignment("Non-Inferiority Testing", DesignParameters.SampleSizeParameters.BinomialNonInferiorityTesting.ToString());
			//AppendAssignment("Delta", DesignParameters.SampleSizeParameters.BinomialDelta);
			//AppendAssignment("Binomial Fixed Design Sample Size", DesignParameters.SampleSizeParameters.BinomialFixedDesignSampleSize);
			//Writer.WriteLine("# =================================");
			//Writer.WriteLine("# Sample Size - Time to Event tab");
			//Writer.WriteLine("# ---------------------------------");
			//AppendAssignment("Specification", DesignParameters.SampleSizeParameters.TimeToEventSpecification.ToString());
			//AppendAssignment("Control", DesignParameters.SampleSizeParameters.TimeToEventControl);
			//AppendAssignment("Experimental", DesignParameters.SampleSizeParameters.TimeToEventExperimental);
			//AppendAssignment("Dropout", DesignParameters.SampleSizeParameters.TimeToEventDropout);
			//AppendAssignment("Hazard Ratio", DesignParameters.SampleSizeParameters.TimeToEventHazardRatio);
			//AppendAssignment("Accrual Duration", DesignParameters.SampleSizeParameters.TimeToEventAccrualDuration);
			//AppendAssignment("Minimum Follow-Up", DesignParameters.SampleSizeParameters.TimeToEventMinimumFollowUp);
			//AppendAssignment("Randomization Ratio", DesignParameters.SampleSizeParameters.TimeToEventRandomizationRatio);
			//AppendAssignment("Hypothesis", DesignParameters.SampleSizeParameters.TimeToEventHypothesis.ToString());
			//AppendAssignment("Accrual Patient Entry Type", DesignParameters.SampleSizeParameters.TimeToEventAccrual.ToString());
			//AppendAssignment("Gamma", DesignParameters.SampleSizeParameters.TimeToEventGamma);
			//AppendAssignment("Time to Event Fixed Design Sample Size", DesignParameters.SampleSizeParameters.TimeToEventFixedDesignSampleSize);
			//AppendAssignment("Time to Event Fixed Design Events", DesignParameters.SampleSizeParameters.TimeToEventFixedDesignEvents);
			//Writer.WriteLine("# =================================");
			//Writer.WriteLine("# Spending Functions tab");
			//Writer.WriteLine("# ---------------------------------");
			//Writer.WriteLine("(Active tab: {0})", DesignParameters.SpendingFunctionParameters.CurrentSpendingFunctionBounds);
			//Writer.WriteLine("# =================================");
			//Writer.WriteLine("# Spending Functions - Lower Spending tab");
			//Writer.WriteLine("# ---------------------------------");
			//Writer.WriteLine("Test type: {0})", DesignParameters.SpendingFunctionParameters.LowerSpendingFunction.SpendingFunctionTestingParameters.SpendingFunctionTestType);
			//Writer.WriteLine("Lower bound spending: {0})", DesignParameters.SpendingFunctionParameters.LowerSpendingFunction.SpendingFunctionTestingParameters.SpendingFunctionLowerBoundSpending);
			//Writer.WriteLine("Lower bound testing: {0})", DesignParameters.SpendingFunctionParameters.LowerSpendingFunction.SpendingFunctionTestingParameters.SpendingFunctionLowerBoundTesting);
			//Writer.WriteLine("(Selected lower spending function: {0})", DesignParameters.SpendingFunctionParameters.LowerSpendingFunction.SpendingFunctionParameterCategory);
			//Writer.WriteLine("Parameter free spending function: {0})", DesignParameters.SpendingFunctionParameters.LowerSpendingFunction.ParameterFreeSpendingFunction.LanDeMetsApproximation);
			//Writer.WriteLine("# =================================");
			//Writer.WriteLine("# Spending Functions - Upper Spending tab");
			//Writer.WriteLine("# ---------------------------------");
			//Writer.WriteLine("Test type: {0})", DesignParameters.SpendingFunctionParameters.UpperSpendingFunction.SpendingFunctionTestingParameters.SpendingFunctionTestType);
			//Writer.WriteLine("Lower bound spending: {0})", DesignParameters.SpendingFunctionParameters.UpperSpendingFunction.SpendingFunctionTestingParameters.SpendingFunctionLowerBoundSpending);
			//Writer.WriteLine("Lower bound testing: {0})", DesignParameters.SpendingFunctionParameters.UpperSpendingFunction.SpendingFunctionTestingParameters.SpendingFunctionLowerBoundTesting);
			//Writer.WriteLine("(Selected upper spending function: {0})", DesignParameters.SpendingFunctionParameters.UpperSpendingFunction.SpendingFunctionParameterCategory);
			//Writer.WriteLine("Parameter free spending function: {0})", DesignParameters.SpendingFunctionParameters.UpperSpendingFunction.ParameterFreeSpendingFunction.LanDeMetsApproximation);
			//Writer.WriteLine("###");
			//Writer.WriteLine();
			//Writer.WriteLine("gsDesign function parameters (TBD)");

			//AppendAssignment("astar", "[TBD]");
			//AppendAssignment("tol", "[TBD]");
			//AppendAssignment("r", "[TBD]");
			//AppendAssignment("upper", "[TBD]");
			//AppendAssignment("lower", "[TBD]");
			//AppendAssignment("n.I", "[TBD]");
			//AppendAssignment("maxn.IPlan", "[TBD]");

			//Writer.WriteLine("##################################################");

			Writer.WriteLine();
			AppendGSDesignFunction();
			Writer.WriteLine();
//			AppendPlot();

			var script = _writer.ToString();
			_writer.Close();
			_writer = null;
			return script;
		}

		private StringWriter Writer
		{
			get { return _writer; }
		}

		private DesignParameters DesignParameters { get; set; }

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

		private void AppendAssignment(object property, object value, params string[] args)
		{
			Writer.WriteLine("{0} <- {1}", property, string.Format(value.ToString(), args));
		}

		private void AppendHeader()
		{
			AppendComment("This R script was created via an export of a group sequential design");
			AppendComment("developed in gsDesign Explorer version 2.0 on {0}", GeneratedTimestamp.ToString());
			Writer.WriteLine();
			Writer.WriteLine("###");
			AppendComment("Design      : {0}", DesignParameters.Name);
			AppendComment("Description : {0}", DesignParameters.Description);
			Writer.WriteLine("###");
			Writer.WriteLine();
		}

		private void AppendGSDesignFunction()
		{
			var sb = new StringBuilder();
			sb.Append("gsDesign(k=k, test.type=test.type, alpha=alpha, beta=beta, n.fix=n.fix,\n")
				.Append("  timing=timing, sfu=sfu, sfupar=sfupar, sfl=sfl, sflpar=sflpar, endpoint=endpoint)");

			AppendAssignment(DesignParameters.Name, sb.ToString());

			// Writer.WriteLine();

			// TODO
			// AppendAssignment("fixedDesign", "list(events = 1, sampleSize = 0)");
		}

		private void AppendPlot()
		{
			AppendComment("Boundaries Plot");
			Writer.WriteLine("plot({0},", DesignParameters.Name);
			Writer.WriteLine("  plottype=1,");
			Writer.WriteLine("  base=TRUE,");
			Writer.WriteLine("  col=c(\"black\", \"black\"),");
			Writer.WriteLine("  lwd=c(1, 1),");
			Writer.WriteLine("  lty=c(\"solid\", \"solid\"),");
			Writer.WriteLine("  dgt=c(2, 2))");
		}

		private void AppendK()
		{
			AppendAssignment("k", DesignParameters.ErrorPowerTimingParameters.K);
		}

		private void AppendTestType()
		{
			AppendAssignment("test.type", DesignParameters.SpendingFunctionParameters.SpendingFunctionTestingParameters.SpendingFunctionTestTypeCode);
		}
	
		private void AppendAlpha()
		{
			AppendAssignment("alpha", Math.Round(DesignParameters.ErrorPowerTimingParameters.Alpha, 6));
		}

		private void AppendBeta()
		{
			AppendAssignment("beta", Math.Round(DesignParameters.ErrorPowerTimingParameters.Beta, 6));
		}

		private void AppendNFix()
		{
			string s = "?";

			switch (DesignParameters.SampleSizeParameters.SampleSizeCategory)
			{
				case SampleSizeCategory.UserInput:
					// s = DesignParameters.SampleSizeParameters.NFix;
					s = DesignParameters.SampleSizeParameters.FixedDesignSampleSize.ToString();
					break;

				case SampleSizeCategory.Binomial:
					s = string.Format("nBinomial(p1=p1, p2=p2, alpha={0}, beta={1}, delta0=delta0, ratio={2})",
						DesignParameters.ErrorPowerTimingParameters.Alpha,
						DesignParameters.ErrorPowerTimingParameters.Beta,
						DesignParameters.SampleSizeParameters.BinomialRandomizationRatio);
					break;

				case SampleSizeCategory.TimeToEvent:
					s = string.Format("{0}Survival$nEvents", DesignParameters.Name);
					break;

			}


			AppendAssignment("n.fix", s);
		}

		private void AppendTiming()
		{
			var sb = new StringBuilder();

			// number of timings is always >= 1
			sb.Append(Math.Round(DesignParameters.ErrorPowerTimingParameters.Timing[0], 4));

			for (int i = 1; i < DesignParameters.ErrorPowerTimingParameters.Timing.Count; i++)
			{
				sb.Append(", ").Append(Math.Round(DesignParameters.ErrorPowerTimingParameters.Timing[i], 4));
			}

			AppendAssignment("timing", "c({0})", sb.ToString());
		}

		private void AppendSfu()
		{
			string s = "?";

			switch (DesignParameters.SpendingFunctionParameters.UpperSpendingFunction.SpendingFunctionParameterCategory)
			{
				case SpendingFunctionParameterCategory.ParameterFree:
					s = "sfLDOF";
					break;

				case SpendingFunctionParameterCategory.OneParameter:
					s = "sfHSD";
					break;

				case SpendingFunctionParameterCategory.TwoParameter:
					break;

				case SpendingFunctionParameterCategory.ThreeParameter:
					break;

				case SpendingFunctionParameterCategory.PiecewiseLinear:
					break;
			}

			AppendAssignment("sfu", s);
		}

		private void AppendSfupar()
		{
			string s = "?";

			switch (DesignParameters.SpendingFunctionParameters.UpperSpendingFunction.SpendingFunctionParameterCategory)
			{
				case SpendingFunctionParameterCategory.ParameterFree:
					s = "0";
					break;

				case SpendingFunctionParameterCategory.OneParameter:
					break;

				case SpendingFunctionParameterCategory.TwoParameter:
					break;

				case SpendingFunctionParameterCategory.ThreeParameter:
					break;

				case SpendingFunctionParameterCategory.PiecewiseLinear:
					break;
			}

			AppendAssignment("sfupar", s);
		}

		private void AppendSfl()
		{
			string s = "?";

			switch (DesignParameters.SpendingFunctionParameters.LowerSpendingFunction.SpendingFunctionParameterCategory)
			{
				case SpendingFunctionParameterCategory.ParameterFree:
					s = "sfLDOF";
					break;

				case SpendingFunctionParameterCategory.OneParameter:
					s = "sfHSD";
					break;

				case SpendingFunctionParameterCategory.TwoParameter:
					break;

				case SpendingFunctionParameterCategory.ThreeParameter:
					break;

				case SpendingFunctionParameterCategory.PiecewiseLinear:
					break;
			}

			AppendAssignment("sfl", s);
		}

		private void AppendSflpar()
		{
			string s = "?";

			switch (DesignParameters.SpendingFunctionParameters.UpperSpendingFunction.SpendingFunctionParameterCategory)
			{
				case SpendingFunctionParameterCategory.ParameterFree:
					s = "0";
					break;

				case SpendingFunctionParameterCategory.OneParameter:
					break;

				case SpendingFunctionParameterCategory.TwoParameter:
					break;

				case SpendingFunctionParameterCategory.ThreeParameter:
					break;

				case SpendingFunctionParameterCategory.PiecewiseLinear:
					break;
			}

			AppendAssignment("sflpar", s);
		}

		private void AppendEndpoint()
		{
			string s = "?";

			switch (DesignParameters.SampleSizeParameters.SampleSizeCategory)
			{
				case SampleSizeCategory.UserInput:
					s = "user";
					break;

				case SampleSizeCategory.Binomial:
					s = "binomial";
					break;

				case SampleSizeCategory.TimeToEvent:
					s = "tte";
					break;

			}

			AppendAssignment("endpoint", "\"{0}\"", s);
		}

		private void AppendBinomial()
		{
			if (DesignParameters.SampleSizeParameters.SampleSizeCategory != SampleSizeCategory.Binomial) return;

			AppendAssignment("p1", DesignParameters.SampleSizeParameters.BinomialControlEventRate);
			AppendAssignment("p2", DesignParameters.SampleSizeParameters.BinomialExperimentalEventRate);
			// AppendAssignment("delta0", DesignParameters.SampleSizeParameters.BinomialNonInferiorityTesting == BinomialNonInferiorityTesting.Superiority ? 0 : DesignParameters.SampleSizeParameters.BinomialDelta);
			AppendAssignment("delta0", DesignParameters.SampleSizeParameters.BinomialDelta);
			AppendAssignment("delta1", "p1 - p2");
		}

		private void AppendTTE()
		{
			if (DesignParameters.SampleSizeParameters.SampleSizeCategory != SampleSizeCategory.TimeToEvent) return;

			var s = string.Format("nSurvival(lambda1=0.11552, lambda2=0.06931, eta=0.05776, Ts=30, Tr=18, ratio=1, alpha=0.025, beta=0.1, sided=1, type=\"rr\", entry=\"unif\", gamma=1e-04)");
			AppendAssignment(string.Format("{0}Survival", DesignParameters.Name), s);
		}
	}
}
