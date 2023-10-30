#region

using System.Collections.Generic;

#endregion

internal static class TwistedVines
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.None };
            output.EyeTypes = 2;
        });


        builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            int headType = input.Actor.Unit.EyeType * 2;
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                headType++;
            }

            output.Sprite(input.Sprites.Plant[headType]);
        });

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Plant[4]);
        });
        builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Plant[5]);
        });

        builder.RenderSingle(SpriteType.Belly, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false) && input.Actor.GetStomachSize() == 15)
            {
                output.Sprite(input.Sprites.Plant[15]);
                return;
            }

            output.Sprite(input.Sprites.Plant[6 + input.Actor.GetStomachSize(8)]);
        });

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(Defaults.RandomCustom);
    });
}