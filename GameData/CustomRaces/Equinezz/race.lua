--- Core function: called once to set up the race.
function setup(output)
    output.Names("Equinezz", "Equinezzs");

    output.SetRaceTraits(function (traits)
        traits.BodySize = 10;
        traits.StomachSize = 16;
        traits.HasTail = true;
        traits.FavoredStat = Stat.Agility;
        traits.RacialTraits = {
            Trait.Charge,
            Trait.StrongMelee
        };
        traits.RaceDescription = "";
    end);

    output.FlavorText(newFlavorText(
            newTextsBasic(),
            newTextsBasic(),
            newTextsBasic().Add("equine").Add("bronco").Add("mare", Gender.Female).Add("stallion", Gender.Male)
    ));

    output.CustomizeButtons(function (unit, buttons)
        buttons.SetText(ButtonType.ClothingExtraType1, "Overtop");
        buttons.SetText(ButtonType.ClothingExtraType2, "Overbottom");
        buttons.SetText(ButtonType.BodyAccentTypes3,  "Skin Pattern");
        buttons.SetText(ButtonType.BodyAccentTypes4, "Head Pattern");
        buttons.SetText(ButtonType.BodyAccentTypes5, "Torso Color");
    end);

    output.TownNames({
        "Cataphracta",
        "Equus",
        "The Ranch",
        "Haciendo",
        "Alfarsan"
    });
    
    output.DickSizes = function () return 3 end;
    output.BreastSizes = function () return 7 end;

    output.SpecialAccessoryCount = 0;
    output.ClothingShift = newVector3(0, 0, 0);
    output.AvoidedEyeTypes = 0;
    output.AvoidedMouthTypes = 0;

    output.HairColors = GetPaletteCount(SwapType.UniversalHair);
    output.HairStyles = 15;
    output.SkinColors = GetPaletteCount(SwapType.HorseSkin);
    output.AccessoryColors = GetPaletteCount(SwapType.HorseSkin);
    output.EyeTypes = 4;
    output.EyeColors = GetPaletteCount(SwapType.EyeColor);
    output.SecondaryEyeColors = 1;
    output.BodySizes = 0;

    output.AllowedMainClothingTypes.Clear();
    output.AllowedWaistTypes.Clear();
    output.AllowedClothingHatTypes.Clear();
    output.MouthTypes = 0;
    output.AvoidedMainClothingTypes = 0;
    output.TailTypes = 6;
    output.BodyAccentTypes3 = 5;
    output.BodyAccentTypes4 = 5;
    output.BodyAccentTypes5 = 2;

    output.ClothingColors = GetPaletteCount(SwapType.Clothing50Spaced);
    output.ExtendedBreastSprites = true;

    output.AllowedMainClothingTypes.Set( -- undertops
            --HorseClothing.HorseUndertop1Instance,
            --HorseClothing.HorseUndertop1Instance2,
            GetClothing("horsetop1"),
            HorseClothing.HorseUndertop2Instance,
            HorseClothing.HorseUndertop3Instance,
            HorseClothing.HorseUndertop4Instance,
            HorseClothing.HorseUndertopM1Instance,
            HorseClothing.HorseUndertopM2Instance,
            HorseClothing.HorseUndertopM3Instance
    );

    output.AllowedWaistTypes.Set( -- underbottoms
            HorseClothing.HorseUBottom1,
            HorseClothing.HorseUBottom2,
            HorseClothing.HorseUBottom3,
            HorseClothing.HorseUBottom4,
            HorseClothing.HorseUBottom5
    );

    output.ExtraMainClothing1Types.Set( -- Overtops
            HorseClothing.HorsePonchoInstance,
            HorseClothing.HorseNecklaceInstance
    );

    output.ExtraMainClothing2Types.Set( -- Overbottoms
            HorseClothing.HorseOBottom1Instance,
            HorseClothing.HorseOBottom2Instance,
            HorseClothing.HorseOBottom3Instance
    );

    output.WholeBodyOffset = newVector2(0, 16 * 0.625);
end

--- Core function: called each frame to render the unit. 
function render(input, output)

    local headSprite = output.NewSprite(SpriteType.Head, 5);
    headSprite.Coloring(SwapType.HorseSkin, input.U.SkinColor);
    headSprite.Sprite("head", input.Sex, ternary(input.A.IsAttacking or input.A.IsEating, "eat", "still"));

    local eyesSprite = output.NewSprite(SpriteType.Eyes, 6);
    eyesSprite.Coloring(SwapType.EyeColor, input.U.EyeColor);
    if (input.U.IsDead and input.U.Items ~= null) then
        eyesSprite.Sprite("eyes", input.Sex, "dead");
    else
        eyesSprite.Sprite0("eyes", input.Sex, input.U.EyeType);
    end


    output.NewSprite(SpriteType.Hair, 21)
          .Coloring(SwapType.UniversalHair, input.U.HairColor)
          .Sprite0("hair_front", input.U.HairStyle);

    output.NewSprite(SpriteType.Hair2, 1)
          .Coloring(SwapType.UniversalHair, input.U.HairColor)
          .Sprite0("hair_back", input.U.HairStyle);

    local bodyName = ternary(input.U.HasBreasts, "body_female", "body_male");
    local bodyIndex = ternary(input.A.IsAttacking, 2, ternary(input.U.HasWeapon, 1, 0));
    output.NewSprite(SpriteType.Body, 4)
          .Coloring(GetPalette(SwapType.HorseSkin, input.U.SkinColor))
          .Sprite0(bodyName, bodyIndex);


    if input.U.BodyAccentType3 ~= 0 then
        local state = ternary(input.A.IsAttacking, "attack", ternary(input.U.HasWeapon, "holdweapon", "stand"));
        output.NewSprite(SpriteType.BodyAccent3, 5)
              .Coloring(GetPalette(SwapType.HorseSkin, input.U.AccessoryColor))
              .Sprite0("skin_pattern", state, input.Sex, input.U.BodyAccentType3 - 1);
    end

    if input.U.BodyAccentType4 ~= 0 then
        local state = ternary(input.A.IsAttacking or input.A.IsEating, "eat", "still");
        output.NewSprite(SpriteType.BodyAccent4, 6)
              .Coloring(GetPalette(SwapType.HorseSkin, input.U.AccessoryColor))
              .Sprite0("head_pattern", input.Sex, state, input.U.BodyAccentType4 - 1);
    end


    output.NewSprite(SpriteType.BodyAccent5, 5)
          .Coloring(SwapType.HorseSkin, input.U.AccessoryColor)
          .Sprite("torso_pattern", input.Sex);


    output.NewSprite(SpriteType.BodyAccent8, 6)
          .Coloring(SwapType.HorseSkin, ternary(input.U.BodyAccentType3 >= 2, input.U.AccessoryColor, input.U.SkinColor))
          .Sprite("leg_tuft");

    output.NewSprite(SpriteType.BodyAccent10, 5)
          .Sprite("hooves", input.Sex);
    
    output.NewSprite(SpriteType.BodyAccessory, 2)
          .Coloring(SwapType.UniversalHair, input.U.HairColor)
          .Sprite0("tail_1", input.U.TailType);


    local tainBitSprite2 = output.NewSprite(SpriteType.BodyAccent9, 3);
    tainBitSprite2.Sprite0("tail_2", input.U.TailType, true);
    if (input.U.BodyAccentType3 == 5) then
        tainBitSprite2.Coloring(SwapType.HorseSkin, input.U.SkinColor);
    else
        tainBitSprite2.Coloring(SwapType.Clothing50Spaced, input.U.ClothingColor);
    end

    if input.U.HasBreasts then
        local breastLeftSprite = output.NewSprite(SpriteType.Breasts, 19);
        breastLeftSprite.Coloring(SpottedBelly(input.Actor));
        if (input.A.PredatorComponent.LeftBreastFullness > 0) then
            local leftSize = math.sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(29 * 29));

            if (leftSize > 26) then leftSize = 26; end

            breastLeftSprite.Sprite0("breast_left", leftSize);
        else
            if (input.U.DefaultBreastSize == 0) then
                breastLeftSprite.Sprite0("breast_left", 0);
            else
                breastLeftSprite.Sprite0("breast_left", input.U.BreastSize);
            end
        end
    end

    if (input.U.HasBreasts) then
        local rightLeftSprite = output.NewSprite(SpriteType.SecondaryBreasts, 19);
        rightLeftSprite.Coloring(SpottedBelly(input.Actor));
        if (input.A.PredatorComponent.RightBreastFullness > 0) then
            local rightSize = math.sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(29 * 29));

            if (rightSize > 26) then rightSize = 26; end

            rightLeftSprite.Sprite0("breast_right", rightSize);
        else
            if (input.U.DefaultBreastSize == 0) then
                rightLeftSprite.Sprite0("breast_right", 0);
            else
                rightLeftSprite.Sprite0("breast_right", input.U.BreastSize);
            end
        end
    end

    if (input.A.HasBelly) then
        local bellySprite = output.NewSprite(SpriteType.Belly, 17);
        bellySprite.Coloring(SpottedBelly(input.Actor));
        local size = input.A.GetStomachSize(29, 1.2);
        local combined = math.min(size, 26);
        bellySprite.Sprite0("belly", combined, true);
    end


    if (input.U.HasDick) then

        -- Dick
        local dickSprite = output.NewSprite(SpriteType.Dick, 14);
        dickSprite.Coloring(SpottedBelly(input.Actor));
        local breastsNotTooBig = input.A.PredatorComponent and input.A.PredatorComponent.VisibleFullness < 0.26 and
                math.sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(29 * 29)) < 16 and
                math.sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(29 * 29)) < 16;

        dickSprite.Layer(ternary(breastsNotTooBig, 24, 14));

        local useErectSprite = input.A.IsErect() or input.A.IsCockVoring;

        if (not useErectSprite and Config.FurryGenitals) then
            dickSprite.Sprite(("penis_furry"));
        else
            if (not breastsNotTooBig and useErectSprite) then
                dickSprite.Sprite0("penis_erect_down", input.U.DickSize);
            else
                dickSprite.Sprite0(ternary(useErectSprite, "penis_erect_up", "penis_flaccid"), input.U.DickSize);
            end
        end
        
        -- Balls
        local ballsSprite = output.NewSprite(SpriteType.Balls, 13);
        ballsSprite.Coloring(SpottedBelly(input.A));
        local size = input.A.GetBallSize(29, 0.8);
        local baseSize = (input.U.DickSize + 1) / 3;
        local combinedSize = math.min(baseSize + size + 2, 26);

        ballsSprite.Sprite0("balls", combinedSize);
    end

    if (input.U.HasWeapon and input.A.Surrendered == false) then
        output.NewSprite(SpriteType.Weapon, 12)
              .Coloring(Defaults.WhiteColored)
              .Sprite(input.SimpleWeaponSpriteFrontV1, true);

        -- bow bit 
        output.NewSprite(SpriteType.SecondaryAccessory, 3)
              .Coloring(Defaults.WhiteColored)
              .Sprite(input.SimpleWeaponSpriteBackV1, true);
    end
end

--- Core function: called to randomize a unit of this race.
function randomCustom(data)
    Defaults.RandomCustom(data);

    data.Unit.BodyAccentType3 = RandomInt(data.MiscRaceData.BodyAccentTypes3);
    data.Unit.BodyAccentType4 = RandomInt(data.MiscRaceData.BodyAccentTypes4);
    data.Unit.BodyAccentType5 = RandomInt(data.MiscRaceData.BodyAccentTypes5);

    data.Unit.HairStyle = RandomInt(data.MiscRaceData.HairStyles);
    data.Unit.TailType = RandomInt(data.MiscRaceData.TailTypes);
end

function SpottedBelly(actor)
    if (actor.Unit.BodyAccentType5 == 1) then
        return GetPalette(SwapType.HorseSkin, actor.Unit.AccessoryColor);
    end

    return GetPalette(SwapType.HorseSkin, actor.Unit.SkinColor);
end
