using System;
using System.Collections.Generic;
using CircleOfWealth.Interactors.Common.Providers;

namespace CircleOfWealth.Interactors.UnitTests.Mocks
{
	internal class MockRandomProvider : IRandomProvider
	{
		internal readonly Dictionary<(int min, int max), int> Values = new Dictionary<(int min, int max), int>();

		public int GetRandom(int min, int max) => Values[(min, max)];
	}
}
