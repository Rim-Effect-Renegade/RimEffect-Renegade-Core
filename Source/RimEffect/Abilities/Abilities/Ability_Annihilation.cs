﻿namespace RimEffect
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RimWorld;
    using RimWorld.Planet;
    using UnityEngine;
    using Verse;
    using Verse.Sound;
    using Ability = VFECore.Abilities.Ability;

    public class Ability_Annihilation : Ability
    {
        public override void Cast(params GlobalTargetInfo[] targets)
        {
            base.Cast(targets);
            foreach (GlobalTargetInfo target in targets)
            {
                AnnihilationField annihilationField = (AnnihilationField)GenSpawn.Spawn(RE_DefOf.RE_Biotic_AnnihilationField, target.Cell, this.pawn.Map);
                annihilationField.caster        = this.pawn;
                annihilationField.casterFaction = this.pawn.Faction;
                annihilationField.radius        = this.GetRadiusForPawn();
                annihilationField.damage        = this.GetPowerForPawn();
                annihilationField.startTick     = Find.TickManager.TicksGame;
                annihilationField.endTick       = Find.TickManager.TicksGame + this.GetDurationForPawn();

                if (this.def.targetFlecks.Any())
                    annihilationField.fleck = this.def.targetFlecks.First();
            }
        }

        public override void CheckCastEffects(GlobalTargetInfo[] targetInfos, out bool cast, out bool target, out bool hediffApply)
        {
            base.CheckCastEffects(targetInfos, out cast, out _, out hediffApply);
            target = false;
        }
    }

    public class AnnihilationField : Thing
    {
        private const int TickInterval = GenTicks.TickRareInterval / 8;

        public Pawn     caster;
        public Faction  casterFaction;
        public FleckDef fleck;

        public float radius;
        public float damage;
        public float startTick;
        public float endTick;

        [Unsaved] 
        public List<Pawn> tmpPawns = new List<Pawn>();
        
        [Unsaved]
        public Graphic graphic;

        public float curRotation = 0f;

        public static float rotSpeed = 2.0f;

        private Sustainer sustainer;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            this.sustainer = RE_DefOf.RE_Biotic_AnnihilationSphere_Sustainer.TrySpawnSustainer(SoundInfo.InMap(this));
        }

        public override Graphic Graphic
        {
            get
            {
                if (this.graphic == null || Math.Abs(this.graphic.drawSize.x - (this.radius * 2)) > float.Epsilon)
                    this.graphic = GraphicDatabase.Get(this.def.graphicData.graphicClass, this.def.graphicData.texPath, this.def.graphicData.shaderType.Shader,
                                                       new Vector2(this.radius * 2, this.radius * 2), this.def.graphicData.color, this.def.graphicData.colorTwo, this.def.graphicData,
                                                       this.def.graphicData.shaderParameters);
                return this.graphic;
            }
        }

        public override void Tick()
        {
            this.sustainer.Maintain();
            this.curRotation += rotSpeed % 360f;

            if ((Find.TickManager.TicksGame - this.startTick) % (this.fleck.Lifespan / 2f) == 0) 
                FleckMaker.Static(this.DrawPos, this.Map, this.fleck, this.radius);

            if (this.tmpPawns.Any() ? this.IsHashIntervalTick(TickInterval) : this.IsHashIntervalTick(GenTicks.TickRareInterval / 32))
            {
                foreach (Pawn pawn in this.tmpPawns)
                {
                    DamageInfo dinfo = new DamageInfo(DamageDefOf.Blunt, TickInterval * (this.damage / GenTicks.TicksPerRealSecond), float.MaxValue, instigator: this.caster);
                    pawn.TakeDamage(dinfo);
                }

                this.tmpPawns.Clear();
                int rangeForGrabbingPawns = Mathf.RoundToInt(this.radius * 2);
                IntVec2 rangeIntVec2 = new IntVec2(rangeForGrabbingPawns, rangeForGrabbingPawns);

                foreach (IntVec3 intVec3 in GenAdj.OccupiedRect(this.Position, this.Rotation, rangeIntVec2))
                {
                    if (!intVec3.InBounds(this.Map))
                        continue;
                    List<Thing> cellThings = this.Map.thingGrid.ThingsListAt(intVec3);
                    if (cellThings == null)
                        continue;

                    for (int i = 0; i < cellThings.Count; i++)
                    {
                        Thing t = cellThings[i];

                        if (t is Pawn p)// && (this.caster != null ? p.HostileTo(this.caster) : p.HostileTo(this.casterFaction)))
                            this.tmpPawns.Add(p);
                    }
                }
            }

            if (Find.TickManager.TicksGame > this.endTick)
                this.Destroy();
        }

        public override void DrawAt(Vector3 drawLoc, bool flip = false)
        {
            Mesh mesh = this.Graphic.MeshAt(this.Rotation);
            Quaternion quat = Quaternion.AngleAxis(this.curRotation, Vector3.up);

            Material mat = this.Graphic.MatAt(this.Rotation, this);

            Graphics.DrawMesh(mesh, drawLoc, quat, mat, 0);
        }

        public override void DeSpawn(DestroyMode mode = DestroyMode.Vanish)
        {
            this.sustainer?.End();

            base.DeSpawn(mode);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_References.Look(ref this.caster,        nameof(this.caster));
            Scribe_References.Look(ref this.casterFaction, nameof(this.casterFaction));
            Scribe_Values.Look(ref this.radius, nameof(this.radius));
            Scribe_Values.Look(ref this.damage, nameof(this.damage));

            Scribe_Values.Look(ref this.curRotation, nameof(this.curRotation));
        }
    }
}
