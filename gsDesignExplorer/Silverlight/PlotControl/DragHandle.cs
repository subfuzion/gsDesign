namespace Subfuzion.Silverlight.UI.Charting
{
	using System.Windows;
	using System.Windows.Controls;

	[TemplatePart(Name = "PART_DragHandle", Type = typeof(Canvas))]
	public class DragHandle : Control
	{
		public DragHandle()
		{
			DefaultStyleKey = typeof(DragHandle);
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Width = 30;
			Height = 30;

			if (DesignerProperties.GetIsInDesignMode(this)) return;


		}
	}
}
