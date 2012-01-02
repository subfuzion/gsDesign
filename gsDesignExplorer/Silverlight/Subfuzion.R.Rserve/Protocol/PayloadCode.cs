namespace Subfuzion.R.Rserve.Protocol
{
	/// <summary>
	/// Used for the transport protocol (QAP1): indicates to rserve the type of content
	/// contained in the request payload. Don't confuse with R expression types.
	/// These correspond to DT_ types in Rsrv.h
	/// </summary>
	/// <remarks>
	/// Only Integer, String, ByteStream, and Rexpression are used in current
	/// version of Rserve.
	/// </remarks>
	public enum PayloadCode : byte
	{
		Empty = 0,
		Integer = 1,
		Character = 2,
		Double = 3,
		String = 4,			// null-terminated
		ByteStream = 5,		// can contain 0
		Rexpression = 10,	// encoded R SEXP (see RexpressionType)
		Array = 11,			// array of objects, first 4 bytes is count (0 is legitimate)
		Large = 64,			// Not a value, but a flag to test; if set, the payload header contains an extra 4 bytes for the size
	}
}
