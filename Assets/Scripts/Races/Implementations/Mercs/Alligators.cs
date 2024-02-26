using System.Collections.Generic;

namespace Races.Graphics.Implementations.Mercs
{
    internal static class Alligators
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Alligator", "Alligators");
                output.FlavorText(new FlavorText(
                    new Texts { "crocodilian", "lumbering", "swampy" },
                    new Texts { "armoured", "large jawed", "swampy", { "interior crocodile alligator", 0.005 } },
                    new Texts { "gator", "alligator", "crocodilian", "reptile" },
                    new Dictionary<string, string>
                    {
                        [WeaponNames.Mace] = "Turtle Club",
                        [WeaponNames.Axe] = "Flint Spear",
                        [WeaponNames.SimpleBow] = "Simple Bow",
                        [WeaponNames.CompoundBow] = "Compound Bow",
                        [WeaponNames.Claw] = "Claws"
                    }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 15,
                    StomachSize = 20,
                    HasTail = true,
                    FavoredStat = Stat.Strength,
                    CanUseRangedWeapons = false,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.CockVore, VoreType.Unbirth, VoreType.Anal },
                    ExpMultiplier = 1.25f,
                    PowerAdjustment = 1.5f,
                    RaceStats = new RaceStats() // Stronger, tougher, slower moving and with slower digestion. (Crocodilians would normally have a very strong digestion, but that reguires focusing on it, not going on fighting.)
                        // Wider, shorter throats also make eating easier, but also make prey's escape easier. (Not in RL, obviously. Or perhaps they would, if crocodilians had a habit of swallowing sizeable living prey.)
                        {
                            Strength = new StatRange(10, 18),
                            Dexterity = new StatRange(4, 7),
                            Endurance = new StatRange(12, 18),
                            Mind = new StatRange(5, 10),
                            Will = new StatRange(8, 14),
                            Agility = new StatRange(6, 10),
                            Voracity = new StatRange(7, 14),
                            Stomach = new StatRange(8, 14),
                        },
                    RacialTraits = new List<TraitType>() // Alligator = Lizard+
                    {
                        TraitType.Ravenous, // Bonus to voracity before eating
                        TraitType.Resilient, // Damage decrease
                        TraitType.Intimidating, // Penalty to enemies in melee range
                    },
                    RaceDescription = "Natives to great swamps on another dimension, the Alligators emerge sporadically from portals across the land. Either unwilling or unable to settle this realm, they instead work as mercenaries for hire. Large, tough and intimidating, they make great bruisers, but seem totally unable to understand the principle of ranged weapons.",
                });
                // Alligators have three different dick sizes and no breasts.
                output.DickSizes = () => 3;
                output.BreastSizes = () => 1;
                // These set the layers and colour options used by the various alligator parts.

                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.Alligator); // All parts use these colours or are precoloured.
                // Alligators have 4 mouth options (three different mouth corner variants and an empty option showing the base expression) and 4 different eyes to choose from.
                output.MouthTypes = 4;
                output.EyeTypes = 4;


                // Alligators use the gentler belly struggle animation since the stronger one would make a mess of the scale pattern.
                output.GentleAnimation = true;
            });


            builder.RenderSingle(SpriteType.Eyes, 0, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Alligators[8 + input.U.EyeType]);
            }); // The eyes come precoloured for the 'gators, and go under the body to boot.
            builder.RenderSingle(SpriteType.Mouth, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Alligator, input.U.SkinColor));
                output.Sprite(input.U.MouthType == 0 ? null : input.Sprites.Alligators[4 + input.U.MouthType]);
            }); // The mouth corners, not the actual mouth.
            builder.RenderSingle(SpriteType.Hair, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Alligator, input.U.SkinColor));
                output.Sprite(input.A.IsOralVoring ? input.Sprites.Alligators[3] : null);
            }); // Open mouth edges.
            builder.RenderSingle(SpriteType.Hair2, 8, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.A.IsOralVoring ? input.Sprites.Alligators[4] : null);
            }); // Open mouth inside.
            builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Alligator, input.U.SkinColor));
                output.Sprite(input.A.IsAttacking ? input.Sprites.Alligators[1] : input.Sprites.Alligators[0]);
            }); // Main body.
            builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Alligator, input.U.SkinColor));
                if (input.U.HasWeapon == false)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Alligators[48]);
                        return;
                    }

                    output.Sprite(input.Sprites.Alligators[45]);
                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Alligators[46]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Alligators[48]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Alligators[47]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Alligators[48]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Alligators[45]);
                        return;
                }
            }); // Arms and hands.

            builder.RenderSingle(SpriteType.BodyAccent2, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Alligators[2]);
            }); // Toenails.
            builder.RenderSingle(SpriteType.BodyAccent3, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Alligator, input.U.SkinColor));
                if (input.A.IsUnbirthing)
                {
                    output.Sprite(input.Sprites.Alligators[23]);
                    return;
                }

                if (input.A.IsAnalVoring)
                {
                    output.Sprite(input.Sprites.Alligators[23]);
                    return;
                }

                if (Config.HideCocks)
                {
                    output.Sprite(input.Sprites.Alligators[16]);
                    return;
                }

                if (input.A.IsErect())
                {
                    output.Sprite(input.Sprites.Alligators[17]);
                }
            }); // The cloacal ring for the dick.

            builder.RenderSingle(SpriteType.BodyAccent4, 10, (input, output) =>
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

                GetItemAccessorySprite(input, output, acc);
            }); // Gear.

            builder.RenderSingle(SpriteType.BodyAccent5, 10, (input, output) =>
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

                GetItemAccessorySprite(input, output, acc);
            }); // Gear.

            builder.RenderSingle(SpriteType.BodyAccessory, 9, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.Level > 19)
                {
                    output.Sprite(input.Sprites.Alligators[44]);
                    return;
                }

                if (input.U.Level > 17)
                {
                    output.Sprite(input.Sprites.Alligators[43]);
                    return;
                }

                if (input.U.Level > 14)
                {
                    output.Sprite(input.Sprites.Alligators[42]);
                    return;
                }

                if (input.U.Level > 11)
                {
                    output.Sprite(input.Sprites.Alligators[41]);
                    return;
                }

                if (input.U.Level > 8)
                {
                    output.Sprite(input.Sprites.Alligators[40]);
                    return;
                }

                if (input.U.Level > 4)
                {
                    output.Sprite(input.Sprites.Alligators[39]);
                }
            }); // The level band.

            builder.RenderSingle(SpriteType.SecondaryAccessory, 10, (input, output) =>
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

                GetItemAccessorySprite(input, output, acc);
            }); // Gear.

            builder.RenderSingle(SpriteType.Belly, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Alligator, input.U.SkinColor));
                int bellySize = input.A.GetUniversalSize(8); //One extra for the empty check
                bellySize -= 1;
                if (bellySize == -1)
                {
                    return;
                }

                output.Sprite(input.Sprites.Alligators[25 + bellySize]);
            }); // Tummy.

            builder.RenderSingle(SpriteType.Dick, 12, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAnalVoring)
                {
                    return;
                }

                if (input.A.IsCockVoring)
                {
                    if (input.U.DickSize < 2)
                    {
                        output.Sprite(input.Sprites.Alligators[21]);
                        return;
                    }

                    output.Sprite(input.Sprites.Alligators[22]);
                    return;
                }

                if (input.A.IsUnbirthing)
                {
                    output.Sprite(input.Sprites.Alligators[24]);
                    return;
                }

                if (input.A.IsErect() && !Config.HideCocks)
                {
                    switch (input.U.DickSize)
                    {
                        case 0:
                            output.Sprite(input.Sprites.Alligators[18]);
                            return;
                        case 1:
                            output.Sprite(input.Sprites.Alligators[19]);
                            return;
                        case 2:
                            output.Sprite(input.Sprites.Alligators[20]);
                            return;
                    }
                }
            }); // Come pre coloured.

            builder.RenderSingle(SpriteType.Weapon, 11, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon == false)
                {
                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Alligators[12]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Alligators[14]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Alligators[13]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Alligators[15]);
                        return;
                    default:
                        return;
                }
            }); // Either the mace or spear, 'gators haven't got ranged weapons. They'd just smack things with those.

            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(Defaults.RandomCustom);
        });


        private static void GetItemAccessorySprite(IRaceRenderInput input, IRaceRenderOutput output, Accessory acc)
        {
            if (acc == null)
            {
                output.Sprite(null);
                return;
            }

            if (acc == State.World.ItemRepository.GetItem(ItemType.BodyArmor))
            {
                output.Sprite(input.Sprites.Alligators[37]);
                return;
            }

            if (acc == State.World.ItemRepository.GetItem(ItemType.Helmet))
            {
                output.Sprite(input.Sprites.Alligators[36]);
                return;
            }

            if (acc == State.World.ItemRepository.GetItem(ItemType.Shoes))
            {
                output.Sprite(input.Sprites.Alligators[38]);
                return;
            }

            if (acc == State.World.ItemRepository.GetItem(ItemType.Gauntlet))
            {
                if (input.U.HasWeapon == false)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Alligators[35]);
                        return;
                    }

                    output.Sprite(input.Sprites.Alligators[33]);
                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Alligators[34]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Alligators[35]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Alligators[34]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Alligators[35]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Alligators[33]);
                        return;
                }
            }

            output.Sprite(null);
        }
    }
}