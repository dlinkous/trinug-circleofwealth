using System;
using Xunit;

namespace CircleOfWealth.Entities.UnitTests
{
	public class GameTests
	{
		[Fact]
		public void NewGamePlayerNamesCannotBeNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				var game = new Game(null, "TestPhrase");
			});
		}

		[Fact]
		public void PlayerNamesAreRequired()
		{
			Assert.Throws<MissingPlayerNamesException>(() =>
			{
				var game = new Game(new string[0], "TestPhrase");
			});
		}

		[Fact]
		public void DuplicatePlayerNamesAreNotAllowed()
		{
			Assert.Throws<DuplicatePlayerNamesException>(() =>
			{
				var game = new Game(new string[] { "Alice", "Bob", "Alice" }, "TestPhrase");
			});
		}

		[Fact]
		public void NewGameContainsCorrectPlayers()
		{
			var game = new Game(new string[] { "Alice", "Bob" }, "TestPhrase");
			Assert.Equal(2, game.Players.Length);
			Assert.Equal("Alice", game.Players[0].Name);
			Assert.Equal("Bob", game.Players[1].Name);
		}

		[Fact]
		public void NewGameStartsWithFirstPlayer()
		{
			var game = new Game(new string[] { "David", "Charlie" }, "TestPhrase");
			Assert.Equal("David", game.CurrentPlayer.Name);
		}

		[Fact]
		public void NewGameContainsCorrectPhrase()
		{
			var game = new Game(new string[] { "Alice", "Bob" }, "TestPhrase");
			Assert.Equal("TestPhrase", game.Phrase.Complete);
			Assert.Equal("__________", game.Phrase.Current);
		}

		[Fact]
		public void NewGameDoesNotRequestLetterNorContainLetterDollarAmount()
		{
			var game = new Game(new string[] { "Alice", "Bob" }, "TestPhrase");
			Assert.False(game.RequestLetter);
			Assert.False(game.LetterDollarAmount.HasValue);
		}

		[Fact]
		public void HandleSpace_WhenDollar_RequestsLetter_AndSavesLetterDollarAmount_AndKeepsCurrentPlayer()
		{
			var game = new Game(new string[] { "Alice", "Bob" }, "TestPhrase");
			Assert.Equal("Alice", game.CurrentPlayer.Name);
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 500));
			Assert.True(game.RequestLetter);
			Assert.True(game.LetterDollarAmount.HasValue);
			Assert.Equal(500, game.LetterDollarAmount.Value);
			Assert.Equal("Alice", game.CurrentPlayer.Name);
		}

		[Fact]
		public void HandleSpace_WhenLoseTurn_DoesNotRequestLetter_DoesNotHaveLetterDollarAmount_ChangesPlayer_MultipleIterations()
		{
			var game = new Game(new string[] { "Alice", "Bob", "Charlie" }, "TestPhrase");
			Assert.Equal("Alice", game.CurrentPlayer.Name);
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.LoseTurn, 0));
			Assert.False(game.RequestLetter);
			Assert.False(game.LetterDollarAmount.HasValue);
			Assert.Equal("Bob", game.CurrentPlayer.Name);
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.LoseTurn, 0));
			Assert.False(game.RequestLetter);
			Assert.False(game.LetterDollarAmount.HasValue);
			Assert.Equal("Charlie", game.CurrentPlayer.Name);
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.LoseTurn, 0));
			Assert.False(game.RequestLetter);
			Assert.False(game.LetterDollarAmount.HasValue);
			Assert.Equal("Alice", game.CurrentPlayer.Name);
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.LoseTurn, 0));
			Assert.False(game.RequestLetter);
			Assert.False(game.LetterDollarAmount.HasValue);
			Assert.Equal("Bob", game.CurrentPlayer.Name);
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.LoseTurn, 0));
			Assert.False(game.RequestLetter);
			Assert.False(game.LetterDollarAmount.HasValue);
			Assert.Equal("Charlie", game.CurrentPlayer.Name);
		}

		[Fact]
		public void HandleSpace_WhenBankrupt_DoesNotRequestLetter_DoesNotHaveLetterDollarAmount_ChangesPlayer_MultipleIterations()
		{
			var game = new Game(new string[] { "Alice", "Bob", "Charlie" }, "TestPhrase");
			Assert.Equal("Alice", game.CurrentPlayer.Name);
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Bankrupt, 0));
			Assert.False(game.RequestLetter);
			Assert.False(game.LetterDollarAmount.HasValue);
			Assert.Equal("Bob", game.CurrentPlayer.Name);
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Bankrupt, 0));
			Assert.False(game.RequestLetter);
			Assert.False(game.LetterDollarAmount.HasValue);
			Assert.Equal("Charlie", game.CurrentPlayer.Name);
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Bankrupt, 0));
			Assert.False(game.RequestLetter);
			Assert.False(game.LetterDollarAmount.HasValue);
			Assert.Equal("Alice", game.CurrentPlayer.Name);
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Bankrupt, 0));
			Assert.False(game.RequestLetter);
			Assert.False(game.LetterDollarAmount.HasValue);
			Assert.Equal("Bob", game.CurrentPlayer.Name);
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Bankrupt, 0));
			Assert.False(game.RequestLetter);
			Assert.False(game.LetterDollarAmount.HasValue);
			Assert.Equal("Charlie", game.CurrentPlayer.Name);
		}

		[Fact]
		public void HandleSpace_LoseTurnPreservesMoney()
		{
			var game = new Game(new string[] { "Alice", "Bob" }, "TestPhrase");
			game.Players[0].AddMoney(750);
			game.Players[1].AddMoney(600);
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.LoseTurn, 0));
			Assert.Equal(750, game.Players[0].Money);
			Assert.Equal(600, game.Players[1].Money);
		}

		[Fact]
		public void HandleSpace_BankruptRemovesMoney()
		{
			var game = new Game(new string[] { "Alice", "Bob" }, "TestPhrase");
			game.Players[0].AddMoney(750);
			game.Players[1].AddMoney(600);
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Bankrupt, 0));
			Assert.Equal(0, game.Players[0].Money);
			Assert.Equal(600, game.Players[1].Money);
		}

		[Fact]
		public void HandleSpace_CanOnlyBeCalledWhenNotWaitingOnLetter()
		{
			var game = new Game(new string[] { "Alice", "Bob" }, "TestPhrase");
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 500));
			Assert.Throws<InvalidOperationException>(() =>
			{
				game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 500));
			});
		}

		[Fact]
		public void HandleLetter_CanOnlyBeCalledWhenWaitingOnLetter()
		{
			var game = new Game(new string[] { "Alice", "Bob" }, "TestPhrase");
			Assert.Throws<InvalidOperationException>(() =>
			{
				game.HandleLetter('A');
			});
		}

		[Fact]
		public void HandleLetter_AlwaysSetsRequestLetterToFalse_AndLetterDollarAmountToNull()
		{
			var game = new Game(new string[] { "Alice", "Bob" }, "TestPhrase");
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 500));
			Assert.True(game.RequestLetter);
			Assert.True(game.LetterDollarAmount.HasValue);
			game.HandleLetter('Z');
			Assert.False(game.RequestLetter);
			Assert.False(game.LetterDollarAmount.HasValue);
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 750));
			Assert.True(game.RequestLetter);
			Assert.True(game.LetterDollarAmount.HasValue);
			game.HandleLetter('T');
			Assert.False(game.RequestLetter);
			Assert.False(game.LetterDollarAmount.HasValue);
		}

		[Fact]
		public void HandleLetter_WhenLetterNotPresent_GivesNoMoney_AndMovesToNextPlayer()
		{
			var game = new Game(new string[] { "Alice", "Bob" }, "TestPhrase");
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 500));
			Assert.Equal("Alice", game.CurrentPlayer.Name);
			Assert.Equal(0, game.Players[0].Money);
			game.HandleLetter('Z');
			Assert.Equal("Bob", game.CurrentPlayer.Name);
			Assert.Equal(0, game.Players[0].Money);
		}

		[Fact]
		public void HandleLetter_WhenLetterPresent_GivesCorrectlyMultipliedMoney_AndStaysOnCurrentPlayer()
		{
			var game = new Game(new string[] { "Alice", "Bob" }, "TestPhrase");
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 500));
			Assert.Equal("Alice", game.CurrentPlayer.Name);
			Assert.Equal(0, game.Players[0].Money);
			game.HandleLetter('e');
			Assert.Equal("Alice", game.CurrentPlayer.Name);
			Assert.Equal(1000, game.Players[0].Money);
		}

		[Fact]
		public void HandleGuess_CanOnlyBeCalledWhenNotWaitingOnLetter()
		{
			var game = new Game(new string[] { "Alice", "Bob" }, "TestPhrase");
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 500));
			Assert.Throws<InvalidOperationException>(() =>
			{
				game.HandleGuess("AnyGuess");
			});
		}

		[Fact]
		public void HandleGuess_WhenIncorrect_MovesToNextPlayer_AndGameContinues()
		{
			var game = new Game(new string[] { "Alice", "Bob" }, "TestPhrase");
			Assert.Equal("Alice", game.CurrentPlayer.Name);
			game.HandleGuess("BadGuess");
			Assert.Equal("Bob", game.CurrentPlayer.Name);
			Assert.False(game.IsOver);
		}

		[Fact]
		public void HandleGuess_WhenCorrect_PutsGameInFinalState()
		{
			var game = new Game(new string[] { "Alice", "Bob" }, "TestPhrase");
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 500));
			game.HandleLetter('e');
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.LoseTurn, 0));
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 500));
			game.HandleLetter('T');
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.LoseTurn, 0));
			game.HandleGuess("TestPhrase");
			Assert.Equal(1000, game.Players[0].Money);
			Assert.Equal(0, game.Players[1].Money);
			Assert.Equal("Alice", game.CurrentPlayer.Name);
			Assert.Equal("TestPhrase", game.Phrase.Current);
			Assert.False(game.RequestLetter);
			Assert.False(game.LetterDollarAmount.HasValue);
			Assert.True(game.IsOver);
		}
	}
}
