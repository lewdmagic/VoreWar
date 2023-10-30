#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

internal static class Bees
{
    private static readonly IClothing LeaderClothes = BeeLeader.BeeLeaderInstance;
    private static readonly IClothing Rags = BeeRags.BeeRagsInstance;

    internal static readonly List<IClothing> DiscardData = new List<IClothing>
    {
        Cuirass.CuirassInstance, LeaderClothes
    };

    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Default<OverSizeParameters>, builder =>
    {
        RaceFrameList frameListWings = new RaceFrameList(new[] { 0, 1, 2, 3, 2, 1 }, new[] { .05f, .05f, .05f, .05f, .05f, .05f });

        builder.Setup(output =>
        {
            output.BreastSizes = () => 8;
            output.DickSizes = () => 8;

            output.BodySizes = 4;
            output.EyeTypes = 8;
            output.SpecialAccessoryCount = 6; // antennae        
            output.HairStyles = 18;
            output.MouthTypes = 0;
            output.EyeColors = 0;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.BeeNewSkin);
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.BeeNewSkin);

            output.ExtendedBreastSprites = true;

            output.AllowedMainClothingTypes.Set(
                GenericTop1.GenericTop1Instance,
                GenericTop2.GenericTop2Instance,
                GenericTop3.GenericTop3Instance,
                GenericTop4.GenericTop4Instance,
                GenericTop5.GenericTop5Instance,
                GenericTop6.GenericTop6Instance,
                MaleTop.MaleTopInstance,
                MaleTop2.MaleTop2Instance,
                Natural.NaturalInstance,
                Cuirass.CuirassInstance,
                Cuirass2.Cuirass2Instance,
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

            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
        });


        builder.RenderSingle(SpriteType.Head, 20, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Bees1[5]);
                return;
            }

            output.Sprite(input.Sprites.Bees1[4]);
        });

        builder.RenderSingle(SpriteType.Eyes, 22, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Bees1[46 + input.Actor.Unit.EyeType]);
        });
        builder.RenderSingle(SpriteType.Mouth, 21, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Bees1[9]);
                return;
            }

            output.Sprite(input.Sprites.Bees1[8]);
        });

        builder.RenderSingle(SpriteType.Hair, 23, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Bees1[66 + input.Actor.Unit.HairStyle]);
        });
        builder.RenderSingle(SpriteType.Body, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Bees1[0]);
                return;
            }

            output.Sprite(input.Sprites.Bees1[1]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.AnimationController.frameLists[0].currentTime >= frameListWings.Times[input.Actor.AnimationController.frameLists[0].currentFrame] && input.Actor.Unit.IsDead == false)
            {
                input.Actor.AnimationController.frameLists[0].currentFrame++;
                input.Actor.AnimationController.frameLists[0].currentTime = 0f;

                if (input.Actor.AnimationController.frameLists[0].currentFrame >= frameListWings.Frames.Length)
                {
                    input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                    input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                }
            }

            output.Sprite(input.Sprites.Bees1[42 + frameListWings.Frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
        }); // Wings

        builder.RenderSingle(SpriteType.BodyAccent2, 24, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Bees1[54 + input.Actor.Unit.SpecialAccessoryType]);
        }); // Antennae 1
        builder.RenderSingle(SpriteType.BodyAccent3, 24, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Bees1[60 + input.Actor.Unit.SpecialAccessoryType]);
        }); // Antennae 2
        builder.RenderSingle(SpriteType.BodyAccent4, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Bees1[2]);
                return;
            }

            output.Sprite(input.Sprites.Bees1[3]);
        }); // Body 2

        builder.RenderSingle(SpriteType.BodyAccent5, 20, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Bees1[7]);
                return;
            }

            output.Sprite(input.Sprites.Bees1[6]);
        }); // Head 2

        builder.RenderSingle(SpriteType.BodyAccent6, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.HasWeapon == false)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Bees1[18]);
                    return;
                }

                output.Sprite(input.Sprites.Bees1[14]);
                return;
            }

            switch (input.Actor.GetWeaponSprite())
            {
                case 0:
                    output.Sprite(input.Sprites.Bees1[14]);
                    return;
                case 1:
                    output.Sprite(input.Sprites.Bees1[15]);
                    return;
                case 2:
                    output.Sprite(input.Sprites.Bees1[16]);
                    return;
                case 3:
                    output.Sprite(input.Sprites.Bees1[17]);
                    return;
                case 4:
                    output.Sprite(input.Sprites.Bees1[14]);
                    return;
                case 5:
                    output.Sprite(input.Sprites.Bees1[18]);
                    return;
                case 6:
                    output.Sprite(input.Sprites.Bees1[19]);
                    return;
                case 7:
                    output.Sprite(input.Sprites.Bees1[20]);
                    return;
                default:
                    output.Sprite(input.Sprites.Bees1[14]);
                    return;
            }
        }); // Arms

        builder.RenderSingle(SpriteType.BodyAccent7, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.IsTailVoring)
            {
                output.Sprite(input.Sprites.Bees1[11]);
                return;
            }

            output.Sprite(input.Sprites.Bees1[10]);
        }); // Legs

        builder.RenderSingle(SpriteType.BodyAccent8, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsTailVoring)
            {
                output.Sprite(input.Sprites.Bees1[13]);
                return;
            }

            output.Sprite(input.Sprites.Bees1[12]);
        }); // Lower Claws

        builder.RenderSingle(SpriteType.BodyAccent9, 19, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.ClothingType == 10 || input.Actor.Unit.ClothingType == 11)
            {
                output.Sprite(input.Sprites.Bees3[134]);
                return;
            }

            output.Sprite(input.Sprites.Bees1[26]);
        }); // Upper FLuff

        builder.RenderSingle(SpriteType.BodyAccent10, 9, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.ClothingType == 12 || (input.Actor.Unit.ClothingType2 == 4 && input.Actor.Unit.ClothingType != 9 && input.Actor.Unit.ClothingType != 13))
            {
                return;
            }

            output.Sprite(input.Sprites.Bees1[25]);
        }); // Lower Fluff

        builder.RenderSingle(SpriteType.BodyAccessory, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, input.Actor.Unit.SkinColor));
            int sizet = input.Actor.GetTailSize(3);
            if (input.Actor.IsTailVoring)
            {
                output.Sprite(input.Sprites.Bees1[34 + sizet]);
                return;
            }

            output.Sprite(input.Sprites.Bees1[27 + sizet]);
        }); // Abdomen

        builder.RenderSingle(SpriteType.SecondaryAccessory, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            switch (input.Actor.GetWeaponSprite())
            {
                case 0:
                    return;
                case 1:
                    return;
                case 2:
                    output.Sprite(input.Sprites.Bees1[21]);
                    return;
                case 3:
                    output.Sprite(input.Sprites.Bees1[22]);
                    return;
                case 4:
                    return;
                case 5:
                    return;
                case 6:
                    output.Sprite(input.Sprites.Bees1[23]);
                    return;
                case 7:
                    output.Sprite(input.Sprites.Bees1[24]);
                    return;
                default:
                    return;
            }
        }); // Upper Claws

        builder.RenderSingle(SpriteType.BodySize, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.BodySize > 0)
            {
                output.Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Bees1[89 + input.Actor.Unit.BodySize] : input.Sprites.Bees1[92 + input.Actor.Unit.BodySize]);
            }
        });

        builder.RenderSingle(SpriteType.Breasts, 17, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.Bees2[69]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.Bees2[68]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.Bees2[67]);
                    return;
                }

                if (leftSize > 28)
                {
                    leftSize = 28;
                }

                output.Sprite(input.Sprites.Bees2[38 + leftSize]);
            }
            else
            {
                output.Sprite(input.Sprites.Bees2[38 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.SecondaryBreasts, 17, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.Bees2[104]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.Bees2[103]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.Bees2[102]);
                    return;
                }

                if (rightSize > 28)
                {
                    rightSize = 28;
                }

                output.Sprite(input.Sprites.Bees2[73 + rightSize]);
            }
            else
            {
                output.Sprite(input.Sprites.Bees2[73 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.BreastShadow, 2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            int sizee = input.Actor.GetTailSize(3);
            if (input.Actor.IsTailVoring)
            {
                output.Sprite(input.Sprites.Bees1[38 + sizee]);
            }
            else if (input.Actor.GetTailSize(3) >= 1)
            {
                output.Sprite(input.Sprites.Bees1[30 + sizee]);
            }
        }); // abdomen extra


        builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(31, 0.8f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Bees2[143]).AddOffset(0, -7 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Bees2[142]).AddOffset(0, -7 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                {
                    output.Sprite(input.Sprites.Bees2[141]).AddOffset(0, -7 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.Bees2[140]).AddOffset(0, -7 * .625f);
                    return;
                }

                switch (size)
                {
                    case 30:
                        output.AddOffset(0, -1 * .625f);
                        break;
                    case 31:
                        output.AddOffset(0, -6 * .625f);
                        break;
                }

                output.Sprite(input.Sprites.Bees2[108 + size]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 11, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect())
            {
                if (input.Actor.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32)) < 16)
                {
                    output.Layer(20);
                    output.Sprite(input.Actor.IsCockVoring ? input.Sprites.Bees1[112 + input.Actor.Unit.DickSize] : input.Sprites.Bees1[96 + input.Actor.Unit.DickSize]);
                }
                else
                {
                    output.Layer(13);
                    output.Sprite(input.Actor.IsCockVoring ? input.Sprites.Bees1[120 + input.Actor.Unit.DickSize] : input.Sprites.Bees1[104 + input.Actor.Unit.DickSize]);
                }
            }

            //output.Layer(11);
        });

        builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, input.Actor.Unit.SkinColor));
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
                output.Sprite(input.Sprites.Bees2[37]);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Bees2[36]);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.Bees2[35]);
                return;
            }

            if (offset > 0)
            {
                output.Sprite(input.Sprites.Bees2[Math.Min(8 + offset, 34)]);
                return;
            }

            output.Sprite(input.Sprites.Bees2[size]);
        });

        builder.RenderSingle(SpriteType.Weapon, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                switch (input.Actor.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Bees1[84]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Bees1[85]);
                        return;
                    case 2:
                        return;
                    case 3:
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Bees1[86]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.Bees1[87]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Bees1[88]);
                        return;
                    case 7:
                        return;
                    default:
                        return;
                }
            }
        });


        builder.RunBefore(Defaults.BasicBellyRunAfter);
        builder.RandomCustom(data =>
        {
            Unit unit = data.Unit;
            Defaults.RandomCustom(data);

            if (unit.HasDick && unit.HasBreasts)
            {
                unit.HairStyle = State.Rand.Next(Config.HermsOnlyUseFemaleHair ? 12 : data.MiscRaceData.HairStyles);
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
                    unit.HairStyle = 8 + State.Rand.Next(10);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(12);
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

            if (unit.Type == UnitType.Leader)
            {
                unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypes, LeaderClothes);
            }
        });
    });


    private static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(State.Rand.Next(0, 6), 0, true)
        }; // Wing controller. Index 0.
    }


    private static bool IsOverSize(Actor_Unit actor)
    {
        if (actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            int leftSize = (int)Math.Sqrt(actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize +
                                          actor.GetLeftBreastSize(32 * 32));
            if (leftSize > actor.Unit.DefaultBreastSize)
            {
                return true;
            }
        }

        if (actor.PredatorComponent?.RightBreastFullness > 0)
        {
            int rightSize = (int)Math.Sqrt(actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize +
                                           actor.GetRightBreastSize(32 * 32));
            if (rightSize > actor.Unit.DefaultBreastSize)
            {
                return true;
            }
        }

        return false;
    }

    private static class GenericTop1
    {
        internal static readonly IClothing GenericTop1Instance = ClothingBuilder.Create(builder =>
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
                if (IsOverSize(input.Actor))
                {
                    output["Clothing1"].Sprite(input.Sprites.Bees3[32]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Bees3[24 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop2
    {
        internal static readonly IClothing GenericTop2Instance = ClothingBuilder.Create(builder =>
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
                if (IsOverSize(input.Actor))
                {
                    output["Clothing1"].Sprite(input.Sprites.Bees3[41]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Bees3[33 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop3
    {
        internal static readonly IClothing GenericTop3Instance = ClothingBuilder.Create(builder =>
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
                if (IsOverSize(input.Actor))
                {
                    output["Clothing1"].Sprite(input.Sprites.Bees3[50]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Bees3[42 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop4
    {
        internal static readonly IClothing GenericTop4Instance = ClothingBuilder.Create(builder =>
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
                if (IsOverSize(input.Actor))
                {
                    output["Clothing1"].Sprite(input.Sprites.Bees3[59]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Bees3[51 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop5
    {
        internal static readonly IClothing GenericTop5Instance = ClothingBuilder.Create(builder =>
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
                if (IsOverSize(input.Actor))
                {
                    output["Clothing1"].Sprite(input.Sprites.Bees3[68]);
                    output["Clothing2"].Sprite(input.Sprites.Bees3[77]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Bees3[60 + input.Actor.Unit.BreastSize]);
                    output["Clothing2"].Sprite(input.Sprites.Bees3[69 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop6
    {
        internal static readonly IClothing GenericTop6Instance = ClothingBuilder.Create(builder =>
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
                if (IsOverSize(input.Actor))
                {
                    output["Clothing1"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Bees3[79 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class MaleTop
    {
        internal static readonly IClothing MaleTopInstance = ClothingBuilder.Create(builder =>
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

                output["Clothing1"].Sprite(input.Actor.HasBelly ? input.Sprites.Bees3[113] : input.Sprites.Bees3[112]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class MaleTop2
    {
        internal static readonly IClothing MaleTop2Instance = ClothingBuilder.Create(builder =>
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
                output["Clothing1"].Sprite(input.Sprites.Bees3[78]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class Natural
    {
        internal static readonly IClothing NaturalInstance = ClothingBuilder.Create(builder =>
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
                if (IsOverSize(input.Actor))
                {
                    output["Clothing1"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Bees3[0 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing2"].Sprite(input.Sprites.Bees3[8]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, input.Actor.Unit.SkinColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, input.Actor.Unit.SkinColor));
            });
        });
    }

    private static class Cuirass
    {
        internal static readonly IClothing CuirassInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Bees3[133];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.FixedColor = true;
                output.Type = 391;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(25);
                output["Clothing1"].Coloring(Color.white);
                if (IsOverSize(input.Actor))
                {
                    output["Clothing1"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Bees3[115 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Bees3[123]);
                }
            });
        });
    }

    private static class Cuirass2
    {
        internal static readonly IClothing Cuirass2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Bees3[133];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.FixedColor = true;
                output.Type = 391;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(25);
                output["Clothing1"].Coloring(Color.white);
                if (IsOverSize(input.Actor))
                {
                    output["Clothing1"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Bees3[124 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Bees3[132]);
                }
            });
        });
    }

    private static class BeeRags
    {
        internal static readonly IClothing BeeRagsInstance = ClothingBuilder.Create(builder =>
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
                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.Unit.BreastSize < 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[90]);
                    }
                    else if (input.Actor.Unit.BreastSize < 6)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[91]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[92]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                output["Clothing2"].Sprite(input.Actor.IsTailVoring ? input.Sprites.Bees3[88] : input.Sprites.Bees3[87]);

                output["Clothing3"].Sprite(input.Sprites.Bees3[89]);
            });
        });
    }

    private static class BeeLeader
    {
        internal static readonly IClothing BeeLeaderInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.LeaderOnly = true;
                output.DiscardSprite = input.Sprites.Bees3[114];
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.Type = 390;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing4"].Layer(25);
                output["Clothing4"].Coloring(Color.white);
                output["Clothing3"].Layer(20);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing2"].Layer(18);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(12);
                output["Clothing1"].Coloring(Color.white);
                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing2"].Sprite(IsOverSize(input.Actor) ? input.Sprites.Bees3[104] : input.Sprites.Bees3[96 + input.Actor.Unit.BreastSize]);

                    output["Clothing1"].Sprite(input.Sprites.Bees3[93]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Bees3[94]);
                    output["Clothing2"].Sprite(input.Sprites.Bees3[95]);
                }

                if (input.Actor.GetWeaponSprite() == 3)
                {
                    output["Clothing3"].Sprite(IsOverSize(input.Actor) ? input.Sprites.Bees3[110] : input.Sprites.Bees3[107]);
                }
                else if (input.Actor.GetWeaponSprite() == 7)
                {
                    output["Clothing3"].Sprite(IsOverSize(input.Actor) ? input.Sprites.Bees3[109] : input.Sprites.Bees3[106]);
                }
                else
                {
                    output["Clothing3"].Sprite(IsOverSize(input.Actor) ? input.Sprites.Bees3[108] : input.Sprites.Bees3[105]);
                }

                output["Clothing4"].Sprite(input.Sprites.Bees3[111]);
            });
        });
    }

    private static class GenericBot1
    {
        internal static readonly IClothing GenericBot1Instance = ClothingBuilder.Create(builder =>
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
                        output["Clothing1"].Sprite(input.Sprites.Bees3[10]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[12]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[11]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                output["Clothing2"].Sprite(input.Sprites.Bees3[9]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericBot2
    {
        internal static readonly IClothing GenericBot2Instance = ClothingBuilder.Create(builder =>
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
                        output["Clothing1"].Sprite(input.Sprites.Bees3[15]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[17]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[16]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Bees3[14]);
                }

                output["Clothing2"].Sprite(input.Sprites.Bees3[13]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericBot3
    {
        internal static readonly IClothing GenericBot3Instance = ClothingBuilder.Create(builder =>
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
                output["Clothing1"].Sprite(input.Sprites.Bees3[18]);
                output["Clothing2"].Sprite(input.Sprites.Bees3[13]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericBot4
    {
        internal static readonly IClothing GenericBot4Instance = ClothingBuilder.Create(builder =>
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
                        output["Clothing1"].Sprite(input.Sprites.Bees3[21]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[23]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[22]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                output["Clothing2"].Sprite(input.Actor.IsTailVoring ? input.Sprites.Bees3[20] : input.Sprites.Bees3[19]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }
}