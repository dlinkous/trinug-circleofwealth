using System;
using System.Linq;
using Xunit;
using CircleOfWealth.Entities;
using CircleOfWealth.Interactors.UnitTests.Mocks;
using CircleOfWealth.Interactors.UnitTests.Presenters;

namespace CircleOfWealth.Interactors.UnitTests.CompositeTests
{
	public class InteractorTests
	{
		[Fact]
		public void FullShortGameTest()
		{
			var mockCircleRepository = new MockCircleRepository();
			var mockGameRepository = new MockGameRepository();
			var mockPhraseRepository = new MockPhraseRepository();
			var mockRandomProvider = new MockRandomProvider();
			mockCircleRepository.Circle = GetCircle();
			mockPhraseRepository.Phrase = "Short Game Test";
			const int indexOneThousand = 7;
			mockRandomProvider.Values.Add((0, 13), indexOneThousand);
			var startNewGamePresenter = new Presenter<StartNewGame.ResponseModel>();
			var startNewGameInteractor = new StartNewGame(mockPhraseRepository, mockGameRepository);
			startNewGameInteractor.HandleRequest(new StartNewGame.RequestModel()
			{
				PlayerNames = new string[]
				{
					"Alice", "Bob", "Charlie"
				}
			}, startNewGamePresenter);
			var spinCirclePresenter = new Presenter<SpinCircle.ResponseModel>();
			var spinCircleInteractor = new SpinCircle(mockGameRepository, mockCircleRepository, mockRandomProvider);
			spinCircleInteractor.HandleRequest(new SpinCircle.RequestModel()
			{
				GameIdentifier = startNewGamePresenter.ResponseModel.GameIdentifier
			}, spinCirclePresenter);
			var pickLetterPresenter = new Presenter<PickLetter.ResponseModel>();
			var pickLetterInteractor = new PickLetter(mockGameRepository);
			pickLetterInteractor.HandleRequest(new PickLetter.RequestModel()
			{
				GameIdentifier = startNewGamePresenter.ResponseModel.GameIdentifier,
				Letter = 'e'
			}, pickLetterPresenter);
			var guessPhrasePresenter = new Presenter<GuessPhrase.ResponseModel>();
			var guessPhraseInteractor = new GuessPhrase(mockGameRepository);
			guessPhraseInteractor.HandleRequest(new GuessPhrase.RequestModel()
			{
				GameIdentifier = startNewGamePresenter.ResponseModel.GameIdentifier,
				Guess = "Short Game Test"
			}, guessPhrasePresenter);
			Assert.Equal("Short Game Test", guessPhrasePresenter.ResponseModel.Phrase);
			Assert.Equal("Alice", guessPhrasePresenter.ResponseModel.CurrentPlayerName);
			Assert.Equal(2000, guessPhrasePresenter.ResponseModel.Reward);
			Assert.True(mockGameRepository.Games.Single().Value.IsOver);
		}

		private Circle GetCircle()
		{
			var circle = new Circle();
			circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Bankrupt, 0));
			circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.LoseTurn, 0));
			circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 500));
			circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 600));
			circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 700));
			circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 800));
			circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 900));
			circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 1000));
			circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 1100));
			circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 1200));
			circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 1300));
			circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 1400));
			circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 1500));
			return circle;
		}
	}
}
