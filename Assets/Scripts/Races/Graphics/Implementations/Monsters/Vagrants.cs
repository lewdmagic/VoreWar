#region

using System.Collections.Generic;
using UnityEngine;

#endregion

internal static class Vagrants
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank<VargantParameters>, builder =>
    {
        builder.Names("Vagrant", "Vagrants");
        builder.BonesInfo(null);
        builder.FlavorText(new FlavorText(
            new Texts { "tentacled", "rubbery", "alien" },
            new Texts { "alien", "stretchy", "translucent" },
            new Texts { "vagrant", "jellyfish", "medusa" },
            "Stinger"
        ));
        builder.RaceTraits(new RaceTraits()
        {
            BodySize = 8,
            StomachSize = 13,
            HasTail = false,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.25f,
            PowerAdjustment = 1f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(10, 26),
                Dexterity = new RaceStats.StatRange(6, 14),
                Endurance = new RaceStats.StatRange(12, 20),
                Mind = new RaceStats.StatRange(10, 22),
                Will = new RaceStats.StatRange(4, 12),
                Agility = new RaceStats.StatRange(8, 20),
                Voracity = new RaceStats.StatRange(10, 18),
                Stomach = new RaceStats.StatRange(20, 30),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.DoubleAttack,
                Traits.Paralyzer
            },
            RaceDescription = "It is a matter of argument whether these beings emerged from the ocean or fell from the skies, or are even a mix of both, but they are among the first and oldest native threats the people who settled this realm faced. Their many tentacles paralyze those they touch and their rubbery bodies easily expand to engulf their prey.",
        });
        builder.IndividualNames(new List<string>
        {
            "Tenty",
            "Stingy",
            "Rubby",
            "Gulfy",
            "Weedy",
            "Squishy",
            "Waddle",
            "Waddly",
            "Domy",
        });
        
        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.None };
            output.SkinColors = 3;
        });


        builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.A.IsAttacking || input.A.IsEating)
            {
                output.Sprite(input.Params.Sprites[28]);
                return;
            }

            output.Sprite(input.Params.Sprites[5]);
        });

        builder.RenderSingle(SpriteType.BodyAccessory, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.A.IsEating)
            {
                output.Sprite(input.Params.Sprites[3]);
                return;
            }

            if (input.A.IsAttacking)
            {
                output.Sprite(input.Params.Sprites[4]);
                return;
            }

            output.Sprite(input.Params.Sprites[2]);
        }); // tentacles

        builder.RenderSingle(SpriteType.SecondaryAccessory, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.A.IsAttacking || input.A.IsEating)
            {
                output.Sprite(input.Params.Sprites[1]);
                return;
            }

            output.Sprite(input.Params.Sprites[0]);
        }); // underneath

        builder.RenderSingle(SpriteType.Belly, 2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.A.HasBelly)
            {
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Params.Sprites[29 + input.A.GetStomachSize(16)]);
                    return;
                }

                output.Sprite(input.Params.Sprites[6 + input.A.GetStomachSize(16)]);
            }
        });


        builder.RunBefore((input, output) =>
        {
            Sprite[][] vagrantSprites =
            {
                State.GameManager.SpriteDictionary.Vagrants,
                State.GameManager.SpriteDictionary.Vagrants2,
                State.GameManager.SpriteDictionary.Vagrants3
            };

            output.Params.Sprites = vagrantSprites[Mathf.Clamp(input.U.SkinColor, 0, 2)];

            output.ChangeSprite(SpriteType.Body).AddOffset(0, 60 * .625f);
            output.ChangeSprite(SpriteType.BodyAccessory).AddOffset(0, 60 * .625f);
            output.ChangeSprite(SpriteType.SecondaryAccessory).AddOffset(0, 60 * .625f);
            output.ChangeSprite(SpriteType.Belly).AddOffset(0, 60 * .625f);
        });

        builder.RandomCustom(Defaults.RandomCustom);
    });

    internal class VargantParameters : IParameters
    {
        internal Sprite[] Sprites;
    }
}