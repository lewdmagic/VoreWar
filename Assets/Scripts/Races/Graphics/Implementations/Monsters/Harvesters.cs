#region

using System.Collections.Generic;

#endregion

internal static class Harvesters
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        RaceFrameList frameListEyes = new RaceFrameList(new int[5] { 0, 1, 2, 1, 0 }, new float[5] { .2f, .2f, .3f, .2f, .2f });
        RaceFrameList frameListArms = new RaceFrameList(new int[5] { 0, 1, 2, 1, 0 }, new float[5] { .2f, .5f, 1.5f, .5f, .2f });
        RaceFrameList frameListDick = new RaceFrameList(new int[6] { 0, 1, 0, 1, 0, 1 }, new float[6] { .2f, .2f, .2f, .2f, .3f, .4f });
        RaceFrameList frameListTongue = new RaceFrameList(new int[13] { 0, 1, 2, 3, 4, 2, 3, 4, 2, 3, 4, 1, 0 }, new float[13] { .2f, .2f, .2f, .3f, .2f, .3f, .2f, .2f, .2f, .2f, .3f, .3f, .3f });


        builder.Setup(output =>
        {
            output.DickSizes = () => 1;
            output.BreastSizes = () => 1;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Harvester);
            output.CanBeGender = new List<Gender> { Gender.Male };
        });

        builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Harvester, input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Harvester[2]);
                return;
            }

            if (input.Actor.IsOralVoring)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                output.Sprite(input.Sprites.Harvester[5]);
                return;
            }

            if (input.Actor.AnimationController.frameLists[0].currentlyActive)
            {
                if (input.Actor.AnimationController.frameLists[0].currentTime >= frameListEyes.times[input.Actor.AnimationController.frameLists[0].currentFrame])
                {
                    input.Actor.AnimationController.frameLists[0].currentFrame++;
                    input.Actor.AnimationController.frameLists[0].currentTime = 0f;

                    if (input.Actor.AnimationController.frameLists[0].currentFrame >= frameListEyes.frames.Length)
                    {
                        input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                        input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                        input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Harvester[2 + frameListEyes.frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                return;
            }

            if (State.Rand.Next(400) == 0)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = true;
            }

            output.Sprite(input.Sprites.Harvester[2]);
        }); // Head

        builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Harvester, input.Actor.Unit.SkinColor));
            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            output.Sprite(input.Sprites.Harvester[0]);
        }); // Body

        builder.RenderSingle(SpriteType.BodyAccent, 0, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Harvester, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Harvester[1]);
        }); // Tail

        builder.RenderSingle(SpriteType.BodyAccent2, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Harvester, input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Harvester[6]);
                return;
            }

            if (input.Actor.IsAttacking)
            {
                input.Actor.AnimationController.frameLists[1].currentlyActive = false;
                input.Actor.AnimationController.frameLists[1].currentFrame = 0;
                input.Actor.AnimationController.frameLists[1].currentTime = 0f;
                output.Sprite(input.Sprites.Harvester[9]);
                return;
            }

            if (input.Actor.AnimationController.frameLists[1].currentlyActive)
            {
                if (input.Actor.AnimationController.frameLists[1].currentTime >= frameListArms.times[input.Actor.AnimationController.frameLists[1].currentFrame])
                {
                    input.Actor.AnimationController.frameLists[1].currentFrame++;
                    input.Actor.AnimationController.frameLists[1].currentTime = 0f;

                    if (input.Actor.AnimationController.frameLists[1].currentFrame >= frameListArms.frames.Length)
                    {
                        input.Actor.AnimationController.frameLists[1].currentlyActive = false;
                        input.Actor.AnimationController.frameLists[1].currentFrame = 0;
                        input.Actor.AnimationController.frameLists[1].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Harvester[6 + frameListArms.frames[input.Actor.AnimationController.frameLists[1].currentFrame]]);
                return;
            }

            if (State.Rand.Next(600) == 0)
            {
                input.Actor.AnimationController.frameLists[1].currentlyActive = true;
            }

            output.Sprite(input.Sprites.Harvester[6]);
        }); // Arms

        builder.RenderSingle(SpriteType.BodyAccent3, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Harvester, input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                return;
            }

            if (input.Actor.IsOralVoring)
            {
                input.Actor.AnimationController.frameLists[3].currentlyActive = false;
                input.Actor.AnimationController.frameLists[3].currentFrame = 0;
                input.Actor.AnimationController.frameLists[3].currentTime = 0f;
                return;
            }

            if (input.Actor.AnimationController.frameLists[3].currentlyActive)
            {
                if (input.Actor.AnimationController.frameLists[3].currentTime >= frameListTongue.times[input.Actor.AnimationController.frameLists[3].currentFrame])
                {
                    input.Actor.AnimationController.frameLists[3].currentFrame++;
                    input.Actor.AnimationController.frameLists[3].currentTime = 0f;

                    if (input.Actor.AnimationController.frameLists[3].currentFrame >= frameListTongue.frames.Length)
                    {
                        input.Actor.AnimationController.frameLists[3].currentlyActive = false;
                        input.Actor.AnimationController.frameLists[3].currentFrame = 0;
                        input.Actor.AnimationController.frameLists[3].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Harvester[12 + frameListTongue.frames[input.Actor.AnimationController.frameLists[3].currentFrame]]);
                return;
            }

            if (State.Rand.Next(500) == 0 && input.Actor.HasBelly)
            {
                input.Actor.AnimationController.frameLists[3].currentlyActive = true;
            }
        }); // Tongue

        builder.RenderSingle(SpriteType.Belly, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Harvester, input.Actor.Unit.SkinColor));
            if (!input.Actor.HasBelly)
            {
                return;
            }

            int size = input.Actor.GetStomachSize(26);

            if (size >= 26 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
            {
                output.Sprite(input.Sprites.Harvester[38]);
                return;
            }

            if (size >= 24 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
            {
                output.Sprite(input.Sprites.Harvester[37]);
                return;
            }

            if (size >= 22 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
            {
                output.Sprite(input.Sprites.Harvester[36]);
                return;
            }

            if (size >= 20 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
            {
                output.Sprite(input.Sprites.Harvester[35]);
                return;
            }

            if (size > 18)
            {
                size = 18;
            }

            output.Sprite(input.Sprites.Harvester[17 + size]);
        }); // Belly

        builder.RenderSingle(SpriteType.Dick, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Harvester, input.Actor.Unit.SkinColor));
            if (!input.Actor.IsErect())
            {
                return;
            }

            if (!input.Actor.Targetable)
            {
                return;
            }

            if (input.Actor.AnimationController.frameLists[2].currentlyActive)
            {
                if (input.Actor.AnimationController.frameLists[2].currentTime >= frameListDick.times[input.Actor.AnimationController.frameLists[2].currentFrame])
                {
                    input.Actor.AnimationController.frameLists[2].currentFrame++;
                    input.Actor.AnimationController.frameLists[2].currentTime = 0f;

                    if (input.Actor.AnimationController.frameLists[2].currentFrame >= frameListDick.frames.Length)
                    {
                        input.Actor.AnimationController.frameLists[2].currentlyActive = false;
                        input.Actor.AnimationController.frameLists[2].currentFrame = 0;
                        input.Actor.AnimationController.frameLists[2].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Harvester[10 + frameListDick.frames[input.Actor.AnimationController.frameLists[2].currentFrame]]);
                return;
            }

            if (State.Rand.Next(300) == 0)
            {
                input.Actor.AnimationController.frameLists[2].currentlyActive = true;
                input.Actor.AnimationController.frameLists[2].currentFrame = 0;
                input.Actor.AnimationController.frameLists[2].currentTime = 0f;
            }

            if (input.Actor.IsErect())
            {
                output.Sprite(input.Sprites.Harvester[10]);
            }
        }); // Dick

        builder.RunBefore(Defaults.Finalize);

        builder.RandomCustom(data =>
        {
            Unit unit = data.Unit;

            unit.SkinColor = State.Rand.Next(0, data.MiscRaceData.SkinColors);
            unit.DickSize = 0;
            unit.SetDefaultBreastSize(0);
        });
    });


    internal static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(0, 0, false), // Eye controller. Index 0.
            new AnimationController.FrameList(0, 0, false), // Arm controller. Index 1.
            new AnimationController.FrameList(0, 0, false), // Dick controller. Index 2.
            new AnimationController.FrameList(0, 0, false)
        }; // Tongue controller. Index 3.
    }
}