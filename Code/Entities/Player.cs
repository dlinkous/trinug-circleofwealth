using System;

namespace CircleOfWealth.Entities
{
	[Serializable]
	public class Player
	{
		public string Name { get; }
		public int Money { get; private set; }

		public Player(string name)
		{
			if (String.IsNullOrWhiteSpace(name)) throw new ArgumentException();
			Name = name;
		}

		public void AddMoney(int money) => Money += money;

		public void RemoveAllMoney() => Money = 0;
	}
}
