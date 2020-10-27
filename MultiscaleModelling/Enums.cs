using System;
using System.Collections.Generic;
using System.Text;

namespace MultiscaleModelling
{
	public enum Bc
	{
		Absorbing,
		Periodic
	}

	public static class EnumsNames
	{
		public static Dictionary<Bc, string> BcNames = new Dictionary<Bc, string>()
		{
			{ Bc.Absorbing, "Absorbing" },
			{ Bc.Periodic, "Periodic" }
		};
	}
}
