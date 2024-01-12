#region

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Races.Graphics.Implementations.MainRaces
{

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

        internal static readonly IRaceData Instance = RaceBuilder.CreateV2(Defaults.Default<SeliciaParameters>, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Lamia", "Lamia");
                output.WallType(WallType.Lamia);
                output.FlavorText(new FlavorText(
                    new Texts { "scaly", "noodly", "double-tasty" },
                    new Texts { "scaly", "long bodied", "sizeable" },
                    new Texts { "lamia", "serpent", "half-snake" }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 15,
                    StomachSize = 25,
                    HasTail = true,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.CockVore, VoreType.BreastVore, VoreType.Anal, VoreType.TailVore },
                    RacialTraits = new List<Traits>()
                    {
                        Traits.Ravenous,
                        Traits.Biter,
                        Traits.DualStomach
                    },
                    RaceDescription = "Natives to this realm, these legless beings were once the strongest and largest hunters of the land. The sudden emergence of many new species left the Lamia uncertain at first, but soon their dual stomachs won and they focused on testing the taste of the new arrivals.",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.BodyAccessoryColor, "Scale Color");
                    buttons.SetText(ButtonType.ExtraColor1, "Accent Color");
                    buttons.SetText(ButtonType.ExtraColor2, "Tail Pattern Color");
                });
                output.TownNames(new List<string>
                {
                    "City of Brass",
                    "Sthenopoli",
                    "Poena",
                    "Fields of Elysium",
                    "Cult of Bronze",
                    "Lair of Gorgon",
                    "Echidnadon",
                    "Nagapolis",
                    "Fountain of Woe"
                });
                output.EyeTypes = 3;
                output.BodySizes = 4;
                output.SpecialAccessoryCount = 2;
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.LizardMain);
                output.ExtraColors1 = ColorPaletteMap.GetPaletteCount(SwapType.LizardMain);
                output.ExtraColors2 = ColorPaletteMap.GetPaletteCount(SwapType.OldImpDark);

                output.ClothingShift = new Vector3(xOffset, yOffset, 0);
                output.AvoidedMainClothingTypes = 2;
                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.Clothing);
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
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
                int eatingOffset = input.A.IsEating ? 1 : 0;
                int genderOffset = input.U.HasBreasts ? 0 : 2;
                output.Sprite(input.Sprites.Lamia[18 + eatingOffset + genderOffset]);
            });

            builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
                output.Sprite(input.Sprites.Lamia[5 + input.U.EyeType]);
            });
            builder.RenderSingle(SpriteType.Mouth, 5, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Mouth].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
            });
            builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Hair].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.U.HairColor));
            });
            builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Hair2].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.U.HairColor));
            });
            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
                output.Sprite(input.Sprites.Scylla[24 + (input.A.IsAttacking ? 1 : 0) + 2 * input.U.BodySize + (input.U.HasBreasts ? 0 : 8)]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.ExtraColor1));

                output.Sprite(input.Sprites.Lamia[1]);
            });

            builder.RenderSingle(SpriteType.BodyAccent2, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.OldImpDark, input.U.ExtraColor2));

                int bonusCap = 0;
                if (input.U.Predator && input.A.PredatorComponent.TailFullness > 0)
                {
                    bonusCap = 1 + input.A.GetTailSize(2);
                }

                if (Config.LamiaUseTailAsSecondBelly && input.U.Predator)
                {
                    output.Sprite(input.Sprites.Lamia[Math.Min(bonusCap + (input.A.PredatorComponent?.Stomach2ndFullness > 0 ? 11 + input.A.GetStomach2Size(2) : 10), 13)]);
                    return;
                }

                output.Sprite(input.Sprites.Lamia[Math.Min(10 + input.U.BodySize + bonusCap, 13)]);
            });

            builder.RenderSingle(SpriteType.BodyAccent3, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.AccessoryColor));
                if (input.U.SpecialAccessoryType == 0)
                {
                    int eatingOffset = input.A.IsEating ? 1 : 0;
                    int genderOffset = input.U.HasBreasts ? 0 : 2;
                    output.Sprite(input.Sprites.Lamia[22 + eatingOffset + genderOffset]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent4, 5, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.BodyAccent4].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.U.HairColor));
            });
            builder.RenderSingle(SpriteType.BodyAccent5, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.AccessoryColor));
                int eatingOffset = input.A.IsEating ? 1 : 0;
                int genderOffset = input.U.HasBreasts ? 0 : 2;
                output.Sprite(input.Sprites.Lamia[26 + eatingOffset + genderOffset]);
            });

            builder.RenderSingle(SpriteType.BodyAccessory, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Lamia[0]);
            });
            builder.RenderSingle(SpriteType.SecondaryAccessory, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.AccessoryColor));
                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Lamia[9]);
                    return;
                }

                output.Sprite(input.Sprites.Lamia[8]);
            });

            builder.RenderSingle(SpriteType.BodySize, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.AccessoryColor));

                int bonusCap = 0;
                if (input.U.Predator && input.A.PredatorComponent.TailFullness > 0)
                {
                    bonusCap = 1 + input.A.GetTailSize(2);
                }

                if (Config.LamiaUseTailAsSecondBelly && input.U.Predator)
                {
                    if (input.A.PredatorComponent.Stomach2ndFullness > 0 || input.A.PredatorComponent.TailFullness > 0)
                    {
                        output.Sprite(input.Sprites.Lamia[Math.Min(2 + input.A.GetStomach2Size(2) + bonusCap, 4)]);
                        return;
                    }

                    output.Sprite(input.Sprites.Lamia[1]);
                }
                else
                {
                    int effectiveSize = Math.Min(input.U.BodySize + bonusCap, 3);
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
                Defaults.SpriteGens3[SpriteType.Breasts].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
            });
            builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));

                if (!Config.LamiaUseTailAsSecondBelly)
                {
                    if (input.A.HasBelly)
                    {
                        output.Sprite(input.Sprites.Bellies[input.A.GetCombinedStomachSize()]);
                        return;
                    }

                    return;
                }

                if (input.A.HasBelly)
                {
                    output.Sprite(input.Sprites.Bellies[input.A.GetStomachSize()]);
                }
            });

            builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect())
                {
                    if (input.A.HasBelly == false)
                    {
                        output.Sprite(input.Sprites.ErectDicks[input.U.DickSize]).Layer(18);
                        return;
                    }

                    output.Sprite(input.Sprites.Dicks[input.U.DickSize]).Layer(12);
                    return;
                }

                output.Sprite(input.Sprites.Dicks[input.U.DickSize]).Layer(9);
            });

            builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Balls].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
            });

            builder.RenderSingle(SpriteType.Weapon, 1, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Weapon].Invoke(input, output);
                output.Coloring(Defaults.WhiteColored);
            });

            // BackWeapon has no Default behavior
            // builder.SetRenderSingle(SpriteType.BackWeapon, 0, (input, output) =>
            // {
            //     output.Color(Defaults.WhiteColored).Palette(null);
            //     Defaults.SpriteGens3[SpriteType.BackWeapon].Invoke(input, output);
            // });


            builder.RunBefore((input, output) =>
            {
                bool Selicia;
                if (input.U.Predator == false)
                {
                    Selicia = false;
                }
                else
                {
                    Selicia = (input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach)
                               || input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.womb)
                               || input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach2))
                              && input.A.GetCombinedStomachSize() == 15;
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
                if (input.U.GetGender() != Gender.Male)
                {
                    output.ChangeSprite(SpriteType.SecondaryAccessory).AddOffset(0, -1 * .625f);
                }

                if (!Config.LamiaUseTailAsSecondBelly)
                {
                    if (input.A.HasBelly)
                    {
                        Vector3 localScale;
                        if (input.A.PredatorComponent.CombinedStomachFullness > 4)
                        {
                            float extraCap = input.A.PredatorComponent.CombinedStomachFullness - 4;
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
            });

            builder.RandomCustom(Defaults.RandomCustom);
        });
    }
}