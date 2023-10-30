internal static class Gazelle
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.SpecialAccessoryCount = 8; // ears
            output.BodyAccentTypes1 = 8; // fur patterns
            output.BodyAccentTypes2 = 10; // horns (for males)
            output.TailTypes = 6;
            output.ClothingColors = 0;
            output.GentleAnimation = true;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.GazelleSkin);
            output.EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);
        });


        builder.RenderSingle(SpriteType.Head, 11, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Gazelle1[24 + (input.Actor.IsAttacking || input.Actor.IsEating ? 1 : 0) + 2 * input.Actor.Unit.BodyAccentType1]);
        });
        builder.RenderSingle(SpriteType.Eyes, 13, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Gazelle1[70]);
        });
        builder.RenderSingle(SpriteType.Mouth, 13, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Gazelle1[71 + (input.Actor.IsAttacking || input.Actor.IsEating ? 1 : 0)]);
        });
        builder.RenderSingle(SpriteType.Body, 10, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Gazelle1[0 + input.Actor.Unit.BodyAccentType1]);
        });
        builder.RenderSingle(SpriteType.BodyAccent, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Gazelle1[9 + input.Actor.Unit.BodyAccentType1 * 2]);
        }); // legs1
        builder.RenderSingle(SpriteType.BodyAccent2, 0, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Gazelle1[8 + input.Actor.Unit.BodyAccentType1 * 2]);
        }); // legs2
        builder.RenderSingle(SpriteType.BodyAccent3, 10, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Gazelle1[40]);
        }); // hoof1
        builder.RenderSingle(SpriteType.BodyAccent4, 0, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Gazelle1[41]);
        }); // hoof2
        builder.RenderSingle(SpriteType.BodyAccent5, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Gazelle1[42]);
        }); // hoof3
        builder.RenderSingle(SpriteType.BodyAccent6, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Gazelle1[43 + input.Actor.Unit.TailType]);
        }); // tail
        builder.RenderSingle(SpriteType.BodyAccent7, 15, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasDick)
            {
                output.Sprite(input.Sprites.Gazelle1[60 + input.Actor.Unit.BodyAccentType2]);
            }
        }); // horns

        builder.RenderSingle(SpriteType.BodyAccent8, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, input.Actor.Unit.SkinColor));
            if (Config.HideCocks)
            {
                return;
            }

            if (input.Actor.Unit.HasDick)
            {
                output.Sprite(input.Sprites.Gazelle2[31]);
            }
        }); // sheath

        builder.RenderSingle(SpriteType.BodyAccent9, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Gazelle1[49]);
        }); // belly cover
        builder.RenderSingle(SpriteType.BodyAccessory, 13, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, input.Actor.Unit.SkinColor));
            if ((input.Actor.Unit.BodyAccentType1 == 1 || input.Actor.Unit.BodyAccentType1 == 5) && input.Actor.Unit.SpecialAccessoryType == 3)
            {
                output.Sprite(input.Sprites.Gazelle1[58]);
                return;
            }

            if ((input.Actor.Unit.BodyAccentType1 == 1 || input.Actor.Unit.BodyAccentType1 == 5) && input.Actor.Unit.SpecialAccessoryType == 5)
            {
                output.Sprite(input.Sprites.Gazelle1[59]);
                return;
            }

            output.Sprite(input.Sprites.Gazelle1[50 + input.Actor.Unit.SpecialAccessoryType]);
        }); // ears

        builder.RenderSingle(SpriteType.Belly, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, input.Actor.Unit.SkinColor));
            output.Layer(input.Actor.GetStomachSize(27) > 9 ? 9 : 6);

            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
            {
                output.Sprite(input.Sprites.Gazelle2[30]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
            {
                if (input.Actor.GetStomachSize(27, .8f) == 27)
                {
                    output.Sprite(input.Sprites.Gazelle2[29]);
                    return;
                }

                if (input.Actor.GetStomachSize(27, .9f) == 27)
                {
                    output.Sprite(input.Sprites.Gazelle2[28]);
                    return;
                }
            }

            output.Sprite(input.Sprites.Gazelle2[0 + input.Actor.GetStomachSize(27)]);
        });

        builder.RenderSingle(SpriteType.Dick, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect())
            {
                output.Sprite(input.Sprites.Gazelle2[32]);
            }
        });

        builder.RenderSingle(SpriteType.Balls, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                {
                    output.Sprite(input.Sprites.Gazelle2[66]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    if (input.Actor.GetBallSize(30, .8f) == 30)
                    {
                        output.Sprite(input.Sprites.Gazelle2[65]);
                        return;
                    }

                    if (input.Actor.GetBallSize(30, .9f) == 30)
                    {
                        output.Sprite(input.Sprites.Gazelle2[64]);
                        return;
                    }
                }

                output.Sprite(input.Sprites.Gazelle2[33 + input.Actor.GetBallSize(30)]);
                return;
            }

            output.Sprite(input.Sprites.Gazelle2[33]);
        });


        builder.RunBefore((input, output) =>
        {
            output.changeSprite(SpriteType.Balls).AddOffset(-30 * .625f, -45 * .625f);
            output.changeSprite(SpriteType.Belly).AddOffset(-10 * .625f, -40 * .625f);
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;


            unit.BodyAccentType1 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes1);
            unit.BodyAccentType2 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes2);
            unit.TailType = State.Rand.Next(data.MiscRaceData.TailTypes);
        });
    });
}