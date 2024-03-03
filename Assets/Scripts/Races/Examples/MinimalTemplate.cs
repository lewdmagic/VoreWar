using UnityEngine;

internal static class MinimalTemplate
{
    internal static RaceDataMaker MyRace = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
    {
        builder.Setup((input, output) => { });

        builder.RandomCustom((data, output) => { });

        builder.RunBefore((input, output) => { });

        builder.RenderSingle(SpriteType.Body, 3, (input, output) => { });
    });

    private static IClothing _rags = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

        builder.RenderAll((input, output) => { });
    });
}