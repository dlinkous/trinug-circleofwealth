using System;
using Xunit;

namespace CircleOfWealth.Entities.UnitTests
{
	public class PhraseTests
	{
		[Fact]
		public void CompleteMustBeValid()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var phrase = new Phrase(null);
			});
			Assert.Throws<ArgumentException>(() =>
			{
				var phrase = new Phrase(String.Empty);
			});
			Assert.Throws<ArgumentException>(() =>
			{
				var phrase = new Phrase(" ");
			});
		}

		[Fact]
		public void CompleteContainsCorrectCompletePhrase()
		{
			var phrase = new Phrase("CompletePhrase");
			Assert.Equal("CompletePhrase", phrase.Complete);
		}

		[Fact]
		public void InvalidCompletePhraseIsNotAllowed()
		{
			Assert.Throws<InvalidPhraseException>(() =>
			{
				var phrase = new Phrase("A 100% Bad Character");
			});
		}

		[Fact]
		public void CurrentContainsCorrectIncompletePhrase()
		{
			var phrase = new Phrase("A currently-valid phrase, for a player's benefit");
			Assert.Equal("_ _________-_____ ______, ___ _ ______'_ _______", phrase.Current);
		}

		[Fact]
		public void AppliesLettersCorrectly()
		{
			var phrase = new Phrase("TEST");
			Assert.Equal("____", phrase.Current);
			Assert.Equal(0, phrase.ApplyLetter('A'));
			Assert.Equal("____", phrase.Current);
			Assert.Equal(0, phrase.ApplyLetter('B'));
			Assert.Equal("____", phrase.Current);
			Assert.Equal(0, phrase.ApplyLetter('Z'));
			Assert.Equal("____", phrase.Current);
			Assert.Equal(0, phrase.ApplyLetter('Z'));
			Assert.Equal("____", phrase.Current);
			Assert.Equal(1, phrase.ApplyLetter('E'));
			Assert.Equal("_E__", phrase.Current);
			Assert.Equal(0, phrase.ApplyLetter('K'));
			Assert.Equal("_E__", phrase.Current);
			Assert.Equal(0, phrase.ApplyLetter('K'));
			Assert.Equal("_E__", phrase.Current);
			Assert.Equal(1, phrase.ApplyLetter('S'));
			Assert.Equal("_ES_", phrase.Current);
			Assert.Equal(2, phrase.ApplyLetter('T'));
			Assert.Equal("TEST", phrase.Current);
			Assert.Equal(0, phrase.ApplyLetter('Z'));
			Assert.Equal("TEST", phrase.Current);
			Assert.Equal(0, phrase.ApplyLetter('E'));
			Assert.Equal("TEST", phrase.Current);
			Assert.Equal(0, phrase.ApplyLetter('S'));
			Assert.Equal("TEST", phrase.Current);
			Assert.Equal(0, phrase.ApplyLetter('T'));
			Assert.Equal("TEST", phrase.Current);
		}

		[Fact]
		public void ShowsCompletePhraseCorrectly()
		{
			var phrase = new Phrase("TEST");
			Assert.Equal("____", phrase.Current);
			phrase.ShowCompletePhrase();
			Assert.Equal("TEST", phrase.Current);
		}
	}
}
