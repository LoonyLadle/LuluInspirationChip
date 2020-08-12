using RimWorld;
using System.Linq;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.InspirationChip
{
	public class HediffComp_Inspiration : HediffComp
	{
		public HediffCompProperties_Inspiration Props => (HediffCompProperties_Inspiration)props;

		public override void CompPostMake()
		{
			base.CompPostMake();
			ResetTicksToInspire();
		}

		private void ResetTicksToInspire()
		{
			ticksToInspire = (int)(Rand.Range(Props.minDays, Props.maxDays) * 60000);
		}

		public override void CompPostTick(ref float severityAdjustment)
		{
			if (--ticksToInspire <= 0)
			{
				InspirationDef inspiration = Props.inspirationDef ?? DefDatabase<InspirationDef>.AllDefs.Where(x => x.Worker.InspirationCanOccur(Pawn)).RandomElementByWeightWithFallback(x => x.Worker.CommonalityFor(Pawn));
				TaggedString reason = "LuluInspirationChip_Reason".Translate(Pawn.Named("PAWN"), parent.LabelBase.Named("HEDIFF"));
				Pawn.mindState.inspirationHandler.TryStartInspiration_NewTemp(inspiration, reason);
				ResetTicksToInspire();
			}
		}

		public override void CompExposeData()
		{
			Scribe_Values.Look(ref ticksToInspire, nameof(ticksToInspire));
		}

		public override string CompDebugString()
		{
			return "ticksToInspire: " + ticksToInspire;
		}

		private int ticksToInspire;
	}
}
