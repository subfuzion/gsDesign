namespace gsDesign.Explorer.ViewModels.Design
{
	using System;
	using Models;
	using Models.Output;
	using Subfuzion.Helpers;

	public class Design : NotifyPropertyChangedBase
	{
		private DesignParameters _designParameters;

		public Design(Func<string, bool> nameValidator)
		{
			NameValidator = nameValidator;
			_designScript = new DesignScript {Design = this};
		}

		internal DesignParameters Model
		{
			get { return _designParameters ?? (_designParameters = new DesignParameters()); }
		}

		public void Reset()
		{
			Model.Reset();
			NotifyPropertyChanged("CurrentDesign");
			ErrorPowerTiming = new ErrorPowerTiming.ErrorPowerTiming(Model);
			SampleSize = new SampleSize.SampleSize(Model);
			SpendingFunctions = new SpendingFunctions.SpendingFunctions(Model);
		}

		#region Name property

		public string Name
		{
			get { return Model.Name; }

			set
			{
				if (Model.Name != value)
				{
					// test here instead of above because we do want
					// to fire the property changed event to force the
					// the UI to refresh regardless
					if (NameValidator == null || (NameValidator != null && NameValidator(value)))
					{
						Model.Name = value;
					}
					NotifyPropertyChanged("Name");
				}
			}
		}

		public Func<string, bool> NameValidator { get; set; }

		#endregion // Name

		#region Description property

		public string Description
		{
			get { return Model.Description ?? string.Format("{0} description...", Name ?? "Design"); }

			set
			{
				if (Model.Description != value)
				{
					Model.Description = value;
					NotifyPropertyChanged("Description");
				}
			}
		}

		#endregion // Description

		#region ErrorPowerTiming property

		private ErrorPowerTiming.ErrorPowerTiming _errorPowerTiming;

		public ErrorPowerTiming.ErrorPowerTiming ErrorPowerTiming
		{
			get
			{
				if (_errorPowerTiming == null)
				{
					_errorPowerTiming = new ErrorPowerTiming.ErrorPowerTiming(Model);
					NotifyPropertyChanged("ErrorPowerTiming");
				}

				return _errorPowerTiming;
			}

			set
			{
				if (_errorPowerTiming != value)
				{
					_errorPowerTiming = value;
					NotifyPropertyChanged("ErrorPowerTiming");
				}
			}
		}

		#endregion // ErrorPowerTimingParameters

		#region SampleSize property

		private SampleSize.SampleSize _sampleSize;

		public SampleSize.SampleSize SampleSize
		{
			get
			{
				if (_sampleSize == null)
				{
					_sampleSize = new SampleSize.SampleSize(Model);
					NotifyPropertyChanged("SampleSize");
				}

				return _sampleSize;
			}

			set
			{
				if (_sampleSize != value)
				{
					_sampleSize = value;
					NotifyPropertyChanged("SampleSize");
				}
			}
		}

		#endregion // SampleSize

		#region SpendingFunctions property

		private SpendingFunctions.SpendingFunctions _spendingFunctions;

		public SpendingFunctions.SpendingFunctions SpendingFunctions
		{
			get
			{
				if (_spendingFunctions == null)
				{
					_spendingFunctions = new SpendingFunctions.SpendingFunctions(Model);
					NotifyPropertyChanged("SpendingFunctions");
				}

				return _spendingFunctions;
			}

			set
			{
				if (_spendingFunctions != value)
				{
					_spendingFunctions = value;
					NotifyPropertyChanged("SpendingFunctions");
				}
			}
		}

		#endregion // SpendingFunctions

		#region DesignScript property

		private DesignScript _designScript;

		public DesignScript DesignScript
		{
			get { return _designScript; }
		}

		#endregion // DesignScript

		#region Output

		#region PlotType

		private PlotType _plotType;

		/// <summary>
		/// Gets or sets the PlotType property.
		/// </summary>
		public PlotType PlotType
		{
			get { return _plotType; }
			set
			{
				if (_plotType != value)
				{
					_plotType = value;
					NotifyPropertyChanged("PlotType");
				}
			}
		}

		#endregion PlotType

		#region PlotRendering

		private PlotRendering _plotRendering;

		/// <summary>
		/// Gets or sets the PlotRendering property.
		/// </summary>
		public PlotRendering PlotRendering
		{
			get { return _plotRendering; }
			set
			{
				if (_plotRendering != value)
				{
					_plotRendering = value;
					NotifyPropertyChanged("PlotRendering");
				}
			}
		}

		#endregion PlotRendering



		#endregion


		#region General properties

		#region IsModified property

		private bool _isModified;

		public bool IsModified
		{
			get { return _isModified; }

			set
			{
				if (_isModified != value)
				{
					_isModified = value;
					NotifyPropertyChanged("IsModified");
				}
			}
		}

		#endregion // IsModified

		#endregion

		#region Object overrides

		public override string ToString()
		{
			return Name ?? "(Design)";
		}

		#region Equality and HashCode

		public bool Equals(Design other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.Model.Name, Model.Name);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (Design)) return false;
			return Equals((Design) obj);
		}

		public override int GetHashCode()
		{
			return (Model.Name != null ? Model.Name.GetHashCode() : 0);
		}

		public static bool operator ==(Design left, Design right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Design left, Design right)
		{
			return !Equals(left, right);
		}

		#endregion

		#endregion
	}
}
