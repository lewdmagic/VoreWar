#region

using CruxClothing;

#endregion

internal static class Crux
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Default, builder =>
    {
        RaceFrameList frameListDrool = new RaceFrameList(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }, new[] { .8f, .6f, .5f, .4f, .4f, .4f, .4f, .4f, .4f });
        // currently unused
        //RaceFrameList frameListWet = new RaceFrameList(new [] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new [] { .4f, .8f, .8f, .7f, .6f, .5f, .4f, .4f, .4f, .4f });


        builder.Setup(output =>
        {
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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, input.Actor.Unit.ExtraColor1 * 56 + input.Actor.Unit.ExtraColor2 * 8 + input.Actor.Unit.ExtraColor3));
            output.Sprite(input.Sprites.Crux[6 + input.Actor.Unit.HeadType]);
        }); // A coconut.
        builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
        {
            output.Coloring(ColorMap.GetEyeColor(input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Crux[30 + input.Actor.Unit.EyeType]);
        }); // The whole expression.

        builder.RenderSingle(SpriteType.SecondaryEyes, 18, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, input.Actor.Unit.ExtraColor1 * 56 + input.Actor.Unit.ExtraColor2 * 8 + input.Actor.Unit.ExtraColor3));
                if (input.Actor.Unit.BodySize <= 1)
                {
                    if (input.Actor.Unit.HasWeapon == false)
                    {
                        if (input.Actor.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Crux[401]);
                            return;
                        }

                        return;
                    }

                    switch (input.Actor.GetWeaponSprite())
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

                if (input.Actor.Unit.HasWeapon == false)
                {
                    if (input.Actor.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Crux[402]);
                        return;
                    }

                    return;
                }

                switch (input.Actor.GetWeaponSprite())
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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, input.Actor.Unit.ExtraColor1 * 56 + input.Actor.Unit.ExtraColor2 * 8 + input.Actor.Unit.ExtraColor3));
            if (input.Actor.IsOralVoring)
            {
                if (input.Actor.Unit.HeadType == 0 || input.Actor.Unit.HeadType == 2)
                {
                    output.Sprite(input.Sprites.Crux[10]);
                    return;
                }

                output.Sprite(input.Sprites.Crux[11]);
            }
        }); // Open mouth.

        builder.RenderSingle(SpriteType.Hair, 22, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, input.Actor.Unit.ExtraColor1 * 56 + input.Actor.Unit.ExtraColor2 * 8 + input.Actor.Unit.ExtraColor3));
            output.Sprite(input.Sprites.Crux[76 + input.Actor.Unit.HairStyle]);
        }); // Hair.

        builder.RenderSingle(SpriteType.Hair2, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, input.Actor.Unit.ExtraColor1 * 56 + input.Actor.Unit.ExtraColor2 * 8 + input.Actor.Unit.ExtraColor3));
            output.Sprite(input.Sprites.Crux[13 + input.Actor.Unit.EarType]);
        }); // Ears.

        builder.RenderSingle(SpriteType.Hair3, 0, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, input.Actor.Unit.ExtraColor1 * 56 + input.Actor.Unit.ExtraColor2 * 8 + input.Actor.Unit.ExtraColor3));
            output.Sprite(input.Sprites.Crux[60 + input.Actor.Unit.TailType]);
        }); // Tail.

        builder.RenderSingle(SpriteType.Beard, 15, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, input.Actor.Unit.ExtraColor1 * 56 + input.Actor.Unit.ExtraColor2 * 8 + input.Actor.Unit.ExtraColor3));
            if (input.Actor.Unit.FurType == 0)
            {
                return;
            }

            output.Sprite(input.Sprites.Crux[49 + input.Actor.Unit.FurType]);
        }); // Neck/Cheek fur.

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, input.Actor.Unit.ExtraColor1 * 56 + input.Actor.Unit.ExtraColor2 * 8 + input.Actor.Unit.ExtraColor3));
            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            output.Sprite(input.Sprites.Crux[input.Actor.Unit.BodySize]);
        }); // Main body.


        builder.RenderSingle(SpriteType.BodyAccent, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, input.Actor.Unit.ExtraColor1 * 56 + input.Actor.Unit.ExtraColor2 * 8 + input.Actor.Unit.ExtraColor3));
            if (input.Actor.Unit.BodySize <= 1)
            {
                if (input.Actor.Unit.HasWeapon == false)
                {
                    if (input.Actor.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Crux[351]);
                        return;
                    }

                    output.Sprite(input.Sprites.Crux[4]);
                    return;
                }

                switch (input.Actor.GetWeaponSprite())
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

            if (input.Actor.Unit.HasWeapon == false)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Crux[352]);
                    return;
                }

                output.Sprite(input.Sprites.Crux[5]);
                return;
            }

            switch (input.Actor.GetWeaponSprite())
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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, input.Actor.Unit.ExtraColor1 * 56 + input.Actor.Unit.ExtraColor2 * 8 + input.Actor.Unit.ExtraColor3));
            if (input.Actor.Unit.BodyAccentType2 == 0)
            {
                return;
            }

            output.Sprite(input.Sprites.Crux[271 + input.Actor.Unit.BodyAccentType2]);
        }); // Leg stripes.

        builder.RenderSingle(SpriteType.BodyAccent3, 10, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Crux, input.Actor.Unit.ExtraColor1 * 56 + input.Actor.Unit.ExtraColor2 * 8 + input.Actor.Unit.ExtraColor3));
            switch (input.Actor.Unit.BodyAccentType3)
            {
                case 0: return;
                case 1:
                {
                    if (input.Actor.Unit.HasWeapon == false || input.Actor.Surrendered)
                    {
                        output.Sprite(input.Sprites.Crux[278]);
                        return;
                    }

                    switch (input.Actor.GetWeaponSprite())
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
                    if (input.Actor.Unit.HasWeapon == false || input.Actor.Surrendered)
                    {
                        output.Sprite(input.Sprites.Crux[279]);
                        return;
                    }

                    switch (input.Actor.GetWeaponSprite())
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
                    if (input.Actor.Unit.HasWeapon == false || input.Actor.Surrendered)
                    {
                        output.Sprite(input.Sprites.Crux[280]);
                        return;
                    }

                    switch (input.Actor.GetWeaponSprite())
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
                    if (input.Actor.Unit.HasWeapon == false || input.Actor.Surrendered)
                    {
                        output.Sprite(input.Sprites.Crux[281]);
                        return;
                    }

                    switch (input.Actor.GetWeaponSprite())
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
            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Crux[12]);
            }
        }); // Teeth.

        builder.RenderSingle(SpriteType.BodyAccent5, 16, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (!input.Actor.Targetable)
            {
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsOralVoring)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                return;
            }

            if (input.Actor.AnimationController.frameLists[0].currentlyActive)
            {
                if (input.Actor.AnimationController.frameLists[0].currentTime >=
                    frameListDrool.times[input.Actor.AnimationController.frameLists[0].currentFrame])
                {
                    input.Actor.AnimationController.frameLists[0].currentFrame++;
                    input.Actor.AnimationController.frameLists[0].currentTime = 0f;

                    if (input.Actor.AnimationController.frameLists[0].currentFrame >= frameListDrool.frames.Length)
                    {
                        input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                        input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                        input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Crux[291 + frameListDrool.frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                return;
            }

            if (input.Actor.PredatorComponent?.VisibleFullness > 0 && State.Rand.Next(600) == 0)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = true;
            }
        }); // Drool animation.

        builder.RenderSingle(SpriteType.BodyAccessory, 23, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            Accessory acc = null;
            if (input.Actor.Unit.Items == null || input.Actor.Unit.Items.Length < 1)
            {
                return;
            }

            if (input.Actor.Unit.Items[0] is Accessory)
            {
                acc = (Accessory)input.Actor.Unit.Items[0];
            }

            if (acc == null)
            {
                return;
            }

            output.Layer(23);

            if (acc == State.World.ItemRepository.GetItem(ItemType.Helmet))
            {
                if (input.Actor.Unit.EarType >= 7)
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
                if (input.Actor.Unit.HasWeapon == false || input.Actor.Surrendered)
                {
                    output.Sprite(input.Sprites.Crux[376]);
                    return;
                }

                switch (input.Actor.GetWeaponSprite())
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
                if (input.Actor.Unit.HasWeapon == false || input.Actor.Surrendered)
                {
                    output.Sprite(input.Sprites.Crux[381]);
                    return;
                }

                switch (input.Actor.GetWeaponSprite())
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
                unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypes, CruxClothingTypes.RagsInstance);
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