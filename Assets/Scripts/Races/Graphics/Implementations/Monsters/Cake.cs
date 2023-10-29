#region

using System.Collections.Generic;

#endregion

internal static class Cake
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.None };
            output.ExtraColors1 = ColorMap.PastelColorCount;
            output.ExtraColors2 = ColorMap.PastelColorCount;
            output.ExtraColors3 = ColorMap.PastelColorCount;
            output.ExtraColors4 = ColorMap.PastelColorCount;
        });


        builder.RenderSingle(SpriteType.Head, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsEating || input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Cake[8]);
                return;
            }

            output.Sprite(input.Sprites.Cake[7]);
        }); // Teeth.

        builder.RenderSingle(SpriteType.Eyes, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            output.Sprite(input.Sprites.Cake[9 + input.Actor.GetStomachSize(7)]);
        }); // Flames.

        builder.RenderSingle(SpriteType.Mouth, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsEating || input.Actor.IsAttacking)
            {
                if (Config.Bones || Config.ScatBones)
                {
                    output.Sprite(input.Sprites.Cake[6]);
                    return;
                }

                output.Sprite(input.Sprites.Cake[5]);
            }
        }); // Mouth.

        builder.RenderSingle(SpriteType.Body, 0, (input, output) =>
        {
            output.Coloring(ColorMap.GetPastelColor(input.Actor.Unit.ExtraColor1));
            if (input.Actor.IsEating || input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Cake[1]);
                return;
            }

            output.Sprite(input.Sprites.Cake[0]);
        }); // Body.

        builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
        {
            output.Coloring(ColorMap.GetPastelColor(input.Actor.Unit.ExtraColor2));
            output.Sprite(input.Sprites.Cake[4]);
        }); // Ring.
        builder.RenderSingle(SpriteType.BodyAccent2, 2, (input, output) =>
        {
            output.Coloring(ColorMap.GetPastelColor(input.Actor.Unit.ExtraColor3));
            output.Sprite(input.Sprites.Cake[2]);
        }); // The balls around the top.
        builder.RenderSingle(SpriteType.BodyAccent3, 3, (input, output) =>
        {
            output.Coloring(ColorMap.GetPastelColor(input.Actor.Unit.ExtraColor4));
            output.Sprite(input.Sprites.Cake[3]);
        }); // Candles.

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(Defaults.RandomCustom);
    });
}