namespace gsDesign.Explorer.ViewModels
{
	using System;
	using System.Text.RegularExpressions;
	using System.Windows;
	using Design;
	using Rserve;
	using Utilities;

	public partial class AppViewModel
	{
		#region Designs collection property

		private DesignCollection _designs;

		public DesignCollection Designs
		{
			get { return _designs ?? (_designs = new DesignCollection()); }

			set
			{
				if (_designs != value)
				{
					_designs = value;
					NotifyPropertyChanged("Designs");
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
					NotifyPropertyChanged("CurrentDesign");

					if (Designs.Contains(value) == false)
					{
						AddDesign(value);
					}
				}
			}
		}

		#endregion // CurrentDesign

		#region Implementation

		private string NewDesignDefaultName
		{
			get { return "Design" + (Designs.Count + 1); }
		}

		private void AddDesign(Design.Design design)
		{
			if (Designs.Contains(design) == false)
			{
				Designs.Add(design);
				NotifyPropertyChanged("Designs");
			}
		}

		private Design.Design CreateDesign(string name = null)
		{
			var design = new Design.Design(IsValidDesignName) {Name = name ?? NewDesignDefaultName};
			CurrentDesign = design;
			return design;
		}

		private bool IsValidDesignName(string name)
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

		private Design.Design OpenDesign(string pathName)
		{
			// TODO
			return null;
		}

		private void SaveDesign(Design.Design design, string pathName)
		{
			// TODO
		}

		private void CloseDesign(Design.Design design)
		{
			// TODO
		}

		#region Run Design implementation

		private void RunDesign()
		{
			Design.Design design = CurrentDesign;
			string script = design.DesignScript.Output;

			// FIXME
			//var rService = new RServiceClient();
			//rService.SaveScriptCompleted += rService_SaveScriptCompleted;
			//rService.SaveScriptAsync(CurrentDesign.DesignScript.Output);

			OutputText = "Running " + CurrentDesign.Name;
			string pathname = FileManager.CreateTempFile(script);
			Action<string> output = result => OutputText = result;
			Action<string> plot = result => OutputPlot = result;
			var runScriptCommand = new EvalCommand(CurrentDesign.Name, RserveClient, pathname, output, plot);
			runScriptCommand.Run();
		}

		//private void rService_SaveScriptCompleted(object sender, SaveScriptCompletedEventArgs e)
		//{
		//    OutputText = "Running " + CurrentDesign.Name;
		//    var pathname = e.Result;
		//    Action<string> success = (result) => OutputText = result;
		//    var runScriptCommand = new RserveRunScriptCommand(CurrentDesign.Name, RserveClient, pathname, success);
		//    runScriptCommand.Run();
		//}

		private void RunDesignCompleted(object parameter = null)
		{
		}

		#endregion Run Design implementation

		#endregion Implementation
	}
}