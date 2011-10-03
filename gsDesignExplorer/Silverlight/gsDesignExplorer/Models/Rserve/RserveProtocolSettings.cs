namespace gsDesign.Explorer.Models.Rserve
{
	using System;
	using System.Text;
	using Helpers;

	public class RserveProtocolSettings : NotifyPropertyChangedBase
	{
		private string _signature;
		private string _version;
		private string _name;
		private bool _isAuthorizationRequired;
		private PasswordEncryption _passwordEncryption;
		private string _passwordEncryptionKey;

		public static RserveProtocolSettings Parse(byte[] bytes)
		{
			if (bytes == null || bytes.Length != 32)
			{
				throw new ArgumentException("Expected 32 bytes for Rserve identification");
			}

			var protocol = new RserveProtocolSettings
			               	{
			               		Signature = Encoding.UTF8.GetString(bytes, 0, 4),
			               		Version = Encoding.UTF8.GetString(bytes, 4, 4),
			               		Name = Encoding.UTF8.GetString(bytes, 8, 4)
			               	};

			for (int i = 12; i < 32; i += 4)
			{
				var attribute = Encoding.UTF8.GetString(bytes, i, 4);

				// don't bother parsing for R version; shouldn't see this
				// attribute any more (versions are too high to be represented
				// now in "R" attribute's remaining 3 bytes)
				// if (attribute.StartsWith("R"))
				// {
				//     _rVersion = attribute;
				// }

				if (attribute.StartsWith("AR"))
				{
					protocol.IsAuthorizationRequired = true;
					if (attribute.EndsWith("pt")) protocol.PasswordEncryption = PasswordEncryption.PlainText;
					else if (attribute.EndsWith("uc")) protocol.PasswordEncryption = PasswordEncryption.UnixCrypt;
				}

				if (attribute.StartsWith("K"))
				{
					protocol.PasswordEncryptionKey = attribute.Substring(1, 3);
				}
			}

			return protocol;
		}

		/// <summary>
		/// The server handshake signature. Must be "Rsrv".
		/// </summary>
		public string Signature
		{
			get { return _signature; }
			set
			{
				if (_signature != value)
				{
					_signature = value;
					RaisePropertyChanged("Signature");
				}
			}
		}

		/// <summary>
		/// The protocol version (currently "0103").
		/// </summary>
		public string Version
		{
			get { return _version; }
			set
			{
				if (_version != value)
				{
					_version = value;
					RaisePropertyChanged("Version");
				}
			}
		}

		/// <summary>
		/// The protocol name (currently "QAP1").
		/// </summary>
		public string Name
		{
			get { return _name; }
			set
			{
				if (_name != value)
				{
					_name = value;
					RaisePropertyChanged("Name");
				}
			}
		}

		public bool IsAuthorizationRequired
		{
			get { return _isAuthorizationRequired; }
			set
			{
				if (_isAuthorizationRequired != value)
				{
					_isAuthorizationRequired = value;
					RaisePropertyChanged("IsAuthorizationRequired");
				}
			}
		}

		public PasswordEncryption PasswordEncryption
		{
			get { return _passwordEncryption; }
			set
			{
				if (_passwordEncryption != value)
				{
					_passwordEncryption = value;
					RaisePropertyChanged("PasswordEncryption");
				}
			}
		}

		/// <summary>
		/// If authentication is challenged (for unix crypt the first
		/// two letters of the key are the salt required by the server)
		/// </summary>
		public string PasswordEncryptionKey
		{
			get { return _passwordEncryptionKey; }
			set
			{
				if (_passwordEncryptionKey != value)
				{
					_passwordEncryptionKey = value;
					RaisePropertyChanged("PasswordEncryptionKey");
				}
			}
		}
	}
}
