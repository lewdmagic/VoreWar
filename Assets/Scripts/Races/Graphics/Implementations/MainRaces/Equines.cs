#region

using System;
using UnityEngine;

#endregion

internal static class Equines
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Default<OverSizeParameters>, builder =>
    {
        builder.Setup(output =>
        {
            output.DickSizes = () => 3;
            output.BreastSizes = () => 7;

            output.SpecialAccessoryCount = 0;
            output.ClothingShift = new Vector3(0, 0, 0);
            output.AvoidedEyeTypes = 0;
            output.AvoidedMouthTypes = 0;

            output.HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UniversalHair);
            output.HairStyles = 15;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.HorseSkin);
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.HorseSkin);
            output.EyeTypes = 4;
            output.EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);
            output.SecondaryEyeColors = 1;
            output.BodySizes = 0;
            output.AllowedMainClothingTypes.Clear();
            output.AllowedWaistTypes.Clear();
            output.AllowedClothingHatTypes.Clear();
            output.MouthTypes = 0;
            output.AvoidedMainClothingTypes = 0;
            output.TailTypes = 6;
            output.BodyAccentTypes3 = 5;
            output.BodyAccentTypes4 = 5;
            output.BodyAccentTypes5 = 2;


            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing50Spaced);

            output.ExtendedBreastSprites = true;

            output.AllowedMainClothingTypes.Set( //undertops
                HorseUndertop1.HorseUndertop1Instance,
                HorseUndertop2.HorseUndertop2Instance,
                HorseUndertop3.HorseUndertop3Instance,
                HorseUndertop4.HorseUndertop4Instance,
                HorseUndertopM1.HorseUndertopM1Instance,
                HorseUndertopM2.HorseUndertopM2Instance,
                HorseUndertopM3.HorseUndertopM3Instance
            );

            output.AllowedWaistTypes.Set( //underbottoms
                HorseUBottom.HorseUBottom1,
                HorseUBottom.HorseUBottom2,
                HorseUBottom.HorseUBottom3,
                HorseUBottom.HorseUBottom4,
                HorseUBottom.HorseUBottom5
            );

            output.ExtraMainClothing1Types.Set( //Overtops
                HorsePoncho.HorsePonchoInstance,
                HorseNecklace.HorseNecklaceInstance
            );

            output.ExtraMainClothing2Types.Set( //Overbottoms
                HorseOBottom1.HorseOBottom1Instance,
                HorseOBottom2.HorseOBottom2Instance,
                HorseOBottom3.HorseOBottom3Instance
            );

            output.WholeBodyOffset = new Vector2(0, 16 * .625f);
        });


        builder.RunBefore(CommonRaceCode.MakeBreastOversize(29 * 29));

        ColorSwapPalette LegTuft(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType3 >= 2)
            {
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, actor.Unit.AccessoryColor);
            }

            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, actor.Unit.SkinColor);
        }

        ColorSwapPalette SpottedBelly(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType5 == 1)
            {
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, actor.Unit.AccessoryColor);
            }

            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, actor.Unit.SkinColor);
        }

        ColorSwapPalette TailBit(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType3 == 5)
            {
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, actor.Unit.SkinColor);
            }

            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
        }

        builder.RenderSingle(SpriteType.Head, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, input.Actor.Unit.SkinColor));
            int attacking = input.Actor.IsAttacking ? 1 : 0;
            int eating = input.Actor.IsEating ? 1 : 0;
            output.Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Horse[13 + attacking + eating] : input.Sprites.Horse[3 + attacking + eating]);
        }); //head

        builder.RenderSingle(SpriteType.Eyes, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            if (input.Actor.Unit.HasBreasts)
            {
                if (input.Actor.Unit.IsDead && input.Actor.Unit.Items != null)
                {
                    output.Sprite(input.Sprites.Horse[9]);
                }
                else
                {
                    output.Sprite(input.Sprites.Horse[5 + input.Actor.Unit.EyeType]);
                }
            }
            else
            {
                if (input.Actor.Unit.IsDead && input.Actor.Unit.Items != null)
                {
                    output.Sprite(input.Sprites.Horse[19]);
                }
                else
                {
                    output.Sprite(input.Sprites.Horse[15 + input.Actor.Unit.EyeType]);
                }
            }
        }); //eyes;

        builder.RenderSingle(SpriteType.Hair, 21, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Horse[150 + 2 * input.Actor.Unit.HairStyle]);
        }); //forward hair;

        builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Horse[151 + 2 * input.Actor.Unit.HairStyle]);
        }); //back hair

        builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, input.Actor.Unit.SkinColor));
            int Hasweapon = input.Actor.Unit.HasWeapon ? 1 : 0;
            if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Actor.IsAttacking ? input.Sprites.Horse[12] : input.Sprites.Horse[10 + Hasweapon]);
            }
            else
            {
                output.Sprite(input.Actor.IsAttacking ? input.Sprites.Horse[2] : input.Sprites.Horse[0 + Hasweapon]);
            }
        }); //body

        builder.RenderSingle(SpriteType.BodyAccent3, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType3 == 0)
            {
            }
            else
            {
                int Hasweapon = input.Actor.Unit.HasWeapon ? 1 : 0;
                if (input.Actor.Unit.HasBreasts == false)
                {
                    output.Sprite(input.Actor.IsAttacking ? input.Sprites.Horse[214 + 6 * input.Actor.Unit.BodyAccentType3] : input.Sprites.Horse[212 + 6 * input.Actor.Unit.BodyAccentType3 + Hasweapon]);
                }
                else
                {
                    output.Sprite(input.Actor.IsAttacking ? input.Sprites.Horse[217 + 6 * input.Actor.Unit.BodyAccentType3] : input.Sprites.Horse[215 + 6 * input.Actor.Unit.BodyAccentType3 + Hasweapon]);
                }
            }
        }); //limb spots

        builder.RenderSingle(SpriteType.BodyAccent4, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType4 == 0)
            {
            }
            else
            {
                if (input.Actor.Unit.HasBreasts)
                {
                    int attack = input.Actor.IsAttacking ? 1 : 0;
                    int eat = input.Actor.IsEating ? 1 : 0;
                    output.Sprite(input.Sprites.Horse[200 + 4 * input.Actor.Unit.BodyAccentType4 + attack + eat]);
                }
                else
                {
                    int attack = input.Actor.IsAttacking ? 1 : 0;
                    int eat = input.Actor.IsEating ? 1 : 0;
                    output.Sprite(input.Sprites.Horse[198 + 4 * input.Actor.Unit.BodyAccentType4 + attack + eat]);
                }
            }
        }); //head spots

        builder.RenderSingle(SpriteType.BodyAccent5, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType5 == 0)
            {
            }
            else
            {
                output.Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Horse[201] : input.Sprites.Horse[200]);
            }
        }); //belly spots, also color breasts/belly/dick

        builder.RenderSingle(SpriteType.BodyAccent8, 6, (input, output) =>
        {
            output.Coloring(LegTuft(input.Actor));
            output.Sprite(input.Sprites.Horse[22]);
        }); //leg tuft

        builder.RenderSingle(SpriteType.BodyAccent9, 3, (input, output) =>
        {
            output.Coloring(TailBit(input.Actor));
            if (input.Actor.Unit.TailType <= 3)
            {
            }
            else if (input.Actor.Unit.TailType == 4)
            {
                output.Sprite(input.Sprites.Horse[185]);
            }
            else if (input.Actor.Unit.TailType == 5)
            {
                output.Sprite(input.Sprites.Horse[187]);
            }
            else
            {
                output.Sprite(input.Sprites.Horse[189]);
            }
        }); //tail bit

        builder.RenderSingle(SpriteType.BodyAccent10, 5, (input, output) => { output.Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Horse[21] : input.Sprites.Horse[20]); }); //leg hoof;

        builder.RenderSingle(SpriteType.BodyAccessory, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.TailType <= 3)
            {
                output.Sprite(input.Sprites.Horse[180 + input.Actor.Unit.TailType]);
            }
            else if (input.Actor.Unit.TailType == 4)
            {
                output.Sprite(input.Sprites.Horse[184]);
            }
            else if (input.Actor.Unit.TailType == 5)
            {
                output.Sprite(input.Sprites.Horse[186]);
            }
            else
            {
                output.Sprite(input.Sprites.Horse[188]);
            }
        }); //tail

        builder.RenderSingle(SpriteType.SecondaryAccessory, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                int weaponSprite2 = input.Actor.GetWeaponSprite();
                switch (weaponSprite2)
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
                        return;
                    case 5:
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Horse[198]);
                        return;
                    case 7:
                        return;
                }
            }
        }); //bow bit

        builder.RenderSingle(SpriteType.Breasts, 19, (input, output) =>
        {
            output.Coloring(SpottedBelly(input.Actor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(29 * 29));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 29)
                {
                    output.Sprite(input.Sprites.Horse[119]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 29)
                {
                    output.Sprite(input.Sprites.Horse[118]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 27)
                {
                    output.Sprite(input.Sprites.Horse[117]);
                    return;
                }


                if (leftSize > 26)
                {
                    leftSize = 26;
                }


                output.Sprite(input.Sprites.Horse[90 + leftSize]);
            }
            else
            {
                if (input.Actor.Unit.DefaultBreastSize == 0)
                {
                    output.Sprite(input.Sprites.Horse[90]);
                    return;
                }

                output.Sprite(input.Sprites.Horse[90 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.SecondaryBreasts, 19, (input, output) =>
        {
            output.Coloring(SpottedBelly(input.Actor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(29 * 29));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 29)
                {
                    output.Sprite(input.Sprites.Horse[149]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 29)
                {
                    output.Sprite(input.Sprites.Horse[148]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 27)
                {
                    output.Sprite(input.Sprites.Horse[147]);
                    return;
                }


                if (rightSize > 26)
                {
                    rightSize = 26;
                }


                output.Sprite(input.Sprites.Horse[120 + rightSize]);
            }
            else
            {
                if (input.Actor.Unit.DefaultBreastSize == 0)
                {
                    output.Sprite(input.Sprites.Horse[120]);
                    return;
                }

                output.Sprite(input.Sprites.Horse[120 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 17, (input, output) =>
        {
            output.Coloring(SpottedBelly(input.Actor));
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(29, 1.2f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.Horse[89]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.Horse[88]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size >= 27)
                {
                    output.Sprite(input.Sprites.Horse[87]);
                    return;
                }

                int combined = Math.Min(size, 26);
                output.Sprite(input.Sprites.Horse[60 + combined]);
            }
        }); //belly

        builder.RenderSingle(SpriteType.Dick, 14, (input, output) =>
        {
            output.Coloring(SpottedBelly(input.Actor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.VisibleFullness < .26f && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(29 * 29)) < 16 && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(29 * 29)) < 16)
            {
                output.Layer(24);
                if (input.Actor.IsCockVoring)
                {
                    output.Sprite(input.Sprites.Horse[25 + 2 * input.Actor.Unit.DickSize]);
                }
                else
                {
                    if (Config.FurryGenitals)
                    {
                        output.Sprite(input.Actor.IsErect() ? input.Sprites.Horse[25 + 2 * input.Actor.Unit.DickSize] : input.Sprites.Horse[23]);
                    }
                    else
                    {
                        output.Sprite(input.Actor.IsErect() ? input.Sprites.Horse[25 + 2 * input.Actor.Unit.DickSize] : input.Sprites.Horse[24 + 2 * input.Actor.Unit.DickSize]);
                    }
                }
            }
            else
            {
                output.Layer(14);
                if (input.Actor.IsCockVoring)
                {
                    output.Sprite(input.Sprites.Horse[24 + 2 * input.Actor.Unit.DickSize]);
                }
                else
                {
                    if (Config.FurryGenitals)
                    {
                        output.Sprite(input.Sprites.Horse[23]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.Horse[24 + 2 * input.Actor.Unit.DickSize]);
                    }
                }
            }
        }); //cocc

        builder.RenderSingle(SpriteType.Balls, 13, (input, output) =>
        {
            output.Coloring(SpottedBelly(input.Actor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            int size = input.Actor.GetBallSize(29, .8f);
            int baseSize = (input.Actor.Unit.DickSize + 1) / 3;

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && size == 29)
            {
                output.Sprite(input.Sprites.Horse[59]);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 29)
            {
                output.Sprite(input.Sprites.Horse[58]);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 27)
            {
                output.Sprite(input.Sprites.Horse[57]);
                return;
            }

            int combined = Math.Min(baseSize + size + 2, 26);
            if (size > 0)
            {
                output.Sprite(input.Sprites.Horse[30 + combined]);
                return;
            }

            output.Sprite(input.Sprites.Horse[30 + baseSize]);
        }); //balls        

        builder.RenderSingle(SpriteType.Weapon, 12, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                int weaponSprite = input.Actor.GetWeaponSprite();
                switch (weaponSprite)
                {
                    case 0:
                        output.Sprite(input.Sprites.Horse[190]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Horse[191]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Horse[192]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Horse[193]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Horse[194]);
                        return;
                    case 5:
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Horse[197]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Horse[199]);
                        return;
                }
            }
        });

        builder.RunBefore((input, output) =>
        {
            CommonRaceCode.MakeBreastOversize(29 * 29).Invoke(input, output);
            Defaults.BasicBellyRunAfter.Invoke(input, output);
        });

        builder.RandomCustom(data =>
        {
            Unit unit = data.Unit;
            Defaults.RandomCustom(data);


            unit.BodyAccentType3 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes3);
            unit.BodyAccentType4 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes4);
            unit.BodyAccentType5 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes5);

            unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
            unit.TailType = State.Rand.Next(data.MiscRaceData.TailTypes);
        });
    });


    private static class HorseUndertops
    {
        internal static IClothing<IOverSizeParameters> MakeCommon(int type, Sprite discard, Sprite sprite1, Func<Actor_Unit, Sprite> sprite2)
        {
            ClothingBuilder<IOverSizeParameters> builder = ClothingBuilder.New<IOverSizeParameters>();

            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = discard;
                output.Type = type;
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(20);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(sprite1);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(sprite2(input.Actor));
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
            return builder.BuildClothing();
        }
    }

    private static class HorseUndertop1
    {
        internal static readonly IClothing<IOverSizeParameters> HorseUndertop1Instance = HorseUndertops.MakeCommon(
            76147,
            State.GameManager.SpriteDictionary.HorseClothing[47],
            State.GameManager.SpriteDictionary.HorseClothing[47],
            actor => State.GameManager.SpriteDictionary.HorseClothing[40 + actor.Unit.BreastSize]
        );
    }

    private static class HorseUndertop2
    {
        internal static readonly IClothing<IOverSizeParameters> HorseUndertop2Instance = HorseUndertops.MakeCommon(
            76148,
            State.GameManager.SpriteDictionary.HorseClothing[48],
            null,
            actor => State.GameManager.SpriteDictionary.HorseClothing[48 + actor.Unit.BreastSize]
        );
    }

    private static class HorseUndertop3
    {
        internal static readonly IClothing<IOverSizeParameters> HorseUndertop3Instance = HorseUndertops.MakeCommon(
            76156,
            State.GameManager.SpriteDictionary.HorseClothing[56],
            null,
            actor => State.GameManager.SpriteDictionary.HorseClothing[56 + actor.Unit.BreastSize]
        );
    }

    private static class HorseUndertop4
    {
        internal static readonly IClothing<IOverSizeParameters> HorseUndertop4Instance = HorseUndertops.MakeCommon(
            76208,
            State.GameManager.SpriteDictionary.HorseExtras1[8],
            State.GameManager.SpriteDictionary.HorseExtras1[7],
            actor => State.GameManager.SpriteDictionary.HorseExtras1[0 + actor.Unit.BreastSize]
        );
    }

    private static class HorseUndertopShared
    {
        internal static IClothing MakeCommon(int type, Sprite discard, Sprite sprite1, Sprite sprite2)
        {
            ClothingBuilder builder = ClothingBuilder.New();

            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = discard;
                output.Type = type;
                output.MaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(20);
                int size = input.Actor.GetStomachSize(32, 1.2f);
                output["Clothing1"].Sprite(size >= 6 ? sprite1 : sprite2);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
            return builder.BuildClothing();
        }
    }

    private static class HorseUndertopM1
    {
        internal static readonly IClothing HorseUndertopM1Instance = HorseUndertopShared.MakeCommon(
            76136,
            State.GameManager.SpriteDictionary.HorseClothing[36],
            State.GameManager.SpriteDictionary.HorseExtras1[17],
            State.GameManager.SpriteDictionary.HorseClothing[36]
        );
    }

    private static class HorseUndertopM2
    {
        internal static readonly IClothing HorseUndertopM2Instance = HorseUndertopShared.MakeCommon(
            76137,
            State.GameManager.SpriteDictionary.HorseClothing[37],
            State.GameManager.SpriteDictionary.HorseExtras1[18],
            State.GameManager.SpriteDictionary.HorseClothing[37]
        );
    }

    private static class HorseUndertopM3
    {
        internal static readonly IClothing HorseUndertopM3Instance = HorseUndertopShared.MakeCommon(
            76138,
            State.GameManager.SpriteDictionary.HorseClothing[38],
            State.GameManager.SpriteDictionary.HorseClothing[39],
            State.GameManager.SpriteDictionary.HorseClothing[38]
        );
    }

    private static class HorsePoncho
    {
        internal static readonly IClothing HorsePonchoInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HorseClothing[33];
                output.Type = 76133;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(21);
                output["Clothing1"].Sprite(input.Sprites.HorseClothing[33]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Layer(3);
                output["Clothing2"].Sprite(input.Sprites.HorseClothing[34]);
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class HorseNecklace
    {
        internal static readonly IClothing HorseNecklaceInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.HorseClothing[35];
                output.Type = 76135;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(21);
                output["Clothing1"].Sprite(input.Sprites.HorseClothing[35]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class HorseUBottom
    {
        internal static readonly IClothing HorseUBottom1 = MakeHorseUBottom(2, 0, 30, 5, 9, State.GameManager.SpriteDictionary.HorseClothing, 76105, false);
        internal static readonly IClothing HorseUBottom2 = MakeHorseUBottom(7, 5, 30, 9, 9, State.GameManager.SpriteDictionary.HorseClothing, 76109, false);
        internal static readonly IClothing HorseUBottom3 = MakeHorseUBottom(17, 15, 30, 19, 9, State.GameManager.SpriteDictionary.HorseClothing, 76119, false);
        internal static readonly IClothing HorseUBottom4 = MakeHorseUBottom(22, 20, 30, 24, 9, State.GameManager.SpriteDictionary.HorseClothing, 76124, false);
        internal static readonly IClothing HorseUBottom5 = MakeHorseUBottom(27, 25, 14, 29, 9, State.GameManager.SpriteDictionary.HorseClothing, 76129, true);

        private static IClothing MakeHorseUBottom(int sprF, int sprM, int bulge, int discard, int layer, Sprite[] sheet, int type, bool black)
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
                if (input.Actor.HasBelly)
                {
                    output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? sheet[sprF + 1] : sheet[sprM + 1]);

                    if (input.Actor.Unit.HasDick)
                    {
                        //if (output.BlocksDick == true)
                        if (true)
                        {
                            output["Clothing2"].Sprite(black ? input.Sprites.HorseExtras1[bulge + input.Actor.Unit.DickSize] : input.Sprites.HorseClothing[bulge + input.Actor.Unit.DickSize]);
                        }
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? sheet[sprF] : sheet[sprM]);

                    if (input.Actor.Unit.HasDick)
                    {
                        //if (output.BlocksDick == true)
                        if (true)
                        {
                            output["Clothing2"].Sprite(black ? input.Sprites.HorseExtras1[bulge + input.Actor.Unit.DickSize] : input.Sprites.HorseClothing[bulge + input.Actor.Unit.DickSize]);
                        }
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
            return builder.BuildClothing();
        }
    }


    private static class HorseOBottom1
    {
        internal static readonly IClothing HorseOBottom1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.DiscardSprite = input.Sprites.HorseClothing[14];
                output.Type = 76114;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(15);
                output["Clothing1"].Coloring(Color.white);

                if (input.Actor.HasBelly)
                {
                    output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.HorseClothing[13] : input.Sprites.HorseClothing[11]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.HorseClothing[12] : input.Sprites.HorseClothing[10]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class HorseOBottom2
    {
        internal static readonly IClothing HorseOBottom2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.DiscardSprite = input.Sprites.HorseClothing[68];
                output.Type = 76168;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(16);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(15);
                output["Clothing1"].Coloring(Color.white);
                {
                    if (input.Actor.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.HorseClothing[67] : input.Sprites.HorseClothing[65]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.HorseClothing[66] : input.Sprites.HorseClothing[64]);
                    }
                }
                if (input.Actor.Unit.HasDick)
                {
                    output["Clothing2"].Sprite(input.Sprites.HorseExtras1[Math.Min(14 + input.Actor.Unit.DickSize, 17)]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class HorseOBottom3
    {
        internal static readonly IClothing HorseOBottom3Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.DiscardSprite = input.Sprites.HorseClothing[73];
                output.Type = 76173;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(15);
                output["Clothing1"].Coloring(Color.white);

                if (input.Actor.HasBelly)
                {
                    output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.HorseClothing[72] : input.Sprites.HorseClothing[70]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.HorseClothing[71] : input.Sprites.HorseClothing[69]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        });
    }
}