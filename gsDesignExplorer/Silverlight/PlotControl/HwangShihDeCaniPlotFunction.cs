namespace Subfuzion.Silverlight.UI.Charting
{
	using System;
	using System.Collections.ObjectModel;
	using System.Windows;

	public class HwangShihDeCaniPlotFunction : PlotFunctionBase
	{
		public static double HwangShihDeCaniFunction(double alpha, double timing, double sfValue)
		{
			return alpha*(1 - Math.Exp(-sfValue*timing))/(1 - Math.Exp(-sfValue));
		}

		public HwangShihDeCaniPlotFunction()
		{
			var coordinates = new ObservableCollection<Point>();
			for (int i = 0; i < 30; i++)
			{
				coordinates.Add(new Point(0,0));
			}
		}

		public override void Update()
		{
			var incrementCount = Coordinates.Count;
			var intervalCount = incrementCount - 1;

			Coordinates[0] = new Point(MinimumX, HwangShihDeCaniFunction(Alpha, MinimumX, SpendingFunctionValue));
			Coordinates[Coordinates.Count - 1] = new Point(MaximumX, HwangShihDeCaniFunction(Alpha, MaximumX, SpendingFunctionValue));

			var increment = (MaximumX - MinimumX) / intervalCount;

			// compute y values for x values between min and max
			for (var i = 1; i < Coordinates.Count - 1; i++)
			{
				var x = Coordinates[i - 1].X;
				var y = HwangShihDeCaniFunction(Alpha, x, SpendingFunctionValue);
				Coordinates[i] = new Point(x,y);
			}
		}

		#region Alpha property

		private double _alpha;

		/// <summary>
		/// Gets or sets the Alpha property.
		/// </summary>
		public double Alpha
		{
			get { return _alpha; }

			set
			{
				if (Math.Abs(_alpha - value) > double.Epsilon)
				{
					_alpha = value;
					NotifyPropertyChanged("Alpha");
				}
			}
		}

		#endregion Alpha

		#region Timing property

		private double _timing;

		/// <summary>
		/// Gets or sets the Timing property.
		/// </summary>
		public double Timing
		{
			get { return _timing; }

			set
			{
				if (Math.Abs(_timing - value) > double.Epsilon)
				{
					_timing = value;
					NotifyPropertyChanged("Timing");
				}
			}
		}

		#endregion Timing

		#region SpendingFunctionValue property

		private double _spendingFunctionValue;

		/// <summary>
		/// Gets or sets the SpendingFunctionValue property.
		/// </summary>
		public double SpendingFunctionValue
		{
			get { return _spendingFunctionValue; }

			set
			{
				if (Math.Abs(_spendingFunctionValue - value) > double.Epsilon)
				{
					_spendingFunctionValue = value;
					NotifyPropertyChanged("SpendingFunctionValue");
				}
			}
		}

		#endregion SpendingFunctionValue
	}
}