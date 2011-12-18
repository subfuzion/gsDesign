namespace gsDesign.Design.SampleSize
{
	public class SampleSizeParameters
	{
		#region NFix property

		public string NFix
		{
			get
			{
				if (SampleSizeCategory == SampleSizeCategory.UserInput)
					return FixedDesignSampleSize.ToString();

				if (SampleSizeCategory == SampleSizeCategory.Binomial)
					// nBinomial(p1=p1, p2=p2, alpha=0.025, beta=0.1, delta0=delta0, ratio=1)
					return "(not implemented yet)";

				if (SampleSizeCategory == SampleSizeCategory.TimeToEvent)
					return "(not implemented yet)";

				return null;
			}
		}

		#endregion // NFix

		#region SampleSizeCategory property

		private SampleSizeCategory _sampleSizeCategory;

		public SampleSizeCategory SampleSizeCategory
		{
			get { return _sampleSizeCategory; }

			set
			{
				_sampleSizeCategory = value;
			}
		}

		#endregion // SampleSizeCategory

		#region User Input

		#region FixedDesignSampleSize property

		private int _fixedDesignSampleSize = 1;

		public int FixedDesignSampleSize
		{
			get { return _fixedDesignSampleSize; }

			set { _fixedDesignSampleSize = value; }
		}

		#endregion // FixedDesignSampleSize

		#endregion

		#region Binomial

		#region BinomialRandomizationRatio property

		private double _binomialRandomizationRatio = 1.0;

		public double BinomialRandomizationRatio
		{
			get { return _binomialRandomizationRatio; }

			set { _binomialRandomizationRatio = value; }
		}

		#endregion // BinomialRandomizationRatio

		#region BinomialControlEventRate property

		private double _binomialControlEventRate = 0.15;

		public double BinomialControlEventRate
		{
			get { return _binomialControlEventRate; }

			set { _binomialControlEventRate = value; }
		}

		#endregion // BinomialControlEventRate

		#region BinomialExperimentalEventRate property

		private double _binomialExperimentalEventRate = 0.1;

		public double BinomialExperimentalEventRate
		{
			get { return _binomialExperimentalEventRate; }

			set
			{
				_binomialExperimentalEventRate = value;
			}
		}

		#endregion // BinomialExperimentalEventRate

		#region BinomialNonInferiorityTesting property

		private BinomialNonInferiorityTesting _binomialNonInferiorityTesting = BinomialNonInferiorityTesting.Superiority;

		public BinomialNonInferiorityTesting BinomialNonInferiorityTesting
		{
			get { return _binomialNonInferiorityTesting; }

			set { _binomialNonInferiorityTesting = value; }
		}

		#endregion // BinomialNonInferiorityTesting

		#region BinomialDelta property

		private double _binomialDelta = 0;

		public double BinomialDelta
		{
			get { return _binomialDelta; }

			set { _binomialDelta = value; }
		}

		#endregion // BinomialDelta

		#region BinomialFixedDesignSampleSize property

		private int _binomialFixedDesignSampleSize = 0;

		public int BinomialFixedDesignSampleSize
		{
			get { return _binomialFixedDesignSampleSize; }

			set
			{
				_binomialFixedDesignSampleSize = value;
			}
		}

		#endregion // BinomialFixedDesignSampleSize

		#endregion // Binomial

		#region Time to Event

		#region TimeToEventSpecification property

		private TimeToEventSpecification _timeToEventSpecification = TimeToEventSpecification.MedianTime;

		public TimeToEventSpecification TimeToEventSpecification
		{
			get { return _timeToEventSpecification; }

			set
			{
				_timeToEventSpecification = value;
			}
		}

		#endregion // TimeToEventSpecification

		#region TimeToEventControl property

		private double _timeToEventControl = 6.0;

		public double TimeToEventControl
		{
			get { return _timeToEventControl; }

			set
			{
				_timeToEventControl = value;
			}
		}

		#endregion // TimeToEventControl

		#region TimeToEventExperimental property

		private double _timeToEventExperimental = 10.0;

		public double TimeToEventExperimental
		{
			get { return _timeToEventExperimental; }

			set
			{
				_timeToEventExperimental = value;
			}
		}

		#endregion // TimeToEventExperimental

		#region TimeToEventDropout property

		private double _timeToEventDropout = 12.0;

		public double TimeToEventDropout
		{
			get { return _timeToEventDropout; }

			set
			{
				_timeToEventDropout = value;
			}
		}

		#endregion // TimeToEventDropout

		#region TimeToEventHazardRatio property

		private double _timeToEventHazardRatio;

		public double TimeToEventHazardRatio
		{
			get { return _timeToEventHazardRatio; }

			set
			{
				_timeToEventHazardRatio = value;
			}
		}

		#endregion // TimeToEventHazardRatio

		#region TimeToEventAccrualDuration property

		private double _timeToEventAccrualDuration = 18.0;

		public double TimeToEventAccrualDuration
		{
			get { return _timeToEventAccrualDuration; }

			set
			{
				_timeToEventAccrualDuration = value;
			}
		}

		#endregion // TimeToEventAccrualDuration

		#region TimeToEventMinimumFollowUp property

		private double _timeToEventMinimumFollowUp = 12.0;

		public double TimeToEventMinimumFollowUp
		{
			get { return _timeToEventMinimumFollowUp; }

			set
			{
				_timeToEventMinimumFollowUp = value;
			}
		}

		#endregion // TimeToEventMinimumFollowUp

		#region TimeToEventRandomizationRatio property

		private double _timeToEventRandomizationRatio = 1.0;

		public double TimeToEventRandomizationRatio
		{
			get { return _timeToEventRandomizationRatio; }

			set
			{
				_timeToEventRandomizationRatio = value;
			}
		}

		#endregion // TimeToEventRandomizationRatio

		#region TimeToEventHypothesis property

		private TimeToEventHypothesis _timeToEventHypothesis = TimeToEventHypothesis.RiskRatio;

		public TimeToEventHypothesis TimeToEventHypothesis
		{
			get { return _timeToEventHypothesis; }

			set
			{
				_timeToEventHypothesis = value;
			}
		}

		#endregion // TimeToEventHypothesis

		#region TimeToEventAccrual property

		private TimeToEventAccrual _timeToEventAccrual = TimeToEventAccrual.Uniform;

		public TimeToEventAccrual TimeToEventAccrual
		{
			get { return _timeToEventAccrual; }

			set
			{
				_timeToEventAccrual = value;
			}
		}

		#endregion // TimeToEventAccrual

		#region TimeToEventGamma property

		private double _timeToEventGamma = 0.0001;

		public double TimeToEventGamma
		{
			get { return _timeToEventGamma; }

			set
			{
				_timeToEventGamma = value;
			}
		}

		#endregion // TimeToEventGamma

		#region TimeToEventFixedDesignSampleSize property

		private int _timeToEventFixedDesignSampleSize = 0;

		public int TimeToEventFixedDesignSampleSize
		{
			get { return _timeToEventFixedDesignSampleSize; }

			set
			{
				_timeToEventFixedDesignSampleSize = value;
			}
		}

		#endregion // TimeToEventFixedDesignSampleSize

		#region TimeToEventFixedDesignEvents property

		private int _timeToEventFixedDesignEvents = 0;

		public int TimeToEventFixedDesignEvents
		{
			get { return _timeToEventFixedDesignEvents; }

			set
			{
				_timeToEventFixedDesignEvents = value;
			}
		}

		#endregion // TimeToEventFixedDesignEvents

		#endregion

	}
}
