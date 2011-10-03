namespace gsDesign.Explorer.Models.Rserve.Protocol
{
	public enum Command
	{
		None = 0,
		Login = 0x001,
		VoidEval = 0x002,
		Eval = 0x003,
		Shutdown = 0x004,
		OpenFile = 0x010,
		CreateFile = 0x011,
		CloseFile = 0x012,
		ReadFile = 0x013,
		WriteFile = 0x014,
		RemoveFile = 0x015,
		SetSexp = 0x020,
		AssignSexp = 0x021,
		SetBufferSize = 0x081,
		SetEncoding = 0x082,
		DetachSession = 0x030,
		DetachedVoidEval = 0x031,
		AttachSession = 0x032,
		CtrlEval = 0x42,
		CtrlSource = 0x45,
		CtrlShutdown = 0x44,
	}
}
