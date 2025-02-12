#region

using System.Collections.Generic;
using UnityEngine;

#endregion

internal static class LizardPeasant
{
    internal static readonly IClothing LizardPeasantInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.BlocksBreasts = true;
            output.RevealsDick = true;
            output.DiscardSprite = null;
            output.ClothingId = new ClothingId("common/78");
            output.OccupiesAllSlots = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing4"].Layer(19);
            output["Clothing4"].Coloring(Color.white);
            output["Clothing3"].Layer(18);
            output["Clothing3"].Coloring(Color.white);
            output["Clothing2"].Layer(17);
            output["Clothing1"].Layer(13);
            int bellySize = input.A.GetStomachSize();
            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrict, input.U.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrict, input.U.ClothingColor));
            output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrict, input.U.ClothingColor));
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Layer(20);
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[43]);
            }
            else if (input.A.PredatorComponent.BallsFullness >= 2)
            {
                output["Clothing1"].Layer(8);
                output["Clothing1"].Sprite(input.Sprites.LizardPeasant[1]);
            }
            else if (input.A.IsErect())
            {
                output["Clothing1"].Layer(10);
                output["Clothing1"].Sprite(input.Sprites.LizardPeasant[1]);
            }
            else
            {
                output["Clothing1"].Layer(13);
                output["Clothing1"].Sprite(input.Sprites.LizardPeasant[0]);
            }

            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing2"].Layer(20);
                output["Clothing2"].Sprite(null);
            }
            else if (input.U.HasBreasts)
            {
                output["Clothing2"].Layer(12);
                output["Clothing2"].Sprite(input.Sprites.LizardPeasant[input.A.IsAttacking ? 3 : 2]);
            }
            else
            {
                output["Clothing2"].Layer(12);
                output["Clothing2"].Sprite(null);
            }

            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing3"].Layer(15);
                output["Clothing3"].Sprite(input.Sprites.LizardsBootyArmor[44]);
            }
            else if (input.A.HasBelly)
            {
                output["Clothing3"].Layer(17);
                if (input.U.HasBreasts && bellySize >= 12)
                {
                    output["Clothing3"].Sprite(input.Sprites.LizardPeasant[20]);
                }
                else if (input.U.HasBreasts && bellySize <= 11)
                {
                    output["Clothing3"].Sprite(input.Sprites.LizardPeasant[9 + bellySize]);
                }
                else
                {
                    output["Clothing3"].Sprite(null);
                }
            }
            else
            {
                output["Clothing3"].Layer(17);
            }

            output["Clothing3"].Sprite(null);
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing4"].Layer(15);
                output["Clothing4"].Sprite(null);
            }
            else if (input.U.HasBreasts)
            {
                output["Clothing4"].Layer(18);
                if (input.U.BreastSize >= 7)
                {
                    output["Clothing4"].Sprite(input.Sprites.LizardPeasant[8]);
                }
                else if (input.U.BreastSize <= 6)
                {
                    output["Clothing4"].Sprite(input.Sprites.LizardPeasant[5 + input.U.BreastSize / 2]);
                }
                else
                {
                    output["Clothing4"].Sprite(null);
                }
            }
            else
            {
                output["Clothing4"].Layer(18);
            }

            output["Clothing4"].Sprite(null);
        });
    });
}

internal static class LizardLeaderCrown
{
    internal static readonly IClothing LizardLeaderCrownInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            {
                output.LeaderOnly = true;
            }
            ;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(10);
            output["Clothing1"].Coloring(Color.white);
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[36]);
            }
            else
            {
                output["Clothing1"].Sprite(input.Sprites.LizardLeader[2]);
            }
        });
    });
}

internal static class LizardLeaderTop
{
    internal static readonly IClothing LizardLeaderTopInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.ClothingId = new ClothingId("common/117");
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.LeaderOnly = true;
            output.FixedColor = true;
            output.DiscardSprite = input.Sprites.LizardLeader[5];
            output.OccupiesAllSlots = false;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(16);
            output["Clothing1"].Coloring(Color.white);
            output.RevealsDick = true;
            bool attacking = input.A.IsAttacking;
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Layer(15);
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[37]);
            }
            else
            {
                output.ChangeRaceSprite(SpriteType.Breasts).Layer(15);
            }

            output["Clothing1"].Sprite(input.Sprites.LizardLeader[0 + (attacking ? 1 : 0)]);
        });
    });
}

internal static class LizardLeaderSkirt
{
    internal static readonly IClothing LizardLeaderSkirtInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.ClothingId = new ClothingId("common/6010");
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.LeaderOnly = true;
            output.FixedColor = true;
            output.DiscardSprite = null;
            output.OccupiesAllSlots = false;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(12);
            output["Clothing1"].Layer(11);
            output.RevealsDick = true;
            output["Clothing1"].Layer(12);
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Layer(20);
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[40]);
            }
            else if (input.A.IsErect())
            {
                output["Clothing1"].Layer(11);
                output["Clothing1"].Sprite(null);
            }
            else
            {
                output["Clothing1"].Layer(11);
            }

            output["Clothing1"].Sprite(input.Sprites.LizardLeader[3]);
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing2"].Layer(17);
                output["Clothing2"].Sprite(input.Sprites.LizardsBootyArmor[41]);
            }
            else
            {
                output["Clothing2"].Layer(12);
            }

            output["Clothing2"].Sprite(null);
        });
    });
}

internal static class LizardLeaderLegguards
{
    internal static readonly IClothing LizardLeaderLegguardsInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.ClothingId = new ClothingId("common/6002");
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.LeaderOnly = true;
            output.FixedColor = true;
            output.DiscardSprite = null;
            output.OccupiesAllSlots = false;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(11);
            output.RevealsDick = true;
            int bellySize = input.A.GetStomachSize();
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Layer(19);
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[42]);
            }

            if (input.A.IsErect())
            {
                output["Clothing1"].Layer(11);
                if (bellySize > 3)
                {
                    output.ChangeRaceSprite(SpriteType.Belly).Layer(14);
                    output.ChangeRaceSprite(SpriteType.Dick).Layer(13);
                    output.ChangeRaceSprite(SpriteType.Balls).Layer(12);
                    output["Clothing1"].Sprite(input.Sprites.LizardLeader[4]);
                }
                else if (bellySize < 3)
                {
                    output.ChangeRaceSprite(SpriteType.Dick).Layer(21);
                    output.ChangeRaceSprite(SpriteType.Balls).Layer(20);
                    output["Clothing1"].Sprite(input.Sprites.LizardLeader[4]);
                }

                output["Clothing1"].Sprite(input.Sprites.LizardLeader[4]);
            }
            else
            {
                output["Clothing1"].Layer(11);
            }

            output["Clothing1"].Sprite(input.Sprites.LizardLeader[4]);
        });
    });
}

internal static class LizardLeaderArmbands
{
    internal static readonly IClothing LizardLeaderArmbandsInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.ClothingId = new ClothingId("common/6002");
            output.RevealsBreasts = true;
            output.LeaderOnly = true;
            output.FixedColor = true;
            output.DiscardSprite = null;
            output.OccupiesAllSlots = false;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(3);
            output["Clothing1"].Coloring(Color.white);
            bool attacking = input.A.IsAttacking;
            output.RevealsDick = true;
            output["Clothing1"].Layer(3);
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Layer(15);
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[38]);
            }
            else
            {
                output["Clothing1"].Sprite(input.Sprites.LizardLeader[6 + (input.A.IsAttacking ? 1 : 0)]);
            }
        });
    });
}

internal static class LizardBoneCrown
{
    internal static readonly IClothing LizardBoneCrownInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            {
                output.LeaderOnly = false;
            }
            ;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(10);
            output["Clothing1"].Coloring(Color.white);
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[24]);
            }
            else
            {
                output["Clothing1"].Sprite(input.Sprites.LizardBone[14]);
            }
        });
    });
}

internal static class LizardBoneTop
{
    internal static readonly IClothing LizardBoneTopInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.ClothingId = new ClothingId("common/6000");
            output.RevealsDick = true;
            output.LeaderOnly = false;
            output.FixedColor = true;
            output.DiscardSprite = input.Sprites.LizardBone[15];
            output.OccupiesAllSlots = false;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(11);
            output["Clothing1"].Layer(10);
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Layer(15);
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[25]);
            }
            else if (input.U.HasBreasts)
            {
                output["Clothing1"].Layer(17);
                if (input.U.BreastSize >= 7)
                {
                    output.RevealsBreasts = true;
                    output["Clothing1"].Sprite(input.Sprites.LizardBone[15]);
                }

                output.RevealsBreasts = true;
                output["Clothing1"].Sprite(input.Sprites.LizardBone[8]);
            }
            else
            {
                output["Clothing1"].Layer(17);
            }

            output["Clothing1"].Sprite(input.Sprites.LizardBone[0]);


            output.RevealsBreasts = true;
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output.ChangeRaceSprite(SpriteType.Breasts).Layer(15);
                output["Clothing2"].Layer(15);
                if (input.U.HasBreasts == false)
                {
                    output["Clothing2"].Sprite(null);
                }

                if (input.U.BreastSize <= 2)
                {
                    output["Clothing2"].Sprite(null);
                }

                if (input.U.BreastSize >= 3)
                {
                    output["Clothing2"].Sprite(input.Sprites.LizardsBootyArmor[30 + input.U.BreastSize - 3]);
                }

                output["Clothing2"].Sprite(null); //Does this work?  I don't know anymore
            }
            else if (input.U.BreastSize >= 0)
            {
                if (input.U.BreastSize >= 6)
                {
                    output.RevealsBreasts = true;
                    output["Clothing2"].Layer(17);
                    //output.changeSprite(SpriteType.Breasts).SetLayer(16);
                    if (input.U.BreastSize >= 7)
                    {
                        output.RevealsBreasts = true;
                        //output.changeSprite(SpriteType.Breasts).SetLayer(16);
                        output["Clothing2"].Sprite(input.Sprites.LizardBone[15]);
                    }
                    else //output.changeSprite(SpriteType.Breasts).SetLayer(16); 
                    {
                        output["Clothing2"].Sprite(input.Sprites.LizardBone[1 + input.U.BreastSize]);
                    }
                }
                else
                {
                    output["Clothing2"].Layer(17);
                    output["Clothing2"].Sprite(input.Sprites.LizardBone[1 + input.U.BreastSize]);
                }
            }
            else
            {
                output["Clothing2"].Sprite(null);
            }
        });
    });
}

internal static class LizardBoneLoins
{
    internal static readonly IClothing LizardBoneLoinsInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.ClothingId = new ClothingId("common/6001");
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.LeaderOnly = false;
            output.FixedColor = true;
            output.DiscardSprite = input.Sprites.LizardBone[10];
            output.OccupiesAllSlots = false;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(12);
            output["Clothing1"].Coloring(Color.white);
            output.RevealsDick = true;
            output["Clothing1"].Layer(12);
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Layer(17);
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[28]);
            }
            else if (input.A.IsErect())
            {
                output["Clothing1"].Sprite(null);
            }
            else
            {
                output["Clothing1"].Sprite(input.Sprites.LizardBone[10]);
            }
        });
    });
}

internal static class LizardBoneLegguards
{
    internal static readonly IClothing LizardBoneLegguardsInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.ClothingId = new ClothingId("common/6002");
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.LeaderOnly = false;
            output.FixedColor = true;
            output.DiscardSprite = null;
            output.OccupiesAllSlots = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(11);
            output.RevealsDick = true;
            int bellySize = input.A.GetStomachSize();
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Layer(19);
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[29]);
            }

            if (input.A.IsErect())
            {
                output["Clothing1"].Layer(11);
                if (bellySize > 3)
                {
                    output.ChangeRaceSprite(SpriteType.Belly).Layer(14);
                    output.ChangeRaceSprite(SpriteType.Dick).Layer(13);
                    output.ChangeRaceSprite(SpriteType.Balls).Layer(12);
                    output["Clothing1"].Sprite(input.Sprites.LizardBone[9]);
                }
                else if (bellySize < 3)
                {
                    output.ChangeRaceSprite(SpriteType.Dick).Layer(21);
                    output.ChangeRaceSprite(SpriteType.Balls).Layer(20);
                    output["Clothing1"].Sprite(input.Sprites.LizardBone[9]);
                }

                output["Clothing1"].Sprite(input.Sprites.LizardBone[9]);
            }
            else
            {
                output["Clothing1"].Layer(11);
            }

            output["Clothing1"].Sprite(input.Sprites.LizardBone[9]);
        });
    });
}

internal static class LizardBoneArmbands
{
    internal static readonly IClothing LizardBoneArmbandsInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.ClothingId = new ClothingId("common/6002");
            output.RevealsBreasts = true;
            output.LeaderOnly = false;
            output.FixedColor = true;
            output.DiscardSprite = null;
            output.OccupiesAllSlots = false;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(10);
            output["Clothing2"].Coloring(Color.white);
            output["Clothing1"].Layer(10);
            output["Clothing1"].Coloring(Color.white);
            bool attacking = input.A.IsAttacking;
            output.RevealsDick = true;
            output["Clothing1"].Layer(2);
            output["Clothing2"].Layer(3);
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Layer(15);
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[26]);
            }
            else
            {
                output["Clothing1"].Layer(2);
            }

            output["Clothing1"].Sprite(input.Sprites.LizardBone[11]);
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing2"].Layer(15);
                output["Clothing2"].Sprite(input.Sprites.LizardsBootyArmor[27]);
            }
            else
            {
                output["Clothing2"].Layer(3);
            }

            output["Clothing2"].Sprite(input.Sprites.LizardBone[12 + (input.A.IsAttacking ? 1 : 0)]);
        });
    });
}

internal static class LizardLeatherCrown
{
    internal static readonly IClothing LizardLeatherCrownInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            {
                output.LeaderOnly = false;
            }
            ;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(10);
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[0]);
            }
            else
            {
                output["Clothing1"].Sprite(input.Sprites.LizardLeather[23]);
            }
        });
    });
}

internal static class LizardLeatherTop
{
    internal static readonly IClothing LizardLeatherTopInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.ClothingId = new ClothingId("common/6000");
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.LeaderOnly = false;
            output.FixedColor = false;
            output.DiscardSprite = input.Sprites.LizardLeather[14];
            output.OccupiesAllSlots = false;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(17);
            output["Clothing1"].Layer(16);
            int bellySize = input.A.GetStomachSize();
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Layer(15);
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[1]);
            }
            else
            {
                output["Clothing1"].Layer(16);
                if (bellySize >= 7)
                {
                    output["Clothing1"].Sprite(input.Sprites.LizardLeather[7]);
                }
                else if (input.A.HasBelly)
                {
                    output["Clothing1"].Sprite(input.Sprites.LizardLeather[0 + bellySize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.LizardLeather[0]);
                }
            }

            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing2"].Layer(15);
                if (input.U.HasBreasts == false)
                {
                    output["Clothing2"].Sprite(null);
                }

                if (input.U.BreastSize <= 2)
                {
                    output["Clothing2"].Sprite(null);
                }

                if (input.U.BreastSize >= 3)
                {
                    output["Clothing2"].Sprite(input.Sprites.LizardsBootyArmor[6 + input.U.BreastSize - 3]);
                }

                output["Clothing2"].Sprite(null); //Does this work?  I don't know anymore
            }
            else
            {
                output["Clothing2"].Layer(17);
                output.RevealsBreasts = true;
                if (input.U.BreastSize <= 1)
                {
                    //CompleteSprite.HideSpritePlaceholder(SpriteType.Clothing2);
                    // TODO this is a very poor approch
                    // it's not going to be re-implemented so a different way should be found to
                    // achieve whatever it was trying to do
                }
                else if (input.U.BreastSize >= 8)
                {
                    output.RevealsBreasts = true;
                    output.ChangeRaceSprite(SpriteType.Breasts).Layer(16);
                    output["Clothing2"].Sprite(input.Sprites.LizardLeather[15]);
                }

                output["Clothing2"].Sprite(input.Sprites.LizardLeather[8 + input.U.BreastSize]);
            }
        });
    });
}

internal static class LizardLeatherLoins
{
    internal static readonly IClothing LizardLeatherLoinsInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.ClothingId = new ClothingId("common/6001");
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.LeaderOnly = false;
            output.FixedColor = false;
            output.DiscardSprite = input.Sprites.LizardLeather[17];
            output.OccupiesAllSlots = false;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(12);
            output.RevealsDick = true;
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Layer(17);
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[4]);
            }
            else if (input.A.IsErect())
            {
                output["Clothing1"].Layer(12);
                output["Clothing1"].Sprite(null);
            }
            else
            {
                output["Clothing1"].Layer(12);
            }

            output["Clothing1"].Sprite(input.Sprites.LizardLeather[17]);
        });
    });
}

internal static class LizardLeatherLegguards
{
    internal static readonly IClothing LizardLeatherLegguardsInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.ClothingId = new ClothingId("common/6002");
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.LeaderOnly = false;
            output.FixedColor = false;
            output.DiscardSprite = null;
            output.OccupiesAllSlots = false;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(11);
            output.RevealsDick = true;
            int bellySize = input.A.GetStomachSize();
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Layer(19);
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[5]);
            }

            if (input.A.IsErect())
            {
                output["Clothing1"].Layer(11);
                if (bellySize > 3)
                {
                    output.ChangeRaceSprite(SpriteType.Belly).Layer(14);
                    output.ChangeRaceSprite(SpriteType.Dick).Layer(13);
                    output.ChangeRaceSprite(SpriteType.Balls).Layer(12);
                    output["Clothing1"].Sprite(input.Sprites.LizardLeather[16]);
                }
                else if (bellySize < 3)
                {
                    output.ChangeRaceSprite(SpriteType.Dick).Layer(21);
                    output.ChangeRaceSprite(SpriteType.Balls).Layer(20);
                    output["Clothing1"].Sprite(input.Sprites.LizardLeather[16]);
                }

                output["Clothing1"].Sprite(input.Sprites.LizardLeather[16]);
            }
            else
            {
                output["Clothing1"].Layer(11);
            }

            output["Clothing1"].Sprite(input.Sprites.LizardLeather[16]);
        });
    });
}

internal static class LizardLeatherArmbands
{
    internal static readonly IClothing LizardLeatherArmbandsInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.ClothingId = new ClothingId("common/6002");
            output.RevealsBreasts = true;
            output.LeaderOnly = false;
            output.FixedColor = true;
            output.DiscardSprite = null;
            output.OccupiesAllSlots = false;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(10);
            output["Clothing2"].Coloring(Color.white);
            output["Clothing1"].Layer(10);
            output["Clothing1"].Coloring(Color.white);
            bool attacking = input.A.IsAttacking;
            output.RevealsDick = true;
            output["Clothing1"].Layer(2);
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Layer(15);
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[2]);
            }
            else
            {
                output["Clothing1"].Layer(2);
            }

            output["Clothing1"].Sprite(input.Sprites.LizardLeather[18 + (attacking ? 1 : 0)]);
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing2"].Layer(15);
                output["Clothing2"].Sprite(input.Sprites.LizardsBootyArmor[3]);
            }
            else
            {
                output["Clothing2"].Layer(3);
            }

            output["Clothing2"].Sprite(null);
        });
    });
}

internal static class LizardClothCrown
{
    internal static readonly IClothing LizardClothCrownInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            {
                output.LeaderOnly = false;
            }
            ;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(10);
            output["Clothing1"].Coloring(Color.white);
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[12]);
            }
            else
            {
                output["Clothing1"].Sprite(input.Sprites.LizardCloth[15]);
            }
        });
    });
}

internal static class LizardClothTop
{
    internal static readonly IClothing LizardClothTopInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.ClothingId = new ClothingId("common/6000");
            output.RevealsDick = true;
            output.LeaderOnly = false;
            output.FixedColor = true;
            output.DiscardSprite = input.Sprites.LizardCloth[14];
            output.OccupiesAllSlots = false;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(17);
            output["Clothing1"].Layer(16);
            output.RevealsBreasts = true;
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Layer(15);
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[13]);
            }
            else if (input.U.BreastSize <= 1)
            {
                output["Clothing1"].Layer(16);
                output["Clothing1"].Sprite(input.Sprites.LizardCloth[1]);
            }
            else if (input.U.BreastSize >= 8)
            {
                output["Clothing1"].Layer(16);
                output["Clothing1"].Sprite(input.Sprites.LizardCloth[7]);
            }
            else
            {
                output["Clothing1"].Layer(16);
                output["Clothing1"].Sprite(input.Sprites.LizardCloth[0 + input.U.BreastSize]);
            }

            output.RevealsBreasts = true;
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing2"].Layer(15);
                if (input.U.HasBreasts == false)
                {
                    output["Clothing2"].Sprite(null);
                }

                if (input.U.BreastSize <= 2)
                {
                    output["Clothing2"].Sprite(null);
                }

                if (input.U.BreastSize >= 3)
                {
                    output["Clothing2"].Sprite(input.Sprites.LizardsBootyArmor[30 + input.U.BreastSize - 3]);
                }

                output["Clothing2"].Sprite(null); //Does this work?  I don't know anymore
            }
            else
            {
                output["Clothing2"].Sprite(null);
            }
        });
    });
}

internal static class LizardClothLoins
{
    internal static readonly IClothing LizardClothLoinsInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.ClothingId = new ClothingId("common/6001");
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.LeaderOnly = false;
            output.FixedColor = true;
            output.DiscardSprite = input.Sprites.LizardCloth[11];
            output.OccupiesAllSlots = false;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(12);
            output["Clothing1"].Coloring(Color.white);
            output.RevealsDick = true;
            output["Clothing1"].Layer(13);
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Layer(17);
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[16]);
            }
            else if (input.A.IsErect())
            {
                output["Clothing1"].Sprite(null);
            }
            else
            {
                output["Clothing1"].Sprite(input.Sprites.LizardCloth[11]);
            }
        });
    });
}

internal static class LizardClothShorts
{
    internal static readonly IClothing LizardClothShortsInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.ClothingId = new ClothingId("common/6002");
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.LeaderOnly = false;
            output.FixedColor = true;
            output.DiscardSprite = null;
            output.OccupiesAllSlots = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(11);
            int bellySize = input.A.GetStomachSize();
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Layer(19);
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[17]);
            }

            if (input.A.IsErect())
            {
                output["Clothing1"].Layer(11);
                if (bellySize > 3)
                {
                    output.ChangeRaceSprite(SpriteType.Belly).Layer(14);
                    output.ChangeRaceSprite(SpriteType.Dick).Layer(13);
                    output.ChangeRaceSprite(SpriteType.Balls).Layer(12);
                }
                else if (bellySize < 3)
                {
                    output.ChangeRaceSprite(SpriteType.Dick).Layer(21);
                    output.ChangeRaceSprite(SpriteType.Balls).Layer(20);
                }

                output["Clothing1"].Sprite(input.Sprites.LizardCloth[9]);
            }
            else
            {
                output["Clothing1"].Layer(11);
                output["Clothing1"].Sprite(input.Sprites.LizardCloth[9]);
            }
        });
    });
}

internal static class LizardClothArmbands
{
    internal static readonly IClothing LizardClothArmbandsInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.ClothingId = new ClothingId("common/6002");
            output.RevealsBreasts = true;
            output.LeaderOnly = false;
            output.FixedColor = true;
            output.DiscardSprite = null;
            output.OccupiesAllSlots = false;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(10);
            output["Clothing2"].Coloring(Color.white);
            output["Clothing1"].Layer(10);
            output["Clothing1"].Coloring(Color.white);
            bool attacking = input.A.IsAttacking;
            output.RevealsDick = true;
            output["Clothing1"].Layer(2);
            output["Clothing2"].Layer(3);
            //output.Clothing1.GetSprite = (s) => Out.Update(State.GameManager.SpriteDictionary.LizardCloth[12]);
            //output.Clothing2.GetSprite = (s) => Out.Update(State.GameManager.SpriteDictionary.LizardCloth[13 + (attacking ? 1 : 0)]);
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Layer(15);
                output["Clothing1"].Sprite(input.Sprites.LizardsBootyArmor[14]);
            }
            else
            {
                output["Clothing1"].Layer(2);
            }

            output["Clothing1"].Sprite(input.Sprites.LizardCloth[12]);
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing2"].Layer(15);
                output["Clothing2"].Sprite(input.Sprites.LizardsBootyArmor[15]);
            }
            else
            {
                output["Clothing2"].Layer(3);
            }

            output["Clothing2"].Sprite(input.Sprites.LizardCloth[13 + (attacking ? 1 : 0)]);
        });
    });
}

internal static class RainCoat
{
    internal static readonly IClothing RainCoatInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.RevealsDick = true;
            output.BlocksBreasts = true;
            output.ClothingId = new ClothingId("common/79");
            output.DiscardSprite = input.Sprites.RainCoats[4];
            output.FixedColor = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing4"].Layer(0);
            output["Clothing3"].Layer(21);
            output["Clothing2"].Layer(20);
            output["Clothing1"].Layer(19);
            bool heavyWeight = input.A.GetBodyWeight() == 1;
            output["Clothing1"].Sprite(input.Sprites.RainCoats[0 + (input.A.IsAttacking ? 1 : 0) + (heavyWeight ? 2 : 0)]);

            int bellySize = input.A.GetStomachSize();

            output.ChangeRaceSprite(SpriteType.Hair).Sprite(input.Sprites.RainCoats[19 + input.U.HairStyle % 4]);
            output.ChangeRaceSprite(SpriteType.Hair2).SetHide(true);
            if (bellySize < 3)
            {
                output["Clothing2"].Sprite(input.Sprites.RainCoats[heavyWeight ? 7 : 6]);
            }
            else if (bellySize < 8)
            {
                output["Clothing2"].Sprite(input.Sprites.RainCoats[8]);
            }
            else if (bellySize < 11)
            {
                output["Clothing2"].Sprite(input.Sprites.RainCoats[9]);
            }
            else if (bellySize < 14)
            {
                output["Clothing2"].Sprite(input.Sprites.RainCoats[10]);
            }
            else if (bellySize < 16)
            {
                output["Clothing2"].Sprite(input.Sprites.RainCoats[11]);
            }

            if (input.U.HasBreasts == false || input.U.BreastSize == 0)
            {
                output["Clothing3"].Sprite(null);
            }
            else
            {
                output["Clothing3"].Sprite(input.Sprites.RainCoats[11 + input.U.BreastSize]);
            }

            output["Clothing4"].Sprite(input.Sprites.RainCoats[4]);
        });
    });
}

internal static class TigerSpecial
{
    internal static readonly IClothing TigerSpecialInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.RevealsBreasts = true;
            output.BlocksBreasts = true;
            output.OccupiesAllSlots = true;
            output.ClothingId = new ClothingId("common/90");
            output.DiscardSprite = input.Sprites.TigerSpecial[22];
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing4"].Layer(8);
            output["Clothing3"].Layer(12);
            output["Clothing2"].Layer(18);
            output["Clothing1"].Layer(10);
            output["Clothing1"].Sprite(input.Sprites.TigerSpecial[3 + input.U.BodySize]);
            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrict, input.U.ClothingColor));

            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrict, input.U.ClothingColor));
            if (input.U.BreastSize >= 0)
            {
                output["Clothing2"].Sprite(input.Sprites.TigerSpecial[11 + input.U.BreastSize]);
            }

            if (!Config.FurryHandsAndFeet && !input.U.Furry)
            {
                output["Clothing3"].Sprite(input.Sprites.TigerSpecial[2]);
            }

            if (input.U.BreastSize > 4)
            {
                output.ChangeRaceSprite(SpriteType.Breasts).Sprite(input.Sprites.TigerSpecial[14 + input.U.BreastSize]);
                output.BlocksBreasts = false;
            }
            else
            {
                output.BlocksBreasts = true;
            }

            output["Clothing4"].Sprite(input.Sprites.TigerSpecial[input.A.IsAttacking ? 1 : 0]);
            output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrict, input.U.ClothingColor));
            output.ChangeRaceSprite(SpriteType.Weapon).SetHide(true);
        });
    });
}

internal static class CatLeader
{
    internal static readonly IClothing CatLeaderInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.LeaderOnly = true;
            output.OccupiesAllSlots = true;
            output.RevealsBreasts = true;
            output.ClothingId = new ClothingId("common/91");
            output.DiscardSprite = input.Sprites.CatLeader[4];
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing6"].Layer(9);
            output["Clothing5"].Layer(9);
            output["Clothing4"].Layer(18);
            output["Clothing3"].Layer(19);
            output["Clothing2"].Layer(11);
            output["Clothing1"].Layer(10);
            //int bodyMod = input.U.BodySize + (input.U.HasBreasts ? 0 : 4);
            int bodyMod = input.U.BodySize + (input.U.HasBreasts ? 0 : 4);
            if (input.U.BodySize == 0)
            {
                bodyMod = 0;
            }

            if (bodyMod > 7)
            {
                bodyMod = 7;
            }

            bool furryArms = input.U.Furry || Config.FurryHandsAndFeet;
            output["Clothing1"].Sprite(input.Sprites.CatLeader[(furryArms ? 0 : 2) + (input.A.IsAttacking ? 1 : 0)]);
            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrict, input.U.ClothingColor));
            output["Clothing2"].Sprite(input.Sprites.CatLeader[6 + bodyMod]);
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrict, input.U.ClothingColor));
            if (input.U.BreastSize >= 1)
            {
                output["Clothing3"].Sprite(input.Sprites.CatLeader[13 + input.U.BreastSize]);
            }

            input.A.SquishedBreasts = true;

            if (input.U.BreastSize < 6)
            {
                output["Clothing4"].Sprite(input.Sprites.CatLeader[21 + bodyMod]);
                output["Clothing5"].Sprite(input.Sprites.CatLeader[29 + bodyMod]);
            }


            if (!furryArms)
            {
                output["Clothing6"].Sprite(input.Sprites.CatLeader[4]);
            }

            output.ChangeRaceSprite(RaceRenderer.AssumedFluffType).SetHide(true);
        });
    });
}

internal static class Toga
{
    internal static readonly IClothing TogaInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.Togas[10];
            output.DiscardUsesPalettes = true;
            output.ClothingId = new ClothingId("common/230");
            output.OccupiesAllSlots = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(10);
            output["Clothing2"].Coloring(Color.white);
            output["Clothing1"].Layer(17);
            output["Clothing1"].Coloring(Color.white);
            //output.Clothing1.GetPalette = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, input.U.ClothingColor);
            //output.Clothing2.GetPalette = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, input.U.ClothingColor);
            output["Clothing1"].Sprite(input.Sprites.Togas[input.U.HasBreasts ? 1 + input.U.BreastSize : 9]);
            output["Clothing2"].Sprite(input.Sprites.Togas[0]);
            //These are there to counteract the lamias natural clothing offset
            output["Clothing1"].SetOffset(1.875f, -3.75f);
            output["Clothing2"].SetOffset(1.875f, -3.75f);
        });
    });
}

internal static class SuccubusDress
{
    internal static readonly IClothing SuccubusDressInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.RevealsDick = true;
            output.RevealsBreasts = true;
            output.DiscardSprite = input.Sprites.SuccubusDress[22];
            output.DiscardUsesPalettes = true;
            output.ClothingId = new ClothingId("common/233");
            output.OccupiesAllSlots = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(17);
            output["Clothing2"].Coloring(Color.white);
            output["Clothing1"].Layer(14);
            output["Clothing1"].Coloring(Color.white);
            output["Clothing1"].SetOffset(0, -32 * .625f);
            output["Clothing2"].SetOffset(0, -32 * .625f);
            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrictRedKey, input.U.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrictRedKey, input.U.ClothingColor));
            int spriteNum = 0;
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                spriteNum = 1;
            }
            else
            {
                if (input.A.HasBelly)
                {
                    spriteNum = 2 + input.A.GetStomachSize();
                }
            }

            output["Clothing1"].Sprite(input.Sprites.SuccubusDress[spriteNum]);
            if (input.U.HasBreasts)
            {
                output["Clothing2"].Sprite(input.Sprites.SuccubusDress[18 + input.U.BreastSize]);
            }

            if (spriteNum < 7)
            {
                output.ChangeRaceSprite(SpriteType.Dick).Layer(12);
            }

            if (input.A.GetBallSize(15) > 6 && spriteNum >= 8)
            {
                output.SkipCheck = true;
            }
        });
    });
}

internal static class SuccubusLeotard
{
    internal static readonly IClothing SuccubusLeotardInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.RevealsBreasts = true;
            output.DiscardSprite = input.Sprites.SuccubusLeotard[36];
            output.DiscardUsesPalettes = true;
            output.ClothingId = new ClothingId("common/234");
            output.OccupiesAllSlots = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing5"].Layer(17);
            output["Clothing5"].Coloring(Color.white);
            output["Clothing4"].Layer(14);
            output["Clothing4"].Coloring(Color.white);
            output["Clothing3"].Layer(6);
            output["Clothing3"].Coloring(Color.white);
            output["Clothing2"].Layer(14);
            output["Clothing2"].Coloring(Color.white);
            output["Clothing1"].Layer(12);
            output["Clothing1"].Coloring(Color.white);
            output["Clothing1"].SetOffset(0, -32 * .625f);
            output["Clothing2"].SetOffset(0, -32 * .625f);
            output["Clothing3"].SetOffset(0, -32 * .625f);
            output["Clothing4"].SetOffset(0, -32 * .625f);
            output["Clothing5"].SetOffset(0, -32 * .625f);
            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrictRedKey, input.U.ClothingColor2));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrictRedKey, input.U.ClothingColor2));
            output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrictRedKey, input.U.ClothingColor2));
            output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrictRedKey, input.U.ClothingColor));
            output["Clothing5"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrictRedKey, input.U.ClothingColor));
            int spriteNum = input.A.GetStomachSize();
            if (input.A.IsUnbirthing || input.A.IsAnalVoring)
            {
                output["Clothing1"].Sprite(input.Sprites.SuccubusLeotard[1]);
                if (spriteNum < 10)
                {
                    output["Clothing2"].Sprite(input.Sprites.SuccubusLeotard[25 + spriteNum]);
                }

                if (input.A.HasBelly)
                {
                    output["Clothing4"].Sprite(input.Sprites.SuccubusLeotard[4 + spriteNum]);
                }
            }
            else
            {
                output["Clothing3"].Sprite(input.Sprites.SuccubusLeotard[input.A.IsAttacking ? 3 : 2]);
                output["Clothing1"].Sprite(input.Sprites.SuccubusLeotard[0]);
                if (input.A.HasBelly)
                {
                    output["Clothing4"].Sprite(input.Sprites.SuccubusLeotard[4 + spriteNum]);
                    if (spriteNum < 10)
                    {
                        output["Clothing2"].Sprite(input.Sprites.SuccubusLeotard[25 + spriteNum]);
                    }
                }
                else
                {
                    output["Clothing4"].Sprite(input.Sprites.SuccubusLeotard[4]);
                }
            }

            if (input.U.HasBreasts)
            {
                output["Clothing5"].Sprite(input.Sprites.SuccubusLeotard[21 + input.U.BreastSize]);
            }
        });
    });
}

internal static class LizardBlackTop
{
    internal static readonly IClothing LizardBlackTopInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = null;
            output.BlocksBreasts = true;
            output.RevealsDick = true;
            output.ClothingId = new ClothingId("common/208");
            output.FixedColor = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(17);
            output["Clothing1"].Coloring(Color.white);
            if ((Equals(input.U.GetRace, Race.Lizard) && input.A.IsAnalVoring) || input.A.IsUnbirthing)
            {
                output.RevealsDick = true;
                output["Clothing1"].Sprite(null);
            }
            else if (input.U.HasBreasts)
            {
                output["Clothing1"].Sprite(input.Sprites.LizardBlackTop[input.U.BreastSize]);
            }
            else
            {
                output["Clothing1"].Sprite(input.Sprites.LizardBlackTop[0]);
            }
        });
    });
}

internal static class LizardBikiniTop
{
    internal static readonly IClothing LizardBikiniTopInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.BikiniTop[9];
            output.FemaleOnly = true;
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.ClothingId = new ClothingId("common/205");
            output.DiscardUsesPalettes = true;
        });


        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(17);
            if ((Equals(input.U.GetRace, Race.Lizard) && input.A.IsAnalVoring) || input.A.IsUnbirthing)
            {
                output.RevealsDick = true;
                output["Clothing1"].Sprite(null);
            }
            else if (input.U.HasBreasts)
            {
                output["Clothing1"].Sprite(input.Sprites.LizardBikiniTop[input.U.BreastSize]);
                input.A.SquishedBreasts = true;
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));
            }
            else
            {
                output["Clothing1"].Sprite(null);
            }
        });
    });
}

internal static class LizardStrapTop
{
    internal static readonly IClothing LizardStrapTopInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.Straps[9];
            output.FemaleOnly = true;
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.ClothingId = new ClothingId("common/204");
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(17);
            if ((Equals(input.U.GetRace, Race.Lizard) && input.A.IsAnalVoring) || input.A.IsUnbirthing)
            {
                output.RevealsDick = true;
                output["Clothing1"].Sprite(null);
            }
            else if (input.U.HasBreasts)
            {
                output["Clothing1"].Sprite(input.Sprites.LizardCrossTop[input.U.BreastSize]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));
                input.A.SquishedBreasts = true;
            }
            else
            {
                output["Clothing1"].Sprite(null);
            }
        });
    });
}

internal static class RaceSpecificClothing
{
    internal static readonly IClothing LizardPeasantInstance = LizardPeasant.LizardPeasantInstance;
    internal static readonly IClothing LizardBlackTopInstance = LizardBlackTop.LizardBlackTopInstance;
    internal static readonly IClothing LizardBikiniTopInstance = LizardBikiniTop.LizardBikiniTopInstance;
    internal static readonly IClothing LizardStrapTopInstance = LizardStrapTop.LizardStrapTopInstance;

    internal static readonly IClothing LizardLeaderCrownInstance = LizardLeaderCrown.LizardLeaderCrownInstance;
    internal static readonly IClothing LizardLeaderTopInstance = LizardLeaderTop.LizardLeaderTopInstance;
    internal static readonly IClothing LizardLeaderSkirtInstance = LizardLeaderSkirt.LizardLeaderSkirtInstance;
    internal static readonly IClothing LizardLeaderLegguardsInstance = LizardLeaderLegguards.LizardLeaderLegguardsInstance;
    internal static readonly IClothing LizardLeaderArmbandsInstance = LizardLeaderArmbands.LizardLeaderArmbandsInstance;

    internal static readonly IClothing LizardBoneCrownInstance = LizardBoneCrown.LizardBoneCrownInstance;
    internal static readonly IClothing LizardBoneTopInstance = LizardBoneTop.LizardBoneTopInstance;
    internal static readonly IClothing LizardBoneLoinsInstance = LizardBoneLoins.LizardBoneLoinsInstance;
    internal static readonly IClothing LizardBoneLegguardsInstance = LizardBoneLegguards.LizardBoneLegguardsInstance;

    internal static readonly IClothing LizardBoneArmbandsInstance = LizardBoneArmbands.LizardBoneArmbandsInstance;
    //internal static LizardBoneArmbands2 LizardBoneArmbands2 = LizardBoneArmbands2.LizardBoneArmbands2Instance;
    //internal static LizardBoneArmbands3 LizardBoneArmbands3 = LizardBoneArmbands3.LizardBoneArmbands3Instance;

    internal static readonly IClothing LizardLeatherCrownInstance = LizardLeatherCrown.LizardLeatherCrownInstance;
    internal static readonly IClothing LizardLeatherTopInstance = LizardLeatherTop.LizardLeatherTopInstance;
    internal static readonly IClothing LizardLeatherLoinsInstance = LizardLeatherLoins.LizardLeatherLoinsInstance;
    internal static readonly IClothing LizardLeatherLegguardsInstance = LizardLeatherLegguards.LizardLeatherLegguardsInstance;

    internal static readonly IClothing LizardLeatherArmbandsInstance = LizardLeatherArmbands.LizardLeatherArmbandsInstance;
    //internal static LizardLeatherArmbands2 LizardLeatherArmbands2 = LizardLeatherArmbands2.LizardLeatherArmbands2Instance;
    //internal static LizardLeatherArmbands3 LizardLeatherArmbands3 = LizardLeatherArmbands3.LizardLeatherArmbands3Instance;

    internal static readonly IClothing LizardClothCrownInstance = LizardClothCrown.LizardClothCrownInstance;
    internal static readonly IClothing LizardClothTopInstance = LizardClothTop.LizardClothTopInstance;
    internal static readonly IClothing LizardClothLoinsInstance = LizardClothLoins.LizardClothLoinsInstance;
    internal static readonly IClothing LizardClothShortsInstance = LizardClothShorts.LizardClothShortsInstance;

    internal static readonly IClothing LizardClothArmbandsInstance = LizardClothArmbands.LizardClothArmbandsInstance;

    //internal static LizardClothArmbands2 LizardClothArmbands2 = LizardClothArmbands2.LizardClothArmbands2Instance;
    //internal static LizardClothArmbands3 LizardClothArmbands3 = LizardClothArmbands3.LizardClothArmbands3Instance;
    internal static readonly IClothing RainCoatInstance = RainCoat.RainCoatInstance;
    internal static readonly IClothing TigerSpecialInstance = TigerSpecial.TigerSpecialInstance;
    internal static readonly IClothing CatLeaderInstance = CatLeader.CatLeaderInstance;
    internal static readonly IClothing TogaInstance = Toga.TogaInstance;
    internal static readonly IClothing SuccubusDressInstance = SuccubusDress.SuccubusDressInstance;
    internal static readonly IClothing SuccubusLeotardInstance = SuccubusLeotard.SuccubusLeotardInstance;


    internal static readonly List<IClothing> All = new List<IClothing>
    {
        LizardPeasantInstance,
        LizardBlackTopInstance,
        LizardBikiniTopInstance,
        LizardStrapTopInstance,
        LizardLeaderTopInstance,
        LizardLeaderSkirtInstance,
        LizardLeaderLegguardsInstance,
        LizardLeaderArmbandsInstance,
        LizardBoneTopInstance,
        LizardBoneLoinsInstance,
        LizardBoneLegguardsInstance,
        LizardBoneArmbandsInstance,
        //LizardBoneArmbands2,
        //LizardBoneArmbands3,
        LizardLeatherTopInstance,
        LizardLeatherLoinsInstance,
        LizardLeatherLegguardsInstance,
        LizardLeatherArmbandsInstance,
        //LizardLeatherArmbands2,
        //LizardLeatherArmbands3,
        LizardClothTopInstance,
        LizardClothLoinsInstance,
        LizardClothShortsInstance,
        LizardClothArmbandsInstance,
        //LizardClothArmbands2,
        //LizardClothArmbands3,
        RainCoatInstance,
        TigerSpecialInstance,
        CatLeaderInstance,
        TogaInstance,
        SuccubusDressInstance,
        SuccubusLeotardInstance
    };

    internal static List<IClothing> Accessories = new List<IClothing>
    {
        LizardLeaderCrownInstance,
        LizardBoneCrownInstance,
        LizardLeatherCrownInstance,
        LizardClothCrownInstance
    };
}