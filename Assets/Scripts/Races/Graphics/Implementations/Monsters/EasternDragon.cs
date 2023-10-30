#region

using System.Collections.Generic;

#endregion

internal static class EasternDragon
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        RaceFrameList frameListEyes = new RaceFrameList(new int[5] { 0, 1, 2, 1, 0 }, new float[5] { .2f, .2f, .3f, .2f, .2f });
        RaceFrameList frameListTongue = new RaceFrameList(new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 }, new float[8] { 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f });

        builder.Setup(output =>
        {
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EasternDragon); // Main body, legs, head, tail upper
            output.GentleAnimation = true;
            output.WeightGainDisabled = true;
            output.CanBeGender = new List<Gender> { Gender.Male, Gender.Female };
            output.BodySizes = 4; // Horn types
        });


        builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EasternDragon, input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.EasternDragon[3]);
                return;
            }

            if (input.Actor.IsOralVoring)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                output.Sprite(input.Sprites.EasternDragon[4]);
                return;
            }

            if (input.Actor.AnimationController.frameLists[0].currentlyActive)
            {
                if (input.Actor.AnimationController.frameLists[0].currentTime >= frameListEyes.Times[input.Actor.AnimationController.frameLists[0].currentFrame])
                {
                    input.Actor.AnimationController.frameLists[0].currentFrame++;
                    input.Actor.AnimationController.frameLists[0].currentTime = 0f;

                    if (input.Actor.AnimationController.frameLists[0].currentFrame >= frameListEyes.Frames.Length)
                    {
                        input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                        input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                        input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.EasternDragon[1 + frameListEyes.Frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                return;
            }

            if (State.Rand.Next(750) == 0)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = true;
            }

            output.Sprite(input.Sprites.EasternDragon[1]);
        }); // Head

        builder.RenderSingle(SpriteType.Mouth, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.EasternDragon[5]);
            }
        }); // Inner Mouth

        builder.RenderSingle(SpriteType.Body, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EasternDragon, input.Actor.Unit.SkinColor));
            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            output.Sprite(input.Sprites.EasternDragon[0]);
        }); // Body

        builder.RenderSingle(SpriteType.BodyAccent, 0, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EasternDragon, input.Actor.Unit.SkinColor));
            if (!Config.HideCocks && input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                output.Sprite(input.Sprites.EasternDragon[38]).AddOffset(128 * .3125f, 0);
                return;
            }

            if (input.Actor.PredatorComponent?.WombFullness > 0)
            {
                output.Sprite(input.Sprites.EasternDragon[38]).AddOffset(128 * .3125f, 0);
                return;
            }

            if (input.Actor.IsCockVoring || input.Actor.IsUnbirthing)
            {
                output.Sprite(input.Sprites.EasternDragon[38]).AddOffset(128 * .3125f, 0);
                return;
            }

            output.Sprite(input.Sprites.EasternDragon[18]).AddOffset(0, 0);
        }); // Tail

        builder.RenderSingle(SpriteType.BodyAccent2, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EasternDragon, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.GetGender() == Gender.Male)
            {
                if (input.Actor.Unit.DickSize < 0)
                {
                    return;
                }

                if (Config.HideCocks)
                {
                    return;
                }

                if (input.Actor.PredatorComponent?.BallsFullness > 0 || input.Actor.IsCockVoring)
                {
                    output.Sprite(input.Sprites.EasternDragon[39]);
                }
            }
            else
            {
                if (input.Actor.PredatorComponent?.WombFullness > 0 || input.Actor.IsUnbirthing)
                {
                    output.Sprite(input.Sprites.EasternDragon[67]);
                }
            }
        }); // Sheath/SnatchBase

        builder.RenderSingle(SpriteType.BodyAccent3, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsUnbirthing)
            {
                output.Sprite(input.Sprites.EasternDragon[68]);
            }
        }); // Snatch

        builder.RenderSingle(SpriteType.BodyAccent4, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EasternDragon, input.Actor.Unit.SkinColor));
            if (input.Actor.GetWombSize(17) > 0)
            {
                int sprite = input.Actor.GetWombSize(17, 0.8f);

                if (sprite == 17 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.womb) ?? false))
                {
                    output.Sprite(input.Sprites.EasternDragon[86]);
                    return;
                }

                if (sprite == 17 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.womb) ?? false))
                {
                    output.Sprite(input.Sprites.EasternDragon[85]);
                    return;
                }

                if (sprite == 16 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.womb) ?? false))
                {
                    output.Sprite(input.Sprites.EasternDragon[84]);
                    return;
                }

                output.Sprite(input.Sprites.EasternDragon[68 + sprite]);
            }
        }); // Womb

        builder.RenderSingle(SpriteType.SecondaryAccessory, 9, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (!input.Actor.Targetable)
            {
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsOralVoring)
            {
                input.Actor.AnimationController.frameLists[1].currentlyActive = false;
                input.Actor.AnimationController.frameLists[1].currentFrame = 0;
                input.Actor.AnimationController.frameLists[1].currentTime = 0f;
                return;
            }

            if (input.Actor.AnimationController.frameLists[1].currentlyActive)
            {
                if (input.Actor.AnimationController.frameLists[1].currentTime >= frameListTongue.Times[input.Actor.AnimationController.frameLists[1].currentFrame])
                {
                    input.Actor.AnimationController.frameLists[1].currentFrame++;
                    input.Actor.AnimationController.frameLists[1].currentTime = 0f;

                    if (input.Actor.AnimationController.frameLists[1].currentFrame >= frameListTongue.Frames.Length)
                    {
                        input.Actor.AnimationController.frameLists[1].currentlyActive = false;
                        input.Actor.AnimationController.frameLists[1].currentFrame = 0;
                        input.Actor.AnimationController.frameLists[1].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.EasternDragon[10 + frameListTongue.Frames[input.Actor.AnimationController.frameLists[1].currentFrame]]);
                return;
            }

            if (input.Actor.PredatorComponent?.VisibleFullness > 0 && State.Rand.Next(1200) == 0)
            {
                input.Actor.AnimationController.frameLists[1].currentlyActive = true;
            }
        }); // Tongue

        builder.RenderSingle(SpriteType.BodySize, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EasternDragon, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.EasternDragon[6 + input.Actor.Unit.BodySize]);
        }); // Horns
        builder.RenderSingle(SpriteType.Belly, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EasternDragon, input.Actor.Unit.SkinColor));
            if (input.Actor.PredatorComponent?.ExclusiveStomachFullness > 0)
            {
                int sprite = input.Actor.GetExclusiveStomachSize(16, 0.8f);

                if (sprite == 16 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false))
                {
                    output.Sprite(input.Sprites.EasternDragon[37]).AddOffset(0, 0 * .625f);
                    return;
                }

                if (sprite == 16 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
                {
                    output.Sprite(input.Sprites.EasternDragon[36]).AddOffset(0, 0 * .625f);
                    return;
                }

                if (sprite == 15 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
                {
                    output.Sprite(input.Sprites.EasternDragon[35]).AddOffset(0, 0 * .625f);
                    return;
                }

                if (sprite >= 14)
                {
                    output.Sprite(input.Sprites.EasternDragon[34]);
                    return;
                }

                output.Sprite(input.Sprites.EasternDragon[20 + sprite]);
                return;
            }

            if (input.Actor.GetExclusiveStomachSize(1) == 0)
            {
                output.Sprite(input.Sprites.EasternDragon[19]);
                return;
            }

            output.Sprite(input.Sprites.EasternDragon[20 + input.Actor.GetExclusiveStomachSize(14, 0.8f)]);
        }); // Belly

        builder.RenderSingle(SpriteType.Dick, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.DickSize < 0)
            {
                return;
            }

            if (Config.HideCocks)
            {
                return;
            }

            if (input.Actor.IsCockVoring)
            {
                output.Sprite(input.Sprites.EasternDragon[41]);
                return;
            }

            if (input.Actor.IsErect())
            {
                output.Sprite(input.Sprites.EasternDragon[40]);
            }
        }); // Dick, CV, UB

        builder.RenderSingle(SpriteType.Balls, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EasternDragon, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.DickSize < 0)
            {
                return;
            }

            if (Config.HideCocks)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.BallsFullness > 0 || input.Actor.IsCockVoring)
            {
                int sprite = input.Actor.GetBallSize(24, 0.8f);

                if (sprite == 24 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false))
                {
                    output.Sprite(input.Sprites.EasternDragon[66]);
                    return;
                }

                if (sprite == 24 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
                {
                    output.Sprite(input.Sprites.EasternDragon[65]);
                    return;
                }

                if (sprite == 23 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
                {
                    output.Sprite(input.Sprites.EasternDragon[64]);
                    return;
                }

                if (sprite >= 22)
                {
                    output.Sprite(input.Sprites.EasternDragon[63]);
                    return;
                }

                output.Sprite(input.Sprites.EasternDragon[41 + sprite]);
            }
        }); // Balls


        builder.RunBefore((input, output) =>
        {
            output.ChangeSprite(SpriteType.Balls).AddOffset(128 * .3125f, 0);
            output.ChangeSprite(SpriteType.Dick).AddOffset(128 * .3125f, 0);
            output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(128 * .3125f, 0);
            output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(128 * .3125f, 0);
            output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(128 * .3125f, 0);
        });


        builder.RandomCustom(Defaults.RandomCustom);
    });


    private static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(0, 0, false), // Eye controller. Index 0.
            new AnimationController.FrameList(0, 0, false)
        }; // Tongue controller. Index 1.
    }
}