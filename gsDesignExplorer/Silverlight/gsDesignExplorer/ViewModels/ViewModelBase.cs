namespace gsDesign.Explorer.ViewModels
{
	using Subfuzion.Helpers;

	public abstract class ViewModelBase : NotifyPropertyChangedBase
	{
		// TODO Force commands to be requeried (DelegateCommand.Requery())
		// Use reflection to query all DelegateCommand properties,
		// then cache property list so don't have to repeat reflection
		public virtual void Requery()
		{
		}
	}
}