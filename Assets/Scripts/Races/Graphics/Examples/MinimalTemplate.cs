using UnityEngine;

internal static class MinimalTemplate
{
    internal static RaceDataMaker MyRace = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            
        });
        
        builder.RandomCustom(data =>
        {
            
        });
        
        builder.RunBefore((input, output) =>
        {
            
        });
        
        builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
        {
            
        });
    });
    
    private static IClothing Rags = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
                
        });
            
        builder.RenderAll((input, output) =>
        {
                
        });
    });
}