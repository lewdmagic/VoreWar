﻿using System.Collections.Generic;

namespace Races.Graphics.Implementations.MainRaces
{
    internal static class Wolves
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            builder.Setup((input, output) =>
            {
                output.Names("Wolf", "Wolves");
                output.FlavorText(new FlavorText(
                    new Texts { "wild", "growling", "wet furred" },
                    new Texts { "spirited", "panting", "long furred" },
                    new Texts { "feral", "canine", { "wolfess", Gender.Female }, { "wolf", Gender.Male } }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 10,
                    StomachSize = 15,
                    HasTail = true,
                    FavoredStat = Stat.Strength,
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.PackStrength,
                        TraitType.PackVoracity
                    },
                    RaceDescription = "Natives of this realm, the Wolves have a history of hunting in packs extending beyond the crafting of their first weapons. While a lone Wolf can still be a worthy adversary, their true strength comes from working with their kin.",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.HairColor, "Hair Color: " + UnitCustomizer.HairColorLookup(unit.HairColor));
                    buttons.SetText(ButtonType.BodyAccessoryColor, "Fur Color: " + UnitCustomizer.HairColorLookup(unit.AccessoryColor));
                });
                output.TownNames(new List<string>
                {
                    "Pax Lupus",
                    "Fort Fang",
                    "Tribe of the Moon",
                    "Camp Claw",
                    "Wolfenburg",
                    "Tooth Tribe",
                    "Pelt Keep",
                    "Lunar Palace",
                    "The Howling Castle",
                    "The Howling Tribe",
                    "Blood Claw Tribe",
                    "The Gurgling Steppe",
                    "House of the Pack",
                    "Gnashing Hill",
                    "Wailing Gut Tribe",
                    "Famine's End",
                });
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
            builder.RenderSingle(SpriteType.BodyAccessory, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Fur, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Bodies[12]);
            });
            builder.RenderSingle(SpriteType.SecondaryAccessory, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Fur, input.U.AccessoryColor));
                output.Sprite(input.Sprites.BodyParts[3]);
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
            builder.RandomCustom(Defaults.Randomize);
        });
    }
}