namespace gsDesign.Explorer.Models
{
	public class GSDesign
	{
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

		#region Ept property

		private GSEpt _ept;

		public GSEpt Ept
		{
			get { return _ept ?? (_ept = new GSEpt()); }

			set
			{
				_ept = value;
			}
		}

		#endregion // Ept

		#region SampleSize property

		private GSSampleSize _sampleSize;

		public GSSampleSize SampleSize
		{
			get { return _sampleSize ?? (_sampleSize = new GSSampleSize()); }

			set
			{
				_sampleSize = value;
			}
		}

		#endregion // SampleSize

		#region SpendingFunctions property

		private GSSpendingFunctions _spendingFunctions;

		public GSSpendingFunctions SpendingFunctions
		{
			get { return _spendingFunctions ?? (_spendingFunctions = new GSSpendingFunctions()); }

			set
			{
				_spendingFunctions = value;
			}
		}

		#endregion // SpendingFunctions

	}
}
