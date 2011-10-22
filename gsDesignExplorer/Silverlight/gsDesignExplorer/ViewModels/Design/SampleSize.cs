namespace gsDesign.Explorer.ViewModels.Design
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using Models;
	using Models.Design.SampleSize;
	using Subfuzion.Helpers;

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
					RaisePropertyChanged("SampleSizeCategory");
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
					RaisePropertyChanged("FixedDesignSampleSize");
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
			Description = "Experimental/control relative sample size:\n0 \u003C Ratio \u2264 1000")]
		public double BinomialRandomizationRatio
		{
			get { return Model.BinomialRandomizationRatio; }

			set
			{
				if (Math.Abs(Model.BinomialRandomizationRatio - value) > double.Epsilon)
				{
					Model.BinomialRandomizationRatio = value;
					RaisePropertyChanged("BinomialRandomizationRatio");
				}
			}
		}

		#endregion // BinomialRandomizationRatio

		#region MinimumBinomialRandomizationRatio property

		private double _minimumBinomialRandomizationRatio = 0.001;

		// [Display(Name = "MinimumBinomialRandomizationRatio",
		//	Description = "")]
		public double MinimumBinomialRandomizationRatio
		{
			get { return _minimumBinomialRandomizationRatio; }

			set
			{
				if (Math.Abs(_minimumBinomialRandomizationRatio - value) > double.Epsilon)
				{
					_minimumBinomialRandomizationRatio = value;
					RaisePropertyChanged("MinimumBinomialRandomizationRatio");
				}
			}
		}

		#endregion // MinimumBinomialRandomizationRatio

		#region MaximumBinomialRandomizationRatio property

		private double _maximumBinomialRandomizationRatio = 1000.0;

		// [Display(Name = "MaximumBinomialRandomizationRatio",
		//	Description = "")]
		public double MaximumBinomialRandomizationRatio
		{
			get { return _maximumBinomialRandomizationRatio; }

			set
			{
				if (Math.Abs(_maximumBinomialRandomizationRatio - value) > double.Epsilon)
				{
					_maximumBinomialRandomizationRatio = value;
					RaisePropertyChanged("MaximumBinomialRandomizationRatio");
				}
			}
		}

		#endregion // MaximumBinomialRandomizationRatio

		#region BinomialControlEventRate property

		[Display(Name = "Control",
			Description = "Control event rate:\n0 \u003C Control \u003C 1")]
		public double BinomialControlEventRate
		{
			get { return Model.BinomialControlEventRate; }

			set
			{
				if (Math.Abs(Model.BinomialControlEventRate - value) > double.Epsilon)
				{
					Model.BinomialControlEventRate = value;
					RaisePropertyChanged("BinomialControlEventRate");
				}
			}
		}

		#endregion // BinomialControlEventRate

		#region MinimumBinomialControlEventRate property

		public double MinimumBinomialControlEventRate
		{
			get { return 0.0001; }
		}

		#endregion // MinimumBinomialControlEventRate

		#region MaximumBinomialControlEventRate property

		public double MaximumBinomialControlEventRate
		{
			get { return 0.9999; }
		}

		#endregion // MaximumBinomialControlEventRate

		#region BinomialExperimentalEventRate property

		[Display(Name = "Experimental",
			Description = "Experimental event rate:\n0 \u003C Experimental \u003C 1")]
		public double BinomialExperimentalEventRate
		{
			get { return Model.BinomialExperimentalEventRate; }

			set
			{
				if (Math.Abs(Model.BinomialExperimentalEventRate - value) > double.Epsilon)
				{
					Model.BinomialExperimentalEventRate = value;
					RaisePropertyChanged("BinomialExperimentalEventRate");
				}
			}
		}

		#endregion // BinomialExperimentalEventRate

		#region MinimumBinomialExperimentalEventRate property

		public double MinimumBinomialExperimentalEventRate
		{
			get { return 0.0001; }
		}

		#endregion // MinimumBinomialExperimentalEventRate

		#region MaximumBinomialExperimentalEventRate property

		public double MaximumBinomialExperimentalEventRate
		{
			get { return 0.9999; }
		}

		#endregion // MaximumBinomialExperimentalEventRate

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
					RaisePropertyChanged("BinomialNonInferiorityTesting");

					BinomialDelta = 0.0;
					RaisePropertyChanged("BinomialDeltaIsEnabled");
				}
			}
		}

		#endregion // BinomialNonInferiorityTesting

		#region BinomialDelta property

		[Display(Name = "Delta",
			Description = "Margin for treatment difference")]
		public double BinomialDelta
		{
			get { return Model.BinomialDelta; }

			set
			{
				if (Math.Abs(Model.BinomialDelta - value) > double.Epsilon)
				{
					Model.BinomialDelta = value;
					RaisePropertyChanged("BinomialDelta");
					RaisePropertyChanged("BinomialDeltaIsEnabled");
				}
			}
		}

		#endregion // BinomialDelta

		#region BinomialDeltaIsEnabled property

		public bool BinomialDeltaIsEnabled
		{
			get { return Model.BinomialNonInferiorityTesting == BinomialNonInferiorityTesting.SuperiorityWithMargin; }
		}

		#endregion // BinomialDeltaIsEnabled

		#region MinimumBinomialDelta property

		public double MinimumBinomialDelta
		{
			get { return -0.0101; }
		}

		#endregion // MinimumBinomialDelta

		#region MaximumBinomialDelta property

		public double MaximumBinomialDelta
		{
			get { return 0.9899; }
		}

		#endregion // MaximumBinomialDelta

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
					RaisePropertyChanged("BinomialSampleSize");
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
					RaisePropertyChanged("TimeToEventSpecification");
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
					RaisePropertyChanged("TimeToEventControl");
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
					RaisePropertyChanged("TimeToEventExperimental");
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
					RaisePropertyChanged("TimeToEventDropout");
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
					RaisePropertyChanged("TimeToEventAccrualDuration");
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
					RaisePropertyChanged("TimeToEventMinimumFollowUp");
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
					RaisePropertyChanged("TimeToEventRandomizationRatio");
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
					RaisePropertyChanged("TimeToEventHypothesis");
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
					RaisePropertyChanged("TimeToEventAccrual");
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
					RaisePropertyChanged("TimeToEventGamma");
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
					RaisePropertyChanged("TimeToEventFixedDesignSampleSize");
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
					RaisePropertyChanged("TimeToEventFixedDesignEvents");
				}
			}
		}

		#endregion // TimeToEventFixedDesignEvents

		#endregion // Time to Event

	}
}
