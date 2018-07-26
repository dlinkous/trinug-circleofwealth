using System;
using Xunit;

namespace CircleOfWealth.Entities.UnitTests
{
	public class CircleTests
	{
		[Fact]
		public void CircleAddsAndCountsAndGetsCorrectly()
		{
			var circle = new Circle();
			Assert.Equal(0, circle.CountSpaces());
			circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 500));
			circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.LoseTurn, 0));
			circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Bankrupt, 0));
			Assert.Equal(3, circle.CountSpaces());
			var space = circle.GetSpace(1);
			Assert.Equal(CircleSpace.SpaceType.LoseTurn, space.Type);
			Assert.Equal(0, space.DollarAmount);
		}
	}
}
