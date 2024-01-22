using System.Collections.Generic;

namespace Races.Graphics.Implementations.MainRaces
{
    internal static class Scylla
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Scylla", "Scylla");
                output.WallType(WallType.Scylla);
                output.FlavorText(new FlavorText(
                    new Texts { "loose limbed", "aquatic", "ten-limbed" },
                    new Texts { "tentacled", "aquatic", "ten-limbed" },
                    new Texts { "scylla", "octopod", "aquanoid" },
                    new Dictionary<string, string>
                    {
                        [WeaponNames.Mace]        = "Knife",
                        [WeaponNames.Axe]         = "Trident",
                        [WeaponNames.SimpleBow]   = "Javelin",
                        [WeaponNames.CompoundBow] = "Medusa Launcher",
                        [WeaponNames.Claw]        = "Tentacle"
                    }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 15,
                    StomachSize = 15,
                    HasTail = false,
                    FavoredStat = Stat.Will,
                    RacialTraits = new List<TraitType>()
                    {
                        //Traits.Aquatic,
                        TraitType.TentacleHarassment
                    },
                    RaceDescription = "Trapped under the surface at their old world, the Scylla surged forth when the appearance of mystical portals gave them passage to lands above water. Their many tentacles seem to act as if having minds of their own, hindering and harassing their enemies.",
                });
                output.TownNames(new List<string>
                {
                    "City of the Conch",
                    "Lost City of Vorantis",
                    "The Trident",
                    "Seafoama",
                    "The Tidepool",
                    "Clamonia",
                    "Baiae",
                    "Olous",
                    "Heracleion",
                    "Pavlopetri",
                    "Ravenser",
                    "Atlit Yum",
                });
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.LizardMain);
                output.BodySizes = 4;
                output.AvoidedMainClothingTypes = 1;
                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.Clothing);
                output.AllowedMainClothingTypes.Set(
                    CommonClothing.BikiniTopInstance,
                    CommonClothing.BeltTopInstance,
                    CommonClothing.StrapTopInstance,
                    CommonClothing.LeotardInstance,
                    CommonClothing.BlackTopInstance,
                    CommonClothing.RagsInstance
                );
                output.AllowedWaistTypes.Set(
                    CommonClothing.LoinclothInstance
                );
            });

            builder.RenderSingle(SpriteType.Head, Defaults.SpriteGens3[SpriteType.Head]);
            builder.RenderSingle(SpriteType.Eyes, Defaults.SpriteGens3[SpriteType.Eyes]);
            builder.RenderSingle(SpriteType.Mouth, Defaults.SpriteGens3[SpriteType.Mouth]);
            builder.RenderSingle(SpriteType.Hair, Defaults.SpriteGens3[SpriteType.Hair]);
            builder.RenderSingle(SpriteType.Hair2, Defaults.SpriteGens3[SpriteType.Hair2]);
        
        
            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
                output.Sprite(input.Sprites.Scylla[24 + (input.A.IsAttacking ? 1 : 0) + 2 * input.U.BodySize + (input.U.HasBreasts ? 0 : 8)]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Scylla[0]);
            });
            builder.RenderSingle(SpriteType.BodyAccent2, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Scylla[1]);
            });

            builder.RenderSingle(SpriteType.BodyAccessory, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Scylla[0]);
            });
            builder.RenderSingle(SpriteType.SecondaryAccessory, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Scylla[0]);
            });


            builder.RenderSingle(SpriteType.BodySize, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Scylla[3 + input.U.BodySize + (input.U.HasBreasts ? 0 : 7)]);
            });
            builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Breasts].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
            });
        
            builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Belly].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
            });
        
            builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Dick].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
            });

            builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Balls].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
            });



            builder.RenderSingle(SpriteType.Weapon, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    int sprite = input.A.GetWeaponSprite();
                    if (sprite == 5)
                    {
                        return;
                    }

                    if (sprite > 5)
                    {
                        sprite--;
                    }

                    output.Sprite(input.Sprites.Scylla[15 + sprite]);
                }
            });

            builder.RenderSingle(SpriteType.BackWeapon, Defaults.SpriteGens3[SpriteType.BackWeapon]);

            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(Defaults.RandomCustom);
        });
    }
}