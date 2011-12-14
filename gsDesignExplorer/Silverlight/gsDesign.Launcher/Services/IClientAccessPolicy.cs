namespace gsDesign.Explorer.Services
{
	using System.IO;
	using System.ServiceModel;
	using System.ServiceModel.Web;

	[ServiceContract]
	public interface IClientAccessPolicy
	{
		[OperationContract, WebGet(UriTemplate = "/clientaccesspolicy.xml")]
		Stream GetClientAccessPolicy();
	}
}