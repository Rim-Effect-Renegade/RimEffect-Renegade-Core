﻿using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace RimEffect
{


    [StaticConstructorOnStartup]
    [HarmonyPatch(typeof(PawnRenderUtility), "DrawEquipmentAndApparelExtras")]
    public static class Patch_PawnRenderUtility_DrawEquipmentAndApparelExtras
    {
        private static readonly Graphic graphic = GraphicDatabase.Get(typeof(Graphic_Single), @"Things/AbilityEffects/Omniblade/Omniblade", ShaderTypeDefOf.Cutout.Shader,
                                                                      new Vector2(1, 1), Color.white, Color.white);

        delegate bool CarryWeaponOpenly(Pawn pawn);

        private static readonly CarryWeaponOpenly carryOpenlyDelegate = AccessTools.MethodDelegate<CarryWeaponOpenly>(AccessTools.Method(typeof(PawnRenderUtility), "CarryWeaponOpenly"));

        [HarmonyPostfix]
        public static void Postfix(Pawn pawn)
        {
            if (!pawn.Dead && pawn.RaceProps.Humanlike && pawn.Spawned)
            {
                if ((pawn.equipment?.PrimaryEq is null && ((pawn.stances.curStance is Stance_Busy stanceBusy && !stanceBusy.neverAimWeapon && stanceBusy.focusTarg.IsValid) || carryOpenlyDelegate(pawn))) ||
                    (!(pawn.equipment?.PrimaryEq?.PrimaryVerb.IsMeleeAttack ?? true) && pawn.mindState.MeleeThreatStillThreat))
                {
                    if (pawn.health.hediffSet.HasHediff(RE_DefOf.RE_OmniToolHediff))
                    {
                        Vector3 drawLoc = pawn.DrawPos;
                        float aimAngle = 0f;

                        if (pawn.Rotation == Rot4.South)
                        {
                            drawLoc += new Vector3(0f, 0f, -0.22f);
                            drawLoc.y += 9f / 245f;
                            aimAngle = 143f;
                        }
                        else if (pawn.Rotation == Rot4.North)
                        {
                            drawLoc += new Vector3(0f, 0f, -0.11f);
                            drawLoc.y += 0f;
                            aimAngle = 143f;
                        }
                        else if (pawn.Rotation == Rot4.East)
                        {
                            drawLoc += new Vector3(0.2f, 0f, -0.22f);
                            drawLoc.y += 9f / 245f;
                            aimAngle = 143f;
                        }
                        else if (pawn.Rotation == Rot4.West)
                        {
                            drawLoc += new Vector3(-0.2f, 0f, -0.22f);
                            drawLoc.y += 9f / 245f;
                            aimAngle = 217f;
                        }



                        Mesh mesh;
                        float num = aimAngle - 90f;
                        float equippedAngleOffset = -65f;

                        if (aimAngle > 20f && aimAngle < 160f)
                        {
                            mesh = MeshPool.plane10;
                            num += equippedAngleOffset;
                        }
                        else if (aimAngle > 200f && aimAngle < 340f)
                        {
                            mesh = MeshPool.plane10Flip;
                            num -= 180f;
                            num -= equippedAngleOffset;
                        }
                        else
                        {
                            mesh = MeshPool.plane10;
                            num += equippedAngleOffset;
                        }

                        num %= 360f;
                        Graphics.DrawMesh(material: graphic.MatSingle, mesh: mesh, position: drawLoc, rotation: Quaternion.AngleAxis(num, Vector3.up), layer: 0);
                    }
                }
            }
        }
        public static Pawn GetPawnAsHolder(this Thing thing)
        {
            if (thing.ParentHolder is Pawn_EquipmentTracker pawn_EquipmentTracker)
            {
                return pawn_EquipmentTracker.pawn;
            }
            if (thing.ParentHolder is Pawn_ApparelTracker pawn_ApparelTracker)
            {
                return pawn_ApparelTracker.pawn;
            }
            return null;
        }
    }
}