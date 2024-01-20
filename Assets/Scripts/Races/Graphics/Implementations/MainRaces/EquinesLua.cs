#region

using System;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.MainRaces
{
    internal static class EquinesLua
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            // builder.RunBefore((input, output) =>
            // {
            //     CommonRaceCode.MakeBreastOversize(29 * 29).Invoke(input, output);
            //     Defaults.BasicBellyRunAfter(input, output);
            // });

            // builder.RandomCustom(data =>
            // {
            //     Defaults.RandomCustom(data);
            //
            //     data.Unit.BodyAccentType3 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes3);
            //     data.Unit.BodyAccentType4 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes4);
            //     data.Unit.BodyAccentType5 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes5);
            //
            //     data.Unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
            //     data.Unit.TailType = State.Rand.Next(data.MiscRaceData.TailTypes);
            // });
        
            //ScriptHelper.ScriptPrep2("GameData/CustomRaces/Equinezz/EquineNeo.lua", "equines_lua", builder);
        });



    

        private static class HorseUndertops
        {
            internal static BindableClothing<IOverSizeParameters> MakeCommon(ClothingId type, Sprite discard, Sprite sprite1, Func<Actor_Unit, Sprite> sprite2)
            {
                return ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
                {
                    builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                    {
                        output.DiscardSprite = discard;
                        output.ClothingId = type;
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

                        output["Clothing1"].Coloring(SwapType.Clothing50Spaced, input.U.ClothingColor);
                    });
                });
            }
        }


        public static class HorseClothing
        {
        
        
            public static readonly BindableClothing<IOverSizeParameters> HorseUndertop1Instance2 = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
                {
                    //ScriptHelper.ScriptPrepClothing("GameData/CustomRaces/Equinezz/Clothing/horsetop1/clothing.lua", builder);
                }
            );
        
            public static BindableClothing<IOverSizeParameters> HorseUndertop1Instance = HorseUndertops.MakeCommon(
                new ClothingId("base.equines/76147"),
                State.GameManager.SpriteDictionary.HorseClothing[47],
                State.GameManager.SpriteDictionary.HorseClothing[47],
                actor => State.GameManager.SpriteDictionary.HorseClothing[40 + actor.Unit.BreastSize]
            );
        
            public static BindableClothing<IOverSizeParameters> HorseUndertop2Instance = HorseUndertops.MakeCommon(
                new ClothingId("base.equines/76148"),
                State.GameManager.SpriteDictionary.HorseClothing[48],
                null,
                actor => State.GameManager.SpriteDictionary.HorseClothing[48 + actor.Unit.BreastSize]
            );
        
            public static BindableClothing<IOverSizeParameters> HorseUndertop3Instance = HorseUndertops.MakeCommon(
                new ClothingId("base.equines/76156"),
                State.GameManager.SpriteDictionary.HorseClothing[56],
                null,
                actor => State.GameManager.SpriteDictionary.HorseClothing[56 + actor.Unit.BreastSize]
            );
        
            public static BindableClothing<IOverSizeParameters> HorseUndertop4Instance = HorseUndertops.MakeCommon(
                new ClothingId("base.equines/76208"),
                State.GameManager.SpriteDictionary.HorseExtras1[8],
                State.GameManager.SpriteDictionary.HorseExtras1[7],
                actor => State.GameManager.SpriteDictionary.HorseExtras1[0 + actor.Unit.BreastSize]
            );
        
            private static IClothing MakeCommon(ClothingId type, Sprite discard, Sprite sprite1, Sprite sprite2)
            {
                ClothingBuilder builder = ClothingBuilder.New();

                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = discard;
                    output.ClothingId = type;
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

                    output["Clothing1"].Coloring(SwapType.Clothing50Spaced, input.U.ClothingColor);
                });
                return builder.BuildClothing();
            }
        
            public static IClothing HorseUndertopM1Instance = MakeCommon(
                new ClothingId("base.equines/76136"),
                State.GameManager.SpriteDictionary.HorseClothing[36],
                State.GameManager.SpriteDictionary.HorseExtras1[17],
                State.GameManager.SpriteDictionary.HorseClothing[36]
            );
        
            public static IClothing HorseUndertopM2Instance = MakeCommon(
                new ClothingId("base.equines/76137"),
                State.GameManager.SpriteDictionary.HorseClothing[37],
                State.GameManager.SpriteDictionary.HorseExtras1[18],
                State.GameManager.SpriteDictionary.HorseClothing[37]
            );
        
            public static IClothing HorseUndertopM3Instance = MakeCommon(
                new ClothingId("base.equines/76138"),
                State.GameManager.SpriteDictionary.HorseClothing[38],
                State.GameManager.SpriteDictionary.HorseClothing[39],
                State.GameManager.SpriteDictionary.HorseClothing[38]
            );
        
            public static IClothing HorsePonchoInstance = ClothingBuilder.Create(builder =>
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
                    output["Clothing1"].Coloring(SwapType.Clothing50Spaced, input.U.ClothingColor);
                    output["Clothing2"].Layer(3);
                    output["Clothing2"].Sprite(input.Sprites.HorseClothing[34]);
                    output["Clothing2"].Coloring(SwapType.Clothing50Spaced, input.U.ClothingColor);
                });
            });
        
            public static IClothing HorseNecklaceInstance = ClothingBuilder.Create(builder =>
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
                    output["Clothing1"].Coloring(SwapType.Clothing50Spaced, input.U.ClothingColor);
                });
            });
        
            public static IClothing HorseUBottom1 = MakeHorseUBottom(2, 0, 30, 5, 9, State.GameManager.SpriteDictionary.HorseClothing, new ClothingId("base.equines/76105"), false);
            public static IClothing HorseUBottom2 = MakeHorseUBottom(7, 5, 30, 9, 9, State.GameManager.SpriteDictionary.HorseClothing, new ClothingId("base.equines/76109"), false);
            public static IClothing HorseUBottom3 = MakeHorseUBottom(17, 15, 30, 19, 9, State.GameManager.SpriteDictionary.HorseClothing, new ClothingId("base.equines/76119"), false);
            public static IClothing HorseUBottom4 = MakeHorseUBottom(22, 20, 30, 24, 9, State.GameManager.SpriteDictionary.HorseClothing, new ClothingId("base.equines/76124"), false);
            public static IClothing HorseUBottom5 = MakeHorseUBottom(27, 25, 14, 29, 9, State.GameManager.SpriteDictionary.HorseClothing, new ClothingId("base.equines/76129"), true);

            private static IClothing MakeHorseUBottom(int sprF, int sprM, int bulge, int discard, int layer, Sprite[] sheet, ClothingId type, bool black)
            {
                ClothingBuilder builder = ClothingBuilder.New();

                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.DiscardSprite = sheet[discard];
                    output.ClothingId = type;
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

                    output["Clothing1"].Coloring(SwapType.Clothing50Spaced, input.U.ClothingColor);
                    output["Clothing2"].Coloring(SwapType.Clothing50Spaced, input.U.ClothingColor);
                });
                return builder.BuildClothing();
            }
        
            public static IClothing HorseOBottom1Instance = ClothingBuilder.Create(builder =>
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

                    output["Clothing1"].Coloring(SwapType.Clothing50Spaced, input.U.ClothingColor);
                });
            });
        
            public static IClothing HorseOBottom2Instance = ClothingBuilder.Create(builder =>
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

                    output["Clothing1"].Coloring(SwapType.Clothing50Spaced, input.U.ClothingColor);
                    output["Clothing1"].Coloring(SwapType.Clothing50Spaced, input.U.ClothingColor);
                });
            });
        
            public static IClothing HorseOBottom3Instance = ClothingBuilder.Create(builder =>
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

                    output["Clothing1"].Coloring(SwapType.Clothing50Spaced, input.U.ClothingColor);
                });
            });
        }
    }
}