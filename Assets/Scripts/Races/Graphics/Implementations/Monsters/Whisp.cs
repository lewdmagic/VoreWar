#region

using System.Collections.Generic;

#endregion

internal static class Whisp
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.None };
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.MermenSkin);
        });

        builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Whisp[0]);
        });

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(Defaults.RandomCustom);
    });
}