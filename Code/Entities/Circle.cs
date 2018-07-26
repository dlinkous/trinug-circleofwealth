using System;
using System.Collections.Generic;

namespace CircleOfWealth.Entities
{
	public class Circle
	{
		private readonly List<CircleSpace> spaces = new List<CircleSpace>();

		public void AddSpace(CircleSpace circleSpace) => spaces.Add(circleSpace);

		public int CountSpaces() => spaces.Count;

		public CircleSpace GetSpace(int index) => spaces[index];
	}
}
