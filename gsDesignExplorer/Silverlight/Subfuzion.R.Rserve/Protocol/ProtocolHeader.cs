namespace Subfuzion.R.Rserve.Protocol
{
	/* QAP1 transport protocol header structure

	   all int and double entries throughout the transfer are in
	   Intel-endianess format: int=0x12345678 -> char[4]=(0x78,0x56,x34,0x12)
	   functions/macros for converting from native to protocol format 
	   are available below

	   Please note also that all values muse be quad-aligned, i.e. the length
	   must be divisible by 4. This is automatically assured for int/double etc.,
	   but care must be taken when using strings and byte streams.
	 */

	/* each entry in the data section (aka parameter list) is preceded by 4 bytes:
	   1 byte : parameter type
	   3 bytes: length
	   parameter list may be terminated by 0/0/0/0 but doesn't have to since "len"
	   field specifies the packet length sufficiently (hint: best method for parsing is
	   to allocate len+4 bytes, set the last 4 bytes to 0 and trverse list of parameters
	   until (int)0 occurs
   
	   since 0102:
	   if the 7-th bit (0x40) in parameter type is set then the length is encoded
	   in 7 bytes enlarging the header by 4 bytes. 
	 */
	public abstract class ProtocolHeader
	{
		public static RequestHeader CreateRequestHeader(CommandCode commandCode, int payloadSize, int payloadOffset = 0, int payloadSize2 = 0)
		{
			return new RequestHeader(commandCode, payloadSize, payloadOffset, payloadSize2);
		}

		public static ResponseHeader CreateResponseHeader(byte[] bytes)
		{
			return ProtocolParser.ParseResponseHeader(bytes);
		}

		public static readonly int HeaderSize = 16;

		public CommandCode CommandCode { get; set; }

		public int PayloadSize { get; set; }

		public int PayloadOffset { get; set; }

		public int PayloadSize2 { get; set; }

	}
}
