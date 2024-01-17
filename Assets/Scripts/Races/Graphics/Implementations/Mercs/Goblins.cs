#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion
namespace Races.Graphics.Implementations.Mercs
{


//TODO:
// recolor bulges on clothes
// add color selection
// add wobble to imp belly accent

    internal static class Goblins
    {
        private static Func<IClothingRenderInput, IOverSizeParameters> paramsCalc = CommonRaceCode.MakeOversizeFunc(22 * 22);
        private static List<IClothingDataSimple> _allClothing;

        internal static readonly IRaceData Instance = RaceBuilder.CreateV2(Defaults.Blank, builder =>
        {
            // Havent been implemented
            // Sprite[] SpritesGloves = State.GameManager.SpriteDictionary.Gobbglove;
            // Sprite[] SpritesLegs = State.GameManager.SpriteDictionary.Gobleggo;
            // Sprite[] SpritesUBottoms = State.GameManager.SpriteDictionary.Gobbunderbottoms;
            // Sprite[] SpritesUTops = State.GameManager.SpriteDictionary.Gobundertops;
            // Sprite[] SpritesOBottoms = State.GameManager.SpriteDictionary.Gobboverbottoms;
            // Sprite[] SpritesOTops = State.GameManager.SpriteDictionary.Gobbovertops;
            // Sprite[] SpritesOnePieces = State.GameManager.SpriteDictionary.Gobbunderonepieces;
            // Sprite[] SpritesOverOnePieces = State.GameManager.SpriteDictionary.Gobboveronepieces;


            builder.Setup(output =>
            {
                output.Names("Goblin", "Goblins");
                output.BonesInfo((unit) => 
                {
                    if (unit.Furry)
                    {
                        return new List<BoneInfo>
                        {
                            new BoneInfo(BoneTypes.FurryBones, unit.Name)
                        };
                    }
                    else
                    {
                        return new List<BoneInfo>
                        {
                            new BoneInfo(BoneTypes.GenericBonePile, unit.Name),
                            new BoneInfo(BoneTypes.Imp2EyeSkull, "")
                        };
                    }
                });
                output.FlavorText(new FlavorText(
                    new Texts { "diminutive", "cursing", "short" },
                    new Texts { "stronger than looks", "knee kicking", "smart" },
                    new Texts { "goblin", "goblinoid", "humanoid" },
                    new Dictionary<string, string>
                    {
                        [WeaponNames.Mace]        = "Cleaver",
                        [WeaponNames.Axe]         = "Sharpened Cleaver",
                        [WeaponNames.SimpleBow]   = "Derringer",
                        [WeaponNames.CompoundBow] = "Pepperbox Pistol",
                        [WeaponNames.Claw]        = "Fist",
                    }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 7,
                    StomachSize = 14,
                    HasTail = false,
                    FavoredStat = Stat.Mind,
                    RacialTraits = new List<Traits>()
                    {
                        Traits.Clever,
                        Traits.Tempered,
                        Traits.ArtfulDodge,
                        Traits.EscapeArtist,
                    },
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(6, 14),
                        Dexterity = new RaceStats.StatRange(10, 18),
                        Endurance = new RaceStats.StatRange(8, 16),
                        Mind = new RaceStats.StatRange(8, 16),
                        Will = new RaceStats.StatRange(10, 16),
                        Agility = new RaceStats.StatRange(10, 16),
                        Voracity = new RaceStats.StatRange(8, 16),
                        Stomach = new RaceStats.StatRange(10, 15),
                    },
                    RaceDescription = "Small and physically unintimidating, the Goblins came from a realm far ahead in technology. Were it not for the lack of materials to replicate their greatest inventions and the small size of their weapons, the Goblins might have claimed the entire land. As it is, they learned to be good at dodging and escaping the maws and guts of predators, through one end or another.",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.BodyAccessoryColor, "Body Accent Color");
                    buttons.SetText(ButtonType.ClothingType, "Under Tops");
                    buttons.SetText(ButtonType.Clothing2Type, "Under Bottoms");
                    buttons.SetText(ButtonType.ClothingExtraType1, "Over Bottoms");
                    buttons.SetText(ButtonType.ClothingExtraType2, "Over Tops");
                    buttons.SetText(ButtonType.ClothingExtraType3, "Legs");
                    buttons.SetText(ButtonType.ClothingExtraType4, "Gloves");
                    buttons.SetText(ButtonType.ClothingExtraType5, "Hats");
                    buttons.SetText(ButtonType.BodyAccentTypes1, "Ear Type");
                    buttons.SetText(ButtonType.BodyAccentTypes2, "Eyebrow Type");
                });
                output.BreastSizes = () => 7;
                output.DickSizes = () => 4;

                output.SpecialAccessoryCount = 0;
                output.ClothingShift = new Vector3(0, 0, 0);
                output.AvoidedEyeTypes = 0;
                output.AvoidedMouthTypes = 0;

                output.HairColors = ColorPaletteMap.GetPaletteCount(SwapType.UniversalHair);
                output.HairStyles = 16;
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.Goblins);
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.ImpDark);
                output.EyeTypes = 8;
                output.EyeColors = ColorPaletteMap.GetPaletteCount(SwapType.EyeColor);
                output.SecondaryEyeColors = 1;
                output.BodySizes = 2;
                output.AllowedWaistTypes.Clear();
                output.MouthTypes = 3;
                output.AvoidedMainClothingTypes = 0;
                output.BodyAccentTypes1 = 4;
                output.BodyAccentTypes2 = 4;


                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.Clothing50Spaced);

                output.ExtendedBreastSprites = true;

                List<IClothing> allowedMainClothingTypes = new List<IClothing>() //undertops
                {
                    GobboLeotard.GobboLeotardInstance,
                    GobboCasinoBunny.GobboCasinoBunnyInstance,
                    GobboUndertop1.GobboUndertop1Instance.Create(paramsCalc),
                    GobboUndertop2.GobboUndertop2Instance.Create(paramsCalc),
                    GobboUndertop3.GobboUndertop3Instance.Create(paramsCalc),
                    GobboUndertop4.GobboUndertop4Instance.Create(paramsCalc),
                    GobboUndertop5.GobboUndertop5Instance.Create(paramsCalc)
                };
                output.AllowedMainClothingTypes.Set(allowedMainClothingTypes); //undertops

                output.AllowedClothingHatTypes.Clear();


                output.AllowedWaistTypes.Set( //underbottoms
                    ImpUBottom.ImpUBottom1,
                    ImpUBottom.ImpUBottom2,
                    ImpUBottom.ImpUBottom3,
                    ImpUBottom.ImpUBottom4,
                    ImpUBottom.ImpUBottom5
                );

                output.ExtraMainClothing1Types.Set( //Overbottoms
                    All.GoblinOBottom1,
                    All.GoblinOBottom2,
                    All.GoblinOBottom3,
                    All.GoblinOBottom4,
                    All.GoblinOBottomAlt1,
                    All.GoblinOBottom5
                );

                output.ExtraMainClothing2Types.Set( //Special clothing
                    GobboOverOpFem.GobboOverOpFemInstance.Create(paramsCalc),
                    //GobboOverOPM.GobboOverOPMInstance,
                    GobboOverTop1.GobboOverTop1Instance.Create(paramsCalc),
                    GobboOverTop2.GobboOverTop2Instance,
                    GobboOverTop3.GobboOverTop3Instance,
                    GobboOverTop4.GobboOverTop4Instance
                );

                List<IClothing> extraMainClothing3Types = new List<IClothing>() //Legs
                {
                    All.GenericLegs1,
                    All.GenericLegs2,
                    All.GenericLegs3,
                    All.GenericLegsAlt1,
                    All.GenericLegs4,
                    All.GenericLegsAlt2,
                    All.GenericLegs5,
                    All.GenericLegs6,
                    All.GenericLegs7,
                    All.GenericLegs8,
                    All.GenericLegs9,
                    All.GenericLegs10
                };
                output.ExtraMainClothing3Types.Set(extraMainClothing3Types);

                List<IClothing> extraMainClothing4Types = new List<IClothing>() //Gloves
                {
                    All.GenericGloves1,
                    All.GenericGlovesPlusSecond1,
                    All.GenericGlovesPlusSecondAlt1,
                    All.GenericGlovesPlusSecond2,
                    All.GenericGlovesPlusSecondAlt2,
                    All.GenericGloves2
                };

                output.ExtraMainClothing4Types.Set(extraMainClothing4Types);

                List<IClothing> extraMainClothing5Types = new List<IClothing>() //Hats
                {
                    All.GoblinHat1,
                    All.GoblinHat2,
                    GoblinHolidayHat.HolidayHatInstance
                };
                output.ExtraMainClothing5Types.Set(extraMainClothing5Types);

                _allClothing = new List<IClothingDataSimple>();
                _allClothing.AddRange(allowedMainClothingTypes);
                _allClothing.AddRange(extraMainClothing3Types);
                _allClothing.AddRange(extraMainClothing4Types);
                _allClothing.AddRange(extraMainClothing5Types);
            });


            builder.RunBefore((input, output) =>
            {
                Defaults.Finalize.Invoke(input, output);
            });


            builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Goblins, input.U.SkinColor));
                int attackingOffset = input.A.IsAttacking ? 1 : 0;
                int eatingOffset = input.A.IsEating ? 2 : 0;
                int hurtOffset = input.U.IsDead && input.U.Items != null ? 3 : 0;
                if (input.U.HasBreasts)
                {
                    output.Sprite(input.Sprites.Gobbo[8 + attackingOffset + eatingOffset + hurtOffset + 8 * input.U.MouthType]);
                }
                else
                {
                    output.Sprite(input.Sprites.Gobbo[12 + attackingOffset + eatingOffset + hurtOffset + 8 * input.U.MouthType]);
                }
            });

            builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.IsDead && input.U.Items != null)
                {
                    int sprite = 80;
                    sprite += input.U.EyeType;
                    output.Sprite(input.Sprites.Gobbo[sprite]);
                }
                else if (input.U.HasBreasts)
                {
                    int sprite = 40;
                    int attackingOffset = input.A.IsAttacking ? 1 : 0;
                    if (input.U.EyeType > 8)
                    {
                        sprite += 2 * input.U.EyeType;
                        output.Sprite(input.Sprites.Gobbo[sprite - 16 + attackingOffset]);
                    }
                    else
                    {
                        sprite += 2 * input.U.EyeType;
                        output.Sprite(input.Sprites.Gobbo[sprite + attackingOffset]);
                    }
                }
                else
                {
                    int sprite = 56;
                    int attackingOffset = input.A.IsAttacking ? 1 : 0;
                    if (input.U.EyeType > 8)
                    {
                        sprite += 2 * input.U.EyeType;
                        output.Sprite(input.Sprites.Gobbo[sprite - 16 + attackingOffset]);
                    }
                    else
                    {
                        sprite += 2 * input.U.EyeType;
                        output.Sprite(input.Sprites.Gobbo[sprite + attackingOffset]);
                    }
                }
            });

            builder.RenderSingle(SpriteType.SecondaryEyes, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
                if (input.U.IsDead && input.U.Items != null)
                {
                }
                else
                {
                    if (input.U.HasBreasts)
                    {
                        int sprite = 72;
                        sprite += input.U.EyeType;
                        output.Sprite(input.Sprites.Gobbo[sprite]);
                    }
                    else
                    {
                        int sprite = 136;
                        sprite += input.U.EyeType;
                        output.Sprite(input.Sprites.Gobbo[sprite]);
                    }
                }
            });


            builder.RenderSingle(SpriteType.Hair, 9, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.UniversalHair, input.U.HairColor));
                if (input.U.ClothingExtraType5 == 1)
                {
                    output.Sprite(input.Sprites.Gobbohat[2 + 2 * input.U.HairStyle]);
                }
                else if (input.U.ClothingExtraType5 == 2)
                {
                    output.Sprite(input.Sprites.Gobbohat[36 + 2 * input.U.HairStyle]);
                }
                else
                {
                    output.Sprite(input.Sprites.Gobbo[96 + 2 * input.U.HairStyle]);
                }
            });

            builder.RenderSingle(SpriteType.Hair2, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.UniversalHair, input.U.HairColor));
                if (input.U.ClothingExtraType5 == 1)
                {
                    output.Sprite(input.Sprites.Gobbohat[3 + 2 * input.U.HairStyle]);
                }
                else if (input.U.ClothingExtraType5 == 2)
                {
                    output.Sprite(input.Sprites.Gobbohat[37 + 2 * input.U.HairStyle]);
                }
                else
                {
                    output.Sprite(input.Sprites.Gobbo[97 + 2 * input.U.HairStyle]);
                }
            });

            builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Goblins, input.U.SkinColor));
                int weightMod = Math.Min(input.U.BodySize * 2, 2);
                if (input.U.HasBreasts)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Gobbo[1 + weightMod]);
                        return;
                    }

                    output.Sprite(input.Sprites.Gobbo[0 + weightMod]);
                }
                else
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Gobbo[5 + weightMod]);
                        return;
                    }

                    output.Sprite(input.Sprites.Gobbo[4 + weightMod]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Goblins, input.U.SkinColor));
                output.Sprite(input.Sprites.Gobbo[32 + input.U.BodyAccentType1]);
            });

            builder.RenderSingle(SpriteType.BodyAccent2, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Goblins, input.U.SkinColor));
                output.Sprite(input.Sprites.Gobbo[36 + input.U.BodyAccentType2]);
            });

            builder.RenderSingle(SpriteType.Breasts, 19, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Goblins, input.U.SkinColor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.LeftBreastFullness > 0)
                {
                    int leftSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(22 * 22));

                    if (leftSize > 17)
                    {
                        leftSize = 17;
                    }


                    output.Sprite(input.Sprites.Gobbovore[leftSize]);
                }
                else
                {
                    if (input.U.DefaultBreastSize == 0)
                    {
                        output.Sprite(input.Sprites.Gobbovore[0]);
                        return;
                    }

                    output.Sprite(input.Sprites.Gobbovore[0 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.SecondaryBreasts, 19, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Goblins, input.U.SkinColor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.RightBreastFullness > 0)
                {
                    int rightSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(22 * 22));

                    if (rightSize > 17)
                    {
                        rightSize = 17;
                    }

                    output.Sprite(input.Sprites.Gobbovore[22 + rightSize]);
                }
                else
                {
                    if (input.U.DefaultBreastSize == 0)
                    {
                        output.Sprite(input.Sprites.Gobbovore[22]);
                        return;
                    }

                    output.Sprite(input.Sprites.Gobbovore[22 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 16, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Goblins, input.U.SkinColor));
                if (input.A.HasBelly)
                {
                    int size = input.A.GetStomachSize(32, 1.2f);

                    switch (size)
                    {
                        case 24:
                            output.AddOffset(0, 0 * .625f);
                            break;
                        case 25:
                            output.AddOffset(0, 0 * .625f);
                            break;
                        case 26:
                            output.AddOffset(0, -1 * .625f);
                            break;
                        case 27:
                            output.AddOffset(0, -5 * .625f);
                            break;
                        case 28:
                            output.AddOffset(0, -8 * .625f);
                            break;
                        case 29:
                            output.AddOffset(0, -15 * .625f);
                            break;
                        case 30:
                            output.AddOffset(0, -17 * .625f);
                            break;
                        case 31:
                            output.AddOffset(0, -19 * .625f);
                            break;
                        case 32:
                            output.AddOffset(0, -22 * .625f);
                            break;
                    }

                    //if (input.A.PredatorComponent.OnlyOnePreyAndLiving() && size >= 8 && size <= 13)
                    //    return Out.Update(State.GameManager.SpriteDictionary.Gobbovore[71]);

                    output.Sprite(input.Sprites.Gobbovore[70 + size]);
                }
            });

            builder.RenderSingle(SpriteType.Dick, 14, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Goblins, input.U.SkinColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect())
                {
                    if (input.A.PredatorComponent?.VisibleFullness < .75f)
                    {
                        output.Sprite(input.Sprites.Gobbo[129 + 2 * input.U.DickSize]).Layer(21);
                        return;
                    }

                    output.Sprite(input.Sprites.Gobbo[129 + 2 * input.U.DickSize]).Layer(14);
                    return;
                }

                output.Sprite(input.Sprites.Gobbo[128 + 2 * input.U.DickSize]).Layer(14);
            });

            builder.RenderSingle(SpriteType.Balls, 13, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Goblins, input.U.SkinColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                int size = input.A.GetBallSize(22, .8f);
                int baseSize = (input.U.DickSize + 1) / 3;

                int combined = Math.Min(baseSize + size + 2, 22);
                if (combined == 22)
                {
                    output.AddOffset(0, -2 * .625f);
                }
                else if (combined >= 21)
                {
                    output.AddOffset(0, -2 * .625f);
                }
                else if (combined >= 20)
                {
                    output.AddOffset(0, -1 * .625f);
                }
                else if (combined >= 19)
                {
                    output.AddOffset(0, 1 * .625f);
                }
                else if (combined >= 18)
                {
                    output.AddOffset(0, 0 * .625f);
                }

                if (size > 0)
                {
                    output.Sprite(input.Sprites.Gobbovore[44 + combined]);
                    return;
                }

                output.Sprite(input.Sprites.Gobbovore[44 + baseSize]);
            });

            builder.RenderSingle(SpriteType.Weapon, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    if (input.A.GetWeaponSprite() == 5)
                    {
                        return;
                    }

                    output.Sprite(input.Sprites.Gobbo[88 + input.A.GetWeaponSprite()]);
                }
            });


            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                Unit unit = data.Unit;

                unit.SkinColor = State.Rand.Next(data.MiscRaceData.SkinColors);
                unit.EyeType = State.Rand.Next(data.MiscRaceData.EyeTypes);
                unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
                unit.HairColor = State.Rand.Next(data.MiscRaceData.HairColors);
                unit.BodyAccentType1 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes1);
                unit.BodyAccentType2 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes2);
                unit.BodyAccentType3 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes3);
                unit.BodyAccentType4 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes4);
                unit.ClothingColor = State.Rand.Next(data.MiscRaceData.ClothingColors);
                unit.ClothingColor2 = State.Rand.Next(data.MiscRaceData.ClothingColors);
            });
        });

        // protected override Color HairColor(Actor_Unit actor) => Color.white;


        private static class All
        {
            internal static readonly IClothing GoblinOBottom1 = ClothingBuilder.Create( b =>
            {
                GoblinOBottom.MakeImpOBottom(b, 0, 2, false, 45, 8, 14, State.GameManager.SpriteDictionary.Gobboverbottoms, 12050);
            });
            internal static readonly IClothing GoblinOBottom2 = ClothingBuilder.Create( b =>
            {
                GoblinOBottom.MakeImpOBottom(b, 9, 11, false, 45, 17, 14, State.GameManager.SpriteDictionary.Gobboverbottoms, 12051);
            });
            internal static readonly IClothing GoblinOBottom3 = ClothingBuilder.Create( b =>
            {
                GoblinOBottom.MakeImpOBottom(b, 18, 20, true, 45, 26, 14, State.GameManager.SpriteDictionary.Gobboverbottoms, 12052);
            });
            internal static readonly IClothing GoblinOBottom4 = ClothingBuilder.Create( b =>
            {
                GoblinOBottom.MakeImpOBottom(b, 27, 29, true, 49, 35, 14, State.GameManager.SpriteDictionary.Gobboverbottoms, 12053);
            });
            internal static readonly IClothing GoblinOBottomAlt1 = ClothingBuilder.Create( b =>
            {
                GoblinOBottomAlt.MakeImpOBottomAlt(b, 27, 29, true, 49, 35, 14, State.GameManager.SpriteDictionary.Gobboverbottoms, 12053);
            });
            internal static readonly IClothing GoblinOBottom5 = ClothingBuilder.Create( b =>
            {
                GoblinOBottom.MakeImpOBottom(b, 36, 38, false, 45, 44, 14, State.GameManager.SpriteDictionary.Gobboverbottoms, 12054);
            });

            internal static readonly IClothing GenericLegs1 = ClothingBuilder.Create( b =>
            {
                GenericLegs.MakeGenericLegs(b, 0, 4, 45, 12055, femaleOnly: true, blocksDick: false);
            });
            internal static readonly IClothing GenericLegs2 = ClothingBuilder.Create( b =>
            {
                GenericLegs.MakeGenericLegs(b, 5, 9, 45, 12056, femaleOnly: true, blocksDick: false);
            });
            internal static readonly IClothing GenericLegs3 = ClothingBuilder.Create( b =>
            {
                GenericLegs.MakeGenericLegs(b, 10, 14, 45, 12057, femaleOnly: true, blocksDick: false);
            });
            internal static readonly IClothing GenericLegsAlt1 = ClothingBuilder.Create( b =>
            {
                GenericLegsAlt.MakeGenericLegsAlt(b, 10, 14, 45, 12057, femaleOnly: true, blocksDick: false);
            });
            internal static readonly IClothing GenericLegs4 = ClothingBuilder.Create( b =>
            {
                GenericLegs.MakeGenericLegs(b, 15, 19, 45, 12058, femaleOnly: true, blocksDick: false);
            });
            internal static readonly IClothing GenericLegsAlt2 = ClothingBuilder.Create( b =>
            {
                GenericLegsAlt.MakeGenericLegsAlt(b, 15, 19, 45, 12058, femaleOnly: true, blocksDick: false);
            });
            internal static readonly IClothing GenericLegs5 = ClothingBuilder.Create( b =>
            {
                GenericLegs.MakeGenericLegs(b, 20, 24, 45, 12059, femaleOnly: true, blocksDick: false);
            });
            internal static readonly IClothing GenericLegs6 = ClothingBuilder.Create( b =>
            {
                GenericLegs.MakeGenericLegs(b, 2, 4, 45, 12060, true, blocksDick: true, black: true);
            });
            internal static readonly IClothing GenericLegs7 = ClothingBuilder.Create( b =>
            {
                GenericLegs.MakeGenericLegs(b, 7, 9, 45, 12061, true, blocksDick: true, black: true);
            });
            internal static readonly IClothing GenericLegs8 = ClothingBuilder.Create( b =>
            {
                GenericLegs.MakeGenericLegs(b, 12, 14, 45, 12062, true, blocksDick: true);
            });
            internal static readonly IClothing GenericLegs9 = ClothingBuilder.Create( b =>
            {
                GenericLegs.MakeGenericLegs(b, 17, 19, 45, 12063, true, blocksDick: true);
            });
            internal static readonly IClothing GenericLegs10 = ClothingBuilder.Create( b =>
            {
                GenericLegs.MakeGenericLegs(b, 22, 24, 45, 12064, true, blocksDick: false);
            });

            internal static readonly IClothing GenericGloves1 = ClothingBuilder.Create( b =>
            {
                GenericGloves.MakeGenericGloves(b, 0, 8, 12065);
            });
            internal static readonly IClothing GenericGlovesPlusSecond1 = ClothingBuilder.Create( b =>
            {
                GenericGlovesPlusSecond.MakeGenericGlovesPlusSecond(b, 9, 17, 12066);
            });
            internal static readonly IClothing GenericGlovesPlusSecondAlt1 = ClothingBuilder.Create( b =>
            {
                GenericGlovesPlusSecondAlt.MakeGenericGlovesPlusSecondAlt(b, 9, 17, 12066);
            });
            internal static readonly IClothing GenericGlovesPlusSecond2 = ClothingBuilder.Create( b =>
            {
                GenericGlovesPlusSecond.MakeGenericGlovesPlusSecond(b, 18, 26, 12067);
            });
            internal static readonly IClothing GenericGlovesPlusSecondAlt2 = ClothingBuilder.Create( b =>
            {
                GenericGlovesPlusSecondAlt.MakeGenericGlovesPlusSecondAlt(b, 18, 26, 12067);
            });
            internal static readonly IClothing GenericGloves2 = ClothingBuilder.Create( b =>
            {
                GenericGloves.MakeGenericGloves(b, 27, 35, 12068);
            });

            internal static readonly IClothing GoblinHat1 = ClothingBuilder.Create( b =>
            {
                GoblinHat.MakeHat(b, 0, 0, State.GameManager.SpriteDictionary.Gobbohat, 12069);
            });
            internal static readonly IClothing GoblinHat2 = ClothingBuilder.Create( b =>
            {
                GoblinHat.MakeHat(b, 34, 0, State.GameManager.SpriteDictionary.Gobbohat, 12070);
            });
        }


        private static class GenericGloves
        {
            internal static void MakeGenericGloves(IClothingBuilder builder, int start, int discard, int type)
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.DiscardSprite = input.Sprites.Gobbglove[discard];
                    output.Type = type;
                    output.FixedColor = false;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(5);
                    if (input.U.HasBreasts)
                    {
                        int weightMod = input.U.BodySize * 2;
                        output["Clothing1"].Sprite(input.A.IsAttacking ? input.Sprites.Gobbglove[start + 1 + weightMod] : input.Sprites.Gobbglove[start + weightMod]);
                    }
                    else
                    {
                        int weightMod = input.U.BodySize * 2;
                        output["Clothing1"].Sprite(input.A.IsAttacking ? input.Sprites.Gobbglove[start + 5 + weightMod] : input.Sprites.Gobbglove[start + 4 + weightMod]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            }
        }

        private static class GenericGlovesPlusSecond
        {
            internal static void MakeGenericGlovesPlusSecond(IClothingBuilder builder, int start, int discard, int type)
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.DiscardSprite = input.Sprites.Gobbglove[discard];
                    output.Type = type;
                    output.FixedColor = false;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(5);
                    if (input.U.HasBreasts)
                    {
                        int weightMod = input.U.BodySize * 2;
                        output["Clothing1"].Sprite(input.A.IsAttacking ? input.Sprites.Gobbglove[start + 1 + weightMod] : input.Sprites.Gobbglove[start + weightMod]);
                    }
                    else
                    {
                        int weightMod = input.U.BodySize * 2;
                        output["Clothing1"].Sprite(input.A.IsAttacking ? input.Sprites.Gobbglove[start + 5 + weightMod] : input.Sprites.Gobbglove[start + 4 + weightMod]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            }
        }

        private static class GenericGlovesPlusSecondAlt
        {
            internal static void MakeGenericGlovesPlusSecondAlt(IClothingBuilder builder, int start, int discard, int type)
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.DiscardSprite = input.Sprites.Gobbglove[discard];
                    output.Type = type;
                    //output.ClothingDefaults1 = SpriteExtraInfo.MakeSpriteExtraInfo(5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.ClothingColor), null); // seems to be overwritten always
                    output.FixedColor = false;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(5);
                    if (input.U.HasBreasts)
                    {
                        int weightMod = input.U.BodySize * 2;
                        output["Clothing1"].Sprite(input.A.IsAttacking ? input.Sprites.Gobbglove[start + 1 + weightMod] : input.Sprites.Gobbglove[start + weightMod]);
                    }
                    else
                    {
                        int weightMod = input.U.BodySize * 2;
                        output["Clothing1"].Sprite(input.A.IsAttacking ? input.Sprites.Gobbglove[start + 5 + weightMod] : input.Sprites.Gobbglove[start + 4 + weightMod]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                });
            }
        }

        private static class GenericLegs
        {
            internal static void MakeGenericLegs(IClothingBuilder builder, int start, int discard, int bulge, int type, bool maleOnly = false, bool femaleOnly = false, bool blocksDick = false, bool black = false)
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.RevealsDick = !blocksDick;
                    output.Type = type;
                    output.DiscardSprite = input.Sprites.Gobleggo[discard];
                    if (maleOnly)
                    {
                        output.MaleOnly = true;
                    }

                    if (femaleOnly)
                    {
                        output.FemaleOnly = true;
                    }

                    output.FixedColor = true; // TODO probably a bug 
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(11);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Coloring(Color.white);
                    int weightMod = input.U.BodySize;
                    output["Clothing1"].Sprite(input.Sprites.Gobleggo[start + weightMod]);
                    if (input.U.HasDick)
                    {
                        if (blocksDick)
                        {
                            output["Clothing2"].Sprite(black ? input.Sprites.Gobbunderbottoms[bulge + 4 + input.U.DickSize] : input.Sprites.Gobbunderbottoms[bulge + input.U.DickSize]);

                            if (input.U.GetGender() == Gender.Hermaphrodite)
                            {
                                output["Clothing2"].SetOffset(0, 2 * .625f);
                            }
                            else
                            {
                                output["Clothing2"].SetOffset(0, 0);
                            }
                        }
                        else
                        {
                            output["Clothing2"].Sprite(null);
                        }
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            }
        }

        private static class GenericLegsAlt
        {
            internal static void MakeGenericLegsAlt(IClothingBuilder builder, int start, int discard, int bulge, int type, bool maleOnly = false, bool femaleOnly = false, bool blocksDick = false, bool black = false)
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.RevealsDick = !blocksDick;
                    output.Type = type;
                    output.DiscardSprite = input.Sprites.Gobleggo[discard];
                    if (maleOnly)
                    {
                        output.MaleOnly = true;
                    }

                    if (femaleOnly)
                    {
                        output.FemaleOnly = true;
                    }

                    output.FixedColor = true; // Probably a bug
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(11);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Coloring(Color.white);
                    int weightMod = input.U.BodySize;
                    output["Clothing1"].Sprite(input.Sprites.Gobleggo[start + weightMod]);
                    if (input.U.HasDick)
                    {
                        if (blocksDick)
                        {
                            output["Clothing2"].Sprite(black ? input.Sprites.Gobbunderbottoms[bulge + 4 + input.U.DickSize] : input.Sprites.Gobbunderbottoms[bulge + input.U.DickSize]);

                            if (input.U.GetGender() == Gender.Hermaphrodite)
                            {
                                output["Clothing2"].SetOffset(0, 2 * .625f);
                            }
                            else
                            {
                                output["Clothing2"].SetOffset(0, 0);
                            }
                        }
                        else
                        {
                            output["Clothing2"].Sprite(null);
                        }
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                });
            }
        }

        private static class ImpUBottom
        {
            internal static readonly IClothing ImpUBottom1 = ClothingBuilder.Create( b =>
            {
                MakeImpUBottom(b, 0, 2, 45, 8, 9, State.GameManager.SpriteDictionary.Gobbunderbottoms, 12045);
            });
            internal static readonly IClothing ImpUBottom2 = ClothingBuilder.Create( b =>
            {
                MakeImpUBottom(b, 9, 11, 45, 17, 9, State.GameManager.SpriteDictionary.Gobbunderbottoms, 12046);
            });
            internal static readonly IClothing ImpUBottom3 = ClothingBuilder.Create( b =>
            {
                MakeImpUBottom(b, 18, 20, 45, 26, 9, State.GameManager.SpriteDictionary.Gobbunderbottoms, 12047, true);
            });
            internal static readonly IClothing ImpUBottom4 = ClothingBuilder.Create( b =>
            {
                MakeImpUBottom(b, 27, 29, 45, 35, 9, State.GameManager.SpriteDictionary.Gobbunderbottoms, 12048);
            });
            internal static readonly IClothing ImpUBottom5 = ClothingBuilder.Create( b =>
            {
                MakeImpUBottom(b, 36, 38, 45, 44, 9, State.GameManager.SpriteDictionary.Gobbunderbottoms, 12049);
            });

            private static void MakeImpUBottom(IClothingBuilder builder, int sprF, int sprM, int bulge, int discard, int layer, Sprite[] sheet, int type, bool black = false)
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.DiscardSprite = sheet[discard];
                    output.Type = type;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(layer + 1);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(layer);
                    output["Clothing1"].Coloring(Color.white);
                    int weightMod = input.U.BodySize;
                    if (input.A.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.U.HasBreasts ? sheet[sprF + 4 + weightMod] : sheet[sprM + 4 + weightMod]);

                        if (input.U.HasDick)
                        {
                            //if (output.BlocksDick == true)
                            if (true) // This was always true, pretty sure
                            {
                                output["Clothing2"].Sprite(black ? input.Sprites.Gobbunderbottoms[bulge + 4 + input.U.DickSize] : input.Sprites.Gobbunderbottoms[bulge + input.U.DickSize]);

                                if (input.U.GetGender() == Gender.Hermaphrodite)
                                {
                                    output["Clothing2"].SetOffset(0, 2 * .625f);
                                }
                                else
                                {
                                    output["Clothing2"].SetOffset(0, 0);
                                }
                            }
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.U.HasBreasts ? sheet[sprF + weightMod] : sheet[sprM + weightMod]);

                        if (input.U.HasDick)
                        {
                            //if (output.BlocksDick == true)
                            if (true) // This was always true, pretty sure
                            {
                                output["Clothing2"].Sprite(black ? input.Sprites.Gobbunderbottoms[bulge + 4 + input.U.DickSize] : input.Sprites.Gobbunderbottoms[bulge + input.U.DickSize]);

                                if (input.U.GetGender() == Gender.Hermaphrodite)
                                {
                                    output["Clothing2"].SetOffset(0, 2 * .625f);
                                }
                                else
                                {
                                    output["Clothing2"].SetOffset(0, 0);
                                }
                            }
                        }
                        else
                        {
                            output["Clothing2"].Sprite(null);
                        }
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                });
            }
        }

        private static class GobboLeotard
        {
            internal static readonly IClothing GobboLeotardInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Gobbunderonepieces[74];
                    output.Type = 12071;
                    output.OccupiesAllSlots = true;
                    output.DiscardUsesPalettes = true;
                });


                builder.RenderAll((input, output) =>
                {
                    output["Clothing3"].Layer(13);
                    output["Clothing2"].Layer(20);
                    output["Clothing1"].Layer(17);
                    int weightMod = input.U.BodySize;
                    int bobs = input.U.BreastSize;
                    if (bobs > 7)
                    {
                        bobs = 7;
                    }

                    int size = input.A.GetStomachSize(32, 1.2f);
                    if (size > 7)
                    {
                        size = 7;
                    }

                    if (input.U.HasBreasts)
                    {
                        if (input.U.HasDick)
                        {
                            output["Clothing3"].Sprite(input.Sprites.Gobbunderbottoms[45 + input.U.DickSize]);
                            if (input.U.GetGender() == Gender.Hermaphrodite)
                            {
                                output["Clothing3"].SetOffset(0, 2 * .625f);
                            }
                            else
                            {
                                output["Clothing3"].SetOffset(0, 0);
                            }
                        }

                        output["Clothing2"].Sprite(bobs == 7 ? null : input.Sprites.Gobbunderonepieces[33 + bobs]);

                        output["Clothing1"].Sprite(input.Sprites.Gobbunderonepieces[41 + size + 8 * weightMod]);

                        output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced,
                            input.U.ClothingColor2));
                        output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced,
                            input.U.ClothingColor2));
                        output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced,
                            input.U.ClothingColor2));
                        output.ChangeSprite(SpriteType.Belly).Coloring(ColorPaletteMap.GetPalette(SwapType.Goblins, input.U.SkinColor));
                    }
                    else
                    {
                        output["Clothing1"].Layer(13);
                        output["Clothing2"].Layer(20);
                        output["Clothing3"].Layer(16);

                        output["Clothing3"].Sprite(input.Sprites.Gobbunderbottoms[45 + input.U.DickSize]);

                        if (input.U.GetGender() == Gender.Hermaphrodite)
                        {
                            output["Clothing3"].SetOffset(0, 2 * .625f);
                        }
                        else
                        {
                            output["Clothing3"].SetOffset(0, 0);
                        }

                        output["Clothing2"].Sprite(null);
                        output["Clothing1"].Sprite(input.Sprites.Gobbunderonepieces[57 + size + 9 * weightMod]);
                        output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                        output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                        output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                        output.ChangeSprite(SpriteType.Belly).Coloring(ColorPaletteMap.GetPalette(SwapType.SkinToClothing, input.U.ClothingColor));
                    }
                });
            });
        }
    }

    internal static class GobboCasinoBunny
    {
        internal static readonly IClothing GobboCasinoBunnyInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Gobbunderonepieces[74];
                output.Type = 12072;
                output.OccupiesAllSlots = true;
                output.DiscardUsesPalettes = true;
            });


            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(13);
                output["Clothing2"].Layer(20);
                output["Clothing1"].Layer(17);
                int weightMod = input.U.BodySize;
                int bobs = input.U.BreastSize;
                if (bobs > 7)
                {
                    bobs = 7;
                }

                int size = input.A.GetStomachSize(32, 1.2f);
                if (size > 5)
                {
                    size = 5;
                }

                if (input.U.HasBreasts)
                {
                    if (input.U.HasDick)
                    {
                        output["Clothing3"].Sprite(input.Sprites.Gobbunderbottoms[45 + input.U.DickSize]);
                        if (input.U.GetGender() == Gender.Hermaphrodite)
                        {
                            output["Clothing3"].SetOffset(0, 2 * .625f);
                        }
                        else
                        {
                            output["Clothing3"].SetOffset(0, 0);
                        }
                    }
                    else
                    {
                        output["Clothing3"].Sprite(null);
                    }


                    output["Clothing2"].Sprite(input.Sprites.Gobbunderonepieces[0 + bobs]);
                    output["Clothing1"].Sprite(input.Sprites.Gobbunderonepieces[8 + size + 6 * weightMod]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced,
                        input.U.ClothingColor2));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced,
                        input.U.ClothingColor2));
                    output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced,
                        input.U.ClothingColor2));
                    output.ChangeSprite(SpriteType.Belly).Coloring(ColorPaletteMap.GetPalette(SwapType.Goblins, input.U.SkinColor));
                }
                else
                {
                    output["Clothing1"].Layer(13);
                    output["Clothing2"].Layer(20);
                    output["Clothing3"].Layer(16);
                    output["Clothing3"].Sprite(input.Sprites.Gobbunderbottoms[45 + input.U.DickSize]);
                    if (input.U.GetGender() == Gender.Hermaphrodite)
                    {
                        output["Clothing3"].SetOffset(0, 2 * .625f);
                    }
                    else
                    {
                        output["Clothing3"].SetOffset(0, 0);
                    }

                    output["Clothing2"].Sprite(null);
                    output["Clothing1"].Sprite(input.Sprites.Gobbunderonepieces[20 + size + 6 * weightMod]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                    output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                    output.ChangeSprite(SpriteType.Belly).Coloring(ColorPaletteMap.GetPalette(SwapType.SkinToClothing, input.U.ClothingColor));
                }
            });
        });
    }


    internal static class GoblinOBottom
    {
        internal static void MakeImpOBottom(IClothingBuilder builder, int sprF, int sprM, bool showbulge, int bulge, int discard, int layer,
            Sprite[] sheet, int type)
        {
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
                int weightMod = input.U.BodySize;
                if (input.A.HasBelly)
                {
                    output["Clothing1"].Sprite(input.U.HasBreasts ? sheet[sprF + 4 + weightMod] : sheet[sprM + 4 + weightMod]);

                    if (input.U.HasDick && showbulge)
                    {
                        //if (output.BlocksDick == true)
                        if (true) // This was always true, pretty sure
                        {
                            output["Clothing2"].Sprite(input.Sprites.Gobbunderbottoms[bulge + input.U.DickSize]);
                        }

                        if (input.U.GetGender() == Gender.Hermaphrodite)
                        {
                            output["Clothing2"].SetOffset(0, 2 * .625f);
                        }
                        else
                        {
                            output["Clothing2"].SetOffset(0, 0);
                        }
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(input.U.HasBreasts ? sheet[sprF + weightMod] : sheet[sprM + weightMod]);

                    if (input.U.HasDick)
                    {
                        //if (output.BlocksDick == true && showbulge == true)
                        output["Clothing2"].Sprite(showbulge ? input.Sprites.Gobbunderbottoms[bulge + input.U.DickSize] : null); // first condition always true, pretty sure

                        if (input.U.GetGender() == Gender.Hermaphrodite)
                        {
                            output["Clothing2"].SetOffset(0, 2 * .625f);
                        }
                        else
                        {
                            output["Clothing2"].SetOffset(0, 0);
                        }
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
        }
    }


    internal static class GoblinOBottomAlt
    {
        internal static void MakeImpOBottomAlt(IClothingBuilder builder, int sprF, int sprM, bool showbulge, int bulge, int discard,
            int layer, Sprite[] sheet, int type)
        {
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
                int weightMod = input.U.BodySize;
                if (input.A.HasBelly)
                {
                    output["Clothing1"].Sprite(input.U.HasBreasts ? sheet[sprF + 4 + weightMod] : sheet[sprM + 4 + weightMod]);

                    if (input.U.HasDick && showbulge)
                    {
                        //if (output.BlocksDick == true)
                        if (true) // This was always true, pretty sure
                        {
                            output["Clothing2"].Sprite(input.Sprites.Gobbunderbottoms[bulge + input.U.DickSize]);
                        }

                        if (input.U.GetGender() == Gender.Hermaphrodite)
                        {
                            output["Clothing2"].SetOffset(0, 2 * .625f);
                        }
                        else
                        {
                            output["Clothing2"].SetOffset(0, 0);
                        }
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(input.U.HasBreasts ? sheet[sprF + weightMod] : sheet[sprM + weightMod]);

                    if (input.U.HasDick)
                    {
                        //if (output.BlocksDick == true && showbulge == true)
                        if (showbulge) // first condition always true, pretty sure
                        {
                            output["Clothing2"].Sprite(input.Sprites.Gobbunderbottoms[bulge + input.U.DickSize]);
                        }

                        if (input.U.GetGender() == Gender.Hermaphrodite)
                        {
                            output["Clothing2"].SetOffset(0, 2 * .625f);
                        }
                        else
                        {
                            output["Clothing2"].SetOffset(0, 0);
                        }
                    }
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
            });
        }
    }

    internal static class GoblinHat
    {
        internal static void MakeHat(IClothingBuilder builder, int start, int discard, Sprite[] sheet, int type)
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardSprite = sheet[discard];
                output.Type = type;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(2);
                output["Clothing1"].Layer(22);
                output["Clothing1"].Sprite(sheet[start]);
                output["Clothing2"].Sprite(sheet[start + 1]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
        }
    }

    internal static class GobboUndertop1
    {
        internal static readonly BindableClothing<IOverSizeParameters> GobboUndertop1Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Gobundertops[8];
                output.Type = 12073;
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output, extra) =>
            {
                output["Clothing1"].Layer(20);
                if (extra.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Gobundertops[7]);
                }
                else if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Gobundertops[0 + input.U.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
            });
        });
    }

    internal static class GobboUndertop2
    {
        internal static readonly BindableClothing<IOverSizeParameters> GobboUndertop2Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Gobundertops[17];
                output.Type = 12074;
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output, extra) =>
            {
                output["Clothing1"].Layer(20);
                if (extra.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Gobundertops[16]);
                }
                else if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Gobundertops[9 + input.U.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
            });
        });
    }

    internal static class GobboUndertop3
    {
        internal static readonly BindableClothing<IOverSizeParameters> GobboUndertop3Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Gobundertops[26];
                output.Type = 12075;
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output, extra) =>
            {
                output["Clothing1"].Layer(20);
                if (extra.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                }
                else if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Gobundertops[18 + input.U.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
            });
        });
    }

    internal static class GobboUndertop4
    {
        internal static readonly BindableClothing<IOverSizeParameters> GobboUndertop4Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Gobundertops[35];
                output.Type = 12076;
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output, extra) =>
            {
                output["Clothing1"].Layer(20);
                if (extra.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Gobundertops[34]);
                }
                else if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Gobundertops[27 + input.U.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
            });
        });
    }

    internal static class GobboUndertop5
    {
        internal static readonly BindableClothing<IOverSizeParameters> GobboUndertop5Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Gobundertops[75];
                output.Type = 12077;
                output.FemaleOnly = false;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output, extra) =>
            {
                output["Clothing2"].Layer(16);
                output["Clothing1"].Layer(20);
                int weightMod = input.U.BodySize;
                int size = input.A.GetStomachSize(32, 1.2f);
                if (size > 5)
                {
                    size = 5;
                }

                if (extra.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Gobundertops[48 + 13 * weightMod]);
                }
                else if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Gobundertops[42 + input.U.BreastSize + 13 * weightMod]);
                }

                output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Gobundertops[36 + size + 13 * weightMod] : input.Sprites.Gobundertops[62 + size + 6 * weightMod]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
            });
        });
    }

    internal static class GobboOverOpFem
    {
        internal static readonly BindableClothing<IOverSizeParameters> GobboOverOpFemInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Gobboveronepieces[23];
                output.Type = 12078;
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output, extra) =>
            {
                output["Clothing2"].Layer(18);
                output["Clothing1"].Layer(20);
                int weightMod = input.U.BodySize;
                int size = input.A.GetStomachSize(32, 1.2f);
                if (size > 7)
                {
                    size = 7;
                }

                int bobs = input.U.BreastSize;
                if (bobs > 7)
                {
                    bobs = 7;
                }

                {
                    if (extra.Oversize || input.U.HasBreasts == false)
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Gobboveronepieces[0 + bobs]);
                    }
                }
                output["Clothing2"].Sprite(input.Sprites.Gobboveronepieces[7 + size + 8 * weightMod]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
        });
    }

    internal static class GobboOverOpm
    {
        internal static IClothing GobboOverOpmInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Gobboveronepieces[40];
                output.Type = 12079;
                output.MaleOnly = true;
                output.RevealsBreasts = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(20);
                output["Clothing1"].Layer(20);
                int weightMod = input.U.BodySize;
                int size = input.A.GetStomachSize(32, 1.2f);
                if (size > 6)
                {
                    size = 6;
                }

                output["Clothing2"].Sprite(input.A.IsAttacking ? input.Sprites.Gobboveronepieces[25] : input.Sprites.Gobboveronepieces[24]);


                output["Clothing1"].Sprite(input.Sprites.Gobboveronepieces[26 + size + 7 * weightMod]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
        });
    }

    internal static class GobboOverTop1
    {
        internal static readonly BindableClothing<IOverSizeParameters> GobboOverTop1Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Gobbovertops[3];
                output.Type = 12080;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output, extra) =>
            {
                output["Clothing2"].Layer(3);
                output["Clothing1"].Layer(21);
                int weightMod = input.U.BodySize;

                if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Gobbovertops[0 + weightMod]);
                    output["Clothing2"].Sprite(input.Sprites.Gobbovertops[2]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Gobbovertops[16 + weightMod]);
                    output["Clothing2"].Sprite(input.Sprites.Gobbovertops[18]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
        });
    }

    internal static class GobboOverTop2
    {
        internal static readonly IClothing GobboOverTop2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Gobbovertops[7];
                output.Type = 12081;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(3);
                output["Clothing1"].Layer(21);
                int weightMod = input.U.BodySize;

                if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Gobbovertops[4 + weightMod]);
                    output["Clothing2"].Sprite(input.Sprites.Gobbovertops[6]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Gobbovertops[20 + weightMod]);
                    output["Clothing2"].Sprite(input.Sprites.Gobbovertops[22]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
        });
    }

    internal static class GobboOverTop3
    {
        internal static readonly IClothing GobboOverTop3Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Gobbovertops[11];
                output.Type = 12082;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(3);
                output["Clothing1"].Layer(21);
                int weightMod = input.U.BodySize;


                if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Gobbovertops[8 + weightMod]);
                    output["Clothing2"].Sprite(input.Sprites.Gobbovertops[10]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Gobbovertops[24 + weightMod]);
                    output["Clothing2"].Sprite(input.Sprites.Gobbovertops[26]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
        });
    }

    internal static class GobboOverTop4
    {
        internal static readonly IClothing GobboOverTop4Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Gobbovertops[15];
                output.Type = 12083;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(3);
                output["Clothing1"].Layer(21);
                int weightMod = input.U.BodySize;

                if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Gobbovertops[12 + weightMod]);
                    output["Clothing2"].Sprite(input.Sprites.Gobbovertops[14]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Gobbovertops[28 + weightMod]);
                    output["Clothing2"].Sprite(input.Sprites.Gobbovertops[30]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
        });
    }

    internal static class GoblinHolidayHat
    {
        internal static readonly IClothing HolidayHatInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.ReqWinterHoliday = true;
                output.DiscardSprite = null;
                output.Type = 0;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(29);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.ImpGobHat[1]);
            });
        });
    }
}