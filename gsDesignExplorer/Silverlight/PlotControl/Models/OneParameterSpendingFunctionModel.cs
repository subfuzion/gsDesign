namespace Subfuzion.Silverlight.UI.Charting.Models
{
	using System.Collections.ObjectModel;
	using System.Windows;

	/// <summary>
	/// Solve for y
	/// </summary>
	/// <param name="yMax"></param>
	/// <param name="x">timing</param>
	/// <param name="spendingParameter"></param>
	/// <returns></returns>
	public delegate double SpendingFunction(double yMax, double x, double spendingParameter);

	/// <summary>
	/// Solve for x
	/// </summary>
	/// <param name="yMax"></param>
	/// <param name="y">interim spending</param>
	/// <param name="spendingParameter"></param>
	/// <returns></returns>
	public delegate double InverseSpendingFunction(double yMax, double y, double spendingParameter);

	/// <summary>
	/// Solve for spending parameter
	/// </summary>
	/// <param name="yMax"></param>
	/// <param name="y">interim spending</param>
	/// <param name="x">timing</param>
	/// <returns></returns>
	public delegate double FittingSpendingFunction(double yMax, double y, double x);

	public class OneParameterSpendingFunctionModel
	{
		public OneParameterSpendingFunctionModel()
		{
			var coordinates = new ObservableCollection<Point>();
			for (var i = 0; i < 30; i++)
			{
				coordinates.Add(new Point(0, 0));
			}

			Coordinates = coordinates;

		}

		#region Functions

		/// <summary>
		/// Solve for y (interim spending)
		/// </summary>
		public SpendingFunction SpendingFunction { get; set; }

		/// <summary>
		/// Solve for x (timing)
		/// </summary>
		public InverseSpendingFunction InverseSpendingFunction { get; set; }

		/// <summary>
		/// Solve for spending parameter
		/// </summary>
		public FittingSpendingFunction ParameterSpendingFunction { get; set; }

		#endregion

		#region Function Parameters

		public double X { get; set; }

		public double XMin { get; set; }

		public double XMax { get; set; }

		public double Y { get; set; }

		public double YMin { get; set; }

		public double YMax { get; set; }

		public double SpendingParameter { get; set; }

		#endregion

		#region Methods

		public void UpdateLine()
		{
			var incrementCount = Coordinates.Count;
			var intervalCount = incrementCount - 1;

			Coordinates[0] = new Point(XMin, SpendingFunction(YMax, XMin, SpendingParameter));
			Coordinates[Coordinates.Count - 1] = new Point(XMax, SpendingFunction(YMax, XMax, SpendingParameter));

			// compute x-axis interval
			var increment = (XMax - XMin) / intervalCount;

			// compute y values for x values between min and max
			for (var i = 1; i < Coordinates.Count - 1; i++)
			{
				// timing = x
				var x = Coordinates[i - 1].X + increment;

				// y is a function of YMax, timing, & spending value
				var y = SpendingFunction(YMax, x, SpendingParameter);
				Coordinates[i] = new Point(x, y);
			}
		}

		#endregion

		#region Coordinates property

		public ObservableCollection<Point> Coordinates { get; set; }

		#endregion Coordinates
	}
}
