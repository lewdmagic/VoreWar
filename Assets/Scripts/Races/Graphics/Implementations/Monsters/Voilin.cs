#region

using System.Collections.Generic;

#endregion

internal static class Voilin
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.EyeTypes = 3;
            output.SkinColors = ColorMap.ExoticColorCount; // Under belly, head
            output.EyeColors = ColorMap.EyeColorCount; // Eye & Spine Colour
            output.ExtraColors1 = ColorMap.ExoticColorCount; // Plates
            output.ExtraColors2 = ColorMap.ExoticColorCount; // Limbs
            output.CanBeGender = new List<Gender> { Gender.None };
            output.GentleAnimation = true;
        });


        builder.RenderSingle(SpriteType.Eyes, 6, (input, output) =>
        {
            output.Coloring(ColorMap.GetEyeColor(input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Voilin[9 + input.Actor.Unit.EyeType]);
        }); // Eyes, Spines

        builder.RenderSingle(SpriteType.Hair, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Voilin[13]);
                return;
            }

            output.Sprite(input.Sprites.Voilin[12]);
        }); // Teeth

        builder.RenderSingle(SpriteType.Body, 0, (input, output) =>
        {
            output.Coloring(ColorMap.GetExoticColor(input.Actor.Unit.ExtraColor2));
            int size = input.Actor.GetStomachSize(23);

            if (size > 11)
            {
                output.Sprite(input.Sprites.Voilin[6]);
                return;
            }

            if (size > 8)
            {
                output.Sprite(input.Sprites.Voilin[3]);
                return;
            }

            output.Sprite(input.Sprites.Voilin[0]);
        }); // Base Body

        builder.RenderSingle(SpriteType.BodyAccent, 2, (input, output) =>
        {
            output.Coloring(ColorMap.GetExoticColor(input.Actor.Unit.ExtraColor2));
            int size = input.Actor.GetStomachSize(23);

            if (size > 11)
            {
                if (input.Actor.IsAttacking || input.Actor.IsEating)
                {
                    output.Sprite(input.Sprites.Voilin[8]);
                    return;
                }

                output.Sprite(input.Sprites.Voilin[7]);
                return;
            }

            if (size > 8)
            {
                if (input.Actor.IsAttacking || input.Actor.IsEating)
                {
                    output.Sprite(input.Sprites.Voilin[5]);
                    return;
                }

                output.Sprite(input.Sprites.Voilin[4]);
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Voilin[2]);
                return;
            }

            output.Sprite(input.Sprites.Voilin[1]);
        }); // Closer Legs, Head

        builder.RenderSingle(SpriteType.BodyAccent2, 3, (input, output) =>
        {
            output.Coloring(ColorMap.GetExoticColor(input.Actor.Unit.ExtraColor1));
            output.Sprite(input.Sprites.Voilin[14]);
        }); // Back Plates

        builder.RenderSingle(SpriteType.BodyAccent3, 4, (input, output) =>
        {
            output.Coloring(ColorMap.GetExoticColor(input.Actor.Unit.SkinColor));
            int size = input.Actor.GetStomachSize(23);

            if (size > 11)
            {
                if (input.Actor.IsAttacking || input.Actor.IsEating)
                {
                    output.Sprite(input.Sprites.Voilin[21]);
                    return;
                }

                output.Sprite(input.Sprites.Voilin[20]);
                return;
            }

            if (size > 8)
            {
                if (input.Actor.IsAttacking || input.Actor.IsEating)
                {
                    output.Sprite(input.Sprites.Voilin[19]);
                    return;
                }

                output.Sprite(input.Sprites.Voilin[18]);
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Voilin[16]);
                return;
            }

            output.Sprite(input.Sprites.Voilin[15]);
        }); // Below Head

        builder.RenderSingle(SpriteType.Belly, 1, (input, output) =>
        {
            output.Coloring(ColorMap.GetExoticColor(input.Actor.Unit.SkinColor));
            int size = input.Actor.GetStomachSize(23);

            if (size == 0)
            {
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false) && size >= 22)
            {
                output.Sprite(input.Sprites.Voilin[40]);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false) && size >= 20)
            {
                output.Sprite(input.Sprites.Voilin[39]);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false) && size >= 18)
            {
                output.Sprite(input.Sprites.Voilin[38]);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false) && size >= 16)
            {
                output.Sprite(input.Sprites.Voilin[37]);
                return;
            }

            if (size > 15)
            {
                size = 15;
            }

            output.Sprite(input.Sprites.Voilin[21 + size]);
        }); // Belly       


        builder.RunBefore((input, output) =>
        {
            int size = input.Actor.GetStomachSize(23);
            if (size > 14)
            {
                output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, (29 + (size - 15) * 2) * .625f);
                output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, (12 + (size - 15) * 2) * .625f);
                output.changeSprite(SpriteType.Hair).AddOffset(0, (29 + (size - 15) * 2) * .625f);
                output.changeSprite(SpriteType.Eyes).AddOffset(0, (29 + (size - 15) * 2) * .625f);
                output.changeSprite(SpriteType.Body).AddOffset(0, (12 + (size - 15) * 2) * .625f);
                output.changeSprite(SpriteType.BodyAccent).AddOffset(0, (12 + (size - 15) * 2) * .625f);
            }

            else if (size > 11)
            {
                output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, (17 + (size - 12) * 4) * .625f);
                output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, (size - 12) * 4 * .625f);
                output.changeSprite(SpriteType.Hair).AddOffset(0, (17 + (size - 12) * 4) * .625f);
                output.changeSprite(SpriteType.Eyes).AddOffset(0, (17 + (size - 12) * 4) * .625f);
                output.changeSprite(SpriteType.Body).AddOffset(0, (size - 12) * 4 * .625f);
                output.changeSprite(SpriteType.BodyAccent).AddOffset(0, (size - 12) * 4 * .625f);
            }

            else if (size > 8)
            {
                output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 0 * .625f);
                output.changeSprite(SpriteType.Hair).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.Eyes).AddOffset(0, 10 * .625f);
                output.changeSprite(SpriteType.Body).AddOffset(0, 0 * .625f);
                output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 0 * .625f);
            }

            else
            {
                output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, 0 * .625f);
                output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, 0 * .625f);
                output.changeSprite(SpriteType.Hair).AddOffset(0, 0 * .625f);
                output.changeSprite(SpriteType.Eyes).AddOffset(0, 0 * .625f);
                output.changeSprite(SpriteType.Body).AddOffset(0, 0 * .625f);
                output.changeSprite(SpriteType.BodyAccent).AddOffset(0, 0 * .625f);
            }
        });
        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.SkinColor = State.Rand.Next(0, data.MiscRaceData.SkinColors);
            unit.EyeColor = State.Rand.Next(0, data.MiscRaceData.EyeColors);
            unit.ExtraColor1 = State.Rand.Next(0, data.MiscRaceData.ExtraColors1);
            unit.ExtraColor2 = State.Rand.Next(0, data.MiscRaceData.ExtraColors2);
        });
    });
}