using System;
using CircleOfWealth.Interactors.Common.Boundaries;
using CircleOfWealth.Interactors.Common.Providers;
using CircleOfWealth.Interactors.Common.Repositories;

namespace CircleOfWealth.Interactors
{
	public class SpinCircle : IInputBoundary<SpinCircle.RequestModel, SpinCircle.ResponseModel>
	{
		public class RequestModel
		{
			public Guid GameIdentifier { get; set; }
		}

		public class ResponseModel
		{
			public string ResultDescription { get; set; }
			public bool RequestLetter { get; set; }
			public string CurrentPlayerName { get; set; }
		}

		private readonly IGameRepository gameRepository;
		private readonly ICircleRepository circleRepository;
		private readonly IRandomProvider randomProvider;

		public SpinCircle(IGameRepository gameRepository, ICircleRepository circleRepository, IRandomProvider randomProvider)
		{
			this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
			this.circleRepository = circleRepository ?? throw new ArgumentNullException(nameof(circleRepository));
			this.randomProvider = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
		}

		public void HandleRequest(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			var game = gameRepository.GetGame(requestModel.GameIdentifier);
			var circle = circleRepository.GetCircle();
			var space = circle.GetSpace(randomProvider.GetRandom(0, circle.CountSpaces()));
			game.HandleSpace(space);
			gameRepository.SaveGame(requestModel.GameIdentifier, game);
			var resultDescription = game.RequestLetter ? game.LetterDollarAmount.Value.ToString() : space.Type.ToString();
			outputBoundary.HandleResponse(new ResponseModel()
			{
				ResultDescription = resultDescription,
				RequestLetter = game.RequestLetter,
				CurrentPlayerName = game.CurrentPlayer.Name
			});
		}
	}
}
