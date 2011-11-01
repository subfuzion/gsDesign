using System.ServiceModel;

namespace gsDesign.LauncherGUI.Services
{
	using System.IO;
	using System.ServiceModel.Web;

	[ServiceContract]
	public interface IClientAccessPolicy
	{
		[OperationContract, WebGet(UriTemplate = "/clientaccesspolicy.xml")]
		Stream GetClientAccessPolicy();
	}
}
