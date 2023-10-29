#region

using System.Collections.Generic;

#endregion

internal static class Selicia
{
    private const float BellyScale = 0.9f;

    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.Female };
            output.GentleAnimation = true;
        });


        builder.RenderSingle(SpriteType.Head, 10, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking || input.Actor.IsUnbirthing)
            {
                output.Sprite(input.Sprites.Selicia[7]);
                return;
            }

            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Selicia[8]);
                return;
            }

            if (input.Actor.GetStomachSize(14, BellyScale) < 5)
            {
                output.Sprite(input.Sprites.Selicia[6]);
                return;
            }

            if (State.Rand.Next(450) == 0)
            {
                input.Actor.SetAnimationMode(1, .75f);
            }

            int specialMode = input.Actor.CheckAnimationFrame();
            if (specialMode == 1)
            {
                output.Sprite(input.Sprites.Selicia[9]);
                return;
            }

            output.Sprite(input.Sprites.Selicia[7]);
        });

        builder.RenderSingle(SpriteType.Body, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            int size = input.Actor.GetStomachSize(14, BellyScale);
            output.Layer(6);
            if (size >= 12)
            {
                output.Sprite(input.Sprites.Selicia[5]).Layer(3);
                return;
            }

            if (size >= 5)
            {
                output.Sprite(input.Sprites.Selicia[2]);
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Selicia[2]);
                return;
            }

            output.Sprite(input.Sprites.Selicia[1]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            int size = input.Actor.GetStomachSize(14, BellyScale);
            if (input.Actor.GetStomachSize(14, BellyScale) < 5)
            {
                if (input.Actor.IsAttacking || input.Actor.IsEating)
                {
                    output.Sprite(input.Sprites.Selicia[3]).Layer(7);
                    return;
                }

                output.Sprite(input.Sprites.Selicia[0]).Layer(1);
                return;
            }

            if (size < 9)
            {
                output.Sprite(input.Sprites.Selicia[3]).Layer(7);
                return;
            }

            if (size < 12)
            {
                output.Sprite(input.Sprites.Selicia[4]).Layer(7);
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent2, 2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.HasBelly)
            {
                if (input.Actor.IsAttacking || input.Actor.IsEating || input.Actor.GetStomachSize(14, BellyScale) >= 5)
                {
                    return;
                }

                output.Sprite(input.Sprites.Selicia[40 + input.Actor.GetStomachSize(14, BellyScale)]);
            }
        });

        builder.RenderSingle(SpriteType.BodySize, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.GetStomachSize(14, BellyScale) < 5)
            {
                output.Sprite(input.Sprites.Selicia[45]);
            }
        });

        builder.RenderSingle(SpriteType.BreastShadow, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.GetStomachSize(14, BellyScale) >= 12)
            {
                output.Sprite(input.Sprites.Selicia[34]);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.HasBelly)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                {
                    output.Sprite(input.Sprites.Selicia[33]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    if (input.Actor.GetStomachSize(14, BellyScale * 0.7f) == 14)
                    {
                        output.Sprite(input.Sprites.Selicia[32]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(14, BellyScale * 0.8f) == 14)
                    {
                        output.Sprite(input.Sprites.Selicia[31]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(14, BellyScale * 0.9f) == 14)
                    {
                        output.Sprite(input.Sprites.Selicia[30]);
                        return;
                    }
                }

                if (input.Actor.IsAttacking || input.Actor.IsEating || input.Actor.GetStomachSize(14, BellyScale) >= 5)
                {
                    output.Sprite(input.Sprites.Selicia[15 + input.Actor.GetStomachSize(14, BellyScale)]);
                    return;
                }

                output.Sprite(input.Sprites.Selicia[10 + input.Actor.GetStomachSize(14, BellyScale)]);
            }
        });


        builder.RunBefore((input, output) =>
        {
            int size = input.Actor.GetStomachSize(14, BellyScale);
            const float defaultYOffset = 20 * .625f;
            if (size == 14)
            {
                output.changeSprite(SpriteType.Body).AddOffset(0, defaultYOffset + 0);
                output.changeSprite(SpriteType.Head).AddOffset(16 * .625f, defaultYOffset + 16 * .625f);
                output.changeSprite(SpriteType.BodyAccent).AddOffset(0, defaultYOffset + 0);
                output.changeSprite(SpriteType.BreastShadow).AddOffset(0, defaultYOffset + 0);
                output.changeSprite(SpriteType.Belly).AddOffset(0, defaultYOffset);
                output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, defaultYOffset);
                output.changeSprite(SpriteType.BodySize).AddOffset(0, defaultYOffset);
            }
            else if (size == 13)
            {
                output.changeSprite(SpriteType.Body).AddOffset(0, defaultYOffset + -8 * .625f);
                output.changeSprite(SpriteType.Head).AddOffset(16 * .625f, defaultYOffset + 8 * .625f);
                output.changeSprite(SpriteType.BodyAccent).AddOffset(0, defaultYOffset + -8 * .625f);
                output.changeSprite(SpriteType.BreastShadow).AddOffset(0, defaultYOffset + -8 * .625f);
                output.changeSprite(SpriteType.Belly).AddOffset(0, defaultYOffset);
                output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, defaultYOffset);
                output.changeSprite(SpriteType.BodySize).AddOffset(0, defaultYOffset);
            }
            else if (size == 12)
            {
                output.changeSprite(SpriteType.Body).AddOffset(0, defaultYOffset + -16 * .625f);
                output.changeSprite(SpriteType.Head).AddOffset(16 * .625f, defaultYOffset + 0);
                output.changeSprite(SpriteType.BodyAccent).AddOffset(0, defaultYOffset + -16 * .625f);
                output.changeSprite(SpriteType.BreastShadow).AddOffset(0, defaultYOffset + -16 * .625f);
                output.changeSprite(SpriteType.Belly).AddOffset(0, defaultYOffset);
                output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, defaultYOffset);
                output.changeSprite(SpriteType.BodySize).AddOffset(0, defaultYOffset);
            }
            else
            {
                output.changeSprite(SpriteType.Body).AddOffset(0, defaultYOffset);
                output.changeSprite(SpriteType.Head).AddOffset(0, defaultYOffset);
                output.changeSprite(SpriteType.BodyAccent).AddOffset(0, defaultYOffset);
                output.changeSprite(SpriteType.BreastShadow).AddOffset(0, defaultYOffset);
                output.changeSprite(SpriteType.Belly).AddOffset(0, defaultYOffset);
                output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, defaultYOffset);
                output.changeSprite(SpriteType.BodySize).AddOffset(0, defaultYOffset);
            }
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.Name = "Selicia";
        });
    });
}