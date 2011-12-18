namespace gsDesign.Explorer.ViewModels.Design.SampleSize
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using Subfuzion.Helpers;
	using gsDesign.Design;
	using gsDesign.Design.SampleSize;

	public class SampleSize : NotifyPropertyChangedBase
	{
		private DesignParameters _designParameters;

		public SampleSize(DesignParameters designParameters)
		{
			_designParameters = designParameters;
		}

		private SampleSizeParameters Model
		{
			get { return _designParameters.SampleSizeParameters; }
		}

		#region SampleSizeCategories property

		public IEnumerable<string> SampleSizeCategories
		{
			get { return EnumHelper.GetNames<SampleSizeCategory>(); }
		}

		#endregion // SampleSizeCategories

		#region SampleSizeCategory property

		public SampleSizeCategory SampleSizeCategory
		{
			get { return Model.SampleSizeCategory; }

			set
			{
				if (Model.SampleSizeCategory != value)
				{
					Model.SampleSizeCategory = value;
					NotifyPropertyChanged("SampleSizeCategory");
				}
			}
		}

		#endregion // SampleSizeCategory

		#region User Input

		#region FixedDesignSampleSize property

		[Display(Name = "Fixed Design Sample Size",
			Description = "Set the sample size for a fixed design")]
		public int FixedDesignSampleSize
		{
			get { return Model.FixedDesignSampleSize; }

			set
			{
				if (Model.FixedDesignSampleSize != value)
				{
					Model.FixedDesignSampleSize = value;
					NotifyPropertyChanged("FixedDesignSampleSize");
				}
			}
		}

		#endregion // FixedDesignSampleSize

		#region MinimumFixedSampleSize property

		public int MinimumFixedSampleSize
		{
			get { return 1; }
		}

		#endregion // MinimumFixedSampleSize

		#region MaximumFixedSampleSize property

		public int MaximumFixedSampleSize
		{
			get { return 100000; }
		}

		#endregion // MaximumFixedSampleSize

		#endregion

		#region Binomial

		#region BinomialRandomizationRatio property

		[Display(Name = "Randomization Ratio",
			Description = "Experimental/control relative sample size:" + Strings.NewLine + "0.1 " + Symbols.LessThanOrEqual + " Ratio " + Symbols.LessThanOrEqual + " 10")]
		public double BinomialRandomizationRatio
		{
			get { return Model.BinomialRandomizationRatio; }

			set
			{
				if (Math.Abs(Model.BinomialRandomizationRatio - value) > double.Epsilon)
				{
					Model.BinomialRandomizationRatio = value;
					NotifyPropertyChanged("BinomialRandomizationRatio");
				}
			}
		}

		public double BinomialRandomizationRatioMinimum
		{
			get { return 0.1; }
		}

		public double BinomialRandomizationRatioMaximum
		{
			get { return 10.0; }
		}

		public int BinomialRandomizationRatioPrecision
		{
			get { return 3; }
		}

		public double BinomialRandomizationRatioIncrement
		{
			get { return 0.5; }
		}

		#endregion // BinomialRandomizationRatio

		#region BinomialControlEventRate property

		[Display(Name = "Control",
			Description = "Control event rate:" + Strings.NewLine + "0 " + Symbols.LessThan + " Control " + Symbols.LessThan + " 1")]
		public double BinomialControlEventRate
		{
			get { return Model.BinomialControlEventRate; }

			set
			{
				if (Math.Abs(Model.BinomialControlEventRate - value) > double.Epsilon)
				{
					Model.BinomialControlEventRate = value;
					NotifyPropertyChanged("BinomialControlEventRate");
				}
			}
		}

		public double BinomialControlEventRateMinimum
		{
			get { return 0.00001; }
		}

		public double BinomialControlEventRateMaximum
		{
			get { return 0.99999; }
		}

		public double BinomialControlEventRateIncrement
		{
			get { return 0.001; }
		}

		public int BinomialControlEventRatePrecision
		{
			get { return 5; }
		}

		#endregion // BinomialControlEventRate

		#region BinomialExperimentalEventRate property

		[Display(Name = "Experimental",
			Description = "Experimental event rate:" + Strings.NewLine + "0 " + Symbols.LessThan + " Control " + Symbols.LessThan + " 1")]
		public double BinomialExperimentalEventRate
		{
			get { return Model.BinomialExperimentalEventRate; }

			set
			{
				if (Math.Abs(Model.BinomialExperimentalEventRate - value) > double.Epsilon)
				{
					Model.BinomialExperimentalEventRate = value;
					NotifyPropertyChanged("BinomialExperimentalEventRate");
				}
			}
		}

		public double BinomialExperimentalEventRateMinimum
		{
			get { return 0.00001; }
		}

		public double BinomialExperimentalEventRateMaximum
		{
			get { return 0.99999; }
		}

		public double BinomialExperimentalEventRateIncrement
		{
			get { return 0.001; }
		}

		public int BinomialExperimentalEventRatePrecision
		{
			get { return 5; }
		}

		#endregion // BinomialExperimentalEventRate

		#region BinomialNonInferiorityTesting property

		[Display(Name = "",
			Description = "Specify either 'Superiority' or\n" +
			              "'Non-Inferiority - superiority with margin'")]
		public BinomialNonInferiorityTesting BinomialNonInferiorityTesting
		{
			get { return Model.BinomialNonInferiorityTesting; }

			set
			{
				if (Model.BinomialNonInferiorityTesting != value)
				{
					Model.BinomialNonInferiorityTesting = value;
					NotifyPropertyChanged("BinomialNonInferiorityTesting");

					BinomialDelta = 0.0;
					NotifyPropertyChanged("BinomialDeltaIsEnabled");
				}
			}
		}

		#endregion // BinomialNonInferiorityTesting

		#region BinomialDelta property

		public bool BinomialDeltaIsEnabled
		{
			get { return Model.BinomialNonInferiorityTesting == BinomialNonInferiorityTesting.SuperiorityWithMargin; }
		}

		[Display(Name = "Delta",
			Description = "Margin for treatment difference:" + Strings.NewLine + "-1 " + Symbols.LessThan + " Delta " + Symbols.LessThan + " 1")]
		public double BinomialDelta
		{
			get { return Model.BinomialDelta; }

			set
			{
				if (Math.Abs(Model.BinomialDelta - value) > double.Epsilon)
				{
					Model.BinomialDelta = value;
					NotifyPropertyChanged("BinomialDelta");
					NotifyPropertyChanged("BinomialDeltaIsEnabled");
				}
			}
		}

		public double BinomialDeltaMinimum
		{
			get { return -0.9999; }
		}

		public double BinomialDeltaMaximum
		{
			get { return 0.9999; }
		}

		public double BinomialDeltaIncrement
		{
			get { return 0.1; }
		}

		public int BinomialDeltaPrecision
		{
			get { return 4; }
		}

		#endregion // BinomialDelta

		#region BinomialSampleSize property

		[Display(Name = "Sample Size",
			Description = "Computed after running the design")]
		public int BinomialSampleSize
		{
			get { return Model.BinomialFixedDesignSampleSize; }

			set
			{
				if (Model.BinomialFixedDesignSampleSize != value)
				{
					Model.BinomialFixedDesignSampleSize = value;
					NotifyPropertyChanged("BinomialSampleSize");
				}
			}
		}

		#endregion // BinomialSampleSize

		#endregion

		#region Time to Event

		#region TimeToEventSpecification property

		[Display(Name = "Specification",
			Description = "Select median time or event rate")]
		public TimeToEventSpecification TimeToEventSpecification
		{
			get { return Model.TimeToEventSpecification; }

			set
			{
				if (Model.TimeToEventSpecification != value)
				{
					Model.TimeToEventSpecification = value;
					NotifyPropertyChanged("TimeToEventSpecification");
				}
			}
		}

		#endregion // TimeToEventSpecification

		#region TimeToEventControl property

		 [Display(Name = "Control",
			Description = "The time to an endpoint for the primary control group")]
		public double TimeToEventControl
		{
			get { return Model.TimeToEventControl; }

			set
			{
				if (Math.Abs(Model.TimeToEventControl - value) > double.Epsilon)
				{
					Model.TimeToEventControl = value;
					NotifyPropertyChanged("TimeToEventControl");
				}
			}
		}

		#endregion // TimeToEventControl

		#region MinimumTimeToEventControl property

		public double MinimumTimeToEventControl
		{
			get { return 0.0; }
		}

		#endregion // MinimumTimeToEventControl

		#region MaximumTimeToEventControl property

		public double MaximumTimeToEventControl
		{
			get { return 1.0E6; }
		}

		#endregion // MaximumTimeToEventControl

		#region TimeToEventExperimental property

		 [Display(Name = "Experimental",
			Description = "The median time or event rate for the experimental group")]
		public double TimeToEventExperimental
		{
			get { return Model.TimeToEventExperimental; }

			set
			{
				if (Math.Abs(Model.TimeToEventExperimental - value) > double.Epsilon)
				{
					Model.TimeToEventExperimental = value;
					NotifyPropertyChanged("TimeToEventExperimental");
				}
			}
		}

		#endregion // TimeToEventExperimental

		#region MinimumTimetoEventExperimental property

		public double MinimumTimetoEventExperimental
		{
			get { return 0; }
		}

		#endregion // MinimumTimetoEventExperimental

		#region MaximumTimeToEventExperimental property

		public double MaximumTimeToEventExperimental
		{
			get { return 1.0E6; }
		}

		#endregion // MaximumTimeToEventExperimental

		#region TimeToEventDropout property

		 [Display(Name = "Dropout",
			Description = "Equal dropout hazard rate for both groups")]
		public double TimeToEventDropout
		{
			get { return Model.TimeToEventDropout; }

			set
			{
				if (Math.Abs(Model.TimeToEventDropout - value) > double.Epsilon)
				{
					Model.TimeToEventDropout = value;
					NotifyPropertyChanged("TimeToEventDropout");
				}
			}
		}

		#endregion // TimeToEventDropout

		#region MinimumTimeToEventDropout property

		public double MinimumTimeToEventDropout
		{
			get { return 0.0; }
		}

		#endregion // MinimumTimeToEventDropout

		#region MaximumTimeToEventDropout property

		public double MaximumTimeToEventDropout
		{
			get { return 1.0E6; }
		}

		#endregion // MaximumTimeToEventDropout

		#region TimeToEventHazardRatio property

		 [Display(Name = "Hazard Ratio",
			Description = "Calculated required sample size")]
		public string TimeToEventHazardRatio
		{
			get { return Model.TimeToEventHazardRatio.ToString("F6"); }
		}

		#endregion // TimeToEventHazardRatio

		#region TimeToEventAccrualDuration property

		 [Display(Name = "Accrual Duration",
			Description = "Accrual (recruitment) duration")]
		public double TimeToEventAccrualDuration
		{
			get { return Model.TimeToEventAccrualDuration; }

			set
			{
				if (Math.Abs(Model.TimeToEventAccrualDuration - value) > double.Epsilon)
				{
					Model.TimeToEventAccrualDuration = value;
					NotifyPropertyChanged("TimeToEventAccrualDuration");
				}
			}
		}

		#endregion // TimeToEventAccrualDuration

		#region MinimumTimeToEventAccrualDuration property

		public double MinimumTimeToEventAccrualDuration
		{
			get { return 0.0001; }
		}

		#endregion // MinimumTimeToEventAccrualDuration

		#region MaximumTimeToEventAccrualDuration property

		public double MaximumTimeToEventAccrualDuration
		{
			get { return 1.0E6; }
		}

		#endregion // MaximumTimeToEventAccrualDuration

		#region TimeToEventMinimumFollowUp property

		 [Display(Name = "Minimum Follow-Up",
			Description = "Time to event minimum follow-up")]
		public double TimeToEventMinimumFollowUp
		{
			get { return Model.TimeToEventMinimumFollowUp; }

			set
			{
				if (Math.Abs(Model.TimeToEventMinimumFollowUp - value) > double.Epsilon)
				{
					Model.TimeToEventMinimumFollowUp = value;
					NotifyPropertyChanged("TimeToEventMinimumFollowUp");
				}
			}
		}

		#endregion // TimeToEventMinimumFollowUp

		#region MinimumTimeToEventMinimumFollowUp property

		public double MinimumTimeToEventMinimumFollowUp
		{
			get { return 0.0001; }
		}

		#endregion // MinimumTimeToEventMinimumFollowUp

		#region MaximumTimeToEventMinimumFollowUp property

		public double MaximumTimeToEventMinimumFollowUp
		{
			get { return 1.0E6; }
		}

		#endregion // MaximumTimeToEventMinimumFollowUp

		#region TimeToEventRandomizationRatio property

		 [Display(Name = "Randomization Ratio",
			Description = "Randomization ratio between placebo and treatment group")]
		public double TimeToEventRandomizationRatio
		{
			get { return Model.TimeToEventRandomizationRatio; }

			set
			{
				if (Math.Abs(Model.TimeToEventRandomizationRatio - value) > double.Epsilon)
				{
					Model.TimeToEventRandomizationRatio = value;
					NotifyPropertyChanged("TimeToEventRandomizationRatio");
				}
			}
		}

		#endregion // TimeToEventRandomizationRatio

		#region MinimumTimeToEventRandomizationRatio property

		public double MinimumTimeToEventRandomizationRatio
		{
			get { return 0.0001; }
		}

		#endregion // MinimumTimeToEventRandomizationRatio

		#region MaximumTimeToEventRandomizationRatio property

		public double MaximumTimeToEventRandomizationRatio
		{
			get { return 1.0; }
		}

		#endregion // MaximumTimeToEventRandomizationRatio

		#region TimeToEventHypothesis property

		 [Display(Name = "Hypothesis",
			Description = "Type of sample size calculation")]
		public TimeToEventHypothesis TimeToEventHypothesis
		{
			get { return Model.TimeToEventHypothesis; }

			set
			{
				if (Model.TimeToEventHypothesis != value)
				{
					Model.TimeToEventHypothesis = value;
					NotifyPropertyChanged("TimeToEventHypothesis");
				}
			}
		}

		#endregion // TimeToEventHypothesis

		#region TimeToEventAccrual property

		 [Display(Name = "Patient Entry Type",
			Description = "Patient entry type")]
		public TimeToEventAccrual TimeToEventAccrual
		{
			get { return Model.TimeToEventAccrual; }

			set
			{
				if (Model.TimeToEventAccrual != value)
				{
					Model.TimeToEventAccrual = value;
					NotifyPropertyChanged("TimeToEventAccrual");
				}
			}
		}

		#endregion // TimeToEventAccrual

		#region TimeToEventGamma property

		 [Display(Name = "Gamma",
			Description = "Rate parameter for exponential entry")]
		public double TimeToEventGamma
		{
			get { return Model.TimeToEventGamma; }

			set
			{
				if (Math.Abs(Model.TimeToEventGamma - value) > double.Epsilon)
				{
					Model.TimeToEventGamma = value;
					NotifyPropertyChanged("TimeToEventGamma");
				}
			}
		}

		#endregion // TimeToEventGamma

		#region MinimumTimeToEventGamma property

		public double MinimumTimeToEventGamma
		{
			get { return 0.0; }
		}

		#endregion // MinimumTimeToEventGamma

		#region MaximumTimeToEventGamma property

		public double MaximumTimeToEventGamma
		{
			get { return 1.0E6; }
		}

		#endregion // MaximumTimeToEventGamma

		#region TimeToEventFixedDesignSampleSize property

		 [Display(Name = "Sample Size",
			Description = "Computed after running the design")]
		public int TimeToEventFixedDesignSampleSize
		{
			get { return Model.TimeToEventFixedDesignSampleSize; }

			set
			{
				if (Model.TimeToEventFixedDesignSampleSize != value)
				{
					Model.TimeToEventFixedDesignSampleSize = value;
					NotifyPropertyChanged("TimeToEventFixedDesignSampleSize");
				}
			}
		}

		#endregion // TimeToEventFixedDesignSampleSize

		#region TimeToEventFixedDesignEvents property

		 [Display(Name = "Events",
			Description = "Computed after running the design")]
		public int TimeToEventFixedDesignEvents
		{
			get { return Model.TimeToEventFixedDesignEvents; }

			set
			{
				if (Model.TimeToEventFixedDesignEvents != value)
				{
					Model.TimeToEventFixedDesignEvents = value;
					NotifyPropertyChanged("TimeToEventFixedDesignEvents");
				}
			}
		}

		#endregion // TimeToEventFixedDesignEvents

		#endregion // Time to Event

	}
}
