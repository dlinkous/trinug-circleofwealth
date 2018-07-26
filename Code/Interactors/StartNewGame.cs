using System;
using CircleOfWealth.Entities;
using CircleOfWealth.Interactors.Common.Boundaries;
using CircleOfWealth.Interactors.Common.Repositories;

namespace CircleOfWealth.Interactors
{
	public class StartNewGame : IInputBoundary<StartNewGame.RequestModel, StartNewGame.ResponseModel>
	{
		public class RequestModel
		{
			public string[] PlayerNames { get; set; }
		}

		public class ResponseModel
		{
			public Guid GameIdentifier { get; set; }
			public string Phrase { get; set; }
			public string CurrentPlayerName { get; set; }
		}

		private readonly IPhraseRepository phraseRepository;
		private readonly IGameRepository gameRepository;

		public StartNewGame(IPhraseRepository phraseRepository, IGameRepository gameRepository)
		{
			this.phraseRepository = phraseRepository ?? throw new ArgumentNullException(nameof(phraseRepository));
			this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
		}

		public void HandleRequest(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			var completePhrase = phraseRepository.GetPhrase();
			var game = new Game(requestModel.PlayerNames, completePhrase);
			var gameIdentifier = gameRepository.CreateNewGame(game);
			outputBoundary.HandleResponse(new ResponseModel()
			{
				GameIdentifier = gameIdentifier,
				Phrase = game.Phrase.Current,
				CurrentPlayerName = game.CurrentPlayer.Name
			});
		}
	}
}
