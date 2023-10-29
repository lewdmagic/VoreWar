﻿internal static class Gryphons
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank<PositionParameters>, builder =>
    {
        builder.Setup(output =>
        {
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.GryphonSkin);
            output.GentleAnimation = true;

            output.SpecialAccessoryCount = 2;
            output.ClothingColors = 0;
        });

        builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.SpecialAccessoryType == 0)
            {
                switch (input.Params.Position)
                {
                    case Position.Standing:
                        if (input.Actor.IsOralVoring || input.Actor.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Gryphon[11]);
                            return;
                        }

                        output.Sprite(input.Sprites.Gryphon[10]);
                        return;
                    case Position.Sitting:
                        if (input.Actor.IsOralVoring || input.Actor.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Gryphon[13]);
                            return;
                        }

                        output.Sprite(input.Sprites.Gryphon[12]);
                        return;
                }

                return;
            }

            switch (input.Params.Position)
            {
                case Position.Standing:
                    if (input.Actor.IsOralVoring || input.Actor.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Griffin[11]);
                        return;
                    }

                    output.Sprite(input.Sprites.Griffin[10]);
                    return;
                case Position.Sitting:
                    if (input.Actor.IsOralVoring || input.Actor.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Griffin[13]);
                        return;
                    }

                    output.Sprite(input.Sprites.Griffin[12]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.SpecialAccessoryType == 0)
            {
                switch (input.Params.Position)
                {
                    case Position.Standing:
                        if (input.Actor.IsOralVoring || input.Actor.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Gryphon[1]);
                            return;
                        }

                        output.Sprite(input.Sprites.Gryphon[0]);
                        return;
                    case Position.Sitting:
                        if (input.Actor.IsOralVoring || input.Actor.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Gryphon[3]);
                            return;
                        }

                        output.Sprite(input.Sprites.Gryphon[2]);
                        return;
                }

                return;
            }

            switch (input.Params.Position)
            {
                case Position.Standing:
                    if (input.Actor.IsOralVoring || input.Actor.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Griffin[1]);
                        return;
                    }

                    output.Sprite(input.Sprites.Griffin[0]);
                    return;
                case Position.Sitting:
                    if (input.Actor.IsOralVoring || input.Actor.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Griffin[3]);
                        return;
                    }

                    output.Sprite(input.Sprites.Griffin[2]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.SpecialAccessoryType == 0)
            {
                switch (input.Params.Position)
                {
                    case Position.Standing:
                        if (input.Actor.IsOralVoring || input.Actor.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Gryphon[7]);
                            return;
                        }

                        output.Sprite(input.Sprites.Gryphon[5]);
                        return;
                    case Position.Sitting:
                        output.Sprite(input.Sprites.Gryphon[9]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Gryphon[5]);
                        return;
                }
            }

            switch (input.Params.Position)
            {
                case Position.Standing:
                    if (input.Actor.IsOralVoring || input.Actor.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Griffin[7]);
                        return;
                    }

                    output.Sprite(input.Sprites.Griffin[5]);
                    return;
                case Position.Sitting:
                    output.Sprite(input.Sprites.Griffin[9]);
                    return;
                default:
                    output.Sprite(input.Sprites.Griffin[5]);
                    return;
            }
        }); // right wing

        builder.RenderSingle(SpriteType.BodyAccent2, 17, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.SpecialAccessoryType == 0)
            {
                switch (input.Params.Position)
                {
                    case Position.Standing:
                        if (input.Actor.IsOralVoring || input.Actor.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Gryphon[6]);
                            return;
                        }

                        output.Sprite(input.Sprites.Gryphon[4]);
                        return;
                    case Position.Sitting:
                        output.Sprite(input.Sprites.Gryphon[8]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Gryphon[4]);
                        return;
                }
            }

            switch (input.Params.Position)
            {
                case Position.Standing:
                    if (input.Actor.IsOralVoring || input.Actor.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Griffin[6]);
                        return;
                    }

                    output.Sprite(input.Sprites.Griffin[4]);
                    return;
                case Position.Sitting:
                    output.Sprite(input.Sprites.Griffin[8]);
                    return;
                default:
                    output.Sprite(input.Sprites.Griffin[4]);
                    return;
            }
        }); // left wing

        builder.RenderSingle(SpriteType.BodyAccent3, 14, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.SpecialAccessoryType == 0)
            {
                if (input.Params.Position == Position.Sitting)
                {
                    output.Sprite(input.Sprites.Gryphon[14]);
                    return;
                }

                return;
            }

            if (input.Params.Position == Position.Sitting)
            {
                output.Sprite(input.Sprites.Griffin[14]);
            }
        }); // left side legs (only sitting)

        builder.RenderSingle(SpriteType.BodyAccent4, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.SpecialAccessoryType == 0)
            {
                switch (input.Params.Position)
                {
                    case Position.Standing:
                        output.Sprite(input.Sprites.Gryphon[15]);
                        return;
                    case Position.Sitting:
                        output.Sprite(input.Sprites.Gryphon[17]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Gryphon[15]);
                        return;
                }
            }

            switch (input.Params.Position)
            {
                case Position.Standing:
                    output.Sprite(input.Sprites.Griffin[15]);
                    return;
                case Position.Sitting:
                    output.Sprite(input.Sprites.Griffin[17]);
                    return;
                default:
                    output.Sprite(input.Sprites.Griffin[15]);
                    return;
            }
        }); // right claw (or both in standing)

        builder.RenderSingle(SpriteType.BodyAccent5, 15, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.SpecialAccessoryType == 0)
            {
                if (input.Params.Position == Position.Sitting)
                {
                    output.Sprite(input.Sprites.Gryphon[16]);
                    return;
                }

                return;
            }

            if (input.Params.Position == Position.Sitting)
            {
                output.Sprite(input.Sprites.Griffin[16]);
            }
        }); // left claw (only sitting)

        builder.RenderSingle(SpriteType.BodyAccent6, 10, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasDick == false || input.Params.Position == Position.Standing)
            {
                return;
            }

            if (input.Actor.GetBallSize(10, 1.5f) > 5)
            {
                output.Layer(1);
                if (input.Actor.PredatorComponent?.BallsFullness > 0)
                {
                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                    {
                        output.Sprite(input.Sprites.Gryphon[47]);
                        return;
                    }

                    output.Sprite(input.Sprites.Gryphon[36 + input.Actor.GetBallSize(10, 1.5f)]);
                    return;
                }

                output.Sprite(input.Sprites.Gryphon[36]);
                return;
            }

            if (input.Actor.GetStomachSize(16) < 3)
            {
                output.Layer(10);
                if (input.Actor.PredatorComponent?.BallsFullness > 0)
                {
                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                    {
                        output.Sprite(input.Sprites.Gryphon[47]);
                        return;
                    }

                    output.Sprite(input.Sprites.Gryphon[36 + input.Actor.GetBallSize(10, 1.5f)]);
                    return;
                }

                output.Sprite(input.Sprites.Gryphon[36]);
                return;
            }

            output.Layer(5);
            if (input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                {
                    output.Sprite(input.Sprites.Gryphon[47]);
                    return;
                }

                output.Sprite(input.Sprites.Gryphon[36 + input.Actor.GetBallSize(10, 1.5f)]);
                return;
            }

            output.Sprite(input.Sprites.Gryphon[36]);
        }); // right ball (only sitting)

        builder.RenderSingle(SpriteType.BodyAccent7, 11, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasDick)
            {
                output.Layer(11);
                if (input.Params.Position == Position.Standing)
                {
                    output.Sprite(input.Sprites.Gryphon[48]);
                    return;
                }

                if (input.Actor.HasBelly)
                {
                    if (input.Actor.GetStomachSize(16) == 0)
                    {
                        output.Sprite(input.Sprites.Gryphon[49]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(16) == 1)
                    {
                        output.Sprite(input.Sprites.Gryphon[50]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(16) > 1 && input.Actor.GetStomachSize(16) < 4)
                    {
                        output.Sprite(input.Sprites.Gryphon[51]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(16) >= 4 && input.Actor.GetStomachSize(16) < 7)
                    {
                        output.Sprite(input.Sprites.Gryphon[52]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(16) >= 7 && input.Actor.GetStomachSize(16) < 11)
                    {
                        output.Sprite(input.Sprites.Gryphon[53]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(16) >= 11)
                    {
                        output.Sprite(input.Sprites.Gryphon[53]).Layer(6);
                        return;
                    }

                    return;
                }

                output.Sprite(input.Sprites.Gryphon[49]);
            }
        }); // dick base

        builder.RenderSingle(SpriteType.BodyAccent8, 16, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.SpecialAccessoryType == 0)
            {
                return;
            }

            switch (input.Params.Position)
            {
                case Position.Standing:
                    output.Sprite(input.Sprites.Griffin[18]);
                    return;
                case Position.Sitting:
                    output.Sprite(input.Sprites.Griffin[19]);
                    return;
                default:
                    output.Sprite(input.Sprites.Griffin[18]);
                    return;
            }
        }); // exra feather patch (only Griffin)

        builder.RenderSingle(SpriteType.BodySize, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, input.Actor.Unit.SkinColor));
            if (input.Params.Position == Position.Sitting && input.Actor.HasBelly == false)
            {
                output.Sprite(input.Sprites.Gryphon[59]);
            }
        }); // belly cover up

        builder.RenderSingle(SpriteType.Belly, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.Predator == false || input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Params.Position == Position.Sitting)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(16) == 16)
                {
                    output.Sprite(input.Sprites.Gryphon[35]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    if (input.Actor.GetStomachSize(16, .8f) == 16)
                    {
                        output.Sprite(input.Sprites.Gryphon[61]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(16, .9f) == 16)
                    {
                        output.Sprite(input.Sprites.Gryphon[60]);
                        return;
                    }
                }

                output.Sprite(input.Sprites.Gryphon[18 + input.Actor.GetStomachSize(16)]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 12, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasDick == false || input.Params.Position == Position.Standing)
            {
                return;
            }

            if (input.Actor.IsErect())
            {
                output.Layer(12);
                if (input.Actor.GetStomachSize(16) == 0)
                {
                    output.Sprite(input.Sprites.Gryphon[54]);
                    return;
                }

                if (input.Actor.GetStomachSize(16) == 1)
                {
                    output.Sprite(input.Sprites.Gryphon[55]);
                    return;
                }

                if (input.Actor.GetStomachSize(16) > 1 && input.Actor.GetStomachSize(16) < 4)
                {
                    output.Sprite(input.Sprites.Gryphon[56]);
                    return;
                }

                if (input.Actor.GetStomachSize(16) >= 4 && input.Actor.GetStomachSize(16) < 7)
                {
                    output.Sprite(input.Sprites.Gryphon[57]);
                    return;
                }

                if (input.Actor.GetStomachSize(16) >= 7 && input.Actor.GetStomachSize(16) < 11)
                {
                    output.Sprite(input.Sprites.Gryphon[58]);
                    return;
                }

                if (input.Actor.GetStomachSize(16) >= 11)
                {
                    output.Sprite(input.Sprites.Gryphon[58]).Layer(7);
                }
            }
        });

        builder.RenderSingle(SpriteType.Balls, 13, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, input.Actor.Unit.SkinColor));
            int sz = input.Actor.GetStomachSize(16);
            int bz = input.Actor.GetBallSize(10, 1.5f);
            if (input.Actor.Unit.HasDick == false || input.Params.Position == Position.Standing)
            {
                return;
            }

            if (input.Actor.GetStomachSize(16) < 12 || sz < bz * 2)
            {
                output.Layer(13);
                if (input.Actor.PredatorComponent?.BallsFullness > 0)
                {
                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                    {
                        output.Sprite(input.Sprites.Gryphon[47]);
                        return;
                    }

                    output.Sprite(input.Sprites.Gryphon[36 + input.Actor.GetBallSize(10, 1.5f)]);
                    return;
                }

                output.Sprite(input.Sprites.Gryphon[36]);
                return;
            }

            output.Layer(8);
            if (input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                {
                    output.Sprite(input.Sprites.Gryphon[47]);
                    return;
                }

                output.Sprite(input.Sprites.Gryphon[36 + input.Actor.GetBallSize(10, 1.5f)]);
                return;
            }

            output.Sprite(input.Sprites.Gryphon[36]);
        });


        builder.RunBefore((input, output) =>
        {
            if (input.Actor.HasBelly || input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                output.Params.Position = Position.Sitting;
            }
            else
            {
                output.Params.Position = Position.Standing;
            }
            //base.RunFirst(data.Actor);

            if (input.Actor.Unit.Predator && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(16) == 16)
            {
                output.changeSprite(SpriteType.Body).AddOffset(0, 30 * .625f);
                output.changeSprite(SpriteType.Head).AddOffset(0, 30 * .625f);
                output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 30 * .625f);
                output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 30 * .625f);
                output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 30 * .625f);
                output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 30 * .625f);
                output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 30 * .625f);
                output.changeSprite(SpriteType.BodyAccent6).AddOffset(15 * .625f, 12 * .625f);
                output.changeSprite(SpriteType.BodyAccent7).AddOffset(0, 30 * .625f);
                output.changeSprite(SpriteType.BodyAccent8).AddOffset(0, 30 * .625f);
                output.changeSprite(SpriteType.Dick).AddOffset(0, 30 * .625f);
                output.changeSprite(SpriteType.Balls).AddOffset(20 * .625f, 10 * .625f);
            }
            else if (input.Actor.Unit.Predator && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(16, .8f) == 16)
            {
                output.changeSprite(SpriteType.Body).AddOffset(0, 20 * .625f);
                output.changeSprite(SpriteType.Head).AddOffset(0, 20 * .625f);
                output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 20 * .625f);
                output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 20 * .625f);
                output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 20 * .625f);
                output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 20 * .625f);
                output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 20 * .625f);
                output.changeSprite(SpriteType.BodyAccent6).AddOffset(15 * .625f, 2 * .625f);
                output.changeSprite(SpriteType.BodyAccent7).AddOffset(0, 20 * .625f);
                output.changeSprite(SpriteType.BodyAccent8).AddOffset(0, 20 * .625f);
                output.changeSprite(SpriteType.Dick).AddOffset(0, 20 * .625f);
                output.changeSprite(SpriteType.Balls).AddOffset(20 * .625f, 0);
            }
            else if (input.Actor.Unit.Predator && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(16, .9f) == 16)
            {
                output.changeSprite(SpriteType.Body).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.Head).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.BodyAccent6).AddOffset(15 * .625f, -8 * .625f);
                output.changeSprite(SpriteType.BodyAccent7).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.BodyAccent8).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.Dick).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.Balls).AddOffset(20 * .625f, -10 * .625f);
            }
            else
            {
                output.changeSprite(SpriteType.Balls).AddOffset(20 * .625f, -20 * .625f);
                output.changeSprite(SpriteType.BodyAccent6).AddOffset(15 * .625f, -18 * .625f);
            }
        });


        builder.RandomCustom(Defaults.RandomCustom);
    });

    private class PositionParameters : IParameters
    {
        internal Position Position;
    }

    private enum Position
    {
        Standing,
        Sitting
    }
}