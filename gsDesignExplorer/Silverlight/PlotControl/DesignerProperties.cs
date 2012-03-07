namespace Subfuzion.Silverlight.UI.Charting
{
	using System.Diagnostics.CodeAnalysis;
	using System.Windows;

	/// <summary>
	/// Provides a custom implementation of DesignerProperties.GetIsInDesignMode
	/// to work around an issue.
	/// </summary>
	/// <see cref="http://blogs.msdn.com/b/delay/archive/2009/02/26/designerproperties-getisindesignmode-forrealz-how-to-reliably-detect-silverlight-design-mode-in-blend-and-visual-studio.aspx"/>
	internal static class DesignerProperties
	{
		/// <summary>
		/// Returns whether the control is in design mode (running under Blend
		/// or Visual Studio).
		/// </summary>
		/// <param name="element">The element from which the property value is
		/// read.</param>
		/// <returns>True if in design mode.</returns>
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "element", Justification =
			"Matching declaration of System.ComponentModel.DesignerProperties.GetIsInDesignMode (which has a bug and is not reliable).")]
		public static bool GetIsInDesignMode(DependencyObject element)
		{
			if (!_isInDesignMode.HasValue)
			{
				_isInDesignMode =
					(null == Application.Current) ||
						Application.Current.GetType() == typeof(Application);
			}
			return _isInDesignMode.Value;
		}

		/// <summary>
		/// Stores the computed InDesignMode value.
		/// </summary>
		private static bool? _isInDesignMode;
	}
}