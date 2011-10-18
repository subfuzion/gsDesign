namespace gsDesign.Explorer.Models
{
	public class GSSampleSize
	{
		#region SampleSizeType property

		private SampleSizeType _sampleSizeType;

		public SampleSizeType SampleSizeType
		{
			get { return _sampleSizeType; }

			set
			{
				if (_sampleSizeType != value)
				{
					_sampleSizeType = value;
				}
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


	}
}
