#region

using System.Collections.Generic;
using UnityEngine;

namespace Races.Graphics.Implementations.UniqueMercs
{

    #endregion

    internal static class Erin
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Erin", "Erin");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 13,
                    StomachSize = 10,
                    FavoredStat = Stat.Endurance,
                    AllowedVoreTypes = new List<VoreType> { },
                    ExpMultiplier = 2.4f,
                    PowerAdjustment = 2f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new StatRange(5, 10),
                        Dexterity = new StatRange(5, 10),
                        Endurance = new StatRange(20, 25),
                        Mind = new StatRange(20, 25),
                        Will = new StatRange(20, 25),
                        Agility = new StatRange(24, 26),
                        Voracity = new StatRange(10, 15),
                        Stomach = new StatRange(9, 10),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Tasty,
                        TraitType.Prey,
                        TraitType.EasyToVore,
                        TraitType.Flight,
                        TraitType.TheGreatEscape
                    },
                    InnateSpells = new List<SpellType>()
                        { SpellType.DivinitysEmbrace },
                    RaceDescription = "Erin belongs to a very rare species known as a Nyangel, the lovechild of an angel and a catgirl.  Thanks to this divine heritage they aremostly all incredible healers... But they're also incredibly tasty.  Every Nyangel has a unique trait to set them apart from eachother, and Erin is no exception to this rule.  Her quirk is total acid resistance, the perfect defense against the raveous predators of this realm.  That doesn't stop her from being devoured, however, and that is unfortunately an all-too-common outcome for the girl.  Regardless of how many times she ends up eaten, the loveable Nyangel still tries her best to heal those she can.",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.ClothingExtraType1, "Panties");
                    buttons.SetText(ButtonType.ClothingExtraType2, "Stockings");
                    buttons.SetText(ButtonType.ClothingExtraType3, "Shoes");
                });
                output.BreastSizes = () => 1;
                output.DickSizes = () => 1;

                output.CanBeGender = new List<Gender> { Gender.Female };
                output.SpecialAccessoryCount = 0;
                output.ClothingShift = new Vector3(0, 0, 0);
                output.AvoidedEyeTypes = 0;
                output.AvoidedMouthTypes = 0;

                output.ClothingColors = 1;

                output.HairColors = 1;
                output.HairStyles = 1;
                output.SkinColors = 1;
                output.AccessoryColors = 1;
                output.EyeTypes = 1;
                output.EyeColors = 1;
                output.SecondaryEyeColors = 1;
                output.BodySizes = 0;
                output.AllowedMainClothingTypes.Set(
                    ErinTop.ErinTopInstance
                );
                output.AllowedWaistTypes.Set( //Bottoms
                    ErinSkirt.ErinSkirtInstance
                );
                output.ExtraMainClothing1Types.Set( //Over
                    ErinPantie.ErinPantieInstance
                );
                output.ExtraMainClothing2Types.Set( //Stocking
                    ErinStocking.ErinStockingInstance
                );

                output.ExtraMainClothing3Types.Set( //Hat
                    ErinShoes.ErinShoesInstance
                );

                output.AllowedClothingHatTypes.Clear();

                output.MouthTypes = 0;
                output.AvoidedMainClothingTypes = 0;

                output.ExtendedBreastSprites = false;
            });


            builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Erin[3]);
                    return;
                }

                if (input.U.IsDead && input.U.Items != null) //Second part checks for a not fully initialized unit, so that she doesn't have the dead face when you view her race info
                {
                    output.Sprite(input.Sprites.Erin[4]);
                    return;
                }

                output.Sprite(input.Sprites.Erin[2]);
            });

            builder.RenderSingle(SpriteType.Hair, 13, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Erin[8]);
            });

            builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Erin[9]);
            });

            builder.RenderSingle(SpriteType.Body, 5, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Erin[1]);
                    return;
                }

                output.Sprite(input.Sprites.Erin[0]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Erin[5]);
            });

            builder.RenderSingle(SpriteType.BodyAccent2, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Erin[6]);
            });

            builder.RenderSingle(SpriteType.BodyAccessory, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Erin[7]);
            });

            builder.RenderSingle(SpriteType.Breasts, 8, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Erin[20]);
            });

            builder.RenderSingle(SpriteType.SecondaryBreasts, 8, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Erin[21]);
            });

            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                IUnitRead unit = data.Unit;

                unit.Name = "Erin";
                unit.ClothingAccessoryType = State.Rand.Next(2);
            });
        });
    }

    internal static class ErinTop
    {
        internal static readonly IClothing ErinTopInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.ClothingId = new ClothingId("base.erin/9764");
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(11);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(12);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.A.IsAttacking ? input.Sprites.Erin[15] : input.Sprites.Erin[14]);

                output["Clothing2"].Sprite(input.Sprites.Erin[16]);
            });
        });
    }

    internal static class ErinPantie
    {
        internal static readonly IClothing ErinPantieInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.ClothingId = new ClothingId("base.erin/9764");
            });
            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(9);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Erin[13]);
            });
        });
    }

    internal static class ErinSkirt
    {
        internal static readonly IClothing ErinSkirtInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.ClothingId = new ClothingId("base.erin/9764");
            });
            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Erin[12]);
            });
        });
    }

    internal static class ErinStocking
    {
        internal static readonly IClothing ErinStockingInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.ClothingId = new ClothingId("base.erin/9764");
            });
            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(9);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Erin[11]);
            });
        });
    }

    internal static class ErinShoes
    {
        internal static readonly IClothing ErinShoesInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.ClothingId = new ClothingId("base.erin/9764");
            });
            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Erin[10]);
            });
        });
    }
}