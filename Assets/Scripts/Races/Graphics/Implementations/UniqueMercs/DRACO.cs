#region

using System.Collections.Generic;

#endregion

internal static class DRACO
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.Male };
            output.GentleAnimation = true;
            output.ClothingColors = 0;
        });


        builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking || input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.DRACO[3]);
                return;
            }

            output.Sprite(input.Sprites.DRACO[2]);
        });

        builder.RenderSingle(SpriteType.Body, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.DRACO[0]);
        });
        builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.DRACO[1]);
        });
        builder.RenderSingle(SpriteType.Belly, 4, (input, output) =>
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
                    output.Sprite(input.Sprites.DRACO[10]);
                    return;
                }
            }

            output.Sprite(input.Actor.HasBelly ? input.Sprites.DRACO[5 + input.Actor.GetStomachSize(4)] : null);
        });

        builder.RunBefore(Defaults.Finalize);

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.Name = "DRACO";
        });
    });
}