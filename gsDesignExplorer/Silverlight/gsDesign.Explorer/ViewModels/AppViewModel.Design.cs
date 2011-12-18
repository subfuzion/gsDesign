namespace gsDesign.Explorer.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Text.RegularExpressions;
	using System.Windows;
	using Design;
	//using RService;
	using Subfuzion.Helpers;
	using Subfuzion.R.Rserve;
	using Subfuzion.R.Rserve.Protocol;
	using Test;
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
			var design = CurrentDesign;
			var script = design.DesignScript.Output;

			// FIXME
			//var rService = new RServiceClient();
			//rService.SaveScriptCompleted += rService_SaveScriptCompleted;
			//rService.SaveScriptAsync(CurrentDesign.DesignScript.Output);

			OutputText = "Running " + CurrentDesign.Name;
			var pathname = FileManager.CreateTempFile(script);
			Action<string> success = result => OutputText = result;
			var runScriptCommand = new RserveRunScriptCommand(CurrentDesign.Name, RserveClient, pathname, success);
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
			BeforeRunExecutedVisibility = Visibility.Collapsed;
			AfterRunExecutedVisibility = Visibility.Visible;
		}

		#endregion Run Design implementation



		#endregion Implementation
	}

	class RserveCommandProcessor
	{
		private List<string> _commands = new List<string>(); 

		public void AddCommand(string command)
		{
			_commands.Add(command);
		}

		public void Run()
		{
			
		}
	}

	class RserveRunScriptCommand
	{
		public RserveRunScriptCommand(string name, RserveClient client, string pathName, Action<string> success)
		{
			Name = name;
			RserveClient = client;
			PathName = pathName;
			Success = success;
		}

		public Action<string> Success { get; set; }

		public string Name { get; set; }

		public RserveClient RserveClient { get; set; }

		public string PathName { get; set; }

		public string Directory
		{
			get { return Path.GetDirectoryName(PathName); }
		}

		public string FileName
		{
			get { return Path.GetFileName(PathName); }
		}

		public string FileNameWithoutExtension
		{
			get { return Path.GetFileNameWithoutExtension(PathName); }
		}

		public string LastCommand { get; set; }

		public void Run()
		{
			var commands = new []
			{
				string.Format(@"library(""{0}"")", "gsDesign"),
				Escape(string.Format(@"setwd(""{0}"")", Directory)),
				string.Format(@"source(""{0}"")", FileName),
				string.Format(@"capture.output(print.gsDesign({0}))", Name),
			};

			var command = string.Join(";", commands);

			// var cmd = Escape(string.Format(@"library(""{0}"")", "gsDesign"));

	
			LastCommand = command;
			var request = Request.Eval(command);
			RserveClient.SendRequest(request, OnRunResponse, OnRunError, this);
		}

		string Escape(string text)
		{
			return text.Replace('\\', '/');
		}

		private void OnRunResponse(Response response, object context)
		{
			var result = PrintResponse(response);

			if (Success != null)
			{
				Success(result);
			}

			return;

			if (response.IsOk && LastCommand.StartsWith("library"))
			{
				var output = PrintResponse(response);
				var cmd = Escape(string.Format(@"setwd(""{0}"")", Directory));
				LastCommand = cmd;
				var request = Request.Eval(cmd);
				RserveClient.SendRequest(request, OnRunResponse, OnRunError, this);
			}
			//else if (response.IsOk && LastCommand.StartsWith("setwd"))
			//{
			//    var output = PrintResponse(response);
			//    var cmd = Escape(string.Format(@"png(Filename=""{0}"")", FileNameWithoutExtension + ".png"));
			//    LastCommand = cmd;
			//    var request = Request.Eval(cmd);
			//    RserveClient.SendRequest(request, OnRunResponse, OnRunError, this);
			//}
			else if (response.IsOk && LastCommand.StartsWith("setwd"))
			{
				var output = PrintResponse(response);
				var cmd = Escape(string.Format(@"source(""{0}"")", FileName));
				LastCommand = cmd;
				var request = Request.Eval(cmd);
				RserveClient.SendRequest(request, OnRunResponse, OnRunError, this);
			}
			//else if (response.IsOk && LastCommand.StartsWith("source"))
			//{
			//    var output = PrintResponse(response);
			//    var cmd = "dev.off()";
			//    LastCommand = cmd;
			//    var request = Request.Eval(cmd);
			//    RserveClient.SendRequest(request, OnRunResponse, OnRunError, this);
			//}
			else if (response.IsOk && LastCommand.StartsWith("source"))
			{
				var output = PrintResponse(response);
				var cmd = Escape(string.Format(@"print.gsDesign(""{0}"")", Name));
				LastCommand = cmd;
				var request = Request.Eval(cmd);
				RserveClient.SendRequest(request, OnRunResponse, OnRunError, this);
			}
			else if (response.IsOk && LastCommand.StartsWith("print"))
			{
				var output = PrintResponse(response);
			}
		}

		private void OnRunError(ErrorCode errorCode, object context)
		{
			// Output = string.Format("Error: {0}\nContext: {1}", errorCode.ToString(), context);
		}

		string PrintResponse(Response response)
		{
			var OutputText = string.Empty;
			var Output = new DiagnosticInfo(response).ToString();

			try
			{
				if (response.Payload.PayloadCode == PayloadCode.Rexpression)
				{
					var content = response.Payload.Content.GetUTF8String();

					var rexp = Rexpression.FromBytes(response.Payload.Content);

					if (rexp.IsStringList)
					{
						var list = rexp.ToStringList();

						foreach (var s in list)
						{
							OutputText += string.Format("{0}\n", s);
						}
					}

					if (rexp.IsDoubleList)
					{
						var list = rexp.ToDoubleList();

						foreach (var d in list)
						{
							OutputText += string.Format("{0}\n", d);
						}
					}
				}
			}
			catch (Exception e)
			{
				Output += string.Format("\n\n(TODO) this response is currently unhandled, raising an exception: {0}\n", e);
			}

			Console.WriteLine(OutputText);
			return OutputText;
		}
	}
}
