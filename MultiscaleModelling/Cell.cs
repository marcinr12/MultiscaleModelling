using System.Drawing;

namespace MultiscaleModelling
{
	public class Cell
	{
		public int Id { get; set; }
		public static double Size { get; private set; }
		public int IndexX { get; private set; }
		public int IndexY { get; private set; }
		public Color Color {get;set;}
		public Cell(int indexX, int indexY, int id = 0, Color color = new Color())
		{
			IndexX = indexX;
			IndexY = indexY;
			Id = id;
			if (id == 0)
				Color = Color.White;
			else
				Color = color;
		}

		public static void SetSize(double size)
		{
			Size = size;
		}
	}
}
