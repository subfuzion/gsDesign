namespace gsDesign.Launcher.Models
{
	public class AppModel
	{
		private Launcher _launcher;

		public Launcher Launcher
		{
			get { return _launcher ?? (_launcher = new Launcher()); }
		}
	}
}