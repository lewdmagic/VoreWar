#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class Whisp
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup((input, output) =>
            {
                output.Names("Whisp", "Whisps");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 7,
                    StomachSize = 10,
                    HasTail = false,
                    FavoredStat = Stat.Mind,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.BreastVore, VoreType.CockVore, VoreType.Anal },
                    ExpMultiplier = 1.1f,
                    PowerAdjustment = 1.2f,
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Whispers,
                        TraitType.ForceFeeder,
                        TraitType.ForcedMetamorphosis,
                        TraitType.GreaterChangeling,
                    },
                    SpawnRace = Race.Youko,
                    RaceDescription = ""
                });
                output.CanBeGender = new List<Gender> { Gender.None };
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.MermenSkin);
            });

            builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.MermenSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Whisp[0]);
            });

            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(Defaults.Randomize);
        });
    }
}