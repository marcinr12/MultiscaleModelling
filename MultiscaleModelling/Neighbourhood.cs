using System;
using System.Collections.Generic;
using System.Text;

namespace MultiscaleModelling
{
	public static class Neighbourhood
	{
		// 0 1 2
		// 7 X 3
		// 6 5 4
		public static Dictionary<string, int[]> Patterns { get; private set; } = new Dictionary<string, int[]>()
		{
			{ "moor", new int[] { 1, 1, 1, 1, 1, 1, 1, 1 } }			// Moor
		};
	}
}
