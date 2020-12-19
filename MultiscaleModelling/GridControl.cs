using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
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

		private readonly Pen blackPen = new Pen(Color.Black);
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
					SolidBrush brush = Cell.Brushes[cell.Color.ToArgb()];
					graphics.FillRectangle(brush, Matrix.CellSize * cell.IndexX - 1, Matrix.CellSize * cell.IndexY - 1, Matrix.CellSize + 1, Matrix.CellSize + 1);
				}
			}
		}
		public void PrintCells(IEnumerable<Cell> cells)
		{
			foreach(Cell cell in cells)
			{
				SolidBrush brush = Cell.Brushes[cell.Color.ToArgb()];
				graphics.FillRectangle(brush, Matrix.CellSize * cell.IndexX - 1, Matrix.CellSize * cell.IndexY - 1, Matrix.CellSize + 1, Matrix.CellSize + 1);
			}
		}
		private void CalculateCellSize()
		{
			float cellWidth = 1.0f * outputPictureBox.Width / GridCellWidth;
			float cellHeight = 1.0f * outputPictureBox.Height / GridCellHeight;
			Matrix.SetCellSize(cellHeight < cellWidth ? cellHeight : cellWidth);
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			bitmap = new Bitmap(outputPictureBox.Width, outputPictureBox.Height);
			graphics = Graphics.FromImage(bitmap);
			Draw();
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
				outputPictureBox.Invoke(new Action(() => graphics.Clear(EmptySpaceColor)));
				PrintCells();
			}

			if (IsGridShowed)
				PrintGrid();
		
			outputPictureBox.Invoke(new Action(() => outputPictureBox.Image = bitmap));
		}
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			Draw();
		}
		public void LoadMatrix(List<(int Id, int Phase, int IndexX, int IndexY)> cells)
		{
			int maxIndexX = cells.Select(c => c.IndexX).Max();
			int maxIndexY = cells.Select(c => c.IndexY).Max();

			GridCellHeight = maxIndexY + 1;
			GridCellWidth = maxIndexX + 1;

			HashSet<int> colors = new HashSet<int>()
			{
				Color.White.ToArgb()
			};

			int coloredCells = cells.Select(c => c.Id).Where(id => id != 0).Distinct().Count();
			while(colors.Count < coloredCells + 1)
				colors.Add(Color.FromArgb(RandomMachine.Next(255), RandomMachine.Next(255), RandomMachine.Next(255)).ToArgb());

			foreach ((int Id, int Phase, int IndexX, int IndexY) in cells)
			{
				Matrix.GetCell(IndexY, IndexX).SetId(Id);
				Matrix.GetCell(IndexY, IndexX).Phase = Phase;
				Matrix.GetCell(IndexY, IndexX).SetColor(Color.FromArgb(colors.ToList().ElementAt(Id)));
			}
		}
		public void LoadMatrix(Bitmap bitmap)
		{
			Matrix.Erase();
			int rowsCount = bitmap.Height / 10;
			int columnsCount = bitmap.Width / 10;

			GridCellHeight = rowsCount;
			GridCellWidth = columnsCount;

			HashSet<int> colors = new HashSet<int>()
			{
				Color.Black.ToArgb(),
				Color.White.ToArgb()
			};

			for (int i = 0; i < rowsCount; i++)
			{
				for (int j = 0; j < columnsCount; j++)
				{
					Color color = bitmap.GetPixel(j * 10, i * 10);
					int colorArgb = color.ToArgb();
					colors.Add(colorArgb);

					var list = colors.ToList();
					Matrix.GetCell(i, j).SetId(list.IndexOf(list.Find(x => x == colorArgb) - 1));
					Matrix.GetCell(i, j).SetColor(color);
				}
			}
		}
	}
}
