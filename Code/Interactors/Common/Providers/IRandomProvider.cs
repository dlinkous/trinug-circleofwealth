using System;

namespace CircleOfWealth.Interactors.Common.Providers
{
	public interface IRandomProvider
	{
		int GetRandom(int min, int max);
	}
}
