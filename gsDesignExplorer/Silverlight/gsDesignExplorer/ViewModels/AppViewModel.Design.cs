namespace gsDesign.Explorer.ViewModels
{
	using System.Text.RegularExpressions;
	using Models;

	public partial class AppViewModel
	{
		#region Designs property

		private DesignCollection _designs;

		public DesignCollection Designs
		{
			get { return _designs ?? (_designs = new DesignCollection()); }

			set
			{
				if (_designs != value)
				{
					_designs = value;
					RaisePropertyChanged("Designs");
				}
			}
		}

		#endregion // Designs

		#region CurrentDesign property

		private Design.Design _currentDesign;

		public Design.Design CurrentDesign
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

		public void AddDesign(Design.Design design)
		{
			if (Designs.Contains(design) == false)
			{
				Designs.Add(design);
				RaisePropertyChanged("Designs");
			}
		}

		public Design.Design CreateDesign(string name = null)
		{
			var design = new Design.Design(IsValidDesignName) { Name = name ?? NewDesignDefaultName };
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

		public Design.Design OpenDesign(string pathName)
		{
			// TODO
			return null;
		}

		public void SaveDesign(Design.Design design, string pathName)
		{
			// TODO
		}

		public void CloseDesign(Design.Design design)
		{
			// TODO
		}
	}
}
