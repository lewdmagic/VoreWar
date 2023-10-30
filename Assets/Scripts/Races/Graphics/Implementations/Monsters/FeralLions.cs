#region

using System.Collections.Generic;

#endregion

internal static class FeralLions
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank<HindViewParameters>, builder =>
    {
        RaceFrameList frameListRumpVore = new RaceFrameList(new int[2] { 0, 1 }, new float[2] { .75f, .5f });


        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.Male, Gender.Female, Gender.Hermaphrodite, Gender.Maleherm };
            output.HairStyles = 10; // Manes
            output.GentleAnimation = true;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.FeralLionsFur);
            output.EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.FeralLionsEyes);
            output.HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.FeralLionsMane);
        });


        builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, input.Actor.Unit.SkinColor));
            if (input.Params.HindView)
            {
                output.Sprite(input.Sprites.FeralLions[48]);
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.FeralLions[26]);
                return;
            }

            output.Sprite(input.Sprites.FeralLions[27]);
        });

        builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsEyes, input.Actor.Unit.EyeColor));
            if (input.Actor.IsOralVoring || input.Actor.IsAttacking || input.Params.HindView || input.Actor.IsAbsorbing || input.Actor.IsDigesting || input.Actor.HasJustVored || input.Actor.IsSuckling || input.Actor.IsBeingSuckled || input.Actor.IsBeingRubbed)
            {
                return;
            }

            output.Sprite(input.Sprites.FeralLions[37]);
        });

        builder.RenderSingle(SpriteType.Mouth, 13, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(!input.Actor.IsAttacking && !input.Actor.IsOralVoring ? null : input.Sprites.FeralLions[91]);
        }); // Maw for vore and attack
        builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, input.Actor.Unit.SkinColor));
            if (input.Params.HindView)
            {
                output.Sprite(input.Sprites.FeralLions[59]).Layer(15);
                return;
            }

            output.Sprite(input.Sprites.FeralLions[0]).Layer(3);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, input.Actor.Unit.SkinColor));
            if (input.Params.HindView)
            {
                output.Sprite(input.Sprites.FeralLions[49]).Layer(1);
            }
            else
            {
                output.Sprite(input.Sprites.FeralLions[11]).Layer(1);
            }
        }); // Foreground legs / Background legs during hind view

        builder.RenderSingle(SpriteType.BodyAccent2, 10, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, input.Actor.Unit.SkinColor));
            if (input.Actor.IsOralVoring || input.Actor.IsAttacking || input.Params.HindView)
            {
                return;
            }

            if (input.Actor.Targetable == false && input.Actor.Visible && input.Actor.Surrendered)
            {
                output.Sprite(input.Sprites.FeralLions[41]);
                return;
            }

            if (input.Actor.IsDigesting || input.Actor.IsAbsorbing || input.Actor.IsBeingSuckled)
            {
                output.Sprite(input.Sprites.FeralLions[40]);
                return;
            }

            if (input.Actor.HasJustVored || input.Actor.IsSuckling || input.Actor.IsBeingRubbed)
            {
                output.Sprite(input.Sprites.FeralLions[39]);
                return;
            }

            output.Sprite(input.Sprites.FeralLions[38]);
        }); // facial expression

        builder.RenderSingle(SpriteType.BodyAccent3, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsMane, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.HairStyle == 0)
            {
                return;
            }

            if (input.Params.HindView)
            {
                output.Sprite(input.Sprites.FeralLions[50 + input.Actor.Unit.HairStyle - 1]);
                return;
            }

            output.Sprite(input.Sprites.FeralLions[28 + input.Actor.Unit.HairStyle - 1]);
        }); // Mane

        builder.RenderSingle(SpriteType.BodyAccent4, 11, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsMane, input.Actor.Unit.HairColor));
            if (input.Params.HindView)
            {
                return;
            }

            output.Sprite(input.Actor.Unit.HairStyle == 0 ? null : input.Actor.IsAttacking || input.Actor.IsOralVoring || input.Actor.IsAbsorbing || input.Actor.DamagedColors ? input.Sprites.FeralLions[45] : input.Sprites.FeralLions[44]);
        }); // Mane over ears

        builder.RenderSingle(SpriteType.BodyAccent5, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsMane, input.Actor.Unit.HairColor));
            output.Sprite(input.Params.HindView ? input.Sprites.FeralLions[90] : input.Sprites.FeralLions[12]);
        }); // Tail tip
        builder.RenderSingle(SpriteType.BodyAccent6, 13, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, input.Actor.Unit.SkinColor));
            output.Sprite(!input.Actor.IsAttacking && !input.Actor.IsOralVoring ? null : input.Sprites.FeralLions[46]);
        }); // The Maw parts that are fur-colored
        builder.RenderSingle(SpriteType.BodyAccent7, 17, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, input.Actor.Unit.SkinColor));
            if (!input.Actor.Unit.HasVagina)
            {
                return;
            }

            output.Sprite(input.Params.HindView ? input.Sprites.FeralLions[88] : null);
        }); // Pussy

        builder.RenderSingle(SpriteType.BodyAccessory, 10, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, input.Actor.Unit.SkinColor));
            if (!input.Params.HindView)
            {
                if (input.Actor.IsAttacking || input.Actor.IsOralVoring || input.Actor.IsAbsorbing ||
                    input.Actor.IsBeingSuckled || input.Actor.DamagedColors)
                {
                    output.Sprite(input.Sprites.FeralLions[42]).Layer(10);
                    return;
                }

                output.Sprite(input.Sprites.FeralLions[43]).Layer(10);
                return;
            }

            output.Sprite(input.Sprites.FeralLions[47]).Layer(1);
        }); // Ears

        builder.RenderSingle(SpriteType.SecondaryAccessory, 18, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, input.Actor.Unit.SkinColor));
            if (!input.Actor.IsAnalVoring && !input.Actor.IsUnbirthing)
            {
                return;
            }

            output.Sprite(input.Actor.IsAnalVoring ? input.Sprites.FeralLions[87] : input.Sprites.FeralLions[89]);
        }); // AV and UB

        builder.RenderSingle(SpriteType.Belly, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            output.Layer(input.Params.HindView ? 14 : 4);
            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
            {
                output.Sprite(input.Params.HindView ? input.Sprites.FeralLions[75] : input.Sprites.FeralLions[10]);
                return;
            }

            output.Sprite(input.Params.HindView ? input.Sprites.FeralLions[60 + input.Actor.GetStomachSize(13)] : input.Sprites.FeralLions[1 + input.Actor.GetStomachSize(8)]);
        });

        builder.RenderSingle(SpriteType.Dick, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, input.Actor.Unit.SkinColor));
            //if ((input.State.HindView ? input.Actor.GetBallSize(8) : input.Actor.GetBallSize(9)) > 2)
            output.Layer(input.Params.HindView ? 15 : 6);
            //else 
            //Dick.layer = input.State.HindView ? 19 : 6;
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect())
            {
                output.Sprite(input.Params.HindView ? input.Sprites.FeralLions[76] : input.Sprites.FeralLions[13]);
            }
        });

        builder.RenderSingle(SpriteType.Balls, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralLionsFur, input.Actor.Unit.SkinColor));
            output.Layer(input.Params.HindView ? 18 : 7);
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                {
                    output.Sprite(input.Params.HindView ? input.Sprites.FeralLions[86] : input.Sprites.FeralLions[25]);
                    return;
                }

                output.Sprite(input.Params.HindView ? input.Sprites.FeralLions[78 + input.Actor.GetBallSize(7)] : input.Sprites.FeralLions[15 + input.Actor.GetBallSize(9)]);
                return;
            }

            output.Sprite(input.Params.HindView ? input.Sprites.FeralLions[77] : input.Sprites.FeralLions[14]);
        });

        builder.RunBefore((input, output) =>
        {
            if (input.Actor.IsAnalVoring || input.Actor.IsUnbirthing || input.Actor.IsCockVoring)
            {
                output.Params.HindView = true;
            }
            else
            {
                output.Params.HindView = false;
            }

            Defaults.Finalize.Invoke(input, output);
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;


            if (unit.GetGender() == Gender.Female || (unit.GetGender() == Gender.Hermaphrodite && Config.HermsOnlyUseFemaleHair))
            {
                unit.HairStyle = 0;
            }
            else
            {
                unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
            }
        });
    });

    private class HindViewParameters : IParameters
    {
        internal bool HindView;
    }
}