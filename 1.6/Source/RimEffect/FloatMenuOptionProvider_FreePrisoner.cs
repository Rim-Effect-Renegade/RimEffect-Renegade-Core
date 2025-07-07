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
    public class FloatMenuOptionProvider_FreePrisoner : FloatMenuOptionProvider
    {
        public override bool Drafted => true;
        public override bool Undrafted => true;
        public override bool Multiselect => false;
        public override bool RequiresManipulation => true;

        public override FloatMenuOption GetSingleOptionFor(Pawn clickedPawn, FloatMenuContext context)
        {
            if (clickedPawn == null || clickedPawn.Dead || !clickedPawn.health.hediffSet.HasHediff(global::RimEffect.RE_DefOf.RE_TurnBackToFormerFaction))
            {
                return null;
            }
            FloatMenuOption resultOption;
            if (!context.FirstSelectedPawn.CanReach(clickedPawn, PathEndMode.Touch, Danger.Deadly))
            {
                resultOption = new FloatMenuOption("CannotGoNoPath".Translate(), null, MenuOptionPriority.Default, null, null, 0f, null, null);
            }
            else
            {
                resultOption = FloatMenuUtility.DecoratePrioritizedTask(new FloatMenuOption("RE.FreeHostages".Translate(), delegate ()
                {
                    context.FirstSelectedPawn.jobs.TryTakeOrderedJob(new Job(RE_DefOf.RE_SaveHostages, clickedPawn), JobTag.Misc);
                }, MenuOptionPriority.RescueOrCapture, null, clickedPawn, 0f, null, null), context.FirstSelectedPawn, clickedPawn, "ReservedBy");
            }
            return resultOption;
        }
    }
}
