namespace gsDesign.Explorer.Models
{
	using Design.ErrorPowerTiming;
	using Design.SampleSize;
	using Design.SpendingFunctions;

	public class DesignParameters
	{
		public void Reset()
		{
			ErrorPowerTimingParameters = new ErrorPowerTimingParameters();
			SampleSizeParameters = new SampleSizeParameters();
			SpendingFunctionParameters = new SpendingFunctionParameters();
		}

		#region Name property

		private string _name;

		public string Name
		{
			get { return _name; }

			set
			{
				_name = value;
			}
		}

		#endregion // Name

		#region Description property

		private string _description;

		public string Description
		{
			get { return _description; }

			set
			{
				_description = value;
			}
		}

		#endregion // Description

		#region ErrorPowerTimingParameters property

		private ErrorPowerTimingParameters _errorPowerTimingParameters;

		public ErrorPowerTimingParameters ErrorPowerTimingParameters
		{
			get { return _errorPowerTimingParameters ?? (_errorPowerTimingParameters = new ErrorPowerTimingParameters()); }

			set
			{
				_errorPowerTimingParameters = value;
			}
		}

		#endregion // ErrorPowerTimingParameters

		#region SampleSizeParameters property

		private SampleSizeParameters _sampleSizeParameters;

		public SampleSizeParameters SampleSizeParameters
		{
			get { return _sampleSizeParameters ?? (_sampleSizeParameters = new SampleSizeParameters()); }

			set
			{
				_sampleSizeParameters = value;
			}
		}

		#endregion // SampleSizeParameters

		#region SpendingFunctionParameters property

		private SpendingFunctionParameters _spendingFunctionParameters;

		public SpendingFunctionParameters SpendingFunctionParameters
		{
			get { return _spendingFunctionParameters ?? (_spendingFunctionParameters = new SpendingFunctionParameters()); }

			set
			{
				_spendingFunctionParameters = value;
			}
		}

		#endregion // SpendingFunctionParameters
	}
}
