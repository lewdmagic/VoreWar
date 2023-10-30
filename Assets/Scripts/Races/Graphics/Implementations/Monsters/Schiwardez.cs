#region

using System.Collections.Generic;

#endregion

internal static class Schiwardez
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.GentleAnimation = true;
            output.CanBeGender = new List<Gender> { Gender.Male };
            output.SkinColors = ColorMap.SchiwardezColorCount;
        });


        builder.RenderSingle(SpriteType.Head, 8, (input, output) =>
            {
                output.Coloring(ColorMap.GetSchiwardezColor(input.Actor.Unit.SkinColor));
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Schiwardez[37]);
                    return;
                }

                if (input.Actor.GetBallSize(24) > 0)
                {
                    output.Sprite(input.Sprites.Schiwardez[5]);
                    return;
                }

                output.Sprite(input.Sprites.Schiwardez[4]);
            }); // Head       

        builder.RenderSingle(SpriteType.Body, 5, (input, output) =>
            {
                output.Coloring(ColorMap.GetSchiwardezColor(input.Actor.Unit.SkinColor));
                if (input.Actor.GetBallSize(24) > 17)
                {
                    output.Sprite(input.Sprites.Schiwardez[1]);
                    return;
                }

                output.Sprite(input.Sprites.Schiwardez[0]);
            }); // Body

        builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
        {
            output.Coloring(ColorMap.GetSchiwardezColor(input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Schiwardez[3]);
        }); // Closer Legs

        builder.RenderSingle(SpriteType.BodyAccent2, 0, (input, output) =>
        {
            output.Coloring(ColorMap.GetSchiwardezColor(input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Schiwardez[2]);
        }); // Far Legs

        builder.RenderSingle(SpriteType.BodyAccent3, 2, (input, output) =>
        {
            output.Coloring(ColorMap.GetSchiwardezColor(input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Schiwardez[8]);
        }); // Sheath

        builder.RenderSingle(SpriteType.BodyAccent4, 6, (input, output) =>
        {
            output.Coloring(ColorMap.GetSchiwardezColor(input.Actor.Unit.SkinColor));
            if (input.Actor.GetBallSize(24) > 17)
            {
                output.Sprite(input.Sprites.Schiwardez[36]);
                return;
            }

            if (input.Actor.GetBallSize(24) > 14)
            {
                output.Sprite(input.Sprites.Schiwardez[35]);
                return;
            }

            if (input.Actor.GetBallSize(24) > 12)
            {
                output.Sprite(input.Sprites.Schiwardez[34]);
                return;
            }

            output.Sprite(input.Sprites.Schiwardez[33]);
        }); // Tail

        builder.RenderSingle(SpriteType.BodyAccent5, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Schiwardez[38]);
            }
        }); // Mouth

        builder.RenderSingle(SpriteType.Dick, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsCockVoring)
            {
                output.Sprite(input.Sprites.Schiwardez[7]);
                return;
            }

            if (input.Actor.IsErect())
            {
                output.Sprite(input.Sprites.Schiwardez[6]);
            }
        }); // Dick

        builder.RenderSingle(SpriteType.Balls, 1, (input, output) =>
            {
                output.Coloring(ColorMap.GetSchiwardezColor(input.Actor.Unit.SkinColor));
                if (input.Actor.GetBallSize(24) == 0 && Config.HideCocks == false)
                {
                    output.Sprite(input.Sprites.Schiwardez[9]);
                    return;
                }

                int size = input.Actor.GetBallSize(24);

                if (size == 24 &&
                    (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true,
                        PreyLocation.balls) ?? false))
                {
                    output.Sprite(input.Sprites.Schiwardez[32]);
                    return;
                }

                if (size >= 23 &&
                    (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false,
                        PreyLocation.balls) ?? false))
                {
                    output.Sprite(input.Sprites.Schiwardez[31]);
                    return;
                }

                if (size >= 21 &&
                    (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false,
                        PreyLocation.balls) ?? false))
                {
                    output.Sprite(input.Sprites.Schiwardez[30]);
                    return;
                }

                if (size >= 19 &&
                    (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false,
                        PreyLocation.balls) ?? false))
                {
                    output.Sprite(input.Sprites.Schiwardez[29]);
                    return;
                }

                if (size > 18)
                {
                    size = 18;
                }

                output.Sprite(input.Sprites.Schiwardez[8 + size]);
            }); // Balls


        builder.RunBefore((input, output) =>
        {
            output.changeSprite(SpriteType.Balls).AddOffset(-125 * .5f, 0);
        });
        builder.RandomCustom(Defaults.RandomCustom);
    });
}