using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace MultiscaleModelling
{
	public class Matrix
	{
		private readonly List<List<Cell>> rows;
		private readonly List<List<(int Id, Color Color)>> copy;
		public int RowsCount => rows.Count;
		public int ColumnsCount => rows.ElementAtOrDefault(0)?.Count ?? 0;
		public double CellSize { get; private set; }
		public readonly Random Random = new Random();
		public Matrix()
		{
			rows = new List<List<Cell>>() { new List<Cell>() { new Cell(0, 0, this) } };
			copy = new List<List<(int Id, Color Color)>>() { new List<(int Id, Color Color)>() { (Id: 1, Color: Color.White) } };
		}

		public Cell GetCell(int row, int column)
		{
			return rows[row][column];
		}
		public IEnumerable<Cell> GetCells()
		{
			return rows.SelectMany(x => x);
		}

		public void AddRow()
		{
			List<Cell> row = new List<Cell>();
			List<(int Id, Color Color)> c = new List<(int Id, Color Color)>();
			for (int i = 0; i < ColumnsCount; i++)
			{
				row.Add(new Cell(indexY: RowsCount, indexX: i, this));
				c.Add((Id: 0, Color: Color.White));
			}
			rows.Add(row);
			copy.Add(c);
		}

		public void AddColumn()
		{
			int indexX = ColumnsCount;
			for (int i = 0; i < RowsCount; i++)
			{
				rows[i].Add(new Cell(indexY: i, indexX, this));
				copy[i].Add((Id: 0, Color: Color.White));
			}
		}

		public void RemoveLastRow()
		{
			if (RowsCount == 1)
				throw new Exception("Last row");
			rows.RemoveAt(RowsCount - 1);
			copy.RemoveAt(RowsCount - 1);
		}

		public void RemoveLastColumn()
		{
			if (ColumnsCount == 1)
				throw new Exception("Last column");
			int indexX = ColumnsCount;
			for (int i = 0; i < RowsCount; i++)
			{
				rows[i].RemoveAt(indexX - 1);
				copy[i].RemoveAt(indexX - 1);
			}
		}
		public void Rearange(int targetSizeX, int targetSizeY)
		{
			if (targetSizeX < 1 || targetSizeY < 1)
				return;
			while (RowsCount > targetSizeY)
				RemoveLastRow();
			while (RowsCount < targetSizeY)
				AddRow();

			while (ColumnsCount > targetSizeX)
				RemoveLastColumn();
			while (ColumnsCount < targetSizeX)
				AddColumn();

			SetNeighborsAbsorbing("moor");
		}
		public void Erase()
		{
			for(int i = 0; i < rows.Count; i++)
			{
				for(int j = 0; j < rows[i].Count; j++)
				{
					rows[i][j].Id = 0;
					rows[i][j].Color = Color.White;
					copy[i][j] = (Id: 0, Color: Color.White);
				}
			}
		}
		public void SetCellSize(double cellSize)
		{
			CellSize = cellSize;
		}
		public void SetRandomCells(int number)
		{
			int cellNumber = RowsCount * ColumnsCount;
			number = number < cellNumber
							? number
							: cellNumber;
			int i = 1;
			while (i <= number)
			{
				int yIndex = Random.Next(RowsCount);
				int xIndex = Random.Next(ColumnsCount);

				Cell cell = GetCell(yIndex, xIndex);
				if (cell.Id == 0)
				{
					cell.Id = i;
					cell.Color = Color.FromArgb(Random.Next(255), Random.Next(255), Random.Next(255));
					copy[yIndex][xIndex] = (cell.Id, cell.Color);
					i++;
				}
			}
		}

		public void SetNeighborsAbsorbing(string selectedNeighborhoodPattern)
		{
			int sizeY = rows.Count;
			int sizeX;

			for (int i = 0; i < RowsCount; i++)     // rows
			{
				sizeX = rows[i].Count;
				for (int j = 0; j < ColumnsCount; j++) // columns
				{
					Cell cell = GetCell(i, j);

					// cell -> current cell
					// i+/- and j+/- -> cell's potential heighbor

					// Top left
					if (Neighbourhood.Patterns[selectedNeighborhoodPattern][0] == 1 && i + 1 < sizeY && j + 1 < sizeX)
						cell.NeighboringCells[0] = rows[i + 1][j + 1];
					else
						cell.NeighboringCells[0] = null;

					// Top
					if (Neighbourhood.Patterns[selectedNeighborhoodPattern][1] == 1 && i + 1 < sizeY)
						cell.NeighboringCells[1] = rows[i + 1][j];
					else
						cell.NeighboringCells[1] = null;

					// Top right
					if (Neighbourhood.Patterns[selectedNeighborhoodPattern][2] == 1 && i + 1 < sizeY && j - 1 >= 0)
						cell.NeighboringCells[2] = rows[i + 1][j - 1];
					else
						cell.NeighboringCells[2] = null;

					// Right
					if (Neighbourhood.Patterns[selectedNeighborhoodPattern][3] == 1 && j - 1 >= 0)
						cell.NeighboringCells[3] = rows[i][j - 1];
					else
						cell.NeighboringCells[3] = null;

					// Bottom right
					if (Neighbourhood.Patterns[selectedNeighborhoodPattern][4] == 1 && i - 1 >= 0 && j - 1 >= 0)
						cell.NeighboringCells[4] = rows[i - 1][j - 1];
					else
						cell.NeighboringCells[4] = null;

					// Bottom
					if (Neighbourhood.Patterns[selectedNeighborhoodPattern][5] == 1 && i - 1 >= 0)
						cell.NeighboringCells[5] = rows[i - 1][j];
					else
						cell.NeighboringCells[5] = null;

					// Bottom left
					if (Neighbourhood.Patterns[selectedNeighborhoodPattern][6] == 1 && i - 1 >= 0 && j + 1 < sizeX)
						cell.NeighboringCells[6] = rows[i - 1][j + 1];
					else
						cell.NeighboringCells[6] = null;

					// Left
					if (Neighbourhood.Patterns[selectedNeighborhoodPattern][7] == 1 && j + 1 < sizeX)
						cell.NeighboringCells[7] = rows[i][j + 1];
					else
						cell.NeighboringCells[7] = null;
				}
			}
		}
		//private List<List<int>> CheckNeighbourhoodPeriodic()
		//{
		//	int sizeY = Matrix.Count;
		//	int sizeX;
		//	int[] type = new int[] { 0, 0, 0, 0 };

		//	List<List<int>> neighbours = new List<List<int>>();

		//	for (int i = 0; i < sizeY; i++)
		//	{
		//		sizeX = Matrix[0].Count;
		//		neighbours.Add(new List<int>());
		//		for (int j = 0; j < sizeX; j++)
		//			neighbours[i].Add(0);
		//	}


		//	for (int i = 0; i < sizeY; i++)
		//	{
		//		sizeX = Matrix[0].Count;
		//		for (int j = 0; j < sizeX; j++)
		//		{
		//			type = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };

		//			if (NeighbourhoodTypes[SelectedNeighbourhoodType][0] == 1)
		//			{
		//				if (i + 1 < sizeY && j + 1 < sizeX)
		//					type[0] = Matrix[i + 1][j + 1].Id;
		//				else if (i + 1 < sizeY && j + 1 >= sizeX)
		//					type[0] = Matrix[i + 1][0].Id;
		//				else if (i + 1 >= sizeY && j + 1 < sizeX)
		//					type[0] = Matrix[0][j + 1].Id;
		//				else
		//					type[0] = Matrix[0][0].Id;
		//			}
		//			if (NeighbourhoodTypes[SelectedNeighbourhoodType][1] == 1)
		//			{
		//				if (i + 1 < sizeY)
		//					type[1] = Matrix[i + 1][j].Id;
		//				else
		//					type[1] = Matrix[0][j].Id;
		//			}
		//			if (NeighbourhoodTypes[SelectedNeighbourhoodType][2] == 1)
		//			{
		//				if (i + 1 < sizeY && j - 1 >= 0)
		//					type[2] = Matrix[i + 1][j - 1].Id;
		//				else if (i + 1 < sizeY && j - 1 < 0)
		//					type[2] = Matrix[i + 1][sizeX - 1].Id;
		//				else if (i + 1 >= sizeY && j - 1 >= 0)
		//					type[2] = Matrix[0][j - 1].Id;
		//				else
		//					type[2] = Matrix[0][sizeX - 1].Id;
		//			}
		//			if (NeighbourhoodTypes[SelectedNeighbourhoodType][3] == 1)
		//			{
		//				if (j - 1 >= 0)
		//					type[3] = Matrix[i][j - 1].Id;
		//				else
		//					type[3] = Matrix[i][sizeX - 1].Id;
		//			}
		//			if (NeighbourhoodTypes[SelectedNeighbourhoodType][4] == 1)
		//			{
		//				if (i - 1 >= 0 && j - 1 >= 0)
		//					type[4] = Matrix[i - 1][j - 1].Id;
		//				else if (i - 1 >= 0 && j - 1 < 0)
		//					type[4] = Matrix[i - 1][sizeX - 1].Id;
		//				else if (i - 1 < 0 && j - 1 >= 0)
		//					type[4] = Matrix[sizeY - 1][j - 1].Id;
		//				else
		//					type[4] = Matrix[sizeY - 1][sizeX - 1].Id;
		//			}
		//			if (NeighbourhoodTypes[SelectedNeighbourhoodType][5] == 1)
		//			{
		//				if (i - 1 >= 0)
		//					type[5] = Matrix[i - 1][j].Id;
		//				else
		//					type[5] = Matrix[sizeY - 1][j].Id;
		//			}
		//			if (NeighbourhoodTypes[SelectedNeighbourhoodType][6] == 1)
		//			{
		//				if (i - 1 >= 0 && j + 1 < sizeX)
		//					type[6] = Matrix[i - 1][j + 1].Id;
		//				else if (i - 1 >= 0 && j + 1 >= sizeX)
		//					type[6] = Matrix[i - 1][0].Id;
		//				else if (i - 1 < 0 && j + 1 < sizeX)
		//					type[6] = Matrix[sizeY - 1][j + 1].Id;
		//				else
		//					type[6] = Matrix[sizeY - 1][0].Id;
		//			}
		//			if (NeighbourhoodTypes[SelectedNeighbourhoodType][7] == 1)
		//			{
		//				if (j + 1 < sizeX)
		//					type[7] = Matrix[i][j + 1].Id;
		//				else
		//					type[7] = Matrix[i][0].Id;
		//			}
		//			neighbours[i][j] = GetMostRepeatedElenent(type);
		//		}
		//	}
		//	return neighbours;
		//}
		public void CalculateNextGeneration()
		{
			Parallel.For(0, RowsCount, i =>
			{
				Parallel.For(0, ColumnsCount, j =>
				{
					if (GetCell(i, j).Id != 0)
					{
						copy[i][j] = (GetCell(i, j).Id, GetCell(i, j).Color);
					}
					else
					{
						Cell cell = GetMostCommonCell(GetCell(i, j).NeighboringCells);
						copy[i][j] = (cell.Id, cell.Color);
					}
				});
			});

			Parallel.For(0, RowsCount, i =>
			{
				Parallel.For(0, ColumnsCount, j =>
				{
					GetCell(i, j).Id = copy[i][j].Id;
					GetCell(i, j).Color = copy[i][j].Color;
				});
			});
		}
		private Cell GetMostCommonCell(IEnumerable<Cell> cells)
		{
			var notNullCells = cells.Where(c => c is Cell);
			Cell cell = notNullCells.First();
			int count = 0;
			foreach(Cell c in notNullCells)
			{
				var foundCells = notNullCells.Where(x => x.Id != 0 && x.Id == c.Id);
				if(foundCells.Count() > count)
				{
					count = foundCells.Count();
					cell = foundCells.First();
				}
			}
			return cell;
		}
	}
}
