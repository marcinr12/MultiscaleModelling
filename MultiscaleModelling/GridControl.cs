using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Convert;

namespace MultiscaleModelling
{
	public partial class GridControl : UserControl
	{
		public PictureBox OutputPictureBox => outputPictureBox;
		//CellsMatrix.Count       -> rows counter
		//CellsMatrix[0].Count    -> columns counter
		public readonly List<List<Cell>> CellsMatrix = new List<List<Cell>>();

		public double CellSize { get; private set; }

		private int _gridCellWidth;
		public int GridCellWidth
		{
			get => _gridCellWidth;
			set
			{
				_gridCellWidth = value;
				RearangeMatrix(_gridCellWidth, _gridCellHeight);
				Refresh();
			}
		}

		private int _gridCellHeight;
		public int GridCellHeight
		{
			get => _gridCellHeight;
			set
			{
				_gridCellHeight = value;
				RearangeMatrix(_gridCellWidth, _gridCellHeight);
				Refresh();
			}
		}

		private bool _isGridShowed;
		public bool IsGridShowed
		{
			get => _isGridShowed;
			set
			{
				_isGridShowed = value;
				Refresh();
			}
		}

		public uint Grains { get; private set; } = 0;
		private readonly Pen blackPen = new Pen(Color.Black);
		public GridControl()
		{
			InitializeComponent();
		}
		public void PrintGrid(ref Graphics g)
		{
			//vertical
			g.DrawLine(blackPen, 0, 0, 0, ToSingle(GridCellHeight * CellSize));
			for (int i = 0; i <= GridCellWidth; i++)
				g.DrawLine(blackPen, ToSingle(i * CellSize) - 1, 0, ToSingle(i * CellSize) - 1, ToSingle(GridCellHeight * CellSize));

			//horizontal
			g.DrawLine(blackPen, 0, 0, ToSingle(GridCellWidth * CellSize), 0);
			for (int i = 0; i <= GridCellHeight; i++)
				g.DrawLine(blackPen, 0, ToSingle(i * CellSize) - 1, ToSingle(GridCellWidth * CellSize), ToSingle(i * CellSize) - 1);

		}
		public void PrintCells(ref Graphics g)
		{
			for (int i = 0; i < CellsMatrix.Count; i++)
			{
				for (int j = 0; j < CellsMatrix[i].Count; j++)
				{
					Cell cell = CellsMatrix[i][j];

					Color color = cell.Color;
					//if (cell.Id != 0)
					//	color = Color.Red;
					//else
					//	color = Color.White;

					SolidBrush brush = new SolidBrush(color);

					g.FillRectangle(brush, ToSingle(CellSize * cell.IndexX) - 1, ToSingle(CellSize * cell.IndexY) - 1, ToSingle(CellSize + 1), ToSingle(CellSize + 1));
				}
			}
		}
		private void CalculateCellSize()
		{
			double cellWidth = 1.0 * outputPictureBox.Width / GridCellWidth;
			double cellHeight = 1.0 * outputPictureBox.Height / GridCellHeight;
			CellSize = cellHeight < cellWidth ? cellHeight : cellWidth;
			Cell.SetSize(CellSize);
		}
		private void RearangeMatrix(int targetSizeX, int targetSizeY)
		{
			// removing rows and columns
			if (CellsMatrix == null)
				throw new NullReferenceException($"{nameof(CellsMatrix)} is null");

			if (targetSizeY < CellsMatrix.Count && CellsMatrix.Count > 0)
				CellsMatrix.RemoveRange(targetSizeY, CellsMatrix.Count - targetSizeY);

			for (int i = 0; i < CellsMatrix.Count; i++)
				if (targetSizeX < CellsMatrix[i].Count && CellsMatrix[i].Count > 0)
					CellsMatrix[i].RemoveRange(targetSizeX, CellsMatrix[i].Count - targetSizeX);

			// adding rows and columns
			for (int i = 0; i < targetSizeY; i++)
			{
				// rows
				if (i >= CellsMatrix.Count)
					CellsMatrix.Add(new List<Cell>());
				// columns
				for (int j = 0; j < targetSizeX; j++)
					if (j >= CellsMatrix[i].Count)
						CellsMatrix[i].Add(new Cell(indexY: i, indexX: j));
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Refresh();
		}
		public override void Refresh()
		{
			OutputPictureBox.Image = null;
			CalculateCellSize();
			Bitmap bitmap = new Bitmap(outputPictureBox.Width, outputPictureBox.Height);
			Graphics g = Graphics.FromImage(bitmap);
			PrintCells(ref g);
			if (IsGridShowed)
				PrintGrid(ref g);

			outputPictureBox.Image?.Dispose();
			outputPictureBox.Image = bitmap;
			g.Dispose();
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

	}
}
