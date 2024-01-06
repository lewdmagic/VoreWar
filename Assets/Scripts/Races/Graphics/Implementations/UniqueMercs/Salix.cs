#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

internal static class Salix
{
    internal static IRaceData Instance = RaceBuilder.CreateV2(Defaults.Default<OverSizeParameters>, builder =>
    {
        builder.Setup(output =>
        {
            output.Names("Salix", "Salix");
            output.RaceTraits(new RaceTraits()
            {
                BodySize = 10,
                StomachSize = 15,
                HasTail = true,
                FavoredStat = Stat.Dexterity,
                AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.BreastVore, VoreType.Anal, VoreType.CockVore },
                ExpMultiplier = 2.4f,
                PowerAdjustment = 5f,
                RaceStats = new RaceStats()
                {
                    Strength = new RaceStats.StatRange(6, 10),
                    Dexterity = new RaceStats.StatRange(10, 15),
                    Endurance = new RaceStats.StatRange(15, 20),
                    Mind = new RaceStats.StatRange(25, 30),
                    Will = new RaceStats.StatRange(20, 25),
                    Agility = new RaceStats.StatRange(24, 26),
                    Voracity = new RaceStats.StatRange(16, 20),
                    Stomach = new RaceStats.StatRange(11, 16),
                },
                RacialTraits = new List<Traits>()
                {
                    Traits.ArcaneMagistrate,
                    Traits.SpellBlade,
                    Traits.ManaAttuned,
                    Traits.ManaRich
                },
                InnateSpells = new List<SpellTypes>()
                    { SpellTypes.AmplifyMagic, SpellTypes.Evocation, SpellTypes.ManaFlux, SpellTypes.UnstableMana},
                RaceDescription = "A demi-mouse mage from a different, mana rich dimension. Has had trouble adapting to the absence of mana here, but makes do.",
            });
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

            List<IClothing<IOverSizeParameters>> allowedMainClothingTypes = new List<IClothing<IOverSizeParameters>>
            {
                SalixTop.SalixTopInstance
            };
            output.AllowedMainClothingTypes.Set(allowedMainClothingTypes);

            List<IClothing<IParameters>> allowedWaistTypes = new List<IClothing<IParameters>>() //Bottoms
            {
                GenericBottom.GenericBottom1
            };
            output.AllowedWaistTypes.Set(allowedWaistTypes);

            output.AllowedClothingHatTypes.Clear();

            List<IClothing<IOverSizeParameters>> extraMainClothing1Types = new List<IClothing<IOverSizeParameters>>() //Over
            {
                Cloak.Cloak1,
                Cloak.Cloak2
            };
            output.ExtraMainClothing1Types.Set(extraMainClothing1Types);

            List<IClothing<IParameters>> extraMainClothing2Types = new List<IClothing<IParameters>>() //Shoes
            {
                SalixShoes.SalixShoesInstance
            };
            output.ExtraMainClothing2Types.Set(extraMainClothing2Types);

            var allClothing = new List<IClothingDataSimple>();
            allClothing.AddRange(allowedMainClothingTypes);
            allClothing.AddRange(allowedWaistTypes);
            allClothing.AddRange(extraMainClothing1Types);
            allClothing.AddRange(extraMainClothing2Types);
        });


        builder.RunBefore((input, output) =>
        {
            CommonRaceCode.MakeBreastOversize(32 * 32).Invoke(input, output);
            Defaults.BasicBellyRunAfter.Invoke(input, output);
        });

        builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.U.BreastSize >= 0)
            {
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Salix[16]);
                    return;
                }

                if (input.U.IsDead && input.U.Items != null) //Second part checks for a not fully initialized unit, so that she doesn't have the dead face when you view her race info
                {
                    output.Sprite(input.Sprites.Salix[17]);
                    return;
                }

                output.Sprite(input.Sprites.Salix[15]);
                return;
            }

            if (input.A.IsAttacking || input.A.IsEating)
            {
                output.Sprite(input.Sprites.Salix[13]);
                return;
            }

            if (input.U.IsDead && input.U.Items != null)
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
            int weightMod = input.U.BodySize * 4;
            if (input.A.IsAttacking)
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


                output.Sprite(input.Sprites.SalixVore[0 + leftSize]);
                return;
            }

            if (input.U.DefaultBreastSize == 0)
            {
                output.Sprite(input.Sprites.SalixVore[0]);
                return;
            }

            if (input.A.SquishedBreasts && input.U.BreastSize < 7 && input.U.BreastSize >= 4)
            {
                output.Sprite(input.Sprites.SalixVore[31 + input.U.BreastSize - 3]);
                return;
            }

            output.Sprite(input.Sprites.SalixVore[0 + input.U.BreastSize]);
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

                output.Sprite(input.Sprites.SalixVore[35 + rightSize]);
                return;
            }

            if (input.U.DefaultBreastSize == 0)
            {
                output.Sprite(input.Sprites.SalixVore[35]);
                return;
            }

            if (input.A.SquishedBreasts && input.U.BreastSize < 7 && input.U.BreastSize >= 4)
            {
                output.Sprite(input.Sprites.SalixVore[66 + input.U.BreastSize - 3]);
                return;
            }

            output.Sprite(input.Sprites.SalixVore[35 + input.U.BreastSize]);
        });

        builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.A.HasBelly)
            {
                int size = input.A.GetStomachSize(32);

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
                    output.Sprite(input.Sprites.SalixVore[106]);
                    return;
                }

                output.Sprite(input.Sprites.SalixVore[70 + size]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.U.HasDick == false)
            {
                return;
            }

            if (input.A.IsErect())
            {
                if (input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32)) < 16)
                {
                    output.Sprite(input.Sprites.SalixGen[1 + input.U.DickSize * 2 + (input.U.BodySize > 1 ? 12 : 0) + (!input.U.HasBreasts ? 24 : 0)]).Layer(20);
                    return;
                }

                output.Sprite(input.Sprites.SalixGen[0 + input.U.DickSize * 2 + (input.U.BodySize > 1 ? 12 : 0) + (!input.U.HasBreasts ? 24 : 0)]).Layer(13);
                return;
            }

            output.Sprite(input.Sprites.SalixGen[0 + input.U.DickSize * 2 + (input.U.BodySize > 1 ? 12 : 0) + (!input.U.HasBreasts ? 24 : 0)]).Layer(11);
        });

        builder.RenderSingle(SpriteType.Balls, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.U.HasDick == false)
            {
                return;
            }

            if (input.A.IsErect() && input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32)) < 16)
            {
                output.Layer(19);
            }
            else
            {
                output.Layer(10);
            }

            int size = input.U.DickSize;
            int offset = input.A.GetBallSize(28, 0.8f);

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
            if (input.A.IsAttacking)
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
        internal static readonly IClothing<IOverSizeParameters> SalixTopInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
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
                else if (input.U.BreastSize < 2)
                {
                    output["Clothing1"].Sprite(input.Sprites.Salix[29]);
                }
                else if (input.U.HasBreasts)
                {
                    input.A.SquishedBreasts = true;
                    output["Clothing1"].Sprite(input.Sprites.Salix[29 + input.U.BreastSize - 1]);
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
        internal static readonly IClothing GenericBottom1 = ClothingBuilder.Create( b =>
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
                output["Clothing1"].Sprite(sheet[sprF + input.U.BodySize]);
            });
        }
    }

    private static class Cloak
    {
        internal static readonly IClothing<IOverSizeParameters> Cloak1 = ClothingBuilder.Create<IOverSizeParameters>( b =>
        {
            MakeCloak(b, true);
        });
        internal static readonly IClothing<IOverSizeParameters> Cloak2 = ClothingBuilder.Create<IOverSizeParameters>( b =>
        {
            MakeCloak(b, false);
        });

        private static void MakeCloak(IClothingBuilder<IOverSizeParameters> builder, bool whole)
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsDick = true;
                output.RevealsBreasts = true;
                output.DiscardSprite = input.Sprites.Salix[65 + (whole ? 1 : 0)];
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
                input.A.SquishedBreasts = true;
                int mod = input.U.BreastSize + (input.U.HasBreasts ? 0 : 1);
                if (whole) // Full cloak sleeves
                {
                    output["Clothing1"].Sprite(input.Params.Oversize ? input.Sprites.Salix[59] : input.Sprites.Salix[52 + mod]); // Cloak Shirt

                    output["Clothing2"].Sprite(input.A.IsAttacking ? input.Sprites.Salix[51] : input.Sprites.Salix[50]);

                    output["Clothing3"].Sprite(input.Sprites.Salix[60 + input.U.BodySize]);
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

                    output["Clothing2"].Sprite(input.A.IsAttacking ? input.Sprites.Salix[39 + sleeveMod] : input.Sprites.Salix[38 + sleeveMod]);

                    output["Clothing3"].Sprite(null);
                }
            });
        }
    }

    private static class SalixShoes
    {
        internal static readonly IClothing SalixShoesInstance = ClothingBuilder.Create(builder =>
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
                output["Clothing1"].Sprite(input.U.BodySize >= 2 ? input.Sprites.Salix[25] : input.Sprites.Salix[24]);
            });
        });
    }
}