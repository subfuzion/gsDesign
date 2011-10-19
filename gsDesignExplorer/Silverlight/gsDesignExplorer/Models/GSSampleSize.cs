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

		#region FixedDesignSampleSize property

		private int _fixedDesignSampleSize;

		public int FixedDesignSampleSize
		{
			get { return _fixedDesignSampleSize; }

			set
			{
				_fixedDesignSampleSize = value;
			}
		}

		#endregion // FixedDesignSampleSize

		#region RandomizationRatio property

		private double _randomizationRatio;

		public double RandomizationRatio
		{
			get { return _randomizationRatio; }

			set
			{
				_randomizationRatio = value;
			}
		}

		#endregion // RandomizationRatio

	}
}
