#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion
namespace Races.Graphics.Implementations.MainRaces
{


    internal static class Kangaroos
    {
        //    The order the sprites should be assembled to stop anything from clashing, aside of the clothing and bellies, is from bottom to front:

        //Body, either base or battle.
        //Arm, if with battle body.
        //Fatness mod, if applicable.
        //Ears, Expression.
        //PG patch if option on.
        //Pouch, if female or herm.
        //Testes, if male or herm and they are on.The diaper loincloth would look weird with the bigger testes though, so nuts should be off if that is used.
        //Gloves, Body Armor, Helmet, Bracelet, Clothing, if worn.
        //Weapon, if equipped.Will occlude the gloves, so has to go after.
        //The fuzz/tail mod. Will partially occlude the anklets of the Body Armor, so has to go after that.
        //Footwear.Occludes few pixels of some of the tail mods, so has to go after those.
        //The gauntlet proxy Bone blade.Will slightly occlude the Bracelet, so has to go after that.
        //Belly.Will, depending on size, occlude a lot of things, so has to go near the end.May also require having clothing off.
        //Leader's necklace. Will occlude the Body Armor.
        //Open mouth. Will occlude part of the expression, some of the Fatness patch, some of Leader's necklace and a sliver of the Body Armor, so has to go after those.


        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Kangaroo", "Kangaroos");
                output.WallType(WallType.WoodenPallisade);        
                output.BonesInfo((unit) => new List<BoneInfo>()
                {
                    new BoneInfo(BoneTypes.Kangaroo, unit.Name)
                });
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 10,
                    StomachSize = 15,
                    HasTail = true,
                    FavoredStat = Stat.Agility,
                    RacialTraits = new List<Traits>()
                    {
                        Traits.BornToMove,
                        Traits.Resourceful,
                    },
                    RaceDescription = "Their old home turning ever drier and hotter, the Kangaroo tribes did not hesitate when mysterious portals opened and granted them passage to greener lands. Nomadic by nature, the Kangaroos are very adept at carrying plenty of gear with them and aren't unused to traveling with a full belly either.",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.HairStyle, "Ear Type");
                });
                output.TownNames(new List<string>
                {
                    "Roostadt",
                    "Pouchbottom",
                    "Over'under",
                    "Red-dust",
                    "Sidney",
                    "Marsupia",
                    "Ayer",
                    "Guardia",
                });
                output.DickSizes = () => 6;
                output.BreastSizes = () => 1;

                output.HairColors = 0;
                output.HairStyles = 9; //Ears
                output.EyeTypes = 23;
                output.SpecialAccessoryCount = 6; //Lower accessory
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.Kangaroo);
                output.AccessoryColors = 0;
                output.EyeColors = ColorPaletteMap.GetPaletteCount(SwapType.EyeColor);

                output.MouthTypes = 0;

                output.BodySizes = 4;

                output.AllowedWaistTypes.Clear();

                output.AvoidedMainClothingTypes = 1;
                output.AllowedMainClothingTypes.Set(
                    Loincloth1.Loincloth1Instance,
                    Loincloth2.Loincloth2Instance,
                    Loincloth3.Loincloth3Instance,
                    Loincloth4.Loincloth4Instance
                );
                output.AvoidedMouthTypes = 0;
                output.AvoidedEyeTypes = 6;
            });


            builder.RenderSingle(SpriteType.Head, 12, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.Level > 15)
                {
                    output.Sprite(input.Sprites.Kangaroos[114]);
                }
                else if (input.U.Level > 10)
                {
                    output.Sprite(input.Sprites.Kangaroos[113]);
                }
                else if (input.U.Level > 7)
                {
                    output.Sprite(input.Sprites.Kangaroos[112]);
                }
                else if (input.U.Level > 5)
                {
                    output.Sprite(input.Sprites.Kangaroos[111]);
                }
                else if (input.U.Level > 3)
                {
                    output.Sprite(input.Sprites.Kangaroos[110]);
                }
            });

            builder.RenderSingle(SpriteType.Eyes, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
                output.Sprite(input.Sprites.Kangaroos[6 + input.U.EyeType]);
            });
            builder.RenderSingle(SpriteType.Mouth, 14, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.A.IsEating ? input.Sprites.Kangaroos[2] : null);
            });
            builder.RenderSingle(SpriteType.Hair, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Kangaroo, input.U.SkinColor));
                output.Sprite(input.Sprites.Kangaroos[41 + input.U.HairStyle]);
            });
            builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Kangaroo, input.U.SkinColor));
                output.Sprite(input.Sprites.Kangaroos[input.U.HasWeapon || input.A.IsAttacking ? 1 : 0]);
            });
            builder.RenderSingle(SpriteType.BodyAccent, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Kangaroo, input.U.SkinColor));
                if (input.U.HasWeapon == false)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Kangaroos[34]);
                        return;
                    }

                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Kangaroos[32]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Kangaroos[34]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Kangaroos[29]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Kangaroos[39]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Kangaroos[29]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.Kangaroos[31]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Kangaroos[29]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Kangaroos[39]);
                        return;
                    default:
                        return;
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent2, 12, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                Accessory acc = null;
                if (input.U.Items == null || input.U.Items.Length < 1)
                {
                    return;
                }

                if (input.U.Items[0] is Accessory)
                {
                    acc = (Accessory)input.U.Items[0];
                }

                SpriteForAccessory(input, output, ref acc);
            });

            builder.RenderSingle(SpriteType.BodyAccent3, 13, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.Type == UnitType.Leader)
                {
                    output.Sprite(input.Sprites.Kangaroos[115]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent4, 12, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                Accessory acc = null;
                if (input.U.Items == null || input.U.Items.Length < 2)
                {
                    return;
                }

                if (input.U.Items[1] is Accessory)
                {
                    acc = (Accessory)input.U.Items[1];
                }

                SpriteForAccessory(input, output, ref acc);
            });

            builder.RenderSingle(SpriteType.BodyAccent5, 12, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                Accessory acc = null;
                if (input.U.Items == null || input.U.Items.Length < 3)
                {
                    return;
                }

                if (input.U.Items[2] is Accessory)
                {
                    acc = (Accessory)input.U.Items[2];
                }

                SpriteForAccessory(input, output, ref acc);
            });

            builder.RenderSingle(SpriteType.BodyAccessory, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Kangaroo, input.U.SkinColor));
                output.Sprite(input.Sprites.Kangaroos[98 + input.U.SpecialAccessoryType]);
            });
            builder.RenderSingle(SpriteType.BodySize, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Kangaroo, input.U.SkinColor));
                if (input.U.BodySize > 0)
                {
                    output.Sprite(input.Sprites.Kangaroos[103 + input.U.BodySize]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Kangaroo, input.U.SkinColor));
                if (input.A.HasBelly)
                {
                    output.Layer(15);

                    int sprite = input.A.GetStomachSize(19, .8f);
                    if (input.U.HasBreasts)
                    {

                        if (sprite <= 15)
                        {
                            output.Sprite(input.Sprites.Kangaroos[78 + sprite]);
                            return;
                        }

                        output.Sprite(input.Sprites.Kangaroos[132 - 16 + sprite]);
                        return;
                    }

                    if (sprite <= 15)
                    {
                        output.Sprite(input.Sprites.Kangaroos[62 + sprite]);
                        return;
                    }

                    output.Sprite(input.Sprites.Kangaroos[127 - 16 + sprite]);
                }
                else
                {
                    if (input.U.HasBreasts)
                    {
                        output.Sprite(input.Sprites.Kangaroos[3]).Layer(7);
                    }
                }
            });

            builder.RenderSingle(SpriteType.Dick, 8, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.DickSize >= 0 && input.A.IsErect())
                {
                    output.Sprite(input.Sprites.Kangaroos[56 + input.U.DickSize]);
                }
            });

            builder.RenderSingle(SpriteType.Balls, 9, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Kangaroo, input.U.SkinColor));
                if (input.U.DickSize < 0)
                {
                    return;
                }

                if (input.U.DickSize >= 0)
                {
                    if (input.A.PredatorComponent?.BallsFullness > 0)
                    {
                        output.Sprite(input.Sprites.Kangaroos[137 + input.A.GetBallSize(10)]);
                        return;
                    }

                    output.Sprite(input.Sprites.Kangaroos[50 + input.U.DickSize]);
                }
            });

            builder.RenderSingle(SpriteType.Weapon, 10, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon == false || input.A.Surrendered)
                {
                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Kangaroos[33]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Kangaroos[35]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Kangaroos[36]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Kangaroos[37]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Kangaroos[30]);
                        return;
                    case 5:
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Kangaroos[38]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Kangaroos[40]);
                        return;
                    default:
                        return;
                }
            });


            builder.RunBefore((input, output) =>
            {
                if (input.A.HasBelly)
                {
                    Vector3 localScale;
                    if (input.A.PredatorComponent.VisibleFullness > 4)
                    {
                        float extraCap = input.A.PredatorComponent.VisibleFullness - 4;
                        float xScale = Mathf.Min(1 + extraCap / 5, 1.8f);
                        float yScale = Mathf.Min(1 + extraCap / 40, 1.1f);
                        localScale = new Vector3(xScale, yScale, 1);
                    }
                    else
                    {
                        localScale = new Vector3(1, 1, 1);
                    }

                    output.ChangeSprite(SpriteType.Belly).SetActive(true).SetLocalScale(localScale);
                }
            });

            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                Unit unit = data.Unit;


                if (unit.Type == UnitType.Mercenary)
                {
                    if (State.Rand.Next(3) == 0)
                    {
                        unit.EyeType = 22 - State.Rand.Next(3);
                    }
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(Math.Max(data.MiscRaceData.HairStyles - 2, 0));
                    unit.SkinColor = State.Rand.Next(Math.Max(data.MiscRaceData.SkinColors - 4, 0));
                }
            });
        });


        private static void SpriteForAccessory(IRaceRenderInput input, IRaceRenderOutput output, ref Accessory acc)
        {
            if (acc == null)
            {
                output.Sprite(null);
                return;
            }

            if (acc == State.World.ItemRepository.GetItem(ItemType.BodyArmor))
            {
                if (input.U.EyeType % 2 == 0)
                {
                    output.Sprite(input.Sprites.Kangaroos[107]);
                    return;
                }

                output.Sprite(input.Sprites.Kangaroos[108]);
                return;
            }

            if (acc == State.World.ItemRepository.GetItem(ItemType.Helmet))
            {
                output.Sprite(input.Sprites.Kangaroos[109]);
                return;
            }

            if (acc == State.World.ItemRepository.GetItem(ItemType.Shoes))
            {
                output.Sprite(input.Sprites.Kangaroos[116]);
                return;
            }

            if (acc == State.World.ItemRepository.GetItem(ItemType.Gloves))
            {
                if (input.U.HasWeapon == false || input.A.Surrendered)
                {
                    output.Sprite(input.Sprites.Kangaroos[117]);
                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Kangaroos[120]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Kangaroos[121]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Kangaroos[118]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Kangaroos[122]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Kangaroos[118]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.Kangaroos[119]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Kangaroos[118]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Kangaroos[122]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Kangaroos[117]);
                        return;
                }
            }

            if (acc == State.World.ItemRepository.GetItem(ItemType.Gauntlet))
            {
                output.Sprite(input.Sprites.Kangaroos[5]);
                return;
            }

            output.Sprite(null);
        }
    }


    internal static class Loincloth1
    {
        internal static readonly IClothing Loincloth1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.ClothingId = new ClothingId("base.kangaroos/755");
                output.OccupiesAllSlots = true;
                output.DiscardSprite = input.Sprites.Kangaroos[123];
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));
                output["Clothing1"].Sprite(input.Sprites.Kangaroos[94]);
            });
        });
    }

    internal static class Loincloth2
    {
        internal static readonly IClothing Loincloth2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.ClothingId = new ClothingId("base.kangaroos/756");
                output.OccupiesAllSlots = true;
                output.DiscardSprite = input.Sprites.Kangaroos[124];
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));
                output["Clothing1"].Sprite(input.Sprites.Kangaroos[95]);
            });
        });
    }

    internal static class Loincloth3
    {
        internal static readonly IClothing Loincloth3Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.ClothingId = new ClothingId("base.kangaroos/756");
                output.OccupiesAllSlots = true;
                output.DiscardSprite = input.Sprites.Kangaroos[124];
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));
                output["Clothing1"].Sprite(input.Sprites.Kangaroos[96]);
            });
        });
    }

    internal static class Loincloth4
    {
        internal static readonly IClothing Loincloth4Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.ClothingId = new ClothingId("base.kangaroos/756");
                output.OccupiesAllSlots = true;
                output.FixedColor = true;
                output.DiscardSprite = input.Sprites.Kangaroos[124];
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);

                output["Clothing1"].Sprite(input.Sprites.Kangaroos[97]);
            });
        });
    }
}