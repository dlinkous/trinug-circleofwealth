using System;
using CircleOfWealth.Interactors.Common.Boundaries;
using CircleOfWealth.Interactors.Common.Repositories;

namespace CircleOfWealth.Interactors
{
	public class PickLetter : IInputBoundary<PickLetter.RequestModel, PickLetter.ResponseModel>
	{
		public class RequestModel
		{
			public Guid GameIdentifier { get; set; }
			public char Letter { get; set; }
		}

		public class ResponseModel
		{
			public string Phrase { get; set; }
			public string CurrentPlayerName { get; set; }
		}

		private readonly IGameRepository gameRepository;

		public PickLetter(IGameRepository gameRepository)
		{
			this.gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
		}

		public void HandleRequest(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			var game = gameRepository.GetGame(requestModel.GameIdentifier);
			game.HandleLetter(requestModel.Letter);
			gameRepository.SaveGame(requestModel.GameIdentifier, game);
			outputBoundary.HandleResponse(new ResponseModel()
			{
				Phrase = game.Phrase.Current,
				CurrentPlayerName = game.CurrentPlayer.Name
			});
		}
	}
}
