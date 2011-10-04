namespace Subfuzion.R.Rserve.Protocol
{
	public enum ErrorCode
	{
		None = 0,
		AuthFailed = 0x41,
		ConnectionBroken = 0x42,
		InvalidCommand = 0x43,
		InvalidParameter = 0x44,
		Rerror = 0x45,
		IoError = 0x46,
		NotOpen = 0x47,
		AccessDenied = 0x48,
		UnsupportedCommand = 0x49,
		UnknownCommand = 0x4a,
		DataOverflow = 0x4b,
		ObjectTooBig = 0x4c,
		OutOfMemory = 0x4d,
		CtrlClosed = 0x4e,
		SessionBusy = 0x50,
		DetachFailed = 0x51,
	}
}
