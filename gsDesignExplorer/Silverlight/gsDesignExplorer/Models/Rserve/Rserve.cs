// ReSharper disable InconsistentNaming
namespace gsDesign.Explorer.Models.Rserve
{
	/*
		Rserve communication is done over any reliable connection-oriented
		protocol (usually TCP/IP or local sockets). After the connection is
		established, the server sends 32 bytes of ID-string defining the
		capabilities of the server. Each attribute of the ID-string is 4 bytes
		long and is meant to be user-readable (i.e. don't use special characters),
		and it's a good idea to make "\r\n\r\n" the last attribute

		the ID string must be of the form:

		[0] "Rsrv" - R-server ID signature
		[4] "0100" - version of the R server
		[8] "QAP1" - protocol used for communication (here Quad Attributes Packets v1)
		[12] any additional attributes follow. \r\n<space> and '-' are ignored.

		optional attributes
		(in any order; it is legitimate to put dummy attributes, like "----" or
		"    " between attributes):

		"R151" - version of R (here 1.5.1)
		"ARpt" - authorization required (here "pt"=plain text, "uc"=unix crypt,
				 "m5"=MD5)
				 connection will be closed if the first packet is not CMD_login.
				 if more AR.. methods are specified, then client is free to
				 use the one he supports (usually the most secure)
		"K***" - key if encoded authentification is challenged (*** is the key)
				 for unix crypt the first two letters of the key are the salt
				 required by the server
	*/
	internal class Rserve
	{
		static readonly int RSRV_VER = 0x000606;

		static readonly int default_Rsrv_port = 6311;

		/* QAP1 transport protocol header structure

		   all int and double entries throughout the transfer are in
		   Intel-endianess format: int=0x12345678 -> char[4]=(0x78,0x56,x34,0x12)
		   functions/macros for converting from native to protocol format 
		   are available below

		   Please note also that all values muse be quad-aligned, i.e. the length
		   must be divisible by 4. This is automatically assured for int/double etc.,
		   but care must be taken when using strings and byte streams.
		 */
		struct phdr
		{
			public int cmd;
			public int len;
			public int dof;
			public int res;
		}

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

		/* macros for handling the first int - split/combine (24-bit version only!) */
		//#define PAR_TYPE(X) ((X) & 255)
		//byte PAR_TYPE()

	}
}
// ReSharper restore InconsistentNaming
