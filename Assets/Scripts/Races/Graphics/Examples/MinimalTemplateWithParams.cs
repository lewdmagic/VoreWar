using UnityEngine;

internal static class MinimalTemplateWithParams
{
    private class SomeParameters : IParameters
    {
        public bool AProperty { get; set; }
    }
    
    internal static IRaceData MyRace = RaceBuilder.Create(Defaults.Blank<SomeParameters>, builder =>
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
    
    private static IClothing<SomeParameters> Rags = ClothingBuilder.Create<SomeParameters>(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
                
        });
            
        builder.RenderAll((input, output) =>
        {
                
        });
    });
}