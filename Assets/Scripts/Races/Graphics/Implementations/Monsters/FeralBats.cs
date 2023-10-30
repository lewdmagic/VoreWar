#region

using System.Collections.Generic;

#endregion

internal static class FeralBats
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        RaceFrameList frameListWings = new RaceFrameList(new int[2] { 0, 1 }, new float[2] { .2f, .2f });

        builder.Setup(output =>
        {
            output.DickSizes = () => 1;
            output.BreastSizes = () => 1;

            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Bat);
            output.CanBeGender = new List<Gender> { Gender.Female, Gender.Male };
        });

        builder.RenderSingle(SpriteType.Head, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Bat, input.Actor.Unit.SkinColor));
            if (input.Actor.IsOralVoring || input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Bat[4]);
                return;
            }

            if (input.Actor.HasBelly)
            {
                output.Sprite(input.Sprites.Bat[3]);
                return;
            }

            output.Sprite(input.Sprites.Bat[2]);
        }); // Head

        builder.RenderSingle(SpriteType.Body, 0, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Bat, input.Actor.Unit.SkinColor));
            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            if (input.Actor.IsUnbirthing || input.Actor.IsAnalVoring)
            {
                output.Sprite(input.Sprites.Bat[1]);
                return;
            }

            output.Sprite(input.Sprites.Bat[0]);
        }); // Body

        builder.RenderSingle(SpriteType.BodyAccent, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Bat, input.Actor.Unit.SkinColor));
            if (input.Actor.AnimationController.frameLists[0].currentTime >= frameListWings.Times[input.Actor.AnimationController.frameLists[0].currentFrame] && input.Actor.Unit.IsDead == false)
            {
                input.Actor.AnimationController.frameLists[0].currentFrame++;
                input.Actor.AnimationController.frameLists[0].currentTime = 0f;

                if (input.Actor.AnimationController.frameLists[0].currentFrame >= frameListWings.Frames.Length)
                {
                    input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                    input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                }
            }

            output.Sprite(input.Sprites.Bat[5 + frameListWings.Frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
        }); // Wings

        builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Bat, input.Actor.Unit.SkinColor));
            if (!input.Actor.Unit.HasDick)
            {
                if (input.Actor.IsAnalVoring)
                {
                    output.Sprite(input.Sprites.Bat[7]);
                    return;
                }

                if (input.Actor.IsUnbirthing)
                {
                    output.Sprite(input.Sprites.Bat[9]);
                    return;
                }

                output.Sprite(input.Sprites.Bat[8]);
            }
        }); // Privates

        builder.RenderSingle(SpriteType.Belly, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Bat, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.Predator == false)
            {
                return;
            }

            if (input.Actor.HasBelly)
            {
                int sprite = input.Actor.GetStomachSize(22);

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) || input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.womb))
                {
                    if (sprite >= 21)
                    {
                        output.Sprite(input.Sprites.Bat[27]);
                        return;
                    }
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) || input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.womb))
                {
                    if (sprite >= 19)
                    {
                        output.Sprite(input.Sprites.Bat[26]);
                        return;
                    }

                    if (sprite >= 17)
                    {
                        output.Sprite(input.Sprites.Bat[25]);
                        return;
                    }

                    if (sprite >= 15)
                    {
                        output.Sprite(input.Sprites.Bat[24]);
                        return;
                    }
                }

                if (sprite >= 15)
                {
                    output.Sprite(input.Sprites.Bat[23]);
                    return;
                }

                output.Sprite(input.Sprites.Bat[9 + sprite]);
            }
        }); // Belly

        builder.RenderSingle(SpriteType.Dick, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Bat, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasDick)
            {
                if (input.Actor.IsCockVoring)
                {
                    output.Sprite(input.Sprites.Bat[29]);
                    return;
                }

                if (input.Actor.IsErect())
                {
                    output.Sprite(input.Sprites.Bat[30]);
                    return;
                }

                output.Sprite(input.Sprites.Bat[28]);
            }
        }); // Dick        

        builder.RenderSingle(SpriteType.Balls, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Bat, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasDick)
            {
                if (input.Actor.Unit.Predator == false)
                {
                    output.Sprite(input.Sprites.Bat[31]);
                    return;
                }

                if (input.Actor.PredatorComponent.BallsFullness <= 0)
                {
                    output.Sprite(input.Sprites.Bat[28]);
                    return;
                }

                int sprite = input.Actor.GetBallSize(21);

                if (sprite >= 20 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false))
                {
                    output.Sprite(input.Sprites.Bat[49]);
                    return;
                }

                if (sprite >= 18 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
                {
                    output.Sprite(input.Sprites.Bat[48]);
                    return;
                }

                if (sprite >= 16 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
                {
                    output.Sprite(input.Sprites.Bat[47]);
                    return;
                }

                if (sprite >= 15)
                {
                    output.Sprite(input.Sprites.Bat[46]);
                    return;
                }

                output.Sprite(input.Sprites.Bat[31 + sprite]);
            }
        }); // Balls

        builder.RunBefore(Defaults.BasicBellyRunAfter);

        builder.RandomCustom(data =>
        {
            Unit unit = data.Unit;
            unit.SkinColor = State.Rand.Next(0, data.MiscRaceData.SkinColors);
        });
    });

    private static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(State.Rand.Next(0, 2), 0, true)
        }; // Wing controller. Index 0.
    }
}