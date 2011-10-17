namespace gsDesign.Explorer.Models
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel.DataAnnotations;
	using Subfuzion.Helpers;

	public class Ept : NotifyPropertyChangedBase
	{
		public Ept()
		{
			Error = 2.5;
			Power = 90.0;
		}

		#region Error property

		private double _error;

		[Display(Name = "Type I Error (1-sided \u03B1 x 100)",
			Description = "0 < Type I Error < Power < 100")]
		public double Error
		{
			get { return _error; }

			set
			{
				if (Math.Abs(_error - value) >= double.Epsilon)
				{
					if (!ValidateErrorValue(value))
						throw new ArgumentException("Error must be a value between 0.0 and 100.0, inclusive");

					_error = Math.Round(value, 4);
					RaisePropertyChanged("Error");

					UpdatePower();
					RaisePropertyChanged("MinimumValidPower");
					RaisePropertyChanged("MinimumPowerDisplay");
				}
			}
		}

		#endregion // Error

		public bool ValidateErrorValue(double value)
		{
			return !(MinimumValidError - value > double.Epsilon || value - MaximumValidError > double.Epsilon);
		}

		#region MinimumValidError property

		public double MinimumValidError
		{
			get { return 0.0; }
		}

		#endregion // MinimumValidError

		#region MaximumValidError property

		public double MaximumValidError
		{
			get { return 100.0; }
		}

		#endregion // MaximumValidError



		#region Power property

		private double _power;

		[Display(Name = "Power (100 x [1-\u03B2])",
			Description = "0 < Type I Error < Power < 100")]
		public double Power
		{
			get { return _power; }

			set
			{
				var power = ComputeValidPower(value, Error);
				if (Math.Abs(_power - power) > double.Epsilon)
				{
					_power = power;
				}

				// because of slider to spinner binding, we need this out
				// here, otherwise slider doesn't always update properly
				// (depending on when the mouse button is actually released)
				RaisePropertyChanged("Power");
				RaisePropertyChanged("MinimumValidPower");
				RaisePropertyChanged("MinimumPowerDisplay");
			}
		}

		#endregion // Power

		#region MinimumValidPower property

		public double MinimumValidPower
		{
			get { return Math.Min(Math.Round(Error + 0.1, 1), MaximumValidPower); }
		}

		#endregion // MinimumValidPower

		#region MaximumValidPower property

		public double MaximumValidPower
		{
			get { return 100.0; }
		}

		public string MinimumPowerDisplay
		{
			get { return string.Format("0.1 (current value={0}, minimum={1})", Power, MinimumValidPower); }
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

		#region Implementation

		private double ComputeValidPower(double power, double error)
		{
			if (power - error < 0.1)
			{
				power = error + 0.1;
			}

			if (power > MaximumValidPower)
			{
				power = MaximumValidPower;
			}

			return Math.Round(power, 1);
		}

		// triggers notification if needs updating
		private void UpdatePower()
		{
			Power = ComputeValidPower(Power, Error);
		}

		private void UpdateTimingTable()
		{
			if (TimingTable.Count < IntervalCount)
			{
				var increment = 0.5;
				for (int i = TimingTable.Count; i < IntervalCount; i++)
				{
					TimingTable.Add(new Timing {Index = i + 1, Value = increment});
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

		#endregion
	}

	public class Timing
	{
		public int Index { get; set; }
		public double Value { get; set; }
	}
}
