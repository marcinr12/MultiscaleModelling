using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiscaleModelling.Extensions
{
	public static class IntegersExtensions
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
