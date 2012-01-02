namespace Subfuzion.R.Rserve.Protocol
{
	/* stat codes; 0-0x3f are reserved for program specific codes - e.g. for R
	   connection they correspond to the stat of Parse command.
	   the following codes are returned by the Rserv itself

	   codes <0 denote Rerror as provided by R_tryEval
	 */
	public enum ErrorCode
	{
		Success = 0,

		/// <summary>
		/// auth.failed or auth.requested but no
		/// login came. in case of authentification
		/// failure due to name/pwd mismatch,
		/// server may send CMD_accessDenied instead
		/// </summary>
		AuthFailed = 0x41,

		/// <summary>
		/// connection closed or broken packet killed it
		/// </summary>
		ConnectionBroken = 0x42,

		/// <summary>
		/// unsupported/invalid command
		/// </summary>
		InvalidCommand = 0x43,

		/// <summary>
		/// some parameters are invalid
		/// </summary>
		InvalidParameter = 0x44,

		/// <summary>
		/// R-error occured, usually followed by connection shutdown
		/// </summary>
		Rerror = 0x45,

		/// <summary>
		/// I/O error
		/// </summary>
		IoError = 0x46,

		/// <summary>
		/// attempt to perform fileRead/Write on closed file
		/// </summary>
		NotOpen = 0x47,

		/// <summary>
		/// this answer is also valid on
		/// CMD_login; otherwise it's sent
		/// if the server deosn;t allow the user
		/// to issue the specified command.
		/// (e.g. some server admins may block
		/// file I/O operations for some users)
		/// </summary>
		AccessDenied = 0x48,

		/// <summary>
		/// unsupported command
		/// </summary>
		UnsupportedCommand = 0x49,

		/// <summary>
		/// unknown command - the difference
		/// between unsupported and unknown is that
		/// unsupported commands are known to the
		/// server but for some reasons (e.g.
		/// platform dependent) it's not supported.
		/// unknown commands are simply not recognized
		/// by the server at all.
		/// </summary>
		UnknownCommand = 0x4a,

		/// <summary>
		/// incoming packet is too big.
		/// currently there is a limit as of the
		/// size of an incoming packet.
		/// </summary>
		DataOverflow = 0x4b,

		/// <summary>
		/// the requested object is too big
		/// to be transported in that way.
		/// If received after CMD_eval then
		/// the evaluation itself was successful.
		/// optional parameter is the size of the object
		/// </summary>
		ObjectTooBig = 0x4c,

		/// <summary>
		/// out of memory. the connection is usually
		/// closed after this error was sent
		/// </summary>
		OutOfMemory = 0x4d,

		/// <summary>
		/// control pipe to the master process is closed or broken
		/// </summary>
		CtrlClosed = 0x4e,

		/// <summary>
		/// session is still busy
		/// </summary>
		SessionBusy = 0x50,

		/// <summary>
		/// unable to detach seesion (cannot determine
		/// peer IP or problems creating a listening
		/// socket for resume)
		/// </summary>
		DetachFailed = 0x51,
	}
}
