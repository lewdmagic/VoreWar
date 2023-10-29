#region

using System.Collections.Generic;

#endregion

internal static class Raptor
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        RaceFrameList frameListTail = new RaceFrameList(new int[24] { 0, 4, 5, 6, 5, 4, 0, 3, 2, 1, 2, 3, 0, 4, 5, 6, 5, 4, 0, 3, 2, 1, 2, 3 }, new float[24] { 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f });


        builder.Setup(output =>
        {
            output.GentleAnimation = true;
            output.CanBeGender = new List<Gender> { Gender.Male };
            output.SkinColors = ColorMap.LizardColorCount;
            output.ExtraColors1 = ColorMap.LizardColorCount;
        });


        builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
        {
            output.Coloring(ColorMap.GetLizardColor(input.Actor.Unit.ExtraColor1));
            output.Sprite(input.Sprites.Raptor[5]);
        });

        builder.RenderSingle(SpriteType.Mouth, 9, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking || input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Raptor[7]);
                return;
            }

            if (input.Actor.GetBallSize(16) > 0)
            {
                output.Sprite(input.Sprites.Raptor[9]);
                return;
            }

            if (input.Actor.HasBelly)
            {
                output.Sprite(input.Sprites.Raptor[8]);
                return;
            }

            output.Sprite(input.Sprites.Raptor[6]);
        });

        builder.RenderSingle(SpriteType.Body, 8, (input, output) =>
        {
            output.Coloring(ColorMap.GetLizardColor(input.Actor.Unit.SkinColor));
            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            if (input.Actor.IsAttacking || input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Raptor[1]);
                return;
            }

            if (input.Actor.GetBallSize(16) > 0)
            {
                output.Sprite(input.Sprites.Raptor[3]);
                return;
            }

            if (input.Actor.HasBelly)
            {
                output.Sprite(input.Sprites.Raptor[2]);
                return;
            }

            output.Sprite(input.Sprites.Raptor[0]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
        {
            output.Coloring(ColorMap.GetLizardColor(input.Actor.Unit.SkinColor));
            if (input.Actor.IsAttacking || input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Raptor[48]);
                return;
            }

            output.Sprite(input.Sprites.Raptor[4]);
        }); // Legs

        builder.RenderSingle(SpriteType.BodyAccent2, 10, (input, output) =>
        {
            output.Coloring(ColorMap.GetLizardColor(input.Actor.Unit.ExtraColor1));
            if (input.Actor.IsAttacking || input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Raptor[49]);
                return;
            }

            output.Sprite(input.Sprites.Raptor[10]);
        }); // Body Stripes

        builder.RenderSingle(SpriteType.BodyAccent3, 4, (input, output) =>
        {
            output.Coloring(ColorMap.GetLizardColor(input.Actor.Unit.ExtraColor1));
            output.Sprite(input.Sprites.Raptor[11]);
        }); // Leg Stripes

        builder.RenderSingle(SpriteType.BodyAccent4, 1, (input, output) =>
        {
            output.Coloring(ColorMap.GetLizardColor(input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                return;
            }

            if (input.Actor.AnimationController.frameLists[0].currentlyActive)
            {
                if (input.Actor.AnimationController.frameLists[0].currentTime >= frameListTail.times[input.Actor.AnimationController.frameLists[0].currentFrame])
                {
                    input.Actor.AnimationController.frameLists[0].currentFrame++;
                    input.Actor.AnimationController.frameLists[0].currentTime = 0f;

                    if (input.Actor.AnimationController.frameLists[0].currentFrame >= frameListTail.frames.Length)
                    {
                        input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                        input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                        input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }

                if (frameListTail.frames[input.Actor.AnimationController.frameLists[0].currentFrame] == 0)
                {
                    return;
                }

                output.Sprite(input.Sprites.Raptor[11 + frameListTail.frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                return;
            }

            if (input.Actor.HasBelly || input.Actor.GetBallSize(18) > 0)
            {
                if (State.Rand.Next(300) == 0)
                {
                    input.Actor.AnimationController.frameLists[0].currentlyActive = true;
                }
            }

            else if (State.Rand.Next(1200) == 0)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = true;
            }
        }); // Tail

        builder.RenderSingle(SpriteType.BodyAccent5, 2, (input, output) =>
        {
            output.Coloring(ColorMap.GetLizardColor(input.Actor.Unit.ExtraColor1));
            if (!input.Actor.Targetable)
            {
                return;
            }

            if (input.Actor.AnimationController.frameLists[0].currentlyActive)
            {
                if (frameListTail.frames[input.Actor.AnimationController.frameLists[0].currentFrame] == 0)
                {
                    return;
                }

                output.Sprite(input.Sprites.Raptor[17 + frameListTail.frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
            }
        }); // Tail Stripes

        builder.RenderSingle(SpriteType.BodyAccent6, 0, (input, output) =>
        {
            output.Coloring(ColorMap.GetLizardColor(input.Actor.Unit.SkinColor));
            if (input.Actor.GetBallSize(24, 2) == 0 && Config.HideCocks == false)
            {
                output.Sprite(input.Sprites.Raptor[52]);
                return;
            }

            int size = input.Actor.GetBallSize(24, 2);

            if (size == 24 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false))
            {
                output.Sprite(input.Sprites.Raptor[75]);
                return;
            }

            if (size >= 23 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
            {
                output.Sprite(input.Sprites.Raptor[74]);
                return;
            }

            if (size == 22 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
            {
                output.Sprite(input.Sprites.Raptor[73]);
                return;
            }

            if (size > 21)
            {
                size = 21;
            }

            output.Sprite(input.Sprites.Raptor[51 + size]);
        }); // Balls

        builder.RenderSingle(SpriteType.Belly, 7, (input, output) =>
        {
            output.Coloring(ColorMap.GetLizardColor(input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            int size = input.Actor.GetStomachSize(24, 2);

            if (size == 24 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false))
            {
                output.Sprite(input.Sprites.Raptor[47]);
                return;
            }

            if (size >= 23 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
            {
                output.Sprite(input.Sprites.Raptor[46]);
                return;
            }

            if (size == 22 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
            {
                output.Sprite(input.Sprites.Raptor[45]);
                return;
            }

            if (size > 21)
            {
                size = 21;
            }

            output.Sprite(input.Sprites.Raptor[23 + size]);
        });

        builder.RenderSingle(SpriteType.Dick, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsCockVoring)
            {
                output.Sprite(input.Sprites.Raptor[51]);
                return;
            }

            if (input.Actor.IsErect())
            {
                output.Sprite(input.Sprites.Raptor[50]);
            }
        });


        builder.RunBefore((input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                output.changeSprite(SpriteType.Dick).AddOffset(0, 0 * .3125f);
                output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 0 * .3125f);
                output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 0 * .3125f);
                output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 0 * .3125f);
                output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 0 * .3125f);
                output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 0 * .3125f);
                output.changeSprite(SpriteType.Body).AddOffset(0, 0 * .3125f);
                output.changeSprite(SpriteType.Eyes).AddOffset(0, 0 * .3125f);
                output.changeSprite(SpriteType.Mouth).AddOffset(0, 0 * .3125f);
            }

            else
            {
                int size = input.Actor.GetStomachSize(24, 2);

                if (size == 24 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false))
                {
                    output.changeSprite(SpriteType.Dick).AddOffset(0, 176 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 176 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 176 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 176 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 176 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 176 * .3125f);
                    output.changeSprite(SpriteType.Body).AddOffset(0, 176 * .3125f);
                    output.changeSprite(SpriteType.Eyes).AddOffset(0, 176 * .3125f);
                    output.changeSprite(SpriteType.Mouth).AddOffset(0, 176 * .3125f);
                }

                else if (size >= 23 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
                {
                    output.changeSprite(SpriteType.Dick).AddOffset(0, 168 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 168 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 168 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 168 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 168 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 168 * .3125f);
                    output.changeSprite(SpriteType.Body).AddOffset(0, 168 * .3125f);
                    output.changeSprite(SpriteType.Eyes).AddOffset(0, 168 * .3125f);
                    output.changeSprite(SpriteType.Mouth).AddOffset(0, 168 * .3125f);
                }

                else if (size == 22 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
                {
                    output.changeSprite(SpriteType.Dick).AddOffset(0, 152 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 152 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 152 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 152 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 152 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 152 * .3125f);
                    output.changeSprite(SpriteType.Body).AddOffset(0, 152 * .3125f);
                    output.changeSprite(SpriteType.Eyes).AddOffset(0, 152 * .3125f);
                    output.changeSprite(SpriteType.Mouth).AddOffset(0, 152 * .3125f);
                }

                else if (size >= 21)
                {
                    output.changeSprite(SpriteType.Dick).AddOffset(0, 144 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 144 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 144 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 144 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 144 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 144 * .3125f);
                    output.changeSprite(SpriteType.Body).AddOffset(0, 144 * .3125f);
                    output.changeSprite(SpriteType.Eyes).AddOffset(0, 144 * .3125f);
                    output.changeSprite(SpriteType.Mouth).AddOffset(0, 144 * .3125f);
                }

                else if (size == 20)
                {
                    output.changeSprite(SpriteType.Dick).AddOffset(0, 128 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 128 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 128 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 128 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 128 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 128 * .3125f);
                    output.changeSprite(SpriteType.Body).AddOffset(0, 128 * .3125f);
                    output.changeSprite(SpriteType.Eyes).AddOffset(0, 128 * .3125f);
                    output.changeSprite(SpriteType.Mouth).AddOffset(0, 128 * .3125f);
                }

                else if (size == 19)
                {
                    output.changeSprite(SpriteType.Dick).AddOffset(0, 112 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 112 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 112 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 112 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 112 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 112 * .3125f);
                    output.changeSprite(SpriteType.Body).AddOffset(0, 112 * .3125f);
                    output.changeSprite(SpriteType.Eyes).AddOffset(0, 112 * .3125f);
                    output.changeSprite(SpriteType.Mouth).AddOffset(0, 112 * .3125f);
                }

                else if (size == 18)
                {
                    output.changeSprite(SpriteType.Dick).AddOffset(0, 96 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 96 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 96 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 96 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 96 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 96 * .3125f);
                    output.changeSprite(SpriteType.Body).AddOffset(0, 96 * .3125f);
                    output.changeSprite(SpriteType.Eyes).AddOffset(0, 96 * .3125f);
                    output.changeSprite(SpriteType.Mouth).AddOffset(0, 96 * .3125f);
                }

                else if (size == 17)
                {
                    output.changeSprite(SpriteType.Dick).AddOffset(0, 80 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 80 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 80 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 80 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 80 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 80 * .3125f);
                    output.changeSprite(SpriteType.Body).AddOffset(0, 80 * .3125f);
                    output.changeSprite(SpriteType.Eyes).AddOffset(0, 80 * .3125f);
                    output.changeSprite(SpriteType.Mouth).AddOffset(0, 80 * .3125f);
                }

                else if (size == 16)
                {
                    output.changeSprite(SpriteType.Dick).AddOffset(0, 64 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 64 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 64 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 64 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 64 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 64 * .3125f);
                    output.changeSprite(SpriteType.Body).AddOffset(0, 64 * .3125f);
                    output.changeSprite(SpriteType.Eyes).AddOffset(0, 64 * .3125f);
                    output.changeSprite(SpriteType.Mouth).AddOffset(0, 64 * .3125f);
                }

                else if (size == 15)
                {
                    output.changeSprite(SpriteType.Dick).AddOffset(0, 48 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 48 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 48 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 48 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 48 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 48 * .3125f);
                    output.changeSprite(SpriteType.Body).AddOffset(0, 48 * .3125f);
                    output.changeSprite(SpriteType.Eyes).AddOffset(0, 48 * .3125f);
                    output.changeSprite(SpriteType.Mouth).AddOffset(0, 48 * .3125f);
                }

                else if (size == 14)
                {
                    output.changeSprite(SpriteType.Dick).AddOffset(0, 32 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 32 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 32 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 32 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 32 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 32 * .3125f);
                    output.changeSprite(SpriteType.Body).AddOffset(0, 32 * .3125f);
                    output.changeSprite(SpriteType.Eyes).AddOffset(0, 32 * .3125f);
                    output.changeSprite(SpriteType.Mouth).AddOffset(0, 32 * .3125f);
                }

                else if (size == 13)
                {
                    output.changeSprite(SpriteType.Dick).AddOffset(0, 24 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 24 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 24 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 24 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 24 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 24 * .3125f);
                    output.changeSprite(SpriteType.Body).AddOffset(0, 24 * .3125f);
                    output.changeSprite(SpriteType.Eyes).AddOffset(0, 24 * .3125f);
                    output.changeSprite(SpriteType.Mouth).AddOffset(0, 24 * .3125f);
                }

                else if (size == 12)
                {
                    output.changeSprite(SpriteType.Dick).AddOffset(0, 16 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 16 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 16 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 16 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 16 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 16 * .3125f);
                    output.changeSprite(SpriteType.Body).AddOffset(0, 16 * .3125f);
                    output.changeSprite(SpriteType.Eyes).AddOffset(0, 16 * .3125f);
                    output.changeSprite(SpriteType.Mouth).AddOffset(0, 16 * .3125f);
                }

                else if (size == 11)
                {
                    output.changeSprite(SpriteType.Dick).AddOffset(0, 8 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 8 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 8 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 8 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 8 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 8 * .3125f);
                    output.changeSprite(SpriteType.Body).AddOffset(0, 8 * .3125f);
                    output.changeSprite(SpriteType.Eyes).AddOffset(0, 8 * .3125f);
                    output.changeSprite(SpriteType.Mouth).AddOffset(0, 8 * .3125f);
                }

                else
                {
                    output.changeSprite(SpriteType.Dick).AddOffset(0, 0 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 0 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 0 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 0 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 0 * .3125f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 0 * .3125f);
                    output.changeSprite(SpriteType.Body).AddOffset(0, 0 * .3125f);
                    output.changeSprite(SpriteType.Eyes).AddOffset(0, 0 * .3125f);
                    output.changeSprite(SpriteType.Mouth).AddOffset(0, 0 * .3125f);
                }
            }
        });

        builder.RandomCustom(data =>
        {
            Unit unit = data.Unit;

            unit.SkinColor = State.Rand.Next(data.MiscRaceData.SkinColors);
            unit.ExtraColor1 = State.Rand.Next(data.MiscRaceData.ExtraColors1);
            unit.DickSize = 1;
            unit.DefaultBreastSize = -1;
        });
    });

    internal static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(0, 0, false) // Tail controller
        };
    }
}