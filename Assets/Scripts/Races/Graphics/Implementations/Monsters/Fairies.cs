#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

internal enum FairyType
{
    Spring,
    Summer,
    Fall,
    Winter
}

internal static class FairyUtil
{
    internal static void SetSeason(Unit unit, FairyType season)
    {
        unit.BodyAccentType1 = (int)season;
        switch (season)
        {
            case FairyType.Spring:
                unit.InnateSpells = new List<SpellTypes> { SpellTypes.Speed };
                break;
            case FairyType.Summer:
                unit.InnateSpells = new List<SpellTypes> { SpellTypes.Valor };
                break;
            case FairyType.Fall:
                unit.InnateSpells = new List<SpellTypes> { SpellTypes.Predation };
                break;
            case FairyType.Winter:
                unit.InnateSpells = new List<SpellTypes> { SpellTypes.Shield };
                break;
        }
    }

    internal static FairyType GetSeason(Unit unit)
    {
        return (FairyType)unit.BodyAccentType1;
    }

    internal static ColorSwapPalette GetClothesColor(Actor_Unit actor)
    {
        switch (GetSeason(actor.Unit))
        {
            case FairyType.Spring:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairySpringClothes, actor.Unit.ClothingColor);
            case FairyType.Summer:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairySummerClothes, actor.Unit.ClothingColor);
            case FairyType.Fall:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairyFallClothes, actor.Unit.ClothingColor);
            default:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairyWinterClothes, actor.Unit.ClothingColor);
        }
    }
}

internal static class Fairies
{
    internal const float GeneralSizeMod = 0.8f;


    internal class FairyParameters : IParameters
    {
        internal bool Encumbered;
        internal bool VeryEncumbered;
        internal FairyType Season;
    }

    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank<FairyParameters>, builder =>
    {
        RaceFrameList springWings = new RaceFrameList(new int[3] { 91, 92, 93 }, new float[3] { .2f, .2f, .2f });
        RaceFrameList summerWings = new RaceFrameList(new int[3] { 94, 95, 96 }, new float[3] { .2f, .2f, .2f });
        RaceFrameList fallWings = new RaceFrameList(new int[3] { 97, 98, 99 }, new float[3] { .2f, .2f, .2f });
        RaceFrameList winterWings = new RaceFrameList(new int[3] { 100, 101, 102 }, new float[3] { .2f, .2f, .2f });
        RaceFrameList springWingsEnc = new RaceFrameList(new int[3] { 103, 104, 105 }, new float[3] { .2f, .2f, .2f });
        RaceFrameList summerWingsEnc = new RaceFrameList(new int[3] { 106, 107, 108 }, new float[3] { .2f, .2f, .2f });
        RaceFrameList fallWingsEnc = new RaceFrameList(new int[3] { 109, 110, 111 }, new float[3] { .2f, .2f, .2f });
        RaceFrameList winterWingsEnc = new RaceFrameList(new int[3] { 112, 113, 114 }, new float[3] { .2f, .2f, .2f });

        builder.Setup(output =>
        {
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
            if (input.Actor.Unit.HairStyle < 3)
            {
                output.Sprite(input.Sprites.Fairy[2 * input.Actor.Unit.HairStyle]);
                return;
            }

            if (input.Actor.Unit.HairStyle > 3)
            {
                output.Sprite(input.Sprites.FairyExtraHair[Math.Min(input.Actor.Unit.HairStyle - 4, 3)]);
                return;
            }

            switch (input.Params.Season)
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
            if (input.Actor.Unit.HairStyle < 3)
            {
                output.Sprite(input.Sprites.Fairy[1 + 2 * input.Actor.Unit.HairStyle]);
                return;
            }

            if (input.Actor.Unit.HairStyle > 3)
            {
                return;
            }

            if (input.Params.Season == FairyType.Summer)
            {
                output.Sprite(input.Sprites.Fairy[9]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(GetSkinColor(input, input.Actor));
            if (input.Params.VeryEncumbered)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Fairy[205]);
                    return;
                }

                output.Sprite(input.Sprites.Fairy[204]);
                return;
            }

            if (input.Params.Encumbered)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Fairy[87]);
                    return;
                }

                output.Sprite(input.Sprites.Fairy[84]);
                return;
            }

            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Fairy[85]);
                return;
            }

            output.Sprite(input.Sprites.Fairy[82]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 16, (input, output) =>
        {
            output.Coloring(GetSkinColor(input, input.Actor));
            if (input.Params.VeryEncumbered)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Fairy[206]);
                    return;
                }

                return;
            }

            if (input.Params.Encumbered)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Fairy[88]);
                }
            }
            else
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Fairy[86]);
                }
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent2, 4, (input, output) =>
        {
            output.Coloring(GetSkinColor(input, input.Actor));
            if (input.Params.VeryEncumbered && input.Actor.IsEating)
            {
                if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false)
                {
                    output.Sprite(input.Sprites.Fairy[207]);
                    return;
                }
            }

            if (input.Params.Encumbered)
            {
                output.Sprite(input.Sprites.Fairy[83]);
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent3, 0, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            if (input.Actor.AnimationController.frameLists[0].currentTime >= springWings.Times[input.Actor.AnimationController.frameLists[0].currentFrame] && input.Actor.Unit.IsDead == false)
            {
                input.Actor.AnimationController.frameLists[0].currentFrame++;
                input.Actor.AnimationController.frameLists[0].currentTime = 0f;

                if (input.Actor.AnimationController.frameLists[0].currentFrame >= springWings.Frames.Length)
                {
                    input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                    input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                }
            }

            if (input.Params.Encumbered)
            {
                switch (input.Params.Season)
                {
                    case FairyType.Spring:
                        output.Sprite(input.Sprites.Fairy[springWingsEnc.Frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                        return;
                    case FairyType.Summer:
                        output.Sprite(input.Sprites.Fairy[summerWingsEnc.Frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                        return;
                    case FairyType.Fall:
                        output.Sprite(input.Sprites.Fairy[fallWingsEnc.Frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Fairy[winterWingsEnc.Frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                        return;
                }
            }

            switch (input.Params.Season)
            {
                case FairyType.Spring:
                    output.Sprite(input.Sprites.Fairy[springWings.Frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                    return;
                case FairyType.Summer:
                    output.Sprite(input.Sprites.Fairy[summerWings.Frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                    return;
                case FairyType.Fall:
                    output.Sprite(input.Sprites.Fairy[fallWings.Frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                    return;
                default:
                    output.Sprite(input.Sprites.Fairy[winterWings.Frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.Breasts, 14, (input, output) =>
        {
            output.Coloring(GetSkinColor(input, input.Actor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.GetLeftBreastSize(21 * 21, GeneralSizeMod));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast))
                {
                    output.Sprite(input.Sprites.Fairy240[8]).AddOffset(-34 * .625f, -57 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize == 21)
                {
                    output.Sprite(input.Sprites.Fairy240[7]).AddOffset(-34 * .625f, -57 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize > 20)
                {
                    output.Sprite(input.Sprites.Fairy240[6]).AddOffset(-34 * .625f, -57 * .625f);
                    return;
                }


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

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                output.Sprite(input.Sprites.Fairy[146]);
                return;
            }

            if (input.Params.Encumbered)
            {
                output.Sprite(input.Sprites.Fairy[140 + input.Actor.Unit.BreastSize]);
                return;
            }

            output.Sprite(input.Sprites.Fairy[143 + input.Actor.Unit.BreastSize]);
        });

        builder.RenderSingle(SpriteType.SecondaryBreasts, 14, (input, output) =>
        {
            output.Coloring(GetSkinColor(input, input.Actor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.GetRightBreastSize(21 * 21, GeneralSizeMod));
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast))
                {
                    output.Sprite(input.Sprites.Fairy240[13]).AddOffset(34 * .625f, -57 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize == 21)
                {
                    output.Sprite(input.Sprites.Fairy240[12]).AddOffset(34 * .625f, -57 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize > 20)
                {
                    output.Sprite(input.Sprites.Fairy240[11]).AddOffset(34 * .625f, -57 * .625f);
                    return;
                }

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

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                output.Sprite(input.Sprites.Fairy[166]);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 13, (input, output) =>
        {
            output.Coloring(GetSkinColor(input, input.Actor));
            if (input.Actor.HasBelly)
            {
                int bellySprite = input.Actor.GetRootedStomachSize(18, GeneralSizeMod);

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                {
                    output.Sprite(input.Sprites.Fairy240[3]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && bellySprite == 18)
                {
                    output.Sprite(input.Sprites.Fairy240[2]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && bellySprite > 17)
                {
                    output.Sprite(input.Sprites.Fairy240[1]);
                    return;
                }

                if (bellySprite == 18)
                {
                    output.Sprite(input.Sprites.Fairy240[0]);
                    return;
                }

                output.Sprite(input.Sprites.Fairy[186 + input.Actor.GetRootedStomachSize(18, GeneralSizeMod)]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 12, (input, output) =>
        {
            output.Coloring(GetSkinColor(input, input.Actor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.GetBallSize(19, GeneralSizeMod) > 4)
            {
                output.Sprite(input.Sprites.Fairy[119 + input.Actor.Unit.DickSize]);
                return;
            }

            if (input.Params.Encumbered)
            {
                output.Sprite(input.Sprites.Fairy[117 + input.Actor.Unit.DickSize]);
                return;
            }

            output.Sprite(input.Sprites.Fairy[115 + input.Actor.Unit.DickSize]);
        });

        builder.RenderSingle(SpriteType.Balls, 11, (input, output) =>
        {
            output.Coloring(GetSkinColor(input, input.Actor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                int ballSize = input.Actor.GetBallSize(17, GeneralSizeMod);
                //AddOffset(Balls, 0, -10 * .625f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                {
                    output.Sprite(input.Sprites.Fairy240[17]).AddOffset(0, -10 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) && ballSize == 17)
                {
                    output.Sprite(input.Sprites.Fairy240[16]).AddOffset(0, -10 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) && ballSize == 16)
                {
                    output.Sprite(input.Sprites.Fairy240[15]).AddOffset(0, -10 * .625f);
                    return;
                }

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

            if (input.Params.Encumbered)
            {
                output.Sprite(input.Sprites.Fairy[123 + input.Actor.Unit.DickSize]);
                return;
            }

            output.Sprite(input.Sprites.Fairy[121 + input.Actor.Unit.DickSize]);
        });


        builder.RunBefore((input, output) =>
        {
            output.Params.Season = (FairyType)input.Actor.Unit.BodyAccentType1; // TODO fix dirty enum casting
            output.Params.Encumbered = input.Actor.PredatorComponent?.Fullness > 0; // Not 100% accurate, but saves effort
            output.Params.VeryEncumbered = input.Actor.GetRootedStomachSize(19, GeneralSizeMod) > 16;
            //base.RunFirst(data.Actor);
            Defaults.BasicBellyRunAfter.Invoke(input, output);
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.HairStyle = State.Rand.Next(7);
            FairyUtil.SetSeason(unit, (FairyType)State.Rand.Next(4));
        });
    });


    private static ColorSwapPalette GetHairColor(IRaceRenderInput<FairyParameters> input, Actor_Unit actor)
    {
        switch (input.Params.Season)
        {
            case FairyType.Spring:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairySpringClothes, actor.Unit.HairColor);
            case FairyType.Summer:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairySummerClothes, actor.Unit.HairColor);
            case FairyType.Fall:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairyFallClothes, actor.Unit.HairColor);
            default:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairyWinterClothes, actor.Unit.HairColor);
        }
    }


    private static ColorSwapPalette GetSkinColor(IRaceRenderInput<FairyParameters> input, Actor_Unit actor)
    {
        switch (input.Params.Season)
        {
            case FairyType.Spring:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairySpringSkin, actor.Unit.SkinColor);
            case FairyType.Summer:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairySummerSkin, actor.Unit.SkinColor);
            case FairyType.Fall:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairyFallSkin, actor.Unit.SkinColor);
            default:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairyWinterSkin, actor.Unit.SkinColor);
        }
    }


    private static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
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

                if (input.Actor.HasBelly || input.Actor.GetLeftBreastSize(19) > 2 || input.Actor.GetRightBreastSize(19) > 2)
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
                if (input.Actor.HasBelly == false)
                {
                    mainSprite = 11;
                }
                else if (input.Actor.GetRootedStomachSize(19, GeneralSizeMod) == 0)
                {
                    mainSprite = 12;
                }
                else
                {
                    mainSprite = 13;
                }

                if (input.Actor.Unit.HasBreasts && (Math.Sqrt(input.Actor.GetLeftBreastSize(21 * 21, GeneralSizeMod)) > 3 || Math.Sqrt(input.Actor.GetRightBreastSize(21 * 21, GeneralSizeMod)) > 3) == false)
                {
                    int encumMod = 0;
                    if (input.Actor.PredatorComponent?.Fullness > 0)
                    {
                        encumMod = 3;
                    }

                    int leftSprite = 14 + input.Actor.Unit.BreastSize + encumMod;
                    int rightSprite = 21 + input.Actor.Unit.BreastSize + encumMod;
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
                if (input.Actor.HasBelly || input.Actor.HasPreyInBreasts)
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
                if (input.Actor.HasBelly == false)
                {
                    mainSprite = 27;
                }
                else if (input.Actor.GetRootedStomachSize(19, GeneralSizeMod) == 0)
                {
                    mainSprite = 28;
                }
                else
                {
                    mainSprite = 29;
                }

                if (input.Actor.Unit.HasBreasts && (Math.Sqrt(input.Actor.GetLeftBreastSize(21 * 21, GeneralSizeMod)) > 3 || Math.Sqrt(input.Actor.GetRightBreastSize(21 * 21, GeneralSizeMod)) > 3) == false)
                {
                    int encumMod = 0;
                    if (input.Actor.PredatorComponent?.Fullness > 0)
                    {
                        encumMod = 3;
                    }

                    int leftSprite = 30 + input.Actor.Unit.BreastSize + encumMod;
                    int rightSprite = 37 + input.Actor.Unit.BreastSize + encumMod;
                    if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
                    {
                        leftSprite = 36;
                    }

                    if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
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
                if (input.Actor.HasBelly || input.Actor.HasPreyInBreasts)
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

                if (input.Actor.HasBelly == false)
                {
                    output["Clothing1"].Sprite(input.Sprites.Fairy[44]);
                }

                if (input.Actor.Unit.HasBreasts && (Math.Sqrt(input.Actor.GetLeftBreastSize(21 * 21, GeneralSizeMod)) > 3 || Math.Sqrt(input.Actor.GetRightBreastSize(21 * 21, GeneralSizeMod)) > 3) == false)
                {
                    int encumMod = 0;
                    if (input.Actor.PredatorComponent?.Fullness > 0)
                    {
                        encumMod = 3;
                    }

                    int leftSprite = 45 + input.Actor.Unit.BreastSize + encumMod;
                    int rightSprite = 52 + input.Actor.Unit.BreastSize + encumMod;
                    if (input.Actor.PredatorComponent?.LeftBreastFullness > 0 || input.Actor.PredatorComponent?.RightBreastFullness > 0)
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
                if (input.Actor.HasBelly || input.Actor.HasPreyInBreasts)
                {
                    output["Clothing1"].Layer(5);
                    output["Clothing2"].Layer(6);
                }
                else
                {
                    output["Clothing1"].Layer(17);
                    output["Clothing2"].Layer(18);
                }

                bool oversize = input.Actor.GetLeftBreastSize(19) > 2 || input.Actor.GetRightBreastSize(19) > 2 || input.Actor.GetRootedStomachSize(19) > 2;


                if (oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Fairy[62]);
                }
                else if (input.Actor.HasBelly)
                {
                    output["Clothing1"].Sprite(input.Sprites.Fairy[60 + Math.Min(input.Actor.GetRootedStomachSize(19, GeneralSizeMod), 1)]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Fairy[59]);
                }

                if (input.Actor.Unit.HasBreasts && (Math.Sqrt(input.Actor.GetLeftBreastSize(21 * 21, GeneralSizeMod)) > 3 || Math.Sqrt(input.Actor.GetRightBreastSize(21 * 21, GeneralSizeMod)) > 3) == false)
                {
                    int encumMod = 0;
                    if (input.Actor.PredatorComponent?.Fullness > 0)
                    {
                        encumMod = 3;
                    }

                    output["Clothing2"].Sprite(input.Sprites.Fairy[65 + input.Actor.Unit.BreastSize + encumMod]);
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

                if (input.Actor.HasBelly || input.Actor.HasPreyInBreasts)
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

                if (input.Actor.Unit.HasBreasts && (Math.Sqrt(input.Actor.GetLeftBreastSize(21 * 21, GeneralSizeMod)) > 3 || Math.Sqrt(input.Actor.GetRightBreastSize(21 * 21, GeneralSizeMod)) > 3) == false)
                {
                    int encumMod = 0;
                    if (input.Actor.PredatorComponent?.Fullness > 0)
                    {
                        encumMod = 3;
                    }

                    output["Clothing2"].Sprite(input.Sprites.Fairy[65 + input.Actor.Unit.BreastSize + encumMod]);
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
                if (input.Actor.PredatorComponent?.Fullness > 0)
                {
                    if (input.Actor.IsAttacking)
                    {
                        output["Clothing1"].Layer(18);
                        output["Clothing1"].Sprite(input.Sprites.Fairy[74]);
                    }

                    output["Clothing1"].Sprite(input.Sprites.Fairy[72]);
                }
                else
                {
                    if (input.Actor.IsAttacking)
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
                if (input.Actor.PredatorComponent?.Fullness > 0)
                {
                    if (input.Actor.IsAttacking)
                    {
                        output["Clothing1"].Layer(18);
                        output["Clothing1"].Sprite(input.Sprites.Fairy[77]);
                    }

                    output["Clothing1"].Sprite(input.Sprites.Fairy[76]);
                }
                else
                {
                    if (input.Actor.IsAttacking)
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

                output["Clothing1"].Sprite(input.Actor.PredatorComponent?.Fullness > 0 ? input.Sprites.Fairy[79] : input.Sprites.Fairy[78]);
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
                output["Clothing1"].Sprite(input.Actor.PredatorComponent?.Fullness > 0 ? input.Sprites.Fairy[81] : input.Sprites.Fairy[80]);
            });
        });
    }
}