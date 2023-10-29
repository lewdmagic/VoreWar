#region

using System;
using System.Collections.Generic;

#endregion

internal static class Zera
{
    private static int StomachSize;

    private static BodyStateType BodyState;


    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        int[] BallsLow = { 0, 1, 2, 3, 4, 5, 6, 7, 9, 10, 11, 12, 17, 18, 19, 20, 21, 22, 35, 34, 33, 32 }; //8 is cut out so the lengths match
        int[] BallsMedium = { 0, 1, 2, 3, 4, 5, 6, 7, 13, 14, 15, 16, 23, 24, 25, 20, 21, 22, 35, 34, 33, 32 };
        int[] BallsHigh = { 0, 1, 2, 3, 4, 5, 6, 7, 13, 14, 15, 16, 26, 27, 28, 29, 30, 31, 35, 34, 33, 32 };


        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.Male };
            output.GentleAnimation = true;
            output.ClothingColors = 0;
            output.TailTypes = 2;
        });


        builder.RenderSingle(SpriteType.Head, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            switch (BodyState)
            {
                case BodyStateType.Third:
                    if (input.Actor.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Zera240[36]);
                        return;
                    }

                    output.Sprite(input.Sprites.Zera240[35]);
                    return;
                case BodyStateType.Second:
                    if (input.Actor.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Zera240[22]);
                        return;
                    }

                    output.Sprite(input.Sprites.Zera240[21]);
                    return;
                default:
                    if (input.Actor.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Zera240[5]);
                        return;
                    }

                    output.Sprite(input.Sprites.Zera240[4]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.Body, -1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            switch (BodyState)
            {
                case BodyStateType.Third:
                    return;
                case BodyStateType.Second:
                    return;
                default:
                    output.Sprite(input.Sprites.Zera240[0]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            switch (BodyState)
            {
                case BodyStateType.Third:
                    return;
                case BodyStateType.Second:
                    output.Sprite(input.Sprites.Zera240[20]);
                    return;
                default:
                    output.Sprite(input.Sprites.Zera240[3]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent2, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            switch (BodyState)
            {
                case BodyStateType.Third:
                    output.Sprite(input.Sprites.Zera240[38]);
                    return;
                case BodyStateType.Second:
                    if (StomachSize > 10)
                    {
                        output.Sprite(input.Sprites.Zera240[24]).AddOffset(0, -23 * .41666f);
                        return;
                    }

                    output.Sprite(input.Sprites.Zera240[19]);
                    return;
                default:
                    output.Sprite(input.Sprites.Zera240[9]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent3, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            switch (BodyState)
            {
                case BodyStateType.Third:
                    output.Sprite(input.Sprites.Zera240[34]);
                    return;
                case BodyStateType.Second:
                    if (StomachSize > 10)
                    {
                        return;
                    }

                    output.Sprite(input.Sprites.Zera240[23]);
                    return;
                default:
                    output.Sprite(input.Sprites.Zera240[2]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent4, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            switch (BodyState)
            {
                case BodyStateType.Third:
                    output.Sprite(input.Sprites.Zera240[37]);
                    return;
                case BodyStateType.Second:
                    return;
                default:
                    output.Sprite(input.Sprites.Zera240[1]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent5, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            switch (BodyState)
            {
                case BodyStateType.Third:
                    return;
                case BodyStateType.Second:
                    output.Sprite(input.Sprites.Zera240[17]);
                    return;
                default:
                    output.Sprite(input.Sprites.Zera240[8]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent6, 0, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            switch (BodyState)
            {
                case BodyStateType.Third:
                    return;
                case BodyStateType.Second:
                    output.Sprite(input.Sprites.Zera240[18]);
                    return;
                default:
                    output.Sprite(input.Sprites.Zera240[6]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent7, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            switch (BodyState)
            {
                case BodyStateType.Third:
                    return;
                case BodyStateType.Second:
                    return;
                default:
                    output.Sprite(input.Sprites.Zera240[7]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.Belly, 2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.HasBelly == false)
            {
                return;
            }


            if (StomachSize < 7 && BodyState == BodyStateType.Second)
            {
                if (StomachSize < 2)
                {
                    return;
                }

                output.Sprite(input.Sprites.Zera240[27 + StomachSize]);
                return;
            }

            if (BodyState == BodyStateType.Third)
            {
                if (StomachSize == 19 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
                {
                    output.Sprite(input.Sprites.ZeraFrontBelly[12]);
                    return;
                }

                if (StomachSize > 16 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) == false)
                {
                    StomachSize = 16;
                }

                if (StomachSize > 11)
                {
                }
                else
                {
                    output.Sprite(input.Sprites.ZeraFrontBelly[Math.Min(StomachSize - 7, 11)]).AddOffset(0, -80 * .625f);
                    return;
                }
            }

            if (StomachSize < 7)
            {
                output.Sprite(input.Sprites.Zera240[10 + StomachSize]);
                return;
            }

            if (StomachSize < 10)
            {
                output.Sprite(input.Sprites.Zera240[18 + StomachSize]);
                return;
            }

            if (StomachSize == 19 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.ZeraBelly[9]);
                return;
            }

            if (StomachSize == 19 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) == false)
            {
                StomachSize = 18;
            }


            if (StomachSize > 17 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) == false)
            {
                StomachSize = 17;
            }

            output.Sprite(input.Sprites.ZeraBelly[StomachSize - 10]);
        });

        builder.RenderSingle(SpriteType.Dick, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (BodyState != BodyStateType.Second)
            {
                return;
            }

            output.Sprite(input.Sprites.Zera240[28]);
        });

        builder.RenderSingle(SpriteType.Balls, 9, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (BodyState != BodyStateType.Second)
            {
                return;
            }

            int ballIndex = input.Actor.GetBallSize(21);
            int ballSprite;
            //int ballSprite = input.Actor.Unit.BodyAccentType2;

            if (input.Actor.Unit.Predator)
            {
                if (ballIndex > 18 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) == false)
                {
                    ballIndex = 18;
                }

                if (ballIndex == 21 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) == false)
                {
                    ballIndex = 20;
                }
            }


            if (StomachSize >= 10)
            {
                ballSprite = BallsHigh[ballIndex];
            }
            else if (StomachSize >= 7)
            {
                ballSprite = BallsMedium[ballIndex];
            }
            else
            {
                ballSprite = BallsLow[ballIndex];
            }


            if (ballSprite <= 8)
            {
            }
            else if (ballSprite <= 12)
            {
                output.AddOffset(80 * .625f, 0);
            }
            else if (ballSprite <= 31)
            {
                output.AddOffset(80 * .625f, -80 * .625f);
            }
            else if (ballSprite == 32)
            {
                output.AddOffset(160 * .625f, -102.4f * .625f);
            }
            else
            {
                output.AddOffset(160 * .625f, -81.6f * .625f);
            }

            output.Sprite(input.Sprites.ZeraBalls[ballSprite]);
        });


        builder.RunBefore((input, output) =>
        {
            StomachSize = input.Actor.GetStomachSize(19);
            //StomachSize = actor.Unit.BodyAccentType1;
            if (StomachSize >= 7 && input.Actor.PredatorComponent?.BallsFullness == 0)
            {
                BodyState = BodyStateType.Third;
            }
            else if (StomachSize >= 7 || input.Actor.Unit.TailType == 1 || input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                BodyState = BodyStateType.Second;
            }
            else
            {
                BodyState = BodyStateType.First;
            }

            adjustBodyOffsets(input, output);

            if (StomachSize >= 10 && BodyState == BodyStateType.Second)
            {
                float offset = 110 * .41666667f;
                output.changeSprite(SpriteType.Head).AddOffset(0, offset);
                output.changeSprite(SpriteType.Body).AddOffset(0, offset);
                output.changeSprite(SpriteType.BodyAccent).AddOffset(0, offset);
                output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, offset);
                output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, offset);
                output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, offset);
                output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, offset);
                output.changeSprite(SpriteType.BodyAccent6).AddOffset(0, offset);
                output.changeSprite(SpriteType.BodyAccent7).AddOffset(0, offset);
                output.changeSprite(SpriteType.Balls).AddOffset(0, offset);
                output.changeSprite(SpriteType.Dick).AddOffset(0, offset);
            }
            else if (StomachSize >= 7 && BodyState == BodyStateType.Second)
            {
                output.changeSprite(SpriteType.Belly).AddOffset(0, -62 * .41666667f);
            }
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.Name = "Zera";
        });


        //BodyAccentTypes1 = 19;
        //BodyAccentTypes2 = 35;
    });

    // Extracted from BellySprite
    private static void adjustBodyOffsets(IRunInput input, IRunOutput output)
    {
        if (input.Actor.HasBelly == false)
        {
            return;
        }

        if (StomachSize < 7 && BodyState == BodyStateType.Second)
        {
            if (StomachSize < 2)
            {
                return;
            }

            return;
        }

        if (BodyState == BodyStateType.Third)
        {
            if (StomachSize == 19 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                SetThirdOffset(59 * .416667f);
                return;
            }

            if (StomachSize > 16 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) == false)
            {
                StomachSize = 16;
            }

            if (StomachSize > 11)
            {
                SetThirdOffset(59 * .416667f);
            }
            else
            {
                return;
            }
        }

        if (StomachSize < 7)
        {
            return;
        }

        if (StomachSize < 10)
        {
            return;
        }

        if (StomachSize == 19 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
        {
            return;
        }

        if (StomachSize == 19 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) == false)
        {
            StomachSize = 18;
        }


        if (StomachSize > 17 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) == false)
        {
            StomachSize = 17;
        }

        return;

        void SetThirdOffset(float y)
        {
            output.changeSprite(SpriteType.Head).AddOffset(0, y);
            output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, y);
            output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, y);
            output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, y);
        }
    }

    private enum BodyStateType
    {
        First,
        Second,
        Third
    }
}