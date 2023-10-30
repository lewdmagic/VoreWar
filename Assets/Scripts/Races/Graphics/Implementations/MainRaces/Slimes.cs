#region

using UnityEngine;

#endregion

internal static class Slimes
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Default, builder =>
    {
        builder.Setup(output =>
        {
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SlimeMain);
            output.EyeTypes = 3;
            output.EyeColors = 1;
            output.HairStyles = 12;
            output.HairColors = 3;
            output.BodySizes = 2;
            //MouthTypes = 0;
            output.AvoidedMainClothingTypes = 1;
            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing);
            output.AllowedMainClothingTypes.Set(
                ClothingTypes.BeltTopInstance,
                ClothingTypes.LeotardInstance,
                ClothingTypes.BlackTopInstance,
                RaceSpecificClothing.RainCoatInstance,
                ClothingTypes.RagsInstance
            );
            output.AllowedWaistTypes.Set(
                ClothingTypes.LoinclothInstance
            );
        });


        bool SlimeWeapon(Actor_Unit actor)
        {
            return actor.GetWeaponSprite() > 1 && actor.GetWeaponSprite() < 6;
        }

        builder.RenderSingle(SpriteType.Eyes, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Slimes[35 + input.Actor.Unit.EyeType]);
        });

        builder.RenderSingle(SpriteType.Mouth, 4, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Mouth].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeSub, input.Actor.Unit.AccessoryColor));
        });

        builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeSub, 3 * input.Actor.Unit.AccessoryColor + input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Slimes[20 + input.Actor.Unit.HairStyle]);
        });
        builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeSub, 3 * input.Actor.Unit.AccessoryColor + input.Actor.Unit.HairColor));
            if (input.Actor.Unit.HairStyle == 1)
            {
                output.Sprite(input.Sprites.Slimes[32]);
                return;
            }

            if (input.Actor.Unit.HairStyle == 3)
            {
                output.Sprite(input.Sprites.Slimes[33]);
                return;
            }

            if (input.Actor.Unit.HairStyle == 2 || input.Actor.Unit.HairStyle == 4 || input.Actor.Unit.HairStyle == 7)
            {
                output.Sprite(input.Sprites.Slimes[34]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Slimes[input.Actor.GetSimpleBodySprite()]);
        });
        builder.RenderSingle(SpriteType.BodyAccent, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeSub, 3 * input.Actor.Unit.AccessoryColor + input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Slimes[4 + input.Actor.GetBodyWeight()]);
        });
        builder.RenderSingle(SpriteType.BodyAccent2, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeSub, 3 * input.Actor.Unit.AccessoryColor + input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Slimes[6 + (input.Actor.IsAttacking ? 1 : 0)]);
        });
        builder.RenderSingle(SpriteType.BodyAccessory, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeSub, 3 * input.Actor.Unit.AccessoryColor + input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Slimes[18]);
        });
        builder.RenderSingle(SpriteType.BodySize, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Actor.GetBodyWeight() == 1 ? input.Sprites.Slimes[3] : null);
        });
        builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Slimes[38 + input.Actor.Unit.BreastSize] : null);
        });
        builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, input.Actor.Unit.AccessoryColor));
            if (input.Actor.HasBelly)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                {
                    output.Sprite(input.Sprites.Slimes[69]).AddOffset(0, -25 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    if (input.Actor.GetStomachSize(15, .75f) == 15)
                    {
                        output.Sprite(input.Sprites.Slimes[68]).AddOffset(0, -25 * .625f);
                        return;
                    }

                    if (input.Actor.GetStomachSize(15, .875f) == 15)
                    {
                        output.Sprite(input.Sprites.Slimes[67]).AddOffset(0, -25 * .625f);
                        return;
                    }
                }

                output.Sprite(input.Sprites.Slimes[51 + input.Actor.GetStomachSize()]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Dick].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, input.Actor.Unit.AccessoryColor));
        });
        builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Balls].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, input.Actor.Unit.AccessoryColor));
        });
        builder.RenderSingle(SpriteType.Weapon, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (SlimeWeapon(input.Actor))
            {
                output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeSub, 3 * input.Actor.Unit.AccessoryColor + input.Actor.Unit.HairColor));
            }
            else
            {
                output.Coloring(Defaults.WhiteColored);
            }

            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                output.Sprite(input.Sprites.Slimes[8 + input.Actor.GetWeaponSprite()]);
            }
        });

        builder.RunBefore((input, output) =>
        {
            output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 2.5f);
            if (input.Actor.HasBelly)
            {
                Vector3 localScale;

                if (input.Actor.PredatorComponent.VisibleFullness > 4)
                {
                    float extraCap = input.Actor.PredatorComponent.VisibleFullness - 4;
                    float xScale = Mathf.Min(1 + extraCap / 5, 1.8f);
                    float yScale = Mathf.Min(1 + extraCap / 40, 1.1f);
                    localScale = new Vector3(xScale, yScale, 1);
                }
                else
                {
                    localScale = new Vector3(1, 1, 1);
                }

                output.ChangeSprite(SpriteType.Belly).SetLocalScale(localScale);
            }
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            if (unit.HasDick && unit.HasBreasts)
            {
                unit.HairStyle = State.Rand.Next(Config.HermsOnlyUseFemaleHair ? 8 : data.MiscRaceData.HairStyles);
            }
            else if (unit.HasDick && Config.FemaleHairForMales)
            {
                unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
            }
            else if (unit.HasDick == false && Config.MaleHairForFemales)
            {
                unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
            }
            else
            {
                if (unit.HasDick)
                {
                    unit.HairStyle = 5 + State.Rand.Next(7);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(8);
                }
            }
        });
    });

    internal static Material GetSlimeAccentMaterial(Actor_Unit actor)
    {
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeSub, 3 * actor.Unit.AccessoryColor + actor.Unit.HairColor).colorSwapMaterial;
    }
}