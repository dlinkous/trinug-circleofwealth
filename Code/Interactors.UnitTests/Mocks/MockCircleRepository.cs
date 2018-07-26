using System;
using CircleOfWealth.Entities;
using CircleOfWealth.Interactors.Common.Repositories;

namespace CircleOfWealth.Interactors.UnitTests.Mocks
{
	public class MockCircleRepository : ICircleRepository
	{
		public Circle Circle;

		public Circle GetCircle() => Circle;
	}
}
