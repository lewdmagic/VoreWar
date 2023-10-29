#region

using System.Collections.Generic;
using UnityEngine;

#endregion

internal static class Vagrants
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank<VargantParameters>, builder =>
    {
        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.None };
            output.SkinColors = 3;
        });


        builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Params.Sprites[28]);
                return;
            }

            output.Sprite(input.Params.Sprites[5]);
        });

        builder.RenderSingle(SpriteType.BodyAccessory, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsEating)
            {
                output.Sprite(input.Params.Sprites[3]);
                return;
            }

            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Params.Sprites[4]);
                return;
            }

            output.Sprite(input.Params.Sprites[2]);
        }); // tentacles

        builder.RenderSingle(SpriteType.SecondaryAccessory, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Params.Sprites[1]);
                return;
            }

            output.Sprite(input.Params.Sprites[0]);
        }); // underneath

        builder.RenderSingle(SpriteType.Belly, 2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.HasBelly)
            {
                if (input.Actor.IsAttacking || input.Actor.IsEating)
                {
                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
                    {
                        output.Sprite(input.Params.Sprites[50]);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
                    {
                        if (input.Actor.GetStomachSize(16, .60f) == 16)
                        {
                            output.Sprite(input.Params.Sprites[49]);
                            return;
                        }

                        if (input.Actor.GetStomachSize(16, .70f) == 16)
                        {
                            output.Sprite(input.Params.Sprites[48]);
                            return;
                        }

                        if (input.Actor.GetStomachSize(16, .80f) == 16)
                        {
                            output.Sprite(input.Params.Sprites[47]);
                            return;
                        }

                        if (input.Actor.GetStomachSize(16, .90f) == 16)
                        {
                            output.Sprite(input.Params.Sprites[46]);
                            return;
                        }
                    }

                    output.Sprite(input.Params.Sprites[29 + input.Actor.GetStomachSize(16)]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
                {
                    output.Sprite(input.Params.Sprites[27]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
                {
                    if (input.Actor.GetStomachSize(16, .60f) == 16)
                    {
                        output.Sprite(input.Params.Sprites[26]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(16, .70f) == 16)
                    {
                        output.Sprite(input.Params.Sprites[25]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(16, .80f) == 16)
                    {
                        output.Sprite(input.Params.Sprites[24]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(16, .90f) == 16)
                    {
                        output.Sprite(input.Params.Sprites[23]);
                        return;
                    }
                }

                output.Sprite(input.Params.Sprites[6 + input.Actor.GetStomachSize(16)]);
            }
        });


        builder.RunBefore((input, output) =>
        {
            Sprite[][] VagrantSprites =
            {
                State.GameManager.SpriteDictionary.Vagrants,
                State.GameManager.SpriteDictionary.Vagrants2,
                State.GameManager.SpriteDictionary.Vagrants3
            };

            output.Params.Sprites = VagrantSprites[Mathf.Clamp(input.Actor.Unit.SkinColor, 0, 2)];

            output.changeSprite(SpriteType.Body).AddOffset(0, 60 * .625f);
            output.changeSprite(SpriteType.BodyAccessory).AddOffset(0, 60 * .625f);
            output.changeSprite(SpriteType.SecondaryAccessory).AddOffset(0, 60 * .625f);
            output.changeSprite(SpriteType.Belly).AddOffset(0, 60 * .625f);
        });

        builder.RandomCustom(Defaults.RandomCustom);
    });

    private class VargantParameters : IParameters
    {
        internal Sprite[] Sprites;
    }
}