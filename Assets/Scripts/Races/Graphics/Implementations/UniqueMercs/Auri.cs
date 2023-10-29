#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

internal static class Auri
{
    private const float stomachMult = 1.7f;

    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Default<OverSizeParameters>, builder =>
    {
        List<IClothingDataSimple> AllClothing;

        RaceFrameList EarAnimation = new RaceFrameList(new int[3] { 22, 23, 22 }, new float[3] { .2f, .2f, .2f });
        RaceFrameList FaceAnimation = new RaceFrameList(new int[3] { 18, 19, 18 }, new float[3] { .25f, .25f, .25f });

        builder.Setup(output =>
        {
            output.BreastSizes = () => 7;
            output.DickSizes = () => 1;

            output.CanBeGender = new List<Gender> { Gender.Female };

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
            output.BodySizes = 2;
            output.AllowedMainClothingTypes.Clear();
            output.AllowedWaistTypes.Clear();
            output.MouthTypes = 0;
            output.AvoidedMainClothingTypes = 0;
            output.TailTypes = 2;
            output.BodyAccentTypes1 = 2;
            output.ClothingColors = 1;

            List<IClothing<IOverSizeParameters>> AllowedMainClothingTypes = new List<IClothing<IOverSizeParameters>>
            {
                AuriTop.AuriTopInstance
            };

            output.AllowedMainClothingTypes.Clear();

            List<IClothing<IParameters>> AllowedWaistTypes = new List<IClothing<IParameters>>() //Bottoms
            {
                GenericBottom.GenericBottom1,
                GenericBottom.GenericBottom2
            };
            output.AllowedWaistTypes.Clear();

            output.AllowedClothingHatTypes.Clear();

            List<IClothing<IOverSizeParameters>> ExtraMainClothing1Types = new List<IClothing<IOverSizeParameters>>() //Over
            {
                Kimono.Kimono1,
                Kimono.Kimono2,
                KimonoHoliday.KimonoHoliday1,
                KimonoHoliday.KimonoHoliday2
            };
            output.ExtraMainClothing1Types.Set(ExtraMainClothing1Types);

            List<IClothing<IParameters>> ExtraMainClothing2Types = new List<IClothing<IParameters>>() //Stocking
            {
                Stocking.Stocking1
            };
            output.ExtraMainClothing2Types.Clear();

            List<IClothing<IParameters>> ExtraMainClothing3Types = new List<IClothing<IParameters>>() //Hat
            {
                Hat.Hat1
            };
            output.ExtraMainClothing3Types.Clear();

            AllClothing = new List<IClothingDataSimple>();
            AllClothing.AddRange(AllowedMainClothingTypes);
            AllClothing.AddRange(AllowedWaistTypes);
            AllClothing.AddRange(ExtraMainClothing1Types);
            AllClothing.AddRange(ExtraMainClothing2Types);
            AllClothing.AddRange(ExtraMainClothing3Types);
        });


        builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Auri[17]);
                return;
            }

            if (input.Actor.Unit.IsDead && input.Actor.Unit.Items != null) //Second part checks for a not fully initialized unit, so that she doesn't have the dead face when you view her race info
            {
                output.Sprite(input.Sprites.Auri[20]);
                return;
            }

            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            if (State.Rand.Next(1600) == 0)
            {
                input.Actor.AnimationController.frameLists[1].currentlyActive = true;
            }

            if (input.Actor.AnimationController.frameLists[1].currentlyActive == false)
            {
                output.Sprite(input.Sprites.Auri[16]);
                return;
            }

            if (input.Actor.AnimationController.frameLists[1].currentTime >= EarAnimation.times[input.Actor.AnimationController.frameLists[1].currentFrame])
            {
                input.Actor.AnimationController.frameLists[1].currentFrame++;
                input.Actor.AnimationController.frameLists[1].currentTime = 0f;

                if (input.Actor.AnimationController.frameLists[1].currentFrame >= EarAnimation.frames.Length)
                {
                    input.Actor.AnimationController.frameLists[1].currentlyActive = false;
                    input.Actor.AnimationController.frameLists[1].currentTime = 0;
                    input.Actor.AnimationController.frameLists[1].currentFrame = 0;
                }
            }

            output.Sprite(input.Sprites.Auri[FaceAnimation.frames[input.Actor.AnimationController.frameLists[1].currentFrame]]);
        });

        builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Auri[42]);
        });

        builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Auri[43]);
        });

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            int weightMod = input.Actor.Unit.BodySize * 4;
            if (input.Actor.Unit.BodyAccentType1 == 0)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Auri[3 + weightMod]);
                    return;
                }

                output.Sprite(input.Sprites.Auri[0 + weightMod]);
                return;
            }

            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Auri[11 + weightMod]);
                return;
            }

            output.Sprite(input.Sprites.Auri[8 + weightMod]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.BodyAccentType1 == 0)
            {
                return;
            }

            int weightMod = input.Actor.Unit.BodySize * 4;
            if (input.Actor.Unit.BodyAccentType1 == 0)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Auri[27 + weightMod]);
                    return;
                }

                output.Sprite(input.Sprites.Auri[24 + weightMod]);
                return;
            }

            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Auri[35 + weightMod]);
                return;
            }

            output.Sprite(input.Sprites.Auri[32 + weightMod]);
        });

        builder.RenderSingle(SpriteType.BodyAccent2, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.BodyAccentType1 == 0)
            {
                return;
            }

            output.Sprite(input.Sprites.Auri[40 + input.Actor.Unit.BodySize]);
        });

        builder.RenderSingle(SpriteType.BodyAccessory, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            if (State.Rand.Next(650) == 0)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = true;
            }

            if (input.Actor.AnimationController.frameLists[0].currentlyActive == false)
            {
                output.Sprite(input.Sprites.Auri[21]);
                return;
            }


            if (input.Actor.AnimationController.frameLists[0].currentTime >= EarAnimation.times[input.Actor.AnimationController.frameLists[0].currentFrame])
            {
                input.Actor.AnimationController.frameLists[0].currentFrame++;
                input.Actor.AnimationController.frameLists[0].currentTime = 0f;

                if (input.Actor.AnimationController.frameLists[0].currentFrame >= EarAnimation.frames.Length)
                {
                    input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                    input.Actor.AnimationController.frameLists[0].currentTime = 0;
                    input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                }
            }

            output.Sprite(input.Sprites.Auri[EarAnimation.frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
        });

        builder.RenderSingle(SpriteType.SecondaryAccessory, 0, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Auri[44 + input.Actor.Unit.TailType]);
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
                if (leftSize > input.Actor.Unit.DefaultBreastSize)
                {
                    input.Params.Oversize = true;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.AuriVore[31]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.AuriVore[30]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.AuriVore[29]);
                    return;
                }

                if (leftSize > 28)
                {
                    leftSize = 28;
                }


                output.Sprite(input.Sprites.AuriVore[0 + leftSize]);
                return;
            }

            if (input.Actor.Unit.DefaultBreastSize == 0)
            {
                output.Sprite(input.Sprites.AuriVore[0]);
                return;
            }

            if (input.Actor.SquishedBreasts && input.Actor.Unit.BreastSize < 6 && input.Actor.Unit.BreastSize >= 4)
            {
                output.Sprite(input.Sprites.AuriVore[31 + input.Actor.Unit.BreastSize - 3]);
                return;
            }

            output.Sprite(input.Sprites.AuriVore[0 + input.Actor.Unit.BreastSize]);
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
                if (rightSize > input.Actor.Unit.DefaultBreastSize)
                {
                    input.Params.Oversize = true;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.AuriVore[66]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.AuriVore[65]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.AuriVore[64]);
                    return;
                }

                if (rightSize > 28)
                {
                    rightSize = 28;
                }

                output.Sprite(input.Sprites.AuriVore[35 + rightSize]);
                return;
            }

            if (input.Actor.Unit.DefaultBreastSize == 0)
            {
                output.Sprite(input.Sprites.AuriVore[35]);
                return;
            }

            if (input.Actor.SquishedBreasts && input.Actor.Unit.BreastSize < 6 && input.Actor.Unit.BreastSize >= 4)
            {
                output.Sprite(input.Sprites.AuriVore[66 + input.Actor.Unit.BreastSize - 3]);
                return;
            }

            output.Sprite(input.Sprites.AuriVore[35 + input.Actor.Unit.BreastSize]);
        });

        builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(32, stomachMult);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.AuriVore[105]).AddOffset(0, -34 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.AuriVore[104]).AddOffset(0, -34 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.AuriVore[103]).AddOffset(0, -34 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                {
                    output.Sprite(input.Sprites.AuriVore[102]).AddOffset(0, -34 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.AuriVore[101]).AddOffset(0, -33 * .625f);
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
                    output.Sprite(input.Sprites.Auri[105]);
                    return;
                }

                output.Sprite(input.Sprites.AuriVore[70 + size]);
            }
        });

        builder.RenderSingle(SpriteType.Weapon, 13, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Auri[47]);
                return;
            }

            output.Sprite(input.Sprites.Auri[46]);
        });


        builder.RunBefore(Defaults.BasicBellyRunAfter);

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.Name = "Auri";
            unit.SetDefaultBreastSize(4);
            unit.BodySize = 0;
            unit.BodyAccentType1 = 0;
            unit.ClothingExtraType1 = 1;
            unit.TailType = 0;
            if (Config.WinterActive())
            {
                unit.ClothingExtraType1 = 3;
            }
        });
    });

    internal static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(),
            new AnimationController.FrameList()
        };
        actor.AnimationController.frameLists[0].currentlyActive = false;
        actor.AnimationController.frameLists[1].currentlyActive = false;
    }

    private static class AuriTop
    {
        internal static IClothing<IOverSizeParameters> AuriTopInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Auri[64];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1422;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Auri[62]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    input.Actor.SquishedBreasts = true;
                    output["Clothing1"].Sprite(input.Sprites.Auri[56 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Auri[56]);
                }
            });
        });
    }


    private static class GenericBottom
    {
        internal static IClothing GenericBottom1 = MakeGenericBottom(52, 52, 0, 56, 8, State.GameManager.SpriteDictionary.Auri, 840);
        internal static IClothing GenericBottom2 = MakeGenericBottom(101, 101, 0, 101, 8, State.GameManager.SpriteDictionary.Auri, 841);

        private static IClothing MakeGenericBottom(int sprF, int sprM, int bulge, int discard, int layer, Sprite[] sheet, int type)
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
                if (input.Actor.Unit.HasDick)
                {
                    output["Clothing1"].Sprite(sheet[sprM]);
                }
                else
                {
                    output["Clothing1"].Sprite(sheet[sprF + input.Actor.Unit.BodySize]);
                }

                if (input.Actor.Unit.HasDick && bulge > 0)
                {
                    if (input.Actor.Unit.DickSize > 2)
                    {
                        output["Clothing2"].Sprite(sheet[bulge + 1]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(sheet[bulge + 1]);
                    }
                }
                else
                {
                    output["Clothing2"].Sprite(null);
                }
            });
            return builder.BuildClothing();
        }
    }

    private static class Kimono
    {
        internal static IClothing<IOverSizeParameters> Kimono1 = MakeKimono(true);
        internal static IClothing<IOverSizeParameters> Kimono2 = MakeKimono(false);

        internal static IClothing<IOverSizeParameters> MakeKimono(bool Skirt)
        {
            ClothingBuilder<IOverSizeParameters> builder = ClothingBuilder.New<IOverSizeParameters>();

            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsDick = true;
                output.RevealsBreasts = true;
                output.DiscardSprite = input.Sprites.Auri[95];
                output.DiscardUsesPalettes = true;
                output.Type = 444;
                output.OccupiesAllSlots = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(11);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing2"].Layer(20);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(12);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].SetOffset(0, 0 * .625f);
                output["Clothing2"].SetOffset(0, 0 * .625f);
                input.Actor.SquishedBreasts = true;
                if (Skirt)
                {
                    int skirtMod = 0;
                    if (input.Actor.Unit.BodySize > 0 || input.Actor.Unit.BodyAccentType1 == 1)
                    {
                        skirtMod = 26;
                    }

                    if (input.Actor.IsUnbirthing || input.Actor.IsAnalVoring)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Auri[86 + skirtMod]);
                    }
                    else
                    {
                        if (input.Actor.GetStomachSize(32, stomachMult) < 8)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Auri[80 + skirtMod + input.Actor.GetStomachSize(32, stomachMult)]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Auri[88]);
                        }
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                int kimMod = Skirt ? 0 : 7;
                if (input.Params.Oversize)
                {
                    output["Clothing2"].Sprite(input.Sprites.Auri[93 + kimMod]);
                }
                else if (input.Actor.Unit.BreastSize < 3)
                {
                    output["Clothing2"].Sprite(input.Sprites.Auri[89 + kimMod]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Auri[89 + kimMod + input.Actor.Unit.BreastSize - 2]);
                }

                int mod = input.Actor.Unit.BodySize * 4;
                if (mod > 4)
                {
                    mod = 4;
                }

                if (input.Actor.Unit.BodyAccentType1 == 1)
                {
                    mod += 8;
                }

                if (input.Actor.IsAttacking)
                {
                    output["Clothing3"].Sprite(input.Sprites.Auri[67 + mod]);
                }
                else
                {
                    output["Clothing3"].Sprite(input.Sprites.Auri[64 + mod]);
                }
            });
            return builder.BuildClothing();
        }
    }

    private static class KimonoHoliday
    {
        internal static IClothing<IOverSizeParameters> KimonoHoliday1 = MakeKimonoHoliday(true);
        internal static IClothing<IOverSizeParameters> KimonoHoliday2 = MakeKimonoHoliday(false);

        internal static IClothing<IOverSizeParameters> MakeKimonoHoliday(bool Skirt)
        {
            ClothingBuilder<IOverSizeParameters> builder = ClothingBuilder.New<IOverSizeParameters>();

            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsDick = true;
                output.RevealsBreasts = true;
                output.DiscardSprite = input.Sprites.Auri[95];
                output.DiscardUsesPalettes = true;
                output.Type = 444;
                output.ReqWinterHoliday = true;
                output.OccupiesAllSlots = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing5"].Layer(15);
                output["Clothing5"].Coloring(Color.white);
                output["Clothing4"].Layer(0);
                output["Clothing4"].Coloring(Color.white);
                output["Clothing3"].Layer(11);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing2"].Layer(20);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(12);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].SetOffset(0, 0 * .625f);
                output["Clothing2"].SetOffset(0, 0 * .625f);
                input.Actor.SquishedBreasts = true;
                if (Skirt)
                {
                    int skirtMod = 0;
                    if (input.Actor.Unit.BodySize > 0 || input.Actor.Unit.BodyAccentType1 == 1)
                    {
                        skirtMod = 2;
                    }

                    if (input.Actor.IsUnbirthing || input.Actor.IsAnalVoring)
                    {
                        output["Clothing1"].Sprite(input.Sprites.AuriHoliday[23 + skirtMod]);
                    }
                    else
                    {
                        if (input.Actor.GetStomachSize(32, stomachMult) < 4 && input.Actor.Unit.BodyAccentType1 == 0)
                        {
                            output["Clothing1"].Sprite(input.Sprites.AuriHoliday[22 + skirtMod]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.AuriHoliday[26 + skirtMod]);
                        }
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                if (input.Params.Oversize)
                {
                    output["Clothing2"].Sprite(input.Sprites.AuriHoliday[20]);
                }
                else if (input.Actor.Unit.BreastSize < 3)
                {
                    output["Clothing2"].Sprite(input.Sprites.AuriHoliday[16]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.AuriHoliday[16 + input.Actor.Unit.BreastSize - 2]);
                }

                int mod = input.Actor.Unit.BodySize * 4;
                if (mod > 4)
                {
                    mod = 4;
                }

                if (input.Actor.Unit.BodyAccentType1 == 1)
                {
                    mod += 8;
                }

                if (input.Actor.IsAttacking)
                {
                    output["Clothing3"].Sprite(input.Sprites.AuriHoliday[3 + mod]);
                }
                else
                {
                    output["Clothing3"].Sprite(input.Sprites.AuriHoliday[0 + mod]);
                }

                output["Clothing4"].Sprite(input.Sprites.AuriHoliday[21]);
                if (input.Actor.GetStomachSize(32, stomachMult) >= 4)
                {
                    output["Clothing5"].Sprite(input.Sprites.AuriHoliday[32]);
                }
                else
                {
                    output["Clothing5"].Sprite(null);
                }
            });
            return builder.BuildClothing();
        }
    }

    private static class Stocking
    {
        internal static IClothing Stocking1 = MakeStocking(48, 0, 48, 3, State.GameManager.SpriteDictionary.Auri, 901);

        private static IClothing MakeStocking(int sprF, int sprM, int discard, int layer, Sprite[] sheet, int type)
        {
            ClothingBuilder builder = ClothingBuilder.New();


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
                if (input.Actor.Unit.BodyAccentType1 == 1)
                {
                    output["Clothing1"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(sheet[sprF + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing1"].Sprite(sheet[sprM]);
                }
            });
            return builder.BuildClothing();
        }
    }

    private static class Hat
    {
        internal static IClothing Hat1 = MakeHat(50, 0, 50, 20, State.GameManager.SpriteDictionary.Auri, 903);

        private static IClothing MakeHat(int sprF, int sprM, int discard, int layer, Sprite[] sheet, int type)
        {
            ClothingBuilder builder = ClothingBuilder.New();

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
                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(sheet[sprF]);
                }
                else
                {
                    output["Clothing1"].Sprite(sheet[sprM]);
                }
            });
            return builder.BuildClothing();
        }
    }
}