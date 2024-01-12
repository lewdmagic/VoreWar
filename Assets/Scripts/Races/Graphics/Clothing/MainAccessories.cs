using System.Collections.Generic;
using UnityEngine;

internal static class AsuraMask
{
    internal static readonly IClothing AsuraMaskInstanceImpsGoblins = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc);

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Sprite(input.Sprites.Asura[40]).Layer(19).SetOffset(0, -22 * .625f);
        });
    });
    
    internal static readonly IClothing AsuraMaskInstanceTaurus = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc);

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Sprite(input.Sprites.Asura[40]).Layer(19).SetOffset(0, 12 * .625f);
        });
    });
    
    internal static readonly IClothing AsuraMaskInstanceNormal = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc);

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Sprite(input.Sprites.Asura[40]).Layer(19).SetOffset(0, 12 * .625f);
        });
    });
}

internal static class SantaHat
{

    private static Dictionary<Race, Vector2> OffsetsForRaces = new Dictionary<Race, Vector2>()
    {
        { Race.Imps,      new Vector2(0, -22 * .625f) },
        { Race.Goblins,   new Vector2(0, 12 * .625f) },
        { Race.Taurus,    new Vector2(0, 9 * .625f) },
        { Race.Demifrogs, new Vector2(0, 26 * .625f) },
        { Race.Merfolk,   new Vector2(0, 14 * .625f) },
        { Race.Kangaroos, new Vector2(0, -3 * .625f) },
        { Race.Sergal,    new Vector2(0, 26 * .625f) },
    };
    
    internal static readonly IClothing SantaHatInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.ReqWinterHoliday = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(28);
            output["Clothing1"].Sprite(input.Sprites.SantaHat);

            // old code
            // switch (RaceFuncs.RaceToSwitch(input.U.GetRace))
            // {
            //     case RaceNumbers.Imps:
            //     case RaceNumbers.Goblins:
            //         output["Clothing1"].SetOffset(0, -22 * .625f);
            //         break;
            //     case RaceNumbers.Taurus:
            //         output["Clothing1"].SetOffset(0, 12 * .625f);
            //         break;
            //     case RaceNumbers.DemiFrogs:
            //         output["Clothing1"].SetOffset(0, 9 * .625f);
            //         break;
            //     case RaceNumbers.Bees:
            //         output["Clothing1"].SetOffset(0, 26 * .625f);
            //         break;
            //     case RaceNumbers.Merfolk:
            //         output["Clothing1"].SetOffset(0, 14 * .625f);
            //         break;
            //     case RaceNumbers.Kangaroos:
            //         output["Clothing1"].SetOffset(0, -3 * .625f);
            //         break;
            //     case RaceNumbers.Sergal:
            //         output["Clothing1"].SetOffset(0, 26 * .625f);
            //         break;
            //     default:
            //         output["Clothing1"].SetOffset(0, 0);
            //         break;
            // }
            
            if (OffsetsForRaces.TryGetValue(input.U.GetRace, out Vector2 offset))
            {
                output["Clothing1"].SetOffset(offset);
            }
            else
            {
                output["Clothing1"].SetOffset(0, 0);
            }
        });
    });

    // Cant be easily used at the moment
    internal static readonly IClothing SantaHatInstanceImpsGoblins = MakeSantaHatWithOffset(new Vector2(0, -22 * .625f));
    internal static readonly IClothing SantaHatInstanceTaurus = MakeSantaHatWithOffset(new Vector2(0, 12 * .625f));
    internal static readonly IClothing SantaHatInstanceFrogs = MakeSantaHatWithOffset(new Vector2(0, 9 * .625f));
    internal static readonly IClothing SantaHatInstanceBees = MakeSantaHatWithOffset(new Vector2(0, 26 * .625f));
    internal static readonly IClothing SantaHatInstanceMerfold = MakeSantaHatWithOffset(new Vector2(0, 14 * .625f));
    internal static readonly IClothing SantaHatInstanceKangaroos = MakeSantaHatWithOffset(new Vector2(0, -3 * .625f));
    internal static readonly IClothing SantaHatInstanceSergal = MakeSantaHatWithOffset(new Vector2(0, 26 * .625f));
    internal static readonly IClothing SantaHatInstanceNormal = MakeSantaHatWithOffset(new Vector2(0, 0));

    private static IClothing MakeSantaHatWithOffset(Vector2 offset)
    {
        return ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.ReqWinterHoliday = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Sprite(input.Sprites.SantaHat).Layer(28).SetOffset(offset);
            });
        });
    }
    
}

internal static class MainAccessories
{
    internal static readonly IClothing SantaHatInstance = SantaHat.SantaHatInstance;
}