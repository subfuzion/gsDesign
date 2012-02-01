namespace gsDesign.Design.SpendingFunctions
{
	using OneParameter;
	using ParameterFree;
	using PiecewiseLinear;
	using ThreeParameter;
	using TwoParameter;

	/// <summary>
	/// Encapsulates a parameterized spending function.
	/// One instance of this class is the LowerSpendingFunction property of the SpendingFunctionParameters
	/// property of DesignParameters; the other is the UpperSpendingFunction property.
	/// </summary>
	public class SpendingFunction
	{
		public SpendingFunction(DesignParameters designParameters, SpendingFunctionBounds bounds)
		{
			DesignParameters = designParameters;
			SpendingFunctionBounds = bounds;
		}

		public DesignParameters DesignParameters { get; private set; }

		public SpendingFunctionBounds SpendingFunctionBounds { get; private set; }

		#region Spending function parameters

		#region SpendingFunctionParameterCategory property

		private SpendingFunctionParameterCategory _spendingFunctionParameterCategory =
			SpendingFunctionParameterCategory.ParameterFree;

		public SpendingFunctionParameterCategory SpendingFunctionParameterCategory
		{
			get { return _spendingFunctionParameterCategory; }

			set { _spendingFunctionParameterCategory = value; }
		}

		#endregion // SpendingFunctionParameterCategory

		#region ParameterFreeSpendingFunction property

		private ParameterFreeSpendingFunction _parameterFreeSpendingFunction;

		public ParameterFreeSpendingFunction ParameterFreeSpendingFunction
		{
			get
			{
				return _parameterFreeSpendingFunction
					?? (_parameterFreeSpendingFunction = new ParameterFreeSpendingFunction(DesignParameters, SpendingFunctionBounds)
					{
						LanDeMetsApproximation = LanDeMetsApproximation.OBrienFleming
					});
			}

			set { _parameterFreeSpendingFunction = value; }
		}

		#endregion // ParameterFreeSpendingFunction

		#region OneParameterSpendingFunction property

		private OneParameterSpendingFunction _oneParameterSpendingFunction;

		public OneParameterSpendingFunction OneParameterSpendingFunction
		{
			get
			{
				return _oneParameterSpendingFunction
					?? (_oneParameterSpendingFunction = new OneParameterSpendingFunction(DesignParameters, SpendingFunctionBounds)
					{
						OneParameterFamily = OneParameterFamily.HwangShihDeCani,
					});
			}

			set { _oneParameterSpendingFunction = value; }
		}

		#endregion // OneParameterSpendingFunction

		#region TwoParameterSpendingFunction property

		private TwoParameterSpendingFunction _twoParameterSpendingFunction;

		public TwoParameterSpendingFunction TwoParameterSpendingFunction
		{
			get
			{
				return _twoParameterSpendingFunction
					?? (_twoParameterSpendingFunction = new TwoParameterSpendingFunction(DesignParameters, SpendingFunctionBounds)
					{
						//TwoParameterFamily = TwoParameterFamily.HwangShihDeCani,
					});
			}

			set { _twoParameterSpendingFunction = value; }
		}

		#endregion // TwoParameterSpendingFunction

		#region ThreeParameterSpendingFunction property

		private ThreeParameterSpendingFunction _threeParameterSpendingFunction;

		public ThreeParameterSpendingFunction ThreeParameterSpendingFunction
		{
			get
			{
				return _threeParameterSpendingFunction
					?? (_threeParameterSpendingFunction = new ThreeParameterSpendingFunction(DesignParameters, SpendingFunctionBounds)
					{
						// ThreeParameterFamily = ThreeParameterFamily.HwangShihDeCani,
					});
			}

			set { _threeParameterSpendingFunction = value; }
		}

		#endregion // ThreeParameterSpendingFunction

		#region PiecewiseLinearSpendingFunction property

		private PiecewiseLinearSpendingFunction _piecewiseLinearSpendingFunction;

		public PiecewiseLinearSpendingFunction PiecewiseLinearSpendingFunction
		{
			get
			{
				return _piecewiseLinearSpendingFunction
					?? (_piecewiseLinearSpendingFunction = new PiecewiseLinearSpendingFunction(DesignParameters, SpendingFunctionBounds)
					{
						// PiecewiseLinearFamily = PiecewiseLinearFamily.HwangShihDeCani,
					});
			}

			set { _piecewiseLinearSpendingFunction = value; }
		}

		#endregion // PiecewiseLinearSpendingFunction

		#endregion
	}
}