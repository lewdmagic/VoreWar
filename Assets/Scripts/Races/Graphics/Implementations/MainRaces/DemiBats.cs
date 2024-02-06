#region

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Races.Graphics.Implementations.MainRaces
{
    internal static class DemiBats
    {
        private static Func<IClothingRenderInput, IOverSizeParameters> paramsCalc = CommonRaceCode.MakeOversizeFunc(32 * 32);
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            RaceFrameList frameListDemibatWings = new RaceFrameList(new[] { 0, 1, 0, 2 }, new[] { .15f, .25f, .15f, .25f });

            IClothing LeaderClothes = DemibatLeader.DemibatLeaderInstance.Create(paramsCalc);
            IClothing Rags = DemibatRags.DemibatRagsInstance;


            builder.Setup(output =>
            {
                output.Names("Bat", "Bats");
                
                
                output.FlavorText(new FlavorText(
                    new Texts {  },
                    new Texts {  },
                    new Texts { "bat", "chiropter", "demi-bat" },
                    new Dictionary<string, string>
                    {
                        [WeaponNames.Mace]        = "Push Dagger",
                        [WeaponNames.Axe]         = "Claw Katar",
                        [WeaponNames.SimpleBow]   = "Iron Throwing Knife",
                        [WeaponNames.CompoundBow] = "Steel Throwing Knife",
                    }
                ));
                
                output.SetFlavorText(FlavorType.RaceSingleDescription, new FlavorEntry("bat"), new FlavorEntry("chiropter"), new FlavorEntry("demi-bat"));
                output.SetFlavorText(FlavorType.WeaponMelee1, new FlavorEntry("Push Dagger"));
                output.SetFlavorText(FlavorType.WeaponMelee2, new FlavorEntry("Claw Katar"));
                output.SetFlavorText(FlavorType.WeaponRanged1, new FlavorEntry("Iron Throwing Knife"));
                output.SetFlavorText(FlavorType.WeaponRanged2, new FlavorEntry("Steel Throwing Knife"));
                
                
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 8,
                    StomachSize = 13,
                    HasTail = false,
                    FavoredStat = Stat.Agility,
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Flight,
                        TraitType.ArtfulDodge,
                        TraitType.Vampirism
                    },
                    RaceDescription = "",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.BodyAccessoryColor, "Fur Color");
                    buttons.SetText(ButtonType.BodyAccessoryType, "Ear Type");
                    buttons.SetText(ButtonType.BodyAccentTypes1, "Collar Fur Type");
                });
                output.TownNames(new List<string>
                {
                    "Dark Grotto",
                    "Black Hollow",
                    "Nightpoint",
                    "Echoing Cavern",
                    "Gotham",
                    "Batville",
                    "Crystal Cave",
                    "Deep Den",
                    "Sylvania",
                    "Strygos",
                });
                output.DickSizes = () => 8;
                output.BreastSizes = () => 8;

                output.BodySizes = 4;
                output.EyeTypes = 6;
                output.SpecialAccessoryCount = 17; // ears        
                output.HairStyles = 24;
                output.MouthTypes = 4;
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.DemibatSkin); // Fur colors
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.DemibatHumanSkin); // Skin colors for demi form
                output.EyeColors = ColorPaletteMap.GetPaletteCount(SwapType.ViperSkin);
                output.BodyAccentTypes1 = 4; // collar fur

                output.ExtendedBreastSprites = true;
                output.FurCapable = true;
                output.AllowedMainClothingTypes.Set(
                    GenericTop1.GenericTop1Instance.Create(paramsCalc),
                    GenericTop2.GenericTop2Instance.Create(paramsCalc),
                    GenericTop3.GenericTop3Instance.Create(paramsCalc),
                    GenericTop4.GenericTop4Instance.Create(paramsCalc),
                    GenericTop5.GenericTop5Instance.Create(paramsCalc),
                    GenericTop6.GenericTop6Instance.Create(paramsCalc),
                    GenericTop7.GenericTop7Instance.Create(paramsCalc),
                    MaleTop.MaleTopInstance,
                    MaleTop2.MaleTop2Instance,
                    Natural.NaturalInstance.Create(paramsCalc),
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

                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.AviansSkin);
            });


            builder.RunBefore((input, output) =>
            {
                Defaults.BasicBellyRunAfter.Invoke(input, output);
            });

            builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemibatHumanSkin, input.U.SkinColor));
                if (input.U.Furry)
                {
                }
                else if (input.U.HasBreasts)
                {
                    output.Sprite(input.Sprites.Demibats1[32 + input.U.BodySize + (input.A.IsAttacking ? 4 : 0)]);
                }
                else
                {
                    output.Sprite(input.Sprites.Demibats1[40 + input.U.BodySize + (input.A.IsAttacking ? 4 : 0)]);
                }
            }); // human part of demi form

            builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.EyeColor));
                output.Sprite(input.Sprites.Demibats1[100]);
            });
            builder.RenderSingle(SpriteType.SecondaryEyes, 5, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Demibats1[76 + input.U.EyeType + (input.U.Furry ? 6 : 0)]);
            }); // white/black sclera
            builder.RenderSingle(SpriteType.Mouth, 5, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsEating || input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Demibats1[73 + (input.U.Furry ? 2 : 0)]);
                    return;
                }

                output.Sprite(input.Sprites.Demibats1[72 + (input.U.Furry ? 2 : 0)]);
            }); // mouth teeths/internal

            builder.RenderSingle(SpriteType.Hair, 20, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.U.HairColor));
                output.Sprite(input.Sprites.Demibats2[0 + input.U.HairStyle]);
            }); // hair part below ears
        
        
            builder.RenderSingle(SpriteType.Hair2, 22, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.U.HairColor));
                output.Sprite(input.Sprites.Demibats2[24 + input.U.HairStyle]);
            }); // hair part above ears
            builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemibatSkin, input.U.AccessoryColor));
                if (input.A.AnimationController.frameLists == null)
                {
                    SetUpAnimations(input.Actor);
                }

                if (input.U.HasBreasts)
                {
                    output.Sprite(input.Sprites.Demibats1[0 + input.U.BodySize + (input.A.IsAttacking ? 4 : 0) + (input.U.Furry ? 16 : 0)]);
                }
                else
                {
                    output.Sprite(input.Sprites.Demibats1[8 + input.U.BodySize + (input.A.IsAttacking ? 4 : 0) + (input.U.Furry ? 16 : 0)]);
                }
            }); // fur part of demi form or full furry form

            builder.RenderSingle(SpriteType.BodyAccent, 5, (input, output) =>
            {
                output.Coloring(FurryColor(input.Actor));
                if (input.A.IsEating || input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Demibats1[71]);
                    return;
                }

                output.Sprite(input.Sprites.Demibats1[67 + input.U.MouthType]);
            }); // mouth external part

            builder.RenderSingle(SpriteType.BodyAccent2, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Demibats1[61]);
                    return;
                }

                output.Sprite(input.Sprites.Demibats1[60]);
            }); //lower claws

            builder.RenderSingle(SpriteType.BodyAccent3, 19, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemibatSkin, input.U.AccessoryColor));
                if (input.U.BodyAccentType1 == 3 || (!input.U.Furry && (input.U.ClothingType == 3 || input.U.ClothingType == 8 || input.U.ClothingType == 9 || (input.U.ClothingType == 12 && !input.U.HasBreasts))))
                {
                    return;
                }

                output.Sprite(input.Sprites.Demibats1[101 + input.U.BodyAccentType1 + (input.U.Furry ? 3 : 0)]);
            }); // collar fur

            builder.RenderSingle(SpriteType.BodyAccent4, 5, (input, output) =>
            {
                output.Coloring(FurryColor(input.Actor));
                output.Sprite(input.Sprites.Demibats1[88 + input.U.EyeType + (input.U.Furry ? 6 : 0)]);
            }); // eyebrows
            builder.RenderSingle(SpriteType.BodyAccent5, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemibatSkin, input.U.AccessoryColor));
                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.Demibats1[48]);
                    return;
                }

                if (input.A.IsAttacking)
                {
                    input.A.AnimationController.frameLists[0].currentlyActive = false;
                    input.A.AnimationController.frameLists[0].currentFrame = 0;
                    input.A.AnimationController.frameLists[0].currentTime = 0f;
                    output.Sprite(input.Sprites.Demibats1[49]);
                    return;
                }

                input.A.AnimationController.frameLists[0].currentlyActive = true;

                if (input.A.AnimationController.frameLists[0].currentTime >= frameListDemibatWings.Times[input.A.AnimationController.frameLists[0].currentFrame] && input.U.IsDead == false && input.A.IsAttacking == false)
                {
                    input.A.AnimationController.frameLists[0].currentFrame++;
                    input.A.AnimationController.frameLists[0].currentTime = 0f;

                    if (input.A.AnimationController.frameLists[0].currentFrame >= frameListDemibatWings.Frames.Length)
                    {
                        input.A.AnimationController.frameLists[0].currentFrame = 0;
                        input.A.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Demibats1[48 + frameListDemibatWings.Frames[input.A.AnimationController.frameLists[0].currentFrame]]);
            }); // wings main

            builder.RenderSingle(SpriteType.BodyAccent6, 3, (input, output) =>
            {
                output.Coloring(FurryColor(input.Actor));
                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Demibats1[52 + (input.U.Furry ? 3 : 0)]);
                    return;
                }

                if (input.A.AnimationController.frameLists[0].currentlyActive)
                {
                    output.Sprite(input.Sprites.Demibats1[51 + frameListDemibatWings.Frames[input.A.AnimationController.frameLists[0].currentFrame] + (input.U.Furry ? 3 : 0)]);
                    return;
                }

                output.Sprite(input.Sprites.Demibats1[51 + (input.U.Furry ? 3 : 0)]);
            }); // arms main

            builder.RenderSingle(SpriteType.BodyAccent7, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Demibats1[58]);
                    return;
                }

                if (input.A.AnimationController.frameLists[0].currentlyActive)
                {
                    output.Sprite(input.Sprites.Demibats1[57 + frameListDemibatWings.Frames[input.A.AnimationController.frameLists[0].currentFrame]]);
                    return;
                }

                output.Sprite(input.Sprites.Demibats1[57]);
            }); // thumbs

            builder.RenderSingle(SpriteType.BodyAccessory, 21, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemibatSkin, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Demibats1[107 + input.U.SpecialAccessoryType]);
            }); // ears
            builder.RenderSingle(SpriteType.Breasts, 17, (input, output) =>
            {
                output.Coloring(FurryColor(input.Actor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.LeftBreastFullness > 0)
                {
                    int leftSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32));

                    if (leftSize > 28)
                    {
                        leftSize = 28;
                    }

                    output.Sprite(input.Sprites.Demibats3[0 + leftSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Demibats3[0 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.SecondaryBreasts, 17, (input, output) =>
            {
                output.Coloring(FurryColor(input.Actor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.RightBreastFullness > 0)
                {
                    int rightSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32));
                
                    if (rightSize > 28)
                    {
                        rightSize = 28;
                    }

                    output.Sprite(input.Sprites.Demibats3[32 + rightSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Demibats3[32 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
            {
                output.Coloring(FurryColor(input.Actor));
                if (input.A.HasBelly)
                {
                    int size = input.A.GetStomachSize(31, 0.7f);

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
            
            
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (!input.U.Furry || !Config.FurryGenitals)
                {
                    output.Coloring(FurryColor(input.Actor));
                }

                if (input.U.Furry && Config.FurryGenitals)
                {

                    if (input.A.IsErect())
                    {
                        if (input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32)) < 16)
                        {
                            output.Layer(24);
                            if (input.A.IsCockVoring)
                            {
                                output.Sprite(input.Sprites.Demibats2[96 + input.U.DickSize]);
                                return;
                            }

                            output.Sprite(input.Sprites.Demibats2[80 + input.U.DickSize]);
                            return;
                        }

                        output.Layer(13);
                        if (input.A.IsCockVoring)
                        {
                            output.Sprite(input.Sprites.Demibats2[104 + input.U.DickSize]);
                            return;
                        }

                        output.Sprite(input.Sprites.Demibats2[88 + input.U.DickSize]);
                        return;
                    }

                    output.Layer(11); //why dis here
                    return;
                }

                if (input.A.IsErect())
                {
                    if (input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32)) < 16)
                    {
                        output.Layer(24);
                        if (input.A.IsCockVoring)
                        {
                            output.Sprite(input.Sprites.Demibats2[72 + input.U.DickSize]);
                            return;
                        }

                        output.Sprite(input.Sprites.Demibats2[64 + input.U.DickSize]);
                        return;
                    }

                    output.Layer(13);
                    if (input.A.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.Demibats2[56 + input.U.DickSize]);
                        return;
                    }

                    output.Sprite(input.Sprites.Demibats2[48 + input.U.DickSize]);
                    return;
                }

                output.Sprite(input.Sprites.Demibats2[48 + input.U.DickSize]).Layer(11);
            });

            builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
            {
                output.Coloring(FurryColor(input.Actor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (!(input.U.Furry && Config.FurryGenitals))
                {
                    output.AddOffset(0, 1 * .625f);
                }

                if (input.A.IsErect() && input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32)) < 16)
                {
                    output.Layer(19);
                }
                else
                {
                    output.Layer(10);
                }

                int size = input.U.DickSize;
                int offset = input.A.GetBallSize(28, .8f);

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
                    output.Sprite(input.Sprites.Demibats3[Math.Min(108 + offset, 134) + (input.U.Furry && Config.FurryGenitals ? 38 : 0)]);
                    return;
                }

                output.Sprite(input.Sprites.Demibats3[100 + size + (input.U.Furry && Config.FurryGenitals ? 38 : 0)]);
            });

            builder.RenderSingle(SpriteType.Weapon, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    if (input.U.Furry && input.A.BestRanged != null && input.U.BodyAccentType1 != 3)
                    {
                        output.AddOffset(0, 2 * .625f);
                    }

                    switch (input.A.GetWeaponSprite())
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
                            output.Sprite(input.Sprites.Demibats1[128 + (input.U.BodyAccentType1 == 3 || (!input.U.Furry && (input.U.ClothingType == 3 || input.U.ClothingType == 8 || input.U.ClothingType == 9 || (input.U.ClothingType == 12 && !input.U.HasBreasts))) ? 5 : 0)]).Layer(23);
                            return;
                        case 5:
                            output.Sprite(input.Sprites.Demibats1[129 + (input.U.BodyAccentType1 == 3 || (!input.U.Furry && (input.U.ClothingType == 3 || input.U.ClothingType == 8 || input.U.ClothingType == 9 || (input.U.ClothingType == 12 && !input.U.HasBreasts))) ? 5 : 0)]).Layer(23);
                            return;
                        case 6:
                            output.Sprite(input.Sprites.Demibats1[130 + (input.U.BodyAccentType1 == 3 || (!input.U.Furry && (input.U.ClothingType == 3 || input.U.ClothingType == 8 || input.U.ClothingType == 9 || (input.U.ClothingType == 12 && !input.U.HasBreasts))) ? 5 : 0)]).Layer(23);
                            return;
                        case 7:
                            output.Sprite(input.Sprites.Demibats1[131 + (input.U.BodyAccentType1 == 3 || (!input.U.Furry && (input.U.ClothingType == 3 || input.U.ClothingType == 8 || input.U.ClothingType == 9 || (input.U.ClothingType == 12 && !input.U.HasBreasts))) ? 5 : 0)]).Layer(23);
                            return;
                        default:
                            //output.Layer(6); why is this here
                            return;
                    }
                }
            });

            builder.RandomCustom(data =>
            {
                IUnitRead unit = data.Unit;
                Defaults.RandomCustom(data);

                if (unit.Type == UnitType.Leader)
                {
                    unit.ClothingType = 1 + Extensions.IndexOf(data.SetupOutput.AllowedMainClothingTypes, LeaderClothes);
                }

                if (unit.HasDick && unit.HasBreasts)
                {
                    unit.HairStyle = State.Rand.Next(Config.HermsOnlyUseFemaleHair ? 18 : data.SetupOutput.HairStyles);
                }
                else if (unit.HasDick && Config.FemaleHairForMales)
                {
                    unit.HairStyle = State.Rand.Next(data.SetupOutput.HairStyles);
                }
                else if (unit.HasDick == false && Config.MaleHairForFemales)
                {
                    unit.HairStyle = State.Rand.Next(data.SetupOutput.HairStyles);
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
                    unit.ClothingHatType = State.Rand.Next(2) == 0 ? 1 : 0;
                }

                if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
                {
                    unit.ClothingType = 1 + Extensions.IndexOf(data.SetupOutput.AllowedMainClothingTypes, Rags);
                    if (unit.ClothingType == 0) //Covers rags not in the list
                    {
                        unit.ClothingType = data.SetupOutput.AllowedMainClothingTypes.Count;
                    }
                }
            });
        });

        private static ColorSwapPalette FurryColor(IActorUnit actor)
        {
            if (actor.Unit.Furry)
            {
                return ColorPaletteMap.GetPalette(SwapType.DemibatSkin, actor.Unit.AccessoryColor);
            }

            return ColorPaletteMap.GetPalette(SwapType.DemibatHumanSkin, actor.Unit.SkinColor);
        }

        private static void SetUpAnimations(IActorUnit actor)
        {
            actor.AnimationController.frameLists = new[]
            {
                new AnimationController.FrameList(State.Rand.Next(0, 4), 0, true)
            }; // Wing controller. Index 0.
        }


        private static class GenericTop1
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop1Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[24];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.demibats/1524");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[31]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[23 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop2
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop2Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[34];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.demibats/1534");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[40]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[32 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop3
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop3Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[44];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.demibats/1544");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[49]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[41 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop4
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop4Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[55];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.demibats/1555");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing2"].Layer(18);
                    output["Clothing2"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[58]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[50 + input.U.BreastSize]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Demibats4[59]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop5
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop5Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[74];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.demibats/1574");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing2"].Layer(18);
                    output["Clothing2"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[68]);
                        output["Clothing2"].Sprite(input.Sprites.Demibats4[77]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[60 + input.U.BreastSize]);
                        output["Clothing2"].Sprite(input.Sprites.Demibats4[69 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop6
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop6Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[88];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.demibats/1588");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[81 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop7
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop7Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[44];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.demibats/1544");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[124]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[116 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
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
                    output.ClothingId = new ClothingId("base.demibats/1579");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);

                    if (input.A.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.U.BodySize == 3 ? input.Sprites.Demibats4[94] : input.Sprites.Demibats4[93]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[89 + input.U.BodySize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
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
                    output.ClothingId = new ClothingId("base.demibats/1579");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);
                    if (input.U.BodySize == 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[80]);
                    }
                    else if (input.U.BodySize == 2)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[79]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[78]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class Natural
        {
            internal static readonly BindableClothing<IOverSizeParameters> NaturalInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.OccupiesAllSlots = true;
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(7);
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats2[112 + input.U.BreastSize]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Demibats2[120]);

                    if (input.U.Furry)
                    {
                        output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.DemibatSkin, input.U.AccessoryColor));
                        output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.DemibatSkin, input.U.AccessoryColor));
                    }
                    else
                    {
                        output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.DemibatHumanSkin, input.U.SkinColor));
                        output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.DemibatHumanSkin, input.U.SkinColor));
                    }
                });
            });
        }

        private static class DemibatRags
        {
            internal static readonly IClothing DemibatRagsInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Rags[23];
                    output.RevealsDick = true;
                    output.InFrontOfDick = true;
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.demibats/207");
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

                    output["Clothing3"].Sprite(input.U.Furry ? input.Sprites.Demibats4[115] : input.Sprites.Demibats4[111]);

                    if (input.U.HasBreasts)
                    {
                        if (input.U.BreastSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Demibats4[112]);
                        }
                        else if (input.U.BreastSize < 6)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Demibats4[113]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Demibats4[114]);
                        }

                        output["Clothing2"].Sprite(input.Sprites.Demibats4[95 + input.U.BodySize + (input.A.IsAttacking ? 4 : 0)]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(null);
                        output["Clothing2"].Sprite(input.Sprites.Demibats4[103 + input.U.BodySize + (input.A.IsAttacking ? 4 : 0)]);
                    }
                });
            });
        }

        private static class DemibatLeader
        {
            internal static readonly BindableClothing<IOverSizeParameters> DemibatLeaderInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.LeaderOnly = true;
                    output.DiscardSprite = input.Sprites.Demibats4[153];
                    output.RevealsBreasts = true;
                    output.OccupiesAllSlots = true;
                    output.ClothingId = new ClothingId("base.demibats/61501");
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing3"].Layer(12);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing2"].Layer(12);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Coloring(Color.white);

                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                        output["Clothing3"].Sprite(input.Sprites.Demibats4[133 + input.U.BodySize + (input.A.IsAttacking ? 4 : 0)]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demibats4[125 + input.U.BreastSize]);
                        output["Clothing3"].Sprite(input.Sprites.Demibats4[133 + input.U.BodySize + (input.A.IsAttacking ? 4 : 0)]);
                    }
                    else
                    {
                        output["Clothing3"].Sprite(input.Sprites.Demibats4[141 + input.U.BodySize]);
                    }

                    if (input.A.HasBelly)
                    {
                        output["Clothing2"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Demibats4[145 + input.U.BodySize]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.Demibats4[149 + input.U.BodySize]);
                    }
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
                    output.ClothingId = new ClothingId("base.demibats/1521");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(12);
                    output["Clothing1"].Layer(13);
                    if (input.U.DickSize > 0)
                    {
                        if (input.U.DickSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Demibats2[129 + (input.U.BodySize == 3 ? 3 : 0)]);
                        }
                        else if (input.U.DickSize > 5)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Demibats2[131 + (input.U.BodySize == 3 ? 3 : 0)]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Demibats2[130 + (input.U.BodySize == 3 ? 3 : 0)]);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(null);
                    }

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Demibats2[121 + input.U.BodySize] : input.Sprites.Demibats2[125 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
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
                    output.ClothingId = new ClothingId("base.demibats/1537");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(13);

                    output["Clothing2"].Layer(12);

                    output["Clothing2"].Coloring(Color.white);

                    if (input.U.DickSize > 0)
                    {
                        if (input.U.DickSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Demibats4[1]);
                        }
                        else if (input.U.DickSize > 5)
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

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Demibats2[135 + input.U.BodySize] : input.Sprites.Demibats2[139 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
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
                    output.ClothingId = new ClothingId("base.demibats/1540");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(13);
                    output["Clothing2"].Layer(12);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Sprite(input.Sprites.Demibats2[143]);

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Demibats2[135 + input.U.BodySize] : input.Sprites.Demibats2[139 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
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
                    output.ClothingId = new ClothingId("base.demibats/1514");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(12);

                    output["Clothing1"].Layer(13);

                    if (input.U.DickSize > 0)
                    {
                        if (input.U.DickSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Demibats4[20]);
                        }
                        else if (input.U.DickSize > 5)
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

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Demibats4[4 + input.U.BodySize + (input.A.IsAttacking ? 4 : 0)] : input.Sprites.Demibats4[12 + input.U.BodySize + (input.A.IsAttacking ? 4 : 0)]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }


        private static class BatHat
        {
            internal static readonly IClothing BatHatInstance = ClothingBuilder.Create(builder =>
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
}