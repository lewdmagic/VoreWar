#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

internal static class Salix
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Default<OverSizeParameters>, builder =>
    {
        builder.Setup(output =>
        {
            output.BreastSizes = () => 8;
            output.DickSizes = () => 6;
            //CanBeGender = new List<Gender>() { Gender.Male, Gender.Female, Gender.Hermaphrodite};

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
            output.BodySizes = 3;
            output.MouthTypes = 0;
            output.AvoidedMainClothingTypes = 0;
            output.ClothingColors = 1;

            List<IClothing<IOverSizeParameters>> AllowedMainClothingTypes = new List<IClothing<IOverSizeParameters>>
            {
                SalixTop.SalixTopInstance
            };
            output.AllowedMainClothingTypes.Set(AllowedMainClothingTypes);

            List<IClothing<IParameters>> AllowedWaistTypes = new List<IClothing<IParameters>>() //Bottoms
            {
                GenericBottom.GenericBottom1
            };
            output.AllowedWaistTypes.Set(AllowedWaistTypes);

            output.AllowedClothingHatTypes.Clear();

            List<IClothing<IOverSizeParameters>> ExtraMainClothing1Types = new List<IClothing<IOverSizeParameters>>() //Over
            {
                Cloak.Cloak1,
                Cloak.Cloak2
            };
            output.ExtraMainClothing1Types.Set(ExtraMainClothing1Types);

            List<IClothing<IParameters>> ExtraMainClothing2Types = new List<IClothing<IParameters>>() //Shoes
            {
                SalixShoes.SalixShoesInstance
            };
            output.ExtraMainClothing2Types.Set(ExtraMainClothing2Types);

            List<IClothingDataSimple> AllClothing;

            AllClothing = new List<IClothingDataSimple>();
            AllClothing.AddRange(AllowedMainClothingTypes);
            AllClothing.AddRange(AllowedWaistTypes);
            AllClothing.AddRange(ExtraMainClothing1Types);
            AllClothing.AddRange(ExtraMainClothing2Types);
        });


        builder.RunBefore((input, output) =>
        {
            CommonRaceCode.MakeBreastOversize(32 * 32).Invoke(input, output);
            Defaults.BasicBellyRunAfter.Invoke(input, output);
        });

        builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.BreastSize >= 0)
            {
                if (input.Actor.IsAttacking || input.Actor.IsEating)
                {
                    output.Sprite(input.Sprites.Salix[16]);
                    return;
                }

                if (input.Actor.Unit.IsDead && input.Actor.Unit.Items != null) //Second part checks for a not fully initialized unit, so that she doesn't have the dead face when you view her race info
                {
                    output.Sprite(input.Sprites.Salix[17]);
                    return;
                }

                output.Sprite(input.Sprites.Salix[15]);
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Salix[13]);
                return;
            }

            if (input.Actor.Unit.IsDead && input.Actor.Unit.Items != null)
            {
                output.Sprite(input.Sprites.Salix[14]);
                return;
            }

            output.Sprite(input.Sprites.Salix[12]);
        });

        builder.RenderSingle(SpriteType.Hair, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Salix[20]);
        });

        builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Salix[21]);
        });

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            int weightMod = input.Actor.Unit.BodySize * 4;
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Salix[3 + weightMod]);
                return;
            }

            output.Sprite(input.Sprites.Salix[2 + weightMod]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(null);
        });
        builder.RenderSingle(SpriteType.BodyAccent2, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(null);
        });
        builder.RenderSingle(SpriteType.BodyAccessory, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Salix[18]);
        });

        builder.RenderSingle(SpriteType.SecondaryAccessory, 0, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Salix[19]);
        });

        builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.SalixVore[31]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.SalixVore[30]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.SalixVore[29]);
                    return;
                }

                if (leftSize > 28)
                {
                    leftSize = 28;
                }


                output.Sprite(input.Sprites.SalixVore[0 + leftSize]);
                return;
            }

            if (input.Actor.Unit.DefaultBreastSize == 0)
            {
                output.Sprite(input.Sprites.SalixVore[0]);
                return;
            }

            if (input.Actor.SquishedBreasts && input.Actor.Unit.BreastSize < 7 && input.Actor.Unit.BreastSize >= 4)
            {
                output.Sprite(input.Sprites.SalixVore[31 + input.Actor.Unit.BreastSize - 3]);
                return;
            }

            output.Sprite(input.Sprites.SalixVore[0 + input.Actor.Unit.BreastSize]);
        });

        builder.RenderSingle(SpriteType.SecondaryBreasts, 16, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.SalixVore[66]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.SalixVore[65]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.SalixVore[64]);
                    return;
                }

                if (rightSize > 28)
                {
                    rightSize = 28;
                }

                output.Sprite(input.Sprites.SalixVore[35 + rightSize]);
                return;
            }

            if (input.Actor.Unit.DefaultBreastSize == 0)
            {
                output.Sprite(input.Sprites.SalixVore[35]);
                return;
            }

            if (input.Actor.SquishedBreasts && input.Actor.Unit.BreastSize < 7 && input.Actor.Unit.BreastSize >= 4)
            {
                output.Sprite(input.Sprites.SalixVore[66 + input.Actor.Unit.BreastSize - 3]);
                return;
            }

            output.Sprite(input.Sprites.SalixVore[35 + input.Actor.Unit.BreastSize]);
        });

        builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(32);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.SalixVore[105]).AddOffset(0, -34 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.SalixVore[104]).AddOffset(0, -34 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.SalixVore[103]).AddOffset(0, -34 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                {
                    output.Sprite(input.Sprites.SalixVore[102]).AddOffset(0, -34 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.SalixVore[101]).AddOffset(0, -33 * .625f);
                    return;
                }

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

                if (input.Actor.PredatorComponent.OnlyOnePreyAndLiving() && size >= 9 && size <= 14)
                {
                    output.Sprite(input.Sprites.SalixVore[106]);
                    return;
                }

                output.Sprite(input.Sprites.SalixVore[70 + size]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect())
            {
                if (input.Actor.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32)) < 16)
                {
                    output.Sprite(input.Sprites.SalixGen[1 + input.Actor.Unit.DickSize * 2 + (input.Actor.Unit.BodySize > 1 ? 12 : 0) + (!input.Actor.Unit.HasBreasts ? 24 : 0)]).Layer(20);
                    return;
                }

                output.Sprite(input.Sprites.SalixGen[0 + input.Actor.Unit.DickSize * 2 + (input.Actor.Unit.BodySize > 1 ? 12 : 0) + (!input.Actor.Unit.HasBreasts ? 24 : 0)]).Layer(13);
                return;
            }

            output.Sprite(input.Sprites.SalixGen[0 + input.Actor.Unit.DickSize * 2 + (input.Actor.Unit.BodySize > 1 ? 12 : 0) + (!input.Actor.Unit.HasBreasts ? 24 : 0)]).Layer(11);
        });

        builder.RenderSingle(SpriteType.Balls, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect() && input.Actor.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32)) < 16)
            {
                output.Layer(19);
            }
            else
            {
                output.Layer(10);
            }

            int size = input.Actor.Unit.DickSize;
            int offset = input.Actor.GetBallSize(28, 0.8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.SalixGen[83]).AddOffset(0, -22 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.SalixGen[82]).AddOffset(0, -22 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.SalixGen[81]).AddOffset(0, -22 * .625f);
                return;
            }

            if (offset >= 17)
            {
                output.AddOffset(0, -22 * .625f);
            }
            else if (offset == 16)
            {
                output.AddOffset(0, -16 * .625f);
            }
            else if (offset == 15)
            {
                output.AddOffset(0, -13 * .625f);
            }
            else if (offset == 14)
            {
                output.AddOffset(0, -11 * .625f);
            }
            else if (offset == 13)
            {
                output.AddOffset(0, -10 * .625f);
            }
            else if (offset == 12)
            {
                output.AddOffset(0, -7 * .625f);
            }
            else if (offset == 11)
            {
                output.AddOffset(0, -6 * .625f);
            }
            else if (offset == 10)
            {
                output.AddOffset(0, -4 * .625f);
            }
            else if (offset == 9)
            {
                output.AddOffset(0, -1 * .625f);
            }


            if (offset > 0)
            {
                output.Sprite(input.Sprites.SalixGen[Math.Min(62 + offset, 80)]);
                return;
            }

            output.Sprite(input.Sprites.SalixGen[48 + size]);
        });

        builder.RenderSingle(SpriteType.Weapon, 13, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Salix[23]);
                return;
            }

            output.Sprite(input.Sprites.Salix[22]);
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.ClothingType = 1;
            unit.ClothingType2 = 1;

            unit.Name = "Salix";
        });
    });


    private static class SalixTop
    {
        internal static IClothing<IOverSizeParameters> SalixTopInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Salix[64];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1301;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                }
                else if (input.Actor.Unit.BreastSize < 2)
                {
                    output["Clothing1"].Sprite(input.Sprites.Salix[29]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    input.Actor.SquishedBreasts = true;
                    output["Clothing1"].Sprite(input.Sprites.Salix[29 + input.Actor.Unit.BreastSize - 1]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Salix[29]);
                }
            });
        });
    }

    private static class GenericBottom
    {
        internal static IClothing GenericBottom1 = ClothingBuilder.Create( b =>
        {
            MakeGenericBottom(b, 26, 63, 13, State.GameManager.SpriteDictionary.Salix, 1300);
        });

        //internal static IClothingData MakeGenericBottom(int sprF, int sprM, int bulge, int discard, int layer, Sprite[] sheet, int type)
        // sprM and bulge were not used. The values were 26 and 0
        private static void MakeGenericBottom(IClothingBuilder builder, int sprF, int discard, int layer, Sprite[] sheet, int type)
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
                output["Clothing1"].Layer(layer);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(sheet[sprF + input.Actor.Unit.BodySize]);
            });
        }
    }

    private static class Cloak
    {
        internal static IClothing<IOverSizeParameters> Cloak1 = ClothingBuilder.Create<IOverSizeParameters>( b =>
        {
            MakeCloak(b, true);
        });
        internal static IClothing<IOverSizeParameters> Cloak2 = ClothingBuilder.Create<IOverSizeParameters>( b =>
        {
            MakeCloak(b, false);
        });

        private static void MakeCloak(IClothingBuilder<IOverSizeParameters> builder, bool Whole)
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsDick = true;
                output.RevealsBreasts = true;
                output.DiscardSprite = input.Sprites.Salix[65 + (Whole ? 1 : 0)];
                output.DiscardUsesPalettes = false;
                output.Type = 1302;
                output.OccupiesAllSlots = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(20);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing2"].Layer(12);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing3"].Layer(3);
                output["Clothing3"].Coloring(Color.white);

                output["Clothing1"].SetOffset(0, 0 * .625f);
                output["Clothing2"].SetOffset(0, 0 * .625f);
                input.Actor.SquishedBreasts = true;
                int mod = input.Actor.Unit.BreastSize + (input.Actor.Unit.HasBreasts ? 0 : 1);
                if (Whole) // Full cloak sleeves
                {
                    if (input.Params.Oversize) // Cloak Shirt
                    {
                        output["Clothing1"].Sprite(input.Sprites.Salix[59]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Salix[52 + mod]);
                    }

                    if (input.Actor.IsAttacking)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Salix[51]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.Salix[50]);
                    }

                    output["Clothing3"].Sprite(input.Sprites.Salix[60 + input.Actor.Unit.BodySize]);
                }
                else // Shoulderless sleeves
                {
                    output["Clothing1"].Sprite(null);
                    if (input.Params.Oversize)
                    {
                        mod = 8;
                    }

                    int sleeveMod = (mod - 5) * 4;
                    if (0 > sleeveMod)
                    {
                        sleeveMod = 0;
                    }

                    if (input.Actor.IsAttacking)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Salix[39 + sleeveMod]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.Salix[38 + sleeveMod]);
                    }

                    output["Clothing3"].Sprite(null);
                }
            });
        }
    }

    private static class SalixShoes
    {
        internal static IClothing SalixShoesInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 9764;
            });
            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(1);
                output["Clothing1"].Coloring(Color.white);
                if (input.Actor.Unit.BodySize >= 2)
                {
                    output["Clothing1"].Sprite(input.Sprites.Salix[25]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Salix[24]);
                }
            });
        });
    }
}