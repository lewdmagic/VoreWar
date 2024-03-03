#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.MainRaces
{
    internal static class Bees
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            RaceFrameList frameListWings = new RaceFrameList(new[] { 0, 1, 2, 3, 2, 1 }, new[] { .05f, .05f, .05f, .05f, .05f, .05f });

            builder.Setup((input, output) =>
            {
                output.Names("Bee", "Bees");
                output.FlavorText(new FlavorText(
                    new Texts { },
                    new Texts { },
                    new Texts { "apid", "bee", { "worker bee", Gender.Female }, { "drone", Gender.Male } },
                    new Dictionary<string, string>
                    {
                        [WeaponNames.Mace] = "Honeycomb WeaponNames.Mace",
                        [WeaponNames.Axe] = "Quad Punch Claws",
                        [WeaponNames.SimpleBow] = "Javelin",
                        [WeaponNames.CompoundBow] = "War Javelin"
                    }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 10,
                    StomachSize = 14,
                    HasTail = true,
                    FavoredStat = Stat.Agility,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.CockVore, VoreType.BreastVore, VoreType.Anal, VoreType.TailVore },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Flight,
                        TraitType.KeenReflexes,
                        TraitType.PackDefense,
                        TraitType.Stinger
                    },
                    RaceDescription = "",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.BodyAccessoryColor, "Exoskeleton Color");
                    buttons.SetText(ButtonType.BodyAccessoryType, "Antennae Type");
                });
                output.TownNames(new List<string>
                {
                    "Queen's Hive",
                    "Honeycomb",
                    "Buzzytown",
                    "Golden Orchard",
                    "Waxville",
                    "Bumblebee Grove",
                    "Beepolis",
                    "Maya's Meadow",
                    "Sweet Pollen",
                    "Black Swarm",
                    "Royal Jelly",
                    "Workers Hive",
                    "Apiarist Respite"
                });
                output.BreastSizes = () => 8;
                output.DickSizes = () => 8;

                output.BodySizes = 4;
                output.EyeTypes = 8;
                output.SpecialAccessoryCount = 6; // antennae        
                output.HairStyles = 18;
                output.MouthTypes = 0;
                output.EyeColors = 0;
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.BeeNewSkin);
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.BeeNewSkin);

                output.ExtendedBreastSprites = true;

                output.AllowedMainClothingTypes.Set(
                    BeesClothing.GenericTop1.GenericTop1Instance,
                    BeesClothing.GenericTop2.GenericTop2Instance,
                    BeesClothing.GenericTop3.GenericTop3Instance,
                    BeesClothing.GenericTop4.GenericTop4Instance,
                    BeesClothing.GenericTop5.GenericTop5Instance,
                    BeesClothing.GenericTop6.GenericTop6Instance,
                    BeesClothing.MaleTop.MaleTopInstance,
                    BeesClothing.MaleTop2.MaleTop2Instance,
                    BeesClothing.Natural.NaturalInstance,
                    BeesClothing.Cuirass.CuirassInstance,
                    BeesClothing.Cuirass2.Cuirass2Instance,
                    BeesClothing.BeeRags.BeeRagsInstance,
                    BeesClothing.BeeLeaderInstance
                );
                output.AvoidedMainClothingTypes = 2;
                output.AvoidedEyeTypes = 0;
                output.AllowedWaistTypes.Set(
                    BeesClothing.GenericBot1.GenericBot1Instance,
                    BeesClothing.GenericBot2.GenericBot2Instance,
                    BeesClothing.GenericBot3.GenericBot3Instance,
                    BeesClothing.GenericBot4.GenericBot4Instance
                );

                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.AviansSkin);
            });


            builder.RenderSingle(SpriteType.Head, 20, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.BeeNewSkin, input.U.SkinColor));
                if (input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Bees1[5]);
                    return;
                }

                output.Sprite(input.Sprites.Bees1[4]);
            });

            builder.RenderSingle(SpriteType.Eyes, 22, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Bees1[46 + input.U.EyeType]);
            });
            builder.RenderSingle(SpriteType.Mouth, 21, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Bees1[9]);
                    return;
                }

                output.Sprite(input.Sprites.Bees1[8]);
            });

            builder.RenderSingle(SpriteType.Hair, 23, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.U.HairColor));
                output.Sprite(input.Sprites.Bees1[66 + input.U.HairStyle]);
            });
            builder.RenderSingle(SpriteType.Body, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.BeeNewSkin, input.U.SkinColor));
                if (input.A.AnimationController.FrameLists == null)
                {
                    SetUpAnimations(input.Actor);
                }

                if (input.U.HasBreasts)
                {
                    output.Sprite(input.Sprites.Bees1[0]);
                    return;
                }

                output.Sprite(input.Sprites.Bees1[1]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.AnimationController.FrameLists[0].CurrentTime >= frameListWings.Times[input.A.AnimationController.FrameLists[0].CurrentFrame] && input.U.IsDead == false)
                {
                    input.A.AnimationController.FrameLists[0].CurrentFrame++;
                    input.A.AnimationController.FrameLists[0].CurrentTime = 0f;

                    if (input.A.AnimationController.FrameLists[0].CurrentFrame >= frameListWings.Frames.Length)
                    {
                        input.A.AnimationController.FrameLists[0].CurrentFrame = 0;
                        input.A.AnimationController.FrameLists[0].CurrentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Bees1[42 + frameListWings.Frames[input.A.AnimationController.FrameLists[0].CurrentFrame]]);
            }); // Wings

            builder.RenderSingle(SpriteType.BodyAccent2, 24, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.BeeNewSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Bees1[54 + input.U.SpecialAccessoryType]);
            }); // Antennae 1
            builder.RenderSingle(SpriteType.BodyAccent3, 24, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.BeeNewSkin, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Bees1[60 + input.U.SpecialAccessoryType]);
            }); // Antennae 2
            builder.RenderSingle(SpriteType.BodyAccent4, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.BeeNewSkin, input.U.AccessoryColor));
                if (input.U.HasBreasts)
                {
                    output.Sprite(input.Sprites.Bees1[2]);
                    return;
                }

                output.Sprite(input.Sprites.Bees1[3]);
            }); // Body 2

            builder.RenderSingle(SpriteType.BodyAccent5, 20, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.BeeNewSkin, input.U.AccessoryColor));
                if (input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Bees1[7]);
                    return;
                }

                output.Sprite(input.Sprites.Bees1[6]);
            }); // Head 2

            builder.RenderSingle(SpriteType.BodyAccent6, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.BeeNewSkin, input.U.AccessoryColor));
                if (input.U.HasWeapon == false)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Bees1[18]);
                        return;
                    }

                    output.Sprite(input.Sprites.Bees1[14]);
                    return;
                }

                switch (input.A.GetWeaponSprite())
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
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.BeeNewSkin, input.U.AccessoryColor));
                if (input.A.IsTailVoring)
                {
                    output.Sprite(input.Sprites.Bees1[11]);
                    return;
                }

                output.Sprite(input.Sprites.Bees1[10]);
            }); // Legs

            builder.RenderSingle(SpriteType.BodyAccent8, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsTailVoring)
                {
                    output.Sprite(input.Sprites.Bees1[13]);
                    return;
                }

                output.Sprite(input.Sprites.Bees1[12]);
            }); // Lower Claws

            builder.RenderSingle(SpriteType.BodyAccent9, 19, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.ClothingType == 10 || input.U.ClothingType == 11)
                {
                    output.Sprite(input.Sprites.Bees3[134]);
                    return;
                }

                output.Sprite(input.Sprites.Bees1[26]);
            }); // Upper FLuff

            builder.RenderSingle(SpriteType.BodyAccent10, 9, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.ClothingType == 12 || (input.U.ClothingType2 == 4 && input.U.ClothingType != 9 && input.U.ClothingType != 13))
                {
                    return;
                }

                output.Sprite(input.Sprites.Bees1[25]);
            }); // Lower Fluff

            builder.RenderSingle(SpriteType.BodyAccessory, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.BeeNewSkin, input.U.SkinColor));
                int sizet = input.A.GetTailSize(3);
                if (input.A.IsTailVoring)
                {
                    output.Sprite(input.Sprites.Bees1[34 + sizet]);
                    return;
                }

                output.Sprite(input.Sprites.Bees1[27 + sizet]);
            }); // Abdomen

            builder.RenderSingle(SpriteType.SecondaryAccessory, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                switch (input.A.GetWeaponSprite())
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
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.BeeNewSkin, input.U.SkinColor));
                if (input.U.BodySize > 0)
                {
                    output.Sprite(input.U.HasBreasts ? input.Sprites.Bees1[89 + input.U.BodySize] : input.Sprites.Bees1[92 + input.U.BodySize]);
                }
            });

            builder.RenderSingle(SpriteType.Breasts, 17, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.BeeNewSkin, input.U.SkinColor));
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

                    output.Sprite(input.Sprites.Bees2[38 + leftSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Bees2[38 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.SecondaryBreasts, 17, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.BeeNewSkin, input.U.SkinColor));
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

                    output.Sprite(input.Sprites.Bees2[73 + rightSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Bees2[73 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.BreastShadow, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                int sizee = input.A.GetTailSize(3);
                if (input.A.IsTailVoring)
                {
                    output.Sprite(input.Sprites.Bees1[38 + sizee]);
                }
                else if (input.A.GetTailSize(3) >= 1)
                {
                    output.Sprite(input.Sprites.Bees1[30 + sizee]);
                }
            }); // abdomen extra


            builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.BeeNewSkin, input.U.SkinColor));
                if (input.A.HasBelly)
                {
                    int size = input.A.GetStomachSize(31, 0.8f);

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
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect())
                {
                    if (input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32)) < 16)
                    {
                        output.Layer(20);
                        output.Sprite(input.A.IsCockVoring ? input.Sprites.Bees1[112 + input.U.DickSize] : input.Sprites.Bees1[96 + input.U.DickSize]);
                    }
                    else
                    {
                        output.Layer(13);
                        output.Sprite(input.A.IsCockVoring ? input.Sprites.Bees1[120 + input.U.DickSize] : input.Sprites.Bees1[104 + input.U.DickSize]);
                    }
                }

                //output.Layer(11);
            });

            builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.BeeNewSkin, input.U.SkinColor));
                if (input.U.HasDick == false)
                {
                    return;
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
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    switch (input.A.GetWeaponSprite())
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
            builder.RandomCustom((data, output) =>   
            {
                IUnitRead unit = data.Unit;
                Defaults.Randomize(data, output);

                if (unit.HasDick && unit.HasBreasts)
                {
                    unit.HairStyle = State.Rand.Next(Config.HermsOnlyUseFemaleHair ? 12 : data.SetupOutput.HairStyles);
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
                        unit.HairStyle = 8 + State.Rand.Next(10);
                    }
                    else
                    {
                        unit.HairStyle = State.Rand.Next(12);
                    }
                }

                if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
                {
                    unit.ClothingType = 1 + Extensions.IndexOf(data.SetupOutput.AllowedMainClothingTypes, BeesClothing.BeeRags.BeeRagsInstance);
                    if (unit.ClothingType == 0) //Covers rags not in the list
                    {
                        unit.ClothingType = data.SetupOutput.AllowedMainClothingTypes.Count;
                    }
                }

                if (unit.Type == UnitType.Leader)
                {
                    unit.ClothingType = 1 + Extensions.IndexOf(data.SetupOutput.AllowedMainClothingTypes, BeesClothing.BeeLeaderInstance);
                }
            });
        });


        private static void SetUpAnimations(IActorUnit actor)
        {
            actor.AnimationController.FrameLists = new[]
            {
                new AnimationController.FrameList(State.Rand.Next(0, 6), 0, true)
            }; // Wing controller. Index 0.
        }


        internal static bool IsOverSize(IActorUnit actor)
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
    }


    internal static class BeesClothing
    {
        internal static class GenericTop1
        {
            internal static readonly IClothing GenericTop1Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[24];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.bees/1524");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);
                    if (Bees.IsOverSize(input.Actor))
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[32]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[24 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        internal static class GenericTop2
        {
            internal static readonly IClothing GenericTop2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[34];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.bees/1534");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);
                    if (Bees.IsOverSize(input.Actor))
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[41]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[33 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        internal static class GenericTop3
        {
            internal static readonly IClothing GenericTop3Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[44];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.bees/1544");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);
                    if (Bees.IsOverSize(input.Actor))
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[50]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[42 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        internal static class GenericTop4
        {
            internal static readonly IClothing GenericTop4Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[55];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.bees/1555");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);
                    if (Bees.IsOverSize(input.Actor))
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[59]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[51 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        internal static class GenericTop5
        {
            internal static readonly IClothing GenericTop5Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[74];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.bees/1574");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing2"].Layer(18);
                    output["Clothing2"].Coloring(Color.white);
                    if (Bees.IsOverSize(input.Actor))
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[68]);
                        output["Clothing2"].Sprite(input.Sprites.Bees3[77]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[60 + input.U.BreastSize]);
                        output["Clothing2"].Sprite(input.Sprites.Bees3[69 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        internal static class GenericTop6
        {
            internal static readonly IClothing GenericTop6Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[88];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.bees/1588");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);
                    if (Bees.IsOverSize(input.Actor))
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[79 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        internal static class MaleTop
        {
            internal static readonly IClothing MaleTopInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[79];
                    output.MaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.bees/1579");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);

                    output["Clothing1"].Sprite(input.A.HasBelly ? input.Sprites.Bees3[113] : input.Sprites.Bees3[112]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        internal static class MaleTop2
        {
            internal static readonly IClothing MaleTop2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[79];
                    output.MaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.bees/1579");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Sprite(input.Sprites.Bees3[78]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        internal static class Natural
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
                    if (Bees.IsOverSize(input.Actor))
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[0 + input.U.BreastSize]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Bees3[8]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.BeeNewSkin, input.U.SkinColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.BeeNewSkin, input.U.SkinColor));
                });
            });
        }

        internal static class Cuirass
        {
            internal static readonly IClothing CuirassInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Bees3[133];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.FixedColor = true;
                    output.ClothingId = new ClothingId("base.bees/391");
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(25);
                    output["Clothing1"].Coloring(Color.white);
                    if (Bees.IsOverSize(input.Actor))
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[115 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[123]);
                    }
                });
            });
        }

        internal static class Cuirass2
        {
            internal static readonly IClothing Cuirass2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Bees3[133];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.FixedColor = true;
                    output.ClothingId = new ClothingId("base.bees/391");
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(25);
                    output["Clothing1"].Coloring(Color.white);
                    if (Bees.IsOverSize(input.Actor))
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[124 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Bees3[132]);
                    }
                });
            });
        }

        internal static class BeeRags
        {
            internal static readonly IClothing BeeRagsInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Rags[23];
                    output.RevealsDick = true;
                    output.InFrontOfDick = true;
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.bees/207");
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
                    if (input.U.HasBreasts)
                    {
                        if (input.U.BreastSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Bees3[90]);
                        }
                        else if (input.U.BreastSize < 6)
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

                    output["Clothing2"].Sprite(input.A.IsTailVoring ? input.Sprites.Bees3[88] : input.Sprites.Bees3[87]);

                    output["Clothing3"].Sprite(input.Sprites.Bees3[89]);
                });
            });
        }

        internal static class GenericBot1
        {
            internal static readonly IClothing GenericBot1Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians3[121];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.bees/1521");
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
                            output["Clothing1"].Sprite(input.Sprites.Bees3[10]);
                        }
                        else if (input.U.DickSize > 5)
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

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        internal static class GenericBot2
        {
            internal static readonly IClothing GenericBot2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians3[137];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.bees/1537");
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
                            output["Clothing1"].Sprite(input.Sprites.Bees3[15]);
                        }
                        else if (input.U.DickSize > 5)
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

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        internal static class GenericBot3
        {
            internal static readonly IClothing GenericBot3Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians3[140];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.bees/1540");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(13);
                    output["Clothing2"].Layer(12);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Sprite(input.Sprites.Bees3[18]);
                    output["Clothing2"].Sprite(input.Sprites.Bees3[13]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        internal static class GenericBot4
        {
            internal static readonly IClothing GenericBot4Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[14];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.bees/1514");
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
                            output["Clothing1"].Sprite(input.Sprites.Bees3[21]);
                        }
                        else if (input.U.DickSize > 5)
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

                    output["Clothing2"].Sprite(input.A.IsTailVoring ? input.Sprites.Bees3[20] : input.Sprites.Bees3[19]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        internal static readonly IClothing BeeLeaderInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.LeaderOnly = true;
                output.DiscardSprite = input.Sprites.Bees3[114];
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.ClothingId = new ClothingId("base.bees/390");
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
                if (input.U.HasBreasts)
                {
                    output["Clothing2"].Sprite(Bees.IsOverSize(input.Actor) ? input.Sprites.Bees3[104] : input.Sprites.Bees3[96 + input.U.BreastSize]);

                    output["Clothing1"].Sprite(input.Sprites.Bees3[93]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Bees3[94]);
                    output["Clothing2"].Sprite(input.Sprites.Bees3[95]);
                }

                if (input.A.GetWeaponSprite() == 3)
                {
                    output["Clothing3"].Sprite(Bees.IsOverSize(input.Actor) ? input.Sprites.Bees3[110] : input.Sprites.Bees3[107]);
                }
                else if (input.A.GetWeaponSprite() == 7)
                {
                    output["Clothing3"].Sprite(Bees.IsOverSize(input.Actor) ? input.Sprites.Bees3[109] : input.Sprites.Bees3[106]);
                }
                else
                {
                    output["Clothing3"].Sprite(Bees.IsOverSize(input.Actor) ? input.Sprites.Bees3[108] : input.Sprites.Bees3[105]);
                }

                output["Clothing4"].Sprite(input.Sprites.Bees3[111]);
            });
        });
    }
}