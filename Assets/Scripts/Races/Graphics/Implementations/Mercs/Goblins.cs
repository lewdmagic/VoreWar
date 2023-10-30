#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

//TODO:
// recolor bulges on clothes
// add color selection
// add wobble to imp belly accent

internal static class Goblins
{
    private static List<IClothingDataSimple> _allClothing;

    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank<OverSizeParameters>, builder =>
    {
        // Havent been implemented
        // Sprite[] SpritesGloves = State.GameManager.SpriteDictionary.Gobbglove;
        // Sprite[] SpritesLegs = State.GameManager.SpriteDictionary.Gobleggo;
        // Sprite[] SpritesUBottoms = State.GameManager.SpriteDictionary.Gobbunderbottoms;
        // Sprite[] SpritesUTops = State.GameManager.SpriteDictionary.Gobundertops;
        // Sprite[] SpritesOBottoms = State.GameManager.SpriteDictionary.Gobboverbottoms;
        // Sprite[] SpritesOTops = State.GameManager.SpriteDictionary.Gobbovertops;
        // Sprite[] SpritesOnePieces = State.GameManager.SpriteDictionary.Gobbunderonepieces;
        // Sprite[] SpritesOverOnePieces = State.GameManager.SpriteDictionary.Gobboveronepieces;


        builder.Setup(output =>
        {
            output.BreastSizes = () => 7;
            output.DickSizes = () => 4;

            output.SpecialAccessoryCount = 0;
            output.ClothingShift = new Vector3(0, 0, 0);
            output.AvoidedEyeTypes = 0;
            output.AvoidedMouthTypes = 0;

            output.HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UniversalHair);
            output.HairStyles = 16;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Goblins);
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.ImpDark);
            output.EyeTypes = 8;
            output.EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);
            output.SecondaryEyeColors = 1;
            output.BodySizes = 2;
            output.AllowedWaistTypes.Clear();
            output.MouthTypes = 3;
            output.AvoidedMainClothingTypes = 0;
            output.BodyAccentTypes1 = 4;
            output.BodyAccentTypes2 = 4;


            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing50Spaced);

            output.ExtendedBreastSprites = true;

            List<IClothing<IOverSizeParameters>> allowedMainClothingTypes = new List<IClothing<IOverSizeParameters>>() //undertops
            {
                GobboLeotard.GobboLeotardInstance,
                GobboCasinoBunny.GobboCasinoBunnyInstance,
                GobboUndertop1.GobboUndertop1Instance,
                GobboUndertop2.GobboUndertop2Instance,
                GobboUndertop3.GobboUndertop3Instance,
                GobboUndertop4.GobboUndertop4Instance,
                GobboUndertop5.GobboUndertop5Instance
            };
            output.AllowedMainClothingTypes.Set(allowedMainClothingTypes); //undertops

            output.AllowedClothingHatTypes.Clear();


            output.AllowedWaistTypes.Set( //underbottoms
                ImpUBottom.ImpUBottom1,
                ImpUBottom.ImpUBottom2,
                ImpUBottom.ImpUBottom3,
                ImpUBottom.ImpUBottom4,
                ImpUBottom.ImpUBottom5
            );

            output.ExtraMainClothing1Types.Set( //Overbottoms
                All.GoblinOBottom1,
                All.GoblinOBottom2,
                All.GoblinOBottom3,
                All.GoblinOBottom4,
                All.GoblinOBottomAlt1,
                All.GoblinOBottom5
            );

            output.ExtraMainClothing2Types.Set( //Special clothing
                GobboOverOpFem.GobboOverOpFemInstance,
                //GobboOverOPM.GobboOverOPMInstance,
                GobboOverTop1.GobboOverTop1Instance,
                GobboOverTop2.GobboOverTop2Instance,
                GobboOverTop3.GobboOverTop3Instance,
                GobboOverTop4.GobboOverTop4Instance
            );

            List<IClothing<IParameters>> extraMainClothing3Types = new List<IClothing<IParameters>>() //Legs
            {
                All.GenericLegs1,
                All.GenericLegs2,
                All.GenericLegs3,
                All.GenericLegsAlt1,
                All.GenericLegs4,
                All.GenericLegsAlt2,
                All.GenericLegs5,
                All.GenericLegs6,
                All.GenericLegs7,
                All.GenericLegs8,
                All.GenericLegs9,
                All.GenericLegs10
            };
            output.ExtraMainClothing3Types.Set(extraMainClothing3Types);

            List<IClothing<IParameters>> extraMainClothing4Types = new List<IClothing<IParameters>>() //Gloves
            {
                All.GenericGloves1,
                All.GenericGlovesPlusSecond1,
                All.GenericGlovesPlusSecondAlt1,
                All.GenericGlovesPlusSecond2,
                All.GenericGlovesPlusSecondAlt2,
                All.GenericGloves2
            };

            output.ExtraMainClothing4Types.Set(extraMainClothing4Types);

            List<IClothing<IParameters>> extraMainClothing5Types = new List<IClothing<IParameters>>() //Hats
            {
                All.GoblinHat1,
                All.GoblinHat2,
                GoblinHolidayHat.HolidayHatInstance
            };
            output.ExtraMainClothing5Types.Set(extraMainClothing5Types);

            _allClothing = new List<IClothingDataSimple>();
            _allClothing.AddRange(allowedMainClothingTypes);
            _allClothing.AddRange(extraMainClothing3Types);
            _allClothing.AddRange(extraMainClothing4Types);
            _allClothing.AddRange(extraMainClothing5Types);
        });


        builder.RunBefore((input, output) =>
        {
            CommonRaceCode.MakeBreastOversize(22 * 22).Invoke(input, output);
            Defaults.Finalize.Invoke(input, output);
        });


        builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, input.Actor.Unit.SkinColor));
            int attackingOffset = input.Actor.IsAttacking ? 1 : 0;
            int eatingOffset = input.Actor.IsEating ? 2 : 0;
            int hurtOffset = input.Actor.Unit.IsDead && input.Actor.Unit.Items != null ? 3 : 0;
            if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Gobbo[8 + attackingOffset + eatingOffset + hurtOffset + 8 * input.Actor.Unit.MouthType]);
            }
            else
            {
                output.Sprite(input.Sprites.Gobbo[12 + attackingOffset + eatingOffset + hurtOffset + 8 * input.Actor.Unit.MouthType]);
            }
        });

        builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.IsDead && input.Actor.Unit.Items != null)
            {
                int sprite = 80;
                sprite += input.Actor.Unit.EyeType;
                output.Sprite(input.Sprites.Gobbo[sprite]);
            }
            else if (input.Actor.Unit.HasBreasts)
            {
                int sprite = 40;
                int attackingOffset = input.Actor.IsAttacking ? 1 : 0;
                if (input.Actor.Unit.EyeType > 8)
                {
                    sprite += 2 * input.Actor.Unit.EyeType;
                    output.Sprite(input.Sprites.Gobbo[sprite - 16 + attackingOffset]);
                }
                else
                {
                    sprite += 2 * input.Actor.Unit.EyeType;
                    output.Sprite(input.Sprites.Gobbo[sprite + attackingOffset]);
                }
            }
            else
            {
                int sprite = 56;
                int attackingOffset = input.Actor.IsAttacking ? 1 : 0;
                if (input.Actor.Unit.EyeType > 8)
                {
                    sprite += 2 * input.Actor.Unit.EyeType;
                    output.Sprite(input.Sprites.Gobbo[sprite - 16 + attackingOffset]);
                }
                else
                {
                    sprite += 2 * input.Actor.Unit.EyeType;
                    output.Sprite(input.Sprites.Gobbo[sprite + attackingOffset]);
                }
            }
        });

        builder.RenderSingle(SpriteType.SecondaryEyes, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            if (input.Actor.Unit.IsDead && input.Actor.Unit.Items != null)
            {
            }
            else
            {
                if (input.Actor.Unit.HasBreasts)
                {
                    int sprite = 72;
                    sprite += input.Actor.Unit.EyeType;
                    output.Sprite(input.Sprites.Gobbo[sprite]);
                }
                else
                {
                    int sprite = 136;
                    sprite += input.Actor.Unit.EyeType;
                    output.Sprite(input.Sprites.Gobbo[sprite]);
                }
            }
        });


        builder.RenderSingle(SpriteType.Hair, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.ClothingExtraType5 == 1)
            {
                output.Sprite(input.Sprites.Gobbohat[2 + 2 * input.Actor.Unit.HairStyle]);
            }
            else if (input.Actor.Unit.ClothingExtraType5 == 2)
            {
                output.Sprite(input.Sprites.Gobbohat[36 + 2 * input.Actor.Unit.HairStyle]);
            }
            else
            {
                output.Sprite(input.Sprites.Gobbo[96 + 2 * input.Actor.Unit.HairStyle]);
            }
        });

        builder.RenderSingle(SpriteType.Hair2, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.ClothingExtraType5 == 1)
            {
                output.Sprite(input.Sprites.Gobbohat[3 + 2 * input.Actor.Unit.HairStyle]);
            }
            else if (input.Actor.Unit.ClothingExtraType5 == 2)
            {
                output.Sprite(input.Sprites.Gobbohat[37 + 2 * input.Actor.Unit.HairStyle]);
            }
            else
            {
                output.Sprite(input.Sprites.Gobbo[97 + 2 * input.Actor.Unit.HairStyle]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, input.Actor.Unit.SkinColor));
            int weightMod = Math.Min(input.Actor.Unit.BodySize * 2, 2);
            if (input.Actor.Unit.HasBreasts)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Gobbo[1 + weightMod]);
                    return;
                }

                output.Sprite(input.Sprites.Gobbo[0 + weightMod]);
            }
            else
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Gobbo[5 + weightMod]);
                    return;
                }

                output.Sprite(input.Sprites.Gobbo[4 + weightMod]);
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Gobbo[32 + input.Actor.Unit.BodyAccentType1]);
        });

        builder.RenderSingle(SpriteType.BodyAccent2, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Gobbo[36 + input.Actor.Unit.BodyAccentType2]);
        });

        builder.RenderSingle(SpriteType.Breasts, 19, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(22 * 22));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 22)
                {
                    output.Sprite(input.Sprites.Gobbovore[21]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 22)
                {
                    output.Sprite(input.Sprites.Gobbovore[20]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 20)
                {
                    output.Sprite(input.Sprites.Gobbovore[19]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 18)
                {
                    output.Sprite(input.Sprites.Gobbovore[18]);
                    return;
                }

                if (leftSize > 17)
                {
                    leftSize = 17;
                }


                output.Sprite(input.Sprites.Gobbovore[leftSize]);
            }
            else
            {
                if (input.Actor.Unit.DefaultBreastSize == 0)
                {
                    output.Sprite(input.Sprites.Gobbovore[0]);
                    return;
                }

                output.Sprite(input.Sprites.Gobbovore[0 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.SecondaryBreasts, 19, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(22 * 22));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 22)
                {
                    output.Sprite(input.Sprites.Gobbovore[43]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 22)
                {
                    output.Sprite(input.Sprites.Gobbovore[42]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 20)
                {
                    output.Sprite(input.Sprites.Gobbovore[41]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 18)
                {
                    output.Sprite(input.Sprites.Gobbovore[40]);
                    return;
                }

                if (rightSize > 17)
                {
                    rightSize = 17;
                }

                output.Sprite(input.Sprites.Gobbovore[22 + rightSize]);
            }
            else
            {
                if (input.Actor.Unit.DefaultBreastSize == 0)
                {
                    output.Sprite(input.Sprites.Gobbovore[22]);
                    return;
                }

                output.Sprite(input.Sprites.Gobbovore[22 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 16, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(32, 1.2f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.Gobbovore[105]).AddOffset(0, -23 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.Gobbovore[104]).AddOffset(0, -22 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size >= 30)
                {
                    output.Sprite(input.Sprites.Gobbovore[103]).AddOffset(0, -22 * .625f);
                    return;
                }

                switch (size)
                {
                    case 24:
                        output.AddOffset(0, 0 * .625f);
                        break;
                    case 25:
                        output.AddOffset(0, 0 * .625f);
                        break;
                    case 26:
                        output.AddOffset(0, -1 * .625f);
                        break;
                    case 27:
                        output.AddOffset(0, -5 * .625f);
                        break;
                    case 28:
                        output.AddOffset(0, -8 * .625f);
                        break;
                    case 29:
                        output.AddOffset(0, -15 * .625f);
                        break;
                    case 30:
                        output.AddOffset(0, -17 * .625f);
                        break;
                    case 31:
                        output.AddOffset(0, -19 * .625f);
                        break;
                    case 32:
                        output.AddOffset(0, -22 * .625f);
                        break;
                }

                //if (input.Actor.PredatorComponent.OnlyOnePreyAndLiving() && size >= 8 && size <= 13)
                //    return Out.Update(State.GameManager.SpriteDictionary.Gobbovore[71]);

                output.Sprite(input.Sprites.Gobbovore[70 + size]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 14, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect())
            {
                if (input.Actor.PredatorComponent?.VisibleFullness < .75f)
                {
                    output.Sprite(input.Sprites.Gobbo[129 + 2 * input.Actor.Unit.DickSize]).Layer(21);
                    return;
                }

                output.Sprite(input.Sprites.Gobbo[129 + 2 * input.Actor.Unit.DickSize]).Layer(14);
                return;
            }

            output.Sprite(input.Sprites.Gobbo[128 + 2 * input.Actor.Unit.DickSize]).Layer(14);
        });

        builder.RenderSingle(SpriteType.Balls, 13, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            int size = input.Actor.GetBallSize(22, .8f);
            int baseSize = (input.Actor.Unit.DickSize + 1) / 3;

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && size == 22)
            {
                output.Sprite(input.Sprites.Gobbovore[69]).AddOffset(0, -19 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 22)
            {
                output.Sprite(input.Sprites.Gobbovore[68]).AddOffset(0, -17 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 20)
            {
                output.Sprite(input.Sprites.Gobbovore[67]).AddOffset(0, -14 * .625f);
                return;
            }

            int combined = Math.Min(baseSize + size + 2, 22);
            if (combined == 22)
            {
                output.AddOffset(0, -2 * .625f);
            }
            else if (combined >= 21)
            {
                output.AddOffset(0, -2 * .625f);
            }
            else if (combined >= 20)
            {
                output.AddOffset(0, -1 * .625f);
            }
            else if (combined >= 19)
            {
                output.AddOffset(0, 1 * .625f);
            }
            else if (combined >= 18)
            {
                output.AddOffset(0, 0 * .625f);
            }

            if (size > 0)
            {
                output.Sprite(input.Sprites.Gobbovore[44 + combined]);
                return;
            }

            output.Sprite(input.Sprites.Gobbovore[44 + baseSize]);
        });

        builder.RenderSingle(SpriteType.Weapon, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                if (input.Actor.GetWeaponSprite() == 5)
                {
                    return;
                }

                output.Sprite(input.Sprites.Gobbo[88 + input.Actor.GetWeaponSprite()]);
            }
        });


        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.SkinColor = State.Rand.Next(data.MiscRaceData.SkinColors);
            unit.EyeType = State.Rand.Next(data.MiscRaceData.EyeTypes);
            unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
            unit.HairColor = State.Rand.Next(data.MiscRaceData.HairColors);
            unit.BodyAccentType1 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes1);
            unit.BodyAccentType2 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes2);
            unit.BodyAccentType3 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes3);
            unit.BodyAccentType4 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes4);
            unit.ClothingColor = State.Rand.Next(data.MiscRaceData.ClothingColors);
            unit.ClothingColor2 = State.Rand.Next(data.MiscRaceData.ClothingColors);
        });
    });

    // protected override Color HairColor(Actor_Unit actor) => Color.white;


    private static class All
    {
        internal static readonly IClothing GoblinOBottom1 = ClothingBuilder.Create( b =>
        {
            GoblinOBottom.MakeImpOBottom(b, 0, 2, false, 45, 8, 14, State.GameManager.SpriteDictionary.Gobboverbottoms, 12050);
        });
        internal static readonly IClothing GoblinOBottom2 = ClothingBuilder.Create( b =>
        {
            GoblinOBottom.MakeImpOBottom(b, 9, 11, false, 45, 17, 14, State.GameManager.SpriteDictionary.Gobboverbottoms, 12051);
        });
        internal static readonly IClothing GoblinOBottom3 = ClothingBuilder.Create( b =>
        {
            GoblinOBottom.MakeImpOBottom(b, 18, 20, true, 45, 26, 14, State.GameManager.SpriteDictionary.Gobboverbottoms, 12052);
        });
        internal static readonly IClothing GoblinOBottom4 = ClothingBuilder.Create( b =>
        {
            GoblinOBottom.MakeImpOBottom(b, 27, 29, true, 49, 35, 14, State.GameManager.SpriteDictionary.Gobboverbottoms, 12053);
        });
        internal static readonly IClothing GoblinOBottomAlt1 = ClothingBuilder.Create( b =>
        {
            GoblinOBottomAlt.MakeImpOBottomAlt(b, 27, 29, true, 49, 35, 14, State.GameManager.SpriteDictionary.Gobboverbottoms, 12053);
        });
        internal static readonly IClothing GoblinOBottom5 = ClothingBuilder.Create( b =>
        {
            GoblinOBottom.MakeImpOBottom(b, 36, 38, false, 45, 44, 14, State.GameManager.SpriteDictionary.Gobboverbottoms, 12054);
        });

        internal static readonly IClothing GenericLegs1 = ClothingBuilder.Create( b =>
        {
            GenericLegs.MakeGenericLegs(b, 0, 4, 45, 12055, femaleOnly: true, blocksDick: false);
        });
        internal static readonly IClothing GenericLegs2 = ClothingBuilder.Create( b =>
        {
            GenericLegs.MakeGenericLegs(b, 5, 9, 45, 12056, femaleOnly: true, blocksDick: false);
        });
        internal static readonly IClothing GenericLegs3 = ClothingBuilder.Create( b =>
        {
            GenericLegs.MakeGenericLegs(b, 10, 14, 45, 12057, femaleOnly: true, blocksDick: false);
        });
        internal static readonly IClothing GenericLegsAlt1 = ClothingBuilder.Create( b =>
        {
            GenericLegsAlt.MakeGenericLegsAlt(b, 10, 14, 45, 12057, femaleOnly: true, blocksDick: false);
        });
        internal static readonly IClothing GenericLegs4 = ClothingBuilder.Create( b =>
        {
            GenericLegs.MakeGenericLegs(b, 15, 19, 45, 12058, femaleOnly: true, blocksDick: false);
        });
        internal static readonly IClothing GenericLegsAlt2 = ClothingBuilder.Create( b =>
        {
            GenericLegsAlt.MakeGenericLegsAlt(b, 15, 19, 45, 12058, femaleOnly: true, blocksDick: false);
        });
        internal static readonly IClothing GenericLegs5 = ClothingBuilder.Create( b =>
        {
            GenericLegs.MakeGenericLegs(b, 20, 24, 45, 12059, femaleOnly: true, blocksDick: false);
        });
        internal static readonly IClothing GenericLegs6 = ClothingBuilder.Create( b =>
        {
            GenericLegs.MakeGenericLegs(b, 2, 4, 45, 12060, true, blocksDick: true, black: true);
        });
        internal static readonly IClothing GenericLegs7 = ClothingBuilder.Create( b =>
        {
            GenericLegs.MakeGenericLegs(b, 7, 9, 45, 12061, true, blocksDick: true, black: true);
        });
        internal static readonly IClothing GenericLegs8 = ClothingBuilder.Create( b =>
        {
            GenericLegs.MakeGenericLegs(b, 12, 14, 45, 12062, true, blocksDick: true);
        });
        internal static readonly IClothing GenericLegs9 = ClothingBuilder.Create( b =>
        {
            GenericLegs.MakeGenericLegs(b, 17, 19, 45, 12063, true, blocksDick: true);
        });
        internal static readonly IClothing GenericLegs10 = ClothingBuilder.Create( b =>
        {
            GenericLegs.MakeGenericLegs(b, 22, 24, 45, 12064, true, blocksDick: false);
        });

        internal static readonly IClothing GenericGloves1 = ClothingBuilder.Create( b =>
        {
            GenericGloves.MakeGenericGloves(b, 0, 8, 12065);
        });
        internal static readonly IClothing GenericGlovesPlusSecond1 = ClothingBuilder.Create( b =>
        {
            GenericGlovesPlusSecond.MakeGenericGlovesPlusSecond(b, 9, 17, 12066);
        });
        internal static readonly IClothing GenericGlovesPlusSecondAlt1 = ClothingBuilder.Create( b =>
        {
            GenericGlovesPlusSecondAlt.MakeGenericGlovesPlusSecondAlt(b, 9, 17, 12066);
        });
        internal static readonly IClothing GenericGlovesPlusSecond2 = ClothingBuilder.Create( b =>
        {
            GenericGlovesPlusSecond.MakeGenericGlovesPlusSecond(b, 18, 26, 12067);
        });
        internal static readonly IClothing GenericGlovesPlusSecondAlt2 = ClothingBuilder.Create( b =>
        {
            GenericGlovesPlusSecondAlt.MakeGenericGlovesPlusSecondAlt(b, 18, 26, 12067);
        });
        internal static readonly IClothing GenericGloves2 = ClothingBuilder.Create( b =>
        {
            GenericGloves.MakeGenericGloves(b, 27, 35, 12068);
        });

        internal static readonly IClothing GoblinHat1 = ClothingBuilder.Create( b =>
        {
            GoblinHat.MakeHat(b, 0, 0, State.GameManager.SpriteDictionary.Gobbohat, 12069);
        });
        internal static readonly IClothing GoblinHat2 = ClothingBuilder.Create( b =>
        {
            GoblinHat.MakeHat(b, 34, 0, State.GameManager.SpriteDictionary.Gobbohat, 12070);
        });
    }


    private static class GenericGloves
    {
        internal static void MakeGenericGloves(IClothingBuilder builder, int start, int discard, int type)
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardSprite = input.Sprites.Gobbglove[discard];
                output.Type = type;
                output.FixedColor = false;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(5);
                if (input.Actor.Unit.HasBreasts)
                {
                    int weightMod = input.Actor.Unit.BodySize * 2;
                    output["Clothing1"].Sprite(input.Actor.IsAttacking ? input.Sprites.Gobbglove[start + 1 + weightMod] : input.Sprites.Gobbglove[start + weightMod]);
                }
                else
                {
                    int weightMod = input.Actor.Unit.BodySize * 2;
                    output["Clothing1"].Sprite(input.Actor.IsAttacking ? input.Sprites.Gobbglove[start + 5 + weightMod] : input.Sprites.Gobbglove[start + 4 + weightMod]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        }
    }

    private static class GenericGlovesPlusSecond
    {
        internal static void MakeGenericGlovesPlusSecond(IClothingBuilder builder, int start, int discard, int type)
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardSprite = input.Sprites.Gobbglove[discard];
                output.Type = type;
                output.FixedColor = false;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(5);
                if (input.Actor.Unit.HasBreasts)
                {
                    int weightMod = input.Actor.Unit.BodySize * 2;
                    output["Clothing1"].Sprite(input.Actor.IsAttacking ? input.Sprites.Gobbglove[start + 1 + weightMod] : input.Sprites.Gobbglove[start + weightMod]);
                }
                else
                {
                    int weightMod = input.Actor.Unit.BodySize * 2;
                    output["Clothing1"].Sprite(input.Actor.IsAttacking ? input.Sprites.Gobbglove[start + 5 + weightMod] : input.Sprites.Gobbglove[start + 4 + weightMod]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        }
    }

    private static class GenericGlovesPlusSecondAlt
    {
        internal static void MakeGenericGlovesPlusSecondAlt(IClothingBuilder builder, int start, int discard, int type)
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardSprite = input.Sprites.Gobbglove[discard];
                output.Type = type;
                //output.ClothingDefaults1 = SpriteExtraInfo.MakeSpriteExtraInfo(5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.ClothingColor), null); // seems to be overwritten always
                output.FixedColor = false;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(5);
                if (input.Actor.Unit.HasBreasts)
                {
                    int weightMod = input.Actor.Unit.BodySize * 2;
                    output["Clothing1"].Sprite(input.Actor.IsAttacking ? input.Sprites.Gobbglove[start + 1 + weightMod] : input.Sprites.Gobbglove[start + weightMod]);
                }
                else
                {
                    int weightMod = input.Actor.Unit.BodySize * 2;
                    output["Clothing1"].Sprite(input.Actor.IsAttacking ? input.Sprites.Gobbglove[start + 5 + weightMod] : input.Sprites.Gobbglove[start + 4 + weightMod]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
            });
        }
    }

    private static class GenericLegs
    {
        internal static void MakeGenericLegs(IClothingBuilder builder, int start, int discard, int bulge, int type, bool maleOnly = false, bool femaleOnly = false, bool blocksDick = false, bool black = false)
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = !blocksDick;
                output.Type = type;
                output.DiscardSprite = input.Sprites.Gobleggo[discard];
                if (maleOnly)
                {
                    output.MaleOnly = true;
                }

                if (femaleOnly)
                {
                    output.FemaleOnly = true;
                }

                output.FixedColor = true; // TODO probably a bug 
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(11);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(10);
                output["Clothing1"].Coloring(Color.white);
                int weightMod = input.Actor.Unit.BodySize;
                output["Clothing1"].Sprite(input.Sprites.Gobleggo[start + weightMod]);
                if (input.Actor.Unit.HasDick)
                {
                    if (blocksDick)
                    {
                        output["Clothing2"].Sprite(black ? input.Sprites.Gobbunderbottoms[bulge + 4 + input.Actor.Unit.DickSize] : input.Sprites.Gobbunderbottoms[bulge + input.Actor.Unit.DickSize]);

                        if (input.Actor.Unit.GetGender() == Gender.Hermaphrodite)
                        {
                            output["Clothing2"].SetOffset(0, 2 * .625f);
                        }
                        else
                        {
                            output["Clothing2"].SetOffset(0, 0);
                        }
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }
                }
                else
                {
                    output["Clothing2"].Sprite(null);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
        }
    }

    private static class GenericLegsAlt
    {
        internal static void MakeGenericLegsAlt(IClothingBuilder builder, int start, int discard, int bulge, int type, bool maleOnly = false, bool femaleOnly = false, bool blocksDick = false, bool black = false)
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = !blocksDick;
                output.Type = type;
                output.DiscardSprite = input.Sprites.Gobleggo[discard];
                if (maleOnly)
                {
                    output.MaleOnly = true;
                }

                if (femaleOnly)
                {
                    output.FemaleOnly = true;
                }

                output.FixedColor = true; // Probably a bug
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(11);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(10);
                output["Clothing1"].Coloring(Color.white);
                int weightMod = input.Actor.Unit.BodySize;
                output["Clothing1"].Sprite(input.Sprites.Gobleggo[start + weightMod]);
                if (input.Actor.Unit.HasDick)
                {
                    if (blocksDick)
                    {
                        output["Clothing2"].Sprite(black ? input.Sprites.Gobbunderbottoms[bulge + 4 + input.Actor.Unit.DickSize] : input.Sprites.Gobbunderbottoms[bulge + input.Actor.Unit.DickSize]);

                        if (input.Actor.Unit.GetGender() == Gender.Hermaphrodite)
                        {
                            output["Clothing2"].SetOffset(0, 2 * .625f);
                        }
                        else
                        {
                            output["Clothing2"].SetOffset(0, 0);
                        }
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }
                }
                else
                {
                    output["Clothing2"].Sprite(null);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
            });
        }
    }

    private static class ImpUBottom
    {
        internal static readonly IClothing ImpUBottom1 = ClothingBuilder.Create( b =>
        {
            MakeImpUBottom(b, 0, 2, 45, 8, 9, State.GameManager.SpriteDictionary.Gobbunderbottoms, 12045);
        });
        internal static readonly IClothing ImpUBottom2 = ClothingBuilder.Create( b =>
        {
            MakeImpUBottom(b, 9, 11, 45, 17, 9, State.GameManager.SpriteDictionary.Gobbunderbottoms, 12046);
        });
        internal static readonly IClothing ImpUBottom3 = ClothingBuilder.Create( b =>
        {
            MakeImpUBottom(b, 18, 20, 45, 26, 9, State.GameManager.SpriteDictionary.Gobbunderbottoms, 12047, true);
        });
        internal static readonly IClothing ImpUBottom4 = ClothingBuilder.Create( b =>
        {
            MakeImpUBottom(b, 27, 29, 45, 35, 9, State.GameManager.SpriteDictionary.Gobbunderbottoms, 12048);
        });
        internal static readonly IClothing ImpUBottom5 = ClothingBuilder.Create( b =>
        {
            MakeImpUBottom(b, 36, 38, 45, 44, 9, State.GameManager.SpriteDictionary.Gobbunderbottoms, 12049);
        });

        private static void MakeImpUBottom(IClothingBuilder builder, int sprF, int sprM, int bulge, int discard, int layer, Sprite[] sheet, int type, bool black = false)
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.DiscardSprite = sheet[discard];
                output.Type = type;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(layer + 1);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(layer);
                output["Clothing1"].Coloring(Color.white);
                int weightMod = input.Actor.Unit.BodySize;
                if (input.Actor.HasBelly)
                {
                    output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? sheet[sprF + 4 + weightMod] : sheet[sprM + 4 + weightMod]);

                    if (input.Actor.Unit.HasDick)
                    {
                        //if (output.BlocksDick == true)
                        if (true) // This was always true, pretty sure
                        {
                            output["Clothing2"].Sprite(black ? input.Sprites.Gobbunderbottoms[bulge + 4 + input.Actor.Unit.DickSize] : input.Sprites.Gobbunderbottoms[bulge + input.Actor.Unit.DickSize]);

                            if (input.Actor.Unit.GetGender() == Gender.Hermaphrodite)
                            {
                                output["Clothing2"].SetOffset(0, 2 * .625f);
                            }
                            else
                            {
                                output["Clothing2"].SetOffset(0, 0);
                            }
                        }
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? sheet[sprF + weightMod] : sheet[sprM + weightMod]);

                    if (input.Actor.Unit.HasDick)
                    {
                        //if (output.BlocksDick == true)
                        if (true) // This was always true, pretty sure
                        {
                            output["Clothing2"].Sprite(black ? input.Sprites.Gobbunderbottoms[bulge + 4 + input.Actor.Unit.DickSize] : input.Sprites.Gobbunderbottoms[bulge + input.Actor.Unit.DickSize]);

                            if (input.Actor.Unit.GetGender() == Gender.Hermaphrodite)
                            {
                                output["Clothing2"].SetOffset(0, 2 * .625f);
                            }
                            else
                            {
                                output["Clothing2"].SetOffset(0, 0);
                            }
                        }
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
            });
        }
    }

    private static class GobboLeotard
    {
        internal static readonly IClothing GobboLeotardInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Gobbunderonepieces[74];
                output.Type = 12071;
                output.OccupiesAllSlots = true;
                output.DiscardUsesPalettes = true;
            });


            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(13);
                output["Clothing2"].Layer(20);
                output["Clothing1"].Layer(17);
                int weightMod = input.Actor.Unit.BodySize;
                int bobs = input.Actor.Unit.BreastSize;
                if (bobs > 7)
                {
                    bobs = 7;
                }

                int size = input.Actor.GetStomachSize(32, 1.2f);
                if (size > 7)
                {
                    size = 7;
                }

                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.Unit.HasDick)
                    {
                        output["Clothing3"].Sprite(input.Sprites.Gobbunderbottoms[45 + input.Actor.Unit.DickSize]);
                        if (input.Actor.Unit.GetGender() == Gender.Hermaphrodite)
                        {
                            output["Clothing3"].SetOffset(0, 2 * .625f);
                        }
                        else
                        {
                            output["Clothing3"].SetOffset(0, 0);
                        }
                    }

                    output["Clothing2"].Sprite(bobs == 7 ? null : input.Sprites.Gobbunderonepieces[33 + bobs]);

                    output["Clothing1"].Sprite(input.Sprites.Gobbunderonepieces[41 + size + 8 * weightMod]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced,
                        input.Actor.Unit.ClothingColor2));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced,
                        input.Actor.Unit.ClothingColor2));
                    output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced,
                        input.Actor.Unit.ClothingColor2));
                    output.ChangeSprite(SpriteType.Belly).Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, input.Actor.Unit.SkinColor));
                }
                else
                {
                    output["Clothing1"].Layer(13);
                    output["Clothing2"].Layer(20);
                    output["Clothing3"].Layer(16);

                    output["Clothing3"].Sprite(input.Sprites.Gobbunderbottoms[45 + input.Actor.Unit.DickSize]);

                    if (input.Actor.Unit.GetGender() == Gender.Hermaphrodite)
                    {
                        output["Clothing3"].SetOffset(0, 2 * .625f);
                    }
                    else
                    {
                        output["Clothing3"].SetOffset(0, 0);
                    }

                    output["Clothing2"].Sprite(null);
                    output["Clothing1"].Sprite(input.Sprites.Gobbunderonepieces[57 + size + 9 * weightMod]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
                    output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
                    output.ChangeSprite(SpriteType.Belly).Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SkinToClothing, input.Actor.Unit.ClothingColor));
                }
            });
        });
    }
}

internal static class GobboCasinoBunny
{
    internal static readonly IClothing GobboCasinoBunnyInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.Gobbunderonepieces[74];
            output.Type = 12072;
            output.OccupiesAllSlots = true;
            output.DiscardUsesPalettes = true;
        });


        builder.RenderAll((input, output) =>
        {
            output["Clothing3"].Layer(13);
            output["Clothing2"].Layer(20);
            output["Clothing1"].Layer(17);
            int weightMod = input.Actor.Unit.BodySize;
            int bobs = input.Actor.Unit.BreastSize;
            if (bobs > 7)
            {
                bobs = 7;
            }

            int size = input.Actor.GetStomachSize(32, 1.2f);
            if (size > 5)
            {
                size = 5;
            }

            if (input.Actor.Unit.HasBreasts)
            {
                if (input.Actor.Unit.HasDick)
                {
                    output["Clothing3"].Sprite(input.Sprites.Gobbunderbottoms[45 + input.Actor.Unit.DickSize]);
                    if (input.Actor.Unit.GetGender() == Gender.Hermaphrodite)
                    {
                        output["Clothing3"].SetOffset(0, 2 * .625f);
                    }
                    else
                    {
                        output["Clothing3"].SetOffset(0, 0);
                    }
                }
                else
                {
                    output["Clothing3"].Sprite(null);
                }


                output["Clothing2"].Sprite(input.Sprites.Gobbunderonepieces[0 + bobs]);
                output["Clothing1"].Sprite(input.Sprites.Gobbunderonepieces[8 + size + 6 * weightMod]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced,
                    input.Actor.Unit.ClothingColor2));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced,
                    input.Actor.Unit.ClothingColor2));
                output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced,
                    input.Actor.Unit.ClothingColor2));
                output.ChangeSprite(SpriteType.Belly).Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, input.Actor.Unit.SkinColor));
            }
            else
            {
                output["Clothing1"].Layer(13);
                output["Clothing2"].Layer(20);
                output["Clothing3"].Layer(16);
                output["Clothing3"].Sprite(input.Sprites.Gobbunderbottoms[45 + input.Actor.Unit.DickSize]);
                if (input.Actor.Unit.GetGender() == Gender.Hermaphrodite)
                {
                    output["Clothing3"].SetOffset(0, 2 * .625f);
                }
                else
                {
                    output["Clothing3"].SetOffset(0, 0);
                }

                output["Clothing2"].Sprite(null);
                output["Clothing1"].Sprite(input.Sprites.Gobbunderonepieces[20 + size + 6 * weightMod]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
                output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
                output.ChangeSprite(SpriteType.Belly).Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SkinToClothing, input.Actor.Unit.ClothingColor));
            }
        });
    });
}


internal static class GoblinOBottom
{
    internal static void MakeImpOBottom(IClothingBuilder builder, int sprF, int sprM, bool showbulge, int bulge, int discard, int layer,
        Sprite[] sheet, int type)
    {
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
            int weightMod = input.Actor.Unit.BodySize;
            if (input.Actor.HasBelly)
            {
                output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? sheet[sprF + 4 + weightMod] : sheet[sprM + 4 + weightMod]);

                if (input.Actor.Unit.HasDick && showbulge)
                {
                    //if (output.BlocksDick == true)
                    if (true) // This was always true, pretty sure
                    {
                        output["Clothing2"].Sprite(input.Sprites.Gobbunderbottoms[bulge + input.Actor.Unit.DickSize]);
                    }

                    if (input.Actor.Unit.GetGender() == Gender.Hermaphrodite)
                    {
                        output["Clothing2"].SetOffset(0, 2 * .625f);
                    }
                    else
                    {
                        output["Clothing2"].SetOffset(0, 0);
                    }
                }
                else
                {
                    output["Clothing2"].Sprite(null);
                }
            }
            else
            {
                output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? sheet[sprF + weightMod] : sheet[sprM + weightMod]);

                if (input.Actor.Unit.HasDick)
                {
                    //if (output.BlocksDick == true && showbulge == true)
                    output["Clothing2"].Sprite(showbulge ? input.Sprites.Gobbunderbottoms[bulge + input.Actor.Unit.DickSize] : null); // first condition always true, pretty sure

                    if (input.Actor.Unit.GetGender() == Gender.Hermaphrodite)
                    {
                        output["Clothing2"].SetOffset(0, 2 * .625f);
                    }
                    else
                    {
                        output["Clothing2"].SetOffset(0, 0);
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
    }
}


internal static class GoblinOBottomAlt
{
    internal static void MakeImpOBottomAlt(IClothingBuilder builder, int sprF, int sprM, bool showbulge, int bulge, int discard,
        int layer, Sprite[] sheet, int type)
    {
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
            int weightMod = input.Actor.Unit.BodySize;
            if (input.Actor.HasBelly)
            {
                output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? sheet[sprF + 4 + weightMod] : sheet[sprM + 4 + weightMod]);

                if (input.Actor.Unit.HasDick && showbulge)
                {
                    //if (output.BlocksDick == true)
                    if (true) // This was always true, pretty sure
                    {
                        output["Clothing2"].Sprite(input.Sprites.Gobbunderbottoms[bulge + input.Actor.Unit.DickSize]);
                    }

                    if (input.Actor.Unit.GetGender() == Gender.Hermaphrodite)
                    {
                        output["Clothing2"].SetOffset(0, 2 * .625f);
                    }
                    else
                    {
                        output["Clothing2"].SetOffset(0, 0);
                    }
                }
                else
                {
                    output["Clothing2"].Sprite(null);
                }
            }
            else
            {
                output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? sheet[sprF + weightMod] : sheet[sprM + weightMod]);

                if (input.Actor.Unit.HasDick)
                {
                    //if (output.BlocksDick == true && showbulge == true)
                    if (showbulge) // first condition always true, pretty sure
                    {
                        output["Clothing2"].Sprite(input.Sprites.Gobbunderbottoms[bulge + input.Actor.Unit.DickSize]);
                    }

                    if (input.Actor.Unit.GetGender() == Gender.Hermaphrodite)
                    {
                        output["Clothing2"].SetOffset(0, 2 * .625f);
                    }
                    else
                    {
                        output["Clothing2"].SetOffset(0, 0);
                    }
                }
            }

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
        });
    }
}

internal static class GoblinHat
{
    internal static void MakeHat(IClothingBuilder builder, int start, int discard, Sprite[] sheet, int type)
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
            output["Clothing2"].Layer(2);
            output["Clothing1"].Layer(22);
            output["Clothing1"].Sprite(sheet[start]);
            output["Clothing2"].Sprite(sheet[start + 1]);

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
        });
    }
}

internal static class GobboUndertop1
{
    internal static readonly IClothing<IOverSizeParameters> GobboUndertop1Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.Gobundertops[8];
            output.Type = 12073;
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
                output["Clothing1"].Sprite(input.Sprites.Gobundertops[7]);
            }
            else if (input.Actor.Unit.HasBreasts)
            {
                output["Clothing1"].Sprite(input.Sprites.Gobundertops[0 + input.Actor.Unit.BreastSize]);
            }

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
        });
    });
}

internal static class GobboUndertop2
{
    internal static readonly IClothing<IOverSizeParameters> GobboUndertop2Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.Gobundertops[17];
            output.Type = 12074;
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
                output["Clothing1"].Sprite(input.Sprites.Gobundertops[16]);
            }
            else if (input.Actor.Unit.HasBreasts)
            {
                output["Clothing1"].Sprite(input.Sprites.Gobundertops[9 + input.Actor.Unit.BreastSize]);
            }

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
        });
    });
}

internal static class GobboUndertop3
{
    internal static readonly IClothing<IOverSizeParameters> GobboUndertop3Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.Gobundertops[26];
            output.Type = 12075;
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
                output["Clothing1"].Sprite(null);
            }
            else if (input.Actor.Unit.HasBreasts)
            {
                output["Clothing1"].Sprite(input.Sprites.Gobundertops[18 + input.Actor.Unit.BreastSize]);
            }

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
        });
    });
}

internal static class GobboUndertop4
{
    internal static readonly IClothing<IOverSizeParameters> GobboUndertop4Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.Gobundertops[35];
            output.Type = 12076;
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
                output["Clothing1"].Sprite(input.Sprites.Gobundertops[34]);
            }
            else if (input.Actor.Unit.HasBreasts)
            {
                output["Clothing1"].Sprite(input.Sprites.Gobundertops[27 + input.Actor.Unit.BreastSize]);
            }

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
        });
    });
}

internal static class GobboUndertop5
{
    internal static readonly IClothing<IOverSizeParameters> GobboUndertop5Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.Gobundertops[75];
            output.Type = 12077;
            output.FemaleOnly = false;
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(16);
            output["Clothing1"].Layer(20);
            int weightMod = input.Actor.Unit.BodySize;
            int size = input.Actor.GetStomachSize(32, 1.2f);
            if (size > 5)
            {
                size = 5;
            }

            if (input.Params.Oversize)
            {
                output["Clothing1"].Sprite(input.Sprites.Gobundertops[48 + 13 * weightMod]);
            }
            else if (input.Actor.Unit.HasBreasts)
            {
                output["Clothing1"].Sprite(input.Sprites.Gobundertops[42 + input.Actor.Unit.BreastSize + 13 * weightMod]);
            }

            output["Clothing2"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Gobundertops[36 + size + 13 * weightMod] : input.Sprites.Gobundertops[62 + size + 6 * weightMod]);

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
        });
    });
}

internal static class GobboOverOpFem
{
    internal static readonly IClothing<IOverSizeParameters> GobboOverOpFemInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.Gobboveronepieces[23];
            output.Type = 12078;
            output.FemaleOnly = true;
            output.RevealsBreasts = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(18);
            output["Clothing1"].Layer(20);
            int weightMod = input.Actor.Unit.BodySize;
            int size = input.Actor.GetStomachSize(32, 1.2f);
            if (size > 7)
            {
                size = 7;
            }

            int bobs = input.Actor.Unit.BreastSize;
            if (bobs > 7)
            {
                bobs = 7;
            }

            {
                if (input.Params.Oversize || input.Actor.Unit.HasBreasts == false)
                {
                    output["Clothing1"].Sprite(null);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Gobboveronepieces[0 + bobs]);
                }
            }
            output["Clothing2"].Sprite(input.Sprites.Gobboveronepieces[7 + size + 8 * weightMod]);

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
        });
    });
}

internal static class GobboOverOpm
{
    internal static IClothing GobboOverOpmInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.Gobboveronepieces[40];
            output.Type = 12079;
            output.MaleOnly = true;
            output.RevealsBreasts = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(20);
            output["Clothing1"].Layer(20);
            int weightMod = input.Actor.Unit.BodySize;
            int size = input.Actor.GetStomachSize(32, 1.2f);
            if (size > 6)
            {
                size = 6;
            }

            output["Clothing2"].Sprite(input.Actor.IsAttacking ? input.Sprites.Gobboveronepieces[25] : input.Sprites.Gobboveronepieces[24]);


            output["Clothing1"].Sprite(input.Sprites.Gobboveronepieces[26 + size + 7 * weightMod]);

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
        });
    });
}

internal static class GobboOverTop1
{
    internal static readonly IClothing<IOverSizeParameters> GobboOverTop1Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.Gobbovertops[3];
            output.Type = 12080;
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(3);
            output["Clothing1"].Layer(21);
            int weightMod = input.Actor.Unit.BodySize;

            if (input.Actor.Unit.HasBreasts)
            {
                output["Clothing1"].Sprite(input.Sprites.Gobbovertops[0 + weightMod]);
                output["Clothing2"].Sprite(input.Sprites.Gobbovertops[2]);
            }
            else
            {
                output["Clothing1"].Sprite(input.Sprites.Gobbovertops[16 + weightMod]);
                output["Clothing2"].Sprite(input.Sprites.Gobbovertops[18]);
            }

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
        });
    });
}

internal static class GobboOverTop2
{
    internal static readonly IClothing GobboOverTop2Instance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.Gobbovertops[7];
            output.Type = 12081;
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(3);
            output["Clothing1"].Layer(21);
            int weightMod = input.Actor.Unit.BodySize;

            if (input.Actor.Unit.HasBreasts)
            {
                output["Clothing1"].Sprite(input.Sprites.Gobbovertops[4 + weightMod]);
                output["Clothing2"].Sprite(input.Sprites.Gobbovertops[6]);
            }
            else
            {
                output["Clothing1"].Sprite(input.Sprites.Gobbovertops[20 + weightMod]);
                output["Clothing2"].Sprite(input.Sprites.Gobbovertops[22]);
            }

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
        });
    });
}

internal static class GobboOverTop3
{
    internal static readonly IClothing GobboOverTop3Instance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.Gobbovertops[11];
            output.Type = 12082;
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(3);
            output["Clothing1"].Layer(21);
            int weightMod = input.Actor.Unit.BodySize;


            if (input.Actor.Unit.HasBreasts)
            {
                output["Clothing1"].Sprite(input.Sprites.Gobbovertops[8 + weightMod]);
                output["Clothing2"].Sprite(input.Sprites.Gobbovertops[10]);
            }
            else
            {
                output["Clothing1"].Sprite(input.Sprites.Gobbovertops[24 + weightMod]);
                output["Clothing2"].Sprite(input.Sprites.Gobbovertops[26]);
            }

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
        });
    });
}

internal static class GobboOverTop4
{
    internal static readonly IClothing GobboOverTop4Instance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.Gobbovertops[15];
            output.Type = 12083;
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(3);
            output["Clothing1"].Layer(21);
            int weightMod = input.Actor.Unit.BodySize;

            if (input.Actor.Unit.HasBreasts)
            {
                output["Clothing1"].Sprite(input.Sprites.Gobbovertops[12 + weightMod]);
                output["Clothing2"].Sprite(input.Sprites.Gobbovertops[14]);
            }
            else
            {
                output["Clothing1"].Sprite(input.Sprites.Gobbovertops[28 + weightMod]);
                output["Clothing2"].Sprite(input.Sprites.Gobbovertops[30]);
            }

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
        });
    });
}

internal static class GoblinHolidayHat
{
    internal static readonly IClothing HolidayHatInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.ReqWinterHoliday = true;
            output.DiscardSprite = null;
            output.Type = 0;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(29);
            output["Clothing1"].Coloring(Color.white);
            output["Clothing1"].Sprite(input.Sprites.ImpGobHat[1]);
        });
    });
}