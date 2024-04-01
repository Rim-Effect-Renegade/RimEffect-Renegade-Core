﻿using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace RimEffect
{
    public class JobDriver_LeaveMap : JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            this.pawn.Map.pawnDestinationReservationManager.Reserve(this.pawn, this.job, this.job.targetA.Cell);
            return true;
        }

        public override IEnumerable<Toil> MakeNewToils()
        {
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);

            Quest quest = Find.QuestManager.QuestsListForReading.Find(q => q.QuestLookTargets.Any(look => look.HasWorldObject && Find.World.worldObjects.ObjectsAt(pawn.Map.Tile).Contains(look.WorldObject)));

            Toil finalize = new Toil();
            finalize.initAction = delegate ()
             {
                 // End related quest if no other pawn of same faction are still on the map
                 if (this.Map.mapPawns.AllPawnsSpawned.Where(predicate => predicate.Faction == pawn.Faction).ToList().Count <= 1)
                 {
                     foreach (var item in Find.QuestManager.QuestsListForReading)
                     {
                         var signal = new Signal("Quest" + quest.id + ".AllHostagesAreFree");
                         Find.SignalManager.SendSignal(signal);
                     }
                 }

                 this.pawn.ExitMap(false, Rot4.Random);
             };
            finalize.defaultCompleteMode = ToilCompleteMode.Instant;
            yield return finalize;
            yield break;
        }
    }
}