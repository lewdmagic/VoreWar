#region

using System.Collections.Generic;

#endregion

internal static class Catfish
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Names("Catfish", "Catfish");
        builder.RaceTraits(new RaceTraits()
        {
            BodySize = 16,
            StomachSize = 16,
            HasTail = true,
            FavoredStat = Stat.Stomach,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.2f,
            PowerAdjustment = 1.5f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 12),
                Dexterity = new RaceStats.StatRange(6, 10),
                Endurance = new RaceStats.StatRange(16, 24),
                Mind = new RaceStats.StatRange(8, 12),
                Will = new RaceStats.StatRange(8, 12),
                Agility = new RaceStats.StatRange(10, 16),
                Voracity = new RaceStats.StatRange(20, 28),
                Stomach = new RaceStats.StatRange(12, 20),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.Slippery,
                Traits.Ravenous,
                Traits.Nauseous,
                Traits.SlowDigestion
            },
            RaceDescription = ""
        });
        
        builder.CustomizeButtons((unit, buttons) =>
        {
            buttons.SetText(ButtonType.Skintone, "Body Color");
            buttons.SetText(ButtonType.BodyAccessoryType, "Barbel (Whisker) Type");
            buttons.SetText(ButtonType.BodyAccentTypes1, "Dorsal Fin Type");
        });
        
        RaceFrameList frameListMouth = new RaceFrameList(new[] { 0, 1, 2, 1, 0, 1, 2, 1, 0 }, new[] { 1.2f, .6f, 1.2f, .6f, 1.2f, .6f, 1.2f, .6f, 1.2f });
        RaceFrameList frameListTail = new RaceFrameList(new[] { 0, 1, 2, 1, 0, 1, 2, 1, 0 }, new[] { .5f, .3f, .5f, .3f, .5f, .3f, .5f, .3f, .5f });


        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.None };
            output.SpecialAccessoryCount = 6; // barbels
            output.BodyAccentTypes1 = 8; // dorsal fins
            output.ClothingColors = 0;
            output.GentleAnimation = true;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.CatfishSkin);
            output.EyeColors = ColorPaletteMap.GetPaletteCount(SwapType.ViperSkin);
        });


        builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.CatfishSkin, input.U.SkinColor));
            if (!input.A.Targetable)
            {
                output.Sprite(input.Sprites.Catfish[4]);
                return;
            }

            if (input.A.IsAttacking || input.A.IsOralVoring)
            {
                input.A.AnimationController.frameLists[0].currentlyActive = false;
                input.A.AnimationController.frameLists[0].currentFrame = 0;
                input.A.AnimationController.frameLists[0].currentTime = 0f;
                output.Sprite(input.Sprites.Catfish[7]);
                return;
            }

            if (input.A.AnimationController.frameLists[0].currentlyActive)
            {
                if (input.A.AnimationController.frameLists[0].currentTime >= frameListMouth.Times[input.A.AnimationController.frameLists[0].currentFrame])
                {
                    input.A.AnimationController.frameLists[0].currentFrame++;
                    input.A.AnimationController.frameLists[0].currentTime = 0f;

                    if (input.A.AnimationController.frameLists[0].currentFrame >= frameListMouth.Frames.Length)
                    {
                        input.A.AnimationController.frameLists[0].currentlyActive = false;
                        input.A.AnimationController.frameLists[0].currentFrame = 0;
                        input.A.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Catfish[4 + frameListMouth.Frames[input.A.AnimationController.frameLists[0].currentFrame]]);
                return;
            }

            if (State.Rand.Next(800) == 0)
            {
                input.A.AnimationController.frameLists[0].currentlyActive = true;
            }

            output.Sprite(input.Sprites.Catfish[4]);
        });

        builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.CatfishSkin, input.U.EyeColor));
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
            if (!input.A.Targetable)
            {
                output.Sprite(input.Sprites.Catfish[8]);
                return;
            }

            if (input.A.IsAttacking || input.A.IsOralVoring)
            {
                input.A.AnimationController.frameLists[0].currentlyActive = false;
                output.Sprite(input.Sprites.Catfish[11]);
                return;
            }

            if (input.A.AnimationController.frameLists[0].currentlyActive)
            {
                output.Sprite(input.Sprites.Catfish[8 + frameListMouth.Frames[input.A.AnimationController.frameLists[0].currentFrame]]);
                return;
            }

            output.Sprite(input.Sprites.Catfish[8]);
        });

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.CatfishSkin, input.U.SkinColor));
            if (input.A.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            if (input.A.HasBelly == false)
            {
                output.Sprite(input.Sprites.Catfish[0]);
                return;
            }

            output.Sprite(input.Sprites.Catfish[60 + input.A.GetStomachSize(20)]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.CatfishSkin, input.U.SkinColor));
            output.Sprite(input.Sprites.Catfish[28 + input.U.BodyAccentType1]);
        }); // dorsal fins
        builder.RenderSingle(SpriteType.BodyAccent2, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.CatfishSkin, input.U.SkinColor));
            output.Sprite(input.Sprites.Catfish[18 + input.U.SpecialAccessoryType]);
        }); // barbels secondary
        builder.RenderSingle(SpriteType.BodyAccent3, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.CatfishSkin, input.U.SkinColor));
            if (!input.A.Targetable)
            {
                output.Sprite(input.Sprites.Catfish[1]);
                return;
            }

            if (input.A.IsAttacking || input.A.IsOralVoring)
            {
                input.A.AnimationController.frameLists[1].currentlyActive = false;
                input.A.AnimationController.frameLists[1].currentFrame = 0;
                input.A.AnimationController.frameLists[1].currentTime = 0f;
                output.Sprite(input.Sprites.Catfish[1]);
                return;
            }

            if (input.A.AnimationController.frameLists[1].currentlyActive)
            {
                if (input.A.AnimationController.frameLists[1].currentTime >= frameListTail.Times[input.A.AnimationController.frameLists[0].currentFrame])
                {
                    input.A.AnimationController.frameLists[1].currentFrame++;
                    input.A.AnimationController.frameLists[1].currentTime = 0f;

                    if (input.A.AnimationController.frameLists[1].currentFrame >= frameListTail.Frames.Length)
                    {
                        input.A.AnimationController.frameLists[1].currentlyActive = false;
                        input.A.AnimationController.frameLists[1].currentFrame = 0;
                        input.A.AnimationController.frameLists[1].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Catfish[1 + frameListTail.Frames[input.A.AnimationController.frameLists[1].currentFrame]]);
                return;
            }

            if (State.Rand.Next(800) == 0)
            {
                input.A.AnimationController.frameLists[1].currentlyActive = true;
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
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.CatfishSkin, input.U.SkinColor));
            output.Sprite(input.Sprites.Catfish[27]);
        }); // pelvic fin
        builder.RenderSingle(SpriteType.BodyAccessory, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.CatfishSkin, input.U.SkinColor));
            output.Sprite(input.Sprites.Catfish[12 + input.U.SpecialAccessoryType]);
        }); // barbels
        builder.RenderSingle(SpriteType.Belly, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.CatfishSkin, input.U.SkinColor));
            if (input.A.HasBelly == false)
            {
                return;
            }

            output.Sprite(input.Sprites.Catfish[36 + input.A.GetStomachSize(20)]);
        });


        builder.RunBefore((input, output) =>
        {
            if (input.U.Predator && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) && input.A.GetStomachSize(20) == 20)
            {
                output.ChangeSprite(SpriteType.Body).AddOffset(0, 10 * .625f);
                output.ChangeSprite(SpriteType.Head).AddOffset(0, 10 * .625f);
                output.ChangeSprite(SpriteType.BodyAccessory).AddOffset(0, 10 * .625f);
                output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 10 * .625f);
                output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 10 * .625f);
                output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(60 * .625f, 10 * .625f);
                output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 8 * .625f);
                output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 8 * .625f);
                output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 10 * .625f);
                output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 10 * .625f);
                output.ChangeSprite(SpriteType.SecondaryEyes).AddOffset(0, 10 * .625f);
            }
            else if (input.U.Predator && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) && input.A.GetStomachSize(20, .8f) == 20)
            {
                output.ChangeSprite(SpriteType.Body).AddOffset(0, 6 * .625f);
                output.ChangeSprite(SpriteType.Head).AddOffset(0, 6 * .625f);
                output.ChangeSprite(SpriteType.BodyAccessory).AddOffset(0, 6 * .625f);
                output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 6 * .625f);
                output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 6 * .625f);
                output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(60 * .625f, 6 * .625f);
                output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 4 * .625f);
                output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 4 * .625f);
                output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 6 * .625f);
                output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 6 * .625f);
                output.ChangeSprite(SpriteType.SecondaryEyes).AddOffset(0, 6 * .625f);
            }
            else if (input.U.Predator && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) && input.A.GetStomachSize(20, .9f) == 20)
            {
                output.ChangeSprite(SpriteType.Body).AddOffset(0, 3 * .625f);
                output.ChangeSprite(SpriteType.Head).AddOffset(0, 3 * .625f);
                output.ChangeSprite(SpriteType.BodyAccessory).AddOffset(0, 3 * .625f);
                output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 3 * .625f);
                output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 3 * .625f);
                output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(60 * .625f, 3 * .625f);
                output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 1 * .625f);
                output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 1 * .625f);
                output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 3 * .625f);
                output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 3 * .625f);
                output.ChangeSprite(SpriteType.SecondaryEyes).AddOffset(0, 3 * .625f);
            }
            else if (input.A.GetStomachSize(20) > 11)
            {
                output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(60 * .625f, 0);
                output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(1 * .625f, -2 * .625f);
                output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(1 * .625f, -2 * .625f);
            }
            else if (input.A.GetStomachSize(20) > 7)
            {
                output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(60 * .625f, 0);
                output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(1 * .625f, -1 * .625f);
                output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(1 * .625f, -1 * .625f);
            }
            else if (input.A.GetStomachSize(20) > 3)
            {
                output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(60 * .625f, 0);
                output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(1 * .625f, 0);
                output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(1 * .625f, 0);
            }
            else
            {
                output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(60 * .625f, 0);
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