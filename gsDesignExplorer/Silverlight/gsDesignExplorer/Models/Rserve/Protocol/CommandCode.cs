namespace gsDesign.Explorer.Models.Rserve.Protocol
{
	public enum CommandCode
	{
	//	============================	//	========================			========================
	//	Command							//	DataTransportCode enum				Return DataTransportCode
	//	============================	//	========================			========================
		None = 0,						//	

		Login = 0x001,					//	String ("name\npassword")			-
		VoidEval = 0x002,				//	String								-
		Eval = 0x003,					//	String								Sexp
		Shutdown = 0x004,				//	[String ("adminpassword")]			-
		
		// File I/O
		OpenFile = 0x010,				//	String ("filename")
		CreateFile = 0x011,				//	String ("filename")
		CloseFile = 0x012,				//	-
		ReadFile = 0x013,				//	[Integer (byte count)]				ByteStream
		WriteFile = 0x014,				//	ByteStream							
		RemoveFile = 0x015,				//	String ("filename")

		// Object manipulation
		SetSexp = 0x020,				//	String ("name"), Sexp
		AssignSexp = 0x021,				//	String ("name"), Sexp

		// Session management
		DetachSession = 0x030,			//	
		DetachedVoidEval = 0x031,		//	
		AttachSession = 0x032,			//	

		// Control commands
		CtrlEval = 0x42,				//	String
		CtrlSource = 0x45,				//	String
		CtrlShutdown = 0x44,			//	-

		// Internal commands
		SetBufferSize = 0x081,			//	Integer (send buffer size)
		SetEncoding = 0x082,			//	String ("native|utf8|latin1")

		// Special commands
		SerializedEval = 0xF5,			// (raw serialized data, no header)
		SerializedAssign = 0xF6,		// (serialized list with [[1]]=name, [[2]]=value)
		SerializedExpressionEval = 0xF7,// (like serEval with one additional evaluation round)
	}
}
