using System.ServiceModel;

namespace gsDesign.LauncherGUI.Services
{
	[ServiceContract]
	public interface IRService
	{
		[OperationContract]
		void DoWork();
	}
}
