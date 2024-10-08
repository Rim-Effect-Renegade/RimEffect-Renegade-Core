﻿using System;
using Verse;
using RimWorld;

namespace RimEffect
{
    public class CompProperties_PlantHarmRadiusIfBroken : CompProperties
    {
        public CompProperties_PlantHarmRadiusIfBroken()
        {
            this.compClass = typeof(CompPlantHarmRadiusIfBroken);
        }

        public float harmFrequencyPerArea = 0.011f;

        public float leaflessPlantKillChance = 0.5f;

        public SimpleCurve radiusPerDayCurve;
    }
}