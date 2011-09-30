// ReSharper disable InconsistentNaming
namespace gsDesign.Explorer.Models.Rserve.Protocol
{
	public class REXPFactory
	{
	/** xpression type: NULL */
	public static readonly int XT_NULL = 0;

	/** xpression type: integer */
	public static readonly int XT_INT = 1;

	/** xpression type: double */
	public static readonly int XT_DOUBLE = 2;

	/** xpression type: String */
	public static readonly int XT_STR = 3;

	/** xpression type: language construct (currently content is same as list) */
	public static readonly int XT_LANG = 4;

	/** xpression type: symbol (content is symbol name: String) */
	public static readonly int XT_SYM = 5;

	/** xpression type: RBool */    
	public static readonly int XT_BOOL = 6;

	/** xpression type: S4 object
	@since Rserve 0.5 */
	public static readonly int XT_S4 = 7;

	/** xpression type: generic vector (RList) */
	public static readonly int XT_VECTOR = 16;

	/** xpression type: dotted-pair list (RList) */
	public static readonly int XT_LIST = 17;

	/** xpression type: closure (there is no class for that type (yet?).
	 * currently the body of the closure is stored in the content part of the REXP. Please
	 * note that this may change in the future!) */
	public static readonly int XT_CLOS = 18;

	/** xpression type: symbol name
	@since Rserve 0.5 */
	public static readonly int XT_SYMNAME = 19;

	/** xpression type: dotted-pair list (w/o tags)
	@since Rserve 0.5 */
	public static readonly int XT_LIST_NOTAG = 20;

	/** xpression type: dotted-pair list (w tags)
	@since Rserve 0.5 */
	public static readonly int XT_LIST_TAG = 21;

	/** xpression type: language list (w/o tags)
	@since Rserve 0.5 */
	public static readonly int XT_LANG_NOTAG = 22;

	/** xpression type: language list (w tags)
	@since Rserve 0.5 */
	public static readonly int XT_LANG_TAG = 23;

	/** xpression type: expression vector */
	public static readonly int XT_VECTOR_EXP = 26;

	/** xpression type: string vector */
	public static readonly int XT_VECTOR_STR = 27;

	/** xpression type: int[] */
	public static readonly int XT_ARRAY_INT = 32;

	/** xpression type: double[] */
	public static readonly int XT_ARRAY_DOUBLE = 33;

	/** xpression type: String[] (currently not used, Vector is used instead) */
	public static readonly int XT_ARRAY_STR = 34;

	/** internal use only! this constant should never appear in a REXP */
	public static readonly int XT_ARRAY_BOOL_UA = 35;

	/** xpression type: RBool[] */
	public static readonly int XT_ARRAY_BOOL = 36;

	/** xpression type: raw (byte[])
	@since Rserve 0.4-? */
	public static readonly int XT_RAW = 37;

	/** xpression type: Complex[]
	@since Rserve 0.5 */
	public static readonly int XT_ARRAY_CPLX = 38;

	/** xpression type: unknown;
	 * no assumptions can be made about the content */
	public static readonly int XT_UNKNOWN = 48;


	/** xpression type: RFactor;
	 * this XT is internally generated (ergo is does not come from Rsrv.h)
	 * to support RFactor class which is built from XT_ARRAY_INT */
	public static readonly int XT_FACTOR = 127;
 

	/** used for transport only - has attribute */
	private static readonly int XT_HAS_ATTR = 128;

	   
	}
}
// ReSharper restore InconsistentNaming
