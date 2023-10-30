#region

using System.Collections.Generic;

#endregion

internal static class Compy
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        RaceFrameList frameListTail = new RaceFrameList(new[] { 2, 1, 0, 1, 2, 3, 4, 3 }, new[] { 0.5f, 0.4f, 0.8f, 0.4f, 0.4f, 0.4f, 0.8f, 0.4f });

        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.Male };
            output.SkinColors = ColorMap.WyvernColorCount;
        });


        builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
        {
            output.Coloring(ColorMap.GetWyvernColor(input.Actor.Unit.SkinColor));
            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            if (input.Actor.IsAttacking || input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Compy[1]);
                return;
            }

            output.Sprite(input.Sprites.Compy[0]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
        {
            output.Coloring(ColorMap.GetWyvernColor(input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Compy[33]);
                return;
            }

            if (input.Actor.AnimationController.frameLists[0].currentlyActive)
            {
                if (input.Actor.AnimationController.frameLists[0].currentTime >= frameListTail.Times[input.Actor.AnimationController.frameLists[0].currentFrame])
                {
                    input.Actor.AnimationController.frameLists[0].currentFrame++;
                    input.Actor.AnimationController.frameLists[0].currentTime = 0f;

                    if (input.Actor.AnimationController.frameLists[0].currentFrame >= frameListTail.Frames.Length)
                    {
                        input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                        input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                        input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Compy[31 + frameListTail.Frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                return;
            }

            if (input.Actor.GetBallSize(30) > 0)
            {
                if (State.Rand.Next(300) == 0)
                {
                    input.Actor.AnimationController.frameLists[0].currentlyActive = true;
                }
            }

            else if (State.Rand.Next(1500) == 0)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = true;
            }

            output.Sprite(input.Sprites.Compy[33]);
        }); // Tail

        builder.RenderSingle(SpriteType.Dick, 2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsCockVoring)
            {
                output.Sprite(input.Sprites.Compy[3]);
                return;
            }

            if (input.Actor.GetBallSize(30) > 0)
            {
                output.Sprite(input.Sprites.Compy[2]);
            }
        });

        builder.RenderSingle(SpriteType.Balls, 0, (input, output) =>
        {
            output.Coloring(ColorMap.GetWyvernColor(input.Actor.Unit.SkinColor));
            if (input.Actor.GetBallSize(30) == 0)
            {
                output.Sprite(input.Sprites.Compy[4]);
                return;
            }

            int size = input.Actor.GetBallSize(30);

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && size >= 30)
            {
                output.Sprite(input.Sprites.Compy[30]);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 28)
            {
                output.Sprite(input.Sprites.Compy[29]);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 26)
            {
                output.Sprite(input.Sprites.Compy[28]);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 24)
            {
                output.Sprite(input.Sprites.Compy[27]);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 22)
            {
                output.Sprite(input.Sprites.Compy[26]);
                return;
            }

            if (size >= 21)
            {
                size = 21;
            }

            output.Sprite(input.Sprites.Compy[3 + size]);
        });

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(data =>
        {
            Unit unit = data.Unit;

            unit.SkinColor = State.Rand.Next(data.MiscRaceData.SkinColors);
            unit.DickSize = 1;
            unit.DefaultBreastSize = -1;
        });
    });

    private static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(0, 0, false) // Tail controller
        };
    }
}