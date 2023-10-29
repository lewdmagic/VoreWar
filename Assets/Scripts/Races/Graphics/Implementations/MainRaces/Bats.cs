#region

using System;
using UnityEngine;

#endregion

internal static class Bats
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Default<OverSizeParameters>, builder =>
    {
        RaceFrameList frameListDemibatWings = new RaceFrameList(new[] { 0, 1, 0, 2 }, new[] { .15f, .25f, .15f, .25f });

        IClothing<IOverSizeParameters> LeaderClothes = DemibatLeader.DemibatLeaderInstance;
        IClothing Rags = DemibatRags.DemibatRagsInstance;


        builder.Setup(output =>
        {
            output.DickSizes = () => 8;
            output.BreastSizes = () => 8;

            output.BodySizes = 4;
            output.EyeTypes = 6;
            output.SpecialAccessoryCount = 17; // ears        
            output.HairStyles = 24;
            output.MouthTypes = 4;
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemibatSkin); // Fur colors
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemibatHumanSkin); // Skin colors for demi form
            output.EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.ViperSkin);
            output.BodyAccentTypes1 = 4; // collar fur

            output.ExtendedBreastSprites = true;
            output.FurCapable = true;
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
                Natural.NaturalInstance,
                Rags,
                LeaderClothes
            );
            output.AvoidedMainClothingTypes = 2;
            output.AvoidedEyeTypes = 0;
            output.AllowedWaistTypes.Set(
                GenericBot1.GenericBot1Instance,
                GenericBot2.GenericBot2Instance,
                GenericBot3.GenericBot3Instance,
                GenericBot4.GenericBot4Instance
            );

            output.AllowedClothingHatTypes.Set(
                BatHat.BatHatInstance
            );

            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
        });


        builder.RunBefore((input, output) =>
        {
            CommonRaceCode.MakeBreastOversize(32 * 32).Invoke(input, output);
            Defaults.BasicBellyRunAfter.Invoke(input, output);
        });

        builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatHumanSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.Furry)
            {
            }
            else if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Demibats1[32 + input.Actor.Unit.BodySize + (input.Actor.IsAttacking ? 4 : 0)]);
            }
            else
            {
                output.Sprite(input.Sprites.Demibats1[40 + input.Actor.Unit.BodySize + (input.Actor.IsAttacking ? 4 : 0)]);
            }
        }); // human part of demi form

        builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Demibats1[100]);
        });
        builder.RenderSingle(SpriteType.SecondaryEyes, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Demibats1[76 + input.Actor.Unit.EyeType + (input.Actor.Unit.Furry ? 6 : 0)]);
        }); // white/black sclera
        builder.RenderSingle(SpriteType.Mouth, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsEating || input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Demibats1[73 + (input.Actor.Unit.Furry ? 2 : 0)]);
                return;
            }

            output.Sprite(input.Sprites.Demibats1[72 + (input.Actor.Unit.Furry ? 2 : 0)]);
        }); // mouth teeths/internal

        builder.RenderSingle(SpriteType.Hair, 20, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Demibats2[0 + input.Actor.Unit.HairStyle]);
        }); // hair part below ears
        
        
        builder.RenderSingle(SpriteType.Hair2, 22, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Demibats2[24 + input.Actor.Unit.HairStyle]);
        }); // hair part above ears
        builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Demibats1[0 + input.Actor.Unit.BodySize + (input.Actor.IsAttacking ? 4 : 0) + (input.Actor.Unit.Furry ? 16 : 0)]);
            }
            else
            {
                output.Sprite(input.Sprites.Demibats1[8 + input.Actor.Unit.BodySize + (input.Actor.IsAttacking ? 4 : 0) + (input.Actor.Unit.Furry ? 16 : 0)]);
            }
        }); // fur part of demi form or full furry form

        builder.RenderSingle(SpriteType.BodyAccent, 5, (input, output) =>
        {
            output.Coloring(FurryColor(input.Actor));
            if (input.Actor.IsEating || input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Demibats1[71]);
                return;
            }

            output.Sprite(input.Sprites.Demibats1[67 + input.Actor.Unit.MouthType]);
        }); // mouth external part

        builder.RenderSingle(SpriteType.BodyAccent2, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Demibats1[61]);
                return;
            }

            output.Sprite(input.Sprites.Demibats1[60]);
        }); //lower claws

        builder.RenderSingle(SpriteType.BodyAccent3, 19, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType1 == 3 || (!input.Actor.Unit.Furry && (input.Actor.Unit.ClothingType == 3 || input.Actor.Unit.ClothingType == 8 || input.Actor.Unit.ClothingType == 9 || (input.Actor.Unit.ClothingType == 12 && !input.Actor.Unit.HasBreasts))))
            {
                return;
            }

            output.Sprite(input.Sprites.Demibats1[101 + input.Actor.Unit.BodyAccentType1 + (input.Actor.Unit.Furry ? 3 : 0)]);
        }); // collar fur

        builder.RenderSingle(SpriteType.BodyAccent4, 5, (input, output) =>
        {
            output.Coloring(FurryColor(input.Actor));
            output.Sprite(input.Sprites.Demibats1[88 + input.Actor.Unit.EyeType + (input.Actor.Unit.Furry ? 6 : 0)]);
        }); // eyebrows
        builder.RenderSingle(SpriteType.BodyAccent5, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatSkin, input.Actor.Unit.AccessoryColor));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Demibats1[48]);
                return;
            }

            if (input.Actor.IsAttacking)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                output.Sprite(input.Sprites.Demibats1[49]);
                return;
            }

            input.Actor.AnimationController.frameLists[0].currentlyActive = true;

            if (input.Actor.AnimationController.frameLists[0].currentTime >= frameListDemibatWings.times[input.Actor.AnimationController.frameLists[0].currentFrame] && input.Actor.Unit.IsDead == false && input.Actor.IsAttacking == false)
            {
                input.Actor.AnimationController.frameLists[0].currentFrame++;
                input.Actor.AnimationController.frameLists[0].currentTime = 0f;

                if (input.Actor.AnimationController.frameLists[0].currentFrame >= frameListDemibatWings.frames.Length)
                {
                    input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                    input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                }
            }

            output.Sprite(input.Sprites.Demibats1[48 + frameListDemibatWings.frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
        }); // wings main

        builder.RenderSingle(SpriteType.BodyAccent6, 3, (input, output) =>
        {
            output.Coloring(FurryColor(input.Actor));
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Demibats1[52 + (input.Actor.Unit.Furry ? 3 : 0)]);
                return;
            }

            if (input.Actor.AnimationController.frameLists[0].currentlyActive)
            {
                output.Sprite(input.Sprites.Demibats1[51 + frameListDemibatWings.frames[input.Actor.AnimationController.frameLists[0].currentFrame] + (input.Actor.Unit.Furry ? 3 : 0)]);
                return;
            }

            output.Sprite(input.Sprites.Demibats1[51 + (input.Actor.Unit.Furry ? 3 : 0)]);
        }); // arms main

        builder.RenderSingle(SpriteType.BodyAccent7, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Demibats1[58]);
                return;
            }

            if (input.Actor.AnimationController.frameLists[0].currentlyActive)
            {
                output.Sprite(input.Sprites.Demibats1[57 + frameListDemibatWings.frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                return;
            }

            output.Sprite(input.Sprites.Demibats1[57]);
        }); // thumbs

        builder.RenderSingle(SpriteType.BodyAccessory, 21, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Demibats1[107 + input.Actor.Unit.SpecialAccessoryType]);
        }); // ears
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
                    output.Sprite(input.Sprites.Demibats3[31]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.Demibats3[30]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.Demibats3[29]);
                    return;
                }

                if (leftSize > 28)
                {
                    leftSize = 28;
                }

                output.Sprite(input.Sprites.Demibats3[0 + leftSize]);
            }
            else
            {
                output.Sprite(input.Sprites.Demibats3[0 + input.Actor.Unit.BreastSize]);
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
                if (rightSize > input.Actor.Unit.DefaultBreastSize)
                {
                    input.Params.Oversize = true;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.Demibats3[63]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.Demibats3[62]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.Demibats3[61]);
                    return;
                }

                if (rightSize > 28)
                {
                    rightSize = 28;
                }

                output.Sprite(input.Sprites.Demibats3[32 + rightSize]);
            }
            else
            {
                output.Sprite(input.Sprites.Demibats3[32 + input.Actor.Unit.BreastSize]);
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
                    output.Sprite(input.Sprites.Demibats3[99]).AddOffset(0, -31 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Demibats3[98]).AddOffset(0, -31 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                {
                    output.Sprite(input.Sprites.Demibats3[97]).AddOffset(0, -31 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.Demibats3[96]).AddOffset(0, -31 * .625f);
                    return;
                }

                switch (size)
                {
                    case 26:
                        output.AddOffset(0, -5 * .625f);
                        break;
                    case 27:
                        output.AddOffset(0, -10 * .625f);
                        break;
                    case 28:
                        output.AddOffset(0, -15 * .625f);
                        break;
                    case 29:
                        output.AddOffset(0, -18 * .625f);
                        break;
                    case 30:
                        output.AddOffset(0, -24 * .625f);
                        break;
                    case 31:
                        output.AddOffset(0, -30 * .625f);
                        break;
                }

                output.Sprite(input.Sprites.Demibats3[64 + size]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 11, (input, output) =>
        {
            
            
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (!input.Actor.Unit.Furry || !Config.FurryGenitals)
            {
                output.Coloring(FurryColor(input.Actor));
            }

            if (input.Actor.Unit.Furry && Config.FurryGenitals)
            {

                if (input.Actor.IsErect())
                {
                    if (input.Actor.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32)) < 16)
                    {
                        output.Layer(24);
                        if (input.Actor.IsCockVoring)
                        {
                            output.Sprite(input.Sprites.Demibats2[96 + input.Actor.Unit.DickSize]);
                            return;
                        }

                        output.Sprite(input.Sprites.Demibats2[80 + input.Actor.Unit.DickSize]);
                        return;
                    }

                    output.Layer(13);
                    if (input.Actor.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.Demibats2[104 + input.Actor.Unit.DickSize]);
                        return;
                    }

                    output.Sprite(input.Sprites.Demibats2[88 + input.Actor.Unit.DickSize]);
                    return;
                }

                output.Layer(11); //why dis here
                return;
            }

            if (input.Actor.IsErect())
            {
                if (input.Actor.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32)) < 16)
                {
                    output.Layer(24);
                    if (input.Actor.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.Demibats2[72 + input.Actor.Unit.DickSize]);
                        return;
                    }

                    output.Sprite(input.Sprites.Demibats2[64 + input.Actor.Unit.DickSize]);
                    return;
                }

                output.Layer(13);
                if (input.Actor.IsCockVoring)
                {
                    output.Sprite(input.Sprites.Demibats2[56 + input.Actor.Unit.DickSize]);
                    return;
                }

                output.Sprite(input.Sprites.Demibats2[48 + input.Actor.Unit.DickSize]);
                return;
            }

            output.Sprite(input.Sprites.Demibats2[48 + input.Actor.Unit.DickSize]).Layer(11);
        });

        builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
        {
            output.Coloring(FurryColor(input.Actor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (!(input.Actor.Unit.Furry && Config.FurryGenitals))
            {
                output.AddOffset(0, 1 * .625f);
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
                output.Sprite(input.Sprites.Demibats3[137 + (input.Actor.Unit.Furry && Config.FurryGenitals ? 38 : 0)]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Demibats3[136 + (input.Actor.Unit.Furry && Config.FurryGenitals ? 38 : 0)]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.Demibats3[135 + (input.Actor.Unit.Furry && Config.FurryGenitals ? 38 : 0)]).AddOffset(0, -18 * .625f);
                return;
            }

            if (offset >= 26)
            {
                output.AddOffset(0, -15 * .625f);
            }
            else if (offset == 25)
            {
                output.AddOffset(0, -9 * .625f);
            }
            else if (offset == 24)
            {
                output.AddOffset(0, -7 * .625f);
            }
            else if (offset == 23)
            {
                output.AddOffset(0, -6 * .625f);
            }
            else if (offset == 22)
            {
                output.AddOffset(0, -3 * .625f);
            }
            else if (offset == 21)
            {
                output.AddOffset(0, -2 * .625f);
            }

            if (offset > 0)
            {
                output.Sprite(input.Sprites.Demibats3[Math.Min(108 + offset, 134) + (input.Actor.Unit.Furry && Config.FurryGenitals ? 38 : 0)]);
                return;
            }

            output.Sprite(input.Sprites.Demibats3[100 + size + (input.Actor.Unit.Furry && Config.FurryGenitals ? 38 : 0)]);
        });

        builder.RenderSingle(SpriteType.Weapon, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                if (input.Actor.Unit.Furry && input.Actor.BestRanged != null && !(input.Actor.Unit.BodyAccentType1 == 3))
                {
                    output.AddOffset(0, 2 * .625f);
                }

                switch (input.Actor.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Demibats1[124]).Layer(6);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Demibats1[125]).Layer(6);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Demibats1[126]).Layer(6);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Demibats1[127]).Layer(6);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Demibats1[128 + (input.Actor.Unit.BodyAccentType1 == 3 || (!input.Actor.Unit.Furry && (input.Actor.Unit.ClothingType == 3 || input.Actor.Unit.ClothingType == 8 || input.Actor.Unit.ClothingType == 9 || (input.Actor.Unit.ClothingType == 12 && !input.Actor.Unit.HasBreasts))) ? 5 : 0)]).Layer(23);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.Demibats1[129 + (input.Actor.Unit.BodyAccentType1 == 3 || (!input.Actor.Unit.Furry && (input.Actor.Unit.ClothingType == 3 || input.Actor.Unit.ClothingType == 8 || input.Actor.Unit.ClothingType == 9 || (input.Actor.Unit.ClothingType == 12 && !input.Actor.Unit.HasBreasts))) ? 5 : 0)]).Layer(23);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Demibats1[130 + (input.Actor.Unit.BodyAccentType1 == 3 || (!input.Actor.Unit.Furry && (input.Actor.Unit.ClothingType == 3 || input.Actor.Unit.ClothingType == 8 || input.Actor.Unit.ClothingType == 9 || (input.Actor.Unit.ClothingType == 12 && !input.Actor.Unit.HasBreasts))) ? 5 : 0)]).Layer(23);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Demibats1[131 + (input.Actor.Unit.BodyAccentType1 == 3 || (!input.Actor.Unit.Furry && (input.Actor.Unit.ClothingType == 3 || input.Actor.Unit.ClothingType == 8 || input.Actor.Unit.ClothingType == 9 || (input.Actor.Unit.ClothingType == 12 && !input.Actor.Unit.HasBreasts))) ? 5 : 0)]).Layer(23);
                        return;
                    default:
                        //output.Layer(6); why is this here
                        return;
                }
            }
        });

        builder.RandomCustom(data =>
        {
            Unit unit = data.Unit;
            Defaults.RandomCustom(data);

            if (unit.Type == UnitType.Leader)
            {
                unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypes, LeaderClothes);
            }

            if (unit.HasDick && unit.HasBreasts)
            {
                if (Config.HermsOnlyUseFemaleHair)
                {
                    unit.HairStyle = State.Rand.Next(18);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
                }
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
                    unit.HairStyle = 10 + State.Rand.Next(14);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(18);
                }
            }

            unit.BodyAccentType1 = 0;

            if (Config.WinterActive())
            {
                if (State.Rand.Next(2) == 0)
                {
                    unit.ClothingHatType = 1;
                }
                else
                {
                    unit.ClothingHatType = 0;
                }
            }

            if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
            {
                unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypes, Rags);
                if (unit.ClothingType == 0) //Covers rags not in the list
                {
                    unit.ClothingType = data.MiscRaceData.AllowedMainClothingTypes.Count;
                }
            }
        });
    });

    private static ColorSwapPalette FurryColor(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatSkin, actor.Unit.AccessoryColor);
        }

        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatHumanSkin, actor.Unit.SkinColor);
    }

    internal static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(State.Rand.Next(0, 4), 0, true)
        }; // Wing controller. Index 0.
    }


    private static class GenericTop1
    {
        internal static IClothing<IOverSizeParameters> GenericTop1Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians4[24];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1524;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats4[31]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats4[23 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop2
    {
        internal static IClothing<IOverSizeParameters> GenericTop2Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians4[34];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1534;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats4[40]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats4[32 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop3
    {
        internal static IClothing<IOverSizeParameters> GenericTop3Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians4[44];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1544;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats4[49]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats4[41 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop4
    {
        internal static IClothing<IOverSizeParameters> GenericTop4Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians4[55];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1555;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                output["Clothing2"].Layer(18);
                output["Clothing2"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats4[58]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats4[50 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing2"].Sprite(input.Sprites.Demibats4[59]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop5
    {
        internal static IClothing<IOverSizeParameters> GenericTop5Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians4[74];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1574;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                output["Clothing2"].Layer(18);
                output["Clothing2"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats4[68]);
                    output["Clothing2"].Sprite(input.Sprites.Demibats4[77]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats4[60 + input.Actor.Unit.BreastSize]);
                    output["Clothing2"].Sprite(input.Sprites.Demibats4[69 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop6
    {
        internal static IClothing<IOverSizeParameters> GenericTop6Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians4[88];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1588;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats4[81 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop7
    {
        internal static IClothing<IOverSizeParameters> GenericTop7Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians4[44];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1544;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats4[124]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats4[116 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class MaleTop
    {
        internal static IClothing MaleTopInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians4[79];
                output.MaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1579;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);

                if (input.Actor.HasBelly)
                {
                    if (input.Actor.Unit.BodySize == 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[94]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[93]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats4[89 + input.Actor.Unit.BodySize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class MaleTop2
    {
        internal static IClothing MaleTop2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians4[79];
                output.MaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1579;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                if (input.Actor.Unit.BodySize == 3)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats4[80]);
                }
                else if (input.Actor.Unit.BodySize == 2)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats4[79]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats4[78]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class Natural
    {
        internal static IClothing<IOverSizeParameters> NaturalInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(7);
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats2[112 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing2"].Sprite(input.Sprites.Demibats2[120]);

                if (input.Actor.Unit.Furry)
                {
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatSkin, input.Actor.Unit.AccessoryColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatSkin, input.Actor.Unit.AccessoryColor));
                }
                else
                {
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatHumanSkin, input.Actor.Unit.SkinColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatHumanSkin, input.Actor.Unit.SkinColor));
                }
            });
        });
    }

    private static class DemibatRags
    {
        internal static IClothing DemibatRagsInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Rags[23];
                output.RevealsDick = true;
                output.InFrontOfDick = true;
                output.RevealsBreasts = true;
                output.Type = 207;
                output.OccupiesAllSlots = true;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(20);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing2"].Layer(12);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);

                if (input.Actor.Unit.Furry)
                {
                    output["Clothing3"].Sprite(input.Sprites.Demibats4[115]);
                }
                else
                {
                    output["Clothing3"].Sprite(input.Sprites.Demibats4[111]);
                }

                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.Unit.BreastSize < 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[112]);
                    }
                    else if (input.Actor.Unit.BreastSize < 6)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[113]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[114]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Demibats4[95 + input.Actor.Unit.BodySize + (input.Actor.IsAttacking ? 4 : 0)]);
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                    output["Clothing2"].Sprite(input.Sprites.Demibats4[103 + input.Actor.Unit.BodySize + (input.Actor.IsAttacking ? 4 : 0)]);
                }
            });
        });
    }

    private static class DemibatLeader
    {
        internal static IClothing<IOverSizeParameters> DemibatLeaderInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.LeaderOnly = true;
                output.DiscardSprite = input.Sprites.Demibats4[153];
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.Type = 61501;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(12);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing2"].Layer(12);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);

                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                    output["Clothing3"].Sprite(input.Sprites.Demibats4[133 + input.Actor.Unit.BodySize + (input.Actor.IsAttacking ? 4 : 0)]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats4[125 + input.Actor.Unit.BreastSize]);
                    output["Clothing3"].Sprite(input.Sprites.Demibats4[133 + input.Actor.Unit.BodySize + (input.Actor.IsAttacking ? 4 : 0)]);
                }
                else
                {
                    output["Clothing3"].Sprite(input.Sprites.Demibats4[141 + input.Actor.Unit.BodySize]);
                }

                if (input.Actor.HasBelly)
                {
                    output["Clothing2"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing2"].Sprite(input.Sprites.Demibats4[145 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Demibats4[149 + input.Actor.Unit.BodySize]);
                }
            });
        });
    }

    private static class GenericBot1
    {
        internal static IClothing GenericBot1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians3[121];
                output.RevealsBreasts = true;
                output.Type = 1521;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(12);
                output["Clothing1"].Layer(13);
                if (input.Actor.Unit.DickSize > 0)
                {
                    if (input.Actor.Unit.DickSize < 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats2[129 + (input.Actor.Unit.BodySize == 3 ? 3 : 0)]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats2[131 + (input.Actor.Unit.BodySize == 3 ? 3 : 0)]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats2[130 + (input.Actor.Unit.BodySize == 3 ? 3 : 0)]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing2"].Sprite(input.Sprites.Demibats2[121 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Demibats2[125 + input.Actor.Unit.BodySize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericBot2
    {
        internal static IClothing GenericBot2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians3[137];
                output.RevealsBreasts = true;
                output.Type = 1537;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(13);

                output["Clothing2"].Layer(12);

                output["Clothing2"].Coloring(Color.white);

                if (input.Actor.Unit.DickSize > 0)
                {
                    if (input.Actor.Unit.DickSize < 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[1]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[3]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[2]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Demibats4[0]);
                }

                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing2"].Sprite(input.Sprites.Demibats2[135 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Demibats2[139 + input.Actor.Unit.BodySize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericBot3
    {
        internal static IClothing GenericBot3Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians3[140];
                output.RevealsBreasts = true;
                output.Type = 1540;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(13);
                output["Clothing2"].Layer(12);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Demibats2[143]);

                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing2"].Sprite(input.Sprites.Demibats2[135 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Demibats2[139 + input.Actor.Unit.BodySize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericBot4
    {
        internal static IClothing GenericBot4Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians4[14];
                output.RevealsBreasts = true;
                output.Type = 1514;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(12);

                output["Clothing1"].Layer(13);

                if (input.Actor.Unit.DickSize > 0)
                {
                    if (input.Actor.Unit.DickSize < 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[20]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[22]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[21]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing2"].Sprite(input.Sprites.Demibats4[4 + input.Actor.Unit.BodySize + (input.Actor.IsAttacking ? 4 : 0)]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Demibats4[12 + input.Actor.Unit.BodySize + (input.Actor.IsAttacking ? 4 : 0)]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class BatHat
    {
        internal static IClothing BatHatInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.ReqWinterHoliday = true;
            });


            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(28);
                output["Clothing1"].Sprite(input.Sprites.HatBat);
            });
        });
    }
}