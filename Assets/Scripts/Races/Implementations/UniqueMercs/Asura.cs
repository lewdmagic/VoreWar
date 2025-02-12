﻿#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.UniqueMercs
{
    internal static class Asura
    {
        private static readonly RaceFrameList[] FrameList =
        {
            new RaceFrameList(new int[7] { 74, 75, 76, 77, 78, 79, 80 }, new float[7] { .15f, .15f, .15f, .15f, .15f, .15f, .15f }),
            new RaceFrameList(new int[7] { 81, 82, 83, 84, 85, 86, 87 }, new float[7] { .15f, .15f, .15f, .15f, .15f, .15f, .15f }),
            new RaceFrameList(new int[7] { 88, 89, 90, 91, 92, 93, 94 }, new float[7] { .15f, .15f, .15f, .15f, .15f, .15f, .15f }),
            new RaceFrameList(new int[7] { 95, 96, 97, 98, 99, 100, 101 }, new float[7] { .15f, .15f, .15f, .15f, .15f, .15f, .15f })
        };

        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup((input, output) =>
            {
                output.Names("Asura", "Asura");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 18,
                    StomachSize = 30,
                    HasTail = true,
                    FavoredStat = Stat.Strength,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.BreastVore, VoreType.Anal },
                    ExpMultiplier = 2.4f,
                    PowerAdjustment = 3f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new StatRange(20, 24),
                        Dexterity = new StatRange(6, 10),
                        Endurance = new StatRange(16, 20),
                        Mind = new StatRange(8, 12),
                        Will = new StatRange(12, 16),
                        Agility = new StatRange(10, 16),
                        Voracity = new StatRange(18, 24),
                        Stomach = new StatRange(8, 12),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Maul,
                        TraitType.Frenzy,
                        TraitType.ShunGokuSatsu
                    },
                    CanUseRangedWeapons = false,
                });
                output.CustomizeButtons((unit, buttons) => { buttons.SetText(ButtonType.ClothingAccessoryType, "Mask"); });
                IClothing outfit = BaseOutfit.BaseOutfitInstance;
                IClothing horns = ReindeerHorns.ReindeerHornsInstance;
                output.BreastSizes = () => 1;
                output.CanBeGender = new List<Gender> { Gender.Female };
                output.ClothingColors = 1;
                output.AllowedMainClothingTypes.Set(outfit);
                output.AllowedClothingHatTypes.Set(horns);
            });

            builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Asura[11]);
                    return;
                }

                output.Sprite(input.Sprites.Asura[9]);
            });

            builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.AnimationController.FrameLists == null)
                {
                    SetUpAnimations(input.Actor);
                }

                ProcessAnimation(input, output, 0);
            });

            builder.RenderSingle(SpriteType.Hair2, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                ProcessAnimation(input, output, 1);
            });
            builder.RenderSingle(SpriteType.Hair3, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                ProcessAnimation(input, output, 2);
            });
            builder.RenderSingle(SpriteType.Beard, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                ProcessAnimation(input, output, 3);
            });
            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking && input.U.ClothingType != 0)
                {
                    output.Sprite(input.Sprites.Asura[2]);
                    return;
                }

                if (input.A.IsAttacking && input.U.ClothingType == 0)
                {
                    output.Sprite(input.Sprites.Asura[1]);
                    return;
                }

                output.Sprite(input.Sprites.Asura[0]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Asura[19]);
            });
            builder.RenderSingle(SpriteType.BodyAccent2, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (Config.FurryHandsAndFeet && input.U.ClothingType == 0)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Asura[21]);
                        return;
                    }

                    output.Sprite(input.Sprites.Asura[20]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent3, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (Config.FurryHandsAndFeet)
                {
                    if (input.U.ClothingType == 0)
                    {
                        output.Sprite(input.Sprites.Asura[22]);
                        return;
                    }

                    output.Sprite(input.Sprites.Asura[27]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent4, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (Config.FurryHandsAndFeet && input.U.ClothingType == 0)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Asura[29]);
                        return;
                    }

                    output.Sprite(input.Sprites.Asura[28]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent5, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (Config.FurryHandsAndFeet)
                {
                    if (input.U.ClothingType == 0)
                    {
                        output.Sprite(input.Sprites.Asura[30]);
                    }
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent6, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Asura[18]);
            });
            builder.RenderSingle(SpriteType.BodyAccent7, 8, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (Config.FurryHandsAndFeet && input.U.ClothingType == 0)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Asura[34]);
                        return;
                    }

                    output.Sprite(input.Sprites.Asura[33]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent8, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (Config.FurryHandsAndFeet)
                {
                    output.Sprite(input.Sprites.Asura[35]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent9, 9, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (Config.FurryHandsAndFeet && input.U.ClothingType == 0)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Asura[42]);
                        return;
                    }

                    output.Sprite(input.Sprites.Asura[41]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent10, 9, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (Config.FurryHandsAndFeet)
                {
                    output.Sprite(input.Sprites.Asura[43]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccessory, 16, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Asura[5]);
            });
            builder.RenderSingle(SpriteType.SecondaryAccessory, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking && input.U.ClothingType != 0)
                {
                    output.Sprite(input.Sprites.Asura[37]);
                }
            });

            builder.RenderSingle(SpriteType.Breasts, 10, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.ClothingType == 0)
                {
                    output.Sprite(input.Sprites.Asura[6]);
                    return;
                }

                output.Sprite(input.Sprites.Asura[7]);
            });

            builder.RenderSingle(SpriteType.BreastShadow, 16, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.TurnUsedShun > 0 && input.A.TurnUsedShun == State.GameManager.TacticalMode.CurrentTurn)
                {
                    output.Sprite(input.Sprites.Asura[Math.Max(59 + input.A.GetStomachSize(), 64)]);
                }
            });


            builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.HasBelly)
                {
                    output.Sprite(input.Sprites.Asura[48 + input.A.GetStomachSize()]);
                }
            });

            builder.RenderSingle(SpriteType.Weapon, 14, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking && input.U.ClothingType != 0)
                {
                    output.Sprite(input.Sprites.Asura[38]);
                }
            });


            builder.RunBefore((input, output) =>
            {
                output.ChangeSprite(SpriteType.Weapon).AddOffset(0, 74 * .625f);
                output.ChangeSprite(SpriteType.SecondaryAccessory).AddOffset(0, 59 * .625f);

                if (input.A.HasBelly)
                {
                    Vector3 localScale = new Vector3(1, 1, 1);
                    output.ChangeSprite(SpriteType.Belly).SetActive(true).SetLocalScale(localScale);
                }
            });

            builder.RandomCustom((data, output) =>   
            {
                Defaults.Randomize(data, output);
                IUnitRead unit = data.Unit;

                unit.Name = "Asura";
                unit.ClothingAccessoryType = State.Rand.Next(2);
            });
        });


        private static void SetUpAnimations(IActorUnit actor)
        {
            actor.AnimationController.FrameLists = new[]
                { new AnimationController.FrameList(), new AnimationController.FrameList(), new AnimationController.FrameList(), new AnimationController.FrameList() };
            actor.AnimationController.FrameLists[0].CurrentlyActive = true;
            actor.AnimationController.FrameLists[1].CurrentlyActive = true;
            actor.AnimationController.FrameLists[2].CurrentlyActive = true;
            actor.AnimationController.FrameLists[3].CurrentlyActive = true;
        }

        private static void ProcessAnimation(IRaceRenderInput input, IRaceRenderOutput output, int list)
        {
            if (input.U.ClothingType == 0)
            {
                output.Sprite(null);
                return;
            }

            if (State.Rand.Next(120) == 0)
            {
                input.A.AnimationController.FrameLists[list].CurrentlyActive = true;
            }

            if (input.A.AnimationController.FrameLists[list].CurrentlyActive == false)
            {
                output.Sprite(null);
                return;
            }

            if (input.A.AnimationController.FrameLists[list].CurrentTime >= FrameList[list].Times[input.A.AnimationController.FrameLists[list].CurrentFrame])
            {
                input.A.AnimationController.FrameLists[list].CurrentFrame++;
                input.A.AnimationController.FrameLists[list].CurrentTime = 0f;

                if (input.A.AnimationController.FrameLists[list].CurrentFrame >= FrameList[list].Frames.Length)
                {
                    input.A.AnimationController.FrameLists[list].CurrentFrame = 0;
                    input.A.AnimationController.FrameLists[list].CurrentlyActive = false;
                    output.Sprite(null);
                    return;
                }
            }

            output.Sprite(input.Sprites.Asura[FrameList[list].Frames[input.A.AnimationController.FrameLists[list].CurrentFrame]]);
        }


        private static class BaseOutfit
        {
            internal static readonly IClothing BaseOutfitInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.OccupiesAllSlots = true;
                    output.RevealsDick = true;
                    output.RevealsBreasts = true;

                    output.DiscardSprite = input.Sprites.Asura[39];
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing5"].Layer(0);
                    output["Clothing4"].Layer(0);
                    output["Clothing3"].Layer(12);
                    output["Clothing2"].Layer(12);
                    output["Clothing1"].Layer(12);
                    float timeDuplicate = 0f;
                    float time = 0f;
                    output["Clothing1"].Sprite(input.Sprites.Asura[input.A.IsAttacking ? 4 : 3]);
                    output["Clothing2"].Sprite(input.Sprites.Asura[input.A.HasBelly ? 32 : 26]);
                    output["Clothing3"].Sprite(input.Sprites.Asura[8]);
                    output["Clothing4"].Sprite(input.Sprites.Asura[input.A.IsAttacking ? 25 : 24]);
                    if (!Mathf.Approximately(timeDuplicate, Time.time))
                    {
                        time -= Time.deltaTime;
                    }

                    if (time <= 0)
                    {
                        time = 1 + (float)State.Rand.NextDouble();
                    }

                    if (time > .45f)
                    {
                        output["Clothing5"].Sprite(null);
                    }

                    if (time > .3f)
                    {
                        output["Clothing5"].Sprite(input.Sprites.Asura[45]);
                    }

                    output["Clothing5"].Sprite(time > .15f ? input.Sprites.Asura[46] : input.Sprites.Asura[47]);
                });
            });
        }

        private static class ReindeerHorns
        {
            internal static readonly IClothing ReindeerHornsInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { output.ReqWinterHoliday = true; });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Sprite(input.U.ClothingType == 0 ? input.Sprites.AsuraHoliday[0] : input.Sprites.AsuraHoliday[1]);
                });
            });
        }
    }
}