using System;
using System.Text;	

namespace CircleOfWealth.Entities
{
	[Serializable]
	public class Phrase
	{
		public string Complete { get; }
		public string Current { get; private set; }

		private const char blankCharacter = '_';
		private const char spaceCharacter = ' ';
		private const char commaCharacter = ',';
		private const char apostropheCharacter = '\'';
		private const char hyphenCharacter = '-';

		public Phrase(string complete)
		{
			if (String.IsNullOrWhiteSpace(complete)) throw new ArgumentException(nameof(complete));
			var currentBuilder = new StringBuilder();
			foreach (var c in complete.ToCharArray())
			{
				if (Char.IsLetter(c)) currentBuilder.Append(blankCharacter);
				else if (c == spaceCharacter) currentBuilder.Append(c);
				else if (c == commaCharacter) currentBuilder.Append(c);
				else if (c == apostropheCharacter) currentBuilder.Append(c);
				else if (c == hyphenCharacter) currentBuilder.Append(c);
				else throw new InvalidPhraseException();
			}
			Complete = complete;
			Current = currentBuilder.ToString();
		}

		public int ApplyLetter(char letter)
		{
			var quantityOfNewlyAppliedLetters = 0;
			var currentChars = Current.ToCharArray();
			for (var i = 0; i < Complete.Length; i++)
			{
				if (Complete[i] == letter)
				{
					if (currentChars[i] != letter)
					{
						currentChars[i] = letter;
						quantityOfNewlyAppliedLetters++;
					}
				}
			}
			Current = new string(currentChars);
			return quantityOfNewlyAppliedLetters;
		}

		public void ShowCompletePhrase() => Current = Complete;
	}
}
