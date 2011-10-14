﻿namespace gsDesign.Explorer.Models
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Subfuzion.Helpers;

	public class Ept : NotifyPropertyChangedBase
	{
		private double ComputeValidPower(double power, double error)
		{
			error = Math.Round(error, 1);

			if (power - error < 0.0001)
			{
				power = error + 0.0001;
			}

			if (power > 100.0)
			{
				power = 100.0;
			}

			return Math.Round(power, 4);
		}

		// triggers notification if needs updating
		private void UpdatePower()
		{
			if (Power - Error < 0.0001)
			{
				Power = ComputeValidPower(Power, Error);
			}
		}

		#region Error property

		private double _error;

		public double Error
		{
			get { return _error; }

			set
			{
				if (Math.Abs(_error - value) >= 0.1)
				{
					_error = Math.Round(value, 1);
					RaisePropertyChanged("Error");
					UpdatePower();
					RaisePropertyChanged("MinimumValidPower");
				}
			}
		}

		#endregion // Error

		#region Power property

		private double _power;

		public double Power
		{
			get { return _power; }

			set
			{
				var power = ComputeValidPower(value, Error);
				if (Math.Abs(_power - power) >= 0.0001)
				{
					_power = power;
					RaisePropertyChanged("Power");
					RaisePropertyChanged("MinimumValidPower");
				}
			}
		}

		#endregion // Power

		#region MinimumValidPower property

		public double MinimumValidPower
		{
			get { return Math.Min(Math.Round(Error + 0.0001, 4), MaximumValidPower); }
		}

		#endregion // MinimumValidPower

		#region MaximumValidPower property

		private double _maximumValidPower = 100.0;

		public double MaximumValidPower
		{
			get { return _maximumValidPower; }

			set
			{
				if (Math.Abs(_maximumValidPower - value) > double.Epsilon)
				{
					_maximumValidPower = value;
					RaisePropertyChanged("MaximumValidPower");
				}
			}
		}

		#endregion // MaximumValidPower

		#region IntervalCount property

		private int _intervalCount = 1;

		public int IntervalCount
		{
			get { return _intervalCount; }

			set
			{
				if (_intervalCount != value && value >= MinimumIntervalCount && value <= MaximumIntervalCount)
				{
					_intervalCount = value;
					RaisePropertyChanged("IntervalCount");
					UpdateTimingTable();
				}
			}
		}

		#endregion // IntervalCount

		#region MinimumIntervalCount property

		private int _minimumIntervalCount = 1;

		public int MinimumIntervalCount
		{
			get { return _minimumIntervalCount; }

			set
			{
				if (_minimumIntervalCount != value)
				{
					_minimumIntervalCount = value;
					RaisePropertyChanged("MinimumIntervalCount");
				}
			}
		}

		#endregion // MinimumIntervalCount

		#region MaximumIntervalCount property

		private int _maximumIntervalCount = 99;

		public int MaximumIntervalCount
		{
			get { return _maximumIntervalCount; }

			set
			{
				if (_maximumIntervalCount != value)
				{
					_maximumIntervalCount = value;
					RaisePropertyChanged("MaximumIntervalCount");
				}
			}
		}

		#endregion // MaximumIntervalCount

		#region Spacing property

		private EptSpacing _spacing;

		public IEnumerable<string> SpacingValues
		{
			get { return EnumHelper.GetNames<EptSpacing>(); }
		}

		public EptSpacing Spacing
		{
			get { return _spacing; }

			set
			{
				if (_spacing != value)
				{
					_spacing = value;
					RaisePropertyChanged("Spacing");
					RaisePropertyChanged("IsTimingTableEnabled");
					UpdateTimingTable();
				}
			}
		}

		#endregion // Spacing

		#region TimingTable property

		private ObservableCollection<Timing> _timingTable = new ObservableCollection<Timing>
		    {
		        new Timing {Index = 1, Value = 0.5},
		    };

		public ObservableCollection<Timing> TimingTable
		{
			get { return _timingTable; }

			set
			{
				if (_timingTable != value)
				{
					_timingTable = value;
					RaisePropertyChanged("TimingTable");
				}
			}
		}

		#endregion // TimingTable

		#region IsTimingTableEnabled property

		public bool IsTimingTableEnabled
		{
			get { return Spacing == EptSpacing.Unequal; }
		}

		#endregion // IsTimingTableEnabled

		private void UpdateTimingTable()
		{
			if (TimingTable.Count < IntervalCount)
			{
				var increment = 0.5;
				for (int i = TimingTable.Count; i < IntervalCount; i++)
				{
					TimingTable.Add(new Timing { Index = i+1, Value = increment});
				}
			}
			else if (TimingTable.Count > IntervalCount)
			{
				for (int i = IntervalCount, count = TimingTable.Count - IntervalCount; count > 0; count--)
				{
					TimingTable.RemoveAt(i);
				}
			}


			RaisePropertyChanged("TimingTable");
		}
	}

	public class Timing
	{
		public int Index { get; set; }
		public double Value { get; set; }
	}
}