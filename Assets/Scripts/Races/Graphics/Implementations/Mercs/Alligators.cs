internal static class Alligators
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            // Alligators have three different dick sizes and no breasts.
            output.DickSizes = () => 3;
            output.BreastSizes = () => 1;
            // These set the layers and colour options used by the various alligator parts.

            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Alligator); // All parts use these colours or are precoloured.
            // Alligators have 4 mouth options (three different mouth corner variants and an empty option showing the base expression) and 4 different eyes to choose from.
            output.MouthTypes = 4;
            output.EyeTypes = 4;


            // Alligators use the gentler belly struggle animation since the stronger one would make a mess of the scale pattern.
            output.GentleAnimation = true;
        });


        builder.RenderSingle(SpriteType.Eyes, 0, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Alligators[8 + input.Actor.Unit.EyeType]);
        }); // The eyes come precoloured for the 'gators, and go under the body to boot.
        builder.RenderSingle(SpriteType.Mouth, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Alligator, input.Actor.Unit.SkinColor));
            output.Sprite(input.Actor.Unit.MouthType == 0 ? null : input.Sprites.Alligators[4 + input.Actor.Unit.MouthType]);
        }); // The mouth corners, not the actual mouth.
        builder.RenderSingle(SpriteType.Hair, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Alligator, input.Actor.Unit.SkinColor));
            output.Sprite(input.Actor.IsOralVoring ? input.Sprites.Alligators[3] : null);
        }); // Open mouth edges.
        builder.RenderSingle(SpriteType.Hair2, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Actor.IsOralVoring ? input.Sprites.Alligators[4] : null);
        }); // Open mouth inside.
        builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Alligator, input.Actor.Unit.SkinColor));
            output.Sprite(input.Actor.IsAttacking ? input.Sprites.Alligators[1] : input.Sprites.Alligators[0]);
        }); // Main body.
        builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Alligator, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasWeapon == false)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Alligators[48]);
                    return;
                }

                output.Sprite(input.Sprites.Alligators[45]);
                return;
            }

            switch (input.Actor.GetWeaponSprite())
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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Alligator, input.Actor.Unit.SkinColor));
            if (input.Actor.IsUnbirthing)
            {
                output.Sprite(input.Sprites.Alligators[23]);
                return;
            }

            if (input.Actor.IsAnalVoring)
            {
                output.Sprite(input.Sprites.Alligators[23]);
                return;
            }

            if (Config.HideCocks)
            {
                output.Sprite(input.Sprites.Alligators[16]);
                return;
            }

            if (input.Actor.IsErect())
            {
                output.Sprite(input.Sprites.Alligators[17]);
            }
        }); // The cloacal ring for the dick.

        builder.RenderSingle(SpriteType.BodyAccent4, 10, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            Accessory acc = null;
            if (input.Actor.Unit.Items == null || input.Actor.Unit.Items.Length < 3)
            {
                return;
            }

            if (input.Actor.Unit.Items[2] is Accessory)
            {
                acc = (Accessory)input.Actor.Unit.Items[2];
            }

            GetItemAccessorySprite(input, output, acc);
        }); // Gear.

        builder.RenderSingle(SpriteType.BodyAccent5, 10, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            Accessory acc = null;
            if (input.Actor.Unit.Items == null || input.Actor.Unit.Items.Length < 2)
            {
                return;
            }

            if (input.Actor.Unit.Items[1] is Accessory)
            {
                acc = (Accessory)input.Actor.Unit.Items[1];
            }

            GetItemAccessorySprite(input, output, acc);
        }); // Gear.

        builder.RenderSingle(SpriteType.BodyAccessory, 9, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.Level > 19)
            {
                output.Sprite(input.Sprites.Alligators[44]);
                return;
            }

            if (input.Actor.Unit.Level > 17)
            {
                output.Sprite(input.Sprites.Alligators[43]);
                return;
            }

            if (input.Actor.Unit.Level > 14)
            {
                output.Sprite(input.Sprites.Alligators[42]);
                return;
            }

            if (input.Actor.Unit.Level > 11)
            {
                output.Sprite(input.Sprites.Alligators[41]);
                return;
            }

            if (input.Actor.Unit.Level > 8)
            {
                output.Sprite(input.Sprites.Alligators[40]);
                return;
            }

            if (input.Actor.Unit.Level > 4)
            {
                output.Sprite(input.Sprites.Alligators[39]);
            }
        }); // The level band.

        builder.RenderSingle(SpriteType.SecondaryAccessory, 10, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            Accessory acc = null;
            if (input.Actor.Unit.Items == null || input.Actor.Unit.Items.Length < 1)
            {
                return;
            }

            if (input.Actor.Unit.Items[0] is Accessory)
            {
                acc = (Accessory)input.Actor.Unit.Items[0];
            }

            GetItemAccessorySprite(input, output, acc);
        }); // Gear.

        builder.RenderSingle(SpriteType.Belly, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Alligator, input.Actor.Unit.SkinColor));
            int bellySize = input.Actor.GetUniversalSize(8); //One extra for the empty check
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
            if (input.Actor.IsAnalVoring)
            {
                return;
            }

            if (input.Actor.IsCockVoring)
            {
                if (input.Actor.Unit.DickSize < 2)
                {
                    output.Sprite(input.Sprites.Alligators[21]);
                    return;
                }

                output.Sprite(input.Sprites.Alligators[22]);
                return;
            }

            if (input.Actor.IsUnbirthing)
            {
                output.Sprite(input.Sprites.Alligators[24]);
                return;
            }

            if (input.Actor.IsErect() && !Config.HideCocks)
            {
                switch (input.Actor.Unit.DickSize)
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
            if (input.Actor.Unit.HasWeapon == false)
            {
                return;
            }

            switch (input.Actor.GetWeaponSprite())
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
            if (input.Actor.Unit.HasWeapon == false)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Alligators[35]);
                    return;
                }

                output.Sprite(input.Sprites.Alligators[33]);
                return;
            }

            switch (input.Actor.GetWeaponSprite())
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