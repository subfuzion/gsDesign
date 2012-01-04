namespace gsDesign.Explorer.Rserve
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using Subfuzion.Helpers;
	using Subfuzion.R.Rserve;
	using Subfuzion.R.Rserve.Protocol;
	using ViewModels.Test;

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

	enum CommandStep
	{
		TryEvalSource,
		CaptureResult,
		Plot,
	}

	class CommandContext
	{
		public EvalCommand EvalCommand { get; set; }
		public CommandStep CommandStep { get; set; }
	}

	public class EvalCommand
	{
		public EvalCommand(string name, RserveConnection client, string pathName, Action<string> output, Action<string> plot)
		{
			Name = name;
			RserveConnection = client;
			PathName = pathName;
			Output = output;
			Plot = plot;
		}

		public Action<string> Output { get; set; }

		public Action<string> Plot { get; set; }

		public Action<string> Error { get; set; }

		public string Name { get; set; }

		public RserveConnection RserveConnection { get; set; }

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

		public string PlotName
		{
			get { return FileNameWithoutExtension + ".png"; }
		}

		public string LastCommand { get; set; }

		public void Run()
		{
			var commands = new []
			{
				string.Format(@"library(""{0}"")", "gsDesign"),
				Escape(string.Format(@"setwd(""{0}"")", Directory)),
				string.Format("library(png)"),
				string.Format("png(filename=\"{0}\")", PlotName),
				string.Format(@"try(source(""{0}""), silent=TRUE)", FileName),
			};

			var command = string.Join(";", commands);
			LastCommand = command;
			var request = Request.Eval(command);

			var context = new CommandContext
			{
				EvalCommand = this,
				CommandStep = CommandStep.TryEvalSource,
			};

			RserveConnection.SendRequest(request, OnRunResponse, OnRunError, context);
		}

		string Escape(string text)
		{
			return text.Replace('\\', '/');
		}

		private void OnRunResponse(Response response, object context)
		{
			var commandContext = context as CommandContext;

			if (commandContext != null)
			{
				if (commandContext.CommandStep == CommandStep.TryEvalSource)
				{
					if (response.Payload.PayloadCode == PayloadCode.Rexpression)
					{
						var content = response.Payload.Content.GetUTF8String();

						var rexp = ProtocolParser.ParseRexpression(response.Payload.Content);

						if (rexp.HasAttribute)
						{
							var tags = rexp.HasAttribute ? rexp.Attribute.ToListTags() : null;
							var tagstr = rexp.Attribute.ToFormattedString();

							if (tags.Any(tag => tag.Name.ToString() == "try-error"))
							{
								var output = string.Format("gsDesign was unable to execute the script. R reported the following error:\n\n{0}",
									rexp.ToFormattedString());
								SendOutput(output);
								return;
							}
						}
					}
					else if (response.Payload.PayloadCode == PayloadCode.Empty)
					{
						SendOutput("Failed: received an empty response");
						return;
					}

					commandContext.CommandStep = CommandStep.CaptureResult;

					var commands = new[]
					{
						"dev.off()",
						string.Format(@"capture.output(print.gsDesign({0}))", Name),
					};

					var command = string.Join(";", commands);
					LastCommand = command;

					var request = Request.Eval(command);

					RserveConnection.SendRequest(request, OnRunResponse, OnRunError, context);
				}
				else if (commandContext.CommandStep == CommandStep.CaptureResult)
				{
					var result = PrintResponse(response);
					SendOutput(result);

					if (Plot != null)
					{
						var pathname = Escape(Path.Combine(Directory, PlotName));
						Plot(pathname);
					}


					//commandContext.CommandStep = CommandStep.Plot;

					//// var plotCommand = string.Format("plot({0}, plottype=1, base=TRUE, col=c(\"black\", \"black\"), lwd=c(1, 1), lty=c(\"solid\", \"solid\"), dgt=c(2, 2))", Name);
					//var plotCommand = string.Format("plot({0})", Name);

					//var commands = new[]
					//{
					//    string.Format("library(png)"),
					//    string.Format("png(filename=\"{0}\")", PlotName),
					//    plotCommand,
					//    "dev.off()",
					//};

					//var command = string.Join(";", commands);
					//LastCommand = command;

					//var request = Request.Eval(command);

					//RserveConnection.SendRequest(request, OnRunResponse, OnRunError, context);

				}
				else if (commandContext.CommandStep == CommandStep.Plot)
				{
					var result = PrintResponse(response);

					if (Plot != null)
					{
						Plot(PlotName);
					}
				}
			}
		}

		private void OnRunError(ErrorCode errorCode, object context)
		{
			SendOutput(string.Format("There was a problem executing the last command\nError code: {0}\nContext: {1}", errorCode.ToString(), context));
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

					var rexp = ProtocolParser.ParseRexpression(response.Payload.Content);

					if (rexp.HasAttribute)
					{
						var tags = rexp.HasAttribute ? rexp.Attribute.ToListTags() : null;
						var tagstr = rexp.Attribute.ToFormattedString();
					}

					OutputText += rexp.ToFormattedString();
				}
			}
			catch (Exception e)
			{
				Output += string.Format("\n\n(TODO) this response is currently unhandled, raising an exception: {0}\n", e);
			}

			Console.WriteLine(OutputText);
			return OutputText;
		}

		private void SendOutput(string output)
		{
			if (Output != null)
			{
				Output(output);
			}
		}
	}
}