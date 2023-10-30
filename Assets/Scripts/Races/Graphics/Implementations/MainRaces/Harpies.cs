#region

using UnityEngine;

#endregion

internal static class Harpies
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Default, builder =>
    {
        builder.Setup(output =>
        {
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur);
            output.SpecialAccessoryCount = 3;
            output.BodySizes = 0;
            output.HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur);
            output.ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur);
            output.ExtraColors2 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur);
            output.ExtraColors3 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur);

            output.BodyAccentTypes1 = 2;
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
                ClothingTypes.BikiniBottomInstance,
                ClothingTypes.LoinclothInstance,
                ClothingTypes.ShortsInstance
            );
        });


        builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Head].Invoke(input, output);
            output.Coloring(Defaults.FurryColor(input.Actor));
            output.AddOffset(calcVector(input));
        });

        builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Eyes].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            output.AddOffset(calcVector(input));
        });

        builder.RenderSingle(SpriteType.Mouth, 5, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Mouth].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Mouth, input.Actor.Unit.SkinColor));
            output.AddOffset(calcVector(input));
        });

        builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Hair].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.HairColor));
            output.AddOffset(calcVector(input));
        });

        builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Hair2].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.HairColor));
            output.AddOffset(calcVector(input));
        });

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Harpies[input.Actor.GetSimpleBodySprite()]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Harpies[3 + input.Actor.GetSimpleBodySprite()]);
        });

        builder.RenderSingle(SpriteType.BodyAccent2, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.ExtraColor1));
            output.Sprite(input.Sprites.Harpies[21 + input.Actor.GetSimpleBodySprite()]);
        });

        builder.RenderSingle(SpriteType.BodyAccent3, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Harpies[35 + input.Actor.GetSimpleBodySprite()]);
        });

        builder.RenderSingle(SpriteType.BodyAccent4, 5, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.BodyAccent4].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.HairColor));
        });

        builder.RenderSingle(SpriteType.BodyAccent5, 0, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.ExtraColor2));
            output.Sprite(input.Sprites.Harpies[24 + input.Actor.GetSimpleBodySprite()]);
        });

        builder.RenderSingle(SpriteType.BodyAccessory, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Harpies[32 + input.Actor.Unit.SpecialAccessoryType]).AddOffset(calcVector(input));
        });

        builder.RenderSingle(SpriteType.SecondaryAccessory, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Harpies[6 + input.Actor.GetSimpleBodySprite()]);
        });

        builder.RenderSingle(SpriteType.BodySize, -1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.ExtraColor3));
            output.Sprite(input.Sprites.Harpies[input.Actor.Unit.BodyAccentType1 == 1 ? 38 + input.Actor.GetSimpleBodySprite() : 29 + input.Actor.GetSimpleBodySprite()]);
        });

        builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Breasts].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
            output.AddOffset(calcVector(input));
        });

        builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Belly].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
            output.AddOffset(calcVector(input));
        });

        builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Dick].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
            output.AddOffset(calcVector(input));
        });

        builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Balls].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
            output.AddOffset(calcVector(input));
        });

        builder.RenderSingle(SpriteType.Weapon, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon)
            {
                output.Sprite(input.Sprites.Harpies[9 + input.Actor.GetSimpleBodySprite() + 3 * (input.Actor.GetWeaponSprite() / 2)]);
            }
            else
            {
                output.Sprite(input.Sprites.Harpies[3 + input.Actor.GetSimpleBodySprite()]);
            }
        });


        builder.RunBefore((input, output) =>
        {
            output.ClothingShift = input.Actor.GetSimpleBodySprite() != 0 ? new Vector3(0, 10, 0) : new Vector3(0, 0, 0);
        });

        builder.RandomCustom(data =>
        {
            Unit unit = data.Unit;
            Defaults.RandomCustom(data);

            unit.BodyAccentType1 = 0;

            if (Config.ExtraRandomHairColors)
            {
                if (data.MiscRaceData.HairColors == ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur))
                {
                    unit.HairColor = State.Rand.Next(data.MiscRaceData.HairColors);
                    unit.AccessoryColor = State.Rand.Next(data.MiscRaceData.HairColors);
                    unit.ExtraColor1 = State.Rand.Next(data.MiscRaceData.HairColors);
                    unit.ExtraColor2 = State.Rand.Next(data.MiscRaceData.HairColors);
                    unit.ExtraColor3 = State.Rand.Next(data.MiscRaceData.HairColors);
                }
            }
            else
            {
                if (data.MiscRaceData.HairColors == ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur))
                {
                    unit.HairColor = State.Rand.Next(ColorPaletteMap.MixedHairColors);
                    unit.AccessoryColor = State.Rand.Next(ColorPaletteMap.MixedHairColors);
                    unit.ExtraColor1 = State.Rand.Next(ColorPaletteMap.MixedHairColors);
                    unit.ExtraColor2 = State.Rand.Next(ColorPaletteMap.MixedHairColors);
                    unit.ExtraColor3 = State.Rand.Next(ColorPaletteMap.MixedHairColors);
                }
            }
        });
    });

    private static Vector2 calcVector(IRaceRenderInput input)
    {
        if (input.Actor.GetSimpleBodySprite() != 0)
        {
            return new Vector2(0, 10);
        }

        return new Vector2(0, 0);
    }
}