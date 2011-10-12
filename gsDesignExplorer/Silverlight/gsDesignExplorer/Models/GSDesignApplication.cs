namespace gsDesign.Explorer.Models
{
	using System.ComponentModel;
	using System.Text.RegularExpressions;
	using Subfuzion.Helpers;

	public class GSDesignApplication : NotifyPropertyChangedBase
	{
		public GSDesignApplication()
		{
			CreateDesign();
		}

		public readonly DesignCollection Designs = new DesignCollection();

		#region CurrentDesign property

		private Design _currentDesign;

		public Design CurrentDesign
		{
			get { return _currentDesign; }

			set
			{
				if (_currentDesign != value)
				{
					_currentDesign = value;
					RaisePropertyChanged("CurrentDesign");

					if (Designs.Contains(value) == false)
					{
						AddDesign(value);
					}
				}
			}
		}

		#endregion // CurrentDesign

		public string NewDesignDefaultName
		{
			get { return "Design" + (Designs.Count + 1); }
		}

		public void AddDesign(Design design)
		{
			if (Designs.Contains(design) == false)
			{
				Designs.Add(design);
				RaisePropertyChanged("Designs");
			}
		}

		public Design CreateDesign(string name = null)
		{
			var design = new Design(IsValidDesignName) { Name = name ?? NewDesignDefaultName };
			CurrentDesign = design;
			return design;
		}

		public bool IsValidDesignName(string name)
		{
			if (!Regex.IsMatch(name, "^[^ ]+$"))
			{
				return false;
			}

			if (Designs.Contains(name))
			{
				return false;
			}

			return true;
		}

		public Design OpenDesign(string pathName)
		{
			// TODO
			return null;
		}
		
		public void SaveDesign(Design design, string pathName)
		{
			// TODO
		}

		public void CloseDesign(Design design)
		{
			// TODO
		}
	}
}
