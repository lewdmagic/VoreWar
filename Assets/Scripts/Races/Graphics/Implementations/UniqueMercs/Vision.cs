#region

using System.Collections.Generic;

#endregion

internal static class Vision
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.Male };
            output.GentleAnimation = true;
        });

        builder.RenderSingle(SpriteType.Body, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Vision[input.Actor.IsOralVoring ? 1 : 0]);
        });
        builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Vision[2]);
        });
        builder.RenderSingle(SpriteType.Belly, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false)
            {
                if (input.Actor.PredatorComponent.VisibleFullness > 3)
                {
                    output.Sprite(input.Sprites.Vision[9]);
                    return;
                }
            }

            output.Sprite(input.Actor.HasBelly ? input.Sprites.Vision[3 + input.Actor.GetStomachSize(5)] : null);
        });

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.Name = "Vision";
        });
    });
}