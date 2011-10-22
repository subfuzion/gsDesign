namespace gsDesign.Explorer.Models.Design.SpendingFunctions
{
	using ParameterFree;

	public class LowerSpendingParameters
	{
		#region ParameterFreeSpendingFunction property

		private ParameterFreeSpendingFunction _parameterFreeSpendingFunction;

		public ParameterFreeSpendingFunction ParameterFreeSpendingFunction
		{
			get { return _parameterFreeSpendingFunction
				?? (_parameterFreeSpendingFunction = new ParameterFreeSpendingFunction
				                                     {
				                                     	LanDeMetsApproximation = LanDeMetsApproximation.OBrienFleming
				                                     });}

			set
			{
				_parameterFreeSpendingFunction = value;
			}
		}

		#endregion // ParameterFreeSpendingFunction

	}
}