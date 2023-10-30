internal static class Scylla
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Default, builder =>
    {
        builder.Setup(output =>
        {
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);
            output.BodySizes = 4;
            output.AvoidedMainClothingTypes = 1;
            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing);
            output.AllowedMainClothingTypes.Set(
                ClothingTypes.BikiniTopInstance,
                ClothingTypes.BeltTopInstance,
                ClothingTypes.StrapTopInstance,
                ClothingTypes.LeotardInstance,
                ClothingTypes.BlackTopInstance,
                ClothingTypes.RagsInstance
            );
            output.AllowedWaistTypes.Set(
                ClothingTypes.LoinclothInstance
            );
        });

        builder.RenderSingle(SpriteType.Head, Defaults.SpriteGens3[SpriteType.Head]);
        builder.RenderSingle(SpriteType.Eyes, Defaults.SpriteGens3[SpriteType.Eyes]);
        builder.RenderSingle(SpriteType.Mouth, Defaults.SpriteGens3[SpriteType.Mouth]);
        builder.RenderSingle(SpriteType.Hair, Defaults.SpriteGens3[SpriteType.Hair]);
        builder.RenderSingle(SpriteType.Hair2, Defaults.SpriteGens3[SpriteType.Hair2]);
        
        
        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Scylla[24 + (input.Actor.IsAttacking ? 1 : 0) + 2 * input.Actor.Unit.BodySize + (input.Actor.Unit.HasBreasts ? 0 : 8)]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Scylla[0]);
        });
        builder.RenderSingle(SpriteType.BodyAccent2, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Scylla[1]);
        });

        builder.RenderSingle(SpriteType.BodyAccessory, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Scylla[0]);
        });
        builder.RenderSingle(SpriteType.SecondaryAccessory, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Scylla[0]);
        });


        builder.RenderSingle(SpriteType.BodySize, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Scylla[3 + input.Actor.Unit.BodySize + (input.Actor.Unit.HasBreasts ? 0 : 7)]);
        });
        builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Breasts].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
        });
        
        builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Belly].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
        });
        
        builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Dick].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
        });

        builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Balls].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
        });



        builder.RenderSingle(SpriteType.Weapon, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                int sprite = input.Actor.GetWeaponSprite();
                if (sprite == 5)
                {
                    return;
                }

                if (sprite > 5)
                {
                    sprite--;
                }

                output.Sprite(input.Sprites.Scylla[15 + sprite]);
            }
        });

        builder.RenderSingle(SpriteType.BackWeapon, Defaults.SpriteGens3[SpriteType.BackWeapon]);

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(Defaults.RandomCustom);
    });
}