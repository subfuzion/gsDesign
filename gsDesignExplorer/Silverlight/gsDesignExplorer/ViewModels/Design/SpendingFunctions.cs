namespace gsDesign.Explorer.ViewModels.Design
{
	using Models;
	using Subfuzion.Helpers;

	public class SpendingFunctions : NotifyPropertyChangedBase
	{
		private GSDesign _design;

		public SpendingFunctions(GSDesign design)
		{
			_design = design;


		}

		private GSSpendingFunctions Model
		{
			get { return _design.SpendingFunctions; }
		}
	}
}
