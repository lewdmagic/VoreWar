#region

using System;
using UnityEngine;

#endregion

internal static class Humans
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Default<OverSizeParameters>, builder =>
    {
        builder.Setup(output =>
        {
            output.DickSizes = () => 6;
            output.BreastSizes = () => 8;

            output.BodySizes = 3;
            output.EyeTypes = 6;
            output.SpecialAccessoryCount = 0;
            output.HairStyles = 36;
            output.MouthTypes = 12;
            output.AccessoryColors = 0;
            output.HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UniversalHair);
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.RedSkin);
            output.EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);
            output.BodyAccentTypes1 = 6; // eyebrows
            output.BeardStyles = 7;

            output.ExtendedBreastSprites = true;
            output.AllowedMainClothingTypes.Set(
                GenericTop1.GenericTop1Instance,
                GenericTop2.GenericTop2Instance,
                GenericTop3.GenericTop3Instance,
                GenericTop4.GenericTop4Instance,
                GenericTop5.GenericTop5Instance,
                GenericTop6.GenericTop6Instance,
                GenericTop7.GenericTop7Instance,
                MaleTop.MaleTopInstance,
                MaleTop2.MaleTop2Instance,
                MaleTop3.MaleTop3Instance,
                MaleTop4.MaleTop4Instance,
                MaleTop5.MaleTop5Instance,
                MaleTop6.MaleTop6Instance,
                Uniform1.Uniform1Instance,
                FemaleOnePiece1.FemaleOnePiece1Instance,
                FemaleOnePiece2.FemaleOnePiece2Instance,
                FemaleOnePiece3.FemaleOnePiece3Instance,
                FemaleOnePiece4.FemaleOnePiece4Instance
            );
            output.AvoidedMainClothingTypes = 3;
            output.AvoidedEyeTypes = 0;
            output.AllowedWaistTypes.Set(
                GenericBot1.GenericBot1Instance,
                GenericBot2.GenericBot2Instance,
                GenericBot3.GenericBot3Instance,
                GenericBot4.GenericBot4Instance,
                GenericBot5.GenericBot5Instance,
                GenericBot6.GenericBot6Instance,
                Uniform2.Uniform2Instance,
                BigLoin.BigLoinInstance,
                Pants1.Pants1Instance,
                Pants2.Pants2Instance,
                Skirt.SkirtInstance
            );
            output.ExtraMainClothing1Types.Set(
            );

            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing50Spaced);
        });

        builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
        {
            output.Coloring(FurryColor(input.Actor));
            if (input.Actor.IsEating)
            {
                if (input.Actor.Unit.HasBreasts)
                {
                    output.Sprite(input.Actor.Unit.BodySize > 1 ? input.Sprites.HumansBodySprites2[4] : input.Sprites.HumansBodySprites2[1]);
                }
                else
                {
                    output.Sprite(input.Sprites.HumansBodySprites2[7 + input.Actor.Unit.BodySize * 3]);
                }
            }
            else if (input.Actor.IsAttacking)
            {
                if (input.Actor.Unit.HasBreasts)
                {
                    output.Sprite(input.Actor.Unit.BodySize > 1 ? input.Sprites.HumansBodySprites2[5] : input.Sprites.HumansBodySprites2[2]);
                }
                else
                {
                    output.Sprite(input.Sprites.HumansBodySprites2[8 + input.Actor.Unit.BodySize * 3]);
                }
            }
            else
            {
                if (input.Actor.Unit.HasBreasts)
                {
                    output.Sprite(input.Actor.Unit.BodySize > 1 ? input.Sprites.HumansBodySprites2[3] : input.Sprites.HumansBodySprites2[0]);
                }
                else
                {
                    output.Sprite(input.Sprites.HumansBodySprites2[6 + input.Actor.Unit.BodySize * 3]);
                }
            }
        });

        builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.IsDead && input.Actor.Unit.Items != null)
            {
                output.Sprite(input.Sprites.HumansBodySprites2[69]);
            }
            else
            {
                output.Sprite(input.Sprites.HumansBodySprites3[24 + 4 * input.Actor.Unit.EyeType + (input.Actor.IsAttacking || input.Actor.IsEating ? 0 : 2)]);
            }
        });

        builder.RenderSingle(SpriteType.SecondaryEyes, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            if (input.Actor.Unit.IsDead && input.Actor.Unit.Items != null)
            {
            }
            else
            {
                output.Sprite(input.Sprites.HumansBodySprites3[25 + 4 * input.Actor.Unit.EyeType + (input.Actor.IsAttacking || input.Actor.IsEating ? 0 : 2)]);
            }
        });


        builder.RenderSingle(SpriteType.Mouth, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsEating || input.Actor.IsAttacking)
            {
            }
            else
            {
                output.Sprite(input.Sprites.HumansBodySprites3[108 + input.Actor.Unit.MouthType]);
            }
        });

        builder.RenderSingle(SpriteType.Hair, 21, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.HumansBodySprites2[71 + 2 * input.Actor.Unit.HairStyle]);
        });

        builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.HumansBodySprites2[72 + 2 * input.Actor.Unit.HairStyle]);
        });

        builder.RenderSingle(SpriteType.Hair3, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.HumansBodySprites3[120 + input.Actor.Unit.BodyAccentType1]);
        }); // Eyebrows

        builder.RenderSingle(SpriteType.Beard, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.BeardStyle == 6)
            {
            }
            else
            {
                output.Sprite(input.Sprites.HumansBodySprites3[126 + input.Actor.Unit.BeardStyle]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
        {
            output.Coloring(FurryColor(input.Actor));
            if (input.Actor.Unit.HasWeapon == false)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.HumansBodySprites1[3 + input.Actor.Unit.BodySize * 4 + (input.Actor.Unit.HasBreasts ? 0 : 12)]);
                    return;
                }

                output.Sprite(input.Sprites.HumansBodySprites1[0 + input.Actor.Unit.BodySize * 4 + (input.Actor.Unit.HasBreasts ? 0 : 12)]);
                return;
            }

            switch (input.Actor.GetWeaponSprite())
            {
                case 0:
                    output.Sprite(input.Sprites.HumansBodySprites1[2 + input.Actor.Unit.BodySize * 4 + (input.Actor.Unit.HasBreasts ? 0 : 12)]);
                    return;
                case 1:
                    output.Sprite(input.Sprites.HumansBodySprites1[3 + input.Actor.Unit.BodySize * 4 + (input.Actor.Unit.HasBreasts ? 0 : 12)]);
                    return;
                case 2:
                    output.Sprite(input.Sprites.HumansBodySprites1[1 + input.Actor.Unit.BodySize * 4 + (input.Actor.Unit.HasBreasts ? 0 : 12)]);
                    return;
                case 3:
                    output.Sprite(input.Sprites.HumansBodySprites1[3 + input.Actor.Unit.BodySize * 4 + (input.Actor.Unit.HasBreasts ? 0 : 12)]);
                    return;
                case 4:
                    output.Sprite(input.Sprites.HumansBodySprites1[2 + input.Actor.Unit.BodySize * 4 + (input.Actor.Unit.HasBreasts ? 0 : 12)]);
                    return;
                case 5:
                    output.Sprite(input.Sprites.HumansBodySprites1[1 + input.Actor.Unit.BodySize * 4 + (input.Actor.Unit.HasBreasts ? 0 : 12)]);
                    return;
                case 6:
                    output.Sprite(input.Sprites.HumansBodySprites1[2 + input.Actor.Unit.BodySize * 4 + (input.Actor.Unit.HasBreasts ? 0 : 12)]);
                    return;
                case 7:
                    output.Sprite(input.Sprites.HumansBodySprites1[1 + input.Actor.Unit.BodySize * 4 + (input.Actor.Unit.HasBreasts ? 0 : 12)]);
                    return;
                default:
                    output.Sprite(input.Sprites.HumansBodySprites1[0 + input.Actor.Unit.BodySize * 4 + (input.Actor.Unit.HasBreasts ? 0 : 12)]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon == false)
            {
                return;
            }

            switch (input.Actor.GetWeaponSprite())
            {
                case 0:
                    return;
                case 1:
                    return;
                case 2:
                    return;
                case 3:
                    return;
                case 4:
                    output.Sprite(input.Sprites.HumansBodySprites3[137]);
                    return;
                case 5:
                    return;
                case 6:
                    output.Sprite(input.Sprites.HumansBodySprites3[140]);
                    return;
                case 7:
                    return;
                default:
                    return;
            }
        }); // Extra weapon sprite

        builder.RenderSingle(SpriteType.BodyAccent2, 0, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon == false)
            {
                return;
            }

            switch (input.Actor.GetWeaponSprite())
            {
                case 0:
                    output.Sprite(input.Sprites.HumansBodySprites3[143]).Layer(22);
                    return;
                case 1:
                    output.Sprite(input.Sprites.HumansBodySprites3[143]).Layer(22);
                    return;
                case 2:
                    output.Sprite(input.Sprites.HumansBodySprites3[143]).Layer(22);
                    return;
                case 3:
                    output.Sprite(input.Sprites.HumansBodySprites3[143]).Layer(22);
                    return;
                case 4:
                    output.Sprite(input.Sprites.HumansBodySprites3[142]).Layer(0);
                    return;
                case 5:
                    output.Sprite(input.Sprites.HumansBodySprites3[142]).Layer(0);
                    return;
                case 6:
                    output.Sprite(input.Sprites.HumansBodySprites3[142]).Layer(0);
                    return;
                case 7:
                    output.Sprite(input.Sprites.HumansBodySprites3[142]).Layer(0);
                    return;
                default:
                    return;
            }
        }); // Back weapon sprite

        builder.RenderSingle(SpriteType.BodyAccessory, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.HumansBodySprites3[0]);
        }); // Ears
        builder.RenderSingle(SpriteType.Breasts, 17, (input, output) =>
        {
            output.Coloring(FurryColor(input.Actor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[31]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[30]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[29]);
                    return;
                }

                if (leftSize > 28)
                {
                    leftSize = 28;
                }

                output.Sprite(input.Sprites.HumansVoreSprites[0 + leftSize]);
            }
            else
            {
                output.Sprite(input.Sprites.HumansVoreSprites[0 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.SecondaryBreasts, 17, (input, output) =>
        {
            output.Coloring(FurryColor(input.Actor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[63]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[62]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[61]);
                    return;
                }

                if (rightSize > 28)
                {
                    rightSize = 28;
                }

                output.Sprite(input.Sprites.HumansVoreSprites[32 + rightSize]);
            }
            else
            {
                output.Sprite(input.Sprites.HumansVoreSprites[32 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
        {
            output.Coloring(FurryColor(input.Actor));
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(31, 0.7f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[105]).AddOffset(0, -33 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[104]).AddOffset(0, -33 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[103]).AddOffset(0, -33 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[102]).AddOffset(0, -33 * .625f);
                    return;
                }

                switch (size)
                {
                    case 26:
                        output.AddOffset(0, -14 * .625f);
                        break;
                    case 27:
                        output.AddOffset(0, -17 * .625f);
                        break;
                    case 28:
                        output.AddOffset(0, -20 * .625f);
                        break;
                    case 29:
                        output.AddOffset(0, -25 * .625f);
                        break;
                    case 30:
                        output.AddOffset(0, -27 * .625f);
                        break;
                    case 31:
                        output.AddOffset(0, -32 * .625f);
                        break;
                }

                output.Sprite(input.Sprites.HumansVoreSprites[70 + size]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 11, (input, output) =>
        {
            output.Coloring(FurryColor(input.Actor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect())
            {
                if (input.Actor.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32)) < 16)
                {
                    output.Sprite(input.Sprites.HumansBodySprites4[1 + input.Actor.Unit.DickSize * 2 + (input.Actor.Unit.BodySize > 1 ? 12 : 0) + (!input.Actor.Unit.HasBreasts ? 24 : 0)]).Layer(20);
                    return;
                }

                output.Sprite(input.Sprites.HumansBodySprites4[0 + input.Actor.Unit.DickSize * 2 + (input.Actor.Unit.BodySize > 1 ? 12 : 0) + (!input.Actor.Unit.HasBreasts ? 24 : 0)]).Layer(13);
                return;
            }

            output.Sprite(input.Sprites.HumansBodySprites4[0 + input.Actor.Unit.DickSize * 2 + (input.Actor.Unit.BodySize > 1 ? 12 : 0) + (!input.Actor.Unit.HasBreasts ? 24 : 0)]).Layer(11);
        });

        builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
        {
            output.Coloring(FurryColor(input.Actor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect() && input.Actor.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32)) < 16)
            {
                output.Layer(19);
            }
            else
            {
                output.Layer(10);
            }

            int size = input.Actor.Unit.DickSize;
            int offset = input.Actor.GetBallSize(28, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.HumansVoreSprites[141]).AddOffset(0, -22 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.HumansVoreSprites[140]).AddOffset(0, -22 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.HumansVoreSprites[139]).AddOffset(0, -22 * .625f);
                return;
            }

            if (offset >= 26)
            {
                output.AddOffset(0, -22 * .625f);
            }
            else if (offset == 25)
            {
                output.AddOffset(0, -16 * .625f);
            }
            else if (offset == 24)
            {
                output.AddOffset(0, -13 * .625f);
            }
            else if (offset == 23)
            {
                output.AddOffset(0, -11 * .625f);
            }
            else if (offset == 22)
            {
                output.AddOffset(0, -10 * .625f);
            }
            else if (offset == 21)
            {
                output.AddOffset(0, -7 * .625f);
            }
            else if (offset == 20)
            {
                output.AddOffset(0, -6 * .625f);
            }
            else if (offset == 19)
            {
                output.AddOffset(0, -4 * .625f);
            }
            else if (offset == 18)
            {
                output.AddOffset(0, -1 * .625f);
            }

            if (offset > 0)
            {
                output.Sprite(input.Sprites.HumansVoreSprites[Math.Min(112 + offset, 138)]);
                return;
            }

            output.Sprite(input.Sprites.HumansVoreSprites[106 + size]);
        });

        builder.RenderSingle(SpriteType.Weapon, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                switch (input.Actor.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.HumansBodySprites3[132]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.HumansBodySprites3[133]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.HumansBodySprites3[134]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.HumansBodySprites3[135]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.HumansBodySprites3[136]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.HumansBodySprites3[138]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.HumansBodySprites3[139]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.HumansBodySprites3[141]);
                        return;
                    default:
                        return;
                }
            }
        });


        builder.RunBefore((input, output) =>
        {
            if (input.Actor.Unit.HasBreasts)
            {
                if (input.Actor.Unit.BodySize > 1)
                {
                    output.changeSprite(SpriteType.Balls).AddOffset(0, 3 * .625f);
                    output.changeSprite(SpriteType.Belly).AddOffset(0, 1 * .625f);
                }
                else
                {
                    output.changeSprite(SpriteType.Balls).AddOffset(0, 3 * .625f);
                    output.changeSprite(SpriteType.Belly).AddOffset(0, 1 * .625f);
                }
            }
            else
            {
                if (input.Actor.Unit.BodySize > 1)
                {
                    output.changeSprite(SpriteType.Balls).AddOffset(0, 1 * .625f);
                    output.changeSprite(SpriteType.Belly).AddOffset(0, 1 * .625f);
                }
                else
                {
                    output.changeSprite(SpriteType.Balls).AddOffset(0, 0);
                    output.changeSprite(SpriteType.Belly).AddOffset(0, 1 * .625f);
                }
            }

            if (input.Actor.GetWeaponSprite() == 0 || input.Actor.GetWeaponSprite() == 4 ||
                input.Actor.GetWeaponSprite() == 6)
            {
                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.Unit.BodySize > 1)
                    {
                        output.changeSprite(SpriteType.Weapon).AddOffset(-1 * .625f, 0);
                    }
                    else
                    {
                        output.changeSprite(SpriteType.Weapon).AddOffset(0, 0);
                    }
                }
                else
                {
                    if (input.Actor.Unit.BodySize > 1)
                    {
                        output.changeSprite(SpriteType.Weapon).AddOffset(1 * .625f, -1 * .625f);
                    }
                    else
                    {
                        output.changeSprite(SpriteType.Weapon).AddOffset(0, -1 * .625f);
                    }
                }
            }
            else if (input.Actor.GetWeaponSprite() == 1 || input.Actor.GetWeaponSprite() == 3)
            {
                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.Unit.BodySize > 1)
                    {
                        output.changeSprite(SpriteType.Weapon).AddOffset(0, -1 * .625f);
                    }
                    else
                    {
                        output.changeSprite(SpriteType.Weapon).AddOffset(0, 0);
                    }
                }
                else
                {
                    if (input.Actor.Unit.BodySize > 1)
                    {
                        output.changeSprite(SpriteType.Weapon).AddOffset(3 * .625f, -3 * .625f);
                    }
                    else
                    {
                        output.changeSprite(SpriteType.Weapon).AddOffset(3 * .625f, -4 * .625f);
                    }
                }
            }
            else if (input.Actor.GetWeaponSprite() == 2)
            {
                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.Unit.BodySize > 1)
                    {
                        output.changeSprite(SpriteType.Weapon).AddOffset(-1 * .625f, 2 * .625f);
                    }
                    else
                    {
                        output.changeSprite(SpriteType.Weapon).AddOffset(-2 * .625f, 3 * .625f);
                    }
                }
                else
                {
                    if (input.Actor.Unit.BodySize > 1)
                    {
                        output.changeSprite(SpriteType.Weapon).AddOffset(0, 0);
                    }
                    else
                    {
                        output.changeSprite(SpriteType.Weapon).AddOffset(0, 0);
                    }
                }
            }
            else if (input.Actor.GetWeaponSprite() == 5 || input.Actor.GetWeaponSprite() == 7)
            {
                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.Unit.BodySize > 1)
                    {
                        output.changeSprite(SpriteType.Weapon).AddOffset(1 * .625f, -1 * .625f);
                    }
                    else
                    {
                        output.changeSprite(SpriteType.Weapon).AddOffset(0, 0);
                    }
                }
                else
                {
                    if (input.Actor.Unit.BodySize > 1)
                    {
                        output.changeSprite(SpriteType.Weapon).AddOffset(2 * .625f, -3 * .625f);
                    }
                    else
                    {
                        output.changeSprite(SpriteType.Weapon).AddOffset(2 * .625f, -3 * .625f);
                    }
                }
            }

            CommonRaceCode.MakeBreastOversize(32 * 32).Invoke(input, output);
            Defaults.BasicBellyRunAfter.Invoke(input, output);
        });


        builder.RandomCustom(data =>
        {
            Unit unit = data.Unit;
            Defaults.RandomCustom(data);

            if (unit.HasDick && unit.HasBreasts)
            {
                unit.HairStyle = State.Rand.Next(Config.HermsOnlyUseFemaleHair ? 18 : data.MiscRaceData.HairStyles);
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
                    unit.HairStyle = 18 + State.Rand.Next(18);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(18);
                }
            }

            if (unit.HasBreasts)
            {
                unit.BeardStyle = 6;
            }
            else
            {
                unit.BeardStyle = State.Rand.Next(6);
            }
        });
    });


    private static void ClothingShared1(IClothingRenderInput<IOverSizeParameters> input, IRaceRenderOutput clothing4)
    {
        if (input.Actor.Unit.BodySize == 2)
        {
            if (input.Actor.HasBelly)
            {
                if (input.Actor.GetStomachSize(31, 0.7f) > 12)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[36]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 11)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[35]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 10)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[34]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 9)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[33]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 8)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[32]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 7)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[31]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 6)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[30]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 5)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[29]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 4)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[28]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 3)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[27]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 2)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[26]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 1)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[25]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 0)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[24]);
                }
                else
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[23]);
                }
            }
            else
            {
                clothing4.Sprite(input.Sprites.HumenFOnePieces[22]);
            }
        }
        else
        {
            if (input.Actor.HasBelly)
            {
                if (input.Actor.GetStomachSize(31, 0.7f) > 12)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[14]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 11)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[13]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 10)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[12]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 9)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[11]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 8)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[10]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 7)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[9]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 6)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[8]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 5)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[7]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 4)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[6]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 3)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[5]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 2)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[4]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 1)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[3]);
                }
                else if (input.Actor.GetStomachSize(31, 0.7f) > 0)
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[2]);
                }
                else
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[1]);
                }
            }
            else
            {
                clothing4.Sprite(input.Sprites.HumenFOnePieces[0]);
            }
        }
    }

    private static ColorSwapPalette FurryColor(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);
        }

        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedSkin, actor.Unit.SkinColor);
    }


    private static class GenericTop1
    {
        internal static readonly IClothing<IOverSizeParameters> GenericTop1Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenFundertops[57];
                output.BlocksBreasts = true;
                output.RevealsBreasts = true;
                output.FemaleOnly = true;
                output.RevealsDick = true;
                output.Type = 60001;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(17);
                output["Clothing2"].Layer(17);
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.HumenFundertops[56]);
                    output.BlocksBreasts = false;
                    output["Clothing2"].Sprite(null);
                    output["Clothing3"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output.BlocksBreasts = true;
                    output["Clothing1"].Sprite(input.Sprites.HumenFundertops[0 + input.Actor.Unit.BreastSize]);
                    if (input.Actor.Unit.BreastSize == 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                    }
                    else if (input.Actor.Unit.BreastSize == 4)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                    }
                    else if (input.Actor.Unit.BreastSize == 5)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.Actor.Unit.BreastSize]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.Actor.Unit.BreastSize]);
                    }
                }
                else
                {
                    output.BlocksBreasts = true;
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(FurryColor(input.Actor));
                output["Clothing3"].Coloring(FurryColor(input.Actor));
            });
        });
    }

    private static class GenericTop2
    {
        internal static readonly IClothing<IOverSizeParameters> GenericTop2Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenFundertops[58];
                output.BlocksBreasts = true;
                output.RevealsBreasts = true;
                output.FemaleOnly = true;
                output.RevealsDick = true;
                output.Type = 60002;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(17);
                output["Clothing2"].Layer(17);
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                    output.BlocksBreasts = false;
                    output["Clothing2"].Sprite(null);
                    output["Clothing3"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output.BlocksBreasts = true;
                    output["Clothing1"].Sprite(input.Sprites.HumenFundertops[8 + input.Actor.Unit.BreastSize]);
                    if (input.Actor.Unit.BreastSize == 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                    }
                    else if (input.Actor.Unit.BreastSize == 4)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                    }
                    else if (input.Actor.Unit.BreastSize == 5)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.Actor.Unit.BreastSize]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.Actor.Unit.BreastSize]);
                    }
                }
                else
                {
                    output.BlocksBreasts = true;
                }

                output["Clothing2"].Coloring(FurryColor(input.Actor));
                output["Clothing3"].Coloring(FurryColor(input.Actor));
            });
        });
    }

    private static class GenericTop3
    {
        internal static readonly IClothing<IOverSizeParameters> GenericTop3Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenFundertops[60];
                output.BlocksBreasts = true;
                output.RevealsBreasts = true;
                output.FemaleOnly = true;
                output.RevealsDick = true;
                output.Type = 60003;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(17);
                output["Clothing2"].Layer(17);
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.HumenFundertops[59]);
                    output.BlocksBreasts = false;
                    output["Clothing2"].Sprite(null);
                    output["Clothing3"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output.BlocksBreasts = true;
                    output["Clothing1"].Sprite(input.Sprites.HumenFundertops[16 + input.Actor.Unit.BreastSize]);
                    if (input.Actor.Unit.BreastSize == 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                    }
                    else if (input.Actor.Unit.BreastSize == 4)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                    }
                    else if (input.Actor.Unit.BreastSize == 5)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.Actor.Unit.BreastSize]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.Actor.Unit.BreastSize]);
                    }
                }
                else
                {
                    output.BlocksBreasts = true;
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(FurryColor(input.Actor));
                output["Clothing3"].Coloring(FurryColor(input.Actor));
            });
        });
    }

    private static class GenericTop4
    {
        internal static readonly IClothing<IOverSizeParameters> GenericTop4Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenFundertops[62];
                output.BlocksBreasts = true;
                output.RevealsBreasts = true;
                output.FemaleOnly = true;
                output.RevealsDick = true;
                output.Type = 60004;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(17);
                output["Clothing2"].Layer(17);
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.HumenFundertops[61]);
                    output.BlocksBreasts = false;
                    output["Clothing2"].Sprite(null);
                    output["Clothing3"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output.BlocksBreasts = true;
                    output["Clothing1"].Sprite(input.Sprites.HumenFundertops[24 + input.Actor.Unit.BreastSize]);
                    if (input.Actor.Unit.BreastSize == 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                    }
                    else if (input.Actor.Unit.BreastSize == 4)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                    }
                    else if (input.Actor.Unit.BreastSize == 5)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.Actor.Unit.BreastSize]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.Actor.Unit.BreastSize]);
                    }
                }
                else
                {
                    output.BlocksBreasts = true;
                }

                output["Clothing2"].Coloring(FurryColor(input.Actor));
                output["Clothing3"].Coloring(FurryColor(input.Actor));
            });
        });
    }

    private static class GenericTop5
    {
        internal static readonly IClothing<IOverSizeParameters> GenericTop5Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenFundertops[64];
                output.BlocksBreasts = true;
                output.RevealsBreasts = true;
                output.FemaleOnly = true;
                output.RevealsDick = true;
                output.Type = 60005;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(17);
                output["Clothing2"].Layer(17);
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.HumenFundertops[63]);
                    output.BlocksBreasts = false;
                    output["Clothing2"].Sprite(null);
                    output["Clothing3"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output.BlocksBreasts = true;
                    output["Clothing1"].Sprite(input.Sprites.HumenFundertops[32 + input.Actor.Unit.BreastSize]);
                    if (input.Actor.Unit.BreastSize == 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                    }
                    else if (input.Actor.Unit.BreastSize == 4)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                    }
                    else if (input.Actor.Unit.BreastSize == 5)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.Actor.Unit.BreastSize]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.Actor.Unit.BreastSize]);
                    }
                }
                else
                {
                    output.BlocksBreasts = true;
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(FurryColor(input.Actor));
                output["Clothing3"].Coloring(FurryColor(input.Actor));
            });
        });
    }

    private static class GenericTop6
    {
        internal static readonly IClothing<IOverSizeParameters> GenericTop6Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenFundertops[66];
                output.BlocksBreasts = true;
                output.RevealsBreasts = true;
                output.FemaleOnly = true;
                output.RevealsDick = true;
                output.Type = 60006;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(17);
                output["Clothing2"].Layer(17);
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.HumenFundertops[65]);
                    output.BlocksBreasts = false;
                    output["Clothing2"].Sprite(null);
                    output["Clothing3"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output.BlocksBreasts = true;
                    output["Clothing1"].Sprite(input.Sprites.HumenFundertops[40 + input.Actor.Unit.BreastSize]);
                    if (input.Actor.Unit.BreastSize == 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                    }
                    else if (input.Actor.Unit.BreastSize == 4)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                    }
                    else if (input.Actor.Unit.BreastSize == 5)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.Actor.Unit.BreastSize]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.Actor.Unit.BreastSize]);
                    }
                }
                else
                {
                    output.BlocksBreasts = true;
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(FurryColor(input.Actor));
                output["Clothing3"].Coloring(FurryColor(input.Actor));
            });
        });
    }

    private static class GenericTop7
    {
        internal static readonly IClothing<IOverSizeParameters> GenericTop7Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenFundertops[68];
                output.BlocksBreasts = true;
                output.RevealsBreasts = true;
                output.FemaleOnly = true;
                output.RevealsDick = true;
                output.Type = 60007;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(17);
                output["Clothing2"].Layer(17);
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.HumenFundertops[67]);
                    output.BlocksBreasts = false;
                    output["Clothing2"].Sprite(null);
                    output["Clothing3"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output.BlocksBreasts = true;
                    output["Clothing1"].Sprite(input.Sprites.HumenFundertops[48 + input.Actor.Unit.BreastSize]);
                    if (input.Actor.Unit.BreastSize == 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                    }
                    else if (input.Actor.Unit.BreastSize == 4)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                    }
                    else if (input.Actor.Unit.BreastSize == 5)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.Actor.Unit.BreastSize]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.Actor.Unit.BreastSize]);
                    }
                }
                else
                {
                    output.BlocksBreasts = true;
                }

                output["Clothing2"].Coloring(FurryColor(input.Actor));
                output["Clothing3"].Coloring(FurryColor(input.Actor));
            });
        });
    }

    private static class MaleTop
    {
        internal static readonly IClothing MaleTopInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenMundertops[5];
                output.MaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 60008;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.HumenMundertops[0]);
            });
        });
    }

    private static class MaleTop2
    {
        internal static readonly IClothing MaleTop2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenMundertops[5];
                output.MaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 60009;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);

                output["Clothing1"].Coloring(Color.white);

                output["Clothing1"].Sprite(input.Actor.HasBelly ? input.Sprites.HumenMundertops[4] : input.Sprites.HumenMundertops[1 + input.Actor.Unit.BodySize]);
            });
        });
    }

    private static class MaleTop3
    {
        internal static readonly IClothing MaleTop3Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenMundertops[11];
                output.MaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 60010;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                output["Clothing1"].Sprite(input.Sprites.HumenMundertops[6]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class MaleTop4
    {
        internal static readonly IClothing MaleTop4Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenMundertops[11];
                output.MaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 60011;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);

                output["Clothing1"].Sprite(input.Actor.HasBelly ? input.Sprites.HumenMundertops[10] : input.Sprites.HumenMundertops[7 + input.Actor.Unit.BodySize]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class MaleTop5
    {
        internal static readonly IClothing MaleTop5Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenMundertops[14];
                output.MaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 60012;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);

                output["Clothing1"].Sprite(input.Actor.Unit.BodySize == 2 ? input.Sprites.HumenMundertops[13] : input.Sprites.HumenMundertops[12]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class MaleTop6
    {
        internal static readonly IClothing MaleTop6Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenMundertops[16];
                output.MaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 60013;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);

                output["Clothing1"].Coloring(Color.white);

                output["Clothing1"].Sprite(input.Sprites.HumenMundertops[15]);
            });
        });
    }

    private static class Uniform1
    {
        internal static readonly IClothing<IOverSizeParameters> Uniform1Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenUniform1[42];
                output.RevealsBreasts = true;
                output.Type = 60025;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(5);
                output["Clothing2"].Layer(15);
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.HumenUniform2[6]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Actor.Unit.BreastSize > 5 ? input.Sprites.HumenUniform2[6] : input.Sprites.HumenUniform2[0 + input.Actor.Unit.BreastSize]);
                }
                
                if (input.Actor.HasBelly)
                {
                    if (input.Actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumenUniform2[13 + 7 * input.Actor.Unit.BodySize + 21 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    }
                    else if (input.Actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumenUniform2[12 + 7 * input.Actor.Unit.BodySize + 21 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    }
                    else if (input.Actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumenUniform2[11 + 7 * input.Actor.Unit.BodySize + 21 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    }
                    else if (input.Actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumenUniform2[10 + 7 * input.Actor.Unit.BodySize + 21 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    }
                    else if (input.Actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumenUniform2[9 + 7 * input.Actor.Unit.BodySize + 21 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumenUniform2[8 + 7 * input.Actor.Unit.BodySize + 21 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    }
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.HumenUniform2[7 + 7 * input.Actor.Unit.BodySize + 21 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                }

                if (input.Actor.Unit.HasWeapon == false)
                {
                    if (input.Actor.IsAttacking)
                    {
                        output["Clothing3"].Sprite(input.Sprites.HumenUniform1[3 + 4 * input.Actor.Unit.BodySize + 12 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    }
                    else
                    {
                        output["Clothing3"].Sprite(input.Sprites.HumenUniform1[0 + 4 * input.Actor.Unit.BodySize + 12 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    }
                }
                else if (input.Actor.GetWeaponSprite() == 0 || input.Actor.GetWeaponSprite() == 4 || input.Actor.GetWeaponSprite() == 6)
                {
                    output["Clothing3"].Sprite(input.Sprites.HumenUniform1[2 + 4 * input.Actor.Unit.BodySize + 12 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                }
                else if (input.Actor.GetWeaponSprite() == 1 || input.Actor.GetWeaponSprite() == 3)
                {
                    output["Clothing3"].Sprite(input.Sprites.HumenUniform1[3 + 4 * input.Actor.Unit.BodySize + 12 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                }
                else
                {
                    output["Clothing3"].Sprite(input.Sprites.HumenUniform1[1 + 4 * input.Actor.Unit.BodySize + 12 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class FemaleOnePiece1
    {
        internal static readonly IClothing<IOverSizeParameters> FemaleOnePiece1Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenFOnePieces[81];
                output.BlocksBreasts = true;
                output.RevealsBreasts = true;
                output.FemaleOnly = true;
                output.OccupiesAllSlots = true;
                output.Type = 60014;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing4"].Layer(15);
                output["Clothing3"].Layer(17);
                output["Clothing2"].Layer(17);
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.HumenFOnePieces[51]);
                    output.BlocksBreasts = false;
                    output["Clothing2"].Sprite(null);
                    output["Clothing3"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output.BlocksBreasts = true;
                    output["Clothing1"].Sprite(input.Sprites.HumenFOnePieces[43 + input.Actor.Unit.BreastSize]);
                    if (input.Actor.Unit.BreastSize == 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                    }
                    else if (input.Actor.Unit.BreastSize == 4)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                    }
                    else if (input.Actor.Unit.BreastSize == 5)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.Actor.Unit.BreastSize]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.Actor.Unit.BreastSize]);
                    }
                }
                else
                {
                    output.BlocksBreasts = true;
                }

                if (input.Actor.Unit.BodySize == 2)
                {
                    if (input.Actor.HasBelly)
                    {
                        if (input.Actor.GetStomachSize(31, 0.7f) > 3)
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[42]);
                        }
                        else if (input.Actor.GetStomachSize(31, 0.7f) > 2)
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[41]);
                        }
                        else if (input.Actor.GetStomachSize(31, 0.7f) > 1)
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[40]);
                        }
                        else if (input.Actor.GetStomachSize(31, 0.7f) > 0)
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[39]);
                        }
                        else
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[38]);
                        }
                    }
                    else
                    {
                        output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[37]);
                    }
                }
                else
                {
                    if (input.Actor.HasBelly)
                    {
                        if (input.Actor.GetStomachSize(31, 0.7f) > 4)
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[21]);
                        }
                        else if (input.Actor.GetStomachSize(31, 0.7f) > 3)
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[20]);
                        }
                        else if (input.Actor.GetStomachSize(31, 0.7f) > 2)
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[19]);
                        }
                        else if (input.Actor.GetStomachSize(31, 0.7f) > 1)
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[18]);
                        }
                        else if (input.Actor.GetStomachSize(31, 0.7f) > 0)
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[17]);
                        }
                        else
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[16]);
                        }
                    }
                    else
                    {
                        output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[15]);
                    }
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(FurryColor(input.Actor));
                output["Clothing3"].Coloring(FurryColor(input.Actor));
                output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class FemaleOnePiece2
    {
        internal static readonly IClothing<IOverSizeParameters> FemaleOnePiece2Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenFOnePieces[80];
                output.BlocksBreasts = true;
                output.RevealsBreasts = true;
                output.FemaleOnly = true;
                output.OccupiesAllSlots = true;
                output.Type = 60015;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing4"].Layer(15);
                output["Clothing3"].Layer(17);
                output["Clothing2"].Layer(17);
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                    output.BlocksBreasts = false;
                    output["Clothing2"].Sprite(null);
                    output["Clothing3"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output.BlocksBreasts = true;
                    output["Clothing1"].Sprite(input.Sprites.HumenFOnePieces[52 + input.Actor.Unit.BreastSize]);
                    if (input.Actor.Unit.BreastSize == 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                    }
                    else if (input.Actor.Unit.BreastSize == 4)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                    }
                    else if (input.Actor.Unit.BreastSize == 5)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.Actor.Unit.BreastSize]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.Actor.Unit.BreastSize]);
                    }
                }
                else
                {
                    output.BlocksBreasts = true;
                }

                ClothingShared1(input, output["Clothing4"]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(FurryColor(input.Actor));
                output["Clothing3"].Coloring(FurryColor(input.Actor));
                output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class FemaleOnePiece3
    {
        internal static readonly IClothing<IOverSizeParameters> FemaleOnePiece3Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenFOnePieces[79];
                output.BlocksBreasts = true;
                output.RevealsBreasts = true;
                output.FemaleOnly = true;
                output.OccupiesAllSlots = true;
                output.Type = 60016;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing4"].Layer(15);
                output["Clothing3"].Layer(17);
                output["Clothing2"].Layer(17);
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.HumenFOnePieces[69]);
                    output.BlocksBreasts = false;
                    output["Clothing2"].Sprite(null);
                    output["Clothing3"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output.BlocksBreasts = true;
                    output["Clothing1"].Sprite(input.Sprites.HumenFOnePieces[61 + input.Actor.Unit.BreastSize]);
                    if (input.Actor.Unit.BreastSize == 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                    }
                    else if (input.Actor.Unit.BreastSize == 4)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                    }
                    else if (input.Actor.Unit.BreastSize == 5)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.Actor.Unit.BreastSize]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.Actor.Unit.BreastSize]);
                    }
                }
                else
                {
                    output.BlocksBreasts = true;
                }

                ClothingShared1(input, output["Clothing4"]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(FurryColor(input.Actor));
                output["Clothing3"].Coloring(FurryColor(input.Actor));
                output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class FemaleOnePiece4
    {
        internal static readonly IClothing<IOverSizeParameters> FemaleOnePiece4Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenFOnePieces[82];
                output.BlocksBreasts = true;
                output.RevealsBreasts = true;
                output.FemaleOnly = true;
                output.OccupiesAllSlots = true;
                output.Type = 60017;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing4"].Layer(15);
                output["Clothing3"].Layer(17);
                output["Clothing2"].Layer(17);
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.HumenFOnePieces[78]);
                    output.BlocksBreasts = false;
                    output["Clothing2"].Sprite(null);
                    output["Clothing3"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output.BlocksBreasts = true;
                    output["Clothing1"].Sprite(input.Sprites.HumenFOnePieces[70 + input.Actor.Unit.BreastSize]);
                    if (input.Actor.Unit.BreastSize == 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                    }
                    else if (input.Actor.Unit.BreastSize == 4)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                    }
                    else if (input.Actor.Unit.BreastSize == 5)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.Actor.Unit.BreastSize]);
                        output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.Actor.Unit.BreastSize]);
                    }
                }
                else
                {
                    output.BlocksBreasts = true;
                }

                if (input.Actor.Unit.BodySize == 2)
                {
                    if (input.Actor.HasBelly)
                    {
                        if (input.Actor.GetStomachSize(31, 0.7f) > 3)
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[42]);
                        }
                        else if (input.Actor.GetStomachSize(31, 0.7f) > 2)
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[41]);
                        }
                        else if (input.Actor.GetStomachSize(31, 0.7f) > 1)
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[40]);
                        }
                        else if (input.Actor.GetStomachSize(31, 0.7f) > 0)
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[39]);
                        }
                        else
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[38]);
                        }
                    }
                    else
                    {
                        output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[37]);
                    }
                }
                else
                {
                    if (input.Actor.HasBelly)
                    {
                        if (input.Actor.GetStomachSize(31, 0.7f) > 4)
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[21]);
                        }
                        else if (input.Actor.GetStomachSize(31, 0.7f) > 3)
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[20]);
                        }
                        else if (input.Actor.GetStomachSize(31, 0.7f) > 2)
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[19]);
                        }
                        else if (input.Actor.GetStomachSize(31, 0.7f) > 1)
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[18]);
                        }
                        else if (input.Actor.GetStomachSize(31, 0.7f) > 0)
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[17]);
                        }
                        else
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[16]);
                        }
                    }
                    else
                    {
                        output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[15]);
                    }
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(FurryColor(input.Actor));
                output["Clothing3"].Coloring(FurryColor(input.Actor));
                output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericBot1
    {
        internal static readonly IClothing GenericBot1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenUnderbottoms[6];
                output.RevealsBreasts = true;
                output.Type = 60018;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(12);
                output["Clothing1"].Layer(13);
                if (input.Actor.Unit.DickSize > 0)
                {
                    output["Clothing1"].Sprite(input.Actor.Unit.DickSize < 4 ? input.Sprites.HumenUnderbottoms[60] : input.Sprites.HumenUnderbottoms[61]);
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                output["Clothing2"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.HumenUnderbottoms[0 + input.Actor.Unit.BodySize] : input.Sprites.HumenUnderbottoms[3 + input.Actor.Unit.BodySize]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericBot2
    {
        internal static readonly IClothing GenericBot2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenUnderbottoms[13];
                output.RevealsBreasts = true;
                output.Type = 60019;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(12);
                output["Clothing1"].Layer(13);
                if (input.Actor.Unit.DickSize > 0)
                {
                    if (input.Actor.Unit.DickSize < 4)
                    {
                        output["Clothing1"].Sprite(input.Sprites.HumenUnderbottoms[60]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.HumenUnderbottoms[61]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                output["Clothing2"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.HumenUnderbottoms[7 + input.Actor.Unit.BodySize] : input.Sprites.HumenUnderbottoms[10 + input.Actor.Unit.BodySize]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericBot3
    {
        internal static readonly IClothing GenericBot3Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenUnderbottoms[26];
                output.RevealsBreasts = true;
                output.Type = 60020;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(12);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(13);
                output["Clothing1"].Coloring(Color.white);
                if (input.Actor.Unit.DickSize > 0)
                {
                    output["Clothing1"].Sprite(input.Actor.Unit.DickSize < 4 ? input.Sprites.HumenUnderbottoms[62] : input.Sprites.HumenUnderbottoms[63]);
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                output["Clothing2"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.HumenUnderbottoms[20 + input.Actor.Unit.BodySize] : input.Sprites.HumenUnderbottoms[23 + input.Actor.Unit.BodySize]);
            });
        });
    }

    private static class GenericBot4
    {
        internal static readonly IClothing GenericBot4Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenUnderbottoms[39];
                output.RevealsBreasts = true;
                output.Type = 60021;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(12);
                output["Clothing1"].Layer(13);
                if (input.Actor.Unit.DickSize > 0)
                {
                    output["Clothing1"].Sprite(input.Actor.Unit.DickSize < 4 ? input.Sprites.HumenUnderbottoms[60] : input.Sprites.HumenUnderbottoms[61]);
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                output["Clothing2"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.HumenUnderbottoms[33 + input.Actor.Unit.BodySize] : input.Sprites.HumenUnderbottoms[36 + input.Actor.Unit.BodySize]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericBot5
    {
        internal static readonly IClothing GenericBot5Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenUnderbottoms[52];
                output.RevealsBreasts = true;
                output.Type = 60022;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(12);
                output["Clothing1"].Layer(13);
                if (input.Actor.Unit.DickSize > 0)
                {
                    output["Clothing1"].Sprite(input.Actor.Unit.DickSize < 4 ? input.Sprites.HumenUnderbottoms[60] : input.Sprites.HumenUnderbottoms[61]);
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                output["Clothing2"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.HumenUnderbottoms[46 + input.Actor.Unit.BodySize] : input.Sprites.HumenUnderbottoms[49 + input.Actor.Unit.BodySize]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericBot6
    {
        internal static readonly IClothing GenericBot6Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenUnderbottoms[59];
                output.RevealsBreasts = true;
                output.Type = 60023;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(12);
                output["Clothing1"].Layer(13);
                if (input.Actor.Unit.DickSize > 0)
                {
                    output["Clothing1"].Sprite(input.Actor.Unit.DickSize < 4 ? input.Sprites.HumenUnderbottoms[60] : input.Sprites.HumenUnderbottoms[61]);
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                output["Clothing2"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.HumenUnderbottoms[53 + input.Actor.Unit.BodySize] : input.Sprites.HumenUnderbottoms[56 + input.Actor.Unit.BodySize]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class Uniform2
    {
        internal static readonly IClothing Uniform2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenUniform1[43];
                output.RevealsBreasts = true;
                output.Type = 60024;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(12);
                output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.HumenUniform1[24 + input.Actor.Unit.BodySize] : input.Sprites.HumenUniform1[33 + input.Actor.Unit.BodySize]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class BigLoin
    {
        internal static readonly IClothing BigLoinInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenBigLoin[12];
                output.RevealsBreasts = true;
                output.Type = 60026;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(12);
                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.HumenBigLoin[0 + 2 * input.Actor.Unit.BodySize + (input.Actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.HumenBigLoin[6 + 2 * input.Actor.Unit.BodySize + (input.Actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class Pants1
    {
        internal static readonly IClothing Pants1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenPants[28];
                output.RevealsBreasts = true;
                output.Type = 60027;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(13);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(12);
                output["Clothing1"].Coloring(Color.white);
                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.Unit.DickSize > 0)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumenPants[24 + (input.Actor.GetStomachSize(31, 0.7f) > 3 ? 2 : 0)]);
                    }

                    output["Clothing1"].Sprite(input.Sprites.HumenPants[0 + 2 * input.Actor.Unit.BodySize + (input.Actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.HumenPants[6 + 2 * input.Actor.Unit.BodySize + (input.Actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)]);
                    output["Clothing2"].Sprite(input.Sprites.HumenPants[25 + (input.Actor.GetStomachSize(31, 0.7f) > 3 ? 2 : 0)]);
                }
            });
        });
    }

    private static class Pants2
    {
        internal static readonly IClothing Pants2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenPants[33];
                output.RevealsBreasts = true;
                output.Type = 60028;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(13);
                output["Clothing1"].Layer(12);
                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.Unit.DickSize > 0)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumenPants[29 + (input.Actor.GetStomachSize(31, 0.7f) > 3 ? 2 : 0)]);
                    }

                    output["Clothing1"].Sprite(input.Sprites.HumenPants[12 + 2 * input.Actor.Unit.BodySize + (input.Actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.HumenPants[18 + 2 * input.Actor.Unit.BodySize + (input.Actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)]);
                    output["Clothing2"].Sprite(input.Sprites.HumenPants[30 + (input.Actor.GetStomachSize(31, 0.7f) > 3 ? 2 : 0)]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class Skirt
    {
        internal static readonly IClothing SkirtInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HumenSkirt[6];
                output.RevealsBreasts = true;
                output.Type = 60029;
                output.FemaleOnly = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(12);
                output["Clothing1"].Sprite(input.Sprites.HumenSkirt[0 + 2 * input.Actor.Unit.BodySize + (input.Actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }
}