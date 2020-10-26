using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace MultiscaleModelling
{
	public class Calculation
	{
		public static Random Random { get; private set; } = new Random();
		public Dictionary<string, int[]> NeighbourhoodTypes { get; private set; } = new Dictionary<string, int[]>()
		{
			{ "moor", new int[] { 1, 1, 1, 1, 1, 1, 1, 1 } }			// Moor
		};

		public string SelectedNeighbourhoodType { get; private set; } = "moor";

		public List<List<Cell>> Matrix { get; private set; }
		public Func<List<List<int>>> CheckNeighbourhood;
		public Calculation(List<List<Cell>> matrix)
		{
			Matrix = matrix;
			//CheckNeighbourhood = CheckNeighbourhoodAbsorbing;
			CheckNeighbourhood = CheckNeighbourhoodPeriodic;
		}

		public void SetRandomCells(int number)
		{
			int cellNumber = Matrix.Count * Matrix[0].Count;
			number = number < cellNumber
							? number
							: cellNumber;
			int i = 1;
			while (i <= number)
			{
				int yIndex = Random.Next(Matrix.Count);
				int xIndex = Random.Next(Matrix[0].Count);

				if (Matrix[yIndex][xIndex].Id == 0)
				{
					Matrix[yIndex][xIndex].Id = i;
					Matrix[yIndex][xIndex].Color = Color.FromArgb(Random.Next(255), Random.Next(255), Random.Next(255));
					i++;
				}
			}
		}
		public void Clear()
		{
			Matrix.ForEach(v => v.ForEach(c => 
			{
				c.Id = 0;
				c.Color = Color.White;
			}));
		}

		public void CalculateNextGeneration()
		{
			List<List<int>> neighbours = CheckNeighbourhood();

			for (int i = 0; i < Matrix.Count; i++)
			{
				for (int j = 0; j < Matrix[i].Count; j++)
				{

					if (neighbours[i][j] > 0 && Matrix[i][j].Id == 0)
					{
						Matrix[i][j].Color = Matrix.SelectMany(x => x).Where(c => c.Id == neighbours[i][j]).Select(x => x.Color).First();
						Matrix[i][j].Id = neighbours[i][j];
						//grains++;
					}
				}
			}
		}

		private int GetMostRepeatedElenent(IEnumerable<int> collection)
		{
			return collection.GroupBy(i => i).OrderBy(g => g.Count()).Select(g => g.Key).ToList().Where(x => x != 0)?.LastOrDefault() ?? 0;
		}
		private List<List<int>> CheckNeighbourhoodAbsorbing()
		{
			int sizeY = Matrix.Count;
			int sizeX;
			int[] type;

			List<List<int>> neighbours = new List<List<int>>();
			for (int i = 0; i < sizeY; i++)
			{
				sizeX = Matrix[i].Count;
				neighbours.Add(new List<int>());
				for (int j = 0; j < sizeX; j++)
					neighbours[i].Add(0);
			}

			for (int i = 0; i < sizeY; i++)
			{
				sizeX = Matrix[i].Count;
				for (int j = 0; j < sizeX; j++)
				{
					type = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };

					if (NeighbourhoodTypes[SelectedNeighbourhoodType][0] == 1 && i + 1 < sizeY && j + 1 < sizeX)
						type[0] = Matrix[i + 1][j + 1].Id;
					if (NeighbourhoodTypes[SelectedNeighbourhoodType][1] == 1 && i + 1 < sizeY)
						type[1] = Matrix[i + 1][j].Id;
					if (NeighbourhoodTypes[SelectedNeighbourhoodType][2] == 1 && i + 1 < sizeY && j - 1 >= 0)
						type[2] = Matrix[i + 1][j - 1].Id;
					if (NeighbourhoodTypes[SelectedNeighbourhoodType][3] == 1 && j - 1 >= 0)
						type[3] = Matrix[i][j - 1].Id;
					if (NeighbourhoodTypes[SelectedNeighbourhoodType][4] == 1 && i - 1 >= 0 && j - 1 >= 0)
						type[4] = Matrix[i - 1][j - 1].Id;
					if (NeighbourhoodTypes[SelectedNeighbourhoodType][5] == 1 && i - 1 >= 0)
						type[5] = Matrix[i - 1][j].Id;
					if (NeighbourhoodTypes[SelectedNeighbourhoodType][6] == 1 && i - 1 >= 0 && j + 1 < sizeX)
						type[6] = Matrix[i - 1][j + 1].Id;
					if (NeighbourhoodTypes[SelectedNeighbourhoodType][7] == 1 && j + 1 < sizeX)
						type[7] = Matrix[i][j + 1].Id;

					neighbours[i][j] = GetMostRepeatedElenent(type);
				}
			}
			return neighbours;
		}


		private List<List<int>> CheckNeighbourhoodPeriodic()
		{
			int sizeY = Matrix.Count;
			int sizeX;
			int[] type = new int[] { 0, 0, 0, 0 };

			List<List<int>> neighbours = new List<List<int>>();

			for (int i = 0; i < sizeY; i++)
			{
				sizeX = Matrix[0].Count;
				neighbours.Add(new List<int>());
				for (int j = 0; j < sizeX; j++)
					neighbours[i].Add(0);
			}


			for (int i = 0; i < sizeY; i++)
			{
				sizeX = Matrix[0].Count;
				for (int j = 0; j < sizeX; j++)
				{
					type = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };

					if (NeighbourhoodTypes[SelectedNeighbourhoodType][0] == 1)
					{
						if (i + 1 < sizeY && j + 1 < sizeX)
							type[0] = Matrix[i + 1][j + 1].Id;
						else if (i + 1 < sizeY && j + 1 >= sizeX)
							type[0] = Matrix[i + 1][0].Id;
						else if (i + 1 >= sizeY && j + 1 < sizeX)
							type[0] = Matrix[0][j + 1].Id;
						else
							type[0] = Matrix[0][0].Id;
					}
					if (NeighbourhoodTypes[SelectedNeighbourhoodType][1] == 1)
					{
						if (i + 1 < sizeY)
							type[1] = Matrix[i + 1][j].Id;
						else
							type[1] = Matrix[0][j].Id;
					}
					if (NeighbourhoodTypes[SelectedNeighbourhoodType][2] == 1)
					{
						if (i + 1 < sizeY && j - 1 >= 0)
							type[2] = Matrix[i + 1][j - 1].Id;
						else if (i + 1 < sizeY && j - 1 < 0)
							type[2] = Matrix[i + 1][sizeX - 1].Id;
						else if (i + 1 >= sizeY && j - 1 >= 0)
							type[2] = Matrix[0][j - 1].Id;
						else
							type[2] = Matrix[0][sizeX - 1].Id;
					}
					if (NeighbourhoodTypes[SelectedNeighbourhoodType][3] == 1)
					{
						if (j - 1 >= 0)
							type[3] = Matrix[i][j - 1].Id;
						else
							type[3] = Matrix[i][sizeX - 1].Id;
					}
					if (NeighbourhoodTypes[SelectedNeighbourhoodType][4] == 1)
					{
						if (i - 1 >= 0 && j - 1 >= 0)
							type[4] = Matrix[i - 1][j - 1].Id;
						else if (i - 1 >= 0 && j - 1 < 0)
							type[4] = Matrix[i - 1][sizeX - 1].Id;
						else if (i - 1 < 0 && j - 1 >= 0)
							type[4] = Matrix[sizeY - 1][j - 1].Id;
						else
							type[4] = Matrix[sizeY - 1][sizeX - 1].Id;
					}
					if (NeighbourhoodTypes[SelectedNeighbourhoodType][5] == 1)
					{
						if (i - 1 >= 0)
							type[5] = Matrix[i - 1][j].Id;
						else
							type[5] = Matrix[sizeY - 1][j].Id;
					}
					if (NeighbourhoodTypes[SelectedNeighbourhoodType][6] == 1)
					{
						if (i - 1 >= 0 && j + 1 < sizeX)
							type[6] = Matrix[i - 1][j + 1].Id;
						else if (i - 1 >= 0 && j + 1 >= sizeX)
							type[6] = Matrix[i - 1][0].Id;
						else if (i - 1 < 0 && j + 1 < sizeX)
							type[6] = Matrix[sizeY - 1][j + 1].Id;
						else
							type[6] = Matrix[sizeY - 1][0].Id;
					}
					if (NeighbourhoodTypes[SelectedNeighbourhoodType][7] == 1)
					{
						if (j + 1 < sizeX)
							type[7] = Matrix[i][j + 1].Id;
						else
							type[7] = Matrix[i][0].Id;
					}
					neighbours[i][j] = GetMostRepeatedElenent(type);
				}
			}
			return neighbours;
		}
	}
}
