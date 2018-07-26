using System;
using Xunit;
using CircleOfWealth.Entities;
using CircleOfWealth.Interactors.UnitTests.Mocks;
using CircleOfWealth.Interactors.UnitTests.Presenters;

namespace CircleOfWealth.Interactors.UnitTests
{
	public class PickLetterTests
	{
		[Fact]
		public void PicksLetterThatIsPresentCorrectly()
		{
			var mockGameRepository = new MockGameRepository();
			var interactor = new PickLetter(mockGameRepository);
			var gameIdentifier = Guid.Parse("00000000-0000-0000-0000-000000000001");
			var game = new Game(new string[] { "Alice", "Bob" }, "TestPhrase");
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 1400));
			mockGameRepository.Games.Add(gameIdentifier, game);
			var requestModel = new PickLetter.RequestModel()
			{
				GameIdentifier = gameIdentifier,
				Letter = 'e'
			};
			var presenter = new Presenter<PickLetter.ResponseModel>();
			interactor.HandleRequest(requestModel, presenter);
			var responseModel = presenter.ResponseModel;
			Assert.Equal("_e_______e", responseModel.Phrase);
			Assert.Equal("Alice", responseModel.CurrentPlayerName);
		}

		[Fact]
		public void PicksLetterThatIsMissingCorrectly()
		{
			var mockGameRepository = new MockGameRepository();
			var interactor = new PickLetter(mockGameRepository);
			var gameIdentifier = Guid.Parse("00000000-0000-0000-0000-000000000001");
			var game = new Game(new string[] { "Alice", "Bob" }, "TestPhrase");
			game.HandleSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 1400));
			mockGameRepository.Games.Add(gameIdentifier, game);
			var requestModel = new PickLetter.RequestModel()
			{
				GameIdentifier = gameIdentifier,
				Letter = 'Z'
			};
			var presenter = new Presenter<PickLetter.ResponseModel>();
			interactor.HandleRequest(requestModel, presenter);
			var responseModel = presenter.ResponseModel;
			Assert.Equal("__________", responseModel.Phrase);
			Assert.Equal("Bob", responseModel.CurrentPlayerName);
		}
	}
}
