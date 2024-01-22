#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.MainRaces
{
    internal static class Crypters
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
        
        
            Color bellyColor = new Color(.2519f, .2519f, .3584f);
            builder.Setup(output =>
            {
                output.Names("Crypter", "Crypters");
                output.WallType(WallType.Crypter);
                output.BonesInfo((unit) => new List<BoneInfo>()
                {
                    new BoneInfo(BoneTypes.CrypterBonePile, unit.Name, unit.AccessoryColor),
                    new BoneInfo(BoneTypes.CrypterSkull, unit.Name)
                });
                output.FlavorText(new FlavorText(
                    new Texts { "mechanical", "artifical", "whirring" },
                    new Texts { "mechanical", "artifical", "rumbling" },
                    new Texts { "crypter", "machinoid", "synthetic", "robotic", "metallic", "futuristic", "fabricated" }, //added "synthetic", "robotic", "metallic", "futuristic", "fabricated" thanks to Flame_Valxsarion
                    new Dictionary<string, string>
                    {
                        [WeaponNames.Mace]        = "Sword",
                        [WeaponNames.Axe]         = "Power Fist",
                        [WeaponNames.SimpleBow]   = "Crossbow",
                        [WeaponNames.CompoundBow] = "Cannon",
                        [WeaponNames.Claw]        = "Metal Fist"
                    } 
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 10,
                    StomachSize = 18,
                    HasTail = false,
                    FavoredStat = Stat.Agility,
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.SlowBreeder,
                        TraitType.MetalBody,
                        TraitType.Resilient
                    },
                    RaceDescription = "Arriving from a realm long dead, the Crypters shambled forth when the smell of the living beckoned them from their ancient tombs. Cold, hard metal resists both damage and attempts to eat it, but the strange powers of this realm provide no aid in crafting new automatons for the ancient spirits to inhabit.",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetActive(ButtonType.ClothingColor, false);
                });
                output.TownNames(new List<string>
                {
                    "The Eternal Palace",
                    "Citadel of Divine Purpose",
                    "Citadel of Syn",
                    "Ur-Babel Arisen",
                    "Citadel of Talmund",
                    "Gullet of Shem",
                    "Crucible of Consumption",
                    "Devouring Catalyst",
                    //Sleeping below this, but not implemented separately
                    "Tongue of Shem",
                    "Crypt of the Undying Queen",
                    "Crypt of Testament",
                    "Tomb of Doubt",
                    "Tomb of Syn",
                    "Ruins of Ur-Babel",
                    "Tomb of Talmund",
                });
                output.DickSizes = () => 1;
                output.BreastSizes = () => 1;


                output.EyeTypes = 4;
                output.MouthTypes = 4;
                output.BodySizes = 7;
                output.HairStyles += 4;
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.LizardMain);

                output.WeightGainDisabled = true;

                output.AvoidedMainClothingTypes = 0;
                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.Clothing);
                output.AllowedMainClothingTypes.Clear();
                output.AllowedWaistTypes.Clear();

                output.AvoidedMouthTypes = 0;
                output.AvoidedEyeTypes = 0;
            });


            builder.RenderSingle(SpriteType.Head, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Crypters[2]);
            });
            builder.RenderSingle(SpriteType.Eyes, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
                output.Sprite(input.Sprites.Crypters[8 + input.U.EyeType]);
            });

            builder.RenderSingle(SpriteType.Mouth, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.MouthType > 3) //Defending against a weird exception.
                {
                    input.U.MouthType = 3;
                }

                output.Sprite(input.Sprites.Crypters[37 + 2 * input.U.MouthType + (input.A.IsEating ? 1 : 0)]);
            });

            builder.RenderSingle(SpriteType.Hair, 12, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.U.HairColor));
                if (input.U.HairStyle < 15)
                {
                    output.Sprite(input.Sprites.Hair[input.U.HairStyle]).AddOffset(0, 1.25f);
                    return;
                }

                output.Sprite(input.Sprites.Crypters[12 + input.U.HairStyle - 15]).AddOffset(0, 0);
            });

            builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.U.HairColor));
                if (input.U.HairStyle < 15)
                {
                    output.AddOffset(0, 1.25f);
                }
                else
                {
                    output.AddOffset(0, 0);
                }

                if (input.U.HairStyle == 1)
                {
                    output.Sprite(input.Sprites.Hair[input.RaceData.HairStyles - 4]);
                }
                else if (input.U.HairStyle == 2)
                {
                    output.Sprite(input.Sprites.Hair[input.RaceData.HairStyles + 1 - 4]);
                }
                else if (input.U.HairStyle == 5)
                {
                    output.Sprite(input.Sprites.Hair[input.RaceData.HairStyles + 3 - 4]);
                }
                else if (input.U.HairStyle == 6 || input.U.HairStyle == 7)
                {
                    output.Sprite(input.Sprites.Hair[input.RaceData.HairStyles + 2 - 4]);
                }
            });

            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Crypters[0]);
            });
            builder.RenderSingle(SpriteType.BodyAccent, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Crypters[4]);
            });
            builder.RenderSingle(SpriteType.BodyAccent2, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Crypters[5]);
            });
            builder.RenderSingle(SpriteType.BodyAccent3, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Crypters[6 + (input.A.IsEating ? 1 : 0)]);
            });
            builder.RenderSingle(SpriteType.BodyAccent4, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Crypters[36]);
            });
            builder.RenderSingle(SpriteType.BodyAccessory, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Crypters[28]);
            });
            builder.RenderSingle(SpriteType.SecondaryAccessory, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Crypters[3]);
            });

            builder.RenderSingle(SpriteType.BodySize, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Crypters[29 + input.U.BodySize]);
            });

            builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Belly].Invoke(input, output);
                output.Coloring(bellyColor);
            });
        
        
            builder.RenderSingle(SpriteType.Weapon, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.CrypterWeapon, input.U.AccessoryColor));
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    output.Sprite(input.Sprites.Crypters[19 + input.A.GetWeaponSprite()]);
                }
                else
                {
                    output.Sprite(input.Sprites.Crypters[17 + (input.A.IsAttacking ? 1 : 0)]);
                }
            });

            builder.RandomCustom(data =>
            {
                Unit unit = data.Unit;
                Defaults.RandomCustom(data);

                if (unit.HasDick && unit.HasBreasts)
                {
                    if (Config.HermsOnlyUseFemaleHair)
                    {
                        unit.HairStyle = State.Rand.Next(8);
                    }
                    else
                    {
                        unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
                    }
                }
                else if (unit.HasDick && Config.FemaleHairForMales)
                {
                    unit.HairStyle = State.Rand.Next(15);
                }
                else if (unit.HasDick == false && Config.MaleHairForFemales)
                {
                    unit.HairStyle = State.Rand.Next(15);
                }
                else
                {
                    if (unit.HasDick)
                    {
                        unit.HairStyle = 8 + State.Rand.Next(7);
                    }
                    else
                    {
                        unit.HairStyle = State.Rand.Next(8);
                    }
                }
            });
        });
    }
}