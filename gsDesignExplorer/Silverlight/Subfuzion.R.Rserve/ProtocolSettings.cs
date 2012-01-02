namespace Subfuzion.R.Rserve
{
	using Helpers;

	public class ProtocolSettings : NotifyPropertyChangedBase
	{
		private bool _isAuthorizationRequired;
		private string _name;
		private PasswordEncryption _passwordEncryption;
		private string _passwordEncryptionKey;
		private string _signature;
		private string _version;

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
					NotifyPropertyChanged("Signature");
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
					NotifyPropertyChanged("Version");
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
					NotifyPropertyChanged("Name");
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
					NotifyPropertyChanged("IsAuthorizationRequired");
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
					NotifyPropertyChanged("PasswordEncryption");
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
					NotifyPropertyChanged("PasswordEncryptionKey");
				}
			}
		}
	}
}