using System;
using CircleOfWealth.Interactors.Common.Providers;

namespace CircleOfWealth.ConsoleApplication
{
	internal class DotNetRandomProvider : IRandomProvider
	{
		private readonly Random random = new Random();

		public int GetRandom(int min, int max) => random.Next(min, max);
	}
}
