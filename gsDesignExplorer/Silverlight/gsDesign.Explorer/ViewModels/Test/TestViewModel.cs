namespace gsDesign.Explorer.ViewModels.Test
{
	using System;
	using System.Windows;
	using Subfuzion.Helpers;
	using Subfuzion.R.Rserve;
	using Subfuzion.R.Rserve.Protocol;

	public class TestViewModel : ViewModelBase
	{
		private string _input;
		private string _output;

		public TestViewModel()
		{
			NewCommand = new DelegateCommand {ExecuteAction = New};
			RunCommand = new DelegateCommand {ExecuteAction = Run, CanExecuteFunc = CanRun};

			AppViewModel.PropertyChanged += (sender, propertyChangedEventArgs) =>
			                                {
			                                	if (propertyChangedEventArgs.Equals("RserveClient"))
			                                		NotifyPropertyChanged("RserveClient");
			                                };
		}

		public AppViewModel AppViewModel
		{
			get { return App.AppViewModel; }
		}

		public override void Requery()
		{
			RunCommand.Requery();
		}

		public RserveConnection RserveClient
		{
			get { return App.AppViewModel.RserveClient; }
		}

		public string Input
		{
			get { return _input; }
			set
			{
				if (_input != value)
				{
					_input = value;
					NotifyPropertyChanged("Input");
					RunCommand.Requery();
				}
			}
		}

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

		public DelegateCommand NewCommand { get; private set; }

		private void New(object parameter = null)
		{
			Input = string.Empty;
		}

		public DelegateCommand RunCommand { get; private set; }

		private void Run(object parameter = null)
		{
			var request = Request.Eval(Input);
			AppViewModel.RserveClient.SendRequest(request, OnResponse, OnError, null);
		}

		private bool CanRun(object parameter = null)
		{
			return !string.IsNullOrWhiteSpace(Input);
		}

		private void OnResponse(Response response, object context)
		{
			Deployment.Current.Dispatcher.BeginInvoke(() =>
			{
				AppViewModel.OutputText = string.Empty;
				Output = new DiagnosticInfo(response).ToString();

				try
				{
					if (response.Payload.PayloadCode == PayloadCode.Rexpression)
					{
						var rexp = ProtocolParser.ParseRexpression(response.Payload.Content);

						if (rexp.HasAttribute)
						{
							var tags = rexp.HasAttribute ? rexp.Attribute.ToListTags() : null;
							var tagstr = rexp.Attribute.ToFormattedString();
						}

						AppViewModel.OutputText += rexp.ToFormattedString();
					}
				}
				catch (Exception e)
				{
					Output += string.Format("\n\n(TODO) this response is currently unhandled, raising an exception: {0}\n", e);
				}
			});
		}

		private void OnError(ErrorCode errorCode, object context)
		{
			Output = string.Format("Error: {0}\nContext: {1}", errorCode.ToString(), context);
		}
	}
}