#region

using System.Collections.Generic;

#endregion

internal static class Mantis
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        RaceFrameList frameListScythesDefault = new RaceFrameList(new int[5] { 0, 1, 2, 1, 0 }, new float[5] { .2f, .5f, 1.5f, .5f, .2f });
        RaceFrameList frameListScythesEating = new RaceFrameList(new int[5] { 0, 1, 2, 1, 0 }, new float[5] { .2f, .5f, 1.5f, .5f, .2f });


        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.Female, Gender.Male };
            output.BodySizes = 5;
            output.EyeTypes = 6;
            output.SpecialAccessoryCount = 9; // antennae
            output.BodyAccentTypes1 = 6; // wings
            output.BodyAccentTypes2 = 6; // spine
            output.ClothingColors = 0;
            output.GentleAnimation = true;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.MantisSkin);
            output.EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.MantisSkin);
        });


        builder.RenderSingle(SpriteType.Head, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, input.Actor.Unit.SkinColor));
            switch (CalcPosition(input.Actor))
            {
                case Position.Default:
                    if (input.Actor.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Mantis[3]);
                        return;
                    }

                    output.Sprite(input.Sprites.Mantis[2]);
                    return;
                case Position.Eating:
                    if (input.Actor.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Mantis[1]);
                        return;
                    }

                    output.Sprite(input.Sprites.Mantis[0]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, input.Actor.Unit.EyeColor));
            switch (CalcPosition(input.Actor))
            {
                case Position.Default:
                    output.Sprite(input.Sprites.Mantis[18 + input.Actor.Unit.EyeType]);
                    return;
                case Position.Eating:
                    if (input.Actor.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Mantis[12 + input.Actor.Unit.EyeType]);
                        return;
                    }

                    output.Sprite(input.Sprites.Mantis[6 + input.Actor.Unit.EyeType]);
                    return;
                default:
                    output.Sprite(input.Sprites.Mantis[18 + input.Actor.Unit.EyeType]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.SecondaryEyes, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            switch (CalcPosition(input.Actor))
            {
                case Position.Default:
                    output.Sprite(input.Sprites.Mantis[44]);
                    return;
                case Position.Eating:
                    if (input.Actor.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Mantis[43]);
                        return;
                    }

                    output.Sprite(input.Sprites.Mantis[42]);
                    return;
                default:
                    output.Sprite(input.Sprites.Mantis[44]);
                    return;
            }
        });


        builder.RenderSingle(SpriteType.Mouth, 9, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            switch (CalcPosition(input.Actor))
            {
                case Position.Default:
                    if (input.Actor.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Mantis[5]);
                        return;
                    }

                    return;
                case Position.Eating:
                    if (input.Actor.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Mantis[4]);
                        return;
                    }

                    return;
                default:
                    return;
            }
        });

        builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            switch (CalcPosition(input.Actor))
            {
                case Position.Default:
                    output.Sprite(input.Sprites.Mantis[64 + input.Actor.Unit.BodySize]);
                    return;
                case Position.Eating:
                    output.Sprite(input.Sprites.Mantis[59 + input.Actor.Unit.BodySize]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, input.Actor.Unit.SkinColor));
            switch (CalcPosition(input.Actor))
            {
                case Position.Default:
                    output.Sprite(input.Sprites.Mantis[75 + input.Actor.Unit.BodyAccentType1]).Layer(6);
                    return;
                case Position.Eating:
                    output.Sprite(input.Sprites.Mantis[69 + input.Actor.Unit.BodyAccentType1]).Layer(1);
                    return;
                default:
                    output.Sprite(input.Sprites.Mantis[75 + input.Actor.Unit.BodyAccentType1]);
                    return;
            }
        }); // wings

        builder.RenderSingle(SpriteType.BodyAccent2, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, input.Actor.Unit.SkinColor));
            if (CalcPosition(input.Actor) == Position.Default)
            {
                output.Sprite(input.Sprites.Mantis[81 + input.Actor.Unit.BodyAccentType2]);
            }
        }); // spine (only default CalcPosition(input.Actor))

        builder.RenderSingle(SpriteType.BodyAccent3, 10, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Mantis[48]);
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsOralVoring || CalcPosition(input.Actor) == Position.Eating)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                return;
            }

            if (input.Actor.AnimationController.frameLists[0].currentlyActive)
            {
                if (input.Actor.AnimationController.frameLists[0].currentTime >= frameListScythesDefault.times[input.Actor.AnimationController.frameLists[0].currentFrame])
                {
                    input.Actor.AnimationController.frameLists[0].currentFrame++;
                    input.Actor.AnimationController.frameLists[0].currentTime = 0f;

                    if (input.Actor.AnimationController.frameLists[0].currentFrame >= frameListScythesDefault.frames.Length)
                    {
                        input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                        input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                        input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Mantis[48 + frameListScythesDefault.frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                return;
            }

            if (State.Rand.Next(600) == 0)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = true;
            }

            output.Sprite(input.Sprites.Mantis[48]);
        }); // scythes (default CalcPosition(input.Actor))

        builder.RenderSingle(SpriteType.BodyAccent4, 10, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsOralVoring || CalcPosition(input.Actor) == Position.Default)
            {
                input.Actor.AnimationController.frameLists[1].currentlyActive = false;
                input.Actor.AnimationController.frameLists[1].currentFrame = 0;
                input.Actor.AnimationController.frameLists[1].currentTime = 0f;
                return;
            }

            if (input.Actor.AnimationController.frameLists[1].currentlyActive)
            {
                if (input.Actor.AnimationController.frameLists[1].currentTime >= frameListScythesEating.times[input.Actor.AnimationController.frameLists[1].currentFrame])
                {
                    input.Actor.AnimationController.frameLists[1].currentFrame++;
                    input.Actor.AnimationController.frameLists[1].currentTime = 0f;

                    if (input.Actor.AnimationController.frameLists[1].currentFrame >= frameListScythesEating.frames.Length)
                    {
                        input.Actor.AnimationController.frameLists[1].currentlyActive = false;
                        input.Actor.AnimationController.frameLists[1].currentFrame = 0;
                        input.Actor.AnimationController.frameLists[1].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Mantis[45 + frameListScythesEating.frames[input.Actor.AnimationController.frameLists[1].currentFrame]]);
                return;
            }

            if (State.Rand.Next(600) == 0)
            {
                input.Actor.AnimationController.frameLists[1].currentlyActive = true;
            }

            output.Sprite(input.Sprites.Mantis[45]);
        }); // scythes (eating CalcPosition(input.Actor))

        builder.RenderSingle(SpriteType.BodyAccent5, 10, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, input.Actor.Unit.SkinColor));
            switch (CalcPosition(input.Actor))
            {
                case Position.Default:
                    if (input.Actor.IsAttacking || input.Actor.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Mantis[52]);
                        return;
                    }

                    return;
                case Position.Eating:
                    if (input.Actor.IsAttacking || input.Actor.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Mantis[51]);
                        return;
                    }

                    return;
                default:
                    return;
            }
        }); // scythes (attacking)

        builder.RenderSingle(SpriteType.BodyAccent6, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, input.Actor.Unit.SkinColor));
            switch (CalcPosition(input.Actor))
            {
                case Position.Default:
                    output.Sprite(input.Sprites.Mantis[58]);
                    return;
                case Position.Eating:
                    output.Sprite(input.Sprites.Mantis[57]);
                    return;
                default:
                    output.Sprite(input.Sprites.Mantis[58]);
                    return;
            }
        }); // thorax

        builder.RenderSingle(SpriteType.BodyAccent7, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, input.Actor.Unit.SkinColor));
            switch (CalcPosition(input.Actor))
            {
                case Position.Default:
                    output.Sprite(input.Sprites.Mantis[55]);
                    return;
                case Position.Eating:
                    output.Sprite(input.Sprites.Mantis[53]);
                    return;
                default:
                    output.Sprite(input.Sprites.Mantis[55]);
                    return;
            }
        }); // left legs

        builder.RenderSingle(SpriteType.BodyAccent8, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, input.Actor.Unit.SkinColor));
            switch (CalcPosition(input.Actor))
            {
                case Position.Default:
                    output.Sprite(input.Sprites.Mantis[56]).Layer(7);
                    return;
                case Position.Eating:
                    output.Sprite(input.Sprites.Mantis[54]).Layer(2);
                    return;
                default:
                    output.Sprite(input.Sprites.Mantis[56]);
                    return;
            }
        }); // right legs

        builder.RenderSingle(SpriteType.BodyAccessory, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, input.Actor.Unit.SkinColor));
            switch (CalcPosition(input.Actor))
            {
                case Position.Default:
                    output.Sprite(input.Sprites.Mantis[33 + input.Actor.Unit.SpecialAccessoryType]);
                    return;
                case Position.Eating:
                    output.Sprite(input.Sprites.Mantis[24 + input.Actor.Unit.SpecialAccessoryType]);
                    return;
                default:
                    output.Sprite(input.Sprites.Mantis[33 + input.Actor.Unit.SpecialAccessoryType]);
                    return;
            }
        }); // antennae

        builder.RenderSingle(SpriteType.Belly, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.Predator == false || input.Actor.HasBelly == false)
            {
                return;
            }

            if (CalcPosition(input.Actor) == Position.Eating)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(17) == 17)
                {
                    output.Sprite(input.Sprites.Mantis[107]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    if (input.Actor.GetStomachSize(17, .8f) == 17)
                    {
                        output.Sprite(input.Sprites.Mantis[106]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(17, .9f) == 17)
                    {
                        output.Sprite(input.Sprites.Mantis[105]);
                        return;
                    }
                }

                output.Sprite(input.Sprites.Mantis[87 + input.Actor.GetStomachSize(17)]);
            }
        });

        builder.RunBefore((input, output) =>
        {
            output.changeSprite(SpriteType.BodyAccent5).AddOffset(-15 * .625f, 15 * .625f);

            if (input.Actor.GetStomachSize(17) > 16)
            {
                output.changeSprite(SpriteType.BodyAccent7).AddOffset(-5 * .625f, 0);
                output.changeSprite(SpriteType.BodyAccent8).AddOffset(5 * .625f, 0);
            }
            else if (input.Actor.GetStomachSize(17) > 14)
            {
                output.changeSprite(SpriteType.BodyAccent7).AddOffset(-4 * .625f, 0);
                output.changeSprite(SpriteType.BodyAccent8).AddOffset(4 * .625f, 0);
            }
            else if (input.Actor.GetStomachSize(17) > 12)
            {
                output.changeSprite(SpriteType.BodyAccent7).AddOffset(-3 * .625f, 0);
                output.changeSprite(SpriteType.BodyAccent8).AddOffset(3 * .625f, 0);
            }
            else if (input.Actor.GetStomachSize(17) > 10)
            {
                output.changeSprite(SpriteType.BodyAccent7).AddOffset(-2 * .625f, 0);
                output.changeSprite(SpriteType.BodyAccent8).AddOffset(2 * .625f, 0);
            }
            else if (input.Actor.GetStomachSize(17) > 8)
            {
                output.changeSprite(SpriteType.BodyAccent7).AddOffset(-1 * .625f, 0);
                output.changeSprite(SpriteType.BodyAccent8).AddOffset(1 * .625f, 0);
            }
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.EyeColor = unit.SkinColor;
            unit.BodyAccentType1 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes1);
            unit.BodyAccentType2 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes2);
        });
    });

    private static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(0, 0, false), // Scythes in Default CalcPosition(input.Actor) controller. Index 0.
            new AnimationController.FrameList(0, 0, false)
        }; // Scythes in Eating CalcPosition(input.Actor) controller. Index 1.
    }

    private static Position CalcPosition(Actor_Unit actor)
    {
        if (actor.HasBelly)
        {
            return Position.Eating;
        }

        return Position.Default;
    }

    private enum Position
    {
        Default,
        Eating
    }
}