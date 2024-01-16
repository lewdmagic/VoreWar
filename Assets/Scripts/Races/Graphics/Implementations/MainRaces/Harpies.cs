#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.MainRaces
{
    internal static class Harpies
    {
        internal static readonly IRaceData Instance = RaceBuilder.CreateV2(Defaults.Default, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Harpy", "Harpies");
                output.FlavorText(new FlavorText(
                    new Texts { "feathered", "keening", "grounded" },
                    new Texts { "winged", "screeching", "taloned" },
                    new Texts { "harpy", "raptor", "harpyia" },
                    new Dictionary<string, string>
                    {
                        [WeaponNames.Mace]        = "Bronze Claws",
                        [WeaponNames.Axe]         = "Steel Claws",
                        [WeaponNames.SimpleBow]   = "Simple Bow",
                        [WeaponNames.CompoundBow] = "Compound Bow",
                        [WeaponNames.Claw]        = "Talons"
                    }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 8,
                    StomachSize = 15,
                    HasTail = true,
                    FavoredStat = Stat.Agility,
                    RacialTraits = new List<Traits>()
                    {
                        Traits.Flight,
                        Traits.Pathfinder,
                        Traits.KeenReflexes
                    },
                    RaceDescription = "Emerging from a portal high in the sky, the Harpyia saw a whole new land beneath them and descended looking for fresh prey. While unable to fly and hold weapons at their claws at the same time, the harpy are quite adept at fighting with their strong talons, as well as at dropping things from high above instead of using more prevalent ranged weapons.",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.ExtraColor1, "Upper Feathers");
                    buttons.SetText(ButtonType.ExtraColor2, "Middle Feathers");
                    buttons.SetText(ButtonType.ExtraColor3, "Lower Feathers");
                    buttons.SetText(ButtonType.BodyAccentTypes1, "Lower Feather brightness");
                });
                output.TownNames(new List<string>
                {
                    "The Erinyes",
                    "Strophades",
                    "Skycliffe Roost",
                    "City of Hargos",
                    "Mount Zephyr",
                    "Oracle of Arphi",
                    "Talontium",
                    "Feathesus",
                    "Nestoli",
                    "Papida",
                    "Mount Hesiod",
                    "Cult of Aeneid",
                    "Cave of Argo",
                    "Cave of Orcus",
                });
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.Fur);
                output.SpecialAccessoryCount = 3;
                output.BodySizes = 0;
                output.HairColors = ColorPaletteMap.GetPaletteCount(SwapType.Fur);
                output.ExtraColors1 = ColorPaletteMap.GetPaletteCount(SwapType.Fur);
                output.ExtraColors2 = ColorPaletteMap.GetPaletteCount(SwapType.Fur);
                output.ExtraColors3 = ColorPaletteMap.GetPaletteCount(SwapType.Fur);

                output.BodyAccentTypes1 = 2;
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
                    CommonClothing.BikiniBottomInstance,
                    CommonClothing.LoinclothInstance,
                    CommonClothing.ShortsInstance
                );
            });


            builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Head].Invoke(input, output);
                output.Coloring(Defaults.FurryColor(input.Actor));
                output.AddOffset(calcVector(input));
            });

            builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Eyes].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
                output.AddOffset(calcVector(input));
            });

            builder.RenderSingle(SpriteType.Mouth, 5, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Mouth].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Mouth, input.U.SkinColor));
                output.AddOffset(calcVector(input));
            });

            builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Hair].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Fur, input.U.HairColor));
                output.AddOffset(calcVector(input));
            });

            builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Hair2].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Fur, input.U.HairColor));
                output.AddOffset(calcVector(input));
            });

            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
                output.Sprite(input.Sprites.Harpies[input.A.GetSimpleBodySprite()]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Harpies[3 + input.A.GetSimpleBodySprite()]);
            });

            builder.RenderSingle(SpriteType.BodyAccent2, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Fur, input.U.ExtraColor1));
                output.Sprite(input.Sprites.Harpies[21 + input.A.GetSimpleBodySprite()]);
            });

            builder.RenderSingle(SpriteType.BodyAccent3, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Harpies[35 + input.A.GetSimpleBodySprite()]);
            });

            builder.RenderSingle(SpriteType.BodyAccent4, 5, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.BodyAccent4].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Fur, input.U.HairColor));
            });

            builder.RenderSingle(SpriteType.BodyAccent5, 0, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Fur, input.U.ExtraColor2));
                output.Sprite(input.Sprites.Harpies[24 + input.A.GetSimpleBodySprite()]);
            });

            builder.RenderSingle(SpriteType.BodyAccessory, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Fur, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Harpies[32 + input.U.SpecialAccessoryType]).AddOffset(calcVector(input));
            });

            builder.RenderSingle(SpriteType.SecondaryAccessory, 5, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Harpies[6 + input.A.GetSimpleBodySprite()]);
            });

            builder.RenderSingle(SpriteType.BodySize, -1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Fur, input.U.ExtraColor3));
                output.Sprite(input.Sprites.Harpies[input.U.BodyAccentType1 == 1 ? 38 + input.A.GetSimpleBodySprite() : 29 + input.A.GetSimpleBodySprite()]);
            });

            builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Breasts].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
                output.AddOffset(calcVector(input));
            });

            builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Belly].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
                output.AddOffset(calcVector(input));
            });

            builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Dick].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
                output.AddOffset(calcVector(input));
            });

            builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Balls].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
                output.AddOffset(calcVector(input));
            });

            builder.RenderSingle(SpriteType.Weapon, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon)
                {
                    output.Sprite(input.Sprites.Harpies[9 + input.A.GetSimpleBodySprite() + 3 * (input.A.GetWeaponSprite() / 2)]);
                }
                else
                {
                    output.Sprite(input.Sprites.Harpies[3 + input.A.GetSimpleBodySprite()]);
                }
            });


            builder.RunBefore((input, output) =>
            {
                output.ClothingShift = input.A.GetSimpleBodySprite() != 0 ? new Vector3(0, 10, 0) : new Vector3(0, 0, 0);
            });

            builder.RandomCustom(data =>
            {
                Unit unit = data.Unit;
                Defaults.RandomCustom(data);

                unit.BodyAccentType1 = 0;

                if (Config.ExtraRandomHairColors)
                {
                    if (data.MiscRaceData.HairColors == ColorPaletteMap.GetPaletteCount(SwapType.Fur))
                    {
                        unit.HairColor = State.Rand.Next(data.MiscRaceData.HairColors);
                        unit.AccessoryColor = State.Rand.Next(data.MiscRaceData.HairColors);
                        unit.ExtraColor1 = State.Rand.Next(data.MiscRaceData.HairColors);
                        unit.ExtraColor2 = State.Rand.Next(data.MiscRaceData.HairColors);
                        unit.ExtraColor3 = State.Rand.Next(data.MiscRaceData.HairColors);
                    }
                }
                else
                {
                    if (data.MiscRaceData.HairColors == ColorPaletteMap.GetPaletteCount(SwapType.Fur))
                    {
                        unit.HairColor = State.Rand.Next(ColorPaletteMap.MixedHairColors);
                        unit.AccessoryColor = State.Rand.Next(ColorPaletteMap.MixedHairColors);
                        unit.ExtraColor1 = State.Rand.Next(ColorPaletteMap.MixedHairColors);
                        unit.ExtraColor2 = State.Rand.Next(ColorPaletteMap.MixedHairColors);
                        unit.ExtraColor3 = State.Rand.Next(ColorPaletteMap.MixedHairColors);
                    }
                }
            });
        });

        private static Vector2 calcVector(IRaceRenderInput input)
        {
            if (input.A.GetSimpleBodySprite() != 0)
            {
                return new Vector2(0, 10);
            }

            return new Vector2(0, 0);
        }
    }
}