using MultiscaleModelling.Extensions;
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
		private readonly List<List<Cell>> _rows;
		private readonly Dictionary<long, Cell> _potentialGrains = new Dictionary<long, Cell>();

		// grain id, Cells
		private List<IGrouping<int, Cell>> _firstPhaseGrains;
		// grain id, cell identifier, cell
		private readonly Dictionary<int, Dictionary<long, Cell>> _secondGrowthPotentialGrains = new Dictionary<int, Dictionary<long, Cell>>();

		public int RowsCount => _rows.Count;
		public int ColumnsCount => _rows.ElementAtOrDefault(0)?.Count ?? 0;
		public float CellSize { get; private set; }
		public Cell SelectedCell { get; set; }

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
			_rows = new List<List<Cell>>() { new List<Cell>() { new Cell(0, 0, this) } };
			BoundaryCondition = Bc.Absorbing;
		}
		public Cell GetCell(int row, int column)
		{
			return _rows[row][column];
		}
		public IEnumerable<Cell> GetCells()
		{
			return _rows.SelectMany(x => x);
		}
		public void AddRow()
		{
			List<Cell> row = new List<Cell>();
			List<(int Id, Color Color)> c = new List<(int Id, Color Color)>();
			for (int i = 0; i < ColumnsCount; i++)
			{
				row.Add(new Cell(indexY: RowsCount, indexX: i, matrix: this));
				c.Add((Id: 0, Color: Cell.EmptySpaceColor.ToColor()));
			}
			_rows.Add(row);
		}
		public void AddColumn()
		{
			int indexX = ColumnsCount;
			for (int i = 0; i < RowsCount; i++)
			{
				_rows[i].Add(new Cell(indexY: i, indexX: indexX, matrix: this));
			}
		}
		public void RemoveLastRow()
		{
			if (RowsCount == 1)
				throw new Exception("Last row");
			_rows.RemoveAt(RowsCount - 1);
		}
		public void RemoveLastColumn()
		{
			if (ColumnsCount == 1)
				throw new Exception("Last column");
			int indexX = ColumnsCount;
			for (int i = 0; i < RowsCount; i++)
			{
				_rows[i].RemoveAt(indexX - 1);
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
				SetNeighborsAbsorbing();
			else if (BoundaryCondition == Bc.Periodic)
				SetNeighborsPeriodic();
			else
				SetNeighborsAbsorbing();
		}
		public void Erase()
		{
			for (int i = 0; i < _rows.Count; i++)
			{
				for (int j = 0; j < _rows[i].Count; j++)
				{
					_rows[i][j].SetId(0);
					_rows[i][j].SetColor(Cell.EmptySpaceColor.ToColor());
				}
			}
		}
		public void EraseFirstPhase()
		{
			Parallel.ForEach(_rows.SelectMany(x => x), cell =>
			{
				if (cell.Phase == 0)
				{
					cell.SetId(0);
					cell.SetColor(Cell.EmptySpaceColor.ToColor());
				}
			});
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
					cell.SetColor(Color.FromArgb(RandomMachine.Next(1, 255), RandomMachine.Next(1, 255), RandomMachine.Next(1, 255)));
					i++;
					attempts = 0;
				}
				attempts++;
			}
		}
		public void AssignFirstPhaseGrains()
		{
			_firstPhaseGrains = _rows.SelectMany(x => x).Where(c => c.Phase == 0).GroupBy(x => x.Id).ToList();
		}
		public void SetRandomCellsSecondGrainGrowth(int number)
		{
			List<int> takenIds = _rows.SelectMany(x => x).Where(c => c.Phase != 0).Select(x => x.Id).Distinct().ToList();
			IEnumerable<int> possibleIds = Enumerable.Range(1, int.MaxValue);

			//Parallel.ForEach(zeroPhaseCells, group =>
			foreach (IGrouping<int, Cell> group in _firstPhaseGrains)
			{

				List<Cell> grain = group.Select(x => x).ToList();
				grain.ForEach(c =>
				{
					c.SetColor(Cell.EmptySpaceColor.ToColor());
					c.SetId(0);
				});

				int attempts = 0;
				int i = 1;
				while (i <= number && attempts < 100_000)
				{
					Cell cell = grain.ElementAt(RandomMachine.Next(grain.Count));
					if (cell.Id == 0)
					{
						int id = possibleIds.Except(takenIds).FirstOrDefault();
						takenIds.Add(id);
						cell.SetId(id);
						cell.SetColor(Color.FromArgb(RandomMachine.Next(1, 255), RandomMachine.Next(1, 255), RandomMachine.Next(1, 255)));

						i++;
						attempts = 0;
					}
					attempts++;
				}
			}
		}
		private void SetNeighborsAbsorbing()
		{
			int sizeY = _rows.Count;
			int sizeX;

			for (int i = 0; i < RowsCount; i++)     // rows
			{
				sizeX = _rows[i].Count;
				for (int j = 0; j < ColumnsCount; j++) // columns
				{
					Cell cell = GetCell(i, j);

					// cell -> current cell
					// i+/- and j+/- -> cell's potential heighbor

					// Top left
					if (i + 1 < sizeY && j + 1 < sizeX)
						cell.NeighboringCells[0] = _rows[i + 1][j + 1];
					else
						cell.NeighboringCells[0] = null;

					// Top
					if (i + 1 < sizeY)
						cell.NeighboringCells[1] = _rows[i + 1][j];
					else
						cell.NeighboringCells[1] = null;

					// Top right
					if (i + 1 < sizeY && j - 1 >= 0)
						cell.NeighboringCells[2] = _rows[i + 1][j - 1];
					else
						cell.NeighboringCells[2] = null;

					// Right
					if (j - 1 >= 0)
						cell.NeighboringCells[3] = _rows[i][j - 1];
					else
						cell.NeighboringCells[3] = null;

					// Bottom right
					if (i - 1 >= 0 && j - 1 >= 0)
						cell.NeighboringCells[4] = _rows[i - 1][j - 1];
					else
						cell.NeighboringCells[4] = null;

					// Bottom
					if (i - 1 >= 0)
						cell.NeighboringCells[5] = _rows[i - 1][j];
					else
						cell.NeighboringCells[5] = null;

					// Bottom left
					if (i - 1 >= 0 && j + 1 < sizeX)
						cell.NeighboringCells[6] = _rows[i - 1][j + 1];
					else
						cell.NeighboringCells[6] = null;

					// Left
					if (j + 1 < sizeX)
						cell.NeighboringCells[7] = _rows[i][j + 1];
					else
						cell.NeighboringCells[7] = null;
				}
			}
		}
		private void SetNeighborsPeriodic()
		{
			int sizeY = _rows.Count;
			int sizeX;

			for (int i = 0; i < sizeY; i++)
			{
				sizeX = _rows[i].Count;
				for (int j = 0; j < sizeX; j++)
				{
					Cell cell = GetCell(i, j);

					// cell -> current cell
					// i+/- and j+/- -> cell's potential heighbor

					if (i + 1 < sizeY && j + 1 < sizeX)
						cell.NeighboringCells[0] = _rows[i + 1][j + 1];
					else if (i + 1 < sizeY && j + 1 >= sizeX)
						cell.NeighboringCells[0] = _rows[i + 1][0];
					else if (i + 1 >= sizeY && j + 1 < sizeX)
						cell.NeighboringCells[0] = _rows[0][j + 1];
					else
						cell.NeighboringCells[0] = _rows[0][0];


					if (i + 1 < sizeY)
						cell.NeighboringCells[1] = _rows[i + 1][j];
					else
						cell.NeighboringCells[1] = _rows[0][j];


					if (i + 1 < sizeY && j - 1 >= 0)
						cell.NeighboringCells[2] = _rows[i + 1][j - 1];
					else if (i + 1 < sizeY && j - 1 < 0)
						cell.NeighboringCells[2] = _rows[i + 1][sizeX - 1];
					else if (i + 1 >= sizeY && j - 1 >= 0)
						cell.NeighboringCells[2] = _rows[0][j - 1];
					else
						cell.NeighboringCells[2] = _rows[0][sizeX - 1];


					if (j - 1 >= 0)
						cell.NeighboringCells[3] = _rows[i][j - 1];
					else
						cell.NeighboringCells[3] = _rows[i][sizeX - 1];


					if (i - 1 >= 0 && j - 1 >= 0)
						cell.NeighboringCells[4] = _rows[i - 1][j - 1];
					else if (i - 1 >= 0 && j - 1 < 0)
						cell.NeighboringCells[4] = _rows[i - 1][sizeX - 1];
					else if (i - 1 < 0 && j - 1 >= 0)
						cell.NeighboringCells[4] = _rows[sizeY - 1][j - 1];
					else
						cell.NeighboringCells[4] = _rows[sizeY - 1][sizeX - 1];


					if (i - 1 >= 0)
						cell.NeighboringCells[5] = _rows[i - 1][j];
					else
						cell.NeighboringCells[5] = _rows[sizeY - 1][j];


					if (i - 1 >= 0 && j + 1 < sizeX)
						cell.NeighboringCells[6] = _rows[i - 1][j + 1];
					else if (i - 1 >= 0 && j + 1 >= sizeX)
						cell.NeighboringCells[6] = _rows[i - 1][0];
					else if (i - 1 < 0 && j + 1 < sizeX)
						cell.NeighboringCells[6] = _rows[sizeY - 1][j + 1];
					else
						cell.NeighboringCells[6] = _rows[sizeY - 1][0];


					if (j + 1 < sizeX)
						cell.NeighboringCells[7] = _rows[i][j + 1];
					else
						cell.NeighboringCells[7] = _rows[i][0];
				}
			}
		}
		public void TryAddPotentialGrains(IEnumerable<Cell> cells)
		{
			foreach (Cell n in cells.Where(c => c is Cell && c.NewId == 0))
			{
				_potentialGrains.TryAdd(n.Identifier, n);
			}
		}
		public void TryAddPotentialGrainsSecondGrowth(int grainId, IEnumerable<Cell> neighboringCells)
		{
			foreach (Cell cell in neighboringCells.Where(c => c is Cell && c.Id == 0))
			{
				IGrouping<int, Cell> grain = _firstPhaseGrains.First(g => g.Key == grainId);
				if (grain.Select(c => c.Identifier).Contains(cell.Identifier))
				{
					_secondGrowthPotentialGrains[grainId].TryAdd(cell.Identifier, cell);
				}
			}
		}
		public void InitialCalculations()
		{
			_potentialGrains.Clear();
			for (int i = 0; i < _rows.Count; i++)
			{
				for (int j = 0; j < _rows[i].Count; j++)
				{
					if (_rows[i][j].Id > 0)
					{
						TryAddPotentialGrains(_rows[i][j].NeighboringCells);
					}
				}
			}
		}
		public void InitialSecondGrowthCalculations()
		{
			_secondGrowthPotentialGrains.Clear();
			foreach (IGrouping<int, Cell> grain in _firstPhaseGrains)
			{
				_secondGrowthPotentialGrains.Add(grain.Key, new Dictionary<long, Cell>());
				foreach (Cell cell in grain.ToList())
				{
					if (cell.Id > 0)
					{
						TryAddPotentialGrainsSecondGrowth(grain.Key, cell.NeighboringCells);
					}
				}
			}
		}

		readonly Stopwatch sw = new Stopwatch();
		public LinkedList<Cell> CalculateNextGeneration(bool shapeControl, int probability)
		{
			sw.Restart();

			ConcurrentQueue<Cell> newColored = new ConcurrentQueue<Cell>();
			Parallel.ForEach(_potentialGrains.Keys, i =>
			{
				Cell cell = _potentialGrains[i];

				int id;
				Color color;

				if (!shapeControl)
					GetCellIdAndColorR4(cell, out id, out color);
				else
					GetCellIdAndColorShapeControl(cell, probability, out id, out color);

				cell.NewId = id;
				cell.NewColor = color;
				if (cell.NewId > 0)
					newColored.Enqueue(cell);
			});

			LinkedList<Cell> toReturn = new LinkedList<Cell>();
			while (!newColored.IsEmpty)
			{
				if (newColored.TryDequeue(out Cell c))
				{
					c.UpdateId();
					TryAddPotentialGrains(c.NeighboringCells);

					_potentialGrains.Remove(c.Identifier);
					toReturn.AddLast(c);
				}
			}
			return toReturn;
			//Trace.WriteLine($"Iteration took: {sw.ElapsedMilliseconds}ms");
		}
		Stopwatch stopwatch = new Stopwatch();
		public LinkedList<Cell> CalculateNextGenerationSecondGrowth(bool shapeControl, int probability)
		{
			sw.Restart();
			ConcurrentQueue<Cell> newColored = new ConcurrentQueue<Cell>();
			LinkedList<Cell> toReturn = new LinkedList<Cell>();

			// for each grain
			//Parallel.ForEach(_secondGrowthPotentialGrains.Keys, grainId =>
			//{
			//	// for each cell in grain
			//	Parallel.ForEach(_secondGrowthPotentialGrains[grainId].Keys, cellIdentifier =>
			//	{
			//		Cell cell = _secondGrowthPotentialGrains[grainId][cellIdentifier];

			//		if (cell.Id > 0)
			//		{

			//		}

			//		int id;
			//		Color color;

			//		if (!shapeControl)
			//			GetCellIdAndColorR4(cell, out id, out color, grainId);
			//		else
			//			GetCellIdAndColorShapeControl(cell, r1, r2, r3, r4, probability, out id, out color);

			//		cell.NewId = id;
			//		cell.NewColor = color;
			//		if (cell.NewId > 0)
			//			newColored.Enqueue(cell);
			//	});

			//	while (!newColored.IsEmpty)
			//	{
			//		if (newColored.TryDequeue(out Cell c))
			//		{
			//			c.UpdateId();
			//			TryAddPotentialGrainsSecondGrowth(grainId, c.NeighboringCells);

			//			_secondGrowthPotentialGrains[grainId].Remove(c.Identifier);
			//			toReturn.AddLast(c);
			//		}
			//	}
			//});


			// for each grain
			//foreach (var grainId in _secondGrowthPotentialGrains.Keys)
			//{
			//	// for each cell in grain
			//	foreach (var cellIdentifier in _secondGrowthPotentialGrains[grainId].Keys)
			//	{
			//		Cell cell = _secondGrowthPotentialGrains[grainId][cellIdentifier];

			//		int id;
			//		Color color;

			//		if (!shapeControl)
			//			GetCellIdAndColorR4(cell, out id, out color, grainId);
			//		else
			//			GetCellIdAndColorShapeControl(cell, r1, r2, r3, r4, probability, out id, out color);

			//		cell.NewId = id;
			//		cell.NewColor = color;
			//		if (cell.NewId > 0)
			//			newColored.Enqueue(cell);
			//	}

			//	while (!newColored.IsEmpty)
			//	{
			//		if (newColored.TryDequeue(out Cell c))
			//		{
			//			c.UpdateId();
			//			TryAddPotentialGrainsSecondGrowth(grainId, c.NeighboringCells);

			//			_secondGrowthPotentialGrains[grainId].Remove(c.Identifier);
			//			toReturn.AddFirst(c);
			//		}
			//	}
			//}



			// for each grain
			foreach (var grainId in _secondGrowthPotentialGrains.Keys)
			{
				//sw.Restart();
				// for each cell in grain
				Parallel.ForEach(_secondGrowthPotentialGrains[grainId].Keys, cellIdentifier =>
				{
					Cell cell = _secondGrowthPotentialGrains[grainId][cellIdentifier];

					int id;
					Color color;

					if (!shapeControl)
						GetCellIdAndColorR4(cell, out id, out color, grainId);
					else
						GetCellIdAndColorShapeControl(cell, probability, out id, out color, grainId);

					cell.NewId = id;
					cell.NewColor = color;
					if (cell.NewId > 0)
						newColored.Enqueue(cell);
				});
				//Trace.WriteLine($"Parallel: {sw.ElapsedMilliseconds}ms");
				sw.Restart();

				while (!newColored.IsEmpty)
				{
					if (newColored.TryDequeue(out Cell c))
					{
						c.UpdateId();
						TryAddPotentialGrainsSecondGrowth(grainId, c.NeighboringCells);
						_secondGrowthPotentialGrains[grainId].Remove(c.Identifier);
						toReturn.AddFirst(c);
					}
				}
				//Trace.WriteLine($"While: {sw.ElapsedMilliseconds}ms");

			}

			Trace.WriteLine($"Iteration took: {sw.ElapsedMilliseconds}ms");
			return toReturn;
		}
		private bool IsCellIntoGrain(Cell cell, int grainId)
		{
			bool a = _firstPhaseGrains.First(x => x.Key == grainId).Any(c => c.Identifier == cell.Identifier);
			return a;
		}
		private IEnumerable<Cell> GetEmptyCellsInNeighborhood(Cell cell, RuleType ruleType, int? grainId)
		{
			int i = 0;
			foreach (int isNeighor in Neighbourhood.Patterns[ruleType])
			{
				if (grainId is null)
				{
					if (isNeighor == 1 && cell.NeighboringCells[i] is Cell ce && ce.Id > 0 && ce.Phase == 0)
						yield return ce;
				}
				else
				{
					if (isNeighor == 1 && cell.NeighboringCells[i] is Cell ce && ce.Id > 0 && ce.Phase == 0 && IsCellIntoGrain(ce, grainId.Value))
						yield return ce;
				}
				i++;
			}
		}
		private void GetCellIdAndColorR4(Cell cell, out int id, out Color color, int? grainId = null)
		{
			IEnumerable<Cell> cells = GetEmptyCellsInNeighborhood(cell, RuleType.Moor, grainId);

			//Cell c = cells.First();
			Cell c = cell;

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

			IEnumerable<IGrouping<int, Cell>> groups = cells.GroupBy(c => c.Id).OrderByDescending(x => x.Count());
			if (groups.Any())
			{
				List<IGrouping<int, Cell>> max = groups.Where(x => x.Count() == groups.First().Count()).ToList();

				if (max.Any())
					c = max.ElementAt(RandomMachine.Next(max.Count)).First();
			}

			id = c.Id;
			color = c.Color;
		}
		private void GetCellIdAndColorShapeControl(Cell cell, int probability, out int id, out Color color, int? grainId = null)
		{
			GetCellIdAndColorR1(cell, out id, out color, grainId);
			if (id != 0)
				return;
	
			GetCellIdAndColorR2(cell, out id, out color, grainId);
			if (id != 0)
				return;

			GetCellIdAndColorR3(cell, out id, out color, grainId);
			if (id != 0)
				return;

			GetCellIdAndColorR4(cell, probability, out id, out color, grainId);
			if (id != 0)
				return;
		}
		private void GetCellIdAndColorR1(Cell cell, out int id, out Color color, int? grainId = null)
		{
			var cells = GetEmptyCellsInNeighborhood(cell, RuleType.Moor, grainId);

			Cell c = cells.GroupBy(c => c.Id).OrderByDescending(x => x.Count()).Where(x => x.Count() >= 5).FirstOrDefault()?.FirstOrDefault();

			id = c?.Id ?? 0;
			color = c?.Color ?? Cell.EmptySpaceColor.ToColor();
		}
		private void GetCellIdAndColorR2(Cell cell, out int id, out Color color, int? grainId = null)
		{
			var cells = GetEmptyCellsInNeighborhood(cell, RuleType.VonNeumann, grainId);

			Cell c = cells.GroupBy(c => c.Id).OrderByDescending(x => x.Count()).Where(x => x.Count() >= 3).FirstOrDefault()?.FirstOrDefault();

			id = c?.Id ?? 0;
			color = c?.Color ?? Cell.EmptySpaceColor.ToColor();
		}
		private void GetCellIdAndColorR3(Cell cell, out int id, out Color color, int? grainId = null)
		{
			var cells = GetEmptyCellsInNeighborhood(cell, RuleType.FurtherMoor, grainId);

			Cell c = cells.GroupBy(c => c.Id).OrderByDescending(x => x.Count()).Where(x => x.Count() >= 3).FirstOrDefault()?.FirstOrDefault();

			id = c?.Id ?? 0;
			color = c?.Color ?? Cell.EmptySpaceColor.ToColor();
		}
		private void GetCellIdAndColorR4(Cell cell, int probability, out int id, out Color color, int? grainId = null)
		{
			if (probability < 1 || probability > 100)
				throw new ArgumentOutOfRangeException("GetCellIdAndColorR4");

			GetCellIdAndColorR4(cell, out id, out color, grainId);

			if (RandomMachine.Next(1, 101) > probability)
			{
				id = cell.Id;
				color = cell.Color;
			}
		}
		public override string ToString()
		{
			int uniquePhases = _rows.SelectMany(x => x).Select(x => x.Phase).Distinct().Count();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append($"{ColumnsCount} {RowsCount} {uniquePhases}\n");
			foreach (List<Cell> row in _rows)
				foreach (Cell cell in row)
					stringBuilder.Append($"{cell.IndexX} {cell.IndexY} {cell.Phase} {cell.Id}\n");
			return stringBuilder.ToString();
		}
		public Bitmap ToBitmap(int celSizeBmp, bool isDualPhase = false)
		{
			Bitmap bitmap = new Bitmap(ColumnsCount * celSizeBmp, RowsCount * celSizeBmp);
			Graphics graphics = Graphics.FromImage(bitmap);

			for (int i = 0; i < _rows.Count; i++)
			{
				for (int j = 0; j < _rows[i].Count; j++)
				{
					Cell cell = _rows[i][j];
					SolidBrush solidBrush;

					if (isDualPhase && cell.Phase > 0)
						solidBrush = Cell.Brushes[Cell.DualPhaseColor];
					else
						solidBrush = cell.Color.ToSolidBrush();

					graphics.FillRectangle(solidBrush, j * celSizeBmp, i * celSizeBmp, celSizeBmp, celSizeBmp);
				}
			}
			return bitmap;
		}
		public void AddInclusions(int number, int radius, InclusionsMode inclusionsMode, InclusionsType inclusionsType)
		{
			if (!SetRandomInclusions(number, radius, inclusionsMode, inclusionsType))
				Trace.WriteLine("Unable to set all inclusions");
		}
		private void GetIndexesInsideCircumscribedSquare(Cell center, double radius, out IEnumerable<int> xIndexes, out IEnumerable<int> yIndexes)
		{
			int r = ToInt32(Math.Ceiling(radius));
			xIndexes = new List<int>();
			yIndexes = new List<int>();

			if (BoundaryCondition == Bc.Absorbing)
			{
				int xMin = Math.Max(center.IndexX - r, 0);
				int xMax = Math.Min(center.IndexX + r, ColumnsCount - 1);

				int yMin = Math.Max(center.IndexY - r, 0);
				int yMax = Math.Min(center.IndexY + r, RowsCount - 1);

				xIndexes = Enumerable.Range(xMin, xMax - xMin + 1);
				yIndexes = Enumerable.Range(yMin, yMax - yMin + 1);

			}
			else if (BoundaryCondition == Bc.Periodic)
			{
				int xMin = Math.Max(center.IndexX - r, 0);
				int xMax = Math.Min(center.IndexX + r, ColumnsCount - 1);

				int xOverflow = Math.Min(ColumnsCount - center.IndexX - r - 1, 0);       // sign "-" if found
				int xLack = Math.Min(center.IndexX - r, 0);                              // sign "-" if found


				int yMin = Math.Max(center.IndexY - r, 0);
				int yMax = Math.Min(center.IndexY + r, RowsCount - 1);

				int yOverflow = Math.Min(RowsCount - center.IndexY - r - 1, 0);          // sign "-" if found
				int yLack = Math.Min(center.IndexY - r, 0);                              // sign "-" if found


				xIndexes = Enumerable.Range(xMin, xMax - xMin + 1).Concat(Enumerable.Range(0, Math.Abs(xOverflow))).Concat(Enumerable.Range(ColumnsCount - Math.Abs(xLack), Math.Abs(xLack)));
				yIndexes = Enumerable.Range(yMin, yMax - yMin + 1).Concat(Enumerable.Range(0, Math.Abs(yOverflow))).Concat(Enumerable.Range(RowsCount - Math.Abs(yLack), Math.Abs(yLack)));
			}
		}
		public void SetInclusion(Cell center, double size, InclusionsType inclusionsType)
		{
			GetIndexesInsideCircumscribedSquare(center, size, out IEnumerable<int> xIndexes, out IEnumerable<int> yIndexes);

			Parallel.ForEach(yIndexes, i =>
			{
				Parallel.ForEach(xIndexes, j =>
				{
					if (inclusionsType == InclusionsType.Round && IsInRadius(center.IndexX, center.IndexY, _rows[i][j].IndexX, _rows[i][j].IndexY, size))
					{
						_rows[i][j].SetColor(Cell.InclusionColor.ToColor());
						_rows[i][j].SetId(-1);
					}
					else if (inclusionsType == InclusionsType.Square)
					{
						_rows[i][j].SetColor(Cell.InclusionColor.ToColor());
						_rows[i][j].SetId(-1);
					}
				});
			});
		}
		public bool SetRandomInclusions(int number, double size, InclusionsMode inclusionsMode, InclusionsType inclusionsType)
		{
			int successfulTries = 0;
			int attempts = 0;
			while (successfulTries < number)
			{
				if (attempts > 1_000_000)
					return false;

				bool isFailed = false;
				int rowIndex = RandomMachine.Next(RowsCount);
				int columnIndex = RandomMachine.Next(ColumnsCount);


				if (_rows[rowIndex][columnIndex].Id == -1)
				{
					isFailed = true;
					attempts++;
				}
				// checking if cell is on border if needed
				else if (inclusionsMode == InclusionsMode.Post
					&& _rows[rowIndex][columnIndex].NeighboringCells.Where(c => c is Cell && c?.Id != -1 && c?.Id != 0).Select(c => c.Id).Distinct().Count() < 2)
				{
					isFailed = true;
					attempts++;
				}

				// square wth circle inside
				GetIndexesInsideCircumscribedSquare(_rows[rowIndex][columnIndex], size, out IEnumerable<int> xIndexes, out IEnumerable<int> yIndexes);
				foreach (int i in yIndexes)
				{
					foreach (int j in xIndexes)
					{
						if ((IsInRadius(columnIndex, rowIndex, j, i, size) && _rows[i][j].Id == -1)
							|| (inclusionsType == InclusionsType.Square && _rows[i][j].Id == -1))
						{
							isFailed = true;
							attempts++;
						}
					}
				}

				if (!isFailed)
				{
					SetInclusion(_rows[rowIndex][columnIndex], size, inclusionsType);
					attempts = 0;
					successfulTries++;
				}
			}
			return true;
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
		public int SetCellsBorders(int thickness, int? cellId = null)
		{
			if (thickness < 1)
				throw new ArgumentException("Thickness cannot be less then 1");

			if (cellId is null)
				ClearCellsBorders();

			int cellsOnBorder = 0;

			Action<int, int, Cell> action1 = new Action<int, int, Cell>((direction, size, cell) =>
			{
				if (size == 0)
					return;

				Cell c = cell.NeighboringCells[direction];
				if (c is Cell
					&& (cellId == null && cell.Id > c.Id)
					|| (cellId is int && cell.Id > c.Id && (c.Id == cellId || cell.Id == cellId)))
				{

					if (!cell.IsOnBorder)
					{
						cell.IsOnBorder = true;
						cellsOnBorder++;
					}

					int i = 0;
					while (i < size && c.NeighboringCells[(direction + 4) % 8] is Cell ce)
					{
						c = ce;
						i++;
						if (!c.IsOnBorder)
						{
							c.IsOnBorder = true;
							cellsOnBorder++;
						}
					}
				}
			});

			Action<int, int, Cell> action2 = new Action<int, int, Cell>((direction, size, cell) =>
			{
				if (size == 0)
					return;

				Cell c = cell.NeighboringCells[direction];
				if (c is Cell
					&& (cellId == null && cell.Id < c.Id)
					|| (cellId is int && cell.Id < c.Id && (c.Id == cellId || cell.Id == cellId)))
				{

					if (!cell.IsOnBorder)
					{
						cell.IsOnBorder = true;
						cellsOnBorder++;
					}

					int i = 0;
					while (i < size && c.NeighboringCells[(direction + 4) % 8] is Cell ce)
					{
						c = ce;
						i++;
						if (!c.IsOnBorder)
						{
							c.IsOnBorder = true;
							cellsOnBorder++;
						}
					}
				}
			});

			var actions = new Action<int, int, Cell>[] { action1, action2 };

			int floor = ToInt32(Math.Floor((1.0 * thickness) / 2));
			int celing = ToInt32(Math.Ceiling((1.0 * thickness) / 2));

			for (int i = 0; i < thickness; i++)
			{
				foreach (Cell cell in _rows.SelectMany(x => x))
				{
					int x = i % 2 == 0 ? celing : floor;

					actions[i % 2].Invoke(1, x, cell);
					actions[i % 2].Invoke(3, x, cell);
					actions[i % 2].Invoke(5, x, cell);
					actions[i % 2].Invoke(7, x, cell);
				}
			}

			return cellsOnBorder;
		}
		public void ClearCellsBorders(int? cellId = null)
		{
			Parallel.ForEach(_rows.SelectMany(x => x), cell =>
			{
				if (!cellId.HasValue)
					cell.IsOnBorder = false;
				else if (cellId.HasValue && cell.Id == cellId)
					cell.IsOnBorder = false;
			});
		}
		public void SetDualPhase(int cellId)
		{
			Parallel.ForEach(_rows.SelectMany(x => x), cell =>
			{
				if (cell.Id == cellId)
					cell.Phase = 1;
			});
		}
		public void ClearDualPhase()
		{
			Parallel.ForEach(_rows.SelectMany(x => x), cell =>
			{
				cell.Phase = 0;
			});
		}
	}
}
