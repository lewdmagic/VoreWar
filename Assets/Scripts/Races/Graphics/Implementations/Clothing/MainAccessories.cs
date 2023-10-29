internal static class AsuraMask
{
    internal static IClothing AsuraMaskInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
{
 { };
});

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(19);
            output["Clothing1"].Sprite(input.Sprites.Asura[40]);
            switch (input.Actor.Unit.Race)
            {
                case Race.Imps:
                case Race.Goblins:
                    output["Clothing1"].SetOffset(0, -22 * .625f);
                    break;
                case Race.Taurus:
                    output["Clothing1"].SetOffset(0, 12 * .625f);
                    break;
                default:
                    output["Clothing1"].SetOffset(0, 0);
                    break;
            }
        });
    });
}

internal static class SantaHat
{
    internal static IClothing SantaHatInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
{
 { output.ReqWinterHoliday = true; };
});

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(28);
            output["Clothing1"].Sprite(input.Sprites.SantaHat);
            switch (input.Actor.Unit.Race)
            {
                case Race.Imps:
                case Race.Goblins:
                    output["Clothing1"].SetOffset(0, -22 * .625f);
                    break;
                case Race.Taurus:
                    output["Clothing1"].SetOffset(0, 12 * .625f);
                    break;
                case Race.Frogs:
                    output["Clothing1"].SetOffset(0, 9 * .625f);
                    break;
                case Race.Bees:
                    output["Clothing1"].SetOffset(0, 26 * .625f);
                    break;
                case Race.Merfolk:
                    output["Clothing1"].SetOffset(0, 14 * .625f);
                    break;
                case Race.Kangaroos:
                    output["Clothing1"].SetOffset(0, -3 * .625f);
                    break;
                case Race.Sergal:
                    output["Clothing1"].SetOffset(0, 26 * .625f);
                    break;
                default:
                    output["Clothing1"].SetOffset(0, 0);
                    break;
            }
        });
    });
}

internal static class MainAccessories
{
    internal static IClothing AsuraMaskInstance = AsuraMask.AsuraMaskInstance;
    internal static IClothing SantaHatInstance = SantaHat.SantaHatInstance;
}