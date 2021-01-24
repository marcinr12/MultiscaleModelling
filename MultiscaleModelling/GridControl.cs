using MultiscaleModelling.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Convert;

namespace MultiscaleModelling
{
	public partial class GridControl : UserControl
	{
		private Bitmap bitmap;
		private Graphics graphics;

		public readonly Matrix Matrix = new Matrix();
		public Color EmptySpaceColor { get; set; } = Color.FromArgb(255, 240, 240, 240);

		private int _gridCellWidth;
		public int GridCellWidth
		{
			get => _gridCellWidth;
			set
			{
				if (_gridCellWidth != value)
				{
					_gridCellWidth = value;
					Matrix.Rearange(_gridCellWidth, _gridCellHeight);
					Draw();
				}
			}
		}

		private int _gridCellHeight;
		public int GridCellHeight
		{
			get => _gridCellHeight;
			set
			{
				if (_gridCellHeight != value)
				{
					_gridCellHeight = value;
					Matrix.Rearange(_gridCellWidth, _gridCellHeight);
					Draw();
				}
			}
		}

		private bool _isGridShowed;
		public bool IsGridShowed
		{
			get => _isGridShowed;
			set
			{
				_isGridShowed = value;
				Draw();
			}
		}

		private ViewMode _viewMode;
		public ViewMode ViewMode
		{
			get => _viewMode;
			set
			{
				_viewMode = value;
				Draw();
			}
		}

		private bool _showGrainBoundaries;
		public bool ShowGrainBoundaries
		{
			get => _showGrainBoundaries;
			set
			{
				_showGrainBoundaries = value;
				Draw();
			}
		}
		private readonly Pen blackPen = new Pen(Cell.GridColor.ToColor());
		public GridControl()
		{
			InitializeComponent();
		}
		public void PrintGrid()
		{
			double cellSize = Matrix.CellSize;
			//vertical
			graphics.DrawLine(blackPen, 0, 0, 0, ToSingle(GridCellHeight * cellSize));
			for (int i = 0; i <= GridCellWidth; i++)
				graphics.DrawLine(blackPen, ToSingle(i * cellSize) - 1, 0, ToSingle(i * cellSize) - 1, ToSingle(GridCellHeight * cellSize));

			//horizontal
			graphics.DrawLine(blackPen, 0, 0, ToSingle(GridCellWidth * cellSize), 0);
			for (int i = 0; i <= GridCellHeight; i++)
				graphics.DrawLine(blackPen, 0, ToSingle(i * cellSize) - 1, ToSingle(GridCellWidth * cellSize), ToSingle(i * cellSize) - 1);
		}
		readonly Stopwatch sw = new Stopwatch();
		public void PrintCells()
		{
			sw.Restart();
			for (int i = 0; i < Matrix.RowsCount; i++)
			{
				for (int j = 0; j < Matrix.ColumnsCount; j++)
				{
					Cell cell = Matrix.GetCell(i, j);
					SolidBrush brush = null;

					if (ShowGrainBoundaries)
					{
						if (cell.IsOnBorder)
							brush = Cell.Brushes[Cell.BorderColor];
						else
						{
							if (ViewMode == ViewMode.DualPhase)
							{
								if (cell.Phase > 0)
									brush = Cell.Brushes[Cell.DualPhaseColor];
								else
									brush = Cell.Brushes[cell.Color.ToArgb()];
							}
							else if (ViewMode == ViewMode.Substracture)
								brush = Cell.Brushes[cell.Color.ToArgb()];
						}
					}
					else
					{
						if (ViewMode == ViewMode.DualPhase)
						{
							if (cell.Phase > 0)
								brush = Cell.Brushes[Cell.DualPhaseColor];
							else
								brush = Cell.Brushes[cell.Color.ToArgb()];
						}
						else if (ViewMode == ViewMode.Substracture)
							brush = Cell.Brushes[cell.Color.ToArgb()];
					}

					try
					{
						graphics.FillRectangle(brush, Matrix.CellSize * cell.IndexX - 1, Matrix.CellSize * cell.IndexY - 1, Matrix.CellSize + 1, Matrix.CellSize + 1);
					}
					catch (Exception e)
					{
						Trace.WriteLine("Exeption: PrintCells(): " + e.Message);
					}
				}
			}
		}
		public void PrintCells(IEnumerable<Cell> cells)
		{
			foreach (Cell cell in cells)
			{
				SolidBrush brush = Cell.Brushes[cell.Color.ToArgb()];
				try
				{
					graphics.FillRectangle(brush, Matrix.CellSize * cell.IndexX - 1, Matrix.CellSize * cell.IndexY - 1, Matrix.CellSize + 1, Matrix.CellSize + 1);
				}
				catch (Exception e)
				{
					Trace.WriteLine("Exeption: PrintCells(IEnumerable<Cell> cells): " + e.Message);
				}
			}
		}
		private void CalculateCellSize()
		{
			float cellWidth = 1.0f * outputPictureBox.Width / GridCellWidth;
			float cellHeight = 1.0f * outputPictureBox.Height / GridCellHeight;
			Matrix.SetCellSize(cellHeight < cellWidth ? cellHeight : cellWidth);
		}
		public void Draw(IEnumerable<Cell> cells = null)
		{
			if (!IsHandleCreated)
				return;

			CalculateCellSize();
			if (outputPictureBox.Image == null)
			{
				outputPictureBox.Image?.Dispose();
				outputPictureBox.Image = new Bitmap(outputPictureBox.Width, outputPictureBox.Height);
				bitmap = new Bitmap(outputPictureBox.Width, outputPictureBox.Height);
				graphics = Graphics.FromImage(bitmap);
			}

			if (cells?.Count() > 0)
				PrintCells(cells);
			else
			{
				//graphics.Clear(EmptySpaceColor);
				try
				{
					outputPictureBox.Invoke(new Action(() => graphics.Clear(EmptySpaceColor)));
				}
				catch (Exception ex)
				{
					Trace.WriteLine("Draw(IEnumerable<Cell> cells = null)" + ex.Message);
				}
				PrintCells();
			}

			if (IsGridShowed)
				PrintGrid();

			outputPictureBox.Invoke(new Action(() => outputPictureBox.Image = bitmap));
		}
		public void LoadMatrix(List<(int Id, int Phase, int IndexX, int IndexY)> cells)
		{
			int maxIndexX = cells.Select(c => c.IndexX).Max();
			int maxIndexY = cells.Select(c => c.IndexY).Max();

			GridCellHeight = maxIndexY + 1;
			GridCellWidth = maxIndexX + 1;

			HashSet<int> colors = new HashSet<int>()
			{
				Cell.InclusionColor,
				Cell.EmptySpaceColor
			};

			int coloredCells = cells.Select(c => c.Id).Where(id => id > 0).Distinct().Count();
			while (colors.Count < coloredCells + 2)
				colors.Add(Color.FromArgb(RandomMachine.Next(255), RandomMachine.Next(255), RandomMachine.Next(255)).ToArgb());

			foreach ((int Id, int Phase, int IndexX, int IndexY) in cells)
			{
				Matrix.GetCell(IndexY, IndexX).SetId(Id);
				Matrix.GetCell(IndexY, IndexX).Phase = Phase;
				Matrix.GetCell(IndexY, IndexX).SetColor(Color.FromArgb(colors.ToList().ElementAt(Id + 1)));
			}
		}
		public void LoadMatrix(Bitmap bitmap, int cellSizeBmp)
		{
			Matrix.Erase();
			int rowsCount = bitmap.Height / cellSizeBmp;
			int columnsCount = bitmap.Width / cellSizeBmp;

			GridCellHeight = rowsCount;
			GridCellWidth = columnsCount;

			HashSet<int> colors = new HashSet<int>()
			{
				Cell.EmptySpaceColor,
				Cell.InclusionColor
			};

			for (int i = 0; i < rowsCount; i++)
			{
				for (int j = 0; j < columnsCount; j++)
				{
					Color color = bitmap.GetPixel(j * cellSizeBmp, i * cellSizeBmp);
					int colorArgb = color.ToArgb();
					colors.Add(colorArgb);

					var list = colors.ToList();

					var id = list.IndexOf(list.Find(x => x == colorArgb)) - 1;
					Matrix.GetCell(i, j).SetId(id);
					Matrix.GetCell(i, j).SetColor(color);
				}
			}
		}


		[Browsable(true)]
		[Category("Action")]
		public new event MouseEventHandler MouseClick
		{
			add { outputPictureBox.MouseClick += value; }
			remove { outputPictureBox.MouseClick -= value; }
		}
		private void OutputPictureBox_MouseClick(object sender, MouseEventArgs e)
		{
			int xIndex = ToInt32(Math.Floor(e.X / Matrix.CellSize));
			int yIndex = ToInt32(Math.Floor(e.Y / Matrix.CellSize));

			if (xIndex < Matrix.ColumnsCount && yIndex < Matrix.RowsCount)
				Matrix.SelectedCell = Matrix.GetCell(yIndex, xIndex);
			else
				Matrix.SelectedCell = null;
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			Draw();
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			bitmap = new Bitmap(outputPictureBox.Width, outputPictureBox.Height);
			graphics = Graphics.FromImage(bitmap);
			Draw();
		}
	}
}
