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
		public static Dictionary<RuleType, int[]> Patterns { get; private set; } = new Dictionary<RuleType, int[]>()
		{
			{ RuleType.Moor,		new int[] { 1, 1, 1, 1, 1, 1, 1, 1 } },		// Moor
			{ RuleType.VonNeumann,	new int[] { 0, 1, 0, 1, 0, 1, 0, 1 } },		// von Neumann
			{ RuleType.FurtherMoor,	new int[] { 1, 0, 1, 0, 1, 0, 1, 0 } },		// Further Moor
		};
	}
}
