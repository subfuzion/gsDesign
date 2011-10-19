namespace gsDesign.Explorer.Models
{
	using System.Collections.Generic;

	public class GSEpt
	{
		#region Alpha property

		private double _alpha = 0.025;

		public double Alpha
		{
			get { return _alpha; }

			set
			{
				_alpha = value;
			}
		}

		#endregion // Alpha

		#region Beta property

		private double _beta = 0.1;

		public double Beta
		{
			get { return _beta; }

			set
			{
				_beta = value;
			}
		}

		#endregion // Beta

		#region K property

		private int _k = 3;

		public int K
		{
			get { return _k; }

			set
			{
				_k = value;
			}
		}

		#endregion // K

		#region Timing property

		private List<double> _timing = new List<double>
		                               {
		                               	0.3333,
		                               	0.6667,
		                               };

		public List<double> Timing
		{
			get { return _timing ?? (_timing = new List<double>{0.3333, 0.6667}); }

			set
			{
				_timing = value;
			}
		}

		#endregion // Timing

	}
}
