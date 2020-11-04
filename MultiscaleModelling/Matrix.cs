using System;
using System.Collections.Concurrent;
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
		private readonly Dictionary<long, Cell> PotentialGrains = new Dictionary<long, Cell>();

		public int RowsCount => rows.Count;
		public int ColumnsCount => rows.ElementAtOrDefault(0)?.Count ?? 0;
		public float CellSize { get; private set; }
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
		}
		public void AddColumn()
		{
			int indexX = ColumnsCount;
			for (int i = 0; i < RowsCount; i++)
			{
				rows[i].Add(new Cell(indexY: i, indexX: indexX, matrix: this));
			}
		}
		public void RemoveLastRow()
		{
			if (RowsCount == 1)
				throw new Exception("Last row");
			rows.RemoveAt(RowsCount - 1);
		}
		public void RemoveLastColumn()
		{
			if (ColumnsCount == 1)
				throw new Exception("Last column");
			int indexX = ColumnsCount;
			for (int i = 0; i < RowsCount; i++)
			{
				rows[i].RemoveAt(indexX - 1);
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
			for (int i = 0; i < rows.Count; i++)
			{
				for (int j = 0; j < rows[i].Count; j++)
				{
					rows[i][j].SetId(0);
					rows[i][j].SetColor(Color.White);
				}
			}
		}
		public void SetCellSize(float cellSize)
		{
			CellSize = cellSize;
		}
		public void SetRandomCells(int number)
		{
			int attempts = 0;
			int i = 1;
			while (i <= number && attempts < 100_000)
			{
				int yIndex = RandomMachine.Next(RowsCount);
				int xIndex = RandomMachine.Next(ColumnsCount);

				Cell cell = GetCell(yIndex, xIndex);
				if (cell.Id == 0)
				{
					cell.SetId(i);
					cell.SetColor(Color.FromArgb(RandomMachine.Next(255), RandomMachine.Next(255), RandomMachine.Next(255)));
					i++;
					attempts = 0;
				}
				attempts++;
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
		public void TryAddPotentialGrains(IEnumerable<Cell> cells)
		{
			foreach (Cell n in cells.Where(c => c is Cell && c.NewId == 0))
				PotentialGrains.TryAdd(n.Identifier, n);
		}
		public void InitialCalculations()
		{
			PotentialGrains.Clear();
			for (int i = 0; i < rows.Count; i++)
			{
				for (int j = 0; j < rows[i].Count; j++)
				{
					if (rows[i][j].Id > 0)
						TryAddPotentialGrains(rows[i][j].NeighboringCells);
				}
			}
		}

		readonly Stopwatch sw = new Stopwatch();
		public LinkedList<Cell> CalculateNextGeneration()
		{
			sw.Restart();

			ConcurrentQueue<Cell> newColored = new ConcurrentQueue<Cell>();
			Parallel.ForEach(PotentialGrains.Keys, i =>
			{
				Cell cell = PotentialGrains[i];
				Cell mostCommonCell = GetMostCommonCell(cell.NeighboringCells);
				cell.NewId = mostCommonCell.Id;
				cell.NewColor = mostCommonCell.Color;
				if (cell.NewId > 0)
					newColored.Enqueue(cell);
			});

			LinkedList<Cell> toReturn = new LinkedList<Cell>();
			while (newColored.Count > 0)
			{
				if (newColored.TryDequeue(out Cell c))
				{
					c.UpdateId();
					TryAddPotentialGrains(c.NeighboringCells);

					PotentialGrains.Remove(c.Identifier);
					toReturn.AddLast(c);
				}
			}
			return toReturn;
			//Trace.WriteLine($"Iteration took: {sw.ElapsedMilliseconds}ms");
		}
		private Cell GetMostCommonCell(IEnumerable<Cell> cells)
		{
			IEnumerable<Cell> notNullCells = cells.Where(c => c?.Id >= 0);
			Cell cell = notNullCells.First();

			// WITHOUT RANDOM
			//int count = 0;
			//foreach(Cell c in notNullCells)
			//{
			//	IEnumerable<Cell> foundCells = notNullCells.Where(x => x.Id > 0 && x.Id == c.Id);
			//	if(foundCells.Count() > count)
			//	{
			//		count = foundCells.Count();
			//		cell = foundCells.First();
			//	}
			//}

			IEnumerable<IGrouping<int, Cell>> groups = notNullCells.Where(x => x.Id > 0).GroupBy(c => c.Id).OrderByDescending(x => x.Count());
			if (groups.Count() > 0)
			{
				IEnumerable<IGrouping<int, Cell>> max = groups.Where(x => x.Count() == groups.First().Count());
				cell = max.ElementAt(RandomMachine.Next(max.Count())).First();
			}

			return cell;
		}
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (List<Cell> row in rows)
				foreach (Cell cell in row)
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
		public void AddInclusions(int number, int radius, InclusionsType inclusionsType)
		{
			List<Point> points = GetRandomInslusionsCentres(number, radius, inclusionsType);

			if (points.Count < number)
				Trace.WriteLine("Unable to set all inclusions");

			Parallel.For(0, points.Count, i =>
			{
				SetInclusion(rows[points[i].Y][points[i].X], radius);
			});
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
					if (IsInRadius(start.IndexX, start.IndexY, rows[i][j].IndexX, rows[i][j].IndexY, radius))
					{
						rows[i][j].SetColor(Color.Black);
						rows[i][j].SetId(-1);
					}
				});
			});
		}
		public List<Point> GetRandomInslusionsCentres(int number, double radius, InclusionsType inclusionsType)
		{
			List<Point> points = new List<Point>();
			int attempts = 0;
			while (points.Count < number)
			{
				if (attempts > 10_000)
					break;

				bool isFailed = false;
				int rowIndex = RandomMachine.Next(RowsCount);
				int columnIndex = RandomMachine.Next(ColumnsCount);


				if (rows[rowIndex][columnIndex].Id == -1)
				{
					isFailed = true;
					attempts++;
				}
				else if (inclusionsType == InclusionsType.OnBorder
					&& rows[rowIndex][columnIndex].NeighboringCells.Where(c => c is Cell && c?.Id != -1 && c?.Id != 0).Select(c => c.Id).Distinct().Count() < 2)
				{
					isFailed = true;
					attempts++;
				}
				for (int i = 0; i < points.Count && !isFailed; i++)
				{
					if (IsInRadius(points[i].X, points[i].Y, columnIndex, rowIndex, 2 * radius))
					{
						isFailed = true;
						attempts++;
					}
				}

				if (!isFailed)
				{
					points.Add(new Point(columnIndex, rowIndex));
					attempts = 0;
				}
			}
			return points;
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
