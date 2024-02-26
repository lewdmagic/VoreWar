#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.MainRaces
{
    internal static class EquinesImrpoved
    {
        private static Func<IClothingRenderInput, IOverSizeParameters> _paramsCalc = CommonRaceCode.MakeOversizeFunc(29 * 29);

        private static ColorSwapPalette LegTuft(IActorUnit actor)
        {
            if (actor.Unit.BodyAccentType3 >= 2)
            {
                return ColorPaletteMap.GetPalette(SwapType.HorseSkin, actor.Unit.AccessoryColor);
            }

            return ColorPaletteMap.GetPalette(SwapType.HorseSkin, actor.Unit.SkinColor);
        }

        private static ColorSwapPalette SpottedBelly(IActorUnit actor)
        {
            if (actor.Unit.BodyAccentType5 == 1)
            {
                return ColorPaletteMap.GetPalette(SwapType.HorseSkin, actor.Unit.AccessoryColor);
            }

            return ColorPaletteMap.GetPalette(SwapType.HorseSkin, actor.Unit.SkinColor);
        }

        private static ColorSwapPalette TailBit(IActorUnit actor)
        {
            if (actor.Unit.BodyAccentType3 == 5)
            {
                return ColorPaletteMap.GetPalette(SwapType.HorseSkin, actor.Unit.SkinColor);
            }

            return ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
        }

        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Equine", "Equines");
                output.FlavorText(new FlavorText(
                    new Texts { },
                    new Texts { },
                    new Texts { "equine", "bronco", { "mare", Gender.Female }, { "stallion", Gender.Male } }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 10,
                    StomachSize = 16,
                    HasTail = true,
                    FavoredStat = Stat.Agility,
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Charge,
                        TraitType.StrongMelee
                    },
                    RaceDescription = "",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.ClothingExtraType1, "Overtop");
                    buttons.SetText(ButtonType.ClothingExtraType2, "Overbottom");
                    buttons.SetText(ButtonType.BodyAccentTypes3, "Skin Pattern");
                    buttons.SetText(ButtonType.BodyAccentTypes4, "Head Pattern");
                    buttons.SetText(ButtonType.BodyAccentTypes5, "Torso Color");
                });
                output.TownNames(new List<string>
                {
                    "Cataphracta",
                    "Equus",
                    "The Ranch",
                    "Haciendo",
                    "Alfarsan"
                });


                output.DickSizes = () => 3;
                output.BreastSizes = () => 7;

                output.SpecialAccessoryCount = 0;
                output.ClothingShift = new Vector3(0, 0, 0);
                output.AvoidedEyeTypes = 0;
                output.AvoidedMouthTypes = 0;

                output.HairColors = ColorPaletteMap.GetPaletteCount(SwapType.UniversalHair);
                output.HairStyles = 15;
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.HorseSkin);
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.HorseSkin);
                output.EyeTypes = 4;
                output.EyeColors = ColorPaletteMap.GetPaletteCount(SwapType.EyeColor);
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


                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.Clothing50Spaced);

                output.ExtendedBreastSprites = true;


                output.AllowedMainClothingTypes.Set( //undertops
                    HorseClothing.HorseUndertop1Instance.Create(_paramsCalc),
                    HorseClothing.HorseUndertop2Instance.Create(_paramsCalc),
                    HorseClothing.HorseUndertop3Instance.Create(_paramsCalc),
                    HorseClothing.HorseUndertop4Instance.Create(_paramsCalc),
                    HorseClothing.HorseUndertopM1Instance,
                    HorseClothing.HorseUndertopM2Instance,
                    HorseClothing.HorseUndertopM3Instance
                );

                output.AllowedWaistTypes.Set( //underbottoms
                    HorseClothing.HorseUBottom1,
                    HorseClothing.HorseUBottom2,
                    HorseClothing.HorseUBottom3,
                    HorseClothing.HorseUBottom4,
                    HorseClothing.HorseUBottom5
                );

                output.ExtraMainClothing1Types.Set( //Overtops
                    HorseClothing.HorsePonchoInstance,
                    HorseClothing.HorseNecklaceInstance
                );

                output.ExtraMainClothing2Types.Set( //Overbottoms
                    HorseClothing.HorseOBottom1Instance,
                    HorseClothing.HorseOBottom2Instance,
                    HorseClothing.HorseOBottom3Instance
                );

                output.WholeBodyOffset = new Vector2(0, 16 * .625f);
            });

            builder.RenderAll((input, output) =>
            {
                string headState = input.A.IsAttacking || input.A.IsEating ? "eat" : "still";
                output.NewSprite(SpriteType.Head, 5)
                    .Coloring(ColorPaletteMap.GetPalette(SwapType.HorseSkin, input.U.SkinColor))
                    .Sprite($"head_{input.Sex}_{headState}");

                var eyes = output.NewSprite(SpriteType.Eyes, 6);
                eyes.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
                if (input.U.IsDead && input.U.Items != null)
                {
                    eyes.Sprite($"eyes_{input.Sex}_dead");
                }
                else
                {
                    eyes.Sprite0($"eyes_{input.Sex}", input.U.EyeType);
                }

                var hair = output.NewSprite(SpriteType.Hair, 21);
                hair.Coloring(ColorPaletteMap.GetPalette(SwapType.UniversalHair, input.U.HairColor));
                hair.Sprite0("hair_front", input.U.HairStyle);


                var backHair = output.NewSprite(SpriteType.Hair2, 1);
                backHair.Coloring(ColorPaletteMap.GetPalette(SwapType.UniversalHair, input.U.HairColor));
                backHair.Sprite0("hair_back", input.U.HairStyle);

                var body = output.NewSprite(SpriteType.Body, 4);
                body.Coloring(ColorPaletteMap.GetPalette(SwapType.HorseSkin, input.U.SkinColor));
                string bodyName = input.U.HasBreasts ? "body_female" : "body_male";
                int bodyIndex = input.A.IsAttacking ? 2 : input.U.HasWeapon ? 1 : 0;
                body.Sprite0(bodyName, bodyIndex);

                if (input.U.BodyAccentType3 != 0)
                {
                    var limbSpots = output.NewSprite(SpriteType.BodyAccent3, 5);
                    limbSpots.Coloring(ColorPaletteMap.GetPalette(SwapType.HorseSkin, input.U.AccessoryColor));
                    string sex = input.U.HasBreasts ? "female" : "male";
                    string state = input.A.IsAttacking ? "attack" : input.U.HasWeapon ? "holdweapon" : "stand";
                    limbSpots.Sprite0($"skin_pattern_{state}_{sex}", input.U.BodyAccentType3 - 1);
                }

                if (input.U.BodyAccentType4 != 0)
                {
                    var headSpots = output.NewSprite(SpriteType.BodyAccent4, 6);
                    headSpots.Coloring(ColorPaletteMap.GetPalette(SwapType.HorseSkin, input.U.AccessoryColor));
                    string state = input.A.IsAttacking || input.A.IsEating ? "eat" : "still";
                    headSpots.Sprite0($"head_pattern_{input.Sex}_{state}", input.U.BodyAccentType4 - 1);
                }

                var bellySpots = output.NewSprite(SpriteType.BodyAccent5, 5); //belly spots, also color breasts/belly/dick
                bellySpots.Coloring(ColorPaletteMap.GetPalette(SwapType.HorseSkin, input.U.AccessoryColor));
                bellySpots.Sprite($"torso_pattern_{input.Sex}");

                var legTuft = output.NewSprite(SpriteType.BodyAccent8, 6);
                legTuft.Coloring(LegTuft(input.Actor));
                legTuft.Sprite("leg_tuft");

                var hooves = output.NewSprite(SpriteType.BodyAccent10, 5);
                hooves.Sprite($"hooves_{input.Sex}");

                var tail = output.NewSprite(SpriteType.BodyAccessory, 2);
                tail.Coloring(ColorPaletteMap.GetPalette(SwapType.UniversalHair, input.U.HairColor));
                tail.Sprite0("tail_1", input.U.TailType);


                var tailBit = output.NewSprite(SpriteType.BodyAccent9, 3);
                tailBit.Coloring(TailBit(input.Actor));
                tailBit.Sprite0("tail_2", input.U.TailType, true);

                if (input.U.HasBreasts)
                {
                    var breasts = output.NewSprite(SpriteType.Breasts, 19);
                    breasts.Coloring(SpottedBelly(input.Actor));

                    if (input.A.PredatorComponent?.LeftBreastFullness > 0)
                    {
                        int leftSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(29 * 29));

                        if (leftSize > 26)
                        {
                            leftSize = 26;
                        }

                        breasts.Sprite0("breast_left", leftSize);
                    }
                    else
                    {
                        if (input.U.DefaultBreastSize == 0)
                        {
                            breasts.Sprite0("breast_left", 0);
                        }
                        else
                        {
                            breasts.Sprite0("breast_left", input.U.BreastSize);
                        }
                    }
                }


                if (input.U.HasBreasts)
                {
                    var secondaryBreasts = output.NewSprite(SpriteType.SecondaryBreasts, 19);
                    secondaryBreasts.Coloring(SpottedBelly(input.Actor));
                    if (input.A.PredatorComponent?.RightBreastFullness > 0)
                    {
                        int rightSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(29 * 29));

                        if (rightSize > 26)
                        {
                            rightSize = 26;
                        }

                        secondaryBreasts.Sprite0("breast_right", rightSize);
                    }
                    else
                    {
                        if (input.U.DefaultBreastSize == 0)
                        {
                            secondaryBreasts.Sprite0("breast_right", 0);
                        }
                        else
                        {
                            secondaryBreasts.Sprite0("breast_right", input.U.BreastSize);
                        }
                    }
                }

                if (input.A.HasBelly)
                {
                    var belly = output.NewSprite(SpriteType.Belly, 17);
                    belly.Coloring(SpottedBelly(input.Actor));
                    int size = input.A.GetStomachSize(29, 1.2f);
                    int combined = Math.Min(size, 26);
                    belly.Sprite0("belly", combined, true);
                }

                if (input.U.HasDick)
                {
                    var cock = output.NewSprite(SpriteType.Dick, 14);
                    cock.Coloring(SpottedBelly(input.Actor));
                    bool breastsNotTooBig = input.A.PredatorComponent?.VisibleFullness < .26f &&
                                            (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(29 * 29)) < 16 &&
                                            (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(29 * 29)) < 16;

                    cock.Layer(breastsNotTooBig ? 24 : 14);

                    bool useErectSprite = input.A.IsErect() || input.A.IsCockVoring;

                    if (!useErectSprite && Config.FurryGenitals)
                    {
                        cock.Sprite("penis_furry");
                    }
                    else
                    {
                        if (!breastsNotTooBig && useErectSprite)
                        {
                            cock.Sprite0("penis_erect_down", input.U.DickSize);
                        }
                        else
                        {
                            cock.Sprite0(useErectSprite ? "penis_erect_up" : "penis_flaccid", input.U.DickSize);
                        }
                    }
                }

                if (input.U.HasDick)
                {
                    var balls = output.NewSprite(SpriteType.Balls, 13);
                    balls.Coloring(SpottedBelly(input.Actor));
                    int size = input.A.GetBallSize(29, .8f);
                    int baseSize = (input.U.DickSize + 1) / 3;
                    int combinedSize = Math.Min(baseSize + size + 2, 26);
                    balls.Sprite0("balls", combinedSize);
                }


                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    var weapon = output.NewSprite(SpriteType.Weapon, 12);
                    weapon.Coloring(Defaults.WhiteColored);
                    weapon.Sprite(input.SimpleWeaponSpriteFrontV1, true);
                }


                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    var bowBit = output.NewSprite(SpriteType.SecondaryAccessory, 3);
                    bowBit.Coloring(Defaults.WhiteColored);
                    bowBit.Sprite(input.SimpleWeaponSpriteBackV1, true);
                }
            });

            builder.RandomCustom(data =>
            {
                IUnitRead unit = data.Unit;
                Defaults.RandomCustom(data);


                unit.BodyAccentType3 = State.Rand.Next(data.SetupOutput.BodyAccentTypes3);
                unit.BodyAccentType4 = State.Rand.Next(data.SetupOutput.BodyAccentTypes4);
                unit.BodyAccentType5 = State.Rand.Next(data.SetupOutput.BodyAccentTypes5);

                unit.HairStyle = State.Rand.Next(data.SetupOutput.HairStyles);
                unit.TailType = State.Rand.Next(data.SetupOutput.TailTypes);
            });
        });


        private static class HorseUndertops
        {
            internal static void MakeCommon(IClothingBuilder<IOverSizeParameters> builder, ClothingId clothingId, Sprite discard, Sprite sprite1, Func<IActorUnit, Sprite> sprite2)
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = discard;
                    output.ClothingId = clothingId;
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
                        output["Clothing1"].Sprite(sprite1);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(sprite2(input.Actor));
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            }
        }


        public static class HorseClothing
        {
            internal static readonly BindableClothing<IOverSizeParameters> HorseUndertop1Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
                {
                    HorseUndertops.MakeCommon(builder, new ClothingId("base.equines/76147"),
                        State.GameManager.SpriteDictionary.HorseClothing[47],
                        State.GameManager.SpriteDictionary.HorseClothing[47],
                        actor => State.GameManager.SpriteDictionary.HorseClothing[40 + actor.Unit.BreastSize]);
                }
            );


            internal static readonly BindableClothing<IOverSizeParameters> HorseUndertop1Instance3 = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
                {
                    builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                    {
                        output.DiscardSprite = null;
                        output.ClothingId = new ClothingId("base.equines/76147");
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
                            output["Clothing1"].Sprite(input.Sprites.HorseClothing[47]);
                        }
                        else if (input.U.HasBreasts)
                        {
                            output["Clothing1"].Sprite(input.Sprites.HorseClothing[40 + input.U.BreastSize]);
                        }

                        output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    });
                }
            );


            internal static readonly BindableClothing<IOverSizeParameters> HorseUndertop1Instance2 = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
                {
                    builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                    {
                        output.DiscardSprite = null;
                        output.ClothingId = new ClothingId("base.equines/76147");
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
                            output["Clothing1"].Sprite(input.Sprites.HorseClothing[47]);
                        }
                        else if (input.U.HasBreasts)
                        {
                            output["Clothing1"].Sprite(input.Sprites.HorseClothing[40 + input.U.BreastSize]);
                        }

                        output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    });
                }
            );


            internal static readonly BindableClothing<IOverSizeParameters> HorseUndertop2Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
                {
                    HorseUndertops.MakeCommon(builder, new ClothingId("base.equines/76148"),
                        State.GameManager.SpriteDictionary.HorseClothing[48],
                        null,
                        actor => State.GameManager.SpriteDictionary.HorseClothing[48 + actor.Unit.BreastSize]);
                }
            );

            internal static readonly BindableClothing<IOverSizeParameters> HorseUndertop3Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
                {
                    HorseUndertops.MakeCommon(builder, new ClothingId("base.equines/76156"),
                        State.GameManager.SpriteDictionary.HorseClothing[56],
                        null,
                        actor => State.GameManager.SpriteDictionary.HorseClothing[56 + actor.Unit.BreastSize]);
                }
            );

            internal static readonly BindableClothing<IOverSizeParameters> HorseUndertop4Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
                {
                    HorseUndertops.MakeCommon(builder, new ClothingId("base.equines/76208"),
                        State.GameManager.SpriteDictionary.HorseExtras1[8],
                        State.GameManager.SpriteDictionary.HorseExtras1[7],
                        actor => State.GameManager.SpriteDictionary.HorseExtras1[0 + actor.Unit.BreastSize]);
                }
            );

            private static IClothing MakeCommon(ClothingId clothingId, Sprite discard, Sprite sprite1, Sprite sprite2)
            {
                ClothingBuilder builder = ClothingBuilder.New();


                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = discard;
                    output.ClothingId = clothingId;
                    output.MaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(20);
                    int size = input.A.GetStomachSize(32, 1.2f);
                    output["Clothing1"].Sprite(size >= 6 ? sprite1 : sprite2);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
                return builder.BuildClothing();
            }

            internal static readonly IClothing HorseUndertopM1Instance = MakeCommon(
                new ClothingId("base.equines/76136"),
                State.GameManager.SpriteDictionary.HorseClothing[36],
                State.GameManager.SpriteDictionary.HorseExtras1[17],
                State.GameManager.SpriteDictionary.HorseClothing[36]
            );

            internal static readonly IClothing HorseUndertopM2Instance = MakeCommon(
                new ClothingId("base.equines/76137"),
                State.GameManager.SpriteDictionary.HorseClothing[37],
                State.GameManager.SpriteDictionary.HorseExtras1[18],
                State.GameManager.SpriteDictionary.HorseClothing[37]
            );

            internal static readonly IClothing HorseUndertopM3Instance = MakeCommon(
                new ClothingId("base.equines/76138"),
                State.GameManager.SpriteDictionary.HorseClothing[38],
                State.GameManager.SpriteDictionary.HorseClothing[39],
                State.GameManager.SpriteDictionary.HorseClothing[38]
            );

            internal static readonly IClothing HorsePonchoInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HorseClothing[33];
                    output.ClothingId = new ClothingId("base.equines/76133");
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(21);
                    output["Clothing1"].Sprite(input.Sprites.HorseClothing[33]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    output["Clothing2"].Layer(3);
                    output["Clothing2"].Sprite(input.Sprites.HorseClothing[34]);
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });

            internal static readonly IClothing HorseNecklaceInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HorseClothing[35];
                    output.ClothingId = new ClothingId("base.equines/76135");
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(21);
                    output["Clothing1"].Sprite(input.Sprites.HorseClothing[35]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });

            internal static readonly IClothing HorseUBottom1 = ClothingBuilder.Create(builder => MakeHorseUBottomV2(builder, 2, 0, 30, 5, 9, State.GameManager.SpriteDictionary.HorseClothing, new ClothingId("base.equines/76105"), false));
            internal static readonly IClothing HorseUBottom2 = ClothingBuilder.Create(builder => MakeHorseUBottomV2(builder, 7, 5, 30, 9, 9, State.GameManager.SpriteDictionary.HorseClothing, new ClothingId("base.equines/76109"), false));
            internal static readonly IClothing HorseUBottom3 = ClothingBuilder.Create(builder => MakeHorseUBottomV2(builder, 17, 15, 30, 19, 9, State.GameManager.SpriteDictionary.HorseClothing, new ClothingId("base.equines/76119"), false));
            internal static readonly IClothing HorseUBottom4 = ClothingBuilder.Create(builder => MakeHorseUBottomV2(builder, 22, 20, 30, 24, 9, State.GameManager.SpriteDictionary.HorseClothing, new ClothingId("base.equines/76124"), false));
            internal static readonly IClothing HorseUBottom5 = ClothingBuilder.Create(builder => MakeHorseUBottomV2(builder, 27, 25, 14, 29, 9, State.GameManager.SpriteDictionary.HorseClothing, new ClothingId("base.equines/76129"), true));

            private static void MakeHorseUBottomV2(IClothingBuilder builder, int sprF, int sprM, int bulge, int discard, int layer, Sprite[] sheet, ClothingId clothingId, bool black)
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.DiscardSprite = sheet[discard];
                    output.ClothingId = clothingId;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(layer);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing2"].Layer(layer + 1);
                    output["Clothing2"].Coloring(Color.white);
                    if (input.A.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.U.HasBreasts ? sheet[sprF + 1] : sheet[sprM + 1]);

                        if (input.U.HasDick)
                        {
                            //if (output.BlocksDick == true)
                            if (true)
                            {
                                output["Clothing2"].Sprite(black ? input.Sprites.HorseExtras1[bulge + input.U.DickSize] : input.Sprites.HorseClothing[bulge + input.U.DickSize]);
                            }
                        }
                        else
                        {
                            output["Clothing2"].Sprite(null);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.U.HasBreasts ? sheet[sprF] : sheet[sprM]);

                        if (input.U.HasDick)
                        {
                            //if (output.BlocksDick == true)
                            if (true)
                            {
                                output["Clothing2"].Sprite(black ? input.Sprites.HorseExtras1[bulge + input.U.DickSize] : input.Sprites.HorseClothing[bulge + input.U.DickSize]);
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

            internal static readonly IClothing HorseOBottom1Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.DiscardSprite = input.Sprites.HorseClothing[14];
                    output.ClothingId = new ClothingId("base.equines/76114");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(15);
                    output["Clothing1"].Coloring(Color.white);

                    if (input.A.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.U.HasBreasts ? input.Sprites.HorseClothing[13] : input.Sprites.HorseClothing[11]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.U.HasBreasts ? input.Sprites.HorseClothing[12] : input.Sprites.HorseClothing[10]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });


            internal static readonly IClothing HorseOBottom2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.DiscardSprite = input.Sprites.HorseClothing[68];
                    output.ClothingId = new ClothingId("base.equines/76168");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(16);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(15);
                    output["Clothing1"].Coloring(Color.white);
                    {
                        if (input.A.HasBelly)
                        {
                            output["Clothing1"].Sprite(input.U.HasBreasts ? input.Sprites.HorseClothing[67] : input.Sprites.HorseClothing[65]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.U.HasBreasts ? input.Sprites.HorseClothing[66] : input.Sprites.HorseClothing[64]);
                        }
                    }
                    if (input.U.HasDick)
                    {
                        output["Clothing2"].Sprite(input.Sprites.HorseExtras1[Math.Min(14 + input.U.DickSize, 17)]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });

            internal static readonly IClothing HorseOBottom3Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.DiscardSprite = input.Sprites.HorseClothing[73];
                    output.ClothingId = new ClothingId("base.equines/76173");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(15);
                    output["Clothing1"].Coloring(Color.white);

                    if (input.A.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.U.HasBreasts ? input.Sprites.HorseClothing[72] : input.Sprites.HorseClothing[70]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.U.HasBreasts ? input.Sprites.HorseClothing[71] : input.Sprites.HorseClothing[69]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });
        }
    }
}