namespace gsDesign.Explorer.ViewModels.Design
{
	using System.Collections.Generic;
	using Models;
	using Subfuzion.Helpers;

	public class SampleSize : NotifyPropertyChangedBase
	{
		private GSDesign _design;

		public SampleSize(GSDesign design)
		{
			_design = design;

			SampleSizeType = SampleSizeType.UserInput;
			FixedDesignSampleSize = 1;
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

	}
}
