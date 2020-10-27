using System.Drawing;

namespace MultiscaleModelling
{
	public class Cell
	{
		public int Id { get; set; }
		public int IndexX { get; private set; }
		public int IndexY { get; private set; }
		public Color Color {get;set;}
		public Cell[] NeighboringCells { get; set; } = new Cell[] { null, null, null, null, null, null, null, null };
		public readonly Matrix Matrix;
		public Cell(int indexY, int indexX, Matrix matrix, int id = 0, Color color = new Color())
		{
			IndexX = indexX;
			IndexY = indexY;
			Id = id;
			Matrix = matrix;
			if (id == 0)
				Color = Color.White;
			else
				Color = color;
		}
	}
}
