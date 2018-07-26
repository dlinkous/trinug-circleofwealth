using System;
using System.Linq;

namespace CircleOfWealth.Entities
{
	[Serializable]
	public class Game
	{
		public Player[] Players { get; }
		public Player CurrentPlayer { get; private set; }
		public Phrase Phrase { get; }
		public bool RequestLetter { get; private set; }
		public int? LetterDollarAmount { get; private set; }
		public bool IsOver { get; private set; }

		public Game(string[] playerNames, string completePhrase)
		{
			if (playerNames == null) throw new ArgumentNullException(nameof(playerNames));
			if (playerNames.Length == 0) throw new MissingPlayerNamesException();
			var distinctPlayerNames = playerNames.Distinct().ToArray();
			if (distinctPlayerNames.Length != playerNames.Length) throw new DuplicatePlayerNamesException();
			Players = distinctPlayerNames.Select(n => new Player(n)).ToArray();
			CurrentPlayer = Players.First();
			Phrase = new Phrase(completePhrase);
		}

		public void HandleSpace(CircleSpace circleSpace)
		{
			if (RequestLetter) throw new InvalidOperationException();
			switch (circleSpace.Type)
			{
				case CircleSpace.SpaceType.Dollar:
					RequestLetter = true;
					LetterDollarAmount = circleSpace.DollarAmount;
					break;
				case CircleSpace.SpaceType.LoseTurn:
					NextPlayer();
					break;
				case CircleSpace.SpaceType.Bankrupt:
					CurrentPlayer.RemoveAllMoney();
					NextPlayer();
					break;
				default:
					throw new NotSupportedException();
			}
		}

		public void HandleLetter(char letter)
		{
			if (!RequestLetter) throw new InvalidOperationException();
			var quantityOfLetters = Phrase.ApplyLetter(letter);
			if (quantityOfLetters > 0)
			{
				var moneyToAdd = LetterDollarAmount.Value * quantityOfLetters;
				CurrentPlayer.AddMoney(moneyToAdd);
			}
			else
			{
				NextPlayer();
			}
			RequestLetter = false;
			LetterDollarAmount = null;
		}

		public void HandleGuess(string guess)
		{
			if (RequestLetter) throw new InvalidOperationException();
			if (guess == Phrase.Complete)
			{
				foreach (var otherPlayer in Players.Where(p => p != CurrentPlayer)) otherPlayer.RemoveAllMoney();
				Phrase.ShowCompletePhrase();
				IsOver = true;
			}
			else
			{
				NextPlayer();
			}
		}

		private void NextPlayer()
		{
			var indexOfCurrentPlayer = GetIndexOfCurrentPlayer();
			var maximumIndex = Players.Length - 1;
			if (indexOfCurrentPlayer < maximumIndex)
				CurrentPlayer = Players[indexOfCurrentPlayer + 1];
			else
				CurrentPlayer = Players[0];
		}

		private int GetIndexOfCurrentPlayer()
		{
			for (var i = 0; i < Players.Length; i++)
				if (Players[i] == CurrentPlayer)
					return i;
			throw new InvalidOperationException();
		}
	}
}
