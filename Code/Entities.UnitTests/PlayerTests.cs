using System;
using Xunit;

namespace CircleOfWealth.Entities.UnitTests
{
	public class PlayerTests
	{
		[Fact]
		public void PlayerNameMustBeValid()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var player = new Player(null);
			});
			Assert.Throws<ArgumentException>(() =>
			{
				var player = new Player(String.Empty);
			});
			Assert.Throws<ArgumentException>(() =>
			{
				var player = new Player(" ");
			});
		}

		[Fact]
		public void PlayerStartsWithZeroMoney()
		{
			var player = new Player("TestName");
			Assert.Equal(0, player.Money);
		}

		[Fact]
		public void MoneyChangesCorrectly()
		{
			var player = new Player("TestName");
			Assert.Equal(0, player.Money);
			player.AddMoney(500);
			Assert.Equal(500, player.Money);
			player.AddMoney(1300);
			Assert.Equal(1800, player.Money);
			player.RemoveAllMoney();
			Assert.Equal(0, player.Money);
			player.AddMoney(750);
			Assert.Equal(750, player.Money);
			player.RemoveAllMoney();
			Assert.Equal(0, player.Money);
		}
	}
}
