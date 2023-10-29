#region

using System.Collections.Generic;

#endregion

internal static class Collectors
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.SkinColors = ColorMap.LizardColorCount; // Majority of the body
            output.EyeColors = ColorMap.EyeColorCount; // Eyes
            output.ExtraColors1 = ColorMap.LizardColorCount; // Plates, claws
            output.ExtraColors2 = ColorMap.WyvernColorCount; // Flesh in mouth, dicks
            output.CanBeGender = new List<Gender> { Gender.Male };
            output.GentleAnimation = true;
        });


        builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
        {
            output.Coloring(ColorMap.GetWyvernColor(input.Actor.Unit.ExtraColor2));
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Collector[7]);
            }
        }); // Mouth Parts

        builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
        {
            output.Coloring(ColorMap.GetEyeColor(input.Actor.Unit.EyeColor));
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                return;
            }

            output.Sprite(input.Sprites.Collector[6]);
        }); // Eyes

        builder.RenderSingle(SpriteType.Hair, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Collector[8]);
            }
        }); // Teeth

        builder.RenderSingle(SpriteType.Body, 0, (input, output) =>
        {
            output.Coloring(ColorMap.GetLizardColor(input.Actor.Unit.SkinColor));
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Collector[1]);
                return;
            }

            output.Sprite(input.Sprites.Collector[0]);
        }); // Body

        builder.RenderSingle(SpriteType.BodyAccent, 2, (input, output) =>
        {
            output.Coloring(ColorMap.GetLizardColor(input.Actor.Unit.ExtraColor1));
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Collector[5]);
                return;
            }

            output.Sprite(input.Sprites.Collector[4]);
        }); // Plates

        builder.RenderSingle(SpriteType.BodyAccent2, 4, (input, output) =>
        {
            output.Coloring(ColorMap.GetLizardColor(input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Collector[2]);
        }); // Closer Legs

        builder.RenderSingle(SpriteType.BodyAccent3, 5, (input, output) =>
        {
            output.Coloring(ColorMap.GetLizardColor(input.Actor.Unit.ExtraColor1));
            output.Sprite(input.Sprites.Collector[3]);
        }); // Closer Legs Claws

        builder.RenderSingle(SpriteType.Belly, 3, (input, output) =>
        {
            output.Coloring(ColorMap.GetLizardColor(input.Actor.Unit.SkinColor));
            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false)
            {
                if (input.Actor.PredatorComponent.VisibleFullness > 4)
                {
                    output.Sprite(input.Sprites.Collector[19]);
                    return;
                }
            }

            if (input.Actor.HasBelly == false)
            {
                return;
            }

            output.Sprite(input.Sprites.Collector[9 + input.Actor.GetStomachSize(9, .8f)]);
        }); // Belly

        builder.RenderSingle(SpriteType.Dick, 1, (input, output) =>
        {
            output.Coloring(ColorMap.GetWyvernColor(input.Actor.Unit.ExtraColor2));
            if (Config.ErectionsFromVore && input.Actor.HasBelly)
            {
                output.Sprite(input.Sprites.Collector[20]);
            }
        }); // Dicks

        builder.RunBefore(Defaults.Finalize);

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.SkinColor = State.Rand.Next(0, data.MiscRaceData.SkinColors);
            unit.EyeColor = State.Rand.Next(0, data.MiscRaceData.EyeColors);
        });
    });
}