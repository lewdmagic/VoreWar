#region

using UnityEngine;

#endregion

internal static class Crypters
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Default, builder =>
    {
        Color bellyColor = new Color(.2519f, .2519f, .3584f);
        builder.Setup(output =>
        {
            output.DickSizes = () => 1;
            output.BreastSizes = () => 1;


            output.EyeTypes = 4;
            output.MouthTypes = 4;
            output.BodySizes = 7;
            output.HairStyles += 4;
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);

            output.WeightGainDisabled = true;

            output.AvoidedMainClothingTypes = 0;
            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing);
            output.AllowedMainClothingTypes.Clear();
            output.AllowedWaistTypes.Clear();

            output.AvoidedMouthTypes = 0;
            output.AvoidedEyeTypes = 0;
        });


        builder.RenderSingle(SpriteType.Head, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Crypters[2]);
        });
        builder.RenderSingle(SpriteType.Eyes, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Crypters[8 + input.Actor.Unit.EyeType]);
        });

        builder.RenderSingle(SpriteType.Mouth, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.MouthType > 3) //Defending against a weird exception.
            {
                input.Actor.Unit.MouthType = 3;
            }

            output.Sprite(input.Sprites.Crypters[37 + 2 * input.Actor.Unit.MouthType + (input.Actor.IsEating ? 1 : 0)]);
        });

        builder.RenderSingle(SpriteType.Hair, 12, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.HairStyle < 15)
            {
                output.Sprite(input.Sprites.Hair[input.Actor.Unit.HairStyle]).AddOffset(0, 1.25f);
                return;
            }

            output.Sprite(input.Sprites.Crypters[12 + input.Actor.Unit.HairStyle - 15]).AddOffset(0, 0);
        });

        builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.HairStyle < 15)
            {
                output.AddOffset(0, 1.25f);
            }
            else
            {
                output.AddOffset(0, 0);
            }

            if (input.Actor.Unit.HairStyle == 1)
            {
                output.Sprite(input.Sprites.Hair[input.RaceData.HairStyles - 4]);
            }
            else if (input.Actor.Unit.HairStyle == 2)
            {
                output.Sprite(input.Sprites.Hair[input.RaceData.HairStyles + 1 - 4]);
            }
            else if (input.Actor.Unit.HairStyle == 5)
            {
                output.Sprite(input.Sprites.Hair[input.RaceData.HairStyles + 3 - 4]);
            }
            else if (input.Actor.Unit.HairStyle == 6 || input.Actor.Unit.HairStyle == 7)
            {
                output.Sprite(input.Sprites.Hair[input.RaceData.HairStyles + 2 - 4]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Crypters[0]);
        });
        builder.RenderSingle(SpriteType.BodyAccent, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Crypters[4]);
        });
        builder.RenderSingle(SpriteType.BodyAccent2, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Crypters[5]);
        });
        builder.RenderSingle(SpriteType.BodyAccent3, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Crypters[6 + (input.Actor.IsEating ? 1 : 0)]);
        });
        builder.RenderSingle(SpriteType.BodyAccent4, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Crypters[36]);
        });
        builder.RenderSingle(SpriteType.BodyAccessory, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Crypters[28]);
        });
        builder.RenderSingle(SpriteType.SecondaryAccessory, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Crypters[3]);
        });

        builder.RenderSingle(SpriteType.BodySize, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Crypters[29 + input.Actor.Unit.BodySize]);
        });

        builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Belly].Invoke(input, output);
            output.Coloring(bellyColor);
        });
        
        
        builder.RenderSingle(SpriteType.Weapon, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CrypterWeapon, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                output.Sprite(input.Sprites.Crypters[19 + input.Actor.GetWeaponSprite()]);
            }
            else
            {
                output.Sprite(input.Sprites.Crypters[17 + (input.Actor.IsAttacking ? 1 : 0)]);
            }
        });

        builder.RandomCustom(data =>
        {
            Unit unit = data.Unit;
            Defaults.RandomCustom(data);

            if (unit.HasDick && unit.HasBreasts)
            {
                if (Config.HermsOnlyUseFemaleHair)
                {
                    unit.HairStyle = State.Rand.Next(8);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
                }
            }
            else if (unit.HasDick && Config.FemaleHairForMales)
            {
                unit.HairStyle = State.Rand.Next(15);
            }
            else if (unit.HasDick == false && Config.MaleHairForFemales)
            {
                unit.HairStyle = State.Rand.Next(15);
            }
            else
            {
                if (unit.HasDick)
                {
                    unit.HairStyle = 8 + State.Rand.Next(7);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(8);
                }
            }
        });
    });
}