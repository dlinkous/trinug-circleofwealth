using System;
using Xunit;
using CircleOfWealth.Entities;
using CircleOfWealth.Interactors.UnitTests.Mocks;
using CircleOfWealth.Interactors.UnitTests.Presenters;

namespace CircleOfWealth.Interactors.UnitTests
{
	public class GuessPhraseTests
	{
		[Fact]
		public void WhenGuessIncorrect_RespondsCorrectly()
		{
			var mockGameRepository = new MockGameRepository();
			var interactor = new GuessPhrase(mockGameRepository);
			var gameIdentifier = Guid.Parse("00000000-0000-0000-0000-000000000001");
			var game = new Game(new string[] { "Alice", "Bob" }, "TestPhrase");
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 1600));
			game.HandleLetter('e');
			mockGameRepository.Games.Add(gameIdentifier, game);
			var requestModel = new GuessPhrase.RequestModel()
			{
				GameIdentifier = gameIdentifier,
				Guess = "WrongGuess"
			};
			var presenter = new Presenter<GuessPhrase.ResponseModel>();
			interactor.HandleRequest(requestModel, presenter);
			var responseModel = presenter.ResponseModel;
			Assert.Equal("_e_______e", responseModel.Phrase);
			Assert.Equal("Bob", responseModel.CurrentPlayerName);
			Assert.Equal(0, responseModel.Reward);
		}

		[Fact]
		public void WhenGuessCorrect_RespondsCorrectly()
		{
			var mockGameRepository = new MockGameRepository();
			var interactor = new GuessPhrase(mockGameRepository);
			var gameIdentifier = Guid.Parse("00000000-0000-0000-0000-000000000001");
			var game = new Game(new string[] { "Alice", "Bob" }, "TestPhrase");
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 1600));
			game.HandleLetter('e');
			mockGameRepository.Games.Add(gameIdentifier, game);
			var requestModel = new GuessPhrase.RequestModel()
			{
				GameIdentifier = gameIdentifier,
				Guess = "TestPhrase"
			};
			var presenter = new Presenter<GuessPhrase.ResponseModel>();
			interactor.HandleRequest(requestModel, presenter);
			var responseModel = presenter.ResponseModel;
			Assert.Equal("TestPhrase", responseModel.Phrase);
			Assert.Equal("Alice", responseModel.CurrentPlayerName);
			Assert.Equal(3200, responseModel.Reward);
		}
	}
}
