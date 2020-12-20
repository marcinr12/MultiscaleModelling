using System;

namespace MultiscaleModelling
{
	public static class RandomMachine
	{
		private static readonly object syncNext = new object();
		private static readonly object syncNextMax = new object();
		private static readonly object syncNextMinMax = new object();
		private readonly static Random Random = new Random();

		public static int Next()
		{
			lock (syncNext)
			{
				return Random.Next();
			}
		}

		public static int Next(int maxValue)
		{
			lock(syncNext)
			{
				return Random.Next(maxValue);
			}
		}

		public static int Next(int minValue, int maxValue)
		{
			lock(syncNext)
			{
				return Random.Next(minValue, maxValue);
			}
		}
	}
}
