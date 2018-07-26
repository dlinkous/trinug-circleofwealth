using System;
using CircleOfWealth.Interactors.Common.Boundaries;
using CircleOfWealth.Interactors.Common.Repositories;

namespace CircleOfWealth.Interactors
{
	public class GuessPhrase : IInputBoundary<GuessPhrase.RequestModel, GuessPhrase.ResponseModel>
	{
		public class RequestModel
		{
			public Guid GameIdentifier { get; set; }
			public string Guess { get; set; }
		}

		public class ResponseModel
		{
			public string Phrase { get; set; }
			public string CurrentPlayerName { get; set; }
			public int Reward { get; set; }
		}

		private readonly IGameRepository gameRepository;

		public GuessPhrase(IGameRepository gameRepository)
		{
			this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
		}

		public void HandleRequest(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			var game = gameRepository.GetGame(requestModel.GameIdentifier);
			game.HandleGuess(requestModel.Guess);
			gameRepository.SaveGame(requestModel.GameIdentifier, game);
			var reward = game.IsOver ? game.CurrentPlayer.Money : 0;
			outputBoundary.HandleResponse(new ResponseModel()
			{
				Phrase = game.Phrase.Current,
				CurrentPlayerName = game.CurrentPlayer.Name,
				Reward = reward
			});
		}
	}
}
