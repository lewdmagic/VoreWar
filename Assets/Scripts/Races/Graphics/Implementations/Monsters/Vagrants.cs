#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class Vagrants
    {

        public static Sprite[] CalcSprites(IActorUnit actor)
        {
            Sprite[][] vagrantSprites =
            {
                State.GameManager.SpriteDictionary.Vagrants,
                State.GameManager.SpriteDictionary.Vagrants2,
                State.GameManager.SpriteDictionary.Vagrants3
            };

            return vagrantSprites[Mathf.Clamp(actor.Unit.SkinColor, 0, 2)];
        }
        
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
        
            builder.Setup(output =>
            {
                output.Names("Vagrant", "Vagrants");
                output.BonesInfo(null);
                output.FlavorText(new FlavorText(
                    new Texts { "tentacled", "rubbery", "alien" },
                    new Texts { "alien", "stretchy", "translucent" },
                    new Texts { "vagrant", "jellyfish", "medusa" },
                    "Stinger"
                ));
                output.RaceTraits(new RaceTraits()
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
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.DoubleAttack,
                        TraitType.Paralyzer
                    },
                    RaceDescription = "It is a matter of argument whether these beings emerged from the ocean or fell from the skies, or are even a mix of both, but they are among the first and oldest native threats the people who settled this realm faced. Their many tentacles paralyze those they touch and their rubbery bodies easily expand to engulf their prey.",
                });
                output.IndividualNames(new List<string>
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
                output.CanBeGender = new List<Gender> { Gender.None };
                output.SkinColors = 3;
            });


            builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(CalcSprites(input.A)[28]);
                    return;
                }

                output.Sprite(CalcSprites(input.A)[5]);
            });

            builder.RenderSingle(SpriteType.BodyAccessory, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsEating)
                {
                    output.Sprite(CalcSprites(input.A)[3]);
                    return;
                }

                if (input.A.IsAttacking)
                {
                    output.Sprite(CalcSprites(input.A)[4]);
                    return;
                }

                output.Sprite(CalcSprites(input.A)[2]);
            }); // tentacles

            builder.RenderSingle(SpriteType.SecondaryAccessory, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(CalcSprites(input.A)[1]);
                    return;
                }

                output.Sprite(CalcSprites(input.A)[0]);
            }); // underneath

            builder.RenderSingle(SpriteType.Belly, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.HasBelly)
                {
                    if (input.A.IsAttacking || input.A.IsEating)
                    {
                        output.Sprite(CalcSprites(input.A)[29 + input.A.GetStomachSize(16)]);
                        return;
                    }

                    output.Sprite(CalcSprites(input.A)[6 + input.A.GetStomachSize(16)]);
                }
            });


            builder.RunBefore((input, output) =>
            {
                output.ChangeSprite(SpriteType.Body).AddOffset(0, 60 * .625f);
                output.ChangeSprite(SpriteType.BodyAccessory).AddOffset(0, 60 * .625f);
                output.ChangeSprite(SpriteType.SecondaryAccessory).AddOffset(0, 60 * .625f);
                output.ChangeSprite(SpriteType.Belly).AddOffset(0, 60 * .625f);
            });

            builder.RandomCustom(Defaults.RandomCustom);
        });


    }
}