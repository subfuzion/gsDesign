namespace gsDesign.Explorer.ViewModels
{
	using System;
	using Subfuzion.Helpers;
	using Subfuzion.R.Rserve;
	using Subfuzion.R.Rserve.Protocol;
	using Test;

	public partial class AppViewModel
	{
		public RserveConnection RserveClient { get; private set; }

		private string _input;
		public string Input
		{
			get { return _input; }
			set
			{
				if (_input != value)
				{
					_input = value;
					NotifyPropertyChanged("Input");
					NotifyPropertyChanged("IsRunEnabled");
				}
			}
		}

		private string _output;
		public string Output
		{
			get { return _output; }
			set
			{
				if (_output != value)
				{
					_output = value;
					NotifyPropertyChanged("Output");
				}
			}
		}

		#region NewCommand

		/// <summary>
		/// This is only used by the Test toolbar
		/// </summary>
		public DelegateCommand NewCommand { get; private set; }

		private void New(object parameter = null)
		{
			Input = string.Empty;
		}

		#endregion

		#region RunCommand

		/// <summary>
		/// This is only used by the Test toolbar
		/// </summary>
		public DelegateCommand RunCommand { get; private set; }

		private void Run(object parameter = null)
		{
			var request = Request.Eval(Input);
			RserveClient.SendRequest(request, OnResponse, OnError, null);
		}

		private bool CanRun(object parameter = null)
		{
			return !string.IsNullOrWhiteSpace(Input);
		}

		public bool IsRunEnabled
		{
			get
			{
				RunCommand.Requery();
				return RunCommand.IsEnabled;
			}
		}

		#endregion

		private void OnResponse(Response response, object context)
		{
			OutputText = string.Empty;
			Output = new DiagnosticInfo(response).ToString();

			try
			{
				if (response.Payload.PayloadCode == PayloadCode.Rexpression)
				{
					var rexp = ProtocolParser.ParseRexpression(response.Payload.Content);

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
		}

		private void OnError(ErrorCode errorCode, object context)
		{
			Output = string.Format("Error: {0}\nContext: {1}", errorCode.ToString(), context);
		}
	}
}
