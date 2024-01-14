function SpottedBelly(actor)
    if (actor.Unit.BodyAccentType5 == 1) then
        return GetPalette(SwapType.HorseSkin, actor.Unit.AccessoryColor);
    end

    return GetPalette(SwapType.HorseSkin, actor.Unit.SkinColor);
end


builder.Names("Equine", "Equines");

builder.SetRaceTraits(function (traits)
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
 
builder.FlavorText(newFlavorText(
    newTextsBasic(),
    newTextsBasic(),
    newTextsBasic().Add("equine").Add("bronco").Add("mare", Gender.Female).Add("stallion", Gender.Male)
));

builder.CustomizeButtons(function (unit, buttons)
    buttons.SetText(ButtonType.ClothingExtraType1, "Overtop");
    buttons.SetText(ButtonType.ClothingExtraType2, "Overbottom");
    buttons.SetText(ButtonType.BodyAccentTypes3,  "Skin Pattern");
    buttons.SetText(ButtonType.BodyAccentTypes4, "Head Pattern");
    buttons.SetText(ButtonType.BodyAccentTypes5, "Torso Color");
end);

builder.TownNames({
    "Cataphracta",
    "Equus",
    "The Ranch",
    "Haciendo",
    "Alfarsan"
});

builder.Setup(function (output)
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
            HorseClothing.HorseUndertop1Instance,
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
end);




builder.RandomCustom(function (data)
    Defaults.RandomCustom(data);

    data.Unit.BodyAccentType3 = RandomInt(data.MiscRaceData.BodyAccentTypes3);
    data.Unit.BodyAccentType4 = RandomInt(data.MiscRaceData.BodyAccentTypes4);
    data.Unit.BodyAccentType5 = RandomInt(data.MiscRaceData.BodyAccentTypes5);

    data.Unit.HairStyle = RandomInt(data.MiscRaceData.HairStyles);
    data.Unit.TailType = RandomInt(data.MiscRaceData.TailTypes);
end);


builder.RunBefore(function (input, output)
    CommonRaceCode.MakeBreastOversize2(input, output, 29 * 29);
    Defaults.BasicBellyRunAfter(input, output);
end);

builder.RenderSingle(SpriteType.Head, 5, function (input, output)
    output.Coloring(SwapType.HorseSkin, input.U.SkinColor);
    local state = ternary(input.A.IsAttacking or input.A.IsEating, "eat", "still");
    output.Sprite("head", input.Sex, state);
end); --head

builder.RenderSingle(SpriteType.Eyes, 6, function (input, output) 
    output.Coloring(SwapType.EyeColor, input.U.EyeColor);
    if (input.U.IsDead and input.U.Items ~= null) then
        output.Sprite("eyes", input.Sex, "dead");
    else
        output.Sprite0("eyes", input.Sex, input.U.EyeType);
    end
end); --eyes;


builder.RenderSingle(SpriteType.Hair, 21, function (input, output)
    output.Coloring(SwapType.UniversalHair, input.U.HairColor);
    output.Sprite0("hair_front", input.U.HairStyle);
end); -- forward hair;

builder.RenderSingle(SpriteType.Hair2, 1, function (input, output)
    output.Coloring(SwapType.UniversalHair, input.U.HairColor);
    output.Sprite0("hair_back", input.U.HairStyle);
end); -- back hair

builder.RenderSingle(SpriteType.Body, 4, function (input, output)
    output.Coloring(GetPalette(SwapType.HorseSkin, input.U.SkinColor));
    local name = ternary(input.U.HasBreasts, "body_female", "body_male");
    local index = ternary(input.A.IsAttacking, 2, ternary(input.U.HasWeapon, 1, 0));

    output.Sprite0(name, index);
end); -- body


builder.RenderSingle(SpriteType.BodyAccent3, 5, function (input, output)
    output.Coloring(GetPalette(SwapType.HorseSkin, input.U.AccessoryColor));
    if input.U.BodyAccentType3 ~= 0 then
        local state = ternary(input.A.IsAttacking, "attack", ternary(input.U.HasWeapon, "holdweapon", "stand"));
        output.Sprite0("skin_pattern", state, input.Sex, input.U.BodyAccentType3 - 1);
    end
end); --limb spots

builder.RenderSingle(SpriteType.BodyAccent4, 6, function (input, output)
    if input.U.BodyAccentType4 ~= 0 then
        output.Coloring(GetPalette(SwapType.HorseSkin, input.U.AccessoryColor));
        local state = ternary(input.A.IsAttacking or input.A.IsEating, "eat", "still");
        output.Sprite0("head_pattern", input.Sex, state, input.U.BodyAccentType4 - 1);
    end
end); --head spots

builder.RenderSingle(SpriteType.BodyAccent5, 5, function (input, output)
    output.Coloring(SwapType.HorseSkin, input.U.AccessoryColor);
    output.Sprite("torso_pattern", input.Sex);
end); -- belly spots, also color breasts/belly/dick


builder.RenderSingle(SpriteType.BodyAccent8, 6, function (input, output)
    output.Coloring(SwapType.HorseSkin, ternary(input.U.BodyAccentType3 >= 2, input.U.AccessoryColor, input.U.SkinColor));
    output.Sprite("leg_tuft");
end); -- leg tuft

builder.RenderSingle(SpriteType.BodyAccent10, 5, function (input, output) 
    output.Sprite("hooves", input.Sex);
end); -- leg hoof;


builder.RenderSingle(SpriteType.BodyAccessory, 2, function (input, output)
    output.Coloring(SwapType.UniversalHair, input.U.HairColor);
    output.Sprite0("tail_1", input.U.TailType);
end); -- tail


builder.RenderSingle(SpriteType.BodyAccent9, 3, function (input, output)
    if (input.U.BodyAccentType3 == 5) then 
        output.Coloring(SwapType.HorseSkin, input.U.SkinColor);
    else
        output.Coloring(SwapType.Clothing50Spaced, input.U.ClothingColor);
    end

    output.Sprite0("tail_2", input.U.TailType, true);
end); -- tail bit

builder.RenderSingle(SpriteType.Breasts, 19, function (input, output)
    if not input.U.HasBreasts then
        return;
    end

    output.Coloring(SpottedBelly(input.Actor));
    if (input.A.PredatorComponent.LeftBreastFullness > 0) then
        local leftSize = math.sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(29 * 29));

        if (leftSize > 26) then leftSize = 26; end

        output.Sprite0("breast_left", leftSize);
    else
        if (input.U.DefaultBreastSize == 0) then
            output.Sprite0("breast_left", 0);
            return;
        end
        
        output.Sprite0("breast_left", input.U.BreastSize);
    end
end);


builder.RenderSingle(SpriteType.SecondaryBreasts, 19, function (input, output)
    if (input.U.HasBreasts == false) then
        return;
    end

    output.Coloring(SpottedBelly(input.Actor));
    if (input.A.PredatorComponent.RightBreastFullness > 0) then
        local rightSize = math.sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(29 * 29));

        if (rightSize > 26) then rightSize = 26; end
        
        output.Sprite0("breast_right", rightSize);
    else
        if (input.U.DefaultBreastSize == 0) then
            output.Sprite0("breast_right", 0);
            return;
        end

        output.Sprite0("breast_right", input.U.BreastSize);
    end
end);

builder.RenderSingle(SpriteType.Belly, 17, function (input, output)
    if (input.A.HasBelly) then
        output.Coloring(SpottedBelly(input.Actor));
        local size = input.A.GetStomachSize(29, 1.2);
        local combined = math.min(size, 26);
        output.Sprite0("belly", combined, true);
    end
end); -- belly

builder.RenderSingle(SpriteType.Dick, 14, function (input, output)
    output.Coloring(SpottedBelly(input.Actor));
    if (input.U.HasDick) then

        local breastsNotTooBig = input.A.PredatorComponent and input.A.PredatorComponent.VisibleFullness < 0.26 and
                                math.sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(29 * 29)) < 16 and
                                math.sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(29 * 29)) < 16;
    
        output.Layer(ternary(breastsNotTooBig, 24, 14));
    
        local useErectSprite = input.A.IsErect() or input.A.IsCockVoring;

        if (not useErectSprite and Config.FurryGenitals) then
            output.Sprite(("penis_furry"));
        else
            if (not breastsNotTooBig and useErectSprite) then
                output.Sprite0("penis_erect_down", input.U.DickSize);
            else
                output.Sprite0(ternary(useErectSprite, "penis_erect_up", "penis_flaccid"), input.U.DickSize);
            end
        end
    end
end); -- cocc


builder.RenderSingle(SpriteType.Balls, 13, function (input, output)
    output.Coloring(SpottedBelly(input.A));
    if (input.U.HasDick == false) then
        return;
    end

    local size = input.A.GetBallSize(29, 0.8);
    local baseSize = (input.U.DickSize + 1) / 3;
    local combinedSize = math.min(baseSize + size + 2, 26);

    output.Sprite0("balls", combinedSize);
end); -- balls        

builder.RenderSingle(SpriteType.Weapon, 12, function (input, output)
    if (input.U.HasWeapon and input.A.Surrendered == false) then
        output.Coloring(Defaults.WhiteColored);
        output.Sprite(input.SimpleWeaponSpriteFrontV1, true);
    end
end);

builder.RenderSingle(SpriteType.SecondaryAccessory, 3, function (input, output)
    if (input.U.HasWeapon and input.A.Surrendered == false) then
        output.Coloring(Defaults.WhiteColored);
        output.Sprite(input.SimpleWeaponSpriteBackV1, true);
    end
end); -- bow bit





