using System.Drawing;

namespace MultiscaleModelling.Extensions
{
	public static class IntegerExtensions
	{
		public static Color ToColor(this int argb)
		{
			return Color.FromArgb(argb);
		}

		public static SolidBrush ToSolidBrush(this int argb)
		{
			return new SolidBrush(argb.ToColor());
		}
	}
}
