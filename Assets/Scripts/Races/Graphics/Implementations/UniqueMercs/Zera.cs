#region

using System;
using System.Collections.Generic;

#endregion

// TODO Code is immune to refactoring. Needs a nearly full rewrite
internal static class Zera
{

    internal class ZeraParameters : IParameters
    {
        internal int StomachSize;
        internal BodyStateType BodyState;
    }
    

    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank<ZeraParameters>, builder =>
    {
        builder.Names("Zera", "Zera");
        builder.FlavorText(new FlavorText(
            new Texts {  },
            new Texts {  },
            new Texts { "nargacuga", "fluffy wyvern", "big kitty" } //new, many thanks to Selicia for the last two
        ));
        builder.RaceTraits(new RaceTraits()
        {
            BodySize = 24,
            StomachSize = 30,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.CockVore },
            ExpMultiplier = 2.4f,
            PowerAdjustment = 4f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(20, 24),
                Dexterity = new RaceStats.StatRange(6, 10),
                Endurance = new RaceStats.StatRange(16, 24),
                Mind = new RaceStats.StatRange(16, 20),
                Will = new RaceStats.StatRange(12, 18),
                Agility = new RaceStats.StatRange(16, 28),
                Voracity = new RaceStats.StatRange(16, 24),
                Stomach = new RaceStats.StatRange(16, 24),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.StrongGullet,
                Traits.ArtfulDodge,
                Traits.NimbleClimber,
                Traits.BornToMove
            },
            RaceDescription = "A devious and voracious wyvern. Known for his agility and cunning, don't ever turn your back to him or you might find yourself in trouble.",
        });
        builder.CustomizeButtons((unit, buttons) =>
        {
            buttons.SetText(ButtonType.TailTypes, "Default Facing");
        });
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
            switch (input.Params.BodyState)
            {
                case BodyStateType.Third:
                    if (input.A.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Zera240[36]);
                        return;
                    }

                    output.Sprite(input.Sprites.Zera240[35]);
                    return;
                case BodyStateType.Second:
                    if (input.A.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Zera240[22]);
                        return;
                    }

                    output.Sprite(input.Sprites.Zera240[21]);
                    return;
                default:
                    if (input.A.IsOralVoring)
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
            switch (input.Params.BodyState)
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
            switch (input.Params.BodyState)
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
            switch (input.Params.BodyState)
            {
                case BodyStateType.Third:
                    output.Sprite(input.Sprites.Zera240[38]);
                    return;
                case BodyStateType.Second:
                    if (input.Params.StomachSize > 10)
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
            switch (input.Params.BodyState)
            {
                case BodyStateType.Third:
                    output.Sprite(input.Sprites.Zera240[34]);
                    return;
                case BodyStateType.Second:
                    if (input.Params.StomachSize > 10)
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
            switch (input.Params.BodyState)
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
            switch (input.Params.BodyState)
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
            switch (input.Params.BodyState)
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
            switch (input.Params.BodyState)
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
            if (input.A.HasBelly == false)
            {
                return;
            }


            if (input.Params.StomachSize < 7 && input.Params.BodyState == BodyStateType.Second)
            {
                if (input.Params.StomachSize < 2)
                {
                    return;
                }

                output.Sprite(input.Sprites.Zera240[27 + input.Params.StomachSize]);
                return;
            }

            if (input.Params.BodyState == BodyStateType.Third)
            {
                if (input.Params.StomachSize == 19 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
                {
                    output.Sprite(input.Sprites.ZeraFrontBelly[12]);
                    return;
                }

                if (input.Params.StomachSize > 16 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) == false)
                {
                    input.Params.StomachSize = 16;
                }

                if (input.Params.StomachSize > 11)
                {
                }
                else
                {
                    output.Sprite(input.Sprites.ZeraFrontBelly[Math.Min(input.Params.StomachSize - 7, 11)]).AddOffset(0, -80 * .625f);
                    return;
                }
            }

            if (input.Params.StomachSize < 7)
            {
                output.Sprite(input.Sprites.Zera240[10 + input.Params.StomachSize]);
                return;
            }

            if (input.Params.StomachSize < 10)
            {
                output.Sprite(input.Sprites.Zera240[18 + input.Params.StomachSize]);
                return;
            }

            if (input.Params.StomachSize == 19 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.ZeraBelly[9]);
                return;
            }

            if (input.Params.StomachSize == 19 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) == false)
            {
                input.Params.StomachSize = 18;
            }


            if (input.Params.StomachSize > 17 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) == false)
            {
                input.Params.StomachSize = 17;
            }

            output.Sprite(input.Sprites.ZeraBelly[input.Params.StomachSize - 10]);
        });

        builder.RenderSingle(SpriteType.Dick, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Params.BodyState != BodyStateType.Second)
            {
                return;
            }

            output.Sprite(input.Sprites.Zera240[28]);
        });

        builder.RenderSingle(SpriteType.Balls, 9, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Params.BodyState != BodyStateType.Second)
            {
                return;
            }

            int ballIndex = input.A.GetBallSize(21);
            int ballSprite;
            //int ballSprite = input.U.BodyAccentType2;

            if (input.U.Predator)
            {
                if (ballIndex > 18 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) == false)
                {
                    ballIndex = 18;
                }

                if (ballIndex == 21 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) == false)
                {
                    ballIndex = 20;
                }
            }


            if (input.Params.StomachSize >= 10)
            {
                ballSprite = BallsHigh[ballIndex];
            }
            else if (input.Params.StomachSize >= 7)
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


        // TODO this is disaster. 
        builder.RunBefore((input, output) =>
        {
            int stomachSize = input.A.GetStomachSize(19);
            BodyStateType bodyState;
            //input.Params.StomachSize = actor.Unit.BodyAccentType1;
            if (stomachSize >= 7 && input.A.PredatorComponent?.BallsFullness == 0)
            {
                bodyState = BodyStateType.Third;
            }
            else if (stomachSize >= 7 || input.U.TailType == 1 || input.A.PredatorComponent?.BallsFullness > 0)
            {
                bodyState = BodyStateType.Second;
            }
            else
            {
                bodyState = BodyStateType.First;
            }

            /////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////

            if (!(bodyState == BodyStateType.Third && stomachSize == 19 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach)))
            {
                if (bodyState == BodyStateType.Third)
                {
                    if (stomachSize > 16 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) == false)
                    {
                        stomachSize = 16;
                    }
                }
            
                if (stomachSize == 19 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
                {
                    // Nothing ? I think
                }

                if (stomachSize == 19 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) == false)
                {
                    stomachSize = 18;
                }

                if (stomachSize > 17 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) == false)
                {
                    stomachSize = 17;
                }
            }
            
            /////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////
            
            output.Params.BodyState = bodyState;
            output.Params.StomachSize = stomachSize;
            
            
            
            AdjustBodyOffsets(input, output, bodyState, stomachSize);

            if (stomachSize >= 10 && bodyState == BodyStateType.Second)
            {
                float offset = 110 * .41666667f;
                output.ChangeSprite(SpriteType.Head).AddOffset(0, offset);
                output.ChangeSprite(SpriteType.Body).AddOffset(0, offset);
                output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, offset);
                output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, offset);
                output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, offset);
                output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, offset);
                output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, offset);
                output.ChangeSprite(SpriteType.BodyAccent6).AddOffset(0, offset);
                output.ChangeSprite(SpriteType.BodyAccent7).AddOffset(0, offset);
                output.ChangeSprite(SpriteType.Balls).AddOffset(0, offset);
                output.ChangeSprite(SpriteType.Dick).AddOffset(0, offset);
            }
            else if (stomachSize >= 7 && bodyState == BodyStateType.Second)
            {
                output.ChangeSprite(SpriteType.Belly).AddOffset(0, -62 * .41666667f);
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
    private static void AdjustBodyOffsets(IRunInput input, IRunOutput output, BodyStateType bodyState, int stomachSize)
    {
        if (input.A.HasBelly == false)
        {
            return;
        }

        if (bodyState == BodyStateType.Third)
        {
            if (stomachSize == 19 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                SetThirdOffset(59 * .416667f);
                return;
            }

            if (stomachSize > 16 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) == false)
            {
                stomachSize = 16;
            }

            if (stomachSize > 11)
            {
                SetThirdOffset(59 * .416667f);
            }
        }

        void SetThirdOffset(float y)
        {
            output.ChangeSprite(SpriteType.Head).AddOffset(0, y);
            output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, y);
            output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, y);
            output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, y);
        }
    }

    // Extracted from BellySprite
    private static void adjustBodyOffsets222(IRunInput input, IRunOutput output, BodyStateType bodyState, int stomachSize)
    {
        if (input.A.HasBelly == false)
        {
            return;
        }

        if (stomachSize < 7 && bodyState == BodyStateType.Second)
        {
            if (stomachSize < 2)
            {
                return;
            }

            return;
        }

        if (bodyState == BodyStateType.Third)
        {
            if (stomachSize == 19 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                SetThirdOffset(59 * .416667f);
                return;
            }

            if (stomachSize > 16 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) == false)
            {
                stomachSize = 16;
            }

            if (stomachSize > 11)
            {
                SetThirdOffset(59 * .416667f);
            }
            else
            {
                return;
            }
        }

        if (stomachSize == 19 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
        {
            return;
        }

        if (stomachSize == 19 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) == false)
        {
            stomachSize = 18;
        }


        if (stomachSize > 17 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) == false)
        {
            stomachSize = 17;
        }

        return;

        void SetThirdOffset(float y)
        {
            output.ChangeSprite(SpriteType.Head).AddOffset(0, y);
            output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, y);
            output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, y);
            output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, y);
        }
    }

    internal enum BodyStateType
    {
        First,
        Second,
        Third
    }
}