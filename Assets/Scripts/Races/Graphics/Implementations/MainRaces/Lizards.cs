#region

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Races.Graphics.Implementations.MainRaces
{

    #endregion

    internal interface IFacingFrontParameters : IParameters
    {
        bool FacingFront { get; }
    }

    internal class FacingFrontParameters : IFacingFrontParameters
    {
        public bool FacingFront { get; set; }
    }
    
    

    internal static class Lizards
    {
        
        private static bool IsFacingFront(Actor_Unit actor)
        {
            if (actor.IsAnalVoring || actor.IsUnbirthing)
            {
                return false;
            }
            else if (actor.Unit.TailType == 0 || actor.IsOralVoring || actor.IsAttacking || actor.IsCockVoring || actor.IsBreastVoring)
            {
                return true;
            }
            else
            {
                return true;
            }
        }
        
        internal static readonly IRaceData Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
        
            builder.Setup(output =>
            {
                output.Names("Lizard", "Lizards");
                output.WallType(WallType.Lizard);
                output.BonesInfo((unit) => 
                {
                    if (unit.Furry)
                    {
                        return new List<BoneInfo>
                        {
                            new BoneInfo(BoneTypes.FurryBones, unit.Name)
                        };
                    }
                    else
                    {
                        return new List<BoneInfo>
                        {
                            new BoneInfo(BoneTypes.GenericBonePile, unit.Name),
                            new BoneInfo(BoneTypes.LizardSkull, "")
                        };
                    }
                });
                output.FlavorText(new FlavorText(
                    new Texts { "hairless", "cold-blooded", "wiry" },
                    new Texts { "thick-scaled", "cold-blooded", "tough" },
                    new Texts { "lizard", "reptile", "reptilian" }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 12,
                    StomachSize = 18,
                    HasTail = true,
                    FavoredStat = Stat.Voracity,
                    RacialTraits = new List<Traits>()
                    {
                        Traits.Resilient,
                        Traits.Intimidating
                    },

                    RaceDescription = "Emerging from dense jungles, the Lizards are eager to expand their presence in the universe. Their hard scales offered them great protection from the thorns and insects of their former home, and still offer natural resistance from harm.",
                });
                output.TownNames(new List<string>
                {
                    "Lizotetca",
                    "Reptula",
                    "Lair of Bone",
                    "Chalscale",
                    "Komodoalco",
                    "Lair of Venom",
                    "Vale of Broken Teeth",
                    "Crocotula",
                    "Iguatepec",
                    "Lair of the Beast",
                    "Cult of the Wyrm",
                    "Dragon Tongue",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetActive(ButtonType.Skintone, false);
                    buttons.SetActive(ButtonType.HairColor, true);
                    buttons.SetText(ButtonType.HairColor, "Horn Color");
                    buttons.SetText(ButtonType.HairStyle, "Horn Style");
                    buttons.SetText(ButtonType.BodyAccessoryColor, "Body Color");
                    buttons.SetText(ButtonType.ClothingExtraType1, "Leg Guards");
                    buttons.SetText(ButtonType.ClothingExtraType2, "Armlets");
                    buttons.SetText(ButtonType.HatType, "Crown");
                });
                output.BreastSizes = () => Config.AllowHugeBreasts ? 8 : 5;

                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.LizardMain);
                output.EyeTypes = 5;
                output.HairStyles = 6;
                output.HairColors = ColorPaletteMap.GetPaletteCount(SwapType.LizardMain);
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.LizardMain);
                output.MouthTypes = 1;
                output.BodySizes = 0;


                output.AllowedClothingHatTypes.Set( //Crown
                    RaceSpecificClothing.LizardLeaderCrownInstance,
                    RaceSpecificClothing.LizardBoneCrownInstance,
                    RaceSpecificClothing.LizardLeatherCrownInstance,
                    RaceSpecificClothing.LizardClothCrownInstance
                );

                output.AvoidedMainClothingTypes = 3;
                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.LizardLight);
                output.AllowedMainClothingTypes.Set(
                    CommonClothing.BeltTopInstance,
                    CommonClothing.LeotardInstance,
                    RaceSpecificClothing.LizardBlackTopInstance,
                    RaceSpecificClothing.LizardBikiniTopInstance,
                    RaceSpecificClothing.LizardStrapTopInstance,
                    RaceSpecificClothing.LizardBoneTopInstance,
                    RaceSpecificClothing.LizardLeatherTopInstance,
                    RaceSpecificClothing.LizardClothTopInstance,
                    RaceSpecificClothing.LizardPeasantInstance,
                    CommonClothing.RagsInstance,
                    RaceSpecificClothing.LizardLeaderTopInstance
                );
                output.AllowedWaistTypes.Set(
                    CommonClothing.BikiniBottomInstance,
                    CommonClothing.LoinclothInstance,
                    CommonClothing.ShortsInstance,
                    RaceSpecificClothing.LizardLeaderSkirtInstance,
                    RaceSpecificClothing.LizardBoneLoinsInstance,
                    RaceSpecificClothing.LizardLeatherLoinsInstance,
                    RaceSpecificClothing.LizardClothLoinsInstance
                );
                output.ExtraMainClothing1Types.Set(
                    RaceSpecificClothing.LizardLeaderLegguardsInstance,
                    RaceSpecificClothing.LizardBoneLegguardsInstance,
                    RaceSpecificClothing.LizardLeatherLegguardsInstance,
                    RaceSpecificClothing.LizardClothShortsInstance
                );
                output.ExtraMainClothing2Types.Set(
                    RaceSpecificClothing.LizardLeaderArmbandsInstance,
                    RaceSpecificClothing.LizardBoneArmbandsInstance,
                    //RaceSpecificClothing.LizardBoneArmbands2,
                    //RaceSpecificClothing.LizardBoneArmbands3,
                    RaceSpecificClothing.LizardLeatherArmbandsInstance,
                    //RaceSpecificClothing.LizardLeatherArmbands2,
                    //RaceSpecificClothing.LizardLeatherArmbands3,
                    RaceSpecificClothing.LizardClothArmbandsInstance
                    //RaceSpecificClothing.LizardClothArmbands2,
                    //RaceSpecificClothing.LizardClothArmbands3
                );

                output.AvoidedMouthTypes = 0;
                output.AvoidedEyeTypes = 0;
            });

            builder.RenderSingle(SpriteType.Head, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.AccessoryColor));
                if (!IsFacingFront(input.A))
                {
                    output.Sprite(input.Sprites.LizardsBooty[0]);
                }
            });

            builder.RenderSingle(SpriteType.Eyes, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
                if (IsFacingFront(input.A))
                {
                    output.Sprite(input.Sprites.Lizards[13 + input.U.EyeType]);
                }
            });

            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.AccessoryColor));
                if (IsFacingFront(input.A))
                {
                    output.Sprite(input.Sprites.Lizards[input.A.GetSimpleBodySprite()]).Layer(2);
                    return;
                }

                output.Sprite(input.Sprites.LizardsBooty[1]).Layer(16);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardLight, input.U.AccessoryColor));
                if (IsFacingFront(input.A))
                {
                    output.Sprite(input.Sprites.Lizards[5 + (input.A.IsOralVoring ? 1 : 0)]).Layer(7);
                    return;
                }

                output.Sprite(input.Sprites.LizardsBooty[76]).Layer(17);
            }); //Ventral Colour

            builder.RenderSingle(SpriteType.BodyAccent2, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (IsFacingFront(input.A))
                {
                    output.Sprite(input.Sprites.Lizards[3 + (input.A.IsAttacking ? 1 : 0)]).Layer(7);
                }
                else
                {
                    output.Layer(25);

                    try
                    {
                        if (input.U.HasDick && input.A.PredatorComponent.BallsFullness >= 2.5)
                        {
                            output.Sprite(input.Sprites.LizardsBooty[75]);
                            return;
                        }
                    }
                    catch (NullReferenceException e)
                    {
                        Debug.Log($"Missing the predator component: {e.Message}");
                    }

                    output.Sprite(input.A.GetStomachSize(16) >= 16 ? input.Sprites.LizardsBooty[74] : input.Sprites.LizardsBooty[6]);
                }
            }); //Claws

            builder.RenderSingle(SpriteType.BodyAccent3, 14, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.AccessoryColor));
                if (!IsFacingFront(input.A))
                {
                    output.Sprite(input.Sprites.LizardsBooty[2]);
                }
            }); //Left Hand

            builder.RenderSingle(SpriteType.BodyAccent4, 14, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.AccessoryColor));
                if (!IsFacingFront(input.A))
                {
                    output.Sprite(input.Sprites.LizardsBooty[3]);
                }
            }); //Right Arm

            builder.RenderSingle(SpriteType.BodyAccent5, 24, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.AccessoryColor));
                if (!IsFacingFront(input.A))
                {
                    output.Sprite(input.Sprites.LizardsBooty[4]);
                }
            }); //Right Hand

            builder.RenderSingle(SpriteType.BodyAccent6, 19, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.AccessoryColor));
                if (!IsFacingFront(input.A))
                {
                    output.Sprite(input.Sprites.LizardsBooty[5]);
                }
            }); //Right Hand Shadow

            builder.RenderSingle(SpriteType.BodyAccessory, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.HairColor));
                if (IsFacingFront(input.A))
                {
                    output.Sprite(input.Sprites.Lizards[7 + input.U.HairStyle]);
                    return;
                }

                output.Sprite(input.Sprites.LizardsBooty[68 + input.U.HairStyle]);
            }); //Horns / Skin

            builder.RenderSingle(SpriteType.Breasts, 17, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardLight, input.U.AccessoryColor));
                if (IsFacingFront(input.A))
                {
                    output.Layer(16);
                    if (Config.LizardsHaveNoBreasts)
                    {
                        return;
                    }

                    if (input.U.HasBreasts == false)
                    {
                        return;
                    }

                    if (input.A.SquishedBreasts && input.U.BreastSize >= 3 && input.U.BreastSize <= 6)
                    {
                        output.Sprite(input.Sprites.SquishedBreasts[input.U.BreastSize - 3]);
                        return;
                    }

                    output.Sprite(input.Sprites.Lizards[18 + input.U.BreastSize]);
                }
                else
                {
                    output.Layer(15);
                    if (Config.LizardsHaveNoBreasts)
                    {
                        return;
                    }

                    if (input.U.HasBreasts == false)
                    {
                        return;
                    }

                    if (input.U.BreastSize <= 2)
                    {
                        return;
                    }

                    if (input.U.BreastSize >= 3)
                    {
                        output.Sprite(input.Sprites.LizardsBooty[46 + input.U.BreastSize - 3]).Layer(7);
                    }
                }
            });

            builder.RenderSingle(SpriteType.Belly, 16, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardLight, input.U.AccessoryColor));
                if (IsFacingFront(input.A))
                {
                    if (input.A.HasBelly)
                    {
                        output.Sprite(input.Sprites.Bellies[input.A.GetStomachSize()]);
                    }
                }
                else
                {
                    if (input.A.HasBelly)
                    {
                        output.Sprite(input.Sprites.LizardsBooty[52 + input.A.GetStomachSize()]);
                    }
                }
            });

            builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardLight, input.U.AccessoryColor));
                if (IsFacingFront(input.A))
                {
                    if (input.U.HasDick == false)
                    {
                        return;
                    }

                    if (input.A.IsErect())
                    {
                        if (input.A.PredatorComponent?.VisibleFullness < .75f)
                        {
                            output.Sprite(input.Sprites.ErectDicks[input.U.DickSize]).Layer(18);
                            return;
                        }

                        output.Sprite(input.Sprites.Dicks[input.U.DickSize]).Layer(12);
                        return;
                    }

                    output.Sprite(input.Sprites.Dicks[input.U.DickSize]).Layer(9);
                }
                else
                {
                    if (input.U.HasDick == false)
                    {
                        return;
                    }

                    output.Sprite(input.Sprites.LizardsBooty[14 + input.U.DickSize]).Layer(19);
                }
            });

            builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardLight, input.U.AccessoryColor));
                if (IsFacingFront(input.A))
                {
                    if (input.U.HasDick == false)
                    {
                        return;
                    }

                    output.Layer(8);
                    output.AddOffset(0, 0);

                    int baseSize = input.U.DickSize / 3;
                    int ballOffset = input.A.GetBallSize(21);

                    int combined = Math.Min(baseSize + ballOffset + 3, 20);
                    // Always false
                    // if (combined == 21) 
                    // {
                    //     output.AddOffset(0, -14 * .625f);
                    // }
                    // else if (combined == 20)
                    if (combined == 20)
                    {
                        output.AddOffset(0, -12 * .625f);
                    }
                    else if (combined >= 17)
                    {
                        output.AddOffset(0, -8 * .625f);
                    }

                    if (ballOffset > 0)
                    {
                        output.Sprite(input.Sprites.Balls[combined]);
                        return;
                    }

                    output.Sprite(input.Sprites.Balls[baseSize]);
                }
                else
                {
                    if (input.U.HasDick == false)
                    {
                        return;
                    }

                    output.Layer(20);
                    output.AddOffset(0, 0);
                    int baseSize = input.U.DickSize / 3;
                    int ballOffset = input.A.GetBallSize(21);

                    int combined = Math.Min(baseSize + ballOffset + 3, 20);
                    // Always false
                    // if (combined == 21)
                    // {
                    //     output.AddOffset(0, -14 * .625f);
                    // }
                    // else if (combined == 20)
                    if (combined == 20)
                    {
                        output.AddOffset(0, -12 * .625f);
                    }
                    else if (combined >= 17)
                    {
                        output.AddOffset(0, -8 * .625f);
                    }

                    if (ballOffset > 0)
                    {
                        output.Sprite(input.Sprites.LizardsBooty[23 + combined]);
                        return;
                    }

                    output.Sprite(input.Sprites.LizardsBooty[23 + baseSize]);
                }
            });

            builder.RenderSingle(SpriteType.Pussy, 21, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardLight, input.U.AccessoryColor));
                if (IsFacingFront(input.A))
                {
                    return;
                }

                if (input.A.IsUnbirthing)
                {
                    if (input.U.HasDick == false && input.U.HasBreasts) // Visible for Females
                    {
                        output.Sprite(input.Sprites.LizardsBooty[12]);
                        return;
                    }

                    if (input.U.HasDick && input.U.HasBreasts == false) // Hide for Males
                    {
                        return;
                    }

                    if (input.U.HasDick && input.U.HasBreasts && !Config.HermsCanUB) // Hide for Herms (Didn't work)
                    {
                        return;
                    }

                    if (input.U.HasDick && input.U.HasBreasts && Config.HermsCanUB) // Visible for Herms
                    {
                        output.Sprite(input.Sprites.LizardsBooty[12]);
                    }
                }

                if (input.A.IsAnalVoring)
                {
                    if (input.U.HasDick == false && input.U.HasBreasts) // Visible for Females
                    {
                        output.Sprite(input.Sprites.LizardsBooty[10]);
                        return;
                    }

                    if (input.U.HasDick && input.U.HasBreasts == false) // Hide for Males
                    {
                        return;
                    }

                    if (input.U.HasDick && input.U.HasBreasts && !Config.HermsCanUB) // Hide for Herms (Didn't work)
                    {
                        return;
                    }

                    if (input.U.HasDick && input.U.HasBreasts && !Config.HermsCanUB) // Visible for Herms
                    {
                        output.Sprite(input.Sprites.LizardsBooty[10]);
                    }
                }
            });

            builder.RenderSingle(SpriteType.PussyIn, 22, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (IsFacingFront(input.A))
                {
                    return;
                }

                if (input.A.IsUnbirthing)
                {
                    if (input.U.HasDick == false && input.U.HasBreasts) // Visible for Females
                    {
                        output.Sprite(input.Sprites.LizardsBooty[13]);
                        return;
                    }

                    if (input.U.HasDick && input.U.HasBreasts == false) // Hide for Males
                    {
                        return;
                    }

                    if (input.U.HasDick && input.U.HasBreasts &&
                        Config.HermsCanUB == false) // Hide for Herms (Didn't work)
                    {
                        return;
                    }

                    if (input.U.HasDick && input.U.HasBreasts &&
                        Config.HermsCanUB) // Visible for Herms
                    {
                        output.Sprite(input.Sprites.LizardsBooty[13]);
                        return;
                    }

                    return; // i dunno what's going on
                }

                if (input.A.IsAnalVoring)
                {
                    if (input.U.HasDick == false && input.U.HasBreasts) // Visible for Females
                    {
                        output.Sprite(input.Sprites.LizardsBooty[11]);
                        return;
                    }

                    if (input.U.HasDick && !input.U.HasBreasts) // Hide for Males
                    {
                        return;
                    }

                    if (input.U.HasDick && input.U.HasBreasts && !Config.HermsCanUB) // Hide for Herms (Didn't work)
                    {
                        return;
                    }

                    if (input.U.HasDick && input.U.HasBreasts && Config.HermsCanUB) // Visible for Herms
                    {
                        output.Sprite(input.Sprites.LizardsBooty[11]);
                    }
                }
            });

            builder.RenderSingle(SpriteType.Anus, 21, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardLight, input.U.AccessoryColor));
                if (IsFacingFront(input.A))
                {
                    return;
                }

                if (input.A.IsUnbirthing)
                {
                    output.Sprite(input.Sprites.LizardsBooty[7]);
                    return;
                }

                if (input.A.IsAnalVoring)
                {
                    output.Sprite(input.Sprites.LizardsBooty[8]);
                }
            });

            builder.RenderSingle(SpriteType.AnusIn, 22, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (IsFacingFront(input.A))
                {
                    return;
                }

                if (input.A.IsUnbirthing)
                {
                    return;
                }

                if (input.A.IsAnalVoring)
                {
                    output.Sprite(input.Sprites.LizardsBooty[9]);
                }
            });

            builder.RenderSingle(SpriteType.Weapon, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (IsFacingFront(input.A) == false)
                {
                    return;
                }

                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    if (input.A.GetWeaponSprite() == 7)
                    {
                        return;
                    }

                    output.Sprite(input.Sprites.Lizards[46 + input.A.GetWeaponSprite()]);
                }
            });

            // BackWeapon has no Default behavior
            // builder.SetRenderSingle(SpriteType.BackWeapon, 0, (input, output) =>
            // {
            //     output.Color(Defaults.WhiteColored).Palette(null);
            //     Defaults.SpriteGens3[SpriteType.BackWeapon].Invoke(input, output);
            // });


            builder.RunBefore((input, output) =>
            {
                if (IsFacingFront(input.A))
                {
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

                        output.ChangeSprite(SpriteType.Belly).SetActive(true).SetLocalScale(localScale);
                    }
                }
                else
                {
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

                        output.ChangeSprite(SpriteType.Belly).SetActive(true).SetLocalScale(localScale);
                    }
                }
            });

            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                Unit unit = data.Unit;

                if (unit.Type == UnitType.Leader)
                {
                    unit.ClothingHatType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedClothingHatTypesBasic, RaceSpecificClothing.LizardLeaderCrownInstance);
                    unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypesBasic, RaceSpecificClothing.LizardLeaderTopInstance);
                    unit.ClothingType2 = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedWaistTypesBasic, RaceSpecificClothing.LizardLeaderSkirtInstance);
                    unit.ClothingExtraType1 = 1 + Extensions.IndexOf(data.MiscRaceData.ExtraMainClothing1TypesBasic, RaceSpecificClothing.LizardLeaderLegguardsInstance);
                    unit.ClothingExtraType2 = 1 + Extensions.IndexOf(data.MiscRaceData.ExtraMainClothing2TypesBasic, RaceSpecificClothing.LizardLeaderArmbandsInstance);
                }

                if (unit.ClothingType == 10) //If in prison garb
                {
                    unit.ClothingHatType = 0;
                    unit.ClothingExtraType1 = 0;
                    unit.ClothingExtraType2 = 0;
                }
            });
        });


        //protected override Color BodyColor(Actor_Unit actor) => ColorMap.GetLizardColor(actor.Unit.AccessoryColor);
        //protected override Color BodyAccessoryColor(Actor_Unit actor) => ColorMap.GetLizardColor(actor.Unit.AccessoryColor);
    }
}