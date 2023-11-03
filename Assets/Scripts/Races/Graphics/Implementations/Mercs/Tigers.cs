using System.Collections.Generic;

internal static class Tigers
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Default, builder =>
    {
        builder.Names("Tiger", "Tigers");
        builder.FlavorText(new FlavorText(
            new Texts { "striped", "roaring", "mewling" },
            new Texts { "striped", "roaring", "sharp toothed" },
            new Texts { "", "", {"", Gender.Female}, {"", Gender.Male} }
        ));
        builder.RaceTraits(new RaceTraits()
        {
            BodySize = 10,
            StomachSize = 18,
            HasTail = true,
            FavoredStat = Stat.Strength,
            PowerAdjustment = 1.3f,
            RacialTraits = new List<Traits>()
            {
                Traits.Maul,
                Traits.Frenzy
            },
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(16, 24),
                Dexterity = new RaceStats.StatRange(10, 18),
                Endurance = new RaceStats.StatRange(12, 22),
                Mind = new RaceStats.StatRange(12, 20),
                Will = new RaceStats.StatRange(10, 22),
                Agility = new RaceStats.StatRange(8, 18),
                Voracity = new RaceStats.StatRange(12, 20),
                Stomach = new RaceStats.StatRange(12, 18),
            },
            RaceDescription = "Somewhat enigmatic, it is uncertain if the Tigers are native to this realm or came from elsewhere. They do not seem interested in settling down though, joining armies to test their considerable skills in battle instead.",
        });
        builder.CustomizeButtons((unit, buttons) =>
        {
            buttons.SetText(ButtonType.HairColor, "Hair Color: " + UnitCustomizer.HairColorLookup(unit.HairColor));
            buttons.SetText(ButtonType.BodyAccessoryColor, "Fur Color: " + UnitCustomizer.HairColorLookup(unit.AccessoryColor));
        });
        builder.Setup(output =>
        {
            output.FurCapable = true;
            output.BaseBody = true;

            output.AllowedMainClothingTypes.Insert(0, RaceSpecificClothing.TigerSpecialInstance);
        });


        builder.RenderSingle(SpriteType.Head, Defaults.SpriteGens3[SpriteType.Head]);
        builder.RenderSingle(SpriteType.Eyes, Defaults.SpriteGens3[SpriteType.Eyes]);
        builder.RenderSingle(SpriteType.Mouth, Defaults.SpriteGens3[SpriteType.Mouth]);
        builder.RenderSingle(SpriteType.Hair, Defaults.SpriteGens3[SpriteType.Hair]);
        builder.RenderSingle(SpriteType.Hair2, Defaults.SpriteGens3[SpriteType.Hair2]);
        builder.RenderSingle(SpriteType.Body, Defaults.SpriteGens3[SpriteType.Body]);
        builder.RenderSingle(SpriteType.BodyAccent, Defaults.SpriteGens3[SpriteType.BodyAccent]);
        builder.RenderSingle(SpriteType.BodyAccent2, Defaults.SpriteGens3[SpriteType.BodyAccent2]);
        builder.RenderSingle(SpriteType.BodyAccent3, Defaults.SpriteGens3[SpriteType.BodyAccent3]);
        builder.RenderSingle(SpriteType.BodyAccent4, Defaults.SpriteGens3[SpriteType.BodyAccent4]);
        builder.RenderSingle(SpriteType.BodyAccent5, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.Fur, input.U.AccessoryColor));
            int thinOffset = input.U.BodySize < 2 ? 8 : 0;
            output.Sprite(Config.FurryHandsAndFeet || input.U.Furry ? input.Sprites.FurryHandsAndFeet[6 + thinOffset + (input.A.IsAttacking ? 1 : 0)] : null);
        });

        builder.RenderSingle(SpriteType.BodyAccessory, 5, (input, output ) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.Fur, input.U.AccessoryColor));
            output.Sprite(input.Sprites.Bodies[14]);
        });
        builder.RenderSingle(SpriteType.SecondaryAccessory, 1, (input, output ) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.Fur, input.U.AccessoryColor));
            output.Sprite(input.Sprites.BodyParts[4]);
        });
        builder.RenderSingle(SpriteType.BodySize, Defaults.SpriteGens3[SpriteType.BodySize]);
        builder.RenderSingle(SpriteType.Breasts, Defaults.SpriteGens3[SpriteType.Breasts]);
        builder.RenderSingle(SpriteType.Belly, Defaults.SpriteGens3[SpriteType.Belly]);
        builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
        {
            output.Coloring(Defaults.FurryColor(input.Actor));
            if (input.U.HasDick == false)
            {
                return;
            }

            if (input.U.Furry && Config.FurryGenitals)
            {
                if (input.A.IsErect() == false)
                {
                    return;
                }

                int type = 0;
                type = input.A.IsCockVoring ? 5 : 2;

                output.Coloring(Defaults.WhiteColored);
                if (input.A.PredatorComponent?.VisibleFullness < .75f)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[24 + type]).Layer(18);
                    return;
                }

                output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[30 + type]).Layer(12);
            }
        });
        builder.RenderSingle(SpriteType.Balls, Defaults.SpriteGens3[SpriteType.Balls]);
        builder.RenderSingle(SpriteType.Weapon, Defaults.SpriteGens3[SpriteType.Weapon]);
        builder.RenderSingle(SpriteType.BackWeapon, Defaults.SpriteGens3[SpriteType.BackWeapon]);

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            if (unit.ClothingType != 0)
            {
                unit.ClothingType = 1;
            }
        });
    });
}