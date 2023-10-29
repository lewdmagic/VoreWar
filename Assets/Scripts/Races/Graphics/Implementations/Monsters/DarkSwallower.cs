#region

using System.Collections.Generic;

#endregion

internal static class DarkSwallower
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        RaceFrameList frameListTail = new RaceFrameList(new int[8] { 0, 1, 2, 3, 4, 3, 2, 1 }, new float[8] { 1.2f, 1f, 1f, 1f, 1.2f, 1f, 1f, 1f });
        RaceFrameList frameListFins = new RaceFrameList(new int[4] { 0, 1, 2, 1 }, new float[4] { 1f, .8f, 1f, .8f });


        builder.Setup(output =>
        {
            output.EyeTypes = 6;
            output.CanBeGender = new List<Gender> { Gender.None };
            output.SkinColors = ColorMap.SharkColorCount;
        });


        builder.RenderSingle(SpriteType.Eyes, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.DarkSwallower[2 + input.Actor.Unit.EyeType]);
        }); // Eyes.
        builder.RenderSingle(SpriteType.Mouth, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Actor.IsAttacking || input.Actor.IsEating ? input.Sprites.DarkSwallower[8] : null);
        }); // Mouth inside.
        builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
        {
            output.Coloring(ColorMap.GetSharkColor(input.Actor.Unit.SkinColor));
            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            if (input.Actor.IsEating || input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.DarkSwallower[1]);
                return;
            }

            output.Sprite(input.Sprites.DarkSwallower[0]);
        }); // Body, open mouth base.

        builder.RenderSingle(SpriteType.BodyAccent, 0, (input, output) =>
        {
            output.Coloring(ColorMap.GetSharkColor(input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.DarkSwallower[9]);
                return;
            }

            if (input.Actor.AnimationController.frameLists[0].currentTime >= frameListTail.times[input.Actor.AnimationController.frameLists[0].currentFrame])
            {
                input.Actor.AnimationController.frameLists[0].currentFrame++;
                input.Actor.AnimationController.frameLists[0].currentTime = 0f;

                if (input.Actor.AnimationController.frameLists[0].currentFrame >= frameListTail.frames.Length)
                {
                    input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                }
            }

            output.Sprite(input.Sprites.DarkSwallower[9 + frameListTail.frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
        }); // Tail.

        builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
        {
            output.Coloring(ColorMap.GetSharkColor(input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.DarkSwallower[14]);
                return;
            }

            if (input.Actor.AnimationController.frameLists[1].currentTime >= frameListFins.times[input.Actor.AnimationController.frameLists[1].currentFrame])
            {
                input.Actor.AnimationController.frameLists[1].currentFrame++;
                input.Actor.AnimationController.frameLists[1].currentTime = 0f;

                if (input.Actor.AnimationController.frameLists[1].currentFrame >= frameListFins.frames.Length)
                {
                    input.Actor.AnimationController.frameLists[1].currentFrame = 0;
                }
            }

            output.Sprite(input.Sprites.DarkSwallower[14 + frameListFins.frames[input.Actor.AnimationController.frameLists[1].currentFrame]]);
        }); // Sidefins.

        builder.RenderSingle(SpriteType.BodyAccent3, 1, (input, output) =>
        {
            output.Coloring(ColorMap.GetSharkColor(input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.DarkSwallower[17]);
                return;
            }

            if (input.Actor.AnimationController.frameLists[0].currentFrame % 2 == 0)
            {
                output.Sprite(input.Sprites.DarkSwallower[17]);
                return;
            }

            output.Sprite(input.Sprites.DarkSwallower[18]);
        }); // Bellyfins.

        builder.RenderSingle(SpriteType.Belly, 2, (input, output) =>
        {
            output.Coloring(ColorMap.GetSharkColor(input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                output.Sprite(input.Sprites.DarkSwallower[19]);
                return;
            }

            int size = input.Actor.GetStomachSize(29);

            if (size >= 28 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
            {
                output.Sprite(input.Sprites.DarkSwallower[44]);
                return;
            }

            if (size >= 26 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
            {
                output.Sprite(input.Sprites.DarkSwallower[43]);
                return;
            }

            if (size >= 24 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
            {
                output.Sprite(input.Sprites.DarkSwallower[42]);
                return;
            }

            if (size >= 22 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
            {
                output.Sprite(input.Sprites.DarkSwallower[41]);
                return;
            }

            if (size > 21)
            {
                size = 21;
            }

            output.Sprite(input.Sprites.DarkSwallower[19 + size]);
        }); // Belly.

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(Defaults.RandomCustom);
    });

    internal static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(State.Rand.Next(0, 8), State.Rand.Next(1, 7) / 10f, true), // Tail controller. Index 0.
            new AnimationController.FrameList(State.Rand.Next(0, 4), State.Rand.Next(1, 9) / 10f, true)
        }; // Fin controller. Index 1.
    }
}