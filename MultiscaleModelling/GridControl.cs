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
		//public void PrintGrid(Graphics g)
		//{
		//	double cellSize = Matrix.CellSize;
		//	//vertical
		//	g.DrawLine(blackPen, 0, 0, 0, ToSingle(GridCellHeight * cellSize));
		//	for (int i = 0; i <= GridCellWidth; i++)
		//		g.DrawLine(blackPen, ToSingle(i * cellSize) - 1, 0, ToSingle(i * cellSize) - 1, ToSingle(GridCellHeight * cellSize));

		//	//horizontal
		//	g.DrawLine(blackPen, 0, 0, ToSingle(GridCellWidth * cellSize), 0);
		//	for (int i = 0; i <= GridCellHeight; i++)
		//		g.DrawLine(blackPen, 0, ToSingle(i * cellSize) - 1, ToSingle(GridCellWidth * cellSize), ToSingle(i * cellSize) - 1);
		//}
		public void PrintGrid()
		{
			int cellSize = Math.Min(outputPictureBox.Height, outputPictureBox.Width) / Math.Max(GridCellHeight, GridCellWidth);
			Bitmap bitmap = outputPictureBox.Image as Bitmap;
			int gridSide = Math.Min(bitmap.Height, bitmap.Width);
			gridSide -= gridSide % cellSize;


			for (int i = 0; i < gridSide; i++)
			{
				for (int j = 0; j < gridSide; j++)
				{
					if (i % cellSize == 0 || j % cellSize == 0)
						Invoke(new Action(() => bitmap.SetPixel(j, i, Color.Black)));
					else if (i == gridSide - 1 || j % cellSize == 0)
						Invoke(new Action(() => bitmap.SetPixel(j, i, Color.Black)));
					else if (i % cellSize == 0 || j == gridSide - 1)
						Invoke(new Action(() => bitmap.SetPixel(j, i, Color.Black)));
				}
			}
			outputPictureBox.Image = bitmap;
		}
		//public void PrintCells(Graphics g)
		//{
		//	for (int i = 0; i < Matrix.RowsCount; i++)
		//	{
		//		for (int j = 0; j < Matrix.ColumnsCount; j++)
		//		{
		//			Cell cell = Matrix.GetCell(i, j);
		//			SolidBrush brush = new SolidBrush(cell.Color);
		//			double cellSize = Matrix.CellSize;
		//			g.FillRectangle(brush, ToSingle(cellSize * cell.IndexX) - 1, ToSingle(cellSize * cell.IndexY) - 1, ToSingle(cellSize + 1), ToSingle(cellSize + 1));
		//		}
		//	}
		//}
		public void PrintCells()
		{
			if (GridCellHeight == 0 || GridCellWidth == 0)
				return;

			int cellSize = Math.Min(outputPictureBox.Height, outputPictureBox.Width) / Math.Max(GridCellHeight, GridCellWidth);
			Bitmap bitmap = outputPictureBox.Image as Bitmap;

			for (int i = 0; i < Matrix.RowsCount; i++)
			{
				for (int j = 0; j < Matrix.ColumnsCount; j++)
					for (int k = 0; k < cellSize; k++)
						for (int l = 0; l < cellSize; l++)
						{
							bitmap.SetPixel(j * cellSize + k, i * cellSize + l, Matrix.GetCell(i, j).Color);
							//Invoke(new Action(() => bitmap.SetPixel(j * cellSize + k, i * cellSize + l, Matrix.GetCell(i, j).Color)));
						}
			}

			Invoke(new Action(() => outputPictureBox.Image = bitmap));			
		}
		public void FillCell(int rowIndex, int columnIndex)
		{
			if (GridCellHeight == 0 || GridCellWidth == 0)
				return;

			int cellSize = Math.Min(outputPictureBox.Height, outputPictureBox.Width) / Math.Max(GridCellHeight, GridCellWidth);
			Bitmap bitmap = outputPictureBox.Image as Bitmap;

			for(int k = 0; k< cellSize; k++)
				for(int l = 0; l < cellSize; l++)
					bitmap.SetPixel(columnIndex * cellSize + k, rowIndex * cellSize + l, Matrix.GetCell(rowIndex, columnIndex).Color);

			//outputPictureBox.Image = bitmap;
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
			//CalculateCellSize();
			//if (outputPictureBox.Image == null || newBitmap)
			//{
			//	outputPictureBox.Image?.Dispose();
			//	outputPictureBox.Image = new Bitmap(outputPictureBox.Width, outputPictureBox.Height);
			//}

			//Bitmap bitmap = outputPictureBox.Image as Bitmap;

			//if (IsHandleCreated)
			//	outputPictureBox.Invoke(new Action(() =>
			//	{
			//		Graphics g = Graphics.FromImage(bitmap);
			//		g.Clear(EmptySpaceColor);

			//		PrintCells(g);
			//		if (IsGridShowed)
			//			PrintGrid(g);

			//		outputPictureBox.Image = bitmap;
			//		g.Dispose();
			//	}));

			//GC.Collect();
			//GC.WaitForPendingFinalizers();





			if (outputPictureBox.Image == null || newBitmap)
			{
				outputPictureBox.Image?.Dispose();
				outputPictureBox.Image = new Bitmap(outputPictureBox.Width, outputPictureBox.Height);
			}
			if (!IsHandleCreated)
				return;
			PrintCells();
			if (IsGridShowed)
				PrintGrid();
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

			int coloredCells = cells.Where(c => c.Id != 0).Count();
			while(colors.Count < coloredCells + 1)
				colors.Add(Color.FromArgb(RandomMachine.Next(255), RandomMachine.Next(255), RandomMachine.Next(255)).ToArgb());				

			foreach ((int Id, int Phase, int IndexX, int IndexY) in cells)
			{
				Matrix.GetCell(IndexY, IndexX).Id = Id;
				Matrix.GetCell(IndexY, IndexX).Phase = Phase;
				Matrix.GetCell(IndexY, IndexX).Color = Color.FromArgb(colors.ToList().ElementAt(Id));
			}
		}
		public void LoadMatrix(Bitmap bitmap)
		{
			Matrix.Erase();
			int rowsCount = bitmap.Height / 10;
			int columnsCount = bitmap.Width / 10;

			GridCellHeight = rowsCount;
			GridCellWidth = columnsCount;


			Stopwatch sw = new Stopwatch();
			sw.Restart();
			HashSet<int> colors = new HashSet<int>()
			{
				Color.White.ToArgb()
			};

			for (int i = 0; i < rowsCount; i++)
			{
				//Trace.WriteLine($"{i} / {rowsCount}");

				for (int j = 0; j < columnsCount; j++)
				{
					Color color = bitmap.GetPixel(j * 10, i * 10);
					int colorArgb = color.ToArgb();
					colors.Add(colorArgb);

					var list = colors.ToList();
					var a = Matrix.GetCell(i, j).Id = list.IndexOf(list.Find(x => x == colorArgb));
					Matrix.GetCell(i, j).Color = color;
				}

				//int currnetId = 1;
				//for (int i = 0; i < rowsCount; i++)
				//{
				//	Trace.WriteLine($"{i} / {rowsCount}");

				//	for (int j = 0; j < columnsCount; j++)
				//	{
				//		Color colorBmp = bitmap.GetPixel(j * 10, i * 10);
				//		if (colorBmp.ToArgb().Equals(Color.White.ToArgb()))
				//			Matrix.GetCell(i, j).Id = 0;
				//		else
				//		{
				//			IEnumerable<Cell> cellsWithColor = Matrix.GetCells().Where(c => c.Color.ToArgb().Equals(colorBmp.ToArgb()));
				//			if (cellsWithColor.Count() > 0)
				//				Matrix.GetCell(i, j).Id = cellsWithColor.First().Id;
				//			else
				//			{
				//				Matrix.GetCell(i, j).Id = currnetId;
				//				currnetId++;
				//			}
				//		}
				//		Matrix.GetCell(i, j).Color = colorBmp;
				//	}
				//}
			}
		}
	}
}
