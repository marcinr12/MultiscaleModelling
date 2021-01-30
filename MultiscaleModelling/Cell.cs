﻿using MultiscaleModelling.Extensions;
using System.Collections.Generic;
using System.Drawing;

namespace MultiscaleModelling
{
	public class Cell
	{
		public static readonly int EmptySpaceColor = Color.White.ToArgb();
		public static readonly int InclusionColor = Color.Black.ToArgb();
		public static readonly int BorderColor = Color.Blue.ToArgb();
		public static readonly int DualPhaseColor = Color.Pink.ToArgb();
		public static readonly int GridColor = Color.Black.ToArgb();

		public static readonly Dictionary<int, SolidBrush> Brushes = new Dictionary<int, SolidBrush>()
		{
			{ EmptySpaceColor, EmptySpaceColor.ToSolidBrush() },
			{ InclusionColor, InclusionColor.ToSolidBrush() },
			{ BorderColor, BorderColor.ToSolidBrush() },
			{ DualPhaseColor, DualPhaseColor.ToSolidBrush() }
		};
		private static long Counter = 0;
		public long Identifier { get; private set; }

		public int Id { get; private set; }
		public int NewId { get; set; }
		public Color Color { get; private set; }
		public Color NewColor { get; set; }
		public int Phase { get; set; }
		public bool IsOnBorder { get; set; }
		public readonly int IndexX;
		public readonly int IndexY;
		public Cell[] NeighboringCells { get; set; } = new Cell[] { null, null, null, null, null, null, null, null };
		public readonly Matrix Matrix;
		public Cell(int indexY, int indexX, Matrix matrix, int phase = 0)
		{
			Identifier = Counter;
			Counter++;

			Phase = phase;
			IndexX = indexX;
			IndexY = indexY;
			Matrix = matrix;
			Color = EmptySpaceColor.ToColor();
		}
		public void SetId(int id)
		{
			Id = id;
			NewId = id;
		}
		public void SetColor(Color color)
		{
			Color = color;
			NewColor = color;
			if (!Brushes.TryGetValue(color.ToArgb(), out SolidBrush _))
				Brushes.Add(color.ToArgb(), new SolidBrush(color));
		}
		public void UpdateId()
		{
			Id = NewId;
			Color = NewColor;
		}
	}
}
