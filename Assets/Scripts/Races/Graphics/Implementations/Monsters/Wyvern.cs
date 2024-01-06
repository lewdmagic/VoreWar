using System.Collections.Generic;

internal static class Wyvern
{
    internal static readonly IRaceData Instance = RaceBuilder.CreateV2(Defaults.Blank, builder =>
    {
        
        RaceFrameList frameListTail = new RaceFrameList(new int[6] { 2, 1, 0, 5, 4, 3 }, new float[6] { 0.55f, 0.55f, 0.55f, 0.55f, 0.55f, 0.55f });
        RaceFrameList frameListTongue = new RaceFrameList(new int[6] { 0, 1, 2, 3, 4, 5 }, new float[6] { 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f });


        builder.Setup(output =>
        {
            output.Names("Wyvern", "Wyverns");
            output.BonesInfo((unit) => new List<BoneInfo>()
            {
                new BoneInfo(BoneTypes.Wyvern, unit.Name)
            });
            output.FlavorText(new FlavorText(
                new Texts { "winged", "horned", "wiry" },
                new Texts { "mighty", "spined", "great-winged" },
                new Texts { "wyvern", "lesser draconic being", "drake" },
                "Claws"
            ));
            output.RaceTraits(new RaceTraits()
            {
                BodySize = 18,
                StomachSize = 18,
                HasTail = true,
                FavoredStat = Stat.Agility,
                AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.CockVore, VoreType.Anal },
                ExpMultiplier = 1.5f,
                PowerAdjustment = 2f,
                RaceStats = new RaceStats()
                {
                    Strength = new RaceStats.StatRange(8, 20),
                    Dexterity = new RaceStats.StatRange(6, 14),
                    Endurance = new RaceStats.StatRange(12, 20),
                    Mind = new RaceStats.StatRange(12, 28),
                    Will = new RaceStats.StatRange(6, 14),
                    Agility = new RaceStats.StatRange(10, 22),
                    Voracity = new RaceStats.StatRange(10, 18),
                    Stomach = new RaceStats.StatRange(8, 16),
                },
                RacialTraits = new List<Traits>()
                {
                    Traits.Flight,
                },
                RaceDescription = "Fast, winged and ravenous. These lesser cousins of dragons do not have the magical abilities of true dragons, but they are still a dangerous force. They are often followed by their younger kin, but their care only extends as far as not snacking on the weaklings themselves. ",

            });
            output.IndividualNames(new List<string>
            {
                "Swiftwing",
                "Deathtalon",
                "Sharpbeak",
                "Spineback",
            });
            output.CustomizeButtons((unit, buttons) =>
            {
                buttons.SetText(ButtonType.BodyWeight, "Horn Type");
            });
            output.BreastSizes = () => 1;
            output.DickSizes = () => 1;

            output.SkinColors = ColorMap.WyvernColorCount; // Main body, legs, wingarms, head, tail upper
            output.AccessoryColors = ColorMap.WyvernBellyColorCount; // Belly, tail under
            output.ExtraColors1 = ColorMap.WyvernBellyColorCount; // Wings
            output.EyeColors = ColorMap.WyvernColorCount; // Eyes
            output.GentleAnimation = true;

            output.WeightGainDisabled = true;
            output.EyeTypes = 4; // Eye types
            output.BodySizes = 4; // Horn types
        });


        // Wyvern colours: Flame, Crimson, Blue, Sky, Black, Deep Green, Purple, Yellow(Main wyvern palette only), Rose Red, Pale Green,


        builder.RenderSingle(SpriteType.Head, 3, (input, output) =>
        {
            output.Coloring(ColorMap.GetWyvernBellyColor(input.U.AccessoryColor));
            output.Sprite(input.Sprites.Wyvern[54]);
        }); // Lower belly piece
        builder.RenderSingle(SpriteType.Eyes, 0, (input, output) =>
        {
            output.Coloring(ColorMap.GetWyvernBellyColor(input.U.EyeColor));
            output.Sprite(input.Sprites.Wyvern[10 + input.U.EyeType]);
        }); // Eyes
        builder.RenderSingle(SpriteType.Mouth, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.A.IsOralVoring)
            {
                output.Sprite(input.Sprites.Wyvern[9]);
            }
        }); // Inner Mouth

        builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
        {
            output.Coloring(ColorMap.GetWyvernColor(input.U.SkinColor));
            if (input.A.IsAttacking)
            {
                output.Sprite(input.Sprites.Wyvern[8]);
            }
        }); // Footpads

        builder.RenderSingle(SpriteType.Hair2, 6, (input, output) =>
        {
            output.Coloring(ColorMap.GetWyvernColor(input.U.SkinColor));
            if (input.A.IsAttacking)
            {
                output.Sprite(input.Sprites.Wyvern[5]);
                return;
            }

            output.Sprite(input.Sprites.Wyvern[4]);
        }); // Body Overlay

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorMap.GetWyvernColor(input.U.SkinColor));
            if (input.A.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            if (input.A.IsOralVoring)
            {
                output.Sprite(input.Sprites.Wyvern[2]);
                return;
            }

            if (input.A.IsAttacking)
            {
                output.Sprite(input.Sprites.Wyvern[1]);
                return;
            }

            if (input.A.PredatorComponent?.VisibleFullness > 0)
            {
                output.Sprite(input.Sprites.Wyvern[3]);
                return;
            }

            output.Sprite(input.Sprites.Wyvern[0]);
        }); // Body

        builder.RenderSingle(SpriteType.BodyAccent, 0, (input, output) =>
        {
            output.Coloring(ColorMap.GetWyvernColor(input.U.SkinColor));
            if (!input.A.Targetable)
            {
                output.Sprite(input.Sprites.Wyvern[24]);
                return;
            }

            if (input.A.IsAttacking)
            {
                input.A.AnimationController.frameLists[0].currentlyActive = false;
                input.A.AnimationController.frameLists[0].currentFrame = 0;
                input.A.AnimationController.frameLists[0].currentTime = 0f;
                return;
            }

            if (input.A.AnimationController.frameLists[0].currentlyActive)
            {
                if (input.A.AnimationController.frameLists[0].currentTime >= frameListTail.Times[input.A.AnimationController.frameLists[0].currentFrame])
                {
                    input.A.AnimationController.frameLists[0].currentFrame++;
                    input.A.AnimationController.frameLists[0].currentTime = 0f;

                    if (input.A.AnimationController.frameLists[0].currentFrame >= frameListTail.Frames.Length)
                    {
                        input.A.AnimationController.frameLists[0].currentlyActive = false;
                        input.A.AnimationController.frameLists[0].currentFrame = 0;
                        input.A.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Wyvern[22 + frameListTail.Frames[input.A.AnimationController.frameLists[0].currentFrame]]);
                return;
            }

            if (State.Rand.Next(400) == 0)
            {
                input.A.AnimationController.frameLists[0].currentlyActive = true;
            }

            output.Sprite(input.Sprites.Wyvern[24]);
        }); // Tail

        builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
        {
            output.Coloring(ColorMap.GetWyvernBellyColor(input.U.AccessoryColor));
            if (!input.A.Targetable)
            {
                output.Sprite(input.Sprites.Wyvern[30]);
                return;
            }

            if (input.A.IsAttacking)
            {
                input.A.AnimationController.frameLists[0].currentlyActive = false;
                output.Sprite(input.Sprites.Wyvern[6]);
                return;
            }

            if (input.A.AnimationController.frameLists[0].currentlyActive)
            {
                output.Sprite(input.Sprites.Wyvern[28 + frameListTail.Frames[input.A.AnimationController.frameLists[0].currentFrame]]);
                return;
            }

            output.Sprite(input.Sprites.Wyvern[30]);
        }); // Tail Under

        builder.RenderSingle(SpriteType.BodyAccent3, 3, (input, output) =>
        {
            output.Coloring(ColorMap.GetWyvernBellyColor(input.U.ExtraColor1));
            if (input.A.IsAttacking)
            {
                output.Sprite(input.Sprites.Wyvern[19]);
                return;
            }

            output.Sprite(input.Sprites.Wyvern[18]);
        }); // Wings

        builder.RenderSingle(SpriteType.BodyAccessory, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.A.IsAttacking)
            {
                output.Sprite(input.Sprites.Wyvern[21]);
                return;
            }

            output.Sprite(input.Sprites.Wyvern[20]);
        }); // Talons & Claws

        builder.RenderSingle(SpriteType.SecondaryAccessory, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (!input.A.Targetable)
            {
                return;
            }

            if (input.A.IsAttacking || input.A.IsOralVoring)
            {
                input.A.AnimationController.frameLists[1].currentlyActive = false;
                input.A.AnimationController.frameLists[1].currentFrame = 0;
                input.A.AnimationController.frameLists[1].currentTime = 0f;
                return;
            }

            if (input.A.AnimationController.frameLists[1].currentlyActive)
            {
                if (input.A.AnimationController.frameLists[1].currentTime >= frameListTongue.Times[input.A.AnimationController.frameLists[0].currentFrame])
                {
                    input.A.AnimationController.frameLists[1].currentFrame++;
                    input.A.AnimationController.frameLists[1].currentTime = 0f;

                    if (input.A.AnimationController.frameLists[1].currentFrame >= frameListTongue.Frames.Length)
                    {
                        input.A.AnimationController.frameLists[1].currentlyActive = false;
                        input.A.AnimationController.frameLists[1].currentFrame = 0;
                        input.A.AnimationController.frameLists[1].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Wyvern[34 + frameListTongue.Frames[input.A.AnimationController.frameLists[1].currentFrame]]);
                return;
            }

            if (input.A.PredatorComponent?.VisibleFullness > 0 && State.Rand.Next(600) == 0)
            {
                input.A.AnimationController.frameLists[1].currentlyActive = true;
            }
        }); // Tongue

        builder.RenderSingle(SpriteType.BodySize, 1, (input, output) =>
        {
            output.Coloring(ColorMap.GetWyvernBellyColor(input.U.AccessoryColor));
            output.Sprite(input.Sprites.Wyvern[14 + input.U.BodySize]);
        }); // Horns
        builder.RenderSingle(SpriteType.Belly, 5, (input, output) =>
        {
            output.Coloring(ColorMap.GetWyvernBellyColor(input.U.AccessoryColor));

            if (input.A.GetUniversalSize(1) == 0)
            {
                output.Sprite(input.Sprites.Wyvern[7]);
                return;
            }

            output.Sprite(input.Sprites.Wyvern[40 + input.A.GetUniversalSize(9, .8f)]);
        }); // Belly

        builder.RenderSingle(SpriteType.Dick, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.A.IsCockVoring)
            {
                output.Sprite(input.Sprites.Wyvern[53]);
                return;
            }

            if (input.A.IsUnbirthing)
            {
                output.Sprite(input.Sprites.Wyvern[51]);
                return;
            }

            if (input.A.IsAnalVoring)
            {
                output.Sprite(input.Sprites.Wyvern[51]);
                return;
            }

            if (input.A.IsErect())
            {
                output.Sprite(input.Sprites.Wyvern[52]);
            }
        }); // Dick, CV, UB

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.BodySize = State.Rand.Next(0, 4);
            unit.EyeType = State.Rand.Next(0, 3);
            while (unit.SkinColor == unit.EyeColor)
            {
                unit.EyeColor = State.Rand.Next(0, ColorMap.WyvernColorCount);
            }

            string name = GetRaceSpecialName(unit);
            if (name != null)
            {
                unit.Name = name;
            }
        });
    });

    private static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(0, 0, false), // Tail controller. Index 0.
            new AnimationController.FrameList(0, 0, false)
        }; // Tongue controller. Index 1.
    }

    private static string GetRaceSpecialName(Unit unit)
    {
        int rand = State.Rand.Next(0, 20);

        if (rand <= 2)
        {
            switch (unit.SkinColor)
            {
                case 0: return "Flamescale";
                case 1: return "Bloodscale";
                case 2: return "Cobaltscale";
                case 3: return "Skyscale";
                case 4: return "Ebonscale";
                case 5: return "Jadescale";
                case 6: return "Duskscale";
                case 7: return "Sunscale";
                case 8: return "Rosescale";
                case 9: return "Budscale";
                case 10: return "Dustscale";
            }
        }

        if (rand <= 3 && unit.EyeType == 2)
        {
            return "Blackeye";
        }

        if (rand <= 5 && unit.EyeType == 2)
        {
            return "Darkeye";
        }

        if (rand <= 5)
        {
            switch (unit.ExtraColor1)
            {
                case 0: return "Firewing";
                case 1: return "Gorewing";
                case 2: return "Thunderwing";
                case 3: return "Skywing";
                case 4: return "Nightwing";
                case 5: return "Jadewing";
                case 6: return "Duskwing";
                case 7: return "Sunsetwing";
                case 8: return "Leafwing";
                case 9: return "Dustscale";
            }
        }

        if (rand <= 8)
        {
            switch (unit.SkinColor)
            {
                case 0: return "Firescale";
                case 1: return "Crimsonscale";
                case 2: return "Thunderscale";
                case 3: return "Aquascale";
                case 4: return "Darkscale";
                case 5: return "Viridscale";
                case 6: return "Poisonscale";
                case 7: return "Brightscale";
                case 8: return "Warmscale";
                case 9: return "Mintscale";
                case 10: return "Sandscale";
            }
        }

        if (rand <= 11)
        {
            switch (unit.ExtraColor1)
            {
                case 0: return "Flamewing";
                case 1: return "Crimsonwing";
                case 2: return "Deepwing";
                case 3: return "Aquawing";
                case 4: return "Shadowwing";
                case 5: return "Viridwing";
                case 6: return "Poisonwing";
            }
        }

        return null;
    }
}