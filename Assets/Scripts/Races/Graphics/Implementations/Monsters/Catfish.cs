#region

using System.Collections.Generic;

#endregion

internal static class Catfish
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        RaceFrameList frameListMouth = new RaceFrameList(new[] { 0, 1, 2, 1, 0, 1, 2, 1, 0 }, new[] { 1.2f, .6f, 1.2f, .6f, 1.2f, .6f, 1.2f, .6f, 1.2f });
        RaceFrameList frameListTail = new RaceFrameList(new[] { 0, 1, 2, 1, 0, 1, 2, 1, 0 }, new[] { .5f, .3f, .5f, .3f, .5f, .3f, .5f, .3f, .5f });


        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.None };
            output.SpecialAccessoryCount = 6; // barbels
            output.BodyAccentTypes1 = 8; // dorsal fins
            output.ClothingColors = 0;
            output.GentleAnimation = true;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.CatfishSkin);
            output.EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.ViperSkin);
        });


        builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CatfishSkin, input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Catfish[4]);
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsOralVoring)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                output.Sprite(input.Sprites.Catfish[7]);
                return;
            }

            if (input.Actor.AnimationController.frameLists[0].currentlyActive)
            {
                if (input.Actor.AnimationController.frameLists[0].currentTime >= frameListMouth.times[input.Actor.AnimationController.frameLists[0].currentFrame])
                {
                    input.Actor.AnimationController.frameLists[0].currentFrame++;
                    input.Actor.AnimationController.frameLists[0].currentTime = 0f;

                    if (input.Actor.AnimationController.frameLists[0].currentFrame >= frameListMouth.frames.Length)
                    {
                        input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                        input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                        input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Catfish[4 + frameListMouth.frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                return;
            }

            if (State.Rand.Next(800) == 0)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = true;
            }

            output.Sprite(input.Sprites.Catfish[4]);
        });

        builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CatfishSkin, input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Catfish[25]);
        });
        builder.RenderSingle(SpriteType.SecondaryEyes, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Catfish[24]);
        });

        builder.RenderSingle(SpriteType.Mouth, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Catfish[8]);
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsOralVoring)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                output.Sprite(input.Sprites.Catfish[11]);
                return;
            }

            if (input.Actor.AnimationController.frameLists[0].currentlyActive)
            {
                output.Sprite(input.Sprites.Catfish[8 + frameListMouth.frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                return;
            }

            output.Sprite(input.Sprites.Catfish[8]);
        });

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CatfishSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            if (input.Actor.HasBelly == false)
            {
                output.Sprite(input.Sprites.Catfish[0]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.Catfish[80]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                if (input.Actor.GetStomachSize(20, .8f) == 20)
                {
                    output.Sprite(input.Sprites.Catfish[80]);
                    return;
                }

                if (input.Actor.GetStomachSize(20, .9f) == 20)
                {
                    output.Sprite(input.Sprites.Catfish[80]);
                    return;
                }
            }

            output.Sprite(input.Sprites.Catfish[60 + input.Actor.GetStomachSize(20)]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CatfishSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Catfish[28 + input.Actor.Unit.BodyAccentType1]);
        }); // dorsal fins
        builder.RenderSingle(SpriteType.BodyAccent2, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CatfishSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Catfish[18 + input.Actor.Unit.SpecialAccessoryType]);
        }); // barbels secondary
        builder.RenderSingle(SpriteType.BodyAccent3, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CatfishSkin, input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Catfish[1]);
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsOralVoring)
            {
                input.Actor.AnimationController.frameLists[1].currentlyActive = false;
                input.Actor.AnimationController.frameLists[1].currentFrame = 0;
                input.Actor.AnimationController.frameLists[1].currentTime = 0f;
                output.Sprite(input.Sprites.Catfish[1]);
                return;
            }

            if (input.Actor.AnimationController.frameLists[1].currentlyActive)
            {
                if (input.Actor.AnimationController.frameLists[1].currentTime >= frameListTail.times[input.Actor.AnimationController.frameLists[0].currentFrame])
                {
                    input.Actor.AnimationController.frameLists[1].currentFrame++;
                    input.Actor.AnimationController.frameLists[1].currentTime = 0f;

                    if (input.Actor.AnimationController.frameLists[1].currentFrame >= frameListTail.frames.Length)
                    {
                        input.Actor.AnimationController.frameLists[1].currentlyActive = false;
                        input.Actor.AnimationController.frameLists[1].currentFrame = 0;
                        input.Actor.AnimationController.frameLists[1].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Catfish[1 + frameListTail.frames[input.Actor.AnimationController.frameLists[1].currentFrame]]);
                return;
            }

            if (State.Rand.Next(800) == 0)
            {
                input.Actor.AnimationController.frameLists[1].currentlyActive = true;
            }

            output.Sprite(input.Sprites.Catfish[1]);
        }); // tail

        builder.RenderSingle(SpriteType.BodyAccent4, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Catfish[26]);
        }); // gills
        builder.RenderSingle(SpriteType.BodyAccent5, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CatfishSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Catfish[27]);
        }); // pelvic fin
        builder.RenderSingle(SpriteType.BodyAccessory, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CatfishSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Catfish[12 + input.Actor.Unit.SpecialAccessoryType]);
        }); // barbels
        builder.RenderSingle(SpriteType.Belly, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CatfishSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.Catfish[59]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                if (input.Actor.GetStomachSize(20, .8f) == 20)
                {
                    output.Sprite(input.Sprites.Catfish[58]);
                    return;
                }

                if (input.Actor.GetStomachSize(20, .9f) == 20)
                {
                    output.Sprite(input.Sprites.Catfish[57]);
                    return;
                }
            }

            output.Sprite(input.Sprites.Catfish[36 + input.Actor.GetStomachSize(20)]);
        });


        builder.RunBefore((input, output) =>
        {
            if (input.Actor.Unit.Predator && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) && input.Actor.GetStomachSize(20) == 20)
            {
                output.changeSprite(SpriteType.Body).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.Head).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.BodyAccessory).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.BodyAccent3).AddOffset(60 * .625f, 10 * .625f);
                output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 8 * .625f);
                output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 8 * .625f);
                output.changeSprite(SpriteType.Mouth).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.Eyes).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.SecondaryEyes).AddOffset(0, 10 * .625f);
            }
            else if (input.Actor.Unit.Predator && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) && input.Actor.GetStomachSize(20, .8f) == 20)
            {
                output.changeSprite(SpriteType.Body).AddOffset(0, 6 * .625f);
                output.changeSprite(SpriteType.Head).AddOffset(0, 6 * .625f);
                output.changeSprite(SpriteType.BodyAccessory).AddOffset(0, 6 * .625f);
                output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 6 * .625f);
                output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 6 * .625f);
                output.changeSprite(SpriteType.BodyAccent3).AddOffset(60 * .625f, 6 * .625f);
                output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 4 * .625f);
                output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 4 * .625f);
                output.changeSprite(SpriteType.Mouth).AddOffset(0, 6 * .625f);
                output.changeSprite(SpriteType.Eyes).AddOffset(0, 6 * .625f);
                output.changeSprite(SpriteType.SecondaryEyes).AddOffset(0, 6 * .625f);
            }
            else if (input.Actor.Unit.Predator && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) && input.Actor.GetStomachSize(20, .9f) == 20)
            {
                output.changeSprite(SpriteType.Body).AddOffset(0, 3 * .625f);
                output.changeSprite(SpriteType.Head).AddOffset(0, 3 * .625f);
                output.changeSprite(SpriteType.BodyAccessory).AddOffset(0, 3 * .625f);
                output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 3 * .625f);
                output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 3 * .625f);
                output.changeSprite(SpriteType.BodyAccent3).AddOffset(60 * .625f, 3 * .625f);
                output.changeSprite(SpriteType.BodyAccent4).AddOffset(0, 1 * .625f);
                output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, 1 * .625f);
                output.changeSprite(SpriteType.Mouth).AddOffset(0, 3 * .625f);
                output.changeSprite(SpriteType.Eyes).AddOffset(0, 3 * .625f);
                output.changeSprite(SpriteType.SecondaryEyes).AddOffset(0, 3 * .625f);
            }
            else if (input.Actor.GetStomachSize(20) > 11)
            {
                output.changeSprite(SpriteType.BodyAccent3).AddOffset(60 * .625f, 0);
                output.changeSprite(SpriteType.BodyAccent4).AddOffset(1 * .625f, -2 * .625f);
                output.changeSprite(SpriteType.BodyAccent5).AddOffset(1 * .625f, -2 * .625f);
            }
            else if (input.Actor.GetStomachSize(20) > 7)
            {
                output.changeSprite(SpriteType.BodyAccent3).AddOffset(60 * .625f, 0);
                output.changeSprite(SpriteType.BodyAccent4).AddOffset(1 * .625f, -1 * .625f);
                output.changeSprite(SpriteType.BodyAccent5).AddOffset(1 * .625f, -1 * .625f);
            }
            else if (input.Actor.GetStomachSize(20) > 3)
            {
                output.changeSprite(SpriteType.BodyAccent3).AddOffset(60 * .625f, 0);
                output.changeSprite(SpriteType.BodyAccent4).AddOffset(1 * .625f, 0);
                output.changeSprite(SpriteType.BodyAccent5).AddOffset(1 * .625f, 0);
            }
            else
            {
                output.changeSprite(SpriteType.BodyAccent3).AddOffset(60 * .625f, 0);
            }
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.BodyAccentType1 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes1);
        });
    });

    internal static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(0, 0, false), // Mouth controller. Index 0.
            new AnimationController.FrameList(0, 0, false)
        }; // Tail controller. Index 1.
    }
}