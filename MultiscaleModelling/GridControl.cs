using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Convert;

namespace MultiscaleModelling
{
	public partial class GridControl : UserControl
	{
		public readonly Matrix Matrix = new Matrix();
		public Color EmptySpaceColor { get; set; } = Color.FromArgb(255, 240, 240, 240);

		private int _gridCellWidth;
		public int GridCellWidth
		{
			get => _gridCellWidth;
			set
			{
				_gridCellWidth = value;
				Matrix.Rearange(_gridCellWidth, _gridCellHeight);
				Draw();
			}
		}

		private int _gridCellHeight;
		public int GridCellHeight
		{
			get => _gridCellHeight;
			set
			{
				_gridCellHeight = value;
				Matrix.Rearange(_gridCellWidth, _gridCellHeight);
				Draw();
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

		public uint Grains { get; private set; } = 0;
		private readonly Pen blackPen = new Pen(Color.Black);
		public GridControl()
		{
			InitializeComponent();
			Draw();
		}
		public void PrintGrid(Graphics g)
		{
			double cellSize = Matrix.CellSize;
			//vertical
			g.DrawLine(blackPen, 0, 0, 0, ToSingle(GridCellHeight * cellSize));
			for (int i = 0; i <= GridCellWidth; i++)
				g.DrawLine(blackPen, ToSingle(i * cellSize) - 1, 0, ToSingle(i * cellSize) - 1, ToSingle(GridCellHeight * cellSize));

			//horizontal
			g.DrawLine(blackPen, 0, 0, ToSingle(GridCellWidth * cellSize), 0);
			for (int i = 0; i <= GridCellHeight; i++)
				g.DrawLine(blackPen, 0, ToSingle(i * cellSize) - 1, ToSingle(GridCellWidth * cellSize), ToSingle(i * cellSize) - 1);

		}
		public void PrintCells(Graphics g)
		{
			for (int i = 0; i < Matrix.RowsCount; i++)
			{
				for (int j = 0; j < Matrix.ColumnsCount; j++)
				{
					Cell cell = Matrix.GetCell(i, j);
					SolidBrush brush = new SolidBrush(cell.Color);
					double cellSize = Matrix.CellSize;
					g.FillRectangle(brush, ToSingle(cellSize * cell.IndexX) - 1, ToSingle(cellSize * cell.IndexY) - 1, ToSingle(cellSize + 1), ToSingle(cellSize + 1));
				}
			}
		}
		private void CalculateCellSize()
		{
			double cellWidth = 1.0 * outputPictureBox.Width / GridCellWidth;
			double cellHeight = 1.0 * outputPictureBox.Height / GridCellHeight;
			Matrix.SetCellSize(cellHeight < cellWidth ? cellHeight : cellWidth);
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Draw(true);
		}
		public void Draw(bool newBitmap = false)
		{
			CalculateCellSize();
			if (outputPictureBox.Image == null || newBitmap)
			{
				outputPictureBox.Image?.Dispose();
				outputPictureBox.Image = new Bitmap(outputPictureBox.Width, outputPictureBox.Height);
			}

			Bitmap bitmap = outputPictureBox.Image as Bitmap;

			if (IsHandleCreated)
				outputPictureBox.Invoke(new Action(() =>
				{
					Graphics g = Graphics.FromImage(bitmap);
					g.Clear(EmptySpaceColor);

					PrintCells(g);
					if (IsGridShowed)
						PrintGrid(g);

					outputPictureBox.Image = bitmap;
					g.Dispose();
				}));
		}
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			Draw();
		}
	}
}
