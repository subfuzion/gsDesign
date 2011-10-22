namespace gsDesign.Explorer.ViewModels.Design
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
					RaisePropertyChanged("Index");
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
					RaisePropertyChanged("Value");
				}
			}
		}

		#endregion // Value
	}
}