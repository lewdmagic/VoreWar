using UnityEngine;

internal static class MinimalTemplateWithParams
{
    private class SomeParameters : IParameters
    {
        public bool AProperty { get; set; }
    }

    internal static RaceDataMaker MyRace = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
    {
        builder.Setup(output => { });

        builder.RandomCustom(data => { });

        builder.RunBefore((input, output) => { });

        builder.RenderSingle(SpriteType.Body, 3, (input, output) => { });
    });

    private static BindableClothing<SomeParameters> Rags = ClothingBuilder.CreateV2<SomeParameters>(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

        builder.RenderAll((input, output, extra) => { });
    });
}