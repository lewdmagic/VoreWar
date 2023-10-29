#region

using System.Collections.Generic;

#endregion

internal static class FeralFrogs
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank<PositionParameters>, builder =>
    {
        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.Female };
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Frog);

            output.EyeTypes = 4;
            output.MouthTypes = 4;
        });


        builder.RenderSingle(SpriteType.Eyes, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            switch (input.Params.Position)
            {
                case Position.Front:
                    output.Sprite(input.Sprites.Frogs[1 + input.Actor.Unit.EyeType]);
                    return;
                case Position.Pouncing:
                    return;
                case Position.Standing:
                    output.Sprite(input.Sprites.Frogs[17 + input.Actor.Unit.EyeType]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.Mouth, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Frog, input.Actor.Unit.AccessoryColor));
            switch (input.Params.Position)
            {
                case Position.Front:
                    output.Sprite(input.Sprites.Frogs[5 + input.Actor.Unit.MouthType]);
                    return;
                case Position.Pouncing:
                    return;
                case Position.Standing:
                    output.Sprite(input.Sprites.Frogs[21 + input.Actor.Unit.MouthType]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Frog, input.Actor.Unit.AccessoryColor));
            switch (input.Params.Position)
            {
                case Position.Front:
                    output.Sprite(input.Sprites.Frogs[0]);
                    return;
                case Position.Pouncing:
                    output.Sprite(input.Sprites.Frogs[32]);
                    return;
                case Position.Standing:
                    output.Sprite(input.Sprites.Frogs[10]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Frog, input.Actor.Unit.AccessoryColor));
            switch (input.Params.Position)
            {
                case Position.Front:
                    return;
                case Position.Pouncing:
                    return;
                case Position.Standing:
                    if (input.Actor.HasBelly)
                    {
                        output.Sprite(input.Sprites.Frogs[13 + input.Actor.GetStomachSize(3, .75f)]);
                    }

                    break;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent2, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Frog, input.Actor.Unit.AccessoryColor));
            switch (input.Params.Position)
            {
                case Position.Front:
                    if (input.Actor.HasBelly)
                    {
                        output.Sprite(input.Sprites.Frogs[33 + input.Actor.GetStomachSize(3, .75f)]);
                    }

                    break;
                case Position.Pouncing:
                    if (input.Actor.HasBelly)
                    {
                        output.Sprite(input.Sprites.Frogs[27 + input.Actor.GetStomachSize(3, .75f)]);
                    }

                    break;
                case Position.Standing:
                    if (input.Actor.HasBelly)
                    {
                        output.Sprite(input.Sprites.Frogs[11 + input.Actor.GetStomachSize(2, .75f)]);
                    }

                    break;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent3, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Frog, input.Actor.Unit.AccessoryColor));
            if (input.Actor.IsOralVoring == false)
            {
                return;
            }

            if (input.Actor.IsOralVoringHalfOver == false)
            {
                return;
            }

            switch (input.Params.Position)
            {
                case Position.Front:
                    if (input.Actor.HasBelly)
                    {
                        output.Sprite(input.Sprites.Frogs[9]);
                    }

                    break;
                case Position.Pouncing:
                    return;
                case Position.Standing:
                    if (input.Actor.HasBelly)
                    {
                        output.Sprite(input.Sprites.Frogs[26]);
                    }

                    break;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent4, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Params.Position == Position.Front && input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Frogs[38]);
                return;
            }

            if (input.Actor.IsOralVoring == false)
            {
                return;
            }

            if (input.Actor.IsOralVoringHalfOver)
            {
                return;
            }

            switch (input.Params.Position)
            {
                case Position.Front:
                    if (input.Actor.HasBelly)
                    {
                        output.Sprite(input.Sprites.Frogs[38]);
                    }

                    break;
                case Position.Pouncing:
                    return;
                case Position.Standing:
                    if (input.Actor.HasBelly && input.Actor.GetStomachSize(3, .75f) != 3)
                    {
                        output.Sprite(input.Sprites.Frogs[25]);
                    }

                    break;
            }
        });


        builder.RunBefore((input, output) =>
        {
            if (input.Actor.IsPouncingFrog)
            {
                output.Params.Position = Position.Pouncing;
            }
            else if (input.Actor.IsEating)
            {
                output.Params.Position = Position.Standing;
            }
            else
            {
                output.Params.Position = Position.Front;
            }

            Defaults.Finalize.Invoke(input, output);
        });

        builder.RandomCustom(Defaults.RandomCustom);
    });

    private class PositionParameters : IParameters
    {
        internal Position Position;
    }

    private enum Position
    {
        Front,
        Pouncing,
        Standing
    }
}