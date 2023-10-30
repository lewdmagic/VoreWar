﻿#region

using MermenClothing;
using UnityEngine;

#endregion

internal static class Merfolk
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Default, builder =>
    {
        IClothing leaderClothes = MermenLeader.MermenLeaderInstance;
        IClothing rags = MermenRags.MermenRagsInstance;


        builder.Setup(output =>
        {
            output.BreastSizes = () => 8;

            output.BodySizes = 4;
            output.EyeTypes = 8;
            output.HairStyles = 12;
            output.SpecialAccessoryCount = 12; //ears
            output.MouthTypes = 8;
            output.AccessoryColors = 0;
            output.HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.MermenHair);
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.MermenSkin);
            output.ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.MermenSkin); // fish parts colors
            output.BodyAccentTypes2 = 12; // tail fins
            output.BodyAccentTypes3 = 7; // arm fins
            output.BodyAccentTypes4 = 4; // eyebrows

            output.AllowedMainClothingTypes.Set(
                MermenTop1.MermenTop1Instance,
                MermenTop2.MermenTop2Instance,
                MermenTop3.MermenTop3Instance,
                MermenBodySuit.MermenBodySuitInstance,
                MermenArmour.MermenArmourInstance,
                rags,
                leaderClothes
            );
            output.AvoidedMainClothingTypes = 2;
            output.AllowedWaistTypes.Set(
                MermenFishTailLink1.MermenFishTailLink1Instance,
                MermenFishTailLink2.MermenFishTailLink2Instance,
                MermenFishTailLink3.MermenFishTailLink3Instance,
                MermenLoincloth.MermenLoinclothInstance,
                MermenBot.MermenBotInstance
            );
            output.AllowedClothingAccessoryTypes.Set(
                MermenShell1.MermenShell1Instance,
                MermenTiara.MermenTiaraInstance,
                MermenStarfish.MermenStarfishInstance,
                MermenShell2.MermenShell2Instance,
                MermenHairpin.MermenHairpinInstance,
                MermenNecklace1.MermenNecklace1Instance,
                MermenNecklace2.MermenNecklace2Instance,
                MermenNecklace3.MermenNecklace3Instance,
                MermenNecklace4.MermenNecklace4Instance,
                MermenNecklace5.MermenNecklace5Instance,
                MermenNecklace6.MermenNecklace6Instance,
                MermenNecklace7.MermenNecklace7Instance,
                MermenNecklace8.MermenNecklace8Instance
            );
            output.ClothingColors = 0;
        });


        builder.RenderSingle(SpriteType.Head, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Mermen[20]);
                return;
            }

            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Mermen[21]);
            }
        });

        builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Mermen[108 + input.Actor.Unit.EyeType]);
        });
        builder.RenderSingle(SpriteType.Mouth, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Mermen[22]);
                return;
            }

            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Mermen[23]);
                return;
            }

            output.Sprite(input.Sprites.Mermen[24 + input.Actor.Unit.MouthType]);
        });

        builder.RenderSingle(SpriteType.Hair, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenHair, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Mermen[84 + input.Actor.Unit.HairStyle]);
        });
        builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenHair, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Mermen[96 + input.Actor.Unit.HairStyle]);
        });
        builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Mermen[0 + input.Actor.Unit.BodySize] : input.Sprites.Mermen[4 + input.Actor.Unit.BodySize]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, input.Actor.Unit.ExtraColor1));
            output.Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Mermen[12 + input.Actor.Unit.BodySize] : input.Sprites.Mermen[16 + input.Actor.Unit.BodySize]);
        }); // fish tail

        builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, input.Actor.Unit.ExtraColor1));
            output.Sprite(input.Sprites.Mermen[60 + input.Actor.Unit.BodyAccentType2]);
        }); //tail fins
        builder.RenderSingle(SpriteType.BodyAccent3, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, input.Actor.Unit.ExtraColor1));
            if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Mermen[32 + (input.Actor.IsAttacking ? 1 : 0) + 4 * input.Actor.Unit.BodyAccentType3]);
            }
            else
            {
                output.Sprite(input.Sprites.Mermen[34 + (input.Actor.IsAttacking ? 1 : 0) + 4 * input.Actor.Unit.BodyAccentType3]);
            }
        }); // arm fins

        builder.RenderSingle(SpriteType.BodyAccent4, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenHair, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Mermen[116 + input.Actor.Unit.BodyAccentType4]);
        }); // eyebrows
        builder.RenderSingle(SpriteType.BodyAccent5, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Mermen[8 + (input.Actor.IsAttacking ? 1 : 0)] : input.Sprites.Mermen[10 + (input.Actor.IsAttacking ? 1 : 0)]);
        }); // arms

        builder.RenderSingle(SpriteType.BodyAccessory, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, input.Actor.Unit.ExtraColor1));
            output.Sprite(input.Sprites.Mermen[72 + input.Actor.Unit.SpecialAccessoryType]);
        }); // ears
        builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Mermen[120 + input.Actor.Unit.BreastSize] : null);
        });
        builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Belly].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, input.Actor.Unit.SkinColor));
        });

        builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Dick].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, input.Actor.Unit.SkinColor));
        });

        builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Balls].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, input.Actor.Unit.SkinColor));
        });
        
        builder.RenderSingle(SpriteType.Weapon, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                output.Sprite(input.Sprites.Mermen[128 + input.Actor.GetWeaponSprite()]);
                output.Layer(input.Actor.IsAttacking ? 20 : 3);
            }
        });


        builder.RunBefore((input, output) =>
        {
            if (input.Actor.Unit.Predator && input.Actor.GetStomachSize(16) > 5)
            {
                output.changeSprite(SpriteType.Belly).AddOffset(0, 13);
                output.changeSprite(SpriteType.Dick).AddOffset(0, 7);
                output.changeSprite(SpriteType.Balls).AddOffset(0, 7);
            }
            else if (input.Actor.Unit.Predator && input.Actor.GetStomachSize(16) > 3)
            {
                output.changeSprite(SpriteType.Belly).AddOffset(0, 12);
                output.changeSprite(SpriteType.Dick).AddOffset(0, 7);
                output.changeSprite(SpriteType.Balls).AddOffset(0, 7);
            }
            else if (input.Actor.Unit.Predator && input.Actor.GetStomachSize(16) > 2)
            {
                output.changeSprite(SpriteType.Belly).AddOffset(0, 11);
                output.changeSprite(SpriteType.Dick).AddOffset(0, 7);
                output.changeSprite(SpriteType.Balls).AddOffset(0, 7);
            }
            else
            {
                output.changeSprite(SpriteType.Belly).AddOffset(0, 10);
                output.changeSprite(SpriteType.Dick).AddOffset(0, 7);
                output.changeSprite(SpriteType.Balls).AddOffset(0, 7);
            }
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
            {
                unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypes, rags);
                if (unit.ClothingType == -1) //Covers rags not in the list
                {
                    unit.ClothingType = 1;
                }
            }

            if (unit.Type == UnitType.Leader)
            {
                unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypes, leaderClothes);
            }

            if (unit.HasDick && unit.HasBreasts)
            {
                unit.HairStyle = State.Rand.Next(Config.HermsOnlyUseFemaleHair ? 6 : data.MiscRaceData.HairStyles);
            }
            else if (unit.HasDick && Config.FemaleHairForMales)
            {
                unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
            }
            else if (unit.HasDick == false && Config.MaleHairForFemales)
            {
                unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
            }
            else
            {
                if (unit.HasDick)
                {
                    unit.HairStyle = 6 + State.Rand.Next(6);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(6);
                }
            }

            unit.BodyAccentType2 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes2);
            unit.BodyAccentType4 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes4);

            if (State.Rand.Next(2) == 0)
            {
                unit.BodyAccentType3 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes3 - 1);
            }
            else
            {
                unit.BodyAccentType3 = data.MiscRaceData.BodyAccentTypes3 - 1;
            }
        });
    });
}

namespace MermenClothing
{
    internal static class MermenTop1
    {
        internal static readonly IClothing MermenTop1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Mermen2[137];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 680;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Mermen2[24 + input.Actor.Unit.BreastSize]);
                }
            });
        });
    }

    internal static class MermenTop2
    {
        internal static readonly IClothing MermenTop2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Mermen2[138];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 681;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Mermen2[40 + input.Actor.Unit.BreastSize]);
                }
            });
        });
    }

    internal static class MermenTop3
    {
        internal static readonly IClothing MermenTop3Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Mermen2[140];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 682;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Mermen2[52 + input.Actor.Unit.BreastSize]);
                }
            });
        });
    }

    internal static class MermenBodySuit
    {
        internal static readonly IClothing MermenBodySuitInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = null;
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(11);
                output["Clothing1"].Layer(18);
                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Mermen2[60 + input.Actor.Unit.BreastSize]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, input.Actor.Unit.ExtraColor1));
                    output["Clothing2"].Sprite(input.Sprites.Mermen2[68 + input.Actor.Unit.BodySize]);
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, input.Actor.Unit.ExtraColor1));
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                    output["Clothing2"].Sprite(input.Sprites.Mermen2[132 + input.Actor.Unit.BodySize]);
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, input.Actor.Unit.ExtraColor1));
                }
            });
        });
    }

    internal static class MermenArmour
    {
        internal static readonly IClothing MermenArmourInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Mermen2[142];
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.RevealsDick = true;
                output.InFrontOfDick = true;
                output.Type = 683;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(11);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Mermen2[98 + input.Actor.Unit.BreastSize]);
                    output["Clothing2"].Sprite(input.Sprites.Mermen2[90 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Mermen2[106]);
                    output["Clothing2"].Sprite(input.Sprites.Mermen2[94 + input.Actor.Unit.BodySize]);
                }
            });
        });
    }

    internal static class MermenRags
    {
        internal static readonly IClothing MermenRagsInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Mermen2[143];
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.RevealsDick = true;
                output.InFrontOfDick = true;
                output.Type = 685;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(11);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.Unit.BreastSize < 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Mermen2[129]);
                    }
                    else if (input.Actor.Unit.BreastSize < 6)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Mermen2[130]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Mermen2[131]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Mermen2[120 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Mermen2[128]);
                    output["Clothing2"].Sprite(input.Sprites.Mermen2[124 + input.Actor.Unit.BodySize]);
                }
            });
        });
    }

    internal static class MermenLeader
    {
        internal static readonly IClothing MermenLeaderInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.LeaderOnly = true;
                output.DiscardSprite = input.Sprites.Mermen2[141];
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.RevealsDick = true;
                output.InFrontOfDick = true;
                output.Type = 686;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(10);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing2"].Layer(11);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Mermen2[80 + input.Actor.Unit.BreastSize]);
                    output["Clothing2"].Sprite(input.Sprites.Mermen2[72 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Mermen2[89]);
                    output["Clothing2"].Sprite(input.Sprites.Mermen2[76 + input.Actor.Unit.BodySize]);
                }

                output["Clothing3"].Sprite(input.Sprites.Mermen2[88]);
            });
        });
    }

    internal static class MermenFishTailLink1
    {
        internal static readonly IClothing MermenFishTailLink1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);
                output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Mermen2[0 + input.Actor.Unit.BodySize] : input.Sprites.Mermen2[4 + input.Actor.Unit.BodySize]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, input.Actor.Unit.ExtraColor1));
            });
        });
    }

    internal static class MermenFishTailLink2
    {
        internal static readonly IClothing MermenFishTailLink2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);
                output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Mermen2[8 + input.Actor.Unit.BodySize] : input.Sprites.Mermen2[12 + input.Actor.Unit.BodySize]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, input.Actor.Unit.ExtraColor1));
            });
        });
    }

    internal static class MermenFishTailLink3
    {
        internal static readonly IClothing MermenFishTailLink3Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);
                output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Mermen2[32 + input.Actor.Unit.BodySize] : input.Sprites.Mermen2[36 + input.Actor.Unit.BodySize]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, input.Actor.Unit.ExtraColor1));
            });
        });
    }

    internal static class MermenLoincloth
    {
        internal static readonly IClothing MermenLoinclothInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Mermen2[136];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.InFrontOfDick = true;
                output.Type = 687;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(11);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Mermen2[16 + input.Actor.Unit.BodySize] : input.Sprites.Mermen2[20 + input.Actor.Unit.BodySize]);
            });
        });
    }

    internal static class MermenBot
    {
        internal static readonly IClothing MermenBotInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Mermen2[139];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.Type = 688;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(11);
                output["Clothing1"].Coloring(Color.white);
                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Mermen2[48 + input.Actor.Unit.BodySize]);
                }
            });
        });
    }

    internal static class MermenShell1
    {
        internal static readonly IClothing MermenShell1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.FemaleOnly = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(9);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Mermen2[107]);
            });
        });
    }

    internal static class MermenTiara
    {
        internal static readonly IClothing MermenTiaraInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.FemaleOnly = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(9);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Mermen2[108]);
            });
        });
    }

    internal static class MermenStarfish
    {
        internal static readonly IClothing MermenStarfishInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.FemaleOnly = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(9);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Mermen2[109]);
            });
        });
    }

    internal static class MermenShell2
    {
        internal static readonly IClothing MermenShell2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.FemaleOnly = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(9);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Mermen2[110]);
            });
        });
    }

    internal static class MermenHairpin
    {
        internal static readonly IClothing MermenHairpinInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.FemaleOnly = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(9);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Mermen2[111]);
            });
        });
    }

    internal static class MermenNecklace1
    {
        internal static readonly IClothing MermenNecklace1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.FemaleOnly = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Mermen2[112]);
            });
        });
    }

    internal static class MermenNecklace2
    {
        internal static readonly IClothing MermenNecklace2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.FemaleOnly = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Mermen2[113]);
            });
        });
    }

    internal static class MermenNecklace3
    {
        internal static readonly IClothing MermenNecklace3Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.FemaleOnly = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Mermen2[114]);
            });
        });
    }

    internal static class MermenNecklace4
    {
        internal static readonly IClothing MermenNecklace4Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.FemaleOnly = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Mermen2[115]);
            });
        });
    }

    internal static class MermenNecklace5
    {
        internal static readonly IClothing MermenNecklace5Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.FemaleOnly = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Mermen2[116]);
            });
        });
    }

    internal static class MermenNecklace6
    {
        internal static readonly IClothing MermenNecklace6Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.FemaleOnly = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Mermen2[117]);
            });
        });
    }

    internal static class MermenNecklace7
    {
        internal static readonly IClothing MermenNecklace7Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.FemaleOnly = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Mermen2[118]);
            });
        });
    }

    internal static class MermenNecklace8
    {
        internal static readonly IClothing MermenNecklace8Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.FemaleOnly = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Mermen2[119]);
            });
        });
    }
}