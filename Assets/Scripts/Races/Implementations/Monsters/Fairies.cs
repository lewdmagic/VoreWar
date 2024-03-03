using System;
using System.Collections.Generic;
using UnityEngine;

namespace Races.Graphics.Implementations.Monsters
{
    internal enum FairyType
    {
        Spring,
        Summer,
        Fall,
        Winter
    }

    internal static class FairyUtil
    {
        internal static void SetSeason(IUnitRead unit, FairyType season)
        {
            unit.BodyAccentType1 = (int)season;
            switch (season)
            {
                case FairyType.Spring:
                    unit.InnateSpells = new List<SpellType> { SpellType.Speed };
                    break;
                case FairyType.Summer:
                    unit.InnateSpells = new List<SpellType> { SpellType.Valor };
                    break;
                case FairyType.Fall:
                    unit.InnateSpells = new List<SpellType> { SpellType.Predation };
                    break;
                case FairyType.Winter:
                    unit.InnateSpells = new List<SpellType> { SpellType.Shield };
                    break;
            }
        }

        internal static FairyType GetSeason(IUnitRead unit)
        {
            return (FairyType)unit.BodyAccentType1;
        }

        internal static ColorSwapPalette GetClothesColor(IActorUnit actor)
        {
            switch (GetSeason(actor.Unit))
            {
                case FairyType.Spring:
                    return ColorPaletteMap.GetPalette(SwapType.FairySpringClothes, actor.Unit.ClothingColor);
                case FairyType.Summer:
                    return ColorPaletteMap.GetPalette(SwapType.FairySummerClothes, actor.Unit.ClothingColor);
                case FairyType.Fall:
                    return ColorPaletteMap.GetPalette(SwapType.FairyFallClothes, actor.Unit.ClothingColor);
                default:
                    return ColorPaletteMap.GetPalette(SwapType.FairyWinterClothes, actor.Unit.ClothingColor);
            }
        }
    }

    internal static class Fairies
    {
        internal const float GeneralSizeMod = 0.8f;

        private static FairyParameters CalcFairyParameters(IActorUnit actor)
        {
            return new FairyParameters()
            {
                Season = (FairyType)actor.Unit.BodyAccentType1, // TODO fix dirty enum casting
                Encumbered = actor.PredatorComponent?.Fullness > 0, // Not 100% accurate, but saves effort
                VeryEncumbered = actor.GetRootedStomachSize(19, GeneralSizeMod) > 16
            };
        }

        internal class FairyParameters : IParameters
        {
            internal bool Encumbered;
            internal bool VeryEncumbered;
            internal FairyType Season;
        }

        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            RaceFrameList springWings = new RaceFrameList(new int[3] { 91, 92, 93 }, new float[3] { .2f, .2f, .2f });
            RaceFrameList summerWings = new RaceFrameList(new int[3] { 94, 95, 96 }, new float[3] { .2f, .2f, .2f });
            RaceFrameList fallWings = new RaceFrameList(new int[3] { 97, 98, 99 }, new float[3] { .2f, .2f, .2f });
            RaceFrameList winterWings = new RaceFrameList(new int[3] { 100, 101, 102 }, new float[3] { .2f, .2f, .2f });
            RaceFrameList springWingsEnc = new RaceFrameList(new int[3] { 103, 104, 105 }, new float[3] { .2f, .2f, .2f });
            RaceFrameList summerWingsEnc = new RaceFrameList(new int[3] { 106, 107, 108 }, new float[3] { .2f, .2f, .2f });
            RaceFrameList fallWingsEnc = new RaceFrameList(new int[3] { 109, 110, 111 }, new float[3] { .2f, .2f, .2f });
            RaceFrameList winterWingsEnc = new RaceFrameList(new int[3] { 112, 113, 114 }, new float[3] { .2f, .2f, .2f });

            builder.Setup((input, output) =>
            {
                output.Names("Fairy", "Fairies");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 7,
                    StomachSize = 10,
                    HasTail = false,
                    FavoredStat = Stat.Mind,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.BreastVore, VoreType.CockVore, VoreType.Anal },
                    ExpMultiplier = 1.1f,
                    PowerAdjustment = 1.2f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new StatRange(4, 8),
                        Dexterity = new StatRange(9, 14),
                        Endurance = new StatRange(6, 12),
                        Mind = new StatRange(14, 22),
                        Will = new StatRange(8, 14),
                        Agility = new StatRange(14, 22),
                        Voracity = new StatRange(8, 14),
                        Stomach = new StatRange(5, 10),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.ArtfulDodge,
                        TraitType.Flight,
                        TraitType.EscapeArtist,
                        TraitType.KeenReflexes,
                        TraitType.EasyToVore
                    },
                    RaceDescription = ""
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.HatType, "Leg Accessory");
                    buttons.SetText(ButtonType.ClothingAccessoryType, "Arm Accessory");
                    buttons.SetText(ButtonType.BodyAccentTypes1, "Fairy Season");
                });
                output.BreastSizes = () => 3;
                output.DickSizes = () => 2;
                output.GentleAnimation = true;
                output.ExtendedBreastSprites = true;

                output.HairStyles = 8;
                output.BodyAccentTypes1 = 4;
                output.SkinColors = 3;
                output.HairColors = 5;
                output.ClothingColors = 5;

                IClothing nightie = Nightie.NightieInstance;
                IClothing onePiece = OnePiece.OnePieceInstance;
                IClothing twoPiece = TwoPiece.TwoPieceInstance;
                IClothing dress = Dress.DressInstance;
                IClothing loincloth = Loincloth.LoinclothInstance;

                IClothing sleeves = Sleeves.SleevesInstance;
                IClothing bracelets = Bracelets.BraceletsInstance;
                IClothing leggings = Leggings.LeggingsInstance;
                IClothing sandals = Sandals.SandalsInstance;

                output.AllowedMainClothingTypes.Set(
                    nightie,
                    onePiece,
                    twoPiece,
                    dress,
                    loincloth
                );
                output.AllowedClothingAccessoryTypes.Set(
                    sleeves,
                    bracelets
                );
                output.AllowedClothingHatTypes.Set(
                    leggings,
                    sandals
                );
            });


            builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
            {
                output.Coloring(GetHairColor(input, input.Actor));
                if (input.U.HairStyle < 3)
                {
                    output.Sprite(input.Sprites.Fairy[2 * input.U.HairStyle]);
                    return;
                }

                if (input.U.HairStyle > 3)
                {
                    output.Sprite(input.Sprites.FairyExtraHair[Math.Min(input.U.HairStyle - 4, 3)]);
                    return;
                }

                switch (CalcFairyParameters(input.A).Season)
                {
                    case FairyType.Spring:
                        output.Sprite(input.Sprites.Fairy[6]);
                        return;
                    case FairyType.Summer:
                        output.Sprite(input.Sprites.Fairy[8]);
                        return;
                    case FairyType.Fall:
                        output.Sprite(input.Sprites.Fairy[7]);
                        return;
                    case FairyType.Winter:
                        output.Sprite(input.Sprites.Fairy[10]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
            {
                output.Coloring(GetHairColor(input, input.Actor));
                if (input.U.HairStyle < 3)
                {
                    output.Sprite(input.Sprites.Fairy[1 + 2 * input.U.HairStyle]);
                    return;
                }

                if (input.U.HairStyle > 3)
                {
                    return;
                }

                if (CalcFairyParameters(input.A).Season == FairyType.Summer)
                {
                    output.Sprite(input.Sprites.Fairy[9]);
                }
            });

            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(GetSkinColor(input, input.Actor));
                if (CalcFairyParameters(input.A).VeryEncumbered)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Fairy[205]);
                        return;
                    }

                    output.Sprite(input.Sprites.Fairy[204]);
                    return;
                }

                if (CalcFairyParameters(input.A).Encumbered)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Fairy[87]);
                        return;
                    }

                    output.Sprite(input.Sprites.Fairy[84]);
                    return;
                }

                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Fairy[85]);
                    return;
                }

                output.Sprite(input.Sprites.Fairy[82]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 16, (input, output) =>
            {
                output.Coloring(GetSkinColor(input, input.Actor));
                if (CalcFairyParameters(input.A).VeryEncumbered)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Fairy[206]);
                        return;
                    }

                    return;
                }

                if (CalcFairyParameters(input.A).Encumbered)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Fairy[88]);
                    }
                }
                else
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Fairy[86]);
                    }
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent2, 4, (input, output) =>
            {
                output.Coloring(GetSkinColor(input, input.Actor));
                if (CalcFairyParameters(input.A).VeryEncumbered && input.A.IsEating)
                {
                }

                if (CalcFairyParameters(input.A).Encumbered)
                {
                    output.Sprite(input.Sprites.Fairy[83]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent3, 0, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.AnimationController.FrameLists == null)
                {
                    SetUpAnimations(input.Actor);
                }

                if (input.A.AnimationController.FrameLists[0].CurrentTime >= springWings.Times[input.A.AnimationController.FrameLists[0].CurrentFrame] && input.U.IsDead == false)
                {
                    input.A.AnimationController.FrameLists[0].CurrentFrame++;
                    input.A.AnimationController.FrameLists[0].CurrentTime = 0f;

                    if (input.A.AnimationController.FrameLists[0].CurrentFrame >= springWings.Frames.Length)
                    {
                        input.A.AnimationController.FrameLists[0].CurrentFrame = 0;
                        input.A.AnimationController.FrameLists[0].CurrentTime = 0f;
                    }
                }

                if (CalcFairyParameters(input.A).Encumbered)
                {
                    switch (CalcFairyParameters(input.A).Season)
                    {
                        case FairyType.Spring:
                            output.Sprite(input.Sprites.Fairy[springWingsEnc.Frames[input.A.AnimationController.FrameLists[0].CurrentFrame]]);
                            return;
                        case FairyType.Summer:
                            output.Sprite(input.Sprites.Fairy[summerWingsEnc.Frames[input.A.AnimationController.FrameLists[0].CurrentFrame]]);
                            return;
                        case FairyType.Fall:
                            output.Sprite(input.Sprites.Fairy[fallWingsEnc.Frames[input.A.AnimationController.FrameLists[0].CurrentFrame]]);
                            return;
                        default:
                            output.Sprite(input.Sprites.Fairy[winterWingsEnc.Frames[input.A.AnimationController.FrameLists[0].CurrentFrame]]);
                            return;
                    }
                }

                switch (CalcFairyParameters(input.A).Season)
                {
                    case FairyType.Spring:
                        output.Sprite(input.Sprites.Fairy[springWings.Frames[input.A.AnimationController.FrameLists[0].CurrentFrame]]);
                        return;
                    case FairyType.Summer:
                        output.Sprite(input.Sprites.Fairy[summerWings.Frames[input.A.AnimationController.FrameLists[0].CurrentFrame]]);
                        return;
                    case FairyType.Fall:
                        output.Sprite(input.Sprites.Fairy[fallWings.Frames[input.A.AnimationController.FrameLists[0].CurrentFrame]]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Fairy[winterWings.Frames[input.A.AnimationController.FrameLists[0].CurrentFrame]]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.Breasts, 14, (input, output) =>
            {
                output.Coloring(GetSkinColor(input, input.Actor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.LeftBreastFullness > 0)
                {
                    int leftSize = (int)Math.Sqrt(input.A.GetLeftBreastSize(21 * 21, GeneralSizeMod));


                    if (leftSize == 21)
                    {
                        output.Sprite(input.Sprites.Fairy240[5]).AddOffset(-34 * .625f, -57 * .625f);
                        return;
                    }

                    if (leftSize == 20)
                    {
                        output.Sprite(input.Sprites.Fairy240[4]).AddOffset(0, -12 * .625f);
                        return;
                    }

                    if (leftSize == 19)
                    {
                        output.Sprite(input.Sprites.Fairy[146 + leftSize]).AddOffset(0, -12 * .625f);
                        return;
                    }

                    output.Sprite(input.Sprites.Fairy[146 + leftSize]);
                    return;
                }

                if (input.A.PredatorComponent?.RightBreastFullness > 0)
                {
                    output.Sprite(input.Sprites.Fairy[146]);
                    return;
                }

                if (CalcFairyParameters(input.A).Encumbered)
                {
                    output.Sprite(input.Sprites.Fairy[140 + input.U.BreastSize]);
                    return;
                }

                output.Sprite(input.Sprites.Fairy[143 + input.U.BreastSize]);
            });

            builder.RenderSingle(SpriteType.SecondaryBreasts, 14, (input, output) =>
            {
                output.Coloring(GetSkinColor(input, input.Actor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.RightBreastFullness > 0)
                {
                    int rightSize = (int)Math.Sqrt(input.A.GetRightBreastSize(21 * 21, GeneralSizeMod));

                    if (rightSize == 21)
                    {
                        output.Sprite(input.Sprites.Fairy240[10]).AddOffset(34 * .625f, -57 * .625f);
                        return;
                    }

                    if (rightSize == 20)
                    {
                        output.Sprite(input.Sprites.Fairy240[9]).AddOffset(0, -12 * .625f);
                        return;
                    }

                    if (rightSize == 19)
                    {
                        output.Sprite(input.Sprites.Fairy[166 + rightSize]).AddOffset(0, -12 * .625f);
                        return;
                    }

                    output.Sprite(input.Sprites.Fairy[166 + rightSize]);
                    return;
                }

                if (input.A.PredatorComponent?.LeftBreastFullness > 0)
                {
                    output.Sprite(input.Sprites.Fairy[166]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 13, (input, output) =>
            {
                output.Coloring(GetSkinColor(input, input.Actor));
                if (input.A.HasBelly)
                {
                    int bellySprite = input.A.GetRootedStomachSize(18, GeneralSizeMod);

                    if (bellySprite == 18)
                    {
                        output.Sprite(input.Sprites.Fairy240[0]);
                        return;
                    }

                    output.Sprite(input.Sprites.Fairy[186 + input.A.GetRootedStomachSize(18, GeneralSizeMod)]);
                }
            });

            builder.RenderSingle(SpriteType.Dick, 12, (input, output) =>
            {
                output.Coloring(GetSkinColor(input, input.Actor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.GetBallSize(19, GeneralSizeMod) > 4)
                {
                    output.Sprite(input.Sprites.Fairy[119 + input.U.DickSize]);
                    return;
                }

                if (CalcFairyParameters(input.A).Encumbered)
                {
                    output.Sprite(input.Sprites.Fairy[117 + input.U.DickSize]);
                    return;
                }

                output.Sprite(input.Sprites.Fairy[115 + input.U.DickSize]);
            });

            builder.RenderSingle(SpriteType.Balls, 11, (input, output) =>
            {
                output.Coloring(GetSkinColor(input, input.Actor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.BallsFullness > 0)
                {
                    int ballSize = input.A.GetBallSize(17, GeneralSizeMod);
                    //AddOffset(Balls, 0, -10 * .625f);

                    if (ballSize >= 16)
                    {
                        output.Sprite(input.Sprites.Fairy240[14]).AddOffset(0, -10 * .625f);
                        return;
                    }

                    if (ballSize == 15)
                    {
                        output.AddOffset(0, -24 * .625f);
                    }

                    output.Sprite(input.Sprites.Fairy[123 + ballSize]);
                    return;
                }

                if (CalcFairyParameters(input.A).Encumbered)
                {
                    output.Sprite(input.Sprites.Fairy[123 + input.U.DickSize]);
                    return;
                }

                output.Sprite(input.Sprites.Fairy[121 + input.U.DickSize]);
            });


            builder.RunBefore((input, output) => { Defaults.BasicBellyRunAfter.Invoke(input, output); });

            builder.RandomCustom((data, output) =>   
            {
                Defaults.Randomize(data, output);
                IUnitRead unit = data.Unit;

                unit.HairStyle = State.Rand.Next(7);
                FairyUtil.SetSeason(unit, (FairyType)State.Rand.Next(4));
            });
        });


        private static ColorSwapPalette GetHairColor(IRaceRenderInput input, IActorUnit actor)
        {
            switch (CalcFairyParameters(input.A).Season)
            {
                case FairyType.Spring:
                    return ColorPaletteMap.GetPalette(SwapType.FairySpringClothes, actor.Unit.HairColor);
                case FairyType.Summer:
                    return ColorPaletteMap.GetPalette(SwapType.FairySummerClothes, actor.Unit.HairColor);
                case FairyType.Fall:
                    return ColorPaletteMap.GetPalette(SwapType.FairyFallClothes, actor.Unit.HairColor);
                default:
                    return ColorPaletteMap.GetPalette(SwapType.FairyWinterClothes, actor.Unit.HairColor);
            }
        }


        private static ColorSwapPalette GetSkinColor(IRaceRenderInput input, IActorUnit actor)
        {
            switch (CalcFairyParameters(input.A).Season)
            {
                case FairyType.Spring:
                    return ColorPaletteMap.GetPalette(SwapType.FairySpringSkin, actor.Unit.SkinColor);
                case FairyType.Summer:
                    return ColorPaletteMap.GetPalette(SwapType.FairySummerSkin, actor.Unit.SkinColor);
                case FairyType.Fall:
                    return ColorPaletteMap.GetPalette(SwapType.FairyFallSkin, actor.Unit.SkinColor);
                default:
                    return ColorPaletteMap.GetPalette(SwapType.FairyWinterSkin, actor.Unit.SkinColor);
            }
        }


        private static void SetUpAnimations(IActorUnit actor)
        {
            actor.AnimationController.FrameLists = new[]
            {
                new AnimationController.FrameList(State.Rand.Next(0, 3), 0, true)
            };
        }

        private static class Nightie
        {
            internal static readonly IClothing NightieInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.DiscardSprite = input.Sprites.Fairy[20];
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing3"].Layer(18);
                    output["Clothing2"].Layer(18);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(FairyUtil.GetClothesColor(input.Actor));
                    output["Clothing2"].Coloring(FairyUtil.GetClothesColor(input.Actor));
                    output["Clothing3"].Coloring(FairyUtil.GetClothesColor(input.Actor));

                    if (input.A.HasBelly || input.A.GetLeftBreastSize(19) > 2 || input.A.GetRightBreastSize(19) > 2)
                    {
                        output["Clothing1"].Layer(5);
                        output["Clothing2"].Layer(6);
                        output["Clothing3"].Layer(6);
                    }
                    else
                    {
                        output["Clothing1"].Layer(17);
                        output["Clothing2"].Layer(18);
                        output["Clothing3"].Layer(18);
                    }

                    int mainSprite;
                    if (input.A.HasBelly == false)
                    {
                        mainSprite = 11;
                    }
                    else if (input.A.GetRootedStomachSize(19, GeneralSizeMod) == 0)
                    {
                        mainSprite = 12;
                    }
                    else
                    {
                        mainSprite = 13;
                    }

                    if (input.U.HasBreasts && (Math.Sqrt(input.A.GetLeftBreastSize(21 * 21, GeneralSizeMod)) > 3 || Math.Sqrt(input.A.GetRightBreastSize(21 * 21, GeneralSizeMod)) > 3) == false)
                    {
                        int encumMod = 0;
                        if (input.A.PredatorComponent?.Fullness > 0)
                        {
                            encumMod = 3;
                        }

                        int leftSprite = 14 + input.U.BreastSize + encumMod;
                        int rightSprite = 21 + input.U.BreastSize + encumMod;
                        output["Clothing2"].Sprite(input.Sprites.Fairy[leftSprite]);
                        output["Clothing3"].Sprite(input.Sprites.Fairy[rightSprite]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                        output["Clothing3"].Sprite(null);
                    }

                    output["Clothing1"].Sprite(input.Sprites.Fairy[mainSprite]);
                });
            });
        }

        private static class OnePiece
        {
            internal static readonly IClothing OnePieceInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.DiscardSprite = input.Sprites.Fairy[20];
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing3"].Layer(18);
                    output["Clothing2"].Layer(18);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(FairyUtil.GetClothesColor(input.Actor));
                    output["Clothing2"].Coloring(FairyUtil.GetClothesColor(input.Actor));
                    output["Clothing3"].Coloring(FairyUtil.GetClothesColor(input.Actor));
                    if (input.A.HasBelly || input.A.HasPreyInBreasts)
                    {
                        output["Clothing1"].Layer(5);
                        output["Clothing2"].Layer(6);
                        output["Clothing3"].Layer(6);
                    }
                    else
                    {
                        output["Clothing1"].Layer(17);
                        output["Clothing2"].Layer(18);
                        output["Clothing3"].Layer(18);
                    }

                    int mainSprite;
                    if (input.A.HasBelly == false)
                    {
                        mainSprite = 27;
                    }
                    else if (input.A.GetRootedStomachSize(19, GeneralSizeMod) == 0)
                    {
                        mainSprite = 28;
                    }
                    else
                    {
                        mainSprite = 29;
                    }

                    if (input.U.HasBreasts && (Math.Sqrt(input.A.GetLeftBreastSize(21 * 21, GeneralSizeMod)) > 3 || Math.Sqrt(input.A.GetRightBreastSize(21 * 21, GeneralSizeMod)) > 3) == false)
                    {
                        int encumMod = 0;
                        if (input.A.PredatorComponent?.Fullness > 0)
                        {
                            encumMod = 3;
                        }

                        int leftSprite = 30 + input.U.BreastSize + encumMod;
                        int rightSprite = 37 + input.U.BreastSize + encumMod;
                        if (input.A.PredatorComponent?.LeftBreastFullness > 0)
                        {
                            leftSprite = 36;
                        }

                        if (input.A.PredatorComponent?.RightBreastFullness > 0)
                        {
                            rightSprite = 43;
                        }

                        output["Clothing2"].Sprite(input.Sprites.Fairy[leftSprite]);
                        output["Clothing3"].Sprite(input.Sprites.Fairy[rightSprite]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                        output["Clothing3"].Sprite(null);
                    }

                    output["Clothing1"].Sprite(input.Sprites.Fairy[mainSprite]);
                });
            });
        }

        private static class TwoPiece
        {
            internal static readonly IClothing TwoPieceInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.DiscardSprite = input.Sprites.Fairy[20];
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing3"].Layer(18);
                    output["Clothing2"].Layer(18);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(FairyUtil.GetClothesColor(input.Actor));
                    output["Clothing2"].Coloring(FairyUtil.GetClothesColor(input.Actor));
                    output["Clothing3"].Coloring(FairyUtil.GetClothesColor(input.Actor));
                    if (input.A.HasBelly || input.A.HasPreyInBreasts)
                    {
                        output["Clothing1"].Layer(5);
                        output["Clothing2"].Layer(6);
                        output["Clothing3"].Layer(6);
                    }
                    else
                    {
                        output["Clothing1"].Layer(17);
                        output["Clothing2"].Layer(18);
                        output["Clothing3"].Layer(18);
                    }

                    if (input.A.HasBelly == false)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Fairy[44]);
                    }

                    if (input.U.HasBreasts && (Math.Sqrt(input.A.GetLeftBreastSize(21 * 21, GeneralSizeMod)) > 3 || Math.Sqrt(input.A.GetRightBreastSize(21 * 21, GeneralSizeMod)) > 3) == false)
                    {
                        int encumMod = 0;
                        if (input.A.PredatorComponent?.Fullness > 0)
                        {
                            encumMod = 3;
                        }

                        int leftSprite = 45 + input.U.BreastSize + encumMod;
                        int rightSprite = 52 + input.U.BreastSize + encumMod;
                        if (input.A.PredatorComponent?.LeftBreastFullness > 0 || input.A.PredatorComponent?.RightBreastFullness > 0)
                        {
                            leftSprite = 51;
                            rightSprite = 0;
                        }

                        output["Clothing2"].Sprite(input.Sprites.Fairy[leftSprite]);
                        if (rightSprite != 0)
                        {
                            output["Clothing3"].Sprite(input.Sprites.Fairy[rightSprite]);
                        }
                    }
                });
            });
        }

        private static class Dress
        {
            internal static readonly IClothing DressInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.DiscardSprite = input.Sprites.Fairy[20];
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(18);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(FairyUtil.GetClothesColor(input.Actor));
                    output["Clothing2"].Coloring(FairyUtil.GetClothesColor(input.Actor));
                    output["Clothing3"].Coloring(FairyUtil.GetClothesColor(input.Actor));
                    if (input.A.HasBelly || input.A.HasPreyInBreasts)
                    {
                        output["Clothing1"].Layer(5);
                        output["Clothing2"].Layer(6);
                    }
                    else
                    {
                        output["Clothing1"].Layer(17);
                        output["Clothing2"].Layer(18);
                    }

                    bool oversize = input.A.GetLeftBreastSize(19) > 2 || input.A.GetRightBreastSize(19) > 2 || input.A.GetRootedStomachSize(19) > 2;


                    if (oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Fairy[62]);
                    }
                    else if (input.A.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Fairy[60 + Math.Min(input.A.GetRootedStomachSize(19, GeneralSizeMod), 1)]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Fairy[59]);
                    }

                    if (input.U.HasBreasts && (Math.Sqrt(input.A.GetLeftBreastSize(21 * 21, GeneralSizeMod)) > 3 || Math.Sqrt(input.A.GetRightBreastSize(21 * 21, GeneralSizeMod)) > 3) == false)
                    {
                        int encumMod = 0;
                        if (input.A.PredatorComponent?.Fullness > 0)
                        {
                            encumMod = 3;
                        }

                        output["Clothing2"].Sprite(input.Sprites.Fairy[65 + input.U.BreastSize + encumMod]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }
                });
            });
        }

        private static class Loincloth
        {
            internal static readonly IClothing LoinclothInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.DiscardSprite = input.Sprites.Fairy[20];
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(18);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(FairyUtil.GetClothesColor(input.Actor));
                    output["Clothing2"].Coloring(FairyUtil.GetClothesColor(input.Actor));

                    if (input.A.HasBelly || input.A.HasPreyInBreasts)
                    {
                        output["Clothing1"].Layer(5);
                        output["Clothing2"].Layer(6);
                    }
                    else
                    {
                        output["Clothing1"].Layer(17);
                        output["Clothing2"].Layer(18);
                    }

                    output["Clothing1"].Sprite(input.Sprites.Fairy[63]);

                    if (input.U.HasBreasts && (Math.Sqrt(input.A.GetLeftBreastSize(21 * 21, GeneralSizeMod)) > 3 || Math.Sqrt(input.A.GetRightBreastSize(21 * 21, GeneralSizeMod)) > 3) == false)
                    {
                        int encumMod = 0;
                        if (input.A.PredatorComponent?.Fullness > 0)
                        {
                            encumMod = 3;
                        }

                        output["Clothing2"].Sprite(input.Sprites.Fairy[65 + input.U.BreastSize + encumMod]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }
                });
            });
        }

        private static class Sleeves
        {
            internal static readonly IClothing SleevesInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Coloring(FairyUtil.GetClothesColor(input.Actor));

                    output["Clothing1"].Layer(10);
                    if (input.A.PredatorComponent?.Fullness > 0)
                    {
                        if (input.A.IsAttacking)
                        {
                            output["Clothing1"].Layer(18);
                            output["Clothing1"].Sprite(input.Sprites.Fairy[74]);
                        }

                        output["Clothing1"].Sprite(input.Sprites.Fairy[72]);
                    }
                    else
                    {
                        if (input.A.IsAttacking)
                        {
                            output["Clothing1"].Layer(18);
                            output["Clothing1"].Sprite(input.Sprites.Fairy[73]);
                        }

                        output["Clothing1"].Sprite(input.Sprites.Fairy[71]);
                    }
                });
            });
        }

        private static class Bracelets
        {
            internal static readonly IClothing BraceletsInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Layer(10);
                    if (input.A.PredatorComponent?.Fullness > 0)
                    {
                        if (input.A.IsAttacking)
                        {
                            output["Clothing1"].Layer(18);
                            output["Clothing1"].Sprite(input.Sprites.Fairy[77]);
                        }

                        output["Clothing1"].Sprite(input.Sprites.Fairy[76]);
                    }
                    else
                    {
                        if (input.A.IsAttacking)
                        {
                            output["Clothing1"].Layer(18);
                            output["Clothing1"].Sprite(input.Sprites.Fairy[77]);
                        }

                        output["Clothing1"].Sprite(input.Sprites.Fairy[75]);
                    }
                });
            });
        }

        private static class Leggings
        {
            internal static readonly IClothing LeggingsInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(8);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Coloring(FairyUtil.GetClothesColor(input.Actor));

                    output["Clothing1"].Sprite(input.A.PredatorComponent?.Fullness > 0 ? input.Sprites.Fairy[79] : input.Sprites.Fairy[78]);
                });
            });
        }

        private static class Sandals
        {
            internal static readonly IClothing SandalsInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(8);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Sprite(input.A.PredatorComponent?.Fullness > 0 ? input.Sprites.Fairy[81] : input.Sprites.Fairy[80]);
                });
            });
        }
    }
}