namespace gsDesign.Explorer.Models
{
	public class GSSampleSize
	{
		#region NFix property

		public string NFix
		{
			get
			{
				if (SampleSizeType == SampleSizeType.UserInput)
					return FixedDesignSampleSize.ToString();

				if (SampleSizeType == SampleSizeType.Binomial)
					return "(not implemented yet)";

				if (SampleSizeType == SampleSizeType.TimeToEvent)
					return "(not implemented yet)";

				return null;
			}
		}

		#endregion // NFix

		#region SampleSizeType property

		private SampleSizeType _sampleSizeType;

		public SampleSizeType SampleSizeType
		{
			get { return _sampleSizeType; }

			set
			{
				_sampleSizeType = value;
			}
		}

		#endregion // SampleSizeType

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

		#region RandomizationRatio property

		private double _randomizationRatio = 1.0;

		public double RandomizationRatio
		{
			get { return _randomizationRatio; }

			set { _randomizationRatio = value; }
		}

		#endregion // RandomizationRatio

		#region ControlEventRate property

		private double _controlEventRate = 0.15;

		public double ControlEventRate
		{
			get { return _controlEventRate; }

			set { _controlEventRate = value; }
		}

		#endregion // ControlEventRate

		#region NonInferiorityTesting property

		private BinomialNonInferiorityTesting _nonInferiorityTesting = BinomialNonInferiorityTesting.Superiority;

		public BinomialNonInferiorityTesting NonInferiorityTesting
		{
			get { return _nonInferiorityTesting; }

			set { _nonInferiorityTesting = value; }
		}

		#endregion // NonInferiorityTesting

		#region Delta property

		private double _delta = 0;

		public double Delta
		{
			get { return _delta; }

			set { _delta = value; }
		}

		#endregion // Delta

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
