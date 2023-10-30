#region

using System.Collections.Generic;

#endregion

internal static class YoungWyvern
{
    private const float StomachGainDivisor = 1.2f; //Higher is faster, should be balanced with stomach size to max out at 80-100 capacity

    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.None };
            output.GentleAnimation = true;
            output.SkinColors = ColorMap.SlimeColorCount;
            output.AccessoryColors = ColorMap.SlimeColorCount;
        });


        builder.RenderSingle(SpriteType.Mouth, 10, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Actor.PredatorComponent?.VisibleFullness >= 5 / StomachGainDivisor && (input.Actor.IsEating || input.Actor.IsAttacking) == false ? input.Sprites.YoungWyvern[17] : null);
        });
        builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
        {
            output.Coloring(ColorMap.GetSlimeColor(input.Actor.Unit.SkinColor));
            if (input.Actor.IsEating || input.Actor.IsAttacking)
            {
                if (input.Actor.PredatorComponent?.VisibleFullness < 0.5f / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[1]);
                    return;
                }

                if (input.Actor.PredatorComponent?.VisibleFullness < 1.75f / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[5]);
                    return;
                }

                if (input.Actor.PredatorComponent?.VisibleFullness < 3 / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[8]);
                    return;
                }

                if (input.Actor.PredatorComponent?.VisibleFullness < 4 / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[11]);
                    return;
                }

                if (input.Actor.PredatorComponent?.VisibleFullness < 5 / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[13]);
                    return;
                }

                output.Sprite(input.Sprites.YoungWyvern[15]);
                return;
            }

            if (input.Actor.HasBelly == false)
            {
                output.Sprite(input.Sprites.YoungWyvern[0]);
                return;
            }

            if (input.Actor.PredatorComponent?.VisibleFullness < 0.5f / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[2]);
                return;
            }

            if (input.Actor.PredatorComponent?.VisibleFullness < 1.75f / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[4]);
                return;
            }

            if (input.Actor.PredatorComponent?.VisibleFullness < 3 / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[7]);
                return;
            }

            if (input.Actor.PredatorComponent?.VisibleFullness < 4 / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[10]);
                return;
            }

            if (input.Actor.PredatorComponent?.VisibleFullness < 5 / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[12]);
                return;
            }

            output.Sprite(input.Sprites.YoungWyvern[14]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
        {
            output.Coloring(ColorMap.GetSlimeColor(input.Actor.Unit.AccessoryColor));
            if (input.Actor.PredatorComponent?.VisibleFullness < 0.5f / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[3]).Layer(5).Coloring(ColorMap.GetSlimeColor(input.Actor.Unit.AccessoryColor));
                return;
            }

            output.Coloring(ColorMap.GetSlimeColor(input.Actor.Unit.SkinColor));
            output.Layer(0);
            if (input.Actor.PredatorComponent?.VisibleFullness < 1.75f / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[29]);
                return;
            }

            if (input.Actor.PredatorComponent?.VisibleFullness < 3 / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[6]);
                return;
            }

            if (input.Actor.PredatorComponent?.VisibleFullness < 4 / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[9]);
                return;
            }

            if (input.Actor.PredatorComponent?.VisibleFullness >= 5 / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[16]);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 2, (input, output) =>
        {
            output.Coloring(ColorMap.GetSlimeColor(input.Actor.Unit.AccessoryColor));
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            output.Layer(8);
            if (input.Actor.PredatorComponent.VisibleFullness < 0.2 / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[18]);
                return;
            }

            if (input.Actor.PredatorComponent.VisibleFullness < 0.5 / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[19]);
                return;
            }

            if (input.Actor.PredatorComponent.VisibleFullness < 1.2 / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[20]);
                return;
            }

            if (input.Actor.PredatorComponent.VisibleFullness < 1.75 / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[21]);
                return;
            }

            if (input.Actor.PredatorComponent.VisibleFullness < 2.5 / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[22]);
                return;
            }

            if (input.Actor.PredatorComponent.VisibleFullness < 3 / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[23]);
                return;
            }

            if (input.Actor.PredatorComponent.VisibleFullness < 3.5 / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[24]);
                return;
            }

            output.Layer(2);
            if (input.Actor.PredatorComponent.VisibleFullness > 5 / StomachGainDivisor && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true))
            {
                output.Sprite(input.Sprites.YoungWyvern[31]);
                return;
            }

            if (input.Actor.PredatorComponent.VisibleFullness > 5 / StomachGainDivisor && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false))
            {
                output.Sprite(input.Sprites.YoungWyvern[30]);
                return;
            }

            if (input.Actor.PredatorComponent.VisibleFullness < 4 / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[25]);
                return;
            }

            if (input.Actor.PredatorComponent.VisibleFullness < 4.5 / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[26]);
                return;
            }

            if (input.Actor.PredatorComponent.VisibleFullness < 5 / StomachGainDivisor)
            {
                output.Sprite(input.Sprites.YoungWyvern[27]);
                return;
            }

            output.Sprite(input.Sprites.YoungWyvern[28]);
        });

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(Defaults.RandomCustom);
    });
}