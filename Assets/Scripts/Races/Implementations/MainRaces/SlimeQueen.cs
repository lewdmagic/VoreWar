#region

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Races.Graphics.Implementations.MainRaces
{

    #endregion

    internal static class SlimeQueen
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            //////////////////////// SLIMES ///////////////////////////////////////////////////////////////
            //////////////////// TO BE REMOVED LATER //////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////

            builder.RenderSingle(SpriteType.BodyAccessory, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeSub, 3 * input.U.AccessoryColor + input.U.HairColor));
                output.Sprite(input.Sprites.Slimes[18]);
            });

            // TODO I think this isnt used by queen
            /*
        Action<RunAfterData> RunAfter = (data) =>
        {
            if (data.Actor.HasBelly)
            {
                GameObject belly = data.Sprites[SpriteType.Belly].GameObject;
                belly.SetActive(true);

                if (data.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && data.Actor.GetStomachSize(15, 1) == 15)
                {
                    belly.transform.localScale = new Vector3(1, 1, 1);
                }
                else if (data.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    if (data.Actor.GetStomachSize(15, .75f) == 15)
                    {
                        belly.transform.localScale = new Vector3(1, 1, 1);
                    }
                    else if (data.Actor.GetStomachSize(15, .875f) == 15)
                    {
                        belly.transform.localScale = new Vector3(1, 1, 1);
                    }
                }

                if (data.Actor.PredatorComponent.VisibleFullness > 4)
                {
                    float extraCap = data.Actor.PredatorComponent.VisibleFullness - 4;
                    float xScale = Mathf.Min(1 + (extraCap / 5), 1.8f);
                    float yScale = Mathf.Min(1 + (extraCap / 40), 1.1f);
                    belly.transform.localScale = new Vector3(xScale, yScale, 1);
                }
                else
                    belly.transform.localScale = new Vector3(1, 1, 1);
            }

            output.changeSprite(SpriteType.Mouth).Offset(0, 2.5f);
        };
        */

            Action<IRandomCustomInput, IRandomCustomOutput> randomCustom = (data, output) =>
            {
                Defaults.Randomize(data, output);
                IUnitRead unit = data.Unit;

                if (unit.HasDick && unit.HasBreasts)
                {
                    if (Config.HermsOnlyUseFemaleHair)
                    {
                        unit.HairStyle = State.Rand.Next(8);
                    }
                    else
                    {
                        unit.HairStyle = State.Rand.Next(data.SetupOutput.HairStyles);
                    }
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
                        unit.HairStyle = 5 + State.Rand.Next(7);
                    }
                    else
                    {
                        unit.HairStyle = State.Rand.Next(8);
                    }
                }
            };


            ///////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////

            builder.Setup((input, output) =>
            {
                output.Names("SlimeQueen", "SlimeQueens");
                output.AvoidedMainClothingTypes = 1;

                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.SlimeMain);
                output.EyeColors = 1;
                output.HairColors = 3;


                output.BreastSizes = () => 4;
                output.DickSizes = () => 1;

                output.EyeTypes = 1;
                output.HairStyles = 2;
                output.CanBeGender = new List<Gender> { Gender.Female, Gender.Hermaphrodite };
                output.AllowedMainClothingTypes.Set(
                    SlimeWithCrown.SlimeWithCrownInstance
                );

                output.AllowedWaistTypes.Clear();
                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.Clothing);
                output.ExtraColors1 = 2;
                output.ExtraColors2 = 2;
                output.BodySizes = 0;
            });


            builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeMain, input.U.AccessoryColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                output.AddOffset(0, 0);
                int baseSize = 6;
                int ballOffset = input.A.GetBallSize(21);

                int combined = Math.Min(baseSize + ballOffset + 3, 20);
                // Always false
                // if (combined == 21)
                // {
                //     output.AddOffset(0, -14 * .625f);
                // }
                // else if (combined == 20)
                if (combined == 20)
                {
                    output.AddOffset(0, -12 * .625f);
                }
                else if (combined >= 17)
                {
                    output.AddOffset(0, -8 * .625f);
                }

                if (ballOffset > 0)
                {
                    output.Sprite(input.Sprites.Balls[combined]);
                    return;
                }

                output.Sprite(input.Sprites.Balls[baseSize]);
            });

            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeMain, input.U.AccessoryColor));
                output.Sprite(input.Sprites.SlimeQueen[input.A.GetSimpleBodySprite()]);
            });
            builder.RenderSingle(SpriteType.BodyAccent, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeSub, 3 * input.U.AccessoryColor + input.U.HairColor));
                output.Sprite(input.Sprites.SlimeQueen[3 + (input.A.IsAttacking ? 1 : 0)]);
            });
            builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeMain, input.U.AccessoryColor));
                output.Sprite(input.Sprites.SlimeQueen[5 + input.U.BreastSize]);
            });

            builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeMain, input.U.AccessoryColor));
                output.Sprite(input.U.HasDick ? input.Sprites.SlimeQueen[9] : null);
            });

            builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeSub, 3 * input.U.AccessoryColor + input.U.HairColor));
                output.Sprite(input.Sprites.SlimeQueen[10 + input.U.HairStyle]);
            });
            builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeSub, 3 * input.U.AccessoryColor + input.U.HairColor));
                output.Sprite(input.Sprites.SlimeQueen[12]);
            });


            builder.RenderSingle(SpriteType.Eyes, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeMain, input.U.AccessoryColor));
                output.Sprite(input.Sprites.SlimeQueen[27]);
            });


            builder.RenderSingle(SpriteType.Mouth, 4, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Mouth].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Mouth, input.U.SkinColor));
            });


            builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeMain, input.U.AccessoryColor));
                if (input.A.HasBelly)
                {
                    output.Sprite(input.Sprites.Slimes[51 + input.A.GetStomachSize()]);
                }
            });


            builder.RenderSingle(SpriteType.Weapon, 12, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.SlimeQueen[23 + (input.A.IsAttacking ? 1 : 0)]);
            });

            builder.RunBefore((input, output) =>
            {
                output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 8.125f);
                output.ChangeSprite(SpriteType.Belly).AddOffset(0, 2 * .625f);
                output.ChangeSprite(SpriteType.Balls).AddOffset(0, 6 * .625f);
            });


            Action<IRandomCustomInput, IRandomCustomOutput> parentRandomCustom = randomCustom;
            builder.RandomCustom((data, output) =>   
            {
                parentRandomCustom(data, output);
                IUnitRead unit = data.Unit;

                unit.HairStyle = State.Rand.Next(data.SetupOutput.HairStyles);
                unit.ExtraColor1 = 0;
                unit.ExtraColor2 = 0;
            });
        });
    }

    internal static class SlimeWithCrown
    {
        internal static readonly IClothing SlimeWithCrownInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.OccupiesAllSlots = true;
                output.RevealsDick = true;
                output.RevealsBreasts = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing6"].Layer(8);
                output["Clothing5"].Layer(10);
                output["Clothing4"].Layer(12);
                output["Clothing3"].Layer(8);
                output["Clothing2"].Layer(8);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Sprite(input.U.ExtraColor1 == 0 ? null : input.Sprites.SlimeQueen[17 + input.U.BreastSize]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor2));
                output["Clothing2"].Sprite(input.Sprites.SlimeQueen[13]);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing3"].Sprite(input.Sprites.SlimeQueen[14]);
                output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));
                output["Clothing4"].Sprite(input.U.ExtraColor2 == 0 ? null : input.Sprites.SlimeQueen[15]);
                output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));
                output["Clothing5"].Sprite(input.Sprites.SlimeQueen[21 + (input.A.IsAttacking ? 1 : 0)]);
                output["Clothing6"].Sprite(input.Sprites.SlimeQueen[25 + (input.A.IsAttacking ? 1 : 0)]);
                output["Clothing6"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor3));


                if (Config.CockVoreHidesClothes && input.A.PredatorComponent?.BallsFullness > 0)
                {
                    output["Clothing4"].Sprite(null);
                }
            });
        });
    }
}