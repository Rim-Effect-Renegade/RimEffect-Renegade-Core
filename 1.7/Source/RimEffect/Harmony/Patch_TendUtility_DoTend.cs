using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

using HarmonyLib;

namespace RimEffect
{
    [HarmonyPatch(typeof(TendUtility), "DoTend")]
    public static class TendUtility_DoTend
    {
        [HarmonyPrefix]
        public static bool Prefix(Pawn doctor, Pawn patient, Medicine medicine)
        {
            if (medicine != null && medicine.def == RE_DefOf.RE_MediGel)
            {
                MediGelUtility.TrySealWounds(patient);
                MediGelUtility.TendAdditional(doctor, patient);
                if (medicine != null)
                {
                    if (medicine.stackCount > 1)
                    {
                        medicine.stackCount--;
                    }
                    if (!medicine.Destroyed)
                    {
                        medicine.Destroy(DestroyMode.Vanish);
                    }
                }
                return false;
            }
            return true;
        }
    }
}
