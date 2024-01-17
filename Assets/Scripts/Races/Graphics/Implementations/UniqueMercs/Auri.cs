#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.UniqueMercs
{
    internal static class Auri
    {
        private static Func<IClothingRenderInput, IOverSizeParameters> paramsCalc = CommonRaceCode.MakeOversizeFunc(32 * 32);
        internal const float StomachMult = 1.7f;

        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            List<IClothingDataSimple> allClothing;

            RaceFrameList earAnimation = new RaceFrameList(new int[3] { 22, 23, 22 }, new float[3] { .2f, .2f, .2f });
            RaceFrameList faceAnimation = new RaceFrameList(new int[3] { 18, 19, 18 }, new float[3] { .25f, .25f, .25f });

            builder.Setup(output =>
            {
                output.Names("Auri", "Auri");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 16,
                    StomachSize = 30,
                    HasTail = true,
                    FavoredStat = Stat.Agility,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.BreastVore, VoreType.Anal },
                    ExpMultiplier = 2.4f,
                    PowerAdjustment = 3.2f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(16, 16),
                        Dexterity = new RaceStats.StatRange(20, 20),
                        Endurance = new RaceStats.StatRange(16, 20),
                        Mind = new RaceStats.StatRange(20, 20),
                        Will = new RaceStats.StatRange(14, 20),
                        Agility = new RaceStats.StatRange(24, 26),
                        Voracity = new RaceStats.StatRange(16, 20),
                        Stomach = new RaceStats.StatRange(12, 16),
                    },
                    RacialTraits = new List<Traits>()
                    {
                        Traits.ArtfulDodge,
                        Traits.ThrillSeeker,
                        Traits.FastCaster
                    },
                    InnateSpells = new List<SpellTypes>()
                        { SpellTypes.Mending, SpellTypes.Summon },
                    RaceDescription = "A fox-woman priestess and self-proclaimed avatar of a creator of the world.",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.Skintone, "Body Color");
                    buttons.SetText(ButtonType.BodyAccessoryType, "Barbel (Whisker) Type");
                    buttons.SetText(ButtonType.BodyAccentTypes1, "Dorsal Fin Type");
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.ClothingType, "Breast Wrap");
                    buttons.SetText(ButtonType.ClothingExtraType1, "Kimono");
                    buttons.SetText(ButtonType.ClothingExtraType2, "Socks");
                    buttons.SetText(ButtonType.ClothingExtraType3, "Hair Ornament");
                    buttons.SetText(ButtonType.TailTypes, "Tail Quantity");
                    buttons.SetText(ButtonType.BodyAccentTypes1, "Beast Mode");
                });
                output.BreastSizes = () => 7;
                output.DickSizes = () => 1;

                output.CanBeGender = new List<Gender> { Gender.Female };

                output.SpecialAccessoryCount = 0;
                output.ClothingShift = new Vector3(0, 0, 0);
                output.AvoidedEyeTypes = 0;
                output.AvoidedMouthTypes = 0;

                output.ExtendedBreastSprites = true;

                output.HairColors = 1;
                output.HairStyles = 1;
                output.SkinColors = 1;
                output.AccessoryColors = 1;
                output.EyeTypes = 1;
                output.EyeColors = 1;
                output.SecondaryEyeColors = 1;
                output.BodySizes = 2;
                output.AllowedMainClothingTypes.Clear();
                output.AllowedWaistTypes.Clear();
                output.MouthTypes = 0;
                output.AvoidedMainClothingTypes = 0;
                output.TailTypes = 2;
                output.BodyAccentTypes1 = 2;
                output.ClothingColors = 1;

                List<IClothing> allowedMainClothingTypes = new List<IClothing>
                {
                    AuriTop.AuriTopInstance.Create(paramsCalc)
                };

                output.AllowedMainClothingTypes.Clear();

                List<IClothing> allowedWaistTypes = new List<IClothing>() //Bottoms
                {
                    GenericBottom.GenericBottom1,
                    GenericBottom.GenericBottom2
                };
                output.AllowedWaistTypes.Clear();

                output.AllowedClothingHatTypes.Clear();

                List<IClothing> extraMainClothing1Types = new List<IClothing>() //Over
                {
                    Kimono.Kimono1.Create(paramsCalc),
                    Kimono.Kimono2.Create(paramsCalc),
                    KimonoHoliday.KimonoHoliday1.Create(paramsCalc),
                    KimonoHoliday.KimonoHoliday2.Create(paramsCalc)
                };
                output.ExtraMainClothing1Types.Set(extraMainClothing1Types);

                List<IClothing> extraMainClothing2Types = new List<IClothing>() //Stocking
                {
                    Stocking.Stocking1
                };
                output.ExtraMainClothing2Types.Clear();

                List<IClothing> extraMainClothing3Types = new List<IClothing>() //Hat
                {
                    Hat.Hat1
                };
                output.ExtraMainClothing3Types.Clear();

                allClothing = new List<IClothingDataSimple>();
                allClothing.AddRange(allowedMainClothingTypes);
                allClothing.AddRange(allowedWaistTypes);
                allClothing.AddRange(extraMainClothing1Types);
                allClothing.AddRange(extraMainClothing2Types);
                allClothing.AddRange(extraMainClothing3Types);
            });


            builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Auri[17]);
                    return;
                }

                if (input.U.IsDead && input.U.Items != null) //Second part checks for a not fully initialized unit, so that she doesn't have the dead face when you view her race info
                {
                    output.Sprite(input.Sprites.Auri[20]);
                    return;
                }

                if (input.A.AnimationController.frameLists == null)
                {
                    SetUpAnimations(input.Actor);
                }

                if (State.Rand.Next(1600) == 0)
                {
                    input.A.AnimationController.frameLists[1].currentlyActive = true;
                }

                if (input.A.AnimationController.frameLists[1].currentlyActive == false)
                {
                    output.Sprite(input.Sprites.Auri[16]);
                    return;
                }

                if (input.A.AnimationController.frameLists[1].currentTime >= earAnimation.Times[input.A.AnimationController.frameLists[1].currentFrame])
                {
                    input.A.AnimationController.frameLists[1].currentFrame++;
                    input.A.AnimationController.frameLists[1].currentTime = 0f;

                    if (input.A.AnimationController.frameLists[1].currentFrame >= earAnimation.Frames.Length)
                    {
                        input.A.AnimationController.frameLists[1].currentlyActive = false;
                        input.A.AnimationController.frameLists[1].currentTime = 0;
                        input.A.AnimationController.frameLists[1].currentFrame = 0;
                    }
                }

                output.Sprite(input.Sprites.Auri[faceAnimation.Frames[input.A.AnimationController.frameLists[1].currentFrame]]);
            });

            builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Auri[42]);
            });

            builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Auri[43]);
            });

            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                int weightMod = input.U.BodySize * 4;
                if (input.U.BodyAccentType1 == 0)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Auri[3 + weightMod]);
                        return;
                    }

                    output.Sprite(input.Sprites.Auri[0 + weightMod]);
                    return;
                }

                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Auri[11 + weightMod]);
                    return;
                }

                output.Sprite(input.Sprites.Auri[8 + weightMod]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 8, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.BodyAccentType1 == 0)
                {
                    return;
                }

                int weightMod = input.U.BodySize * 4;
                if (input.U.BodyAccentType1 == 0)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Auri[27 + weightMod]);
                        return;
                    }

                    output.Sprite(input.Sprites.Auri[24 + weightMod]);
                    return;
                }

                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Auri[35 + weightMod]);
                    return;
                }

                output.Sprite(input.Sprites.Auri[32 + weightMod]);
            });

            builder.RenderSingle(SpriteType.BodyAccent2, 8, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.BodyAccentType1 == 0)
                {
                    return;
                }

                output.Sprite(input.Sprites.Auri[40 + input.U.BodySize]);
            });

            builder.RenderSingle(SpriteType.BodyAccessory, 5, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.AnimationController.frameLists == null)
                {
                    SetUpAnimations(input.Actor);
                }

                if (State.Rand.Next(650) == 0)
                {
                    input.A.AnimationController.frameLists[0].currentlyActive = true;
                }

                if (input.A.AnimationController.frameLists[0].currentlyActive == false)
                {
                    output.Sprite(input.Sprites.Auri[21]);
                    return;
                }


                if (input.A.AnimationController.frameLists[0].currentTime >= earAnimation.Times[input.A.AnimationController.frameLists[0].currentFrame])
                {
                    input.A.AnimationController.frameLists[0].currentFrame++;
                    input.A.AnimationController.frameLists[0].currentTime = 0f;

                    if (input.A.AnimationController.frameLists[0].currentFrame >= earAnimation.Frames.Length)
                    {
                        input.A.AnimationController.frameLists[0].currentlyActive = false;
                        input.A.AnimationController.frameLists[0].currentTime = 0;
                        input.A.AnimationController.frameLists[0].currentFrame = 0;
                    }
                }

                output.Sprite(input.Sprites.Auri[earAnimation.Frames[input.A.AnimationController.frameLists[0].currentFrame]]);
            });

            builder.RenderSingle(SpriteType.SecondaryAccessory, 0, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Auri[44 + input.U.TailType]);
            });

            builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
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


                    output.Sprite(input.Sprites.AuriVore[0 + leftSize]);
                    return;
                }

                if (input.U.DefaultBreastSize == 0)
                {
                    output.Sprite(input.Sprites.AuriVore[0]);
                    return;
                }

                if (input.A.SquishedBreasts && input.U.BreastSize < 6 && input.U.BreastSize >= 4)
                {
                    output.Sprite(input.Sprites.AuriVore[31 + input.U.BreastSize - 3]);
                    return;
                }

                output.Sprite(input.Sprites.AuriVore[0 + input.U.BreastSize]);
            });

            builder.RenderSingle(SpriteType.SecondaryBreasts, 16, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
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

                    output.Sprite(input.Sprites.AuriVore[35 + rightSize]);
                    return;
                }

                if (input.U.DefaultBreastSize == 0)
                {
                    output.Sprite(input.Sprites.AuriVore[35]);
                    return;
                }

                if (input.A.SquishedBreasts && input.U.BreastSize < 6 && input.U.BreastSize >= 4)
                {
                    output.Sprite(input.Sprites.AuriVore[66 + input.U.BreastSize - 3]);
                    return;
                }

                output.Sprite(input.Sprites.AuriVore[35 + input.U.BreastSize]);
            });

            builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.HasBelly)
                {
                    int size = input.A.GetStomachSize(32, StomachMult);

                    if (size > 30)
                    {
                        size = 30;
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
                    }

                    if (input.A.PredatorComponent.OnlyOnePreyAndLiving() && size >= 9 && size <= 14)
                    {
                        output.Sprite(input.Sprites.Auri[105]);
                        return;
                    }

                    output.Sprite(input.Sprites.AuriVore[70 + size]);
                }
            });

            builder.RenderSingle(SpriteType.Weapon, 13, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Auri[47]);
                    return;
                }

                output.Sprite(input.Sprites.Auri[46]);
            });


            builder.RunBefore((input, output) => { Defaults.BasicBellyRunAfter.Invoke(input, output); });

            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                Unit unit = data.Unit;

                unit.Name = "Auri";
                unit.SetDefaultBreastSize(4);
                unit.BodySize = 0;
                unit.BodyAccentType1 = 0;
                unit.ClothingExtraType1 = 1;
                unit.TailType = 0;
                if (Config.WinterActive())
                {
                    unit.ClothingExtraType1 = 3;
                }
            });
        });

        private static void SetUpAnimations(Actor_Unit actor)
        {
            actor.AnimationController.frameLists = new[]
            {
                new AnimationController.FrameList(),
                new AnimationController.FrameList()
            };
            actor.AnimationController.frameLists[0].currentlyActive = false;
            actor.AnimationController.frameLists[1].currentlyActive = false;
        }

                private static class AuriTop
        {
            internal static readonly BindableClothing<IOverSizeParameters> AuriTopInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Auri[64];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.Type = 1422;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Auri[62]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        input.A.SquishedBreasts = true;
                        output["Clothing1"].Sprite(input.Sprites.Auri[56 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Auri[56]);
                    }
                });
            });
        }


        private static class GenericBottom
        {
            internal static readonly IClothing GenericBottom1 = MakeGenericBottom(52, 52, 0, 56, 8, State.GameManager.SpriteDictionary.Auri, 840);
            internal static readonly IClothing GenericBottom2 = MakeGenericBottom(101, 101, 0, 101, 8, State.GameManager.SpriteDictionary.Auri, 841);

            private static IClothing MakeGenericBottom(int sprF, int sprM, int bulge, int discard, int layer, Sprite[] sheet, int type)
            {
                ClothingBuilder builder = ClothingBuilder.New();

                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.DiscardSprite = sheet[discard];
                    output.Type = type;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(layer);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing2"].Layer(layer + 1);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Sprite(input.U.HasDick ? sheet[sprM] : sheet[sprF + input.U.BodySize]);

                    if (input.U.HasDick && bulge > 0)
                    {
                        if (input.U.DickSize > 2)
                        {
                            output["Clothing2"].Sprite(sheet[bulge + 1]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(sheet[bulge + 1]);
                        }
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }
                });
                return builder.BuildClothing();
            }
        }

        private static class Kimono
        {
            internal static readonly BindableClothing<IOverSizeParameters> Kimono1 = MakeKimono(true);
            internal static readonly BindableClothing<IOverSizeParameters> Kimono2 = MakeKimono(false);

            private static BindableClothing<IOverSizeParameters> MakeKimono(bool skirt)
            {

                return ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
                {
                    builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                    {
                        output.RevealsDick = true;
                        output.RevealsBreasts = true;
                        output.DiscardSprite = input.Sprites.Auri[95];
                        output.DiscardUsesPalettes = true;
                        output.Type = 444;
                        output.OccupiesAllSlots = true;
                    });
                    
                    builder.RenderAll((input, output, extra) =>
                    {
                        output["Clothing3"].Layer(11);
                        output["Clothing3"].Coloring(Color.white);
                        output["Clothing2"].Layer(20);
                        output["Clothing2"].Coloring(Color.white);
                        output["Clothing1"].Layer(12);
                        output["Clothing1"].Coloring(Color.white);
                        output["Clothing1"].SetOffset(0, 0 * .625f);
                        output["Clothing2"].SetOffset(0, 0 * .625f);
                        input.A.SquishedBreasts = true;
                        if (skirt)
                        {
                            int skirtMod = 0;
                            if (input.U.BodySize > 0 || input.U.BodyAccentType1 == 1)
                            {
                                skirtMod = 26;
                            }

                            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
                            {
                                output["Clothing1"].Sprite(input.Sprites.Auri[86 + skirtMod]);
                            }
                            else
                            {
                                if (input.A.GetStomachSize(32, StomachMult) < 8)
                                {
                                    output["Clothing1"].Sprite(input.Sprites.Auri[80 + skirtMod + input.A.GetStomachSize(32, StomachMult)]);
                                }
                                else
                                {
                                    output["Clothing1"].Sprite(input.Sprites.Auri[88]);
                                }
                            }
                        }
                        else
                        {
                            output["Clothing1"].Sprite(null);
                        }

                        int kimMod = skirt ? 0 : 7;
                        if (extra.Oversize)
                        {
                            output["Clothing2"].Sprite(input.Sprites.Auri[93 + kimMod]);
                        }
                        else if (input.U.BreastSize < 3)
                        {
                            output["Clothing2"].Sprite(input.Sprites.Auri[89 + kimMod]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(input.Sprites.Auri[89 + kimMod + input.U.BreastSize - 2]);
                        }

                        int mod = input.U.BodySize * 4;
                        if (mod > 4)
                        {
                            mod = 4;
                        }

                        if (input.U.BodyAccentType1 == 1)
                        {
                            mod += 8;
                        }

                        output["Clothing3"].Sprite(input.A.IsAttacking ? input.Sprites.Auri[67 + mod] : input.Sprites.Auri[64 + mod]);
                    });
                });
            }


        }
                    private static class KimonoHoliday
            {
                internal static readonly BindableClothing<IOverSizeParameters> KimonoHoliday1 = MakeKimonoHoliday(true);
                internal static readonly BindableClothing<IOverSizeParameters> KimonoHoliday2 = MakeKimonoHoliday(false);

                private static BindableClothing<IOverSizeParameters> MakeKimonoHoliday(bool skirt)
                {
                    return ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
                    {
                        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                        {
                            output.RevealsDick = true;
                            output.RevealsBreasts = true;
                            output.DiscardSprite = input.Sprites.Auri[95];
                            output.DiscardUsesPalettes = true;
                            output.Type = 444;
                            output.ReqWinterHoliday = true;
                            output.OccupiesAllSlots = true;
                        });
                        
                        builder.RenderAll((input, output, extra) =>
                        {
                            output["Clothing5"].Layer(15);
                            output["Clothing5"].Coloring(Color.white);
                            output["Clothing4"].Layer(0);
                            output["Clothing4"].Coloring(Color.white);
                            output["Clothing3"].Layer(11);
                            output["Clothing3"].Coloring(Color.white);
                            output["Clothing2"].Layer(20);
                            output["Clothing2"].Coloring(Color.white);
                            output["Clothing1"].Layer(12);
                            output["Clothing1"].Coloring(Color.white);
                            output["Clothing1"].SetOffset(0, 0 * .625f);
                            output["Clothing2"].SetOffset(0, 0 * .625f);
                            input.A.SquishedBreasts = true;
                            if (skirt)
                            {
                                int skirtMod = 0;
                                if (input.U.BodySize > 0 || input.U.BodyAccentType1 == 1)
                                {
                                    skirtMod = 2;
                                }

                                if (input.A.IsUnbirthing || input.A.IsAnalVoring)
                                {
                                    output["Clothing1"].Sprite(input.Sprites.AuriHoliday[23 + skirtMod]);
                                }
                                else
                                {
                                    if (input.A.GetStomachSize(32, StomachMult) < 4 && input.U.BodyAccentType1 == 0)
                                    {
                                        output["Clothing1"].Sprite(input.Sprites.AuriHoliday[22 + skirtMod]);
                                    }
                                    else
                                    {
                                        output["Clothing1"].Sprite(input.Sprites.AuriHoliday[26 + skirtMod]);
                                    }
                                }
                            }
                            else
                            {
                                output["Clothing1"].Sprite(null);
                            }

                            if (extra.Oversize)
                            {
                                output["Clothing2"].Sprite(input.Sprites.AuriHoliday[20]);
                            }
                            else if (input.U.BreastSize < 3)
                            {
                                output["Clothing2"].Sprite(input.Sprites.AuriHoliday[16]);
                            }
                            else
                            {
                                output["Clothing2"].Sprite(input.Sprites.AuriHoliday[16 + input.U.BreastSize - 2]);
                            }

                            int mod = input.U.BodySize * 4;
                            if (mod > 4)
                            {
                                mod = 4;
                            }

                            if (input.U.BodyAccentType1 == 1)
                            {
                                mod += 8;
                            }

                            output["Clothing3"].Sprite(input.A.IsAttacking ? input.Sprites.AuriHoliday[3 + mod] : input.Sprites.AuriHoliday[0 + mod]);

                            output["Clothing4"].Sprite(input.Sprites.AuriHoliday[21]);
                            if (input.A.GetStomachSize(32, StomachMult) >= 4)
                            {
                                output["Clothing5"].Sprite(input.Sprites.AuriHoliday[32]);
                            }
                        });
                    });
                }
            }

            private static class Stocking
            {
                internal static readonly IClothing Stocking1 = MakeStocking(48, 0, 48, 3, State.GameManager.SpriteDictionary.Auri, 901);

                private static IClothing MakeStocking(int sprF, int sprM, int discard, int layer, Sprite[] sheet, int type)
                {
                    ClothingBuilder builder = ClothingBuilder.New();


                    builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                    {
                        output.RevealsBreasts = true;
                        output.RevealsDick = true;
                        output.DiscardSprite = sheet[discard];
                        output.Type = type;
                    });

                    builder.RenderAll((input, output) =>
                    {
                        output["Clothing1"].Layer(layer);
                        output["Clothing1"].Coloring(Color.white);
                        if (input.U.BodyAccentType1 == 1)
                        {
                            output["Clothing1"].Sprite(null);
                        }
                        else if (input.U.HasBreasts)
                        {
                            output["Clothing1"].Sprite(sheet[sprF + input.U.BodySize]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(sheet[sprM]);
                        }
                    });
                    return builder.BuildClothing();
                }
            }

            private static class Hat
            {
                internal static readonly IClothing Hat1 = MakeHat(50, 0, 50, 20, State.GameManager.SpriteDictionary.Auri, 903);

                private static IClothing MakeHat(int sprF, int sprM, int discard, int layer, Sprite[] sheet, int type)
                {
                    ClothingBuilder builder = ClothingBuilder.New();

                    builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                    {
                        output.RevealsBreasts = true;
                        output.RevealsDick = true;
                        output.DiscardSprite = sheet[discard];
                        output.Type = type;
                    });

                    builder.RenderAll((input, output) =>
                    {
                        output["Clothing1"].Layer(layer);
                        output["Clothing1"].Coloring(Color.white);
                        output["Clothing1"].Sprite(input.U.HasBreasts ? sheet[sprF] : sheet[sprM]);
                    });
                    return builder.BuildClothing();
                }
            }

    }
}