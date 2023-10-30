internal static class Dragon
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank<DragonParameters>, builder =>
    {
        builder.Setup(output =>
        {
            output.GentleAnimation = true;
            output.SpecialAccessoryCount = 3;

            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Dragon);
            output.ClothingColors = 0;
        });

        builder.RenderSingle(SpriteType.Head, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, input.Actor.Unit.AccessoryColor));
            switch (input.Params.Position)
            {
                case Position.Down:
                    output.Sprite(input.Sprites.Dragon[3]);
                    return;
                case Position.Standing:
                    if (input.Actor.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Dragon[5]);
                        return;
                    }

                    output.Sprite(input.Sprites.Dragon[4]);
                    return;
                case Position.StandingCrouch:
                    if (input.Actor.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Dragon[7]);
                        return;
                    }

                    output.Sprite(input.Sprites.Dragon[6]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, input.Actor.Unit.AccessoryColor));
            switch (input.Params.Position)
            {
                case Position.Down:
                    output.Sprite(input.Sprites.Dragon[0]);
                    return;
                case Position.Standing:
                    output.Sprite(input.Sprites.Dragon[1]);
                    return;
                case Position.StandingCrouch:
                    output.Sprite(input.Sprites.Dragon[2]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            switch (input.Params.Position)
            {
                case Position.Down:
                    output.Sprite(input.Sprites.Dragon[11]);
                    return;
                case Position.Standing:
                    output.Sprite(input.Sprites.Dragon[8]);
                    return;
                default:
                    output.Sprite(input.Sprites.Dragon[9]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent2, 17, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            switch (input.Params.Position)
            {
                case Position.Standing:
                    if (input.Actor.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Dragon[13]);
                        return;
                    }

                    output.Sprite(input.Sprites.Dragon[12]);
                    return;
                case Position.StandingCrouch:
                    if (input.Actor.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Dragon[15]);
                        return;
                    }

                    output.Sprite(input.Sprites.Dragon[14]);
                    return;
                default:
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent3, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Params.Position == Position.Down)
            {
                output.Sprite(input.Sprites.Dragon[10]);
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent4, 17, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, input.Actor.Unit.AccessoryColor));
            if (input.Params.Position == Position.Down)
            {
                return;
            }

            if (input.Params.Position == Position.Standing)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Dragon[17]);
                    return;
                }

                output.Sprite(input.Sprites.Dragon[16]);
                return;
            }

            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Dragon[19]);
                return;
            }

            output.Sprite(input.Sprites.Dragon[18]);
        });

        builder.RenderSingle(SpriteType.BodyAccent5, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, input.Actor.Unit.AccessoryColor));
            switch (input.Params.Position)
            {
                case Position.Standing:
                    if (input.Actor.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Dragon[37]);
                        return;
                    }

                    output.Sprite(input.Sprites.Dragon[36]);
                    return;
                case Position.StandingCrouch:
                    if (input.Actor.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Dragon[38]);
                        return;
                    }

                    output.Sprite(input.Sprites.Dragon[39]);
                    return;
                default:
                    output.Sprite(input.Sprites.Dragon[35]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent6, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, input.Actor.Unit.AccessoryColor));
            switch (input.Params.Position)
            {
                case Position.Standing:
                    output.Sprite(input.Sprites.Dragon[41]);
                    return;
                case Position.StandingCrouch:
                    output.Sprite(input.Sprites.Dragon[42]);
                    return;
                default:
                    output.Sprite(input.Sprites.Dragon[40]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent7, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, input.Actor.Unit.AccessoryColor));
            switch (input.Params.Position)
            {
                case Position.Standing:
                    output.Sprite(input.Sprites.Dragon[44]);
                    return;
                case Position.StandingCrouch:
                    output.Sprite(input.Sprites.Dragon[45]);
                    return;
                default:
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent8, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, input.Actor.Unit.AccessoryColor));
            if (input.Params.Position == Position.Standing)
            {
                output.Sprite(input.Sprites.Dragon[49]);
            }
        });

        builder.RenderSingle(SpriteType.BodyAccessory, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, input.Actor.Unit.AccessoryColor));
            int sprite = 20 + 5 * input.Actor.Unit.SpecialAccessoryType;
            switch (input.Params.Position)
            {
                case Position.Standing:
                    if (input.Actor.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Dragon[sprite + 1]);
                        return;
                    }

                    output.Sprite(input.Sprites.Dragon[sprite + 2]);
                    return;
                case Position.StandingCrouch:
                    if (input.Actor.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Dragon[sprite + 3]);
                        return;
                    }

                    output.Sprite(input.Sprites.Dragon[sprite + 4]);
                    return;
                default:
                    output.Sprite(input.Sprites.Dragon[sprite]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodySize, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, input.Actor.Unit.AccessoryColor));
            switch (input.Params.Position)
            {
                case Position.Standing:
                    output.Sprite(input.Sprites.Dragon[47]);
                    return;
                case Position.StandingCrouch:
                    output.Sprite(input.Sprites.Dragon[48]);
                    return;
                default:
                    output.Sprite(input.Sprites.Dragon[46]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.Predator == false || input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Params.Position == Position.Standing || input.Params.Position == Position.StandingCrouch)
            {
                output.Layer(16);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                {
                    output.Sprite(input.Sprites.Dragon[69]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    if (input.Actor.GetStomachSize(17, 1.4f) == 17)
                    {
                        output.Sprite(input.Sprites.Dragon[68]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(17, 1.6f) == 17)
                    {
                        output.Sprite(input.Sprites.Dragon[67]);
                        return;
                    }
                }

                output.Sprite(input.Sprites.Dragon[50 + input.Actor.GetStomachSize(16, 1.75f)]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 15, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, input.Actor.Unit.AccessoryColor));
            if (input.Params.Position == Position.Down)
            {
                return;
            }

            if (input.Actor.GetStomachSize(16) > 1)
            {
                return;
            }

            if (input.Actor.Unit.DickSize >= 0)
            {
                output.Sprite(input.Sprites.Dragon[73 + input.Actor.Unit.DickSize]);
                return;
            }

            if (input.Actor.IsUnbirthing)
            {
                output.Sprite(input.Sprites.Dragon[72]);
                return;
            }

            output.Sprite(input.Sprites.Dragon[71]);
        });

        builder.RenderSingle(SpriteType.Balls, 11, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.HasDick == false || input.Params.Position == Position.Down)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                output.AddOffset(0, 1 * .625f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                {
                    output.Sprite(input.Sprites.Dragon[91]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    if (input.Actor.GetBallSize(14, 1.4f) == 14)
                    {
                        output.Sprite(input.Sprites.Dragon[90]);
                        return;
                    }

                    if (input.Actor.GetBallSize(14, 1.6f) == 14)
                    {
                        output.Sprite(input.Sprites.Dragon[89]);
                        return;
                    }
                }

                output.Sprite(input.Sprites.Dragon[75 + input.Actor.GetBallSize(13, 1.75f)]);
                return;
            }

            output.Sprite(input.Sprites.Dragon[75]);
        });


        builder.RunBefore((input, output) =>
        {
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Params.Position = Position.StandingCrouch;
                output.ChangeSprite(SpriteType.Belly).AddOffset(0, 14 * .625f);
            }
            else if (input.Actor.HasBelly || input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                output.Params.Position = Position.Standing;
            }
            else
            {
                output.Params.Position = Position.Down;
            }
            //base.RunFirst(actor);
        });

        builder.RandomCustom(Defaults.RandomCustom);
    });

    private enum Position
    {
        Down,
        Standing,
        StandingCrouch
    }

    private class DragonParameters : IParameters
    {
        internal Position Position;
    }
}