﻿#region

using System.Collections.Generic;

#endregion

internal static class Dratopyr
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        RaceFrameList frameListTail = new RaceFrameList(new int[8] { 2, 1, 0, 1, 2, 3, 4, 3 }, new float[8] { 0.55f, 0.55f, 0.75f, 0.55f, 0.55f, 0.55f, 0.75f, 0.55f });
        RaceFrameList frameListEyes = new RaceFrameList(new int[3] { 1, 2, 1 }, new float[3] { 0.3f, 0.3f, 0.3f });
        RaceFrameList frameListShake = new RaceFrameList(new int[5] { 0, 1, 0, 2, 0 }, new float[5] { 0.6f, 0.6f, 0.6f, 0.6f, 0.6f });
        RaceFrameList frameListWings = new RaceFrameList(new int[4] { 0, 1, 2, 1 }, new float[4] { 0.3f, 0.3f, 0.3f, 0.3f });
        RaceFrameList frameListEars = new RaceFrameList(new int[18] { 0, 1, 2, 1, 0, 1, 0, 1, 2, 1, 2, 1, 0, 1, 2, 1, 2, 1 }, new float[18] { 2.2f, 0.3f, 0.5f, 0.2f, 0.8f, 0.3f, 1.5f, 0.9f, 1.3f, 0.6f, 0.4f, 0.3f, 2.2f, 1.5f, 0.6f, 0.3f, 0.8f, 0.2f });


        builder.Setup(output =>
        {
            output.BreastSizes = () => 2;
            output.DickSizes = () => 2;

            output.SkinColors = ColorMap.DratopyrMainColorCount; // Majority of the unit
            output.AccessoryColors = ColorMap.DratopyrWingColorCount; // Wing Webbing
            output.ExtraColors1 = ColorMap.DratopyrFleshColorCount; // Flesh
            output.ExtraColors2 = ColorMap.DratopyrEyeColorCount; // Eye "Whites"
            output.EyeColors = ColorMap.EyeColorCount; // Eyes
            output.GentleAnimation = true;

            output.WeightGainDisabled = true;

            output.CanBeGender = new List<Gender> { Gender.Male, Gender.Female };
        });


        builder.RenderSingle(SpriteType.Head, 10, (input, output) =>
        {
            output.Coloring(ColorMap.GetDratopyrMainColor(input.Actor.Unit.SkinColor));
            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Dratopyr[5]);
                return;
            }

            if (input.Actor.GetBallSize(22) > 0)
            {
                output.Sprite(input.Sprites.Dratopyr[1]);
                return;
            }

            output.Sprite(input.Sprites.Dratopyr[0]);
        }); // Head

        builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
        {
            output.Coloring(ColorMap.GetEyeColor(input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Dratopyr[2]);
        }); // Eye Iris
        builder.RenderSingle(SpriteType.Mouth, 12, (input, output) =>
        {
            output.Coloring(ColorMap.GetDratopyrFleshColor(input.Actor.Unit.ExtraColor1));
            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Dratopyr[6]);
            }
        }); // Inner Mouth

        builder.RenderSingle(SpriteType.Hair, 11, (input, output) =>
        {
            output.Coloring(ColorMap.GetDratopyrMainColor(input.Actor.Unit.SkinColor));
            if (input.Actor.IsOralVoring)
            {
                return;
            }

            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Dratopyr[14]);
                return;
            }

            if (input.Actor.AnimationController.frameLists[4].currentTime >= frameListEars.times[input.Actor.AnimationController.frameLists[4].currentFrame])
            {
                input.Actor.AnimationController.frameLists[4].currentFrame++;
                input.Actor.AnimationController.frameLists[4].currentTime = 0f;

                if (input.Actor.AnimationController.frameLists[4].currentFrame >= frameListEars.frames.Length)
                {
                    input.Actor.AnimationController.frameLists[4].currentFrame = 0;
                    input.Actor.AnimationController.frameLists[4].currentTime = 0f;
                }
            }

            output.Sprite(input.Sprites.Dratopyr[14 + frameListEars.frames[input.Actor.AnimationController.frameLists[4].currentFrame]]);
        }); // Ears

        builder.RenderSingle(SpriteType.Body, 9, (input, output) =>
        {
            output.Coloring(ColorMap.GetDratopyrMainColor(input.Actor.Unit.SkinColor));
            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            if (input.Actor.IsAttacking || input.Actor.IsCockVoring || input.Actor.IsUnbirthing)
            {
                output.Sprite(input.Sprites.Dratopyr[22]);
                return;
            }

            output.Sprite(input.Sprites.Dratopyr[21]);
        }); // Upper Body & Arms

        builder.RenderSingle(SpriteType.BodyAccent, 0, (input, output) =>
        {
            output.Coloring(ColorMap.GetDratopyrMainColor(input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Dratopyr[28]);
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

                output.Sprite(input.Sprites.Dratopyr[26 + frameListTail.frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                return;
            }

            if (State.Rand.Next(250) == 0)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = true;
            }

            output.Sprite(input.Sprites.Dratopyr[28]);
        }); // Tail

        builder.RenderSingle(SpriteType.BodyAccent2, 3, (input, output) =>
        {
            output.Coloring(ColorMap.GetDratopyrMainColor(input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Dratopyr[17]);
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsOralVoring || input.Actor.IsCockVoring || input.Actor.IsUnbirthing)
            {
                input.Actor.AnimationController.frameLists[2].currentlyActive = false;
                input.Actor.AnimationController.frameLists[2].currentFrame = 0;
                input.Actor.AnimationController.frameLists[2].currentTime = 0f;

                output.Sprite(input.Sprites.Dratopyr[20]);
                return;
            }

            if (input.Actor.AnimationController.frameLists[2].currentlyActive)
            {
                if (input.Actor.AnimationController.frameLists[2].currentTime >= frameListShake.times[input.Actor.AnimationController.frameLists[2].currentFrame])
                {
                    input.Actor.AnimationController.frameLists[2].currentFrame++;
                    input.Actor.AnimationController.frameLists[2].currentTime = 0f;

                    if (input.Actor.AnimationController.frameLists[2].currentFrame >= frameListShake.frames.Length)
                    {
                        input.Actor.AnimationController.frameLists[2].currentlyActive = false;
                        input.Actor.AnimationController.frameLists[2].currentFrame = 0;
                        input.Actor.AnimationController.frameLists[2].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Dratopyr[17 + frameListShake.frames[input.Actor.AnimationController.frameLists[2].currentFrame]]);
                return;
            }

            if (State.Rand.Next(350) == 0)
            {
                input.Actor.AnimationController.frameLists[2].currentlyActive = true;
            }

            output.Sprite(input.Sprites.Dratopyr[17]);
        }); // Legs

        builder.RenderSingle(SpriteType.BodyAccent3, 1, (input, output) =>
        {
            output.Coloring(ColorMap.GetDratopyrWingColor(input.Actor.Unit.ExtraColor1));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Dratopyr[8]);
                return;
            }

            if (input.Actor.AnimationController.frameLists[3].currentTime >= frameListWings.times[input.Actor.AnimationController.frameLists[3].currentFrame])
            {
                input.Actor.AnimationController.frameLists[3].currentFrame++;
                input.Actor.AnimationController.frameLists[3].currentTime = 0f;

                if (input.Actor.AnimationController.frameLists[3].currentFrame >= frameListWings.frames.Length)
                {
                    input.Actor.AnimationController.frameLists[3].currentFrame = 0;
                    input.Actor.AnimationController.frameLists[3].currentTime = 0f;
                }
            }

            output.Sprite(input.Sprites.Dratopyr[8 + frameListWings.frames[input.Actor.AnimationController.frameLists[3].currentFrame]]);
        }); // Wing Webbing

        builder.RenderSingle(SpriteType.BodyAccent4, 2, (input, output) =>
        {
            output.Coloring(ColorMap.GetDratopyrMainColor(input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Dratopyr[11]);
                return;
            }

            output.Sprite(input.Sprites.Dratopyr[11 + frameListWings.frames[input.Actor.AnimationController.frameLists[3].currentFrame]]);
        }); // Wing Bones

        builder.RenderSingle(SpriteType.BodyAccent5, 7, (input, output) =>
        {
            output.Coloring(ColorMap.GetDratopyrEyeColor(input.Actor.Unit.ExtraColor2));
            output.Sprite(input.Sprites.Dratopyr[25]);
        }); // Eye Whites

        builder.RenderSingle(SpriteType.BodyAccent6, 9, (input, output) =>
        {
            output.Coloring(ColorMap.GetDratopyrMainColor(input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Dratopyr[4]);
                return;
            }

            if (input.Actor.AnimationController.frameLists[1].currentlyActive)
            {
                if (input.Actor.AnimationController.frameLists[1].currentTime >= frameListEyes.times[input.Actor.AnimationController.frameLists[1].currentFrame])
                {
                    input.Actor.AnimationController.frameLists[1].currentFrame++;
                    input.Actor.AnimationController.frameLists[1].currentTime = 0f;

                    if (input.Actor.AnimationController.frameLists[1].currentFrame >= frameListEyes.frames.Length)
                    {
                        input.Actor.AnimationController.frameLists[1].currentlyActive = false;
                        input.Actor.AnimationController.frameLists[1].currentFrame = 0;
                        input.Actor.AnimationController.frameLists[1].currentTime = 0f;
                    }
                }

                if (frameListEyes.frames[input.Actor.AnimationController.frameLists[1].currentFrame] == 0)
                {
                    return;
                }

                output.Sprite(input.Sprites.Dratopyr[2 + frameListEyes.frames[input.Actor.AnimationController.frameLists[1].currentFrame]]);
                return;
            }

            if (State.Rand.Next(400) == 0)
            {
                input.Actor.AnimationController.frameLists[1].currentlyActive = true;
            }
        }); // Eyelids

        builder.RenderSingle(SpriteType.BodyAccent7, 13, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Dratopyr[7]);
            }
        }); // Teeth

        builder.RenderSingle(SpriteType.BodyAccent8, 5, (input, output) =>
        {
            output.Coloring(ColorMap.GetDratopyrMainColor(input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.DickSize >= 0)
            {
                if (input.Actor.GetStomachSize(23, 0.7f) > 14)
                {
                    if (!input.Actor.Targetable)
                    {
                        output.Sprite(input.Sprites.Dratopyr[34]);
                        return;
                    }

                    output.Sprite(input.Sprites.Dratopyr[34 + frameListShake.frames[input.Actor.AnimationController.frameLists[2].currentFrame]]);
                    return;
                }

                if (!input.Actor.Targetable)
                {
                    output.Sprite(input.Sprites.Dratopyr[31]);
                    return;
                }

                output.Sprite(input.Sprites.Dratopyr[31 + frameListShake.frames[input.Actor.AnimationController.frameLists[2].currentFrame]]);
            }
        }); // Sheath

        builder.RenderSingle(SpriteType.Belly, 7, (input, output) =>
        {
            output.Coloring(ColorMap.GetDratopyrMainColor(input.Actor.Unit.SkinColor));
            int bellySize = input.Actor.GetStomachSize(23, 0.7f);
            int shake = frameListShake.frames[input.Actor.AnimationController.frameLists[2].currentFrame];

            if (!input.Actor.Targetable)
            {
                shake = 0;
            }

            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false)
            {
                output.Sprite(input.Sprites.Dratopyr[168 + shake]);
                return;
            }

            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false)
            {
                if (bellySize > 22)
                {
                    output.Sprite(input.Sprites.Dratopyr[165 + shake]);
                    return;
                }

                if (bellySize > 21)
                {
                    output.Sprite(input.Sprites.Dratopyr[162 + shake]);
                    return;
                }

                if (bellySize > 20)
                {
                    output.Sprite(input.Sprites.Dratopyr[159 + shake]);
                    return;
                }

                if (bellySize > 19)
                {
                    output.Sprite(input.Sprites.Dratopyr[156 + shake]);
                    return;
                }
            }

            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.womb) ?? false)
            {
                output.Sprite(input.Sprites.Dratopyr[168 + shake]);
                return;
            }

            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.womb) ?? false)
            {
                if (bellySize > 22)
                {
                    output.Sprite(input.Sprites.Dratopyr[165 + shake]);
                    return;
                }

                if (bellySize > 21)
                {
                    output.Sprite(input.Sprites.Dratopyr[162 + shake]);
                    return;
                }

                if (bellySize > 20)
                {
                    output.Sprite(input.Sprites.Dratopyr[159 + shake]);
                    return;
                }

                if (bellySize > 19)
                {
                    output.Sprite(input.Sprites.Dratopyr[156 + shake]);
                    return;
                }
            }

            if (bellySize > 18)
            {
                bellySize = 18;
            }

            output.Sprite(input.Sprites.Dratopyr[102 + bellySize * 3 + shake]);
        }); // Belly

        builder.RenderSingle(SpriteType.Dick, 10, (input, output) =>
        {
            output.Coloring(ColorMap.GetDratopyrFleshColor(input.Actor.Unit.ExtraColor1));
            if (input.Actor.Unit.DickSize >= 0)
            {
                if (input.Actor.GetStomachSize(23, 0.7f) > 1)
                {
                    output.Layer(6);

                    if (input.Actor.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.Dratopyr[44]);
                        return;
                    }

                    if (input.Actor.IsErect())
                    {
                        if (!input.Actor.Targetable)
                        {
                            output.Sprite(input.Sprites.Dratopyr[41]);
                            return;
                        }

                        if (input.Actor.AnimationController.frameLists[2].currentlyActive)
                        {
                            output.Sprite(input.Sprites.Dratopyr[41 + frameListShake.frames[input.Actor.AnimationController.frameLists[2].currentFrame]]);
                            return;
                        }

                        output.Sprite(input.Sprites.Dratopyr[41]);
                        return;
                    }
                }
                else
                {
                    output.Layer(10);

                    if (input.Actor.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.Dratopyr[40]);
                        return;
                    }

                    if (input.Actor.IsErect())
                    {
                        if (!input.Actor.Targetable)
                        {
                            output.Sprite(input.Sprites.Dratopyr[37]);
                            return;
                        }

                        if (input.Actor.AnimationController.frameLists[2].currentlyActive)
                        {
                            output.Sprite(input.Sprites.Dratopyr[37 + frameListShake.frames[input.Actor.AnimationController.frameLists[2].currentFrame]]);
                            return;
                        }

                        output.Sprite(input.Sprites.Dratopyr[37]);
                        return;
                    }
                }
            }

            if (input.Actor.Unit.DickSize == -1)
            {
                if (input.Actor.IsUnbirthing)
                {
                    output.Sprite(input.Sprites.Dratopyr[172]).Layer(6);
                    return;
                }

                output.Sprite(input.Sprites.Dratopyr[171]).Layer(6);
            }
        }); // Dick

        builder.RenderSingle(SpriteType.Balls, 4, (input, output) =>
        {
            output.Coloring(ColorMap.GetDratopyrMainColor(input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.DickSize == -1)
            {
                return;
            }

            if (Config.HideCocks)
            {
                return;
            }

            int shake = frameListShake.frames[input.Actor.AnimationController.frameLists[2].currentFrame];
            int ballSize = input.Actor.GetBallSize(21, 0.6f);

            if (!input.Actor.Targetable)
            {
                shake = 0;
            }

            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false)
            {
                output.Sprite(input.Sprites.Dratopyr[99 + shake]);
                return;
            }

            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false)
            {
                if (ballSize > 19)
                {
                    output.Sprite(input.Sprites.Dratopyr[96 + shake]);
                    return;
                }

                if (ballSize > 17)
                {
                    output.Sprite(input.Sprites.Dratopyr[93 + shake]);
                    return;
                }

                if (ballSize > 15)
                {
                    output.Sprite(input.Sprites.Dratopyr[90 + shake]);
                    return;
                }

                if (ballSize > 13)
                {
                    output.Sprite(input.Sprites.Dratopyr[87 + shake]);
                    return;
                }
            }

            if (ballSize > 13)
            {
                ballSize = 13;
            }

            output.Sprite(input.Sprites.Dratopyr[45 + ballSize * 3 + shake]);
        }); // Balls


        builder.RunBefore((input, output) =>
        {
            output.changeSprite(SpriteType.Balls).AddOffset(0, -80 * .625f);
            output.changeSprite(SpriteType.Belly).AddOffset(0, -80 * .625f);
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;


            int rand = State.Rand.Next(100);

            if (rand < 92)
            {
                unit.ExtraColor2 = 0;
            }
            else if (rand < 97)
            {
                unit.ExtraColor2 = 1;
            }
            else
            {
                unit.ExtraColor2 = 2;
            }
        });
    });

    internal static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(0, 0, false), // Tail controller. Index 0.
            new AnimationController.FrameList(0, 0, false), // Eye controller. Index 1.
            new AnimationController.FrameList(0, 0, false), // Shimmyshake controller. Index 2.
            new AnimationController.FrameList(State.Rand.Next(0, 3), 0, true), // Wing controller. Index 3.
            new AnimationController.FrameList(State.Rand.Next(0, 17), 0, true)
        }; // Ear controller. Index 4.
    }
}