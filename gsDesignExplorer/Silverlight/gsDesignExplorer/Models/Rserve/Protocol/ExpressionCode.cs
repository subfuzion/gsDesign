namespace gsDesign.Explorer.Models.Rserve.Protocol
{
	// http://www.rforge.net/Rserve/dev.html
	public enum ExpressionCode : byte
	{
		Null = 0,

		// S4 object
		S4 = 7,

		// generic vector (RList)
		// data: (n*?) SEXP
		Vector = 16,

		// dotted-pair list (RList)
		// data: SEXP head, SEXP values, [SEXP tag]
		List = 17,

		// closure (currently the body of the closure is stored in the content part of the REXP)
		// data: SEXP formals, SEXP body
		Closure = 18,

		// symbol name
		// data: (n) char null-terminated string
		SymbolName = 19,

		// dotted-pair list (w/o tags)
		// data: same as Vector
		ListNoTags = 20,

		// dotted-pair list (w tags)
		// data: SEXP tag, SEXP value, ...
		ListTags = 21,

		// language list (w/o tags)
		// data: same as ListNoTags (LANGSXP)
		LanguageListNoTags = 22,

		// language list (w tags)
		// data: same as ListTags (LANGSXP)
		LanguageListTags = 23,

		// expression vector
		// data: same as Vector (EXPSXP)
		ExpressionVector = 26,

		// string vector
		// data: (?) string,string,...
		StringVector = 27,

		// int[]
		// data: (n*4) int,int,...
		IntegerArray = 32,

		// double[]
		// data: (n*8) double,double,...
		DoubleArray = 33,

		// string[] (currently not used, Vector is used instead)
		StringArray = 34,

		// internal use only! this constant should never appear in a REXP
		BoolArrayInternal = 35,

		// RBool[]
		// data: (n) byte,byte,...
		BoolArray = 36,

		// raw (byte[])
		// data: (1) int n (n) byte,byte,...
		ByteArray = 37,

		// Complex[]
		// data: (n) double(re),double(im),...
		ComplexArray = 38,

		// unknown, no assumptions can be made about the content
		// data: (d) int - SEXP type as defined in R
		Unknown = 48,

		// RFactor, this expression type is internally generated to support RFactor class which is built from IntegerArray
		Factor = 127,

		// used for transport only - has attribute
		HasAttribute = 128,
	}
}