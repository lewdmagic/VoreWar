API_VERSION = "0.0.1"

--- Core function: called once to set up the race.
---@param output ISetupOutput
function Setup(output)
    output.Names("Bella", "Bellas");

    --output.FixedGender = true;
    output.CanBeGender = { Gender.Female };
    output.SkinColors = 1;
    output.EyeTypes = 1;
    output.GentleAnimation = false;
    output.BodySizes = 2;
    output.BodyAccentTypes2 = 2;
    output.ClothingColors = 0;

    output.SetRaceTraits(function (traits)
        traits.BodySize = 20;
        traits.StomachSize = 15;
        traits.FavoredStat = Stat.Endurance;
        traits.AllowedVoreTypes = {
            VoreType.Oral, VoreType.Unbirth, VoreType.Anal
        };

        traits.ExpMultiplier = 1.2;
        traits.PowerAdjustment = 1.2;

        traits.RaceStats.Strength = NewStatRange(6, 10);
        traits.RaceStats.Dexterity = NewStatRange(10, 15);
        traits.RaceStats.Endurance = NewStatRange(25, 30);
        traits.RaceStats.Mind = NewStatRange(15, 20);
        traits.RaceStats.Will = NewStatRange(20, 25);
        traits.RaceStats.Agility = NewStatRange(10, 15);
        traits.RaceStats.Voracity = NewStatRange(20, 25);
        traits.RaceStats.Stomach = NewStatRange(15, 20);

        traits.RacialTraits = {
            TraitType.Tenacious,
            TraitType.Resilient,
            TraitType.EfficientGuts,
            TraitType.SteadyStomach,
            TraitType.ThrillSeeker,
            TraitType.ArcaneMagistrate,
            TraitType.ManaRich,
            TraitType.SpellBlade,
            TraitType.Clumsy
        };

        traits.InnateSpells = { SpellType.Mending, SpellType.Fireball };
        traits.RaceDescription = "\"A shy cowgirl ^o^\" - Made by AgentAmbi";
    end);

    output.SetFlavorText(FlavorType.RaceSingleDescription,
        NewFlavorEntry("equine"),
        NewFlavorEntry("bronco"),
        NewFlavorEntryGendered("mare", Gender.Female),
        NewFlavorEntryGendered("stallion", Gender.Male)
    );

    output.SetFlavorText(FlavorType.WeaponMelee1, NewFlavorEntry("Push Dagger"));
    output.SetFlavorText(FlavorType.WeaponMelee2, NewFlavorEntry("Claw Katar"));
    output.SetFlavorText(FlavorType.WeaponRanged1, NewFlavorEntry("Iron Throwing Knife"));
    output.SetFlavorText(FlavorType.WeaponRanged2, NewFlavorEntry("Steel Throwing Knife"));

    output.TownNames({
        "Bella Capital",
        "Bella Town",
        "Bella Village"
    });

    output.AllowedMainClothingTypes.AddRange({-- only clothing
        MakeClothing("robe")
    });
end

--- Core function: called each frame to render the unit. 
---@param input IRaceRenderInput
---@param output IRaceRenderAllOutput
function Render(input, output)
    local stomachSize = input.A.GetStomachSize(4, 1)
    local bodySize = tostring(input.U.BodySize + 1);
    output.NewSprite(SpriteType.Body, 2)
          .Sprite0("body", bodySize, stomachSize);

    local armState = ternary(input.A.IsAttacking, "attack", "idle");
    output.NewSprite(SpriteType.Weapon, 1)
          .Sprite("arm", armState, bodySize);

    local headSprite = output.NewSprite(SpriteType.Head, 4);
    if (input.A.IsOralVoring and input.A.HasBelly) then
        headSprite.Sprite("head_swallow");
    elseif input.A.IsOralVoring then
        headSprite.Sprite("head_swallow_fail");
    else
        headSprite.Sprite("head_idle");
    end
end

--- Core function: called to randomize a unit of this race.
---@param input IRandomCustomInput
function RandomCustom(input)
    Defaults.RandomCustom(input);
    input.Unit.Name = "Bella";
end