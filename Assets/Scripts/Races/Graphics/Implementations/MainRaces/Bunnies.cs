using System.Collections.Generic;

namespace Races.Graphics.Implementations.MainRaces
{
    internal static class Bunnies
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
        
            builder.Setup((output) =>
            {
            
                output.Names("Bunny", "Bunnies");
                output.WallType(WallType.Bunny);
        
                output.BonesInfo((unit) => 
                {
                    if (unit.Furry)
                    {
                        return new List<BoneInfo>
                        {
                            new BoneInfo(BoneType.FurryRabbitBones, unit.Name)
                        };
                    }
                    else
                    {
                        return new List<BoneInfo>
                        {
                            new BoneInfo(BoneType.GenericBonePile, unit.Name)
                        };
                    }
                });
                output.FlavorText(new FlavorText(
                    new Texts { "long eared", "bushy tailed", "leaf biting" },
                    new Texts { "sharp eared", "strong footed", "chisel-toothed" },
                    new Texts { "bunny", "rabbit", "lagomorph", {"doe", Gender.Female}, {"buck", Gender.Male} } // This is correct. Apparently thats what they are called
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 10,
                    StomachSize = 15,
                    HasTail = true,
                    FavoredStat = Stat.Dexterity,
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.ProlificBreeder,
                        TraitType.EasyToVore,
                        TraitType.ArtfulDodge,
                        TraitType.EvasiveBattler
                    },
                    RaceDescription = "Among the weaker but more numerous of the native sapient species, the Bunnies are on the verge of turning predators themselves. While lacking in sheer strength they make up for it with agility and numbers, having much fun ensuring the latter.",
                });
                output.TownNames(new List<string>
                {
                    "Hoppington",
                    "Lopdon",
                    "Bunburg",
                    "Pawdale",
                    "Rabiton",
                    "Watershed",
                    "Cottontail Cove",
                });
                output.PreyTownNames(new List<string>
                {
                    "The Warren",
                    "Underbrush Shelter",
                    "Tree Hollow",
                    "Hidden Haven",
                    "Sanctuary",
                    "Bunny Burrow",
                    "Felt Burrow",
                    "Carrot Burrow",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.HairColor, "Hair Color: " + UnitCustomizer.HairColorLookup(unit.HairColor));
                    buttons.SetText(ButtonType.BodyAccessoryColor, "Fur Color: " + UnitCustomizer.HairColorLookup(unit.AccessoryColor));
                });
            });
        
            builder.RandomCustom(Defaults.RandomCustom);

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
                output.Sprite(input.Sprites.Bodies[13]);
            });
            builder.RenderSingle(SpriteType.SecondaryAccessory, Defaults.SpriteGens3[SpriteType.SecondaryAccessory]);
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
                    type = input.A.IsCockVoring ? 5 : 0;

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
        });
    }
}