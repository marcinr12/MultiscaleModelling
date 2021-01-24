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

	public enum RuleType
	{
		Moor,
		VonNeumann,
		FurtherMoor,
		MoorWithProbability
	}

	public enum ViewMode
	{
		Substracture,
		DualPhase
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

		public static readonly Dictionary<RuleType, string> RuleTypeNames = new Dictionary<RuleType, string>()
		{
			{ RuleType.Moor, "Moor" },
			{ RuleType.VonNeumann, "VonNeumann" },
			{ RuleType.FurtherMoor, "FurtherMoor" },
			{ RuleType.MoorWithProbability, "MoorWithProbability" }
		};
	}
}
