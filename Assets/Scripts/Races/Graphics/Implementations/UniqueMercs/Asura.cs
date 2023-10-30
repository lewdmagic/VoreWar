#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

internal static class Asura
{
    private static readonly RaceFrameList[] frameList =
    {
        new RaceFrameList(new int[7] { 74, 75, 76, 77, 78, 79, 80 }, new float[7] { .15f, .15f, .15f, .15f, .15f, .15f, .15f }),
        new RaceFrameList(new int[7] { 81, 82, 83, 84, 85, 86, 87 }, new float[7] { .15f, .15f, .15f, .15f, .15f, .15f, .15f }),
        new RaceFrameList(new int[7] { 88, 89, 90, 91, 92, 93, 94 }, new float[7] { .15f, .15f, .15f, .15f, .15f, .15f, .15f }),
        new RaceFrameList(new int[7] { 95, 96, 97, 98, 99, 100, 101 }, new float[7] { .15f, .15f, .15f, .15f, .15f, .15f, .15f })
    };

    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
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
            if (input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Asura[11]);
                return;
            }

            output.Sprite(input.Sprites.Asura[9]);
        });

        builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.AnimationController.frameLists == null)
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
            if (input.Actor.IsAttacking && input.Actor.Unit.ClothingType != 0)
            {
                output.Sprite(input.Sprites.Asura[2]);
                return;
            }

            if (input.Actor.IsAttacking && input.Actor.Unit.ClothingType == 0)
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
            if (Config.FurryHandsAndFeet && input.Actor.Unit.ClothingType == 0)
            {
                if (input.Actor.IsAttacking)
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
                if (input.Actor.Unit.ClothingType == 0)
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
            if (Config.FurryHandsAndFeet && input.Actor.Unit.ClothingType == 0)
            {
                if (input.Actor.IsAttacking)
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
                if (input.Actor.Unit.ClothingType == 0)
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
            if (Config.FurryHandsAndFeet && input.Actor.Unit.ClothingType == 0)
            {
                if (input.Actor.IsAttacking)
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
            if (Config.FurryHandsAndFeet && input.Actor.Unit.ClothingType == 0)
            {
                if (input.Actor.IsAttacking)
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
            if (input.Actor.IsAttacking && input.Actor.Unit.ClothingType != 0)
            {
                output.Sprite(input.Sprites.Asura[37]);
            }
        });

        builder.RenderSingle(SpriteType.Breasts, 10, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.ClothingType == 0)
            {
                output.Sprite(input.Sprites.Asura[6]);
                return;
            }

            output.Sprite(input.Sprites.Asura[7]);
        });

        builder.RenderSingle(SpriteType.BreastShadow, 16, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.TurnUsedShun > 0 && input.Actor.TurnUsedShun == State.GameManager.TacticalMode.currentTurn)
            {
                output.Sprite(input.Sprites.Asura[Math.Max(59 + input.Actor.GetStomachSize(), 64)]);
            }
        });


        builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.HasBelly)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                {
                    output.Sprite(input.Sprites.Asura[104]).AddOffset(0, -24 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    if (input.Actor.GetStomachSize(15, .90f) == 15)
                    {
                        output.Sprite(input.Sprites.Asura[103]).AddOffset(0, -24 * .625f);
                        return;
                    }

                    if (input.Actor.GetStomachSize(15, 1.05f) == 15)
                    {
                        output.Sprite(input.Sprites.Asura[102]).AddOffset(0, -16 * .625f);
                        return;
                    }
                }

                output.Sprite(input.Sprites.Asura[48 + input.Actor.GetStomachSize()]);
            }
        });

        builder.RenderSingle(SpriteType.Weapon, 14, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking && input.Actor.Unit.ClothingType != 0)
            {
                output.Sprite(input.Sprites.Asura[38]);
            }
        });


        builder.RunBefore((input, output) =>
        {
            output.ChangeSprite(SpriteType.Weapon).AddOffset(0, 74 * .625f);
            output.ChangeSprite(SpriteType.SecondaryAccessory).AddOffset(0, 59 * .625f);

            if (input.Actor.HasBelly)
            {
                Vector3 localScale = new Vector3(1, 1, 1);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                {
                    localScale = new Vector3(1, 1, 1);
                }
                else if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    if (input.Actor.GetStomachSize(15, .90f) == 15)
                    {
                        localScale = new Vector3(1, 1, 1);
                    }
                    else if (input.Actor.GetStomachSize(15, 1.05f) == 15)
                    {
                        localScale = new Vector3(1, 1, 1);
                    }
                }

                output.ChangeSprite(SpriteType.Belly).SetActive(true).SetLocalScale(localScale);
            }
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.Name = "Asura";
            unit.ClothingAccessoryType = State.Rand.Next(2);
        });
    });


    private static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
            { new AnimationController.FrameList(), new AnimationController.FrameList(), new AnimationController.FrameList(), new AnimationController.FrameList() };
        actor.AnimationController.frameLists[0].currentlyActive = true;
        actor.AnimationController.frameLists[1].currentlyActive = true;
        actor.AnimationController.frameLists[2].currentlyActive = true;
        actor.AnimationController.frameLists[3].currentlyActive = true;
    }

    private static void ProcessAnimation(IRaceRenderInput input, IRaceRenderOutput output, int list)
    {
        if (input.Actor.Unit.ClothingType == 0)
        {
            output.Sprite(null);
            return;
        }

        if (State.Rand.Next(120) == 0)
        {
            input.Actor.AnimationController.frameLists[list].currentlyActive = true;
        }

        if (input.Actor.AnimationController.frameLists[list].currentlyActive == false)
        {
            output.Sprite(null);
            return;
        }

        if (input.Actor.AnimationController.frameLists[list].currentTime >= frameList[list].Times[input.Actor.AnimationController.frameLists[list].currentFrame])
        {
            input.Actor.AnimationController.frameLists[list].currentFrame++;
            input.Actor.AnimationController.frameLists[list].currentTime = 0f;

            if (input.Actor.AnimationController.frameLists[list].currentFrame >= frameList[list].Frames.Length)
            {
                input.Actor.AnimationController.frameLists[list].currentFrame = 0;
                input.Actor.AnimationController.frameLists[list].currentlyActive = false;
                output.Sprite(null);
                return;
            }
        }

        output.Sprite(input.Sprites.Asura[frameList[list].Frames[input.Actor.AnimationController.frameLists[list].currentFrame]]);
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
                output["Clothing1"].Sprite(input.Sprites.Asura[input.Actor.IsAttacking ? 4 : 3]);
                output["Clothing2"].Sprite(input.Sprites.Asura[input.Actor.HasBelly ? 32 : 26]);
                output["Clothing3"].Sprite(input.Sprites.Asura[8]);
                output["Clothing4"].Sprite(input.Sprites.Asura[input.Actor.IsAttacking ? 25 : 24]);
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
                output["Clothing1"].Sprite(input.Actor.Unit.ClothingType == 0 ? input.Sprites.AsuraHoliday[0] : input.Sprites.AsuraHoliday[1]);
            });
        });
    }
}