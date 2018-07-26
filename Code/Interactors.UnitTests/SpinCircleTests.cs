using System;
using Xunit;
using CircleOfWealth.Entities;
using CircleOfWealth.Interactors.UnitTests.Mocks;
using CircleOfWealth.Interactors.UnitTests.Presenters;

namespace CircleOfWealth.Interactors.UnitTests
{
	public class SpinCircleTests
	{
		[Fact]
		public void WhenDollarSpin_RespondsCorrectly()
		{
			var mockGameRepository = new MockGameRepository();
			var mockCircleRepository = new MockCircleRepository();
			var mockRandomProvider = new MockRandomProvider();
			var interactor = new SpinCircle(mockGameRepository, mockCircleRepository, mockRandomProvider);
			var gameIdentifier = Guid.Parse("00000000-0000-0000-0000-000000000001");
			mockGameRepository.Games.Add(gameIdentifier, new Game(new string[] { "Alice", "Bob" }, "TestPhrase"));
			mockCircleRepository.Circle = GetCircle();
			const int indexOneThousand = 7;
			mockRandomProvider.Values.Add((0, 13), indexOneThousand);
			var requestModel = new SpinCircle.RequestModel()
			{
				GameIdentifier = gameIdentifier
			};
			var presenter = new Presenter<SpinCircle.ResponseModel>();
			interactor.HandleRequest(requestModel, presenter);
			var responseModel = presenter.ResponseModel;
			Assert.Equal("1000", responseModel.ResultDescription);
			Assert.True(responseModel.RequestLetter);
			Assert.Equal("Alice", responseModel.CurrentPlayerName);
		}

		[Fact]
		public void WhenLoseTurn_RespondsCorrectly()
		{
			var mockGameRepository = new MockGameRepository();
			var mockCircleRepository = new MockCircleRepository();
			var mockRandomProvider = new MockRandomProvider();
			var interactor = new SpinCircle(mockGameRepository, mockCircleRepository, mockRandomProvider);
			var gameIdentifier = Guid.Parse("00000000-0000-0000-0000-000000000001");
			mockGameRepository.Games.Add(gameIdentifier, new Game(new string[] { "Alice", "Bob" }, "TestPhrase"));
			mockCircleRepository.Circle = GetCircle();
			const int indexLoseTurn = 1;
			mockRandomProvider.Values.Add((0, 13), indexLoseTurn);
			var requestModel = new SpinCircle.RequestModel()
			{
				GameIdentifier = gameIdentifier
			};
			var presenter = new Presenter<SpinCircle.ResponseModel>();
			interactor.HandleRequest(requestModel, presenter);
			var responseModel = presenter.ResponseModel;
			Assert.Equal("LoseTurn", responseModel.ResultDescription);
			Assert.False(responseModel.RequestLetter);
			Assert.Equal("Bob", responseModel.CurrentPlayerName);
		}

		[Fact]
		public void WhenBankrupt_RespondsCorrectly()
		{
			var mockGameRepository = new MockGameRepository();
			var mockCircleRepository = new MockCircleRepository();
			var mockRandomProvider = new MockRandomProvider();
			var interactor = new SpinCircle(mockGameRepository, mockCircleRepository, mockRandomProvider);
			var gameIdentifier = Guid.Parse("00000000-0000-0000-0000-000000000001");
			mockGameRepository.Games.Add(gameIdentifier, new Game(new string[] { "Alice", "Bob" }, "TestPhrase"));
			mockCircleRepository.Circle = GetCircle();
			const int indexBankrupt = 0;
			mockRandomProvider.Values.Add((0, 13), indexBankrupt);
			var requestModel = new SpinCircle.RequestModel()
			{
				GameIdentifier = gameIdentifier
			};
			var presenter = new Presenter<SpinCircle.ResponseModel>();
			interactor.HandleRequest(requestModel, presenter);
			var responseModel = presenter.ResponseModel;
			Assert.Equal("Bankrupt", responseModel.ResultDescription);
			Assert.False(responseModel.RequestLetter);
			Assert.Equal("Bob", responseModel.CurrentPlayerName);
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
