namespace gsDesign.Explorer.ViewModels.Design.ErrorPowerTiming
{
	using System;
	using Subfuzion.Helpers;

	public class IntervalTiming : NotifyPropertyChangedBase
	{
		#region Index property

		private int _index;

		public int Index
		{
			get { return _index; }

			set
			{
				if (_index != value)
				{
					_index = value;
					NotifyPropertyChanged("Index");
				}
			}
		}

		#endregion // Index

		#region Value property

		private double _value;

		public double Value
		{
			get { return _value; }

			set
			{
				if (Math.Abs(_value - value) > double.Epsilon)
				{
					_value = value;
					NotifyPropertyChanged("Value");
				}
			}
		}

		#endregion // Value
	}
}