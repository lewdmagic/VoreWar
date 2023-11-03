using System.Collections.Generic;

internal static class Foxes
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Default, builder =>
    {        
        builder.Names("Fox", "Foxes");
        builder.WallType(WallType.Fox);
        builder.FlavorText(new FlavorText(
            new Texts { "fluffy tailed", "squirming", "whimpering" },
            new Texts { "cunning", "grinning", "sly" },
            new Texts { "fox", "vulpine", "canid", {"vixen", Gender.Female}, {"tod", Gender.Male} }
        ));
        builder.RaceTraits(new RaceTraits()
        {
            BodySize = 10,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Mind,
            RacialTraits = new List<Traits>()
            {
                Traits.ArtfulDodge,
                Traits.ThrillSeeker
            },
            LeaderRace = Race.Youko,
            RaceDescription = "Natives of this realm, the Foxes seem unable of taking danger seriously. They dodge attacks at the last second and only seem to grow ever bolder as death approaches them. Entire armies have fallen exhausted as a group of foxes dances among them, ready to be devoured once the time is right.",
        });
        builder.CustomizeButtons((unit, buttons) =>
        {
            buttons.SetText(ButtonType.HairColor, "Hair Color: " + UnitCustomizer.HairColorLookup(unit.HairColor));
            buttons.SetText(ButtonType.BodyAccessoryColor, "Fur Color: " + UnitCustomizer.HairColorLookup(unit.AccessoryColor));
        });
        builder.TownNames(new List<string>
        {
            "Vulpeska",
            "Trickster's Den",
            "Foxsaw",
            "Den of Cunning",
            "Fennecow",
            "Trickstadov",
            "Trapper's Den",
            "Caniska",
            "Yelpitz",
            "Valley of Deceit",
            "Preyland",
            "Den of the Ruthless",
            "Den of Gnarling",
        });
        builder.Setup(output =>
        {
            output.FurCapable = true;
            output.BaseBody = true;
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
        builder.RenderSingle(SpriteType.BodyAccessory, 5, (input, output ) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.Fur, input.U.AccessoryColor));
            output.Sprite(input.Sprites.Bodies[11]);
        });
        builder.RenderSingle(SpriteType.SecondaryAccessory, 1, (input, output ) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.Fur, input.U.AccessoryColor));
            output.Sprite(input.Sprites.BodyParts[2]);
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
                type = input.A.IsCockVoring ? 5 : 1;

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
        builder.RandomCustom(Defaults.RandomCustom);
    });
}