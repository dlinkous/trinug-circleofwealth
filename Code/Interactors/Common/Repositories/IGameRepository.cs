using System;
using CircleOfWealth.Entities;

namespace CircleOfWealth.Interactors.Common.Repositories
{
	public interface IGameRepository
	{
		Guid CreateNewGame(Game game);
		Game GetGame(Guid gameIdentifier);
		void SaveGame(Guid gameIdentifier, Game game);
	}
}
