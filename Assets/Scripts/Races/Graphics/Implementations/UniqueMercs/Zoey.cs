#region

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

internal static class Zoey
{
    internal static BodyState bodyState = BodyState.Normal;

    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        RaceFrameList SpinEffect = new RaceFrameList(new int[2] { 25, 19 }, new float[2] { .375f, .375f });
        builder.Setup(output =>
        {
            output.BreastSizes = () => 5;

            output.CanBeGender = new List<Gender> { Gender.Female };
            output.GentleAnimation = true;
            output.ClothingColors = 0;

            output.AllowedMainClothingTypes.Set(
                ZoeyTop.ZoeyTopInstance
            );
        });


        builder.RenderSingle(SpriteType.Head, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            switch (bodyState)
            {
                case BodyState.SpinAttack:
                case BodyState.SideBelly:
                    return;
                default:
                    if (input.Actor.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Zoey[3]);
                        return;
                    }

                    if (input.Actor.PredatorComponent?.Fullness > 2)
                    {
                        if (State.Rand.Next(650) == 0)
                        {
                            input.Actor.SetAnimationMode(1, .5f);
                        }

                        int specialMode = input.Actor.CheckAnimationFrame();
                        if (specialMode == 1)
                        {
                            output.Sprite(input.Sprites.Zoey[5]);
                            return;
                        }

                        output.Sprite(input.Sprites.Zoey[4]);
                        return;
                    }

                    output.Sprite(input.Sprites.Zoey[2]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.Hair, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            switch (bodyState)
            {
                case BodyState.SpinAttack:
                case BodyState.SideBelly:
                    return;
                default:
                    output.Sprite(input.Sprites.Zoey[7]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.Hair2, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            switch (bodyState)
            {
                case BodyState.SpinAttack:
                case BodyState.SideBelly:
                    return;
                default:
                    output.Sprite(input.Sprites.Zoey[8]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Layer(1);
            switch (bodyState)
            {
                case BodyState.HighBelly:
                    if (input.Actor.IsAttacking == false)
                    {
                        output.Sprite(input.Sprites.Zoey[9]);
                        return;
                    }

                    output.Sprite(input.Sprites.Zoey[10]);
                    return;
                case BodyState.SideBelly:
                    output.Sprite(input.Sprites.Zoey[12]).Layer(4);
                    return;
                case BodyState.SpinAttack:
                    output.Sprite(input.Sprites.Zoey[6]);
                    return;
                default:
                    if (input.Actor.IsAttacking == false)
                    {
                        output.Sprite(input.Sprites.Zoey[0]);
                        return;
                    }

                    output.Sprite(input.Sprites.Zoey[1]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent, 0, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            switch (bodyState)
            {
                case BodyState.HighBelly:
                    output.Sprite(input.Sprites.Zoey[11]);
                    return;
                case BodyState.SideBelly:
                    output.Sprite(input.Sprites.Zoey[13]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent2, 14, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.AnimationController.frameLists[0].currentlyActive)
            {
                if (input.Actor.AnimationController.frameLists[0].currentTime >= SpinEffect.Times[input.Actor.AnimationController.frameLists[0].currentFrame])
                {
                    input.Actor.AnimationController.frameLists[0].currentFrame++;
                    input.Actor.AnimationController.frameLists[0].currentTime = 0f;

                    if (input.Actor.AnimationController.frameLists[0].currentFrame >= SpinEffect.Frames.Length)
                    {
                        input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                        input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                        input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }
            }
            else
            {
                return;
            }

            output.Sprite(input.Sprites.Zoey[SpinEffect.Frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
        });

        builder.RenderSingle(SpriteType.Breasts, 10, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (bodyState == BodyState.SideBelly)
            {
                output.Layer(3);
            }
            else
            {
                output.Layer(10);
            }

            switch (bodyState)
            {
                case BodyState.SpinAttack:
                case BodyState.SideBelly:
                    if (input.Actor.PredatorComponent.VisibleFullness > 1)
                    {
                        output.Sprite(input.Sprites.Zoey[26 + input.Actor.Unit.BreastSize]);
                        return;
                    }

                    output.Sprite(input.Sprites.Zoey[20 + input.Actor.Unit.BreastSize]);
                    return;
                default:
                    output.Sprite(input.Sprites.Zoey[14 + input.Actor.Unit.BreastSize]);
                    return;
            }
        });

        builder.RenderSingle(SpriteType.Belly, 2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.HasBelly)
            {
                switch (bodyState)
                {
                    case BodyState.SpinAttack:
                    case BodyState.SideBelly:
                        if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(19) == 19)
                        {
                            output.Sprite(input.Sprites.Zoey[72]);
                            return;
                        }

                        if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                        {
                            if (input.Actor.GetStomachSize(19, 0.7f) == 19)
                            {
                                output.Sprite(input.Sprites.Zoey[78]);
                                return;
                            }

                            if (input.Actor.GetStomachSize(19, 0.8f) == 19)
                            {
                                output.Sprite(input.Sprites.Zoey[77]);
                                return;
                            }

                            if (input.Actor.GetStomachSize(19, 0.9f) == 19)
                            {
                                output.Sprite(input.Sprites.Zoey[76]);
                                return;
                            }
                        }

                        output.Sprite(input.Sprites.Zoey[52 + input.Actor.GetStomachSize(19)]);
                        return;
                    default:
                        if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(19) == 19)
                        {
                            output.Sprite(input.Sprites.Zoey[51]);
                            return;
                        }

                        if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                        {
                            if (input.Actor.GetStomachSize(19, 0.7f) == 19)
                            {
                                output.Sprite(input.Sprites.Zoey[75]);
                                return;
                            }

                            if (input.Actor.GetStomachSize(19, 0.8f) == 19)
                            {
                                output.Sprite(input.Sprites.Zoey[74]);
                                return;
                            }

                            if (input.Actor.GetStomachSize(19, 0.9f) == 19)
                            {
                                output.Sprite(input.Sprites.Zoey[73]);
                                return;
                            }
                        }

                        output.Sprite(input.Sprites.Zoey[31 + input.Actor.GetStomachSize(19)]);
                        return;
                }
            }
        });

        builder.RunBefore((input, output) =>
        {
            if (input.Actor.AnimationController.frameLists == null || Enumerable.Count(input.Actor.AnimationController.frameLists) == 0)
            {
                SetUpAnimations(input.Actor);
            }

            if (input.Actor.AnimationController?.frameLists[0].currentlyActive ?? false)
            {
                if (input.Actor.GetStomachSize(19) >= 17)
                {
                    bodyState = BodyState.SideBelly;
                }
                else
                {
                    bodyState = BodyState.SpinAttack;
                }
            }
            else if (input.Actor.GetStomachSize(19) >= 18)
            {
                bodyState = BodyState.HighBelly;
            }
            else
            {
                bodyState = BodyState.Normal;
            }

            output.ClothingShift = new Vector3(0, 0);
            switch (bodyState)
            {
                case BodyState.HighBelly:
                    if (input.Actor.GetStomachSize(19) == 19)
                    {
                        output.ChangeSprite(SpriteType.Head).AddOffset(0, 30 * .625f);
                        output.ChangeSprite(SpriteType.Hair).AddOffset(0, 30 * .625f);
                        output.ChangeSprite(SpriteType.Hair2).AddOffset(0, 30 * .625f);
                        output.ChangeSprite(SpriteType.Breasts).AddOffset(0, 30 * .625f);
                        output.ClothingShift = new Vector3(0, 30 * .625f);
                    }
                    else //18
                    {
                        output.ChangeSprite(SpriteType.Body).AddOffset(0, -14 * .625f);
                        output.ChangeSprite(SpriteType.Head).AddOffset(0, 16 * .625f);
                        output.ChangeSprite(SpriteType.Hair).AddOffset(0, 16 * .625f);
                        output.ChangeSprite(SpriteType.Hair2).AddOffset(0, 16 * .625f);
                        output.ChangeSprite(SpriteType.Breasts).AddOffset(0, 16 * .625f);
                        output.ClothingShift = new Vector3(0, 16 * .625f);
                    }

                    break;
                case BodyState.SideBelly:
                    if (input.Actor.GetStomachSize(19) == 19)
                    {
                        output.ChangeSprite(SpriteType.Breasts).AddOffset(-5 * .625f, 12 * .625f);
                    }
                    else if (input.Actor.GetStomachSize(19) == 18)
                    {
                        output.ChangeSprite(SpriteType.Breasts).AddOffset(-2 * .625f, 0);
                        output.ChangeSprite(SpriteType.Body).AddOffset(0, -16 * .625f);
                        output.ChangeSprite(SpriteType.Head).AddOffset(0, -16 * .625f);
                        output.ChangeSprite(SpriteType.Hair).AddOffset(0, -16 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, -16 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, -16 * .625f);
                    }
                    else
                    {
                        output.ChangeSprite(SpriteType.Breasts).AddOffset(-5 * .625f, 0);
                        output.ChangeSprite(SpriteType.Body).AddOffset(0, -32 * .625f);
                        output.ChangeSprite(SpriteType.Head).AddOffset(0, -32 * .625f);
                        output.ChangeSprite(SpriteType.Hair).AddOffset(0, -32 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, -32 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, -32 * .625f);
                    }

                    break;
            }
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.Name = "Zoey";
        });
    });

    internal static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(0, 0, false)
        };
    }


    internal enum BodyState
    {
        Normal,
        HighBelly,
        SideBelly,
        SpinAttack
    }

    private static class ZoeyTop
    {
        internal static IClothing ZoeyTopInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = null;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1417;
                output.ReqWinterHoliday = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing4"].Layer(7);
                output["Clothing4"].Coloring(Color.white);
                output["Clothing3"].Layer(12);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing2"].Layer(12);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(11);
                output["Clothing1"].Coloring(Color.white);
                var state = bodyState;
                //Sweater
                switch (state)
                {
                    case BodyState.SpinAttack:
                        if ((input.Actor.Unit.BreastSize == 4) | (input.Actor.GetStomachSize(19) >= 4))
                        {
                            output["Clothing1"].Sprite(input.Sprites.ZoeyHoliday[6]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.ZoeyHoliday[4]);
                        }

                        break;
                    case BodyState.SideBelly:
                        output["Clothing1"].Sprite(input.Sprites.ZoeyHoliday[5]);
                        break;
                    default:
                        if ((input.Actor.Unit.BreastSize == 4) | (input.Actor.GetStomachSize(19) >= 4))
                        {
                            output["Clothing1"].Sprite(input.Sprites.ZoeyHoliday[24 + (input.Actor.IsAttacking ? 1 : 0)]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.ZoeyHoliday[22 + (input.Actor.IsAttacking ? 1 : 0)]);
                        }

                        break;
                }

                //Boobs
                if (state == BodyState.SideBelly)
                {
                    output["Clothing2"].Layer(4);
                }
                else
                {
                    output["Clothing2"].Layer(12);
                }

                switch (state)
                {
                    case BodyState.SpinAttack:
                    case BodyState.SideBelly:
                        if (input.Actor.PredatorComponent?.VisibleFullness > 1)
                        {
                            output["Clothing2"].Sprite(input.Sprites.ZoeyHoliday[15 + Math.Min(input.Actor.Unit.BreastSize, 3)]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(input.Sprites.ZoeyHoliday[11 + Math.Min(input.Actor.Unit.BreastSize, 3)]);
                        }

                        break;
                    default:
                        if ((input.Actor.Unit.BreastSize == 4) | (input.Actor.GetStomachSize(19) >= 4))
                        {
                            output["Clothing2"].Sprite(input.Sprites.ZoeyHoliday[24]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(input.Sprites.ZoeyHoliday[7 + Math.Min(input.Actor.Unit.BreastSize, 3)]);
                        }

                        break;
                }

                switch (state)
                {
                    case BodyState.SpinAttack:
                        output["Clothing3"].Sprite(input.Sprites.ZoeyHoliday[20]);
                        break;
                    case BodyState.SideBelly:
                        output["Clothing3"].Sprite(input.Sprites.ZoeyHoliday[21]);
                        break;
                    default:
                        output["Clothing3"].Sprite(input.Sprites.ZoeyHoliday[19]);
                        break;
                }

                switch (state)
                {
                    case BodyState.HighBelly:
                        output["Clothing4"].Sprite(input.Sprites.ZoeyHoliday[2 + (input.Actor.IsAttacking ? 1 : 0)]);
                        break;
                    case BodyState.SideBelly:
                        output["Clothing4"].Sprite(null);
                        break;
                    case BodyState.SpinAttack:
                        output["Clothing4"].Sprite(null);
                        break;
                    default:
                        output["Clothing4"].Sprite(input.Sprites.ZoeyHoliday[0 + (input.Actor.IsAttacking ? 1 : 0)]);
                        break;
                }
            });
        });
    }
}