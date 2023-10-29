#region

using System.Collections.Generic;

#endregion

internal static class Abakhanskya
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output => { output.CanBeGender = new List<Gender>
        {
            Gender.Female
        }; });


        builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Abakhanskya[2]);
                return;
            }

            if (input.Actor.HasBelly)
            {
                output.Sprite(input.Sprites.Abakhanskya[3]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Abakhanskya[1]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 0, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Abakhanskya[5]);
        });

        builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Abakhanskya[4]);
        });

        builder.RenderSingle(SpriteType.BodyAccent3, 2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Abakhanskya[6]);
        });

        builder.RenderSingle(SpriteType.BodyAccent4, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            var sprite = 7 + input.Actor.GetStomachSize(5);
            if (sprite == 12 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
            {
                sprite = 13;
            }

            output.Sprite(input.Sprites.Abakhanskya[sprite]);
        });

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.Name = "Abakhanskya";
        });
    });
}