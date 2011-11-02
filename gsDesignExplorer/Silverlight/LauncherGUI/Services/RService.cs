namespace gsDesign.LauncherGUI.Services
{
	using System;
	using System.IO;
	using System.ServiceModel.Web;
	using System.Text;

	public class RService : IRService, IClientAccessPolicy
	{
		#region IRService Members

		public void DoWork()
		{
		}

		#endregion

		#region Implementation of IClientAccessPolicy

		public Stream GetClientAccessPolicy()
		{
			const string result =
@"<?xml version=""1.0"" encoding=""utf-8""?>
<access-policy>
	<cross-domain-access>
		<policy>
			<allow-from http-request-headers=""*"">
				<domain uri=""*""/>
			</allow-from>
			<grant-to>
				<resource path=""/"" include-subpaths=""true""/>
			</grant-to>
		</policy>
	</cross-domain-access>
</access-policy>";

			if (WebOperationContext.Current != null)
			{
				WebOperationContext.Current.OutgoingResponse.ContentType = "application/xml";
			}

			return new MemoryStream(Encoding.UTF8.GetBytes(result));
		}

		#endregion

		private static readonly string UserFileDirectoryName = "User";
		private static readonly string RExtension = ".R";

		public string GetUserDirectory()
		{
			return UserFilePath;
		}

		public string SaveScript(string script)
		{
			var filename = Guid.NewGuid() + ".R";
			var pathname = Path.Combine(UserFilePath, filename);

			using (var w = new StreamWriter(pathname))
			{
				w.Write(script ?? string.Empty);
			}

			return pathname;
		}

		private string UserFilePath
		{
			get { return EnsurePath(UserFileDirectoryName); }
		}

		private string EnsurePath(string path)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			return Path.GetFullPath(path);
		}
	}
}
