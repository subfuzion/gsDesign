namespace gsDesign.Explorer.ViewModels
{
	using System;
	using System.Collections.Generic;

	class ViewModePanelVisibilityManager
	{
		private ViewMode _currentViewMode;
		private Action<string> _propertyChanged;

		public ViewModePanelVisibilityManager(Action<string> propertyChanged)
		{
			_propertyChanged = propertyChanged;
		}

		public Dictionary<ViewMode, string> VisibilityMap { get; set; }

		public ViewMode CurrentViewMode
		{
			get { return _currentViewMode; }

			set
			{
				string property;
				if (VisibilityMap.TryGetValue(value, out property))
				{
					_currentViewMode = value;

					// NOT DONE -- FINISH LATER
					
					if (_propertyChanged != null)
					{
						_propertyChanged(property);
					}
				}
			}
		}
	}
}
