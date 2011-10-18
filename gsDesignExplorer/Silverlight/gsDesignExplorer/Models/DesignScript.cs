namespace gsDesign.Explorer.Models
{
	using System;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.IO;
	using Subfuzion.Helpers;
	using ViewModels.Design;

	public class DesignScript : NotifyPropertyChangedBase
	{
		#region Design property

		private Design _design;

		[Display(Name = "Design",
			Description = "")]
		public Design Design
		{
			get { return _design; }

			set
			{
				if (_design != value)
				{
					RemoveHandlers();
					_design = value;
					AddHandlers();
					RaisePropertyChanged("Design");
					GenerateScript();
				}
			}
		}

		#endregion // Design

		#region Output property

		private string _output;

		[Display(Name = "Output",
			Description = "")]
		public string Output
		{
			get { return _output; }

			set
			{
				if (_output != value)
				{
					_output = value;
					RaisePropertyChanged("Output");
				}
			}
		}

		#endregion // Output

		#region GeneratedTimestamp property

		private DateTime _generatedTimestamp;

		[Display(Name = "GeneratedTimestamp",
			Description = "")]
		public DateTime GeneratedTimestamp
		{
			get { return _generatedTimestamp; }

			set
			{
				if (_generatedTimestamp != value)
				{
					_generatedTimestamp = value;
					RaisePropertyChanged("GeneratedTimestamp");
				}
			}
		}

		#endregion // GeneratedTimestamp



		private void RemoveHandlers()
		{
			if (Design == null) return;

			Design.PropertyChanged -= OnDesignPropertiesChanged;
			Design.Ept.PropertyChanged -= OnDesignPropertiesChanged;
		}

		private void AddHandlers()
		{
			if (Design == null) return;

			Design.PropertyChanged += OnDesignPropertiesChanged;
			Design.Ept.PropertyChanged += OnDesignPropertiesChanged;
		}

		private void OnDesignPropertiesChanged(object sender, PropertyChangedEventArgs e)
		{
			Output = GenerateScript();
		}

		public string GenerateScript()
		{
			if (Design == null) return null;

			GeneratedTimestamp = DateTime.Now;

			var writer = new StringWriter();

			AppendHeader(writer);

			return writer.ToString();
		}

		private void AppendHeader(TextWriter writer)
		{
			AppendComment(writer, "This R script was created via an export of a group sequential design");
			AppendComment(writer, "developed in gsDesign Explorer version 2.0 on {0}", GeneratedTimestamp.ToString());
			writer.WriteLine();
			writer.WriteLine("###");
			AppendComment(writer, "Design      : {0}", Design.Name);
			AppendComment(writer, "Description : {0}", Design.Description);
			writer.WriteLine("###");
			writer.WriteLine();
		}

		private void AppendComment(TextWriter writer, string comment, params string[] args)
		{
			writer.WriteLine("# {0}", string.Format(comment, args));
		}


	}
}
