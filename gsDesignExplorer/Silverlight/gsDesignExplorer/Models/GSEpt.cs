namespace gsDesign.Explorer.Models
{
	using System;
	using System.Collections.Generic;

	public class GSEpt
	{
		#region Alpha property

		private double _alpha;

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

		private double _beta;

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

		private int _k;

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

		private List<double> _timing;

		public List<double> Timing
		{
			get { return _timing ?? (_timing = new List<double>()); }

			set
			{
				_timing = value;
			}
		}

		#endregion // Timing

	}
}
