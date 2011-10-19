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

	}
}
