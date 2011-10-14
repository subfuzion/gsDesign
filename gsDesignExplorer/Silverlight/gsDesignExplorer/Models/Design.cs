namespace gsDesign.Explorer.Models
{
	using System;
	using Subfuzion.Helpers;

	public class Design : NotifyPropertyChangedBase
	{
		public Design(Func<string, bool> nameValidator)
		{
			NameValidator = nameValidator;
		}

		#region Name property

		private string _name;

		public string Name
		{
			get { return _name; }

			set
			{
				if (_name != value)
				{
					// test here instead of above because we do want
					// to fire the property changed event to force the
					// the UI to refresh regardless
					if (NameValidator == null || (NameValidator != null && NameValidator(value)))
					{
						_name = value;
					}
					RaisePropertyChanged("Name");
				}
			}
		}

		public Func<string, bool> NameValidator { get; set; }

		#endregion // Name

		#region Description property

		private string _description;

		public string Description
		{
			get { return _description ?? string.Format("{0} description...", Name ?? "Design"); }

			set
			{
				if (_description != value)
				{
					_description = value;
					RaisePropertyChanged("Description");
				}
			}
		}

		#endregion // Description

		#region Ept property

		private Ept _ept;

		public Ept Ept
		{
			get { return _ept ?? (_ept = new Ept()); }

			set
			{
				if (_ept != value)
				{
					_ept = value;
					RaisePropertyChanged("Ept");
				}
			}
		}

		#endregion // Ept

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
					RaisePropertyChanged("IsModified");
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
			return Equals(other._name, _name);
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
			return (_name != null ? _name.GetHashCode() : 0);
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
