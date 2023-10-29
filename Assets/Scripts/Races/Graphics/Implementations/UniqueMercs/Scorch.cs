#region

using System.Collections.Generic;

#endregion

internal static class Scorch
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.Male };
            output.GentleAnimation = true;
        });


        builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Scorch[input.Actor.IsAttacking || input.Actor.IsOralVoring ? 1 : 0]);
        });
        builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Scorch[2]);
        });
        builder.RenderSingle(SpriteType.Belly, 2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false)
            {
                if (input.Actor.PredatorComponent.VisibleFullness > 4)
                {
                    output.Sprite(input.Sprites.Scorch[7]);
                    return;
                }
            }

            output.Sprite(input.Actor.HasBelly ? input.Sprites.Scorch[3 + input.Actor.GetStomachSize(3)] : null);
        });

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.Name = "Scorch";
        });
    });
}