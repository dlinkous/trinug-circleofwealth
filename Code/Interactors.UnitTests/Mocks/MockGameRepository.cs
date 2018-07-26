using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CircleOfWealth.Entities;
using CircleOfWealth.Interactors.Common.Repositories;

namespace CircleOfWealth.Interactors.UnitTests.Mocks
{
	public class MockGameRepository : IGameRepository
	{
		public Guid GameIdentifier;
		public readonly Dictionary<Guid, Game> Games = new Dictionary<Guid, Game>();

		public Guid CreateNewGame(Game game)
		{
			Games.Add(GameIdentifier, CopyGame(game));
			return GameIdentifier;
		}

		public void SaveGame(Guid gameIdentifier, Game game) => Games[gameIdentifier] = CopyGame(game);

		public Game GetGame(Guid gameIdentifier) => CopyGame(Games[gameIdentifier]);

		private Game CopyGame(Game original)
		{
			var formatter = new BinaryFormatter();
			byte[] bytes;
			using (var originalStream = new MemoryStream())
			{
				formatter.Serialize(originalStream, original);
				bytes = originalStream.ToArray();
			}
			using (var copyStream = new MemoryStream(bytes))
			{
				var copy = (Game)formatter.Deserialize(copyStream);
				return copy;
			}
		}
	}
}
