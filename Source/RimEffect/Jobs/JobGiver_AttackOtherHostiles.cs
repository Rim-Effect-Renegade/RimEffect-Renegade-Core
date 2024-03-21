﻿using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace RimEffect
{
	public class JobGiver_AttackOtherHostiles : JobGiver_AIFightEnemies
	{
		public List<Pawn> enemies = new List<Pawn>();
        protected override Thing FindAttackTarget(Pawn pawn)
        {
			TargetScanFlags targetScanFlags = TargetScanFlags.NeedLOSToPawns 
				| TargetScanFlags.NeedReachableIfCantHitFromMyPos | TargetScanFlags.NeedThreat | TargetScanFlags.NeedAutoTargetable;
			if (PrimaryVerbIsIncendiary(pawn))
			{
				targetScanFlags |= TargetScanFlags.NeedNonBurning;
			}
			var target = enemies.RandomElement();
			if (target is null)
            {
				return (Thing)AttackTargetFinder.BestAttackTarget
				(pawn, targetScanFlags, (Thing x) => ExtraTargetValidator(pawn, x), 0f, pawn.Map.Size.x, GetFlagPosition(pawn), GetFlagRadius(pawn));
			}
			return target;
		}

		private bool PrimaryVerbIsIncendiary(Pawn pawn)
		{
			if (pawn.equipment != null && pawn.equipment.Primary != null)
            {
                List<Verb> allVerbs = pawn.equipment.Primary.GetComp<CompEquippable>().AllVerbs;
                foreach (Verb v in allVerbs)
                    if (v.verbProps.isPrimary)
                        return v.IsIncendiary_Melee() || v.IsIncendiary_Ranged();
            }
			return false;
		}
	}
}
