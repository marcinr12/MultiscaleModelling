﻿using System.Collections.Generic;
using System.Drawing;

namespace MultiscaleModelling
{
	public class Cell
	{
		public static readonly Dictionary<int, SolidBrush> Brushes = new Dictionary<int, SolidBrush>()
		{
			{ Color.White.ToArgb(), new SolidBrush(Color.White) },
			{ Color.Black.ToArgb(), new SolidBrush(Color.Black) }
		};

		public int Id { get; private set; }
		public Color Color {get; private set;}
		public int Phase { get; set; }
		public readonly int IndexX;
		public readonly int IndexY;
		public Cell[] NeighboringCells { get; set; } = new Cell[] { null, null, null, null, null, null, null, null };
		public readonly Matrix Matrix;
		public Cell(int indexY, int indexX, Matrix matrix, int phase = 0)
		{
			Phase = phase;
			IndexX = indexX;
			IndexY = indexY;
			Matrix = matrix;
			Color = Color.White;

		}

		public void SetId(int id)
		{
			Id = id;
		}
		public void SetColor(Color color)
		{
			Color = color;
			if (!Brushes.TryGetValue(color.ToArgb(), out SolidBrush _))
				Brushes.Add(color.ToArgb(), new SolidBrush(color));
		}
	}
}
