using HarmonyLib;
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
        public static ThingDef 
            RE_AmmoCryoBelt, 
            RE_AmmoDisruptorBelt, 
            RE_AmmoExplosiveBelt, 
            RE_AmmoIncendiaryBelt, 
            RE_AmmoPiercingBelt, 
            RE_AmmoToxicBelt, 
            RE_PrefabWall, 
            RE_Turret_MassAccelerator, 
            RE_PrefabBarricade, 
            RE_Mechanoids_LOKIBase, 
            RE_Mechanoids_YMIRBase,
            RE_Biotic_SingularityThing, 
            RE_Biotic_AnnihilationField, 
            RE_Biotic_FlareThing,
            RE_ProtheanBeacon,
            RE_CrashedKodiakShuttle,
            RE_MediGel,
            RE_SpacerTechResearchBench;

        public static HediffDef 
            Hypothermia, 
            RE_HypothermicSlowdown, 
            RE_TargetingVI, 
            RE_Spectre, 
            RE_BioticAmpHediff,
            RE_OmniToolHediff,
            RE_TurnBackToFormerFaction,
            RE_BioticNatural;

        public static DamageDef 
            RE_BombNoShake;

        public static SoundDef 
            RE_Ammo_Enable,
            RE_Biotic_AnnihilationSphere_Sustainer;

        public static JobDef
            RE_PyjakNuzzle,
            RE_UseCommsConsole,
            RE_OpenBeacon,
            RE_SaveHostages,
            RE_LeaveMap;

        public static ThoughtDef 
            RE_Thought_PyjakNuzzle;

        public static InteractionDef 
            RE_Interaction_PyjakNuzzle;

        public static FactionDef 
            RE_SystemsAlliance;

        public static PawnKindDef 
            RE_Pyjak, 
            RE_Mechanoids_LOKI, 
            RE_Mechanoids_YMIR,
            RE_Colonist,
            RE_Alliance_Marine;

        public static DutyDef
            RE_AssaultEnemies;

        public static StatDef
            RE_BioticAbilityCostMultiplier,
            RE_BioticEnergyMax,
            RE_BioticEnergyRecoveryRate;

        public static MentalStateDef 
            RE_SabotageBerserk,
            RE_DominationBerserk;

        public static QuestScriptDef 
            RE_ColonyGrowth;

        public static BodyPartDef
            Brain;
    }
}
 