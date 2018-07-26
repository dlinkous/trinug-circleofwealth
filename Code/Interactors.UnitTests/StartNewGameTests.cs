using System;
using Xunit;
using CircleOfWealth.Interactors.UnitTests.Mocks;
using CircleOfWealth.Interactors.UnitTests.Presenters;

namespace CircleOfWealth.Interactors.UnitTests
{
	public class StartNewGameTests
	{
		[Fact]
		public void StartsNewGameCorrectly()
		{
			var mockPhraseRepository = new MockPhraseRepository();
			var mockGameRepository = new MockGameRepository();
			var interactor = new StartNewGame(mockPhraseRepository, mockGameRepository);
			mockPhraseRepository.Phrase = "Small Test Phrase";
			mockGameRepository.GameIdentifier = Guid.Parse("00000000-0000-0000-0000-000000000001");
			var requestModel = new StartNewGame.RequestModel()
			{
				PlayerNames = new string[] { "Alice", "Bob", "Charlie" }
			};
			var presenter = new Presenter<StartNewGame.ResponseModel>();
			interactor.HandleRequest(requestModel, presenter);
			var responseModel = presenter.ResponseModel;
			Assert.Equal(Guid.Parse("00000000-0000-0000-0000-000000000001"), responseModel.GameIdentifier);
			Assert.Equal("_____ ____ ______", responseModel.Phrase);
			Assert.Equal("Alice", responseModel.CurrentPlayerName);
		}
	}
}
