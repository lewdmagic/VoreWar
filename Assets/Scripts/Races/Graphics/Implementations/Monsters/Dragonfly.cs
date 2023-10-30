#region

using System.Collections.Generic;

#endregion

internal static class Dragonfly
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        RaceFrameList frameListWings = new RaceFrameList(new int[3] { 0, 1, 2 }, new float[3] { .02f, .02f, .02f });

        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.None };

            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Dragonfly);
            output.GentleAnimation = true;
        });


        builder.RenderSingle(SpriteType.Head, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragonfly, input.Actor.Unit.SkinColor));
            if (input.Actor.IsOralVoring || input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Dragonfly[1]);
                return;
            }

            output.Sprite(input.Sprites.Dragonfly[0]);
        }); // Head

        builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragonfly, input.Actor.Unit.SkinColor));
            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            output.Sprite(input.Sprites.Dragonfly[2]);
        }); // Body

        builder.RenderSingle(SpriteType.BodyAccent, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragonfly, input.Actor.Unit.SkinColor));
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

            output.Sprite(input.Sprites.Dragonfly[3 + frameListWings.Frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
        }); // Wings

        builder.RenderSingle(SpriteType.Belly, 0, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragonfly, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.Predator == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                if (input.Actor.PredatorComponent.VisibleFullness > 3)
                {
                    output.Sprite(input.Sprites.Dragonfly[27]);
                    return;
                }
            }

            if (!input.Actor.HasBelly)
            {
                output.Sprite(input.Sprites.Dragonfly[6]);
                return;
            }

            output.Sprite(input.Sprites.Dragonfly[7 + input.Actor.GetStomachSize(19)]);
        }); // Belly

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(Defaults.RandomCustom);
    });

    private static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(State.Rand.Next(0, 3), 0, true)
        }; // Wing controller. Index 0.
    }
}