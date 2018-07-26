using System;

namespace CircleOfWealth.Entities
{
	public struct CircleSpace
	{
		public enum SpaceType
		{
			Dollar,
			LoseTurn,
			Bankrupt
		}

		public SpaceType Type;
		public int DollarAmount;

		public CircleSpace(SpaceType type, int dollarAmount)
		{
			Type = type;
			DollarAmount = dollarAmount;
		}
	}
}
