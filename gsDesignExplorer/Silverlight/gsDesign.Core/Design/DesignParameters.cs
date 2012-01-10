namespace gsDesign.Design
{
	using ErrorPowerTiming;
	using SampleSize;
	using SpendingFunctions;

	public class DesignParameters
	{
		public void Reset()
		{
			ErrorPowerTimingParameters = new ErrorPowerTimingParameters();
			SampleSizeParameters = new SampleSizeParameters();
			SpendingFunctionParameters = new SpendingFunctionParameters(this);
		}

		#region Name property

		public string Name { get; set; }

		#endregion // Name

		#region Description property

		public string Description { get; set; }

		#endregion // Description

		#region ErrorPowerTimingParameters property

		private ErrorPowerTimingParameters _errorPowerTimingParameters;

		public ErrorPowerTimingParameters ErrorPowerTimingParameters
		{
			get { return _errorPowerTimingParameters ?? (_errorPowerTimingParameters = new ErrorPowerTimingParameters()); }

			set { _errorPowerTimingParameters = value; }
		}

		#endregion // ErrorPowerTimingParameters

		#region SampleSizeParameters property

		private SampleSizeParameters _sampleSizeParameters;

		public SampleSizeParameters SampleSizeParameters
		{
			get { return _sampleSizeParameters ?? (_sampleSizeParameters = new SampleSizeParameters()); }

			set { _sampleSizeParameters = value; }
		}

		#endregion // SampleSizeParameters

		#region SpendingFunctionParameters property

		private SpendingFunctionParameters _spendingFunctionParameters;

		public SpendingFunctionParameters SpendingFunctionParameters
		{
			get { return _spendingFunctionParameters ?? (_spendingFunctionParameters = new SpendingFunctionParameters(this)); }

			set { _spendingFunctionParameters = value; }
		}

		#endregion // SpendingFunctionParameters
	}
}