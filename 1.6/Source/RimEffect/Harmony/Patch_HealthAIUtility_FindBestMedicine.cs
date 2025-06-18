using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

using HarmonyLib;
using Verse.AI;

namespace RimEffect
{
    [HarmonyPatch(typeof(HealthAIUtility), "FindBestMedicine")]
    static class HealthAIUtility_FindBestMedicine
    {
        [HarmonyPostfix]
        static void Postfix(Pawn healer, Pawn patient, ref Thing __result)
        {
            if (__result != null && __result.def.defName == "RE_MediGel")
            {
                if (!MediGelUtility.CanSealWounds(patient))
                {
                    return;
                }
                Predicate<Thing> validator = (Thing m) => !m.IsForbidden(healer) && patient.playerSettings.medCare.AllowsMedicine(m.def) && healer.CanReserve(m, 10, 1, null, false) && m.def.defName != "RE_MediGel";
                Func<Thing, float> priorityGetter = (Thing t) => t.def.GetStatValueAbstract(StatDefOf.MedicalPotency, null);
                __result = GenClosest.ClosestThing_Global_Reachable(patient.Position, patient.Map, patient.Map.listerThings.ThingsInGroup(ThingRequestGroup.Medicine), PathEndMode.ClosestTouch, TraverseParms.For(healer, Danger.Deadly, TraverseMode.ByPawn, false), 9999f, validator, priorityGetter);
            }
        }
    }
}
