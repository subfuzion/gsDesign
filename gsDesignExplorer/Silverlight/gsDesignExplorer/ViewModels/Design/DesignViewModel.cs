namespace gsDesign.Explorer.ViewModels.Design
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using Subfuzion.Helpers;
	using Models;

	public class DesignViewModel : NotifyPropertyChangedBase
	{
		#region EptError property

		private double _eptError;

		/// <summary>
		/// Valid range: 0.0 - 100.0
		/// Increment: 0.1
		/// </summary>
		public double EptError
		{
			get { return _eptError; }

			set
			{
				if (Math.Abs(_eptError - value) > double.Epsilon)
				{
					_eptError = value;
					RaisePropertyChanged("EptError");
				}
			}
		}

		#endregion // EptError

		#region EptPower property

		private double _eptPower;

		/// <summary>
		/// Valid range: EptError + 0.0001 - 100.0
		/// Increment: 0.0005
		/// </summary>
		public double EptPower
		{
			get { return _eptPower; }

			set
			{
				if (Math.Abs(_eptPower - value) > double.Epsilon)
				{
					_eptPower = value;
					RaisePropertyChanged("EptPower");
				}
			}
		}

		#endregion // EptPower

		#region EptInterval property

		private string _eptInterval;

		/// <summary>
		/// Valid range: 1 - 99
		/// </summary>
		public string EptInterval
		{
			get { return _eptInterval; }

			set
			{
				if (_eptInterval != value)
				{
					_eptInterval = value;
					RaisePropertyChanged("EptInterval");
				}
			}
		}

		#endregion // EptInterval

		#region EptSpacing property

		private EptSpacing _eptSpacing;

		public EptSpacing EptSpacing
		{
			get { return _eptSpacing; }

			set
			{
				if (_eptSpacing != value)
				{
					_eptSpacing = value;
					RaisePropertyChanged("EptSpacing");
				}
			}
		}

		#endregion // EptSpacing

		#region EptTimingTable property

		private List<string> _eptTimingTable;

		public List<string> EptTimingTable
		{
			get { return _eptTimingTable; }

			set
			{
				if (_eptTimingTable != value)
				{
					_eptTimingTable = value;
					RaisePropertyChanged("EptTimingTable");
				}
			}
		}

		#endregion // EptTimingTable


		//private void RemoveDesignHandlers()
		//{
		//    if (Design != null)
		//    {
		//        Design.Ept.PropertyChanged -= OnEptPropertyChanged;
		//    }
		//}

		//private void AddDesignHandlers()
		//{
		//    if (Design != null)
		//    {
		//        Design.Ept.PropertyChanged += OnEptPropertyChanged;
		//    }
		//}

		//void OnEptPropertyChanged(object sender, PropertyChangedEventArgs e)
		//{
		//    var ept = (Ept) sender;

		//    switch (e.PropertyName)
		//    {
		//        case "Error":
		//            EptError = ept.Error;
		//            break;

		//        case "Power":
		//            EptPower = ept.Power;
		//            break;

		//        case "IntervalCount":
		//            break;

		//        case "Spacing":
		//            break;

		//        case "TimingTable":
		//            break;

		//        default:
		//            throw new Exception("Unrecognized property: " + e.PropertyName);
		//    }
		//}


		private void Update()
		{
			
		}
	
	}
}
