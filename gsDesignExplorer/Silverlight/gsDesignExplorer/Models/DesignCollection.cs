namespace gsDesign.Explorer.Models
{
	using System.Collections.ObjectModel;
	using System.Linq;

	public class DesignCollection : ObservableCollection<Design>
	{
		public bool Contains(string name)
		{
			return (Items.Where(item => item.Name == name)).Count() > 0;
		}
	}
}
