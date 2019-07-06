using RimWorld;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.InspirationChip
{
   public class HediffCompProperties_Inspiration : HediffCompProperties
   {
      public HediffCompProperties_Inspiration() => compClass = typeof(HediffComp_Inspiration);

      public InspirationDef inspirationDef;
      public float minDays;
      public float maxDays;
   }
}
