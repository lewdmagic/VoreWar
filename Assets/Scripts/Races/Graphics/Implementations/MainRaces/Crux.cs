#region

using System.Collections.Generic;
using CruxClothing;

#endregion

internal static class Crux
{
    internal static readonly IRaceData Instance = RaceBuilder.CreateV2(Defaults.Default, builder =>
    {
        RaceFrameList frameListDrool = new RaceFrameList(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }, new[] { .8f, .6f, .5f, .4f, .4f, .4f, .4f, .4f, .4f });
        // currently unused
        //RaceFrameList frameListWet = new RaceFrameList(new [] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new [] { .4f, .8f, .8f, .7f, .6f, .5f, .4f, .4f, .4f, .4f });


        builder.Setup(output =>
        {
        output.Names("Crux", "Crux");
        output.WallType(WallType.Crux);
        output.FlavorText(new FlavorText(
            new Texts { "crazy", "curly eared", "complaining" },
            new Texts { "curly eared", "crazed", "eager" },
            new Texts { "crux", "lab-critter", "gene-engineered creature" },
            new Dictionary<string, string>
            {
                [WeaponNames.Mace]        = "Bat",
                [WeaponNames.Axe]         = "Machete",
                [WeaponNames.SimpleBow]   = "Handbow",
                [WeaponNames.CompoundBow] = "Compound Bow",
                [WeaponNames.Claw]        = "Claws"
            }
            /* TODO special case?
                         if (weapon.Name == "WeaponNames.Mace" && unit.BasicMeleeWeaponType == 0) return "Bat";
                        else if (weapon.Name == "WeaponNames.Mace" && unit.BasicMeleeWeaponType == 1) return "Pipe";
                        else if (weapon.Name == "WeaponNames.Mace" && unit.BasicMeleeWeaponType == 2) return "Dildo";
                        else if (weapon.Name == "WeaponNames.Axe" && unit.AdvancedMeleeWeaponType == 0) return "Machete";
                        else if (weapon.Name == "WeaponNames.Axe" && unit.AdvancedMeleeWeaponType == 1) return "WeaponNames.Axe";
                        else if (weapon.Name == "Simple Bow") return "Handbow";
                        else if (weapon.Name == "Compound Bow") return "Compound Bow";
                        else if (weapon.Name == "WeaponNames.Claw") return "Claws";
             */
        ));
        output.RaceTraits(new RaceTraits()
        {
            BodySize = 9,
            StomachSize = 14,
            HasTail = true,
            FavoredStat = Stat.Will,
            RacialTraits = new List<Traits>()
            {
                Traits.KeenReflexes,
                Traits.EscapeArtist,
                Traits.MadScience
            },
            RaceDescription = "Their own world having risen and fallen, the Crux arrived to this one almost by accident. While they initially thought it rather a boring place, they soon realised its potential and were eager to try and shape it according to their own ever shifting ideals.",
        });
        output.CustomizeButtons((unit, buttons) =>
        {
            buttons.SetText(ButtonType.ClothingColor2, "Pack / Boxer Color");
            buttons.SetActive(ButtonType.ClothingColor2, true);
            buttons.SetText(ButtonType.BodyWeight, "Body Type");
            buttons.SetText(ButtonType.EyeType, "Face Expression");
            buttons.SetText(ButtonType.ExtraColor1, "Primary Color");
            buttons.SetText(ButtonType.ExtraColor2, "Secondary Color");
            buttons.SetText(ButtonType.ExtraColor3, "Flesh Color");
            // TODO uhh, not sure what to do here
            //buttons[UnitCustomizer.ButtonTypes.ExtraColor4].gameObject.SetActive(Config.HideCocks == false && actor.Unit.GetBestMelee().Damage == 4);
            buttons.SetText(ButtonType.ExtraColor4, "Dildo Color");
            buttons.SetText(ButtonType.FurTypes, "Head Fluff");
            buttons.SetText(ButtonType.BodyAccentTypes1, "Visible Areola");
            buttons.SetText(ButtonType.BodyAccentTypes2, "Leg Stripes");
            buttons.SetText(ButtonType.BodyAccentTypes3, "Arm Stripes");
        });
        output.TownNames(new List<string>
        {
            "The Gate",
            "Facility 789",
            "Ingenuity",
            "Cruxus",
            "Facility Ornd"
        });
            output.BreastSizes = () => 7;
            output.DickSizes = () => 9;


            output.ExtraColors1 = 7; // Main colour
            output.ExtraColors2 = 7; // Secondary colour
            output.ExtraColors3 = 8; // Flesh colour
            output.ExtraColors4 = ColorMap.SlimeColorCount;
            output.EyeColors = ColorMap.EyeColorCount;

            output.SkinColors = 0;
            output.HairColors = 0;
            output.AccessoryColors = 0;
            output.MouthTypes = 0;

            output.WeightGainDisabled = true;

            output.ClothingColors = ColorMap.ClothingColorCount;

            output.GentleAnimation = false;

            output.BodySizes = 4;
            output.HairStyles = 14;

            output.HeadTypes = 4;
            output.TailTypes = 16;
            output.FurTypes = 11;
            output.EarTypes = 17;
            output.BodyAccentTypes1 = 2; // Used for checking if breasts have a visible areola or not.
            output.BodyAccentTypes2 = 7; // Leg Stripe patterns.
            output.BodyAccentTypes3 = 5; // Arm Stripe patterns.
            output.BallsSizes = 3;
            output.VulvaTypes = 3;
            output.BasicMeleeWeaponTypes = 2;
            output.AdvancedMeleeWeaponTypes = 2;
            output.BasicRangedWeaponTypes = 2;
            output.AdvancedRangedWeaponTypes = 2;


            output.AvoidedMainClothingTypes = 2;
            output.AllowedMainClothingTypes.Set(
                CruxClothingTypes.TShirtInstance,
                CruxClothingTypes.NetShirtInstance,
                CruxClothingTypes.RaggedBraInstance,
                CruxClothingTypes.LabCoatInstance,
                CruxClothingTypes.RagsInstance,
                CruxClothingTypes.SlaveCollarInstance
            );
            output.AllowedWaistTypes.Set(
                CruxClothingTypes.CruxJeansInstance,
                CruxClothingTypes.Boxers1Instance,
                CruxClothingTypes.Boxers2Instance,
                CruxClothingTypes.FannyBagInstance,
                CruxClothingTypes.BeltBagsInstance
            );
            output.AllowedClothingHatTypes.Set(
            );
            output.AllowedClothingAccessoryTypes.Set(
                CruxClothingTypes.NecklaceGoldInstance,
                CruxClothingTypes.NecklaceCruxInstance
            );
        });


        // These check which types of a body and head the crux has, then return the appropriate sprite.


        builder.RenderSingle(SpriteType.Head, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.Crux, input.U.ExtraColor1 * 56 + input.U.ExtraColor2 * 8 + input.U.ExtraColor3));
            output.Sprite(input.Sprites.Crux[6 + input.U.HeadType]);
        }); // A coconut.
        builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
        {
            output.Coloring(ColorMap.GetEyeColor(input.U.EyeColor));
            output.Sprite(input.Sprites.Crux[30 + input.U.EyeType]);
        }); // The whole expression.

        builder.RenderSingle(SpriteType.SecondaryEyes, 18, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Crux, input.U.ExtraColor1 * 56 + input.U.ExtraColor2 * 8 + input.U.ExtraColor3));
                if (input.U.BodySize <= 1)
                {
                    if (input.U.HasWeapon == false)
                    {
                        if (input.A.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Crux[401]);
                            return;
                        }

                        return;
                    }

                    switch (input.A.GetWeaponSprite())
                    {
                        case 0:
                            output.Sprite(input.Sprites.Crux[401]);
                            return;
                        case 1:
                            output.Sprite(input.Sprites.Crux[405]);
                            return;
                        case 2:
                            output.Sprite(input.Sprites.Crux[401]);
                            return;
                        case 3:
                            output.Sprite(input.Sprites.Crux[405]);
                            return;
                        case 4:
                            output.Sprite(input.Sprites.Crux[401]);
                            return;
                        case 5:
                            output.Sprite(input.Sprites.Crux[407]);
                            return;
                        case 6:
                            output.Sprite(input.Sprites.Crux[403]);
                            return;
                        case 7:
                            output.Sprite(input.Sprites.Crux[407]);
                            return;
                        default:
                            return;
                    }
                }

                if (input.U.HasWeapon == false)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Crux[402]);
                        return;
                    }

                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Crux[402]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Crux[406]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Crux[402]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Crux[406]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Crux[402]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.Crux[408]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Crux[404]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Crux[408]);
                        return;
                    default:
                        return;
                }
            }); // Hands.


        builder.RenderSingle(SpriteType.Mouth, 19, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.Crux, input.U.ExtraColor1 * 56 + input.U.ExtraColor2 * 8 + input.U.ExtraColor3));
            if (input.A.IsOralVoring)
            {
                if (input.U.HeadType == 0 || input.U.HeadType == 2)
                {
                    output.Sprite(input.Sprites.Crux[10]);
                    return;
                }

                output.Sprite(input.Sprites.Crux[11]);
            }
        }); // Open mouth.

        builder.RenderSingle(SpriteType.Hair, 22, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.Crux, input.U.ExtraColor1 * 56 + input.U.ExtraColor2 * 8 + input.U.ExtraColor3));
            output.Sprite(input.Sprites.Crux[76 + input.U.HairStyle]);
        }); // Hair.

        builder.RenderSingle(SpriteType.Hair2, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.Crux, input.U.ExtraColor1 * 56 + input.U.ExtraColor2 * 8 + input.U.ExtraColor3));
            output.Sprite(input.Sprites.Crux[13 + input.U.EarType]);
        }); // Ears.

        builder.RenderSingle(SpriteType.Hair3, 0, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.Crux, input.U.ExtraColor1 * 56 + input.U.ExtraColor2 * 8 + input.U.ExtraColor3));
            output.Sprite(input.Sprites.Crux[60 + input.U.TailType]);
        }); // Tail.

        builder.RenderSingle(SpriteType.Beard, 15, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.Crux, input.U.ExtraColor1 * 56 + input.U.ExtraColor2 * 8 + input.U.ExtraColor3));
            if (input.U.FurType == 0)
            {
                return;
            }

            output.Sprite(input.Sprites.Crux[49 + input.U.FurType]);
        }); // Neck/Cheek fur.

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.Crux, input.U.ExtraColor1 * 56 + input.U.ExtraColor2 * 8 + input.U.ExtraColor3));
            if (input.A.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            output.Sprite(input.Sprites.Crux[input.U.BodySize]);
        }); // Main body.


        builder.RenderSingle(SpriteType.BodyAccent, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.Crux, input.U.ExtraColor1 * 56 + input.U.ExtraColor2 * 8 + input.U.ExtraColor3));
            if (input.U.BodySize <= 1)
            {
                if (input.U.HasWeapon == false)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Crux[351]);
                        return;
                    }

                    output.Sprite(input.Sprites.Crux[4]);
                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Crux[351]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Crux[355]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Crux[351]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Crux[355]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Crux[351]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.Crux[357]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Crux[353]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Crux[357]);
                        return;
                    default:
                        return;
                }
            }

            if (input.U.HasWeapon == false)
            {
                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Crux[352]);
                    return;
                }

                output.Sprite(input.Sprites.Crux[5]);
                return;
            }

            switch (input.A.GetWeaponSprite())
            {
                case 0:
                    output.Sprite(input.Sprites.Crux[352]);
                    return;
                case 1:
                    output.Sprite(input.Sprites.Crux[356]);
                    return;
                case 2:
                    output.Sprite(input.Sprites.Crux[352]);
                    return;
                case 3:
                    output.Sprite(input.Sprites.Crux[356]);
                    return;
                case 4:
                    output.Sprite(input.Sprites.Crux[352]);
                    return;
                case 5:
                    output.Sprite(input.Sprites.Crux[358]);
                    return;
                case 6:
                    output.Sprite(input.Sprites.Crux[354]);
                    return;
                case 7:
                    output.Sprite(input.Sprites.Crux[358]);
                    return;
                default:
                    return;
            }
        }); // Arms.


        builder.RenderSingle(SpriteType.BodyAccent2, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.Crux, input.U.ExtraColor1 * 56 + input.U.ExtraColor2 * 8 + input.U.ExtraColor3));
            if (input.U.BodyAccentType2 == 0)
            {
                return;
            }

            output.Sprite(input.Sprites.Crux[271 + input.U.BodyAccentType2]);
        }); // Leg stripes.

        builder.RenderSingle(SpriteType.BodyAccent3, 10, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.Crux, input.U.ExtraColor1 * 56 + input.U.ExtraColor2 * 8 + input.U.ExtraColor3));
            switch (input.U.BodyAccentType3)
            {
                case 0: return;
                case 1:
                {
                    if (input.U.HasWeapon == false || input.A.Surrendered)
                    {
                        output.Sprite(input.Sprites.Crux[278]);
                        return;
                    }

                    switch (input.A.GetWeaponSprite())
                    {
                        case 0:
                            output.Sprite(input.Sprites.Crux[278]);
                            return;
                        case 1:
                            output.Sprite(input.Sprites.Crux[283]);
                            return;
                        case 2:
                            output.Sprite(input.Sprites.Crux[278]);
                            return;
                        case 3:
                            output.Sprite(input.Sprites.Crux[283]);
                            return;
                        case 4:
                            output.Sprite(input.Sprites.Crux[278]);
                            return;
                        case 5:
                            output.Sprite(input.Sprites.Crux[284]);
                            return;
                        case 6:
                            output.Sprite(input.Sprites.Crux[278]);
                            return;
                        case 7:
                            output.Sprite(input.Sprites.Crux[284]);
                            return;
                        default:
                            output.Sprite(input.Sprites.Crux[278]);
                            return;
                    }
                }
                case 2:
                {
                    if (input.U.HasWeapon == false || input.A.Surrendered)
                    {
                        output.Sprite(input.Sprites.Crux[279]);
                        return;
                    }

                    switch (input.A.GetWeaponSprite())
                    {
                        case 0:
                            output.Sprite(input.Sprites.Crux[279]);
                            return;
                        case 1:
                            output.Sprite(input.Sprites.Crux[285]);
                            return;
                        case 2:
                            output.Sprite(input.Sprites.Crux[279]);
                            return;
                        case 3:
                            output.Sprite(input.Sprites.Crux[285]);
                            return;
                        case 4:
                            output.Sprite(input.Sprites.Crux[279]);
                            return;
                        case 5:
                            output.Sprite(input.Sprites.Crux[286]);
                            return;
                        case 6:
                            output.Sprite(input.Sprites.Crux[378]);
                            return;
                        case 7:
                            output.Sprite(input.Sprites.Crux[286]);
                            return;
                        default:
                            output.Sprite(input.Sprites.Crux[279]);
                            return;
                    }
                }
                case 3:
                {
                    if (input.U.HasWeapon == false || input.A.Surrendered)
                    {
                        output.Sprite(input.Sprites.Crux[280]);
                        return;
                    }

                    switch (input.A.GetWeaponSprite())
                    {
                        case 0:
                            output.Sprite(input.Sprites.Crux[282]);
                            return;
                        case 1:
                            output.Sprite(input.Sprites.Crux[287]);
                            return;
                        case 2:
                            output.Sprite(input.Sprites.Crux[282]);
                            return;
                        case 3:
                            output.Sprite(input.Sprites.Crux[287]);
                            return;
                        case 4:
                            output.Sprite(input.Sprites.Crux[282]);
                            return;
                        case 5:
                            output.Sprite(input.Sprites.Crux[288]);
                            return;
                        case 6:
                            output.Sprite(input.Sprites.Crux[282]);
                            return;
                        case 7:
                            output.Sprite(input.Sprites.Crux[288]);
                            return;
                        default:
                            output.Sprite(input.Sprites.Crux[280]);
                            return;
                    }
                }
                case 4:
                {
                    if (input.U.HasWeapon == false || input.A.Surrendered)
                    {
                        output.Sprite(input.Sprites.Crux[281]);
                        return;
                    }

                    switch (input.A.GetWeaponSprite())
                    {
                        case 0:
                            output.Sprite(input.Sprites.Crux[281]);
                            return;
                        case 1:
                            output.Sprite(input.Sprites.Crux[289]);
                            return;
                        case 2:
                            output.Sprite(input.Sprites.Crux[281]);
                            return;
                        case 3:
                            output.Sprite(input.Sprites.Crux[289]);
                            return;
                        case 4:
                            output.Sprite(input.Sprites.Crux[281]);
                            return;
                        case 5:
                            output.Sprite(input.Sprites.Crux[290]);
                            return;
                        case 6:
                            output.Sprite(input.Sprites.Crux[281]);
                            return;
                        case 7:
                            output.Sprite(input.Sprites.Crux[290]);
                            return;
                        default:
                            output.Sprite(input.Sprites.Crux[281]);
                            return;
                    }
                }
                default: return;
            }
        }); // Arm stripes.

        builder.RenderSingle(SpriteType.BodyAccent4, 20, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.A.IsOralVoring)
            {
                output.Sprite(input.Sprites.Crux[12]);
            }
        }); // Teeth.

        builder.RenderSingle(SpriteType.BodyAccent5, 16, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (!input.A.Targetable)
            {
                return;
            }

            if (input.A.IsAttacking || input.A.IsOralVoring)
            {
                input.A.AnimationController.frameLists[0].currentlyActive = false;
                input.A.AnimationController.frameLists[0].currentFrame = 0;
                input.A.AnimationController.frameLists[0].currentTime = 0f;
                return;
            }

            if (input.A.AnimationController.frameLists[0].currentlyActive)
            {
                if (input.A.AnimationController.frameLists[0].currentTime >=
                    frameListDrool.Times[input.A.AnimationController.frameLists[0].currentFrame])
                {
                    input.A.AnimationController.frameLists[0].currentFrame++;
                    input.A.AnimationController.frameLists[0].currentTime = 0f;

                    if (input.A.AnimationController.frameLists[0].currentFrame >= frameListDrool.Frames.Length)
                    {
                        input.A.AnimationController.frameLists[0].currentlyActive = false;
                        input.A.AnimationController.frameLists[0].currentFrame = 0;
                        input.A.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Crux[291 + frameListDrool.Frames[input.A.AnimationController.frameLists[0].currentFrame]]);
                return;
            }

            if (input.A.PredatorComponent?.VisibleFullness > 0 && State.Rand.Next(600) == 0)
            {
                input.A.AnimationController.frameLists[0].currentlyActive = true;
            }
        }); // Drool animation.

        builder.RenderSingle(SpriteType.BodyAccessory, 23, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            Accessory acc = null;
            if (input.U.Items == null || input.U.Items.Length < 1)
            {
                return;
            }

            if (input.U.Items[0] is Accessory)
            {
                acc = (Accessory)input.U.Items[0];
            }

            if (acc == null)
            {
                return;
            }

            output.Layer(23);

            if (acc == State.World.ItemRepository.GetItem(ItemType.Helmet))
            {
                if (input.U.EarType >= 7)
                {
                    output.Sprite(input.Sprites.Crux[374]);
                    return;
                }

                output.Sprite(input.Sprites.Crux[373]);
                return;
            }

            if (acc == State.World.ItemRepository.GetItem(ItemType.BodyArmor))
            {
                output.Sprite(input.Sprites.Crux[375]).Layer(21);
                return;
            }

            if (acc == State.World.ItemRepository.GetItem(ItemType.Shoes))
            {
                output.Sprite(input.Sprites.Crux[386]).Layer(11);
                return;
            }

            if (acc == State.World.ItemRepository.GetItem(ItemType.Gloves))
            {
                if (input.U.HasWeapon == false || input.A.Surrendered)
                {
                    output.Sprite(input.Sprites.Crux[376]);
                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Crux[377]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Crux[379]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Crux[377]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Crux[379]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Crux[377]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.Crux[380]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Crux[378]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Crux[380]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Crux[376]);
                        return;
                }
            }

            if (acc == State.World.ItemRepository.GetItem(ItemType.Gauntlet))
            {
                if (input.U.HasWeapon == false || input.A.Surrendered)
                {
                    output.Sprite(input.Sprites.Crux[381]);
                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Crux[382]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Crux[384]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Crux[382]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Crux[384]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Crux[382]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.Crux[385]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Crux[383]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Crux[385]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Crux[381]);
                        return;
                }
            }
        });

        builder.RandomCustom(data =>
        {
            Unit unit = data.Unit;
            Defaults.RandomCustom(data);

            unit.BodySize = State.Rand.Next(0, data.MiscRaceData.BodySizes);
            unit.HeadType = State.Rand.Next(0, data.MiscRaceData.HeadTypes);
            unit.TailType = State.Rand.Next(0, data.MiscRaceData.TailTypes);
            unit.FurType = State.Rand.Next(0, data.MiscRaceData.FurTypes);
            unit.EarType = State.Rand.Next(0, data.MiscRaceData.EarTypes);
            unit.BallsSize = State.Rand.Next(0, data.MiscRaceData.BallsSizes);
            unit.VulvaType = State.Rand.Next(0, data.MiscRaceData.VulvaTypes);
            unit.BodyAccentType1 = State.Rand.Next(0, data.MiscRaceData.BodyAccentTypes1);
            unit.BodyAccentType2 = State.Rand.Next(0, data.MiscRaceData.BodyAccentTypes2);
            unit.BodyAccentType3 = State.Rand.Next(0, data.MiscRaceData.BodyAccentTypes3);

            unit.BasicMeleeWeaponType = State.Rand.Next(0, data.MiscRaceData.BasicMeleeWeaponTypes);
            unit.AdvancedMeleeWeaponType = State.Rand.Next(0, data.MiscRaceData.AdvancedMeleeWeaponTypes);
            unit.BasicRangedWeaponType = State.Rand.Next(0, data.MiscRaceData.BasicRangedWeaponTypes);
            unit.AdvancedRangedWeaponType = State.Rand.Next(0, data.MiscRaceData.AdvancedRangedWeaponTypes);

            if (!Config.HideCocks)
            {
                if (State.Rand.Next(0, 100) < 20)
                {
                    unit.BasicMeleeWeaponType = 2;
                }
            }

            unit.ClothingColor = State.Rand.Next(0, data.MiscRaceData.ClothingColors);
            unit.ClothingColor2 = State.Rand.Next(0, data.MiscRaceData.ClothingColors);

            if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
            {
                unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypesBasic, CruxClothingTypes.RagsInstance);
                if (unit.ClothingType == -1) //Covers rags not in the list
                {
                    unit.ClothingType = 1;
                }
            }
        });
    });


    internal static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(), // Drool controller. Index 0.
            new AnimationController.FrameList()
        }; // Wetness controller. Index 1.
    }
}