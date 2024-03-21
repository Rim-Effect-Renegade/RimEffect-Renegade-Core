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
    [DefOf]
    public static class RE_DefOf
    {
        public static ThingDef RE_AmmoCryoBelt;
        public static ThingDef RE_AmmoDisruptorBelt;
        public static ThingDef RE_AmmoExplosiveBelt;
        public static ThingDef RE_AmmoIncendiaryBelt;
        public static ThingDef RE_AmmoPiercingBelt;
        public static ThingDef RE_AmmoToxicBelt;

        public static HediffDef Hypothermia;
        public static HediffDef RE_HypothermicSlowdown;
        public static DamageDef RE_BombNoShake;

        public static SoundDef RE_Ammo_Enable;
        public static JobDef RE_PyjakNuzzle;

        public static ThoughtDef RE_Thought_PyjakNuzzle;
        public static InteractionDef RE_Interaction_PyjakNuzzle;
        public static FactionDef RE_SystemsAlliance;
        public static HediffDef RE_Spectre;
        public static PawnKindDef RE_Pyjak;
        public static ThingDef RE_PrefabWall;
        public static ThingDef RE_Turret_MassAccelerator;
        public static ThingDef RE_PrefabBarricade;

        public static PawnKindDef RE_Mechanoids_LOKI;
        public static PawnKindDef RE_Mechanoids_YMIR;
        public static HediffDef RE_TargetingVI;

        public static ThingDef RE_Mechanoids_LOKIBase;
        public static ThingDef RE_Mechanoids_YMIRBase;


        public static JobDef         RE_UseCommsConsole;
        public static DutyDef        RE_AssaultEnemies;
        public static PawnKindDef    RE_Colonist;
        public static HediffDef      RE_BioticAmpHediff;
        public static StatDef        RE_BioticAbilityCostMultiplier;
        public static StatDef        RE_BioticEnergyMax;
        public static StatDef        RE_BioticEnergyRecoveryRate;
        public static ThingDef       RE_Biotic_SingularityThing;
        public static ThingDef       RE_Biotic_AnnihilationField;
        public static ThingDef       RE_Biotic_FlareThing;
        public static HediffDef      RE_OmniToolHediff;
        public static SoundDef       RE_Biotic_AnnihilationSphere_Sustainer;
        public static MentalStateDef RE_SabotageBerserk;
        public static MentalStateDef RE_DominationBerserk;

        public static ThingDef RE_MediGel;

        public static ThingDef    RE_ProtheanBeacon;
        public static ThingDef    RE_CrashedKodiakShuttle;
        public static JobDef      RE_OpenBeacon;
        public static JobDef      RE_SaveHostages;
        public static JobDef      RE_LeaveMap;
        public static HediffDef   RE_TurnBackToFormerFaction;
        public static PawnKindDef RE_Alliance_Marine;
        public static ThingDef    RE_SpacerTechResearchBench;
        public static HediffDef   RE_BioticNatural;

        public static QuestScriptDef RE_ColonyGrowth;
    }
}
 