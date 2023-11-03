#region

using System.Collections.Generic;

#endregion

internal static class Earthworms
{

    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank<EarthWormParameters>, builder =>
    {
        RaceFrameList frameListHeadIdle = new RaceFrameList(new int[5] { 0, 1, 2, 1, 0 }, new float[5] { .5f, .5f, 1.5f, .5f, .5f });


        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.None };
            output.ClothingColors = 0;
            output.GentleAnimation = true;
            output.WeightGainDisabled = true;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EarthwormSkin);
        });


        builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EarthwormSkin, input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Earthworms[8]);
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsEating || input.Params.Position == Position.Underground)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                input.Actor.AnimationController.frameLists[0].currentTime = 0f;

                if (input.Actor.IsEating || input.Actor.IsAttacking)
                {
                    if (input.Params.Position == Position.Underground)
                    {
                        output.Sprite(input.Sprites.Earthworms[16]);
                        return;
                    }

                    output.Sprite(input.Sprites.Earthworms[11]);
                    return;
                }

                if (input.Params.Position == Position.Underground)
                {
                    return;
                }

                output.Sprite(input.Sprites.Earthworms[8]);
                return;
            }

            if (input.Actor.AnimationController.frameLists[0].currentlyActive)
            {
                if (input.Actor.AnimationController.frameLists[0].currentTime >= frameListHeadIdle.Times[input.Actor.AnimationController.frameLists[0].currentFrame])
                {
                    input.Actor.AnimationController.frameLists[0].currentFrame++;
                    input.Actor.AnimationController.frameLists[0].currentTime = 0f;

                    if (input.Actor.AnimationController.frameLists[0].currentFrame >= frameListHeadIdle.Frames.Length)
                    {
                        input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                        input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                        input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Earthworms[8 + frameListHeadIdle.Frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                return;
            }

            if (State.Rand.Next(600) == 0)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = true;
            }

            output.Sprite(input.Sprites.Earthworms[8]);
        });

        builder.RenderSingle(SpriteType.Mouth, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Earthworms[12]);
                return;
            }

            switch (input.Params.Position)
            {
                case Position.Underground:
                    if (input.Actor.IsEating || input.Actor.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Earthworms[17]);
                        return;
                    }

                    return;
                case Position.Aboveground:
                    if (input.Actor.IsEating || input.Actor.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Earthworms[15]);
                        return;
                    }

                    output.Sprite(input.Sprites.Earthworms[12 + frameListHeadIdle.Frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                    return;
                default:
                    return;
            }
        });

        builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EarthwormSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Earthworms[4]);
                return;
            }

            switch (input.Params.Position)
            {
                case Position.Underground:
                    if (input.Actor.IsEating || input.Actor.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Earthworms[1]);
                        return;
                    }

                    output.Sprite(input.Sprites.Earthworms[0]);
                    return;
                case Position.Aboveground:
                    output.Sprite(input.Sprites.Earthworms[4]);
                    return;
            }

            int attackingOffset = input.Actor.IsAttacking ? 1 : 0;
            if (input.Actor.Unit.BodySize == 0)
            {
                output.Sprite(input.Sprites.Bodies[attackingOffset]);
                return;
            }

            int genderOffset = input.Actor.Unit.HasBreasts ? 0 : 8;

            output.Sprite(input.Actor.HasBodyWeight ? input.Sprites.Legs[(input.Actor.Unit.BodySize - 1) * 2 + genderOffset + attackingOffset] : null);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EarthwormSkin, input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                return;
            }

            if (input.Params.Position == Position.Aboveground)
            {
                output.Sprite(input.Sprites.Earthworms[6]);
            }
        }); // belly support

        builder.RenderSingle(SpriteType.BodyAccent2, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EarthwormSkin, input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Earthworms[7]);
                return;
            }

            if (input.Params.Position == Position.Aboveground && input.Actor.HasBelly == false)
            {
                output.Sprite(input.Sprites.Earthworms[7]);
            }
        }); // belly cover

        builder.RenderSingle(SpriteType.BodyAccent3, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EarthwormSkin, input.Actor.Unit.SkinColor));
            if (!input.Actor.Targetable)
            {
                output.Sprite(input.Sprites.Earthworms[5]);
                return;
            }

            if (input.Params.Position == Position.Aboveground)
            {
                output.Sprite(input.Sprites.Earthworms[5]);
            }
        }); // tail

        builder.RenderSingle(SpriteType.BodyAccessory, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (!input.Actor.Targetable)
            {
                return;
            }

            switch (input.Params.Position)
            {
                case Position.Underground:
                    if (input.Actor.IsEating || input.Actor.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Earthworms[3]);
                        return;
                    }

                    output.Sprite(input.Sprites.Earthworms[2]);
                    return;
                case Position.Aboveground:
                    return;
                default:
                    return;
            }
        }); // rocks

        builder.RenderSingle(SpriteType.Belly, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EarthwormSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Params.Position == Position.Aboveground)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
                {
                    output.Sprite(input.Sprites.Earthworms[43]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
                {
                    if (input.Actor.GetStomachSize(21, .76f) == 21)
                    {
                        output.Sprite(input.Sprites.Earthworms[42]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(21, .84f) == 21)
                    {
                        output.Sprite(input.Sprites.Earthworms[41]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(21, .92f) == 21)
                    {
                        output.Sprite(input.Sprites.Earthworms[40]);
                        return;
                    }
                }

                output.Sprite(input.Sprites.Earthworms[18 + input.Actor.GetStomachSize(21)]);
            }
        });


        builder.RunBefore((input, output) =>
        {
            output.Params.Position = !input.Actor.HasAttackedThisCombat ? Position.Underground : Position.Aboveground;
            //base.RunFirst(data.Actor);

            output.ChangeSprite(SpriteType.Belly).AddOffset(0, -48 * .625f);
        });

        builder.RandomCustom(Defaults.RandomCustom);
    });

    private static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(0, 0, false)
        }; // Index 0.
    }

    internal enum Position
    {
        Underground,
        Aboveground
    }
    
    internal class EarthWormParameters : IParameters
    {
        internal Position Position;
    }
}