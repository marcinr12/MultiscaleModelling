using System.Drawing;

namespace MultiscaleModelling.Extensions
{
	public static class ColorExtensions
	{
		public static SolidBrush ToSolidBrush(this Color color)
		{
			return new SolidBrush(color);
		}
	}
}
