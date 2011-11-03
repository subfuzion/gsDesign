namespace gsDesign.Explorer.ViewModels
{
	using System;
	using System.IO;
	using System.Text.RegularExpressions;
	using System.Windows;
	using Design;
	using RService;
	using Subfuzion.R.Rserve.Protocol;

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
				RaisePropertyChanged("Designs");
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
			var design = CurrentDesign;
			var script = design.DesignScript.Output;

			var rService = new RServiceClient();
			rService.SaveScriptCompleted += new EventHandler<SaveScriptCompletedEventArgs>(rService_SaveScriptCompleted);
			rService.SaveScriptAsync(CurrentDesign.DesignScript.Output);
		}

		private void rService_SaveScriptCompleted(object sender, SaveScriptCompletedEventArgs e)
		{
			var pathname = e.Result;

			var path = Path.GetDirectoryName(pathname);
			var filename = Path.GetFileName(pathname);
			var name = Path.GetFileNameWithoutExtension(pathname);
			var ext = Path.GetExtension(pathname);

			var cmd = string.Format(@"setwd(""{0}"")", path);

			cmd = Escape(cmd);

			var request = Request.Eval(cmd);
			RserveClient.SendRequest(request, OnResponse, OnError, null);
		}

		private void RunDesignCompleted(object parameter = null)
		{
			BeforeRunExecutedVisibility = Visibility.Collapsed;
			AfterRunExecutedVisibility = Visibility.Visible;
		}

		string Escape(string text)
		{
			return text.Replace('\\', '/');
		}

		#endregion Run Design implementation



		#endregion Implementation
	}

	class RserveContext
	{
		public void AddCommand()
		{
			
		}
	}
}
