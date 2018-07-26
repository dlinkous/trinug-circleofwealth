using System;
using CircleOfWealth.Interactors.Common.Repositories;

namespace CircleOfWealth.Interactors.UnitTests.Mocks
{
	public class MockPhraseRepository : IPhraseRepository
	{
		public string Phrase;

		public string GetPhrase() => Phrase;
	}
}
