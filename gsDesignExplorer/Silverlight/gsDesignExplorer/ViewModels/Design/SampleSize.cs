namespace gsDesign.Explorer.ViewModels.Design
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using Models;
	using Subfuzion.Helpers;

	public class SampleSize : NotifyPropertyChangedBase
	{
		private GSDesign _design;

		public SampleSize(GSDesign design)
		{
			_design = design;
		}

		private GSSampleSize Model
		{
			get { return _design.SampleSize; }
		}

		#region SampleSizeTypes property

		public IEnumerable<string> SampleSizeTypes
		{
			get { return EnumHelper.GetNames<SampleSizeType>(); }
		}

		#endregion // SampleSizeTypes

		#region CurrentSampleSizeType property

		public int CurrentSampleSizeType
		{
			get { return (int)SampleSizeType; }

			set
			{
				if (SampleSizeType != (SampleSizeType)value)
				{
					SampleSizeType = (SampleSizeType)value;
					RaisePropertyChanged("CurrentSampleSizeType");
				}
			}
		}

		#endregion // CurrentSampleSizeType

		#region SampleSizeType property

		public SampleSizeType SampleSizeType
		{
			get { return Model.SampleSizeType; }

			set
			{
				if (Model.SampleSizeType != value)
				{
					Model.SampleSizeType = value;
					RaisePropertyChanged("SampleSizeType");
				}
			}
		}

		#endregion // SampleSizeType

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


		#endregion

		#region Binomial

		#region RandomizationRatio property

		[Display(Name = "Randomization Ratio",
			Description = "Experimental/control relative sample size:\n0 \u003C Ratio \u2264 1000")]
		public double RandomizationRatio
		{
			get { return Model.RandomizationRatio; }

			set
			{
				if (Math.Abs(Model.RandomizationRatio - value) > double.Epsilon)
				{
					Model.RandomizationRatio = value;
					RaisePropertyChanged("RandomizationRatio");
				}
			}
		}

		#endregion // RandomizationRatio

		#region MinimumRandomizationRatio property

		private double _minimumRandomizationRatio = 0.001;

		// [Display(Name = "MinimumRandomizationRatio",
		//	Description = "")]
		public double MinimumRandomizationRatio
		{
			get { return _minimumRandomizationRatio; }

			set
			{
				if (Math.Abs(_minimumRandomizationRatio - value) > double.Epsilon)
				{
					_minimumRandomizationRatio = value;
					RaisePropertyChanged("MinimumRandomizationRatio");
				}
			}
		}

		#endregion // MinimumRandomizationRatio

		#region MaximumRandomizationRatio property

		private double _maximumRandomizationRatio = 1000.0;

		// [Display(Name = "MaximumRandomizationRatio",
		//	Description = "")]
		public double MaximumRandomizationRatio
		{
			get { return _maximumRandomizationRatio; }

			set
			{
				if (Math.Abs(_maximumRandomizationRatio - value) > double.Epsilon)
				{
					_maximumRandomizationRatio = value;
					RaisePropertyChanged("MaximumRandomizationRatio");
				}
			}
		}

		#endregion // MaximumRandomizationRatio

		#region ControlEventRate property

		[Display(Name = "Control",
			Description = "Control event rate:\n0 \u003C Control \u003C 1")]
		public double ControlEventRate
		{
			get { return Model.ControlEventRate; }

			set
			{
				if (Math.Abs(Model.ControlEventRate - value) > double.Epsilon)
				{
					Model.ControlEventRate = value;
					RaisePropertyChanged("ControlEventRate");
				}
			}
		}

		#endregion // ControlEventRate

		#region MinimumControlEventRate property

		public double MinimumControlEventRate
		{
			get { return 0.0001; }
		}

		#endregion // MinimumControlEventRate

		#region MaximumControlEventRate property

		public double MaximumControlEventRate
		{
			get { return 0.9999; }
		}

		#endregion // MaximumControlEventRate

		#region ExperimentalEventRate property

		private double _experimentalEventRate;

		[Display(Name = "Experimental",
			Description = "Experimental event rate:\n0 \u003C Experimental \u003C 1")]
		public double ExperimentalEventRate
		{
			get { return _experimentalEventRate; }

			set
			{
				if (Math.Abs(_experimentalEventRate - value) > double.Epsilon)
				{
					_experimentalEventRate = value;
					RaisePropertyChanged("ExperimentalEventRate");
				}
			}
		}

		#endregion // ExperimentalEventRate

		#region MinimumExperimentalEventRate property

		public double MinimumExperimentalEventRate
		{
			get { return 0.0001; }
		}

		#endregion // MinimumExperimentalEventRate

		#region MaximumExperimentalEventRate property

		public double MaximumExperimentalEventRate
		{
			get { return 0.9999; }
		}

		#endregion // MaximumExperimentalEventRate

		#region NonInferiorityTesting property

		[Display(Name = "",
			Description = "Specify either 'Superiority' or\n" +
			              "'Non-Inferiority - superiority with margin'")]
		public BinomialNonInferiorityTesting NonInferiorityTesting
		{
			get { return Model.NonInferiorityTesting; }

			set
			{
				if (Model.NonInferiorityTesting != value)
				{
					Model.NonInferiorityTesting = value;
					RaisePropertyChanged("NonInferiorityTesting");

					Delta = 0.0;
					RaisePropertyChanged("DeltaIsEnabled");
				}
			}
		}

		#endregion // NonInferiorityTesting

		#region Delta property

		[Display(Name = "Delta",
			Description = "Margin for treatment difference")]
		public double Delta
		{
			get { return Model.Delta; }

			set
			{
				if (Math.Abs(Model.Delta - value) > double.Epsilon)
				{
					Model.Delta = value;
					RaisePropertyChanged("Delta");
					RaisePropertyChanged("DeltaIsEnabled");
				}
			}
		}

		#endregion // Delta

		#region DeltaIsEnabled property

		public bool DeltaIsEnabled
		{
			get { return Model.NonInferiorityTesting == BinomialNonInferiorityTesting.SuperiorityWithMargin; }
		}

		#endregion // DeltaIsEnabled

		#region MinimumDelta property

		public double MinimumDelta
		{
			get { return -0.0101; }
		}

		#endregion // MinimumDelta

		#region MaximumDelta property

		public double MaximumDelta
		{
			get { return 0.9899; }
		}

		#endregion // MaximumDelta

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
