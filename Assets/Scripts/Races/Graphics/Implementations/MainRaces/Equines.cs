#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

internal static class Equines
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank<OverSizeParameters>, builder =>
    {
        builder.Names("Equine", "Equines");
        builder.FlavorText(new FlavorText(
            new Texts {  },
            new Texts {  },
            new Texts { "equine", "bronco", {"mare", Gender.Female}, {"stallion", Gender.Male} }
        ));
        builder.RaceTraits(new RaceTraits()
        {
            BodySize = 10,
            StomachSize = 16,
            HasTail = true,
            FavoredStat = Stat.Agility,
            RacialTraits = new List<Traits>()
            {
                Traits.Charge,
                Traits.StrongMelee
            },
            RaceDescription = "",
        });
        builder.CustomizeButtons((unit, buttons) =>
        {
            buttons[(int)UnitCustomizer.ButtonTypes.ClothingExtraType1].Label.text = "Overtop";
            buttons[(int)UnitCustomizer.ButtonTypes.ClothingExtraType2].Label.text = "Overbottom";
            buttons[(int)UnitCustomizer.ButtonTypes.BodyAccentTypes3].Label.text = "Skin Pattern";
            buttons[(int)UnitCustomizer.ButtonTypes.BodyAccentTypes4].Label.text = "Head Pattern";
            buttons[(int)UnitCustomizer.ButtonTypes.BodyAccentTypes5].Label.text = "Torso Color";
        });
        builder.TownNames(new List<string>
        {
            "Cataphracta",
            "Equus",
            "The Ranch",
            "Haciendo",
            "Alfarsan"
        });
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
                HorseClothing.HorseUndertop1Instance,
                HorseClothing.HorseUndertop2Instance,
                HorseClothing.HorseUndertop3Instance,
                HorseClothing.HorseUndertop4Instance,
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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, input.U.SkinColor));
            string state = (input.A.IsAttacking || input.A.IsEating) ? "eat" : "still";
            output.Sprite(($"head_{input.Sex}_{state}"));
        }); //head

        builder.RenderSingle(SpriteType.Eyes, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.U.EyeColor));
            if (input.U.IsDead && input.U.Items != null)
            {
                output.Sprite(($"eyes_{input.Sex}_dead"));
            }
            else
            {
                output.Sprite0($"eyes_{input.Sex}", input.U.EyeType);
            }
        }); //eyes;

        builder.RenderSingle(SpriteType.Hair, 21, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, input.U.HairColor));
            output.Sprite0("hair_front", input.U.HairStyle);
        }); //forward hair;

        builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, input.U.HairColor));
            output.Sprite0("hair_back", input.U.HairStyle);
        }); //back hair

        builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, input.U.SkinColor));
            string name = input.U.HasBreasts ? "body_female" : "body_male";
            int index = input.A.IsAttacking ? 2 : (input.U.HasWeapon ? 1 : 0);

            output.Sprite0(name, index);
        }); //body

        builder.RenderSingle(SpriteType.BodyAccent3, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, input.U.AccessoryColor));
            if (input.U.BodyAccentType3 != 0)
            {
                string sex = input.U.HasBreasts ? "female" : "male";
                string state = input.A.IsAttacking ? "attack" : (input.U.HasWeapon ? "holdweapon" : "stand");
                output.Sprite0($"skin_pattern_{state}_{sex}", input.U.BodyAccentType3 - 1);
            }
        }); //limb spots

        builder.RenderSingle(SpriteType.BodyAccent4, 6, (input, output) =>
        {
            if (input.U.BodyAccentType4 != 0)
            {
                output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, input.U.AccessoryColor));
                string state = (input.A.IsAttacking || input.A.IsEating) ? "eat" : "still";
                output.Sprite0($"head_pattern_{input.Sex}_{state}", input.U.BodyAccentType4 - 1);
            }
        }); //head spots

        builder.RenderSingle(SpriteType.BodyAccent5, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, input.U.AccessoryColor));
            output.Sprite(($"torso_pattern_{input.Sex}"));
        }); //belly spots, also color breasts/belly/dick

        builder.RenderSingle(SpriteType.BodyAccent8, 6, (input, output) =>
        {
            output.Coloring(LegTuft(input.Actor));
            output.Sprite(("leg_tuft"));
        }); //leg tuft



        builder.RenderSingle(SpriteType.BodyAccent10, 5, (input, output) =>
        {
            output.Sprite(($"hooves_{input.Sex}"));
        }); //leg hoof;

        builder.RenderSingle(SpriteType.BodyAccessory, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, input.U.HairColor));
            output.Sprite0("tail_1", input.U.TailType);
        }); //tail
        
        builder.RenderSingle(SpriteType.BodyAccent9, 3, (input, output) =>
        {
            output.Coloring(TailBit(input.Actor));
            output.Sprite0("tail_2", input.U.TailType, true);
        }); //tail bit


        builder.RenderSingle(SpriteType.Breasts, 19, (input, output) =>
        {
            output.Coloring(SpottedBelly(input.Actor));
            if (input.U.HasBreasts == false)
            {
                return;
            }

            if (input.A.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(29 * 29));

                if (leftSize > 26)
                {
                    leftSize = 26;
                }

                output.Sprite0("breast_left", leftSize);
            }
            else
            {
                if (input.U.DefaultBreastSize == 0)
                {
                    output.Sprite0("breast_left", 0);
                    return;
                }
                
                output.Sprite0("breast_left", input.U.BreastSize);
            }
        });

        builder.RenderSingle(SpriteType.SecondaryBreasts, 19, (input, output) =>
        {
            output.Coloring(SpottedBelly(input.Actor));
            if (input.U.HasBreasts == false)
            {
                return;
            }

            if (input.A.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(29 * 29));

                if (rightSize > 26)
                {
                    rightSize = 26;
                }
                
                output.Sprite0("breast_right", rightSize);
            }
            else
            {
                if (input.U.DefaultBreastSize == 0)
                {
                    output.Sprite0("breast_right", 0);
                    return;
                }

                output.Sprite0("breast_right", input.U.BreastSize);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 17, (input, output) =>
        {
            output.Coloring(SpottedBelly(input.Actor));
            if (input.A.HasBelly)
            {
                int size = input.A.GetStomachSize(29, 1.2f);
                int combined = Math.Min(size, 26);
                output.Sprite0("belly", combined, true);
            }
        }); //belly

        builder.RenderSingle(SpriteType.Dick, 14, (input, output) =>
        {
            output.Coloring(SpottedBelly(input.Actor));
            if (input.U.HasDick)
            {
                bool breastsNotTooBig = input.A.PredatorComponent?.VisibleFullness < .26f &&
                                        (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(29 * 29)) < 16 &&
                                        (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(29 * 29)) < 16;
            
                output.Layer(breastsNotTooBig ? 24 : 14);
            
                bool useErectSprite = input.A.IsErect() || input.A.IsCockVoring;

                if (!useErectSprite && Config.FurryGenitals)
                {
                    output.Sprite(("penis_furry"));
                }
                else
                {
                    if (!breastsNotTooBig && useErectSprite)
                    {
                        output.Sprite0("penis_erect_down", input.U.DickSize);
                    }
                    else
                    {
                        output.Sprite0(useErectSprite ? "penis_erect_up" : "penis_flaccid", input.U.DickSize);
                    }
                    
                }
            }
        }); //cocc

        builder.RenderSingle(SpriteType.Balls, 13, (input, output) =>
        {
            output.Coloring(SpottedBelly(input.Actor));
            if (input.U.HasDick == false)
            {
                return;
            }

            int size = input.A.GetBallSize(29, .8f);
            int baseSize = (input.U.DickSize + 1) / 3;
            int combinedSize = Math.Min(baseSize + size + 2, 26);

            output.Sprite0("balls", combinedSize);
        }); //balls        

        builder.RenderSingle(SpriteType.Weapon, 12, (input, output) =>
        {
            if (input.U.HasWeapon && input.A.Surrendered == false)
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.SimpleWeaponSpriteFrontV1, true);
            }
        });
        
        builder.RenderSingle(SpriteType.SecondaryAccessory, 3, (input, output) =>
        {            
            if (input.U.HasWeapon && input.A.Surrendered == false)
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.SimpleWeaponSpriteBackV1, true);
            }
        }); //bow bit

        builder.RunBefore((input, output) =>
        {
            // if (input.U.HasWeapon && input.A.Surrendered == false)
            // {
            //     output.ChangeSprite(SpriteType.SecondaryAccessory)
            //         .Sprite(input.SimpleWeaponSpriteBackV1, true)
            //         .Coloring(Defaults.WhiteColored)
            //         .Layer(3);
            //     
            //     output.ChangeSprite(SpriteType.SecondaryAccessory)
            //         .Sprite(input.SimpleWeaponSpriteFrontV1, true)
            //         .Coloring(Defaults.WhiteColored)
            //         .Layer(12);
            // }
            
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
                else if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(sprite2(input.Actor));
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
            return builder.BuildClothing();
        }
    }


    public static class HorseClothing
    {
        internal static readonly IClothing<IOverSizeParameters> HorseUndertop1Instance = HorseUndertops.MakeCommon(
            76147,
            State.GameManager.SpriteDictionary.HorseClothing[47],
            State.GameManager.SpriteDictionary.HorseClothing[47],
            actor => State.GameManager.SpriteDictionary.HorseClothing[40 + actor.Unit.BreastSize]
        );
        
        internal static readonly IClothing<IOverSizeParameters> HorseUndertop2Instance = HorseUndertops.MakeCommon(
            76148,
            State.GameManager.SpriteDictionary.HorseClothing[48],
            null,
            actor => State.GameManager.SpriteDictionary.HorseClothing[48 + actor.Unit.BreastSize]
        );
        
        internal static readonly IClothing<IOverSizeParameters> HorseUndertop3Instance = HorseUndertops.MakeCommon(
            76156,
            State.GameManager.SpriteDictionary.HorseClothing[56],
            null,
            actor => State.GameManager.SpriteDictionary.HorseClothing[56 + actor.Unit.BreastSize]
        );
        
        internal static readonly IClothing<IOverSizeParameters> HorseUndertop4Instance = HorseUndertops.MakeCommon(
            76208,
            State.GameManager.SpriteDictionary.HorseExtras1[8],
            State.GameManager.SpriteDictionary.HorseExtras1[7],
            actor => State.GameManager.SpriteDictionary.HorseExtras1[0 + actor.Unit.BreastSize]
        );
        
        private static IClothing MakeCommon(int type, Sprite discard, Sprite sprite1, Sprite sprite2)
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
                int size = input.A.GetStomachSize(32, 1.2f);
                output["Clothing1"].Sprite(size >= 6 ? sprite1 : sprite2);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
            return builder.BuildClothing();
        }
        
        internal static readonly IClothing HorseUndertopM1Instance = MakeCommon(
            76136,
            State.GameManager.SpriteDictionary.HorseClothing[36],
            State.GameManager.SpriteDictionary.HorseExtras1[17],
            State.GameManager.SpriteDictionary.HorseClothing[36]
        );
        
        internal static readonly IClothing HorseUndertopM2Instance = MakeCommon(
            76137,
            State.GameManager.SpriteDictionary.HorseClothing[37],
            State.GameManager.SpriteDictionary.HorseExtras1[18],
            State.GameManager.SpriteDictionary.HorseClothing[37]
        );
        
        internal static readonly IClothing HorseUndertopM3Instance = MakeCommon(
            76138,
            State.GameManager.SpriteDictionary.HorseClothing[38],
            State.GameManager.SpriteDictionary.HorseClothing[39],
            State.GameManager.SpriteDictionary.HorseClothing[38]
        );
        
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
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.U.ClothingColor));
                output["Clothing2"].Layer(3);
                output["Clothing2"].Sprite(input.Sprites.HorseClothing[34]);
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
        });
        
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
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
        });
        
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

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.U.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
            return builder.BuildClothing();
        }
        
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

                if (input.A.HasBelly)
                {
                    output["Clothing1"].Sprite(input.U.HasBreasts ? input.Sprites.HorseClothing[13] : input.Sprites.HorseClothing[11]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.U.HasBreasts ? input.Sprites.HorseClothing[12] : input.Sprites.HorseClothing[10]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
        });
        
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

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.U.ClothingColor));
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
        });
        
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

                if (input.A.HasBelly)
                {
                    output["Clothing1"].Sprite(input.U.HasBreasts ? input.Sprites.HorseClothing[72] : input.Sprites.HorseClothing[70]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.U.HasBreasts ? input.Sprites.HorseClothing[71] : input.Sprites.HorseClothing[69]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
        });
    }
}