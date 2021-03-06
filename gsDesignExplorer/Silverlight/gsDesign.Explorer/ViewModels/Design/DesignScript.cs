﻿namespace gsDesign.Explorer.ViewModels.Design
{
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using Subfuzion.Helpers;
	using gsDesign.Output;

	public class DesignScript : NotifyPropertyChangedBase
	{
		private readonly DesignScriptGenerator _designScriptGenerator = new DesignScriptGenerator();

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
					NotifyPropertyChanged("Design");
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
					NotifyPropertyChanged("Output");
				}
			}
		}

		#endregion // Output

		private void RemoveHandlers()
		{
			if (Design == null) return;

			Design.PropertyChanged -= OnDesignPropertiesChanged;
			Design.ErrorPowerTiming.PropertyChanged -= OnDesignPropertiesChanged;
			Design.SampleSize.PropertyChanged -= OnDesignPropertiesChanged;
			Design.SpendingFunctions.PropertyChanged -= OnDesignPropertiesChanged;
			Design.SpendingFunctions.LowerSpendingFunction.PropertyChanged -= OnDesignPropertiesChanged;
			Design.SpendingFunctions.UpperSpendingFunction.PropertyChanged -= OnDesignPropertiesChanged;
		}

		private void AddHandlers()
		{
			if (Design == null) return;

			Design.PropertyChanged += OnDesignPropertiesChanged;
			Design.ErrorPowerTiming.PropertyChanged += OnDesignPropertiesChanged;
			Design.SampleSize.PropertyChanged += OnDesignPropertiesChanged;
			Design.SpendingFunctions.PropertyChanged += OnDesignPropertiesChanged;
			Design.SpendingFunctions.LowerSpendingFunction.PropertyChanged += OnDesignPropertiesChanged;
			Design.SpendingFunctions.UpperSpendingFunction.PropertyChanged += OnDesignPropertiesChanged;
		}

		private void OnDesignPropertiesChanged(object sender, PropertyChangedEventArgs e)
		{
			Output = GenerateScript();
		}

		public string GenerateScript()
		{
			if (Design == null) return null;

			return _designScriptGenerator.GenerateScript(Design.Model);
		}
	}
}
