#region

using System;
using UnityEngine;

#endregion

internal interface ISeliciaParameters : IParameters
{
    bool Selicia { get; }
}

internal class SeliciaParameters : ISeliciaParameters
{
    public bool Selicia { get; set; }
}

internal static class Lamia
{
    private static readonly float xOffset = -1.875f; //3 pixels * 5/8
    private static readonly float yOffset = 3.75f;

    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Default<SeliciaParameters>, builder =>
    {
        builder.Setup(output =>
        {
            output.EyeTypes = 3;
            output.BodySizes = 4;
            output.SpecialAccessoryCount = 2;
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);
            output.ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);
            output.ExtraColors2 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.OldImpDark);

            output.ClothingShift = new Vector3(xOffset, yOffset, 0);
            output.AvoidedMainClothingTypes = 2;
            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing);
            output.AllowedMainClothingTypes.Set(
                ClothingTypes.BikiniTopInstance,
                ClothingTypes.BeltTopInstance,
                ClothingTypes.StrapTopInstance,
                ClothingTypes.LeotardInstance,
                ClothingTypes.BlackTopInstance,
                ClothingTypes.RagsInstance,
                RaceSpecificClothing.TogaInstance
            );
            output.AllowedWaistTypes.Set(
                ClothingTypes.BikiniBottomInstance,
                ClothingTypes.LoinclothInstance
            );
        });


        builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
            int eatingOffset = input.Actor.IsEating ? 1 : 0;
            int genderOffset = input.Actor.Unit.HasBreasts ? 0 : 2;
            output.Sprite(input.Sprites.Lamia[18 + eatingOffset + genderOffset]);
        });

        builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Lamia[5 + input.Actor.Unit.EyeType]);
        });
        builder.RenderSingle(SpriteType.Mouth, 5, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Mouth].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
        });
        builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Hair].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
        });
        builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Hair2].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
        });
        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Scylla[24 + (input.Actor.IsAttacking ? 1 : 0) + 2 * input.Actor.Unit.BodySize + (input.Actor.Unit.HasBreasts ? 0 : 8)]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.ExtraColor1));
            if (input.Params.Selicia)
            {
                output.Sprite(input.Sprites.Lamia[16]);
                return;
            }

            output.Sprite(input.Sprites.Lamia[1]);
        });

        builder.RenderSingle(SpriteType.BodyAccent2, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImpDark, input.Actor.Unit.ExtraColor2));
            if (input.Params.Selicia)
            {
                output.Sprite(input.Sprites.Lamia[17]);
                return;
            }

            int bonusCap = 0;
            if (input.Actor.Unit.Predator && input.Actor.PredatorComponent.TailFullness > 0)
            {
                bonusCap = 1 + input.Actor.GetTailSize(2);
            }

            if (Config.LamiaUseTailAsSecondBelly && input.Actor.Unit.Predator)
            {
                output.Sprite(input.Sprites.Lamia[Math.Min(bonusCap + (input.Actor.PredatorComponent?.Stomach2ndFullness > 0 ? 11 + input.Actor.GetStomach2Size(2) : 10), 13)]);
                return;
            }

            output.Sprite(input.Sprites.Lamia[Math.Min(10 + input.Actor.Unit.BodySize + bonusCap, 13)]);
        });

        builder.RenderSingle(SpriteType.BodyAccent3, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.SpecialAccessoryType == 0)
            {
                int eatingOffset = input.Actor.IsEating ? 1 : 0;
                int genderOffset = input.Actor.Unit.HasBreasts ? 0 : 2;
                output.Sprite(input.Sprites.Lamia[22 + eatingOffset + genderOffset]);
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent4, 5, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.BodyAccent4].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
        });
        builder.RenderSingle(SpriteType.BodyAccent5, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.AccessoryColor));
            int eatingOffset = input.Actor.IsEating ? 1 : 0;
            int genderOffset = input.Actor.Unit.HasBreasts ? 0 : 2;
            output.Sprite(input.Sprites.Lamia[26 + eatingOffset + genderOffset]);
        });

        builder.RenderSingle(SpriteType.BodyAccessory, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Lamia[0]);
        });
        builder.RenderSingle(SpriteType.SecondaryAccessory, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.AccessoryColor));
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Lamia[9]);
                return;
            }

            output.Sprite(input.Sprites.Lamia[8]);
        });

        builder.RenderSingle(SpriteType.BodySize, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.AccessoryColor));
            if (input.Params.Selicia)
            {
                output.Sprite(input.Sprites.Lamia[14]);
                return;
            }

            int bonusCap = 0;
            if (input.Actor.Unit.Predator && input.Actor.PredatorComponent.TailFullness > 0)
            {
                bonusCap = 1 + input.Actor.GetTailSize(2);
            }

            if (Config.LamiaUseTailAsSecondBelly && input.Actor.Unit.Predator)
            {
                if (input.Actor.PredatorComponent.Stomach2ndFullness > 0 || input.Actor.PredatorComponent.TailFullness > 0)
                {
                    output.Sprite(input.Sprites.Lamia[Math.Min(2 + input.Actor.GetStomach2Size(2) + bonusCap, 4)]);
                    return;
                }

                output.Sprite(input.Sprites.Lamia[1]);
            }
            else
            {
                int effectiveSize = Math.Min(input.Actor.Unit.BodySize + bonusCap, 3);
                if (effectiveSize == 0)
                {
                }
                else
                {
                    output.Sprite(input.Sprites.Lamia[1 + effectiveSize]);
                }
            }
        });

        builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Breasts].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
        });
        builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
            if (input.Params.Selicia)
            {
                output.Sprite(input.Sprites.Lamia[15]);
                return;
            }

            if (!Config.LamiaUseTailAsSecondBelly)
            {
                if (input.Actor.HasBelly)
                {
                    output.Sprite(input.Sprites.Bellies[input.Actor.GetCombinedStomachSize()]);
                    return;
                }

                return;
            }

            if (input.Actor.HasBelly)
            {
                output.Sprite(input.Sprites.Bellies[input.Actor.GetStomachSize()]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect())
            {
                if (input.Actor.HasBelly == false)
                {
                    output.Sprite(input.Sprites.ErectDicks[input.Actor.Unit.DickSize]).Layer(18);
                    return;
                }

                output.Sprite(input.Sprites.Dicks[input.Actor.Unit.DickSize]).Layer(12);
                return;
            }

            output.Sprite(input.Sprites.Dicks[input.Actor.Unit.DickSize]).Layer(9);
        });

        builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Balls].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
        });

        builder.RenderSingle(SpriteType.Weapon, 1, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Weapon].Invoke(input, output);
            output.Coloring(Defaults.WhiteColored);
        });

        // BackWeapon has no Default behavior
        // builder.SetRenderSingle(SpriteType.BackWeapon, 0, (input, output) =>
        // {
        //     output.Color(Defaults.WhiteColored).Palette(null);
        //     Defaults.SpriteGens2[SpriteType.BackWeapon].Invoke(input, output);
        // });


        builder.RunBefore((input, output) =>
        {
            bool Selicia;
            if (input.Actor.Unit.Predator == false)
            {
                Selicia = false;
            }
            else
            {
                Selicia = (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach)
                           || input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.womb)
                           || input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach2))
                          && input.Actor.GetCombinedStomachSize() == 15;
            }

            output.Params.Selicia = Selicia;

            output.ChangeSprite(SpriteType.Body).AddOffset(xOffset, yOffset);
            output.ChangeSprite(SpriteType.Head).AddOffset(xOffset, yOffset);
            output.ChangeSprite(SpriteType.Mouth).AddOffset(xOffset, yOffset);
            output.ChangeSprite(SpriteType.Hair).AddOffset(xOffset, yOffset);
            output.ChangeSprite(SpriteType.Hair2).AddOffset(xOffset, yOffset);
            output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(xOffset, yOffset);
            output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(xOffset, yOffset);
            output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(xOffset, yOffset);
            output.ChangeSprite(SpriteType.Weapon).AddOffset(xOffset + 1.25f, yOffset + 1.25f);
            output.ChangeSprite(SpriteType.BackWeapon).AddOffset(xOffset, yOffset);
            if (Selicia == false)
            {
                output.ChangeSprite(SpriteType.Belly).AddOffset(xOffset, yOffset);
            }

            output.ChangeSprite(SpriteType.Breasts).AddOffset(xOffset, yOffset);
            output.ChangeSprite(SpriteType.Dick).AddOffset(xOffset, yOffset + 2.5f);
            output.ChangeSprite(SpriteType.Balls).AddOffset(xOffset, yOffset + 2.5f);
            output.ChangeSprite(SpriteType.Eyes).AddOffset(0, -1 * .625f);
            if (input.Actor.Unit.GetGender() != Gender.Male)
            {
                output.ChangeSprite(SpriteType.SecondaryAccessory).AddOffset(0, -1 * .625f);
            }

            if (!Config.LamiaUseTailAsSecondBelly)
            {
                if (input.Actor.HasBelly)
                {
                    Vector3 localScale;
                    if (input.Actor.PredatorComponent.CombinedStomachFullness > 4)
                    {
                        float extraCap = input.Actor.PredatorComponent.CombinedStomachFullness - 4;
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

                output.ChangeSprite(SpriteType.Belly).SetActive(true).SetLocalScale(localScale);
            }
        });

        builder.RandomCustom(Defaults.RandomCustom);
    });
}