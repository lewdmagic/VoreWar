internal static class Cats
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Default, builder =>
    {
        builder.RandomCustom(data =>
        {
            Unit unit = data.Unit;
            Defaults.RandomCustom(data);
            if (unit.Type == UnitType.Leader)
            {
                unit.ClothingType = 1 + data.MiscRaceData.AllowedMainClothingTypes.IndexOf(RaceSpecificClothing.CatLeaderInstance);
            }
        });

        builder.Setup(output =>
        {
            output.FurCapable = true;
            output.BaseBody = true;
            output.AllowedMainClothingTypes.Add(RaceSpecificClothing.CatLeaderInstance);
            output.AvoidedMainClothingTypes++;
        });


        builder.RenderSingle(SpriteType.Head, Defaults.SpriteGens3[SpriteType.Head]);
        builder.RenderSingle(SpriteType.Eyes, Defaults.SpriteGens3[SpriteType.Eyes]);
        builder.RenderSingle(SpriteType.Mouth, Defaults.SpriteGens3[SpriteType.Mouth]);
        builder.RenderSingle(SpriteType.Hair, Defaults.SpriteGens3[SpriteType.Hair]);
        builder.RenderSingle(SpriteType.Hair2, Defaults.SpriteGens3[SpriteType.Hair2]);
        builder.RenderSingle(SpriteType.Body, Defaults.SpriteGens3[SpriteType.Body]);
        builder.RenderSingle(SpriteType.BodyAccent, Defaults.SpriteGens3[SpriteType.BodyAccent]);
        builder.RenderSingle(SpriteType.BodyAccent2, Defaults.SpriteGens3[SpriteType.BodyAccent2]);
        builder.RenderSingle(SpriteType.BodyAccent3, Defaults.SpriteGens3[SpriteType.BodyAccent3]);
        builder.RenderSingle(SpriteType.BodyAccent4, Defaults.SpriteGens3[SpriteType.BodyAccent4]);
        builder.RenderSingle(SpriteType.BodyAccessory, 5, (input, output ) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Bodies[8]);
        });
        builder.RenderSingle(SpriteType.SecondaryAccessory, 1, (input, output ) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.BodyParts[0]);
        });
        builder.RenderSingle(SpriteType.BodySize, Defaults.SpriteGens3[SpriteType.BodySize]);
        builder.RenderSingle(SpriteType.Breasts, Defaults.SpriteGens3[SpriteType.Breasts]);
        builder.RenderSingle(SpriteType.Belly, Defaults.SpriteGens3[SpriteType.Belly]);
        builder.RenderSingle(SpriteType.Dick, Defaults.SpriteGens3[SpriteType.Dick]);
        builder.RenderSingle(SpriteType.Balls, Defaults.SpriteGens3[SpriteType.Balls]);
        builder.RenderSingle(SpriteType.Weapon, Defaults.SpriteGens3[SpriteType.Weapon]);
        builder.RenderSingle(SpriteType.BackWeapon, Defaults.SpriteGens3[SpriteType.BackWeapon]);
    });
}