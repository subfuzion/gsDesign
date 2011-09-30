namespace gsDesign.Explorer.Models.Rserve
{
	using System;

	public class RserveException : Exception
	{
		public RserveException()
		{
		}

		public RserveException(string message) : base(message)
		{
		}

		public RserveException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
