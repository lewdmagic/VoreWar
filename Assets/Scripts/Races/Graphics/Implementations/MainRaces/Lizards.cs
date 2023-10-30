#region

using System;
using UnityEngine;

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
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Default<FacingFrontParameters>, builder =>
    {
        builder.Setup(output =>
        {
            output.BreastSizes = () => Config.AllowHugeBreasts ? 8 : 5;

            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);
            output.EyeTypes = 5;
            output.HairStyles = 6;
            output.HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);
            output.MouthTypes = 1;
            output.BodySizes = 0;


            output.AllowedClothingHatTypes.Set( //Crown
                RaceSpecificClothing.LizardLeaderCrownInstance,
                RaceSpecificClothing.LizardBoneCrownInstance,
                RaceSpecificClothing.LizardLeatherCrownInstance,
                RaceSpecificClothing.LizardClothCrownInstance
            );

            output.AvoidedMainClothingTypes = 3;
            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardLight);
            output.AllowedMainClothingTypes.Set(
                ClothingTypes.BeltTopInstance,
                ClothingTypes.LeotardInstance,
                RaceSpecificClothing.LizardBlackTopInstance,
                RaceSpecificClothing.LizardBikiniTopInstance,
                RaceSpecificClothing.LizardStrapTopInstance,
                RaceSpecificClothing.LizardBoneTopInstance,
                RaceSpecificClothing.LizardLeatherTopInstance,
                RaceSpecificClothing.LizardClothTopInstance,
                RaceSpecificClothing.LizardPeasantInstance,
                ClothingTypes.RagsInstance,
                RaceSpecificClothing.LizardLeaderTopInstance
            );
            output.AllowedWaistTypes.Set(
                ClothingTypes.BikiniBottomInstance,
                ClothingTypes.LoinclothInstance,
                ClothingTypes.ShortsInstance,
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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.AccessoryColor));
            if (!input.Params.FacingFront)
            {
                output.Sprite(input.Sprites.LizardsBooty[0]);
            }
        });

        builder.RenderSingle(SpriteType.Eyes, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            if (input.Params.FacingFront)
            {
                output.Sprite(input.Sprites.Lizards[13 + input.Actor.Unit.EyeType]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.AccessoryColor));
            if (input.Params.FacingFront)
            {
                output.Sprite(input.Sprites.Lizards[input.Actor.GetSimpleBodySprite()]).Layer(2);
                return;
            }

            output.Sprite(input.Sprites.LizardsBooty[1]).Layer(16);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, input.Actor.Unit.AccessoryColor));
            if (input.Params.FacingFront)
            {
                output.Sprite(input.Sprites.Lizards[5 + (input.Actor.IsOralVoring ? 1 : 0)]).Layer(7);
                return;
            }

            output.Sprite(input.Sprites.LizardsBooty[76]).Layer(17);
        }); //Ventral Colour

        builder.RenderSingle(SpriteType.BodyAccent2, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Params.FacingFront)
            {
                output.Sprite(input.Sprites.Lizards[3 + (input.Actor.IsAttacking ? 1 : 0)]).Layer(7);
            }
            else
            {
                output.Layer(25);

                try
                {
                    if (input.Actor.Unit.HasDick && input.Actor.PredatorComponent.BallsFullness >= 2.5)
                    {
                        output.Sprite(input.Sprites.LizardsBooty[75]);
                        return;
                    }
                }
                catch (NullReferenceException e)
                {
                    Debug.Log($"Missing the predator component: {e.Message}");
                }

                output.Sprite(input.Actor.GetStomachSize(16) >= 16 ? input.Sprites.LizardsBooty[74] : input.Sprites.LizardsBooty[6]);
            }
        }); //Claws

        builder.RenderSingle(SpriteType.BodyAccent3, 14, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.AccessoryColor));
            if (!input.Params.FacingFront)
            {
                output.Sprite(input.Sprites.LizardsBooty[2]);
            }
        }); //Left Hand

        builder.RenderSingle(SpriteType.BodyAccent4, 14, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.AccessoryColor));
            if (!input.Params.FacingFront)
            {
                output.Sprite(input.Sprites.LizardsBooty[3]);
            }
        }); //Right Arm

        builder.RenderSingle(SpriteType.BodyAccent5, 24, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.AccessoryColor));
            if (!input.Params.FacingFront)
            {
                output.Sprite(input.Sprites.LizardsBooty[4]);
            }
        }); //Right Hand

        builder.RenderSingle(SpriteType.BodyAccent6, 19, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.AccessoryColor));
            if (!input.Params.FacingFront)
            {
                output.Sprite(input.Sprites.LizardsBooty[5]);
            }
        }); //Right Hand Shadow

        builder.RenderSingle(SpriteType.BodyAccessory, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.HairColor));
            if (input.Params.FacingFront)
            {
                output.Sprite(input.Sprites.Lizards[7 + input.Actor.Unit.HairStyle]);
                return;
            }

            output.Sprite(input.Sprites.LizardsBooty[68 + input.Actor.Unit.HairStyle]);
        }); //Horns / Skin

        builder.RenderSingle(SpriteType.Breasts, 17, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, input.Actor.Unit.AccessoryColor));
            if (input.Params.FacingFront)
            {
                output.Layer(16);
                if (Config.LizardsHaveNoBreasts)
                {
                    return;
                }

                if (input.Actor.Unit.HasBreasts == false)
                {
                    return;
                }

                if (input.Actor.SquishedBreasts && input.Actor.Unit.BreastSize >= 3 && input.Actor.Unit.BreastSize <= 6)
                {
                    output.Sprite(input.Sprites.SquishedBreasts[input.Actor.Unit.BreastSize - 3]);
                    return;
                }

                output.Sprite(input.Sprites.Lizards[18 + input.Actor.Unit.BreastSize]);
            }
            else
            {
                output.Layer(15);
                if (Config.LizardsHaveNoBreasts)
                {
                    return;
                }

                if (input.Actor.Unit.HasBreasts == false)
                {
                    return;
                }

                if (input.Actor.Unit.BreastSize <= 2)
                {
                    return;
                }

                if (input.Actor.Unit.BreastSize >= 3)
                {
                    output.Sprite(input.Sprites.LizardsBooty[46 + input.Actor.Unit.BreastSize - 3]).Layer(7);
                }
            }
        });

        builder.RenderSingle(SpriteType.Belly, 16, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, input.Actor.Unit.AccessoryColor));
            if (input.Params.FacingFront)
            {
                if (input.Actor.HasBelly)
                {
                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                    {
                        output.Sprite(input.Sprites.Bellies[17]).AddOffset(0, -30 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                    {
                        output.Sprite(input.Sprites.Bellies[16]).AddOffset(0, -30 * .625f);
                        return;
                    }

                    output.Sprite(input.Sprites.Bellies[input.Actor.GetStomachSize()]);
                }
            }
            else
            {
                if (input.Actor.HasBelly)
                {
                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                    {
                        output.Sprite(input.Sprites.Bellies[17]).AddOffset(0, -30 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                    {
                        output.Sprite(input.Sprites.Bellies[16]).AddOffset(0, -30 * .625f);
                        return;
                    }

                    output.Sprite(input.Sprites.LizardsBooty[52 + input.Actor.GetStomachSize()]);
                }
            }
        });

        builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, input.Actor.Unit.AccessoryColor));
            if (input.Params.FacingFront)
            {
                if (input.Actor.Unit.HasDick == false)
                {
                    return;
                }

                if (input.Actor.IsErect())
                {
                    if (input.Actor.PredatorComponent?.VisibleFullness < .75f)
                    {
                        output.Sprite(input.Sprites.ErectDicks[input.Actor.Unit.DickSize]).Layer(18);
                        return;
                    }

                    output.Sprite(input.Sprites.Dicks[input.Actor.Unit.DickSize]).Layer(12);
                    return;
                }

                output.Sprite(input.Sprites.Dicks[input.Actor.Unit.DickSize]).Layer(9);
            }
            else
            {
                if (input.Actor.Unit.HasDick == false)
                {
                    return;
                }

                output.Sprite(input.Sprites.LizardsBooty[14 + input.Actor.Unit.DickSize]).Layer(19);
            }
        });

        builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, input.Actor.Unit.AccessoryColor));
            if (input.Params.FacingFront)
            {
                if (input.Actor.Unit.HasDick == false)
                {
                    return;
                }

                output.Layer(8);
                output.AddOffset(0, 0);

                int baseSize = input.Actor.Unit.DickSize / 3;
                int ballOffset = input.Actor.GetBallSize(21);
                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) &&
                    input.Actor.GetBallSize(21, .9f) == 21)
                {
                    output.Sprite(input.Sprites.Balls[24]).AddOffset(0, -18 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) &&
                    input.Actor.GetBallSize(21, .9f) == 21)
                {
                    output.Sprite(input.Sprites.Balls[23]).AddOffset(0, -18 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) &&
                    input.Actor.GetBallSize(21, .9f) == 20)
                {
                    output.Sprite(input.Sprites.Balls[22]).AddOffset(0, -15 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) &&
                    input.Actor.GetBallSize(21, .9f) == 19)
                {
                    output.Sprite(input.Sprites.Balls[21]).AddOffset(0, -14 * .625f);
                    return;
                }

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
                if (input.Actor.Unit.HasDick == false)
                {
                    return;
                }

                output.Layer(20);
                output.AddOffset(0, 0);
                int baseSize = input.Actor.Unit.DickSize / 3;
                int ballOffset = input.Actor.GetBallSize(21);
                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) &&
                    input.Actor.GetBallSize(21, .9f) == 21)
                {
                    output.Sprite(input.Sprites.LizardsBooty[42]).AddOffset(0, -18 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) &&
                    input.Actor.GetBallSize(21, .9f) == 21)
                {
                    output.Sprite(input.Sprites.LizardsBooty[41]).AddOffset(0, -18 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) &&
                    input.Actor.GetBallSize(21, .9f) == 20)
                {
                    output.Sprite(input.Sprites.LizardsBooty[40]).AddOffset(0, -15 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) &&
                    input.Actor.GetBallSize(21, .9f) == 19)
                {
                    output.Sprite(input.Sprites.LizardsBooty[39]).AddOffset(0, -14 * .625f);
                    return;
                }

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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, input.Actor.Unit.AccessoryColor));
            if (input.Params.FacingFront)
            {
                return;
            }

            if (input.Actor.IsUnbirthing)
            {
                if (input.Actor.Unit.HasDick == false && input.Actor.Unit.HasBreasts) // Visible for Females
                {
                    output.Sprite(input.Sprites.LizardsBooty[12]);
                    return;
                }

                if (input.Actor.Unit.HasDick && input.Actor.Unit.HasBreasts == false) // Hide for Males
                {
                    return;
                }

                if (input.Actor.Unit.HasDick && input.Actor.Unit.HasBreasts && !Config.HermsCanUB) // Hide for Herms (Didn't work)
                {
                    return;
                }

                if (input.Actor.Unit.HasDick && input.Actor.Unit.HasBreasts && Config.HermsCanUB) // Visible for Herms
                {
                    output.Sprite(input.Sprites.LizardsBooty[12]);
                }
            }

            if (input.Actor.IsAnalVoring)
            {
                if (input.Actor.Unit.HasDick == false && input.Actor.Unit.HasBreasts) // Visible for Females
                {
                    output.Sprite(input.Sprites.LizardsBooty[10]);
                    return;
                }

                if (input.Actor.Unit.HasDick && input.Actor.Unit.HasBreasts == false) // Hide for Males
                {
                    return;
                }

                if (input.Actor.Unit.HasDick && input.Actor.Unit.HasBreasts && !Config.HermsCanUB) // Hide for Herms (Didn't work)
                {
                    return;
                }

                if (input.Actor.Unit.HasDick && input.Actor.Unit.HasBreasts && !Config.HermsCanUB) // Visible for Herms
                {
                    output.Sprite(input.Sprites.LizardsBooty[10]);
                }
            }
        });

        builder.RenderSingle(SpriteType.PussyIn, 22, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Params.FacingFront)
            {
                return;
            }

            if (input.Actor.IsUnbirthing)
            {
                if (input.Actor.Unit.HasDick == false && input.Actor.Unit.HasBreasts) // Visible for Females
                {
                    output.Sprite(input.Sprites.LizardsBooty[13]);
                    return;
                }

                if (input.Actor.Unit.HasDick && input.Actor.Unit.HasBreasts == false) // Hide for Males
                {
                    return;
                }

                if (input.Actor.Unit.HasDick && input.Actor.Unit.HasBreasts &&
                    Config.HermsCanUB == false) // Hide for Herms (Didn't work)
                {
                    return;
                }

                if (input.Actor.Unit.HasDick && input.Actor.Unit.HasBreasts &&
                    Config.HermsCanUB) // Visible for Herms
                {
                    output.Sprite(input.Sprites.LizardsBooty[13]);
                    return;
                }

                return; // i dunno what's going on
            }

            if (input.Actor.IsAnalVoring)
            {
                if (input.Actor.Unit.HasDick == false && input.Actor.Unit.HasBreasts) // Visible for Females
                {
                    output.Sprite(input.Sprites.LizardsBooty[11]);
                    return;
                }

                if (input.Actor.Unit.HasDick && !input.Actor.Unit.HasBreasts) // Hide for Males
                {
                    return;
                }

                if (input.Actor.Unit.HasDick && input.Actor.Unit.HasBreasts && !Config.HermsCanUB) // Hide for Herms (Didn't work)
                {
                    return;
                }

                if (input.Actor.Unit.HasDick && input.Actor.Unit.HasBreasts && Config.HermsCanUB) // Visible for Herms
                {
                    output.Sprite(input.Sprites.LizardsBooty[11]);
                }
            }
        });

        builder.RenderSingle(SpriteType.Anus, 21, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, input.Actor.Unit.AccessoryColor));
            if (input.Params.FacingFront)
            {
                return;
            }

            if (input.Actor.IsUnbirthing)
            {
                output.Sprite(input.Sprites.LizardsBooty[7]);
                return;
            }

            if (input.Actor.IsAnalVoring)
            {
                output.Sprite(input.Sprites.LizardsBooty[8]);
            }
        });

        builder.RenderSingle(SpriteType.AnusIn, 22, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Params.FacingFront)
            {
                return;
            }

            if (input.Actor.IsUnbirthing)
            {
                return;
            }

            if (input.Actor.IsAnalVoring)
            {
                output.Sprite(input.Sprites.LizardsBooty[9]);
            }
        });

        builder.RenderSingle(SpriteType.Weapon, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Params.FacingFront == false)
            {
                return;
            }

            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                if (input.Actor.GetWeaponSprite() == 7)
                {
                    return;
                }

                output.Sprite(input.Sprites.Lizards[46 + input.Actor.GetWeaponSprite()]);
            }
        });

        // BackWeapon has no Default behavior
        // builder.SetRenderSingle(SpriteType.BackWeapon, 0, (input, output) =>
        // {
        //     output.Color(Defaults.WhiteColored).Palette(null);
        //     Defaults.SpriteGens2[SpriteType.BackWeapon].Invoke(input, output);
        // });


        builder.RunBefore((input, output) =>
        {
            bool facingFront;
            if (input.Actor.IsAnalVoring || input.Actor.IsUnbirthing)
            {
                facingFront = false;
            }
            else if (input.Actor.Unit.TailType == 0 || input.Actor.IsOralVoring || input.Actor.IsAttacking || input.Actor.IsCockVoring || input.Actor.IsBreastVoring)
            {
                facingFront = true;
            }
            else
            {
                facingFront = true;
            }

            output.Params.FacingFront = facingFront;

            if (facingFront)
            {
                if (input.Actor.HasBelly)
                {
                    Vector3 localScale;

                    if (input.Actor.PredatorComponent.VisibleFullness > 4)
                    {
                        float extraCap = input.Actor.PredatorComponent.VisibleFullness - 4;
                        float xScale = Mathf.Min(1 + extraCap / 5, 1.8f);
                        float yScale = Mathf.Min(1 + extraCap / 40, 1.1f);
                        localScale = new Vector3(xScale, yScale, 1);
                    }
                    else
                    {
                        localScale = new Vector3(1, 1, 1);
                    }

                    output.changeSprite(SpriteType.Belly).SetActive(true).SetLocalScale(localScale);
                }
            }
            else
            {
                if (input.Actor.HasBelly)
                {
                    Vector3 localScale;
                    if (input.Actor.PredatorComponent.VisibleFullness > 4)
                    {
                        float extraCap = input.Actor.PredatorComponent.VisibleFullness - 4;
                        float xScale = Mathf.Min(1 + extraCap / 5, 1.8f);
                        float yScale = Mathf.Min(1 + extraCap / 40, 1.1f);
                        localScale = new Vector3(xScale, yScale, 1);
                    }
                    else
                    {
                        localScale = new Vector3(1, 1, 1);
                    }

                    output.changeSprite(SpriteType.Belly).SetActive(true).SetLocalScale(localScale);
                }
            }
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            if (unit.Type == UnitType.Leader)
            {
                unit.ClothingHatType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedClothingHatTypes, RaceSpecificClothing.LizardLeaderCrownInstance);
                unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypes, RaceSpecificClothing.LizardLeaderTopInstance);
                unit.ClothingType2 = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedWaistTypes, RaceSpecificClothing.LizardLeaderSkirtInstance);
                unit.ClothingExtraType1 = 1 + Extensions.IndexOf(data.MiscRaceData.ExtraMainClothing1Types, RaceSpecificClothing.LizardLeaderLegguardsInstance);
                unit.ClothingExtraType2 = 1 + Extensions.IndexOf(data.MiscRaceData.ExtraMainClothing2Types, RaceSpecificClothing.LizardLeaderArmbandsInstance);
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