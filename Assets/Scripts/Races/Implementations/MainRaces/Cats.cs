using System.Collections.Generic;

namespace Races.Graphics.Implementations.MainRaces
{
    internal static class Cats
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            builder.RandomCustom((data, output) =>   
            {
                IUnitRead unit = data.Unit;
                Defaults.Randomize(data, output);
                if (unit.Type == UnitType.Leader)
                {
                    unit.ClothingType = 1 + data.SetupOutput.AllowedMainClothingTypes.IndexOf(RaceSpecificClothing.CatLeaderInstance);
                }
            });

            builder.Setup((input, output) =>
            {
                output.Names("Cat", "Cats");
                output.WallType(WallType.Cat);
                output.FlavorText(new FlavorText(
                    new Texts { "whiskered", "hissing", "bristle tailed" },
                    new Texts { "purring", "sharp-toothed", "whiskered" },
                    new Texts { "cat", "whiskered", { "queen", Gender.Female }, { "tom", Gender.Male } }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 10,
                    StomachSize = 15,
                    HasTail = true,
                    FavoredStat = Stat.Agility,
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Pounce,
                        TraitType.EscapeArtist
                    },
                    RaceDescription = "Natives to the realm, the Cats are skilled at pouncing on their enemy with a sudden burst of speed. Many a wounded warrior has found themselves devoured by a feline jumping over a wall of their fellow warriors, while the Cat's allies defy their enemies by somehow squirming out of their stomach's.",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.HairColor, "Hair Color: " + UnitCustomizer.HairColorLookup(unit.HairColor));
                    buttons.SetText(ButtonType.BodyAccessoryColor, "Fur Color: " + UnitCustomizer.HairColorLookup(unit.AccessoryColor));
                });
                output.TownNames(new List<string>
                {
                    "Pyramid of Indulgence",
                    "Catro",
                    "Meowixandria",
                    "Feliyum",
                    "Al Bastet",
                    "Catazig",
                    "Pursia",
                    "Palace of Decedance",
                    "Yarnodos",
                    "Meopolis",
                    "Catolomeic Palace",
                    "Catopolis",
                    "Catville",
                    "Nekotown",
                    "Meowscow",
                    "Caturdayton",
                });
                output.FurCapable = true;
                output.BaseBody = true;
                output.AllowedMainClothingTypes.Add(RaceSpecificClothing.CatLeaderInstance);
                output.AvoidedMainClothingTypes++;
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
                output.Sprite(input.Sprites.Bodies[8]);
            });
            builder.RenderSingle(SpriteType.SecondaryAccessory, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Fur, input.U.AccessoryColor));
                output.Sprite(input.Sprites.BodyParts[0]);
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
        });
    }
}