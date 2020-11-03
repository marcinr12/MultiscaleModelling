﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Convert;

namespace MultiscaleModelling
{
	public class Matrix
	{
		private readonly List<List<Cell>> rows;
		private readonly List<List<(int Id, Color Color)>> copy;
		public int RowsCount => rows.Count;
		public int ColumnsCount => rows.ElementAtOrDefault(0)?.Count ?? 0;
		public double CellSize { get; private set; }
		private Bc _boundaryContition;
		public Bc BoundaryCondition
		{
			get => _boundaryContition;
			set 
			{
				_boundaryContition = value;
				SetBoundaryCondition();
			}
		}

		public Matrix()
		{
			rows = new List<List<Cell>>() { new List<Cell>() { new Cell(0, 0, this) } };
			copy = new List<List<(int Id, Color Color)>>() { new List<(int Id, Color Color)>() { (Id: 1, Color: Color.White) } };

			BoundaryCondition = Bc.Absorbing;
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
				row.Add(new Cell(indexY: RowsCount, indexX: i, matrix: this));
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
				rows[i].Add(new Cell(indexY: i, indexX: indexX, matrix: this));
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

			SetBoundaryCondition();
		}
		private void SetBoundaryCondition()
		{
			if (BoundaryCondition == Bc.Absorbing)
				SetNeighborsAbsorbing("moor");
			else if (BoundaryCondition == Bc.Periodic)
				SetNeighborsPeriodic("moor");
			else
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
				int yIndex = RandomMachine.Random.Next(RowsCount);
				int xIndex = RandomMachine.Random.Next(ColumnsCount);

				Cell cell = GetCell(yIndex, xIndex);
				if (cell.Id == 0)
				{
					cell.Id = i;
					cell.Color = Color.FromArgb(RandomMachine.Random.Next(255), RandomMachine.Random.Next(255), RandomMachine.Random.Next(255));
					copy[yIndex][xIndex] = (cell.Id, cell.Color);
					i++;
				}
			}
		}
		private void SetNeighborsAbsorbing(string selectedNeighborhoodPattern)
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
		private void SetNeighborsPeriodic(string selectedNeighborhoodPattern)
		{
			int sizeY = rows.Count;
			int sizeX;

			for (int i = 0; i < sizeY; i++)
			{
				sizeX = rows[i].Count;
				for (int j = 0; j < sizeX; j++)
				{
					Cell cell = GetCell(i, j);

					// cell -> current cell
					// i+/- and j+/- -> cell's potential heighbor

					if (Neighbourhood.Patterns[selectedNeighborhoodPattern][0] == 1)
					{
						if (i + 1 < sizeY && j + 1 < sizeX)
							cell.NeighboringCells[0] = rows[i + 1][j + 1];
						else if (i + 1 < sizeY && j + 1 >= sizeX)
							cell.NeighboringCells[0] = rows[i + 1][0];
						else if (i + 1 >= sizeY && j + 1 < sizeX)
							cell.NeighboringCells[0] = rows[0][j + 1];
						else
							cell.NeighboringCells[0] = rows[0][0];
					}
					else
						cell.NeighboringCells[0] = null;

					if (Neighbourhood.Patterns[selectedNeighborhoodPattern][1] == 1)
					{
						if (i + 1 < sizeY)
							cell.NeighboringCells[1] = rows[i + 1][j];
						else
							cell.NeighboringCells[1] = rows[0][j];
					}
					else
						cell.NeighboringCells[1] = null;

					if (Neighbourhood.Patterns[selectedNeighborhoodPattern][2] == 1)
					{
						if (i + 1 < sizeY && j - 1 >= 0)
							cell.NeighboringCells[2] = rows[i + 1][j - 1];
						else if (i + 1 < sizeY && j - 1 < 0)
							cell.NeighboringCells[2] = rows[i + 1][sizeX - 1];
						else if (i + 1 >= sizeY && j - 1 >= 0)
							cell.NeighboringCells[2] = rows[0][j - 1];
						else
							cell.NeighboringCells[2] = rows[0][sizeX - 1];
					}
					else
						cell.NeighboringCells[2] = null;

					if (Neighbourhood.Patterns[selectedNeighborhoodPattern][3] == 1)
					{
						if (j - 1 >= 0)
							cell.NeighboringCells[3] = rows[i][j - 1];
						else
							cell.NeighboringCells[3] = rows[i][sizeX - 1];
					}
					else
						cell.NeighboringCells[3] = null;

					if (Neighbourhood.Patterns[selectedNeighborhoodPattern][4] == 1)
					{
						if (i - 1 >= 0 && j - 1 >= 0)
							cell.NeighboringCells[4] = rows[i - 1][j - 1];
						else if (i - 1 >= 0 && j - 1 < 0)
							cell.NeighboringCells[4] = rows[i - 1][sizeX - 1];
						else if (i - 1 < 0 && j - 1 >= 0)
							cell.NeighboringCells[4] = rows[sizeY - 1][j - 1];
						else
							cell.NeighboringCells[4] = rows[sizeY - 1][sizeX - 1];
					}
					else
						cell.NeighboringCells[4] = null;

					if (Neighbourhood.Patterns[selectedNeighborhoodPattern][5] == 1)
					{
						if (i - 1 >= 0)
							cell.NeighboringCells[5] = rows[i - 1][j];
						else
							cell.NeighboringCells[5] = rows[sizeY - 1][j];
					}
					else
						cell.NeighboringCells[5] = null;

					if (Neighbourhood.Patterns[selectedNeighborhoodPattern][6] == 1)
					{
						if (i - 1 >= 0 && j + 1 < sizeX)
							cell.NeighboringCells[6] = rows[i - 1][j + 1];
						else if (i - 1 >= 0 && j + 1 >= sizeX)
							cell.NeighboringCells[6] = rows[i - 1][0];
						else if (i - 1 < 0 && j + 1 < sizeX)
							cell.NeighboringCells[6] = rows[sizeY - 1][j + 1];
						else
							cell.NeighboringCells[6] = rows[sizeY - 1][0];
					}
					else
						cell.NeighboringCells[6] = null;

					if (Neighbourhood.Patterns[selectedNeighborhoodPattern][7] == 1)
					{
						if (j + 1 < sizeX)
							cell.NeighboringCells[7] = rows[i][j + 1];
						else
							cell.NeighboringCells[7] = rows[i][0];
					}
					else
						cell.NeighboringCells[7] = null;
				}
			}
		}

		readonly Stopwatch sw = new Stopwatch();
		public List<long> times = new List<long>();
		public void CalculateNextGeneration()
		{
			times.Clear();
			sw.Restart();
			Parallel.For(0, RowsCount, i =>
			{
				Parallel.For(0, ColumnsCount, j =>
				{
					if (GetCell(i, j).Id == 0)
					{
						Cell cell = GetMostCommonCell(GetCell(i, j).NeighboringCells);
						copy[i][j] = (cell.Id, cell.Color);
					}
					else
					{
						copy[i][j] = (GetCell(i, j).Id, GetCell(i, j).Color);
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
			times.Add(sw.ElapsedMilliseconds);
			Trace.WriteLine($"Iteration took: {times.Last()}ms");
		}
		private Cell GetMostCommonCell(IEnumerable<Cell> cells)
		{
			IEnumerable<Cell> notNullCells = cells.Where(c => c?.Id >= 0);
			Cell cell = notNullCells.First();

			int count = 0;
			foreach(Cell c in notNullCells)
			{
				IEnumerable<Cell> foundCells = notNullCells.Where(x => x.Id > 0 && x.Id == c.Id);
				if(foundCells.Count() > count)
				{
					count = foundCells.Count();
					cell = foundCells.First();
				}
			}
			return cell;
		}
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach(List<Cell> row in rows)
				foreach(Cell cell in row)
					stringBuilder.Append($"{cell.IndexX} {cell.IndexY} {cell.Phase} {cell.Id}\n");
			return stringBuilder.ToString();
		}
		public Bitmap ToBitmap()
		{
			Bitmap bitmap = new Bitmap(ColumnsCount * 10, RowsCount * 10);
			Graphics graphics = Graphics.FromImage(bitmap);

			for (int i = 0; i < rows.Count; i++)
				for (int j = 0; j < rows[i].Count; j++)
					graphics.FillRectangle(new SolidBrush(rows[i][j].Color), j * 10, i * 10, 10, 10);
			return bitmap;
		}
		public void PreAddInclusions(int number, int radius)
		{
			List<Point> points = GetRandomInslusionsCentres(number, radius);

			if (points.Count < number)
				Trace.WriteLine("Unable to set all inclusions");

			Parallel.For(0, points.Count, i =>
				{
					SetInclusion(rows[points[i].Y][points[i].X], radius);
				});
		}
		public void PostAddInclusions(int number, int radius)
		{
			if (BoundaryCondition == Bc.Absorbing)
			{

			}
			else if (BoundaryCondition == Bc.Periodic)
			{

			}
		}
		public void SetInclusion(Cell start, double radius)
		{
			int r = ToInt32(Math.Ceiling(radius));
			IEnumerable<int> xIndexes = new List<int>();
			IEnumerable<int> yIndexes = new List<int>();
			if (BoundaryCondition == Bc.Absorbing)
			{
				int xMin = Math.Max(start.IndexX - r, 0);
				int xMax = Math.Min(start.IndexX + r, ColumnsCount - 1);

				int yMin = Math.Max(start.IndexY - r, 0);
				int yMax = Math.Min(start.IndexY + r, RowsCount - 1);

				xIndexes = Enumerable.Range(xMin, xMax - xMin + 1);
				yIndexes = Enumerable.Range(yMin, yMax - yMin + 1);

			}
			else if (BoundaryCondition == Bc.Periodic)
			{
				int xMin = Math.Max(start.IndexX - r, 0);
				int xMax = Math.Min(start.IndexX + r, ColumnsCount - 1);

				int xOverflow = Math.Min(ColumnsCount - start.IndexX - r - 1, 0);       // sign "-" if found
				int xLack = Math.Min(start.IndexX - r, 0);                              // sign "-" if found


				int yMin = Math.Max(start.IndexY - r, 0);
				int yMax = Math.Min(start.IndexY + r, RowsCount - 1);

				int yOverflow = Math.Min(RowsCount - start.IndexY - r - 1, 0);          // sign "-" if found
				int yLack = Math.Min(start.IndexY - r, 0);                              // sign "-" if found


				xIndexes = Enumerable.Range(xMin, xMax - xMin + 1).Concat(Enumerable.Range(0, Math.Abs(xOverflow))).Concat(Enumerable.Range(ColumnsCount - Math.Abs(xLack), Math.Abs(xLack)));
				yIndexes = Enumerable.Range(yMin, yMax - yMin + 1).Concat(Enumerable.Range(0, Math.Abs(yOverflow))).Concat(Enumerable.Range(RowsCount - Math.Abs(yLack), Math.Abs(yLack)));
			}

			Parallel.ForEach(yIndexes, i =>
			{
				Parallel.ForEach(xIndexes, j =>
				{
					if (IsInRadius(start, rows[i][j], radius))
					{
						rows[i][j].Color = Color.Black;
						rows[i][j].Id = -1;
					}
				});
			});
		}
		public List<Point> GetRandomInslusionsCentres(int number, double radius)
		{
			List<Point> points = new List<Point>();
			int failed = 0;
			while (points.Count < number)
			{
				if (failed > 10_000)
					break;

				bool isInRadius = false;
				int rowIndex = RandomMachine.Random.Next(RowsCount);
				int columnIndex = RandomMachine.Random.Next(ColumnsCount);
				for (int i = 0; i < points.Count; i++)
					if (IsInRadius(points[i].X, points[i].Y, columnIndex, rowIndex, 2 * radius))
					{
						isInRadius = true;
						failed++;
						break;
					}

				if (!isInRadius)
				{
					points.Add(new Point(columnIndex, rowIndex));
					failed = 0;
				}
			}
			return points;
		}
		public bool IsInRadius(Cell c1, Cell c2, double radius)
		{
			if (BoundaryCondition == Bc.Absorbing)
				return Math.Pow(c2.IndexX - c1.IndexX, 2) + Math.Pow(c2.IndexY - c1.IndexY, 2) <= radius * radius;
			else if (BoundaryCondition == Bc.Periodic)
			{
				if (Math.Pow(c2.IndexX - c1.IndexX, 2) + Math.Pow(c2.IndexY - c1.IndexY, 2) <= radius * radius)                                     // Absorbing
					return true;
				else if (Math.Pow(Math.Abs(c2.IndexX - c1.IndexX) - ColumnsCount, 2) + Math.Pow(c2.IndexY - c1.IndexY, 2) <= radius * radius)       // <--- / --->
					return true;
				else if (Math.Pow(c2.IndexX - c1.IndexX, 2) + Math.Pow(Math.Abs(c2.IndexY - c1.IndexY) - RowsCount, 2) <= radius * radius)          // ^ / v
					return true;
				else if (Math.Pow(Math.Abs(c2.IndexX - c1.IndexX) - ColumnsCount, 2) + Math.Pow(Math.Abs(c2.IndexY - c1.IndexY) - RowsCount, 2) <= radius * radius) // <--- / ---> & ^ / v
					return true;
				else
					return false;
			}
			else
				throw new Exception();
		}
		public bool IsInRadius(int x1, int y1, int x2, int y2, double radius)
		{
			if (BoundaryCondition == Bc.Absorbing)
				return Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2) <= radius * radius;
			else if (BoundaryCondition == Bc.Periodic)
			{
				if (Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2) <= radius * radius)                                     // Absorbing
					return true;
				else if (Math.Pow(Math.Abs(x2 - x1) - ColumnsCount, 2) + Math.Pow(y2 - y1, 2) <= radius * radius)       // <--- / --->
					return true;
				else if (Math.Pow(x2 - x1, 2) + Math.Pow(Math.Abs(y2- y1) - RowsCount, 2) <= radius * radius)          // ^ / v
					return true;
				else if (Math.Pow(Math.Abs(x2 - x1) - ColumnsCount, 2) + Math.Pow(Math.Abs(y2 - y1) - RowsCount, 2) <= radius * radius) // <--- / ---> & ^ / v
					return true;
				else
					return false;
			}
			else
				throw new Exception();
		}
		public bool IsInRadius(double x1, double y1, double x2, double y2, double radius)
		{
			if (BoundaryCondition == Bc.Absorbing)
				return Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2) <= radius * radius;
			else if (BoundaryCondition == Bc.Periodic)
			{
				if (Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2) <= radius * radius)                                     // Absorbing
					return true;
				else if (Math.Pow(Math.Abs(x2 - x1) - ColumnsCount, 2) + Math.Pow(y2 - y1, 2) <= radius * radius)       // <--- / --->
					return true;
				else if (Math.Pow(x2 - x1, 2) + Math.Pow(Math.Abs(y2 - y1) - RowsCount, 2) <= radius * radius)          // ^ / v
					return true;
				else if (Math.Pow(Math.Abs(x2 - x1) - ColumnsCount, 2) + Math.Pow(Math.Abs(y2 - y1) - RowsCount, 2) <= radius * radius) // <--- / ---> & ^ / v
					return true;
				else
					return false;
			}
			else
				throw new Exception();
		}
	}
}
