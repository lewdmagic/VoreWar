#region

using System.Collections.Generic;
using UnityEngine;

#endregion

internal static class Slimes
{
    internal static readonly IRaceData Instance = RaceBuilder.CreateV2(Defaults.Default, builder =>
    {
        
        
        builder.Setup(output =>
        {
        output.Names("Slime", "Slimes");
        output.WallType(WallType.Slime);
        output.BonesInfo((unit) => new List<BoneInfo>()
        {
            new BoneInfo(BoneTypes.SlimePile, unit.Name, unit.AccessoryColor)
        });
        output.FlavorText(new FlavorText(
            new Texts { "amorphous", "sludgy", "juicy" },
            new Texts { "amorphous", "flowing", "hard-cored" },
            new Texts { "slime", "ooze", "jelly" },
            new Dictionary<string, string>
            {
                [WeaponNames.Mace]        = "Bone Blade",
                [WeaponNames.Axe]         = "Whip",
                [WeaponNames.SimpleBow]   = "Floating Slimey",
                [WeaponNames.CompoundBow] = "Bioelectricity",
                [WeaponNames.Claw]        = "Hardened Lump"
            }
        ));
        output.RaceTraits(new RaceTraits()
        {
            BodySize = 7,
            StomachSize = 20,
            HasTail = false,
            FavoredStat = Stat.Stomach,
            RacialTraits = new List<Traits>()
            {
                Traits.BoggingSlime,
                Traits.GelatinousBody,
                Traits.SoftBody,
            },
            RaceDescription = "A puddle of goo given form by the power of their core, the Slimes have a need to act as if they had solid bodies. Their true from is still almost liquid though, lacking organs or other features of note, and thus very hard to damage by normal means.",
        });
        output.TownNames(new List<string>
        {
            "The Nucleus",
            "Gootopia",
            "Slimesville",
            "Ectopolis",
            "Blobsburg",
            "Petri Town",
            "Splaton",
        });
        output.CustomizeButtons((unit, buttons) =>
        {       
            buttons.SetActive(ButtonType.HairColor, true);
            buttons.SetActive(ButtonType.Skintone, false);
            buttons.SetText(ButtonType.BodyAccessoryColor, "Body Color");
            buttons.SetText(ButtonType.HairColor, "Secondary Color");
            if (unit.Type == UnitType.Leader)
            {
                buttons.SetText(ButtonType.ExtraColor1, "Breast Covering");
                buttons.SetText(ButtonType.ExtraColor2, "Cock Covering");
            }
        });
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.SlimeMain);
            output.EyeTypes = 3;
            output.EyeColors = 1;
            output.HairStyles = 12;
            output.HairColors = 3;
            output.BodySizes = 2;
            //MouthTypes = 0;
            output.AvoidedMainClothingTypes = 1;
            output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.Clothing);
            output.AllowedMainClothingTypes.Set(
                ClothingTypes.BeltTopInstance,
                ClothingTypes.LeotardInstance,
                ClothingTypes.BlackTopInstance,
                RaceSpecificClothing.RainCoatInstance,
                ClothingTypes.RagsInstance
            );
            output.AllowedWaistTypes.Set(
                ClothingTypes.LoinclothInstance
            );
        });


        bool SlimeWeapon(Actor_Unit actor)
        {
            return actor.GetWeaponSprite() > 1 && actor.GetWeaponSprite() < 6;
        }

        builder.RenderSingle(SpriteType.Eyes, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeMain, input.U.AccessoryColor));
            output.Sprite(input.Sprites.Slimes[35 + input.U.EyeType]);
        });

        builder.RenderSingle(SpriteType.Mouth, 4, (input, output) =>
        {
            Defaults.SpriteGens3[SpriteType.Mouth].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeSub, input.U.AccessoryColor));
        });

        builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeSub, 3 * input.U.AccessoryColor + input.U.HairColor));
            output.Sprite(input.Sprites.Slimes[20 + input.U.HairStyle]);
        });
        builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeSub, 3 * input.U.AccessoryColor + input.U.HairColor));
            if (input.U.HairStyle == 1)
            {
                output.Sprite(input.Sprites.Slimes[32]);
                return;
            }

            if (input.U.HairStyle == 3)
            {
                output.Sprite(input.Sprites.Slimes[33]);
                return;
            }

            if (input.U.HairStyle == 2 || input.U.HairStyle == 4 || input.U.HairStyle == 7)
            {
                output.Sprite(input.Sprites.Slimes[34]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeMain, input.U.AccessoryColor));
            output.Sprite(input.Sprites.Slimes[input.A.GetSimpleBodySprite()]);
        });
        builder.RenderSingle(SpriteType.BodyAccent, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeSub, 3 * input.U.AccessoryColor + input.U.HairColor));
            output.Sprite(input.Sprites.Slimes[4 + input.A.GetBodyWeight()]);
        });
        builder.RenderSingle(SpriteType.BodyAccent2, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeSub, 3 * input.U.AccessoryColor + input.U.HairColor));
            output.Sprite(input.Sprites.Slimes[6 + (input.A.IsAttacking ? 1 : 0)]);
        });
        builder.RenderSingle(SpriteType.BodyAccessory, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeSub, 3 * input.U.AccessoryColor + input.U.HairColor));
            output.Sprite(input.Sprites.Slimes[18]);
        });
        builder.RenderSingle(SpriteType.BodySize, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeMain, input.U.AccessoryColor));
            output.Sprite(input.A.GetBodyWeight() == 1 ? input.Sprites.Slimes[3] : null);
        });
        builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeMain, input.U.AccessoryColor));
            output.Sprite(input.U.HasBreasts ? input.Sprites.Slimes[38 + input.U.BreastSize] : null);
        });
        builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeMain, input.U.AccessoryColor));
            if (input.A.HasBelly)
            {
                output.Sprite(input.Sprites.Slimes[51 + input.A.GetStomachSize()]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
        {
            Defaults.SpriteGens3[SpriteType.Dick].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeMain, input.U.AccessoryColor));
        });
        builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
        {
            Defaults.SpriteGens3[SpriteType.Balls].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeMain, input.U.AccessoryColor));
        });
        builder.RenderSingle(SpriteType.Weapon, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (SlimeWeapon(input.Actor))
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlimeSub, 3 * input.U.AccessoryColor + input.U.HairColor));
            }
            else
            {
                output.Coloring(Defaults.WhiteColored);
            }

            if (input.U.HasWeapon && input.A.Surrendered == false)
            {
                output.Sprite(input.Sprites.Slimes[8 + input.A.GetWeaponSprite()]);
            }
        });

        builder.RunBefore((input, output) =>
        {
            output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 2.5f);
            if (input.A.HasBelly)
            {
                Vector3 localScale;

                if (input.A.PredatorComponent.VisibleFullness > 4)
                {
                    float extraCap = input.A.PredatorComponent.VisibleFullness - 4;
                    float xScale = Mathf.Min(1 + extraCap / 5, 1.8f);
                    float yScale = Mathf.Min(1 + extraCap / 40, 1.1f);
                    localScale = new Vector3(xScale, yScale, 1);
                }
                else
                {
                    localScale = new Vector3(1, 1, 1);
                }

                output.ChangeSprite(SpriteType.Belly).SetLocalScale(localScale);
            }
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            if (unit.HasDick && unit.HasBreasts)
            {
                unit.HairStyle = State.Rand.Next(Config.HermsOnlyUseFemaleHair ? 8 : data.MiscRaceData.HairStyles);
            }
            else if (unit.HasDick && Config.FemaleHairForMales)
            {
                unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
            }
            else if (unit.HasDick == false && Config.MaleHairForFemales)
            {
                unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
            }
            else
            {
                if (unit.HasDick)
                {
                    unit.HairStyle = 5 + State.Rand.Next(7);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(8);
                }
            }
        });
    });

    internal static Material GetSlimeAccentMaterial(Actor_Unit actor)
    {
        return ColorPaletteMap.GetPalette(SwapType.SlimeSub, 3 * actor.Unit.AccessoryColor + actor.Unit.HairColor).colorSwapMaterial;
    }
}