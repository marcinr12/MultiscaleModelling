using System.Drawing;

namespace MultiscaleModelling
{
	public class Cell
	{
		public int Id { get; set; }
		public Color Color {get;set;}
		public int Phase { get; set; }
		public readonly int IndexX;
		public readonly int IndexY;
		public Cell[] NeighboringCells { get; set; } = new Cell[] { null, null, null, null, null, null, null, null };
		public readonly Matrix Matrix;
		public Cell(int indexY, int indexX, Matrix matrix, int id = 0, int phase = 0, Color color = new Color())
		{
			Id = id;
			Phase = phase;
			IndexX = indexX;
			IndexY = indexY;
			Matrix = matrix;
			if (id == 0)
				Color = Color.White;
			else
				Color = color;
		}
	}
}
