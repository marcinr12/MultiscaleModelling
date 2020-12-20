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

	public enum InclusionsMode
	{
		Pre,
		Post
	}

	public enum InclusionsType
	{
		Squre,
		Round
	}

	public static class EnumsNames
	{
		public static readonly Dictionary<Bc, string> BcNames = new Dictionary<Bc, string>()
		{
			{ Bc.Absorbing, "Absorbing" },
			{ Bc.Periodic, "Periodic" }
		};

		public static readonly Dictionary<InclusionsMode, string> InclusionsModeNames = new Dictionary<InclusionsMode, string>()
		{
			{ InclusionsMode.Pre, "Pre" },
			{ InclusionsMode.Post, "Post" }
		};

		public static readonly Dictionary<InclusionsType, string> InclusionsTypeNames = new Dictionary<InclusionsType, string>()
		{
			{ InclusionsType.Squre, "Squre" },
			{ InclusionsType.Round, "Round" }
		};
	}
}
