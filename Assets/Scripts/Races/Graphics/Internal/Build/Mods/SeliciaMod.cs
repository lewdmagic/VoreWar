using System;
using UnityEngine;

internal static class SeliciaMod
{


    internal static void ModAll()
    {
        ModDefaults();
        
        ModAabayx();
        ModAntQueen();
        ModAnts();
        ModAvians();
        ModBats();
        ModBees();
        ModDeer();
        ModDemifrogs();
        ModDemisharks();
        //ModEquines(); TODO
        ModHumans();
        ModImps();
        ModKangarros();
        //ModLamia();
        ModLizards();
        ModPanthers();
        ModSergal();
        ModSlimeQueen();
        ModSlimes();
        ModTaurus();
        ModCockatrice();
        ModGoblins();
        ModHippos();
        ModKomodos();
        ModPuca();
        ModSuccubi();
        ModVargul();
        ModVipers();
        ModCatfish();
        ModCollectors();
        ModCompy();
        ModCoralSlugs();
        ModDarkSwallower();
        ModDragon();
        ModDragonfly();
        ModDratopyr();
        ModEarthworms();
        ModEasternDragon();
        ModFairies();
        ModFeralAnts();
        ModFeralBats();
        ModFeralLions();
        ModFeralLizards();
        ModFeralSharks();
        ModFeralWolves();
        ModGazelle();
        ModGryphons();
        ModHarvesters();
        ModKobolds();
        ModMantis();
        ModMonitors();
        ModRaptor();
        ModRockSlugs();
        ModSalamanders();
        ModSchiwardez();
        ModSpitterSlugs();
        ModSpringSlugs();
        ModTerrorbird();
        ModTwistedVines();
        ModVagrants();
        ModVoilin();
        ModWarriorAnts();
        ModWyvern();
        ModYoungWyvern();
        ModAbakhanskya();
        ModAsura();
        ModAuri();
        ModDRACO();
        ModKi();
        ModSalix();
        ModScorch();
        ModVision();
        ModZoey();
    }

    private static void ModDefaults()
    {
        Defaults.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.Bellies[17]).AddOffset(0, -30 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.Bellies[16]).AddOffset(0, -30 * .625f);
                    return;
                }
            }
        });
        
        Defaults.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.Unit.Furry && Config.FurryGenitals)
            {
                int offset = input.Actor.GetBallSize(18, .8f);
                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 18)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[42]).AddOffset(0, -23 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && offset == 18)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[41]).AddOffset(0, -23 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && offset == 17)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[40]).AddOffset(0, -20 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && offset == 16)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[39]).AddOffset(0, -19 * .625f);
                    return;
                }

                if (offset > 0 && offset <= 12)
                {
                    return;
                }

                if (offset > 12)
                {
                    return;
                }
                
                return;
            }
            
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .9f) == 21)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Balls[24]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .9f) == 21)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Balls[23]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .9f) == 20)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Balls[22]).AddOffset(0, -15 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .9f) == 19)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Balls[21]).AddOffset(0, -14 * .625f);
                return;
            }
        });
    }
    
    
    private static void ModAabayx()
    {
        var raceTyped = (RaceData<IParameters>) Aabayx.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }
            
            int offsetI = input.Actor.GetBallSize(28, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offsetI == 28)
            {
                output.Sprite(input.Sprites.HumansVoreSprites[141]).AddOffset(0, -22 * .625f);
            }
            else if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offsetI == 28)
            {
                output.Sprite(input.Sprites.HumansVoreSprites[140]).AddOffset(0, -22 * .625f);
            }
            else if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offsetI == 27)
            {
                output.Sprite(input.Sprites.HumansVoreSprites[139]).AddOffset(0, -22 * .625f);
            }
            
        });
    }

    private static void ModAntQueen()
    {
        var raceTyped = (RaceData<OverSizeParameters>) AntQueen.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }
            
            int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(31 * 31));

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0) {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 31)
                {
                    output.Sprite(input.Sprites.AntQueen2[30]);
                }
                else if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 29)
                {
                    output.Sprite(input.Sprites.AntQueen2[29]);
                }
                else if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 27)
                {
                    output.Sprite(input.Sprites.AntQueen2[28]);
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(31 * 31));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 31)
                {
                    output.Sprite(input.Sprites.AntQueen2[61]);
                }
                else if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 29)
                {
                    output.Sprite(input.Sprites.AntQueen2[60]);
                }
                else if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 27)
                {
                    output.Sprite(input.Sprites.AntQueen2[59]);
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(29, 0.8f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.AntQueen2[95]).AddOffset(0, -26 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.AntQueen2[94]).AddOffset(0, -26 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 28)
                {
                    output.Sprite(input.Sprites.AntQueen2[93]).AddOffset(0, -26 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 27)
                {
                    output.Sprite(input.Sprites.AntQueen2[92]).AddOffset(0, -26 * .625f);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            int offsetI = input.Actor.GetBallSize(27, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offsetI == 27)
            {
                output.Sprite(input.Sprites.AntQueen2[132]).AddOffset(0, -17 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offsetI == 27)
            {
                output.Sprite(input.Sprites.AntQueen2[131]).AddOffset(0, -15 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offsetI == 26)
            {
                output.Sprite(input.Sprites.AntQueen2[130]).AddOffset(0, -13 * .625f);
                return;
            }
        });
    }

    
    
    
    private static void ModAnts()
    {
        var raceTyped = (RaceData<OverSizeParameters>) Ants.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));
                
                if (SeliciaMod.SelLeftBreast(input, output, leftSize, input.Sprites.Demiants2[31], input.Sprites.Demiants2[30], input.Sprites.Demiants2[29], 32, 30, 28))
                {
                    return;
                }
            }
        });        
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));

                if (SeliciaMod.SelRightBreast(input, output, rightSize, input.Sprites.Demiants2[63], input.Sprites.Demiants2[62], input.Sprites.Demiants2[61], 32, 30, 28))
                {
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(31, 0.8f);
                if (SeliciaMod.SelBelly(input, output, size, 31, 31, 30, 29, new Vector2(0, -32 * .625f), new Vector2(0, -32 * .625f), new Vector2(0, -32 * .625f), new Vector2(0, -32 * .625f), input.Sprites.Demiants2[99], input.Sprites.Demiants2[98], input.Sprites.Demiants2[97], input.Sprites.Demiants2[96]))
                {
                    return;
                }
            }
        });
        
        
    }
    
    
    
    private static void ModAvians()
    {
        var raceTyped = (RaceData<OverSizeParameters>) Avians.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.Avians3[31]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.Avians3[30]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.Avians3[29]);
                    return;
                }
            }
        });
        
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.Avians3[63]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.Avians3[62]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.Avians3[61]);
                    return;
                }
            }
        });
        
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(31, 0.8f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Avians3[96]).AddOffset(0, -34 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Avians3[143]).AddOffset(0, -34 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                {
                    output.Sprite(input.Sprites.Avians3[142]).AddOffset(0, -34 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.Avians3[141]).AddOffset(0, -34 * .625f);
                    return;
                }

            }
        });
        
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {            
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }
            int offsetI = input.Actor.GetBallSize(28, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offsetI == 28)
            {
                output.Sprite(input.Sprites.Avians1[137]).AddOffset(0, -23 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offsetI == 28)
            {
                output.Sprite(input.Sprites.Avians1[136]).AddOffset(0, -21 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offsetI == 27)
            {
                output.Sprite(input.Sprites.Avians1[135]).AddOffset(0, -19 * .625f);
                return;
            }

        });
    }
    
    
    
    private static void ModBats()
    {
        var raceTyped = (RaceData<OverSizeParameters>) Bats.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.Demibats3[31]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.Demibats3[30]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.Demibats3[29]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));


                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.Demibats3[63]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.Demibats3[62]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.Demibats3[61]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(31, 0.7f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Demibats3[99]).AddOffset(0, -31 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Demibats3[98]).AddOffset(0, -31 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                {
                    output.Sprite(input.Sprites.Demibats3[97]).AddOffset(0, -31 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.Demibats3[96]).AddOffset(0, -31 * .625f);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }
            
            int offset = input.Actor.GetBallSize(28, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Demibats3[137 + (input.Actor.Unit.Furry && Config.FurryGenitals ? 38 : 0)]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Demibats3[136 + (input.Actor.Unit.Furry && Config.FurryGenitals ? 38 : 0)]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.Demibats3[135 + (input.Actor.Unit.Furry && Config.FurryGenitals ? 38 : 0)]).AddOffset(0, -18 * .625f);
                return;
            }
        });
    }
    
    
    private static void ModBees()
    {
        var raceTyped = (RaceData<OverSizeParameters>) Bees.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.Bees2[69]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.Bees2[68]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.Bees2[67]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.Bees2[104]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.Bees2[103]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.Bees2[102]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(31, 0.8f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Bees2[143]).AddOffset(0, -7 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Bees2[142]).AddOffset(0, -7 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                {
                    output.Sprite(input.Sprites.Bees2[141]).AddOffset(0, -7 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.Bees2[140]).AddOffset(0, -7 * .625f);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            int offset = input.Actor.GetBallSize(28, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Bees2[37]);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Bees2[36]);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.Bees2[35]);
                return;
            }
        });
    }
    
    private static void ModDeer()
    {
        var raceTyped = (RaceData<OverSizeParameters>) Deer.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.Cockatrice2[31]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.Cockatrice2[30]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.Cockatrice2[29]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.Cockatrice2[63]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.Cockatrice2[62]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.Cockatrice2[61]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(31, 0.7f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Cockatrice2[99]).AddOffset(0, -29 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Cockatrice2[98]).AddOffset(0, -29 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                {
                    output.Sprite(input.Sprites.Cockatrice2[97]).AddOffset(0, -29 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.Cockatrice2[96]).AddOffset(0, -29 * .625f);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }
            
            int offset = input.Actor.GetBallSize(28, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Deer3[139 - (input.Actor.Unit.Furry && Config.FurryGenitals ? 102 : 0)]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Deer3[138 - (input.Actor.Unit.Furry && Config.FurryGenitals ? 102 : 0)]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.Deer3[137 - (input.Actor.Unit.Furry && Config.FurryGenitals ? 102 : 0)]).AddOffset(0, -18 * .625f);
                return;
            }
        });
    }
    
    
    private static void ModDemifrogs()
    {
        var raceTyped = (RaceData<OverSizeParameters>) Demifrogs.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.Unit.SpecialAccessoryType == 6)
            {
                if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
                {
                    int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(31 * 31));
                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 31)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[30]);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 29)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[29]);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 27)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[28]);
                        return;
                    }
                }
            }
            else
            {
                if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
                {
                    int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(31 * 31));

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 31)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[30]);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 29)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[29]);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 27)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[28]);
                        return;
                    }
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.Unit.SpecialAccessoryType == 6)
            {
                if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
                {
                    int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(31 * 31));

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 31)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[61]);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 29)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[60]);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 27)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[59]);
                        return;
                    }
                }
            }
            else
            {
                if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
                {
                    int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(31 * 31));

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 31)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[61]);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 29)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[60]);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 27)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[59]);
                        return;
                    }
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(28, 0.6f);
                if (input.Actor.Unit.SpecialAccessoryType == 6)
                {
                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 28)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[94]).AddOffset(0, -26 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 28)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[93]).AddOffset(0, -26 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 27)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[92]).AddOffset(0, -26 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 26)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[91]).AddOffset(0, -26 * .625f);
                        return;
                    }
                }
                else
                {
                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 28)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[94]).AddOffset(0, -26 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 28)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[93]).AddOffset(0, -26 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 27)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[92]).AddOffset(0, -26 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 26)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[91]).AddOffset(0, -26 * .625f);
                        return;
                    }
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            int offset = input.Actor.GetBallSize(28, .8f);


            if (input.Actor.Unit.SpecialAccessoryType == 6)
            {
                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ??
                     false) && offset == 28)
                {
                    output.Sprite(input.Sprites.Demifrogs3alt[132]).AddOffset(0, -22 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ??
                     false) && offset == 28)
                {
                    output.Sprite(input.Sprites.Demifrogs3alt[131]).AddOffset(0, -20 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ??
                     false) && offset == 27)
                {
                    output.Sprite(input.Sprites.Demifrogs3alt[130]).AddOffset(0, -18 * .625f);
                    return;
                }
            }
            else
            {
                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ??
                     false) && offset == 28)
                {
                    output.Sprite(input.Sprites.Demifrogs3[132]).AddOffset(0, -22 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ??
                     false) && offset == 28)
                {
                    output.Sprite(input.Sprites.Demifrogs3[131]).AddOffset(0, -20 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ??
                     false) && offset == 27)
                {
                    output.Sprite(input.Sprites.Demifrogs3[130]).AddOffset(0, -18 * .625f);
                    return;
                }
            }
        });
    }
    
    
    private static void ModDemisharks()
    {
        var raceTyped = (RaceData<OverSizeParameters>) Demisharks.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.Sharks4[31]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.Sharks4[30]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.Sharks4[29]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.Sharks4[63]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.Sharks4[62]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.Sharks4[61]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(31, 0.7f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Sharks4[99]).AddOffset(0, -29 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Sharks4[98]).AddOffset(0, -29 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                {
                    output.Sprite(input.Sprites.Sharks4[97]).AddOffset(0, -29 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.Sharks4[96]).AddOffset(0, -29 * .625f);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            int offset = input.Actor.GetBallSize(28, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Sharks4[137]).AddOffset(0, -21 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Sharks4[136]).AddOffset(0, -19 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.Sharks4[135]).AddOffset(0, -17 * .625f);
                return;
            }
        });
    }
    
    internal static void ModEquines()
    {
        var raceTyped = (RaceData<OverSizeParameters>) EquinesLua.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(29, 1.2f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.Horse[89]);
                }
                else if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.Horse[88]);
                }
                else if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size >= 27)
                {
                    output.Sprite(input.Sprites.Horse[87]);
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(29 * 29));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 29)
                {
                    output.Sprite(input.Sprites.Horse[119]);
                } else if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 29)
                {
                    output.Sprite(input.Sprites.Horse[118]);
                } else if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 27)
                {
                    output.Sprite(input.Sprites.Horse[117]);
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(29 * 29));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 29)
                {
                    output.Sprite(input.Sprites.Horse[149]);
                } 
                else if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 29)
                {
                    output.Sprite(input.Sprites.Horse[148]);
                } 
                else if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 27)
                {
                    output.Sprite(input.Sprites.Horse[147]);
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            int size = input.Actor.GetBallSize(29, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && size == 29)
            {
                output.Sprite(input.Sprites.Horse[59]);
            } 
            else if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 29)
            {
                output.Sprite(input.Sprites.Horse[58]);
            }
            else if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 27)
            {
                output.Sprite(input.Sprites.Horse[57]);
            }
        });
    }

    
    private static void ModHumans()
    {
        var raceTyped = (RaceData<OverSizeParameters>) Humans.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[31]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[30]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[29]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[63]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[62]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[61]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(31, 0.7f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[105]).AddOffset(0, -33 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[104]).AddOffset(0, -33 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[103]).AddOffset(0, -33 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[102]).AddOffset(0, -33 * .625f);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            int offset = input.Actor.GetBallSize(28, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.HumansVoreSprites[141]).AddOffset(0, -22 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.HumansVoreSprites[140]).AddOffset(0, -22 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.HumansVoreSprites[139]).AddOffset(0, -22 * .625f);
                return;
            }
        });
    }
    
    private static void ModImps()
    {
        var raceTyped = (RaceData<OverSizeParameters>) Imps.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.BodyAccent6, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.BodyAccentType1 == 2)
            {
                if (input.Actor.HasBelly)
                {
                    int size = input.Actor.GetStomachSize(32, 1.2f);
                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                    {
                        output.Sprite(input.Sprites.NewimpVore[141]).AddOffset(0, -31 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                    {
                        output.Sprite(input.Sprites.NewimpVore[140]).AddOffset(0, -30 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size >= 30)
                    {
                        output.Sprite(input.Sprites.NewimpVore[139]).AddOffset(0, -30 * .625f);
                        return;
                    }
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(22 * 22));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 22)
                {
                    output.Sprite(input.Sprites.NewimpVore[21]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 22)
                {
                    output.Sprite(input.Sprites.NewimpVore[20]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 20)
                {
                    output.Sprite(input.Sprites.NewimpVore[19]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 18)
                {
                    output.Sprite(input.Sprites.NewimpVore[18]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(22 * 22));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 22)
                {
                    output.Sprite(input.Sprites.NewimpVore[43]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 22)
                {
                    output.Sprite(input.Sprites.NewimpVore[42]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 20)
                {
                    output.Sprite(input.Sprites.NewimpVore[41]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 18)
                {
                    output.Sprite(input.Sprites.NewimpVore[40]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(32, 1.2f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.NewimpVore[105]).AddOffset(0, -31 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.NewimpVore[104]).AddOffset(0, -30 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size >= 30)
                {
                    output.Sprite(input.Sprites.NewimpVore[103]).AddOffset(0, -30 * .625f);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            int size = input.Actor.GetBallSize(22, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && size == 22)
            {
                output.Sprite(input.Sprites.NewimpVore[69]).AddOffset(0, -24 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 22)
            {
                output.Sprite(input.Sprites.NewimpVore[68]).AddOffset(0, -22 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 20)
            {
                output.Sprite(input.Sprites.NewimpVore[67]).AddOffset(0, -20 * .625f);
                return;
            }
        });
    }
    
    
    
    
    private static void ModKangarros()
    {
        var raceTyped = (RaceData<IParameters>) Kangaroos.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int sprite = input.Actor.GetStomachSize(19, .8f);
                if (input.Actor.Unit.HasBreasts)
                {
                    if (sprite == 19 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                    {
                        output.Sprite(input.Sprites.Kangaroos[136]);
                        return;
                    }
                }

                if (sprite == 19 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                {
                    output.Sprite(input.Sprites.Kangaroos[131]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.DickSize < 0)
            {
                return;
            }

            if (input.Actor.Unit.DickSize >= 0)
            {
                if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false)
                {
                    if (input.Actor.PredatorComponent.BallsFullness > 3)
                    {
                        output.Sprite(input.Sprites.Kangaroos[148]);
                        return;
                    }
                }
            }
        });
    }
    
    // TODO
    private static void ModLamia()
    {
        var raceTyped = (RaceData<SeliciaParameters>) Lamia.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            
        });
    }
    
    private static void ModLizards()
    {
        var raceTyped = (RaceData<FacingFrontParameters>) Lizards.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Params.FacingFront)
            {
                if (input.Actor.HasBelly)
                {
                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                    {
                        output.Sprite(input.Sprites.Bellies[17]).AddOffset(0, -30 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                    {
                        output.Sprite(input.Sprites.Bellies[16]).AddOffset(0, -30 * .625f);
                        return;
                    }
                }
            }
            else
            {
                if (input.Actor.HasBelly)
                {
                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                    {
                        output.Sprite(input.Sprites.Bellies[17]).AddOffset(0, -30 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                    {
                        output.Sprite(input.Sprites.Bellies[16]).AddOffset(0, -30 * .625f);
                        return;
                    }
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Params.FacingFront)
            {
                if (input.Actor.Unit.HasDick == false)
                {
                    return;
                }
                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) &&
                    input.Actor.GetBallSize(21, .9f) == 21)
                {
                    output.Sprite(input.Sprites.Balls[24]).AddOffset(0, -18 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) &&
                    input.Actor.GetBallSize(21, .9f) == 21)
                {
                    output.Sprite(input.Sprites.Balls[23]).AddOffset(0, -18 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) &&
                    input.Actor.GetBallSize(21, .9f) == 20)
                {
                    output.Sprite(input.Sprites.Balls[22]).AddOffset(0, -15 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) &&
                    input.Actor.GetBallSize(21, .9f) == 19)
                {
                    output.Sprite(input.Sprites.Balls[21]).AddOffset(0, -14 * .625f);
                    return;
                }
            }
            else
            {
                if (input.Actor.Unit.HasDick == false)
                {
                    return;
                }

                output.Layer(20);
                output.AddOffset(0, 0);
                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) &&
                    input.Actor.GetBallSize(21, .9f) == 21)
                {
                    output.Sprite(input.Sprites.LizardsBooty[42]).AddOffset(0, -18 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) &&
                    input.Actor.GetBallSize(21, .9f) == 21)
                {
                    output.Sprite(input.Sprites.LizardsBooty[41]).AddOffset(0, -18 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) &&
                    input.Actor.GetBallSize(21, .9f) == 20)
                {
                    output.Sprite(input.Sprites.LizardsBooty[40]).AddOffset(0, -15 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) &&
                    input.Actor.GetBallSize(21, .9f) == 19)
                {
                    output.Sprite(input.Sprites.LizardsBooty[39]).AddOffset(0, -14 * .625f);
                    return;
                }
            }
        });
    }
    
    private static void ModPanthers()
    {
        var raceTyped = (RaceData<OverSizeParameters>) Panthers.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[35]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[34]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[33]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[70]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[69]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[68]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(32, 1.2f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[145]).AddOffset(0, -22 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[144]).AddOffset(0, -22 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[143]).AddOffset(0, -22 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[142]).AddOffset(0, -22 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[141]).AddOffset(0, -22 * .625f);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.Unit.Predator == false)
            {
                return;
            }

            int size = input.Actor.GetBallSize(31, .8f);
            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) && size == 31)
            {
                output.Sprite(input.Sprites.PantherVoreParts[139]).AddOffset(0, -19 * .625f);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) &&
                size == 31)
            {
                output.Sprite(input.Sprites.PantherVoreParts[138]).AddOffset(0, -16 * .625f);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) &&
                size == 30)
            {
                output.Sprite(input.Sprites.PantherVoreParts[137]).AddOffset(0, -15 * .625f);
                return;
            }
        });
    }
    
    private static void ModSergal()
    {
        var raceTyped = (RaceData<IParameters>) Sergal.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            int size = input.Actor.GetStomachSize(18);
            if (size == 18 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true,
                    PreyLocation.stomach, PreyLocation.womb))
            {
                output.Sprite(input.Sprites.Sergal[45]);
                return;
            }
        });
    }
    
    private static void ModSlimeQueen()
    {
        var raceTyped = (RaceData<IParameters>) SlimeQueen.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                {
                    output.Sprite(input.Sprites.Slimes[69]).AddOffset(0, -25 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    if (input.Actor.GetStomachSize(15, .75f) == 15)
                    {
                        output.Sprite(input.Sprites.Slimes[68]).AddOffset(0, -25 * .625f);
                        return;
                    }

                    if (input.Actor.GetStomachSize(15, .875f) == 15)
                    {
                        output.Sprite(input.Sprites.Slimes[67]).AddOffset(0, -25 * .625f);
                        return;
                    }
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .9f) == 21)
            {
                output.Sprite(input.Sprites.Balls[24]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .9f) == 21)
            {
                output.Sprite(input.Sprites.Balls[23]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .9f) == 20)
            {
                output.Sprite(input.Sprites.Balls[22]).AddOffset(0, -15 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .9f) == 19)
            {
                output.Sprite(input.Sprites.Balls[21]).AddOffset(0, -14 * .625f);
                return;
            }
        });
    }
    
    private static void ModSlimes()
    {
        var raceTyped = (RaceData<IParameters>) Slimes.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                {
                    output.Sprite(input.Sprites.Slimes[69]).AddOffset(0, -25 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    if (input.Actor.GetStomachSize(15, .75f) == 15)
                    {
                        output.Sprite(input.Sprites.Slimes[68]).AddOffset(0, -25 * .625f);
                        return;
                    }

                    if (input.Actor.GetStomachSize(15, .875f) == 15)
                    {
                        output.Sprite(input.Sprites.Slimes[67]).AddOffset(0, -25 * .625f);
                        return;
                    }
                }
            }
        });
    }
    
    private static void ModTaurus()
    {
        var raceTyped = (RaceData<IParameters>) Taurus.Instance;

        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(11, .95f) == 11)
                {
                    output.Sprite(input.Sprites.CowsSeliciaBelly[1]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(11, .95f) == 11)
                {
                    output.Sprite(input.Sprites.CowsSeliciaBelly[0]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 21)
            {
                output.Sprite(input.Sprites.Balls[24]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 21)
            {
                output.Sprite(input.Sprites.Balls[23]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 20)
            {
                output.Sprite(input.Sprites.Balls[22]).AddOffset(0, -15 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 19)
            {
                output.Sprite(input.Sprites.Balls[21]).AddOffset(0, -14 * .625f);
                return;
            }
        });
    }
    
    private static void ModCockatrice()
    {
        var raceTyped = (RaceData<OverSizeParameters>) Cockatrice.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.Cockatrice2[31]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.Cockatrice2[30]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.Cockatrice2[29]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.Cockatrice2[63]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.Cockatrice2[62]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.Cockatrice2[61]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(31, 0.7f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Cockatrice2[99]).AddOffset(0, -29 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Cockatrice2[98]).AddOffset(0, -29 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                {
                    output.Sprite(input.Sprites.Cockatrice2[97]).AddOffset(0, -29 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.Cockatrice2[96]).AddOffset(0, -29 * .625f);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }
            
            int offset = input.Actor.GetBallSize(28, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Cockatrice2[137]).AddOffset(0, -19 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Cockatrice2[136]).AddOffset(0, -19 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.Cockatrice2[135]).AddOffset(0, -19 * .625f);
                return;
            }
        });
    }
    
    
    private static void ModGoblins()
    {
        var raceTyped = (RaceData<OverSizeParameters>) Goblins.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(22 * 22));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 22)
                {
                    output.Sprite(input.Sprites.Gobbovore[21]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 22)
                {
                    output.Sprite(input.Sprites.Gobbovore[20]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 20)
                {
                    output.Sprite(input.Sprites.Gobbovore[19]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 18)
                {
                    output.Sprite(input.Sprites.Gobbovore[18]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(22 * 22));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 22)
                {
                    output.Sprite(input.Sprites.Gobbovore[43]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 22)
                {
                    output.Sprite(input.Sprites.Gobbovore[42]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 20)
                {
                    output.Sprite(input.Sprites.Gobbovore[41]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 18)
                {
                    output.Sprite(input.Sprites.Gobbovore[40]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(32, 1.2f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.Gobbovore[105]).AddOffset(0, -23 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.Gobbovore[104]).AddOffset(0, -22 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size >= 30)
                {
                    output.Sprite(input.Sprites.Gobbovore[103]).AddOffset(0, -22 * .625f);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            int size = input.Actor.GetBallSize(22, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && size == 22)
            {
                output.Sprite(input.Sprites.Gobbovore[69]).AddOffset(0, -19 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 22)
            {
                output.Sprite(input.Sprites.Gobbovore[68]).AddOffset(0, -17 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 20)
            {
                output.Sprite(input.Sprites.Gobbovore[67]).AddOffset(0, -14 * .625f);
                return;
            }
        });
    }
    
    
    private static void ModHippos()
    {
        var raceTyped = (RaceData<OverSizeParameters>) Hippos.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.Hippos3[31]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.Hippos3[30]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.Hippos3[29]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.Hippos3[63]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.Hippos3[62]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.Hippos3[61]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(26, 0.7f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 26)
                {
                    output.Sprite(input.Sprites.Hippos3[94]).AddOffset(0, -9 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 26)
                {
                    output.Sprite(input.Sprites.Hippos3[93]).AddOffset(0, -9 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 25)
                {
                    output.Sprite(input.Sprites.Hippos3[92]).AddOffset(0, -9 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 24)
                {
                    output.Sprite(input.Sprites.Hippos3[91]).AddOffset(0, -9 * .625f);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            int ballOffset = input.Actor.GetBallSize(26, .9f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && ballOffset == 26)
            {
                output.Sprite(input.Sprites.Hippos[119]).AddOffset(0, -26 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && ballOffset == 26)
            {
                output.Sprite(input.Sprites.Hippos[118]).AddOffset(0, -21 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && ballOffset >= 24)
            {
                output.Sprite(input.Sprites.Hippos[117]).AddOffset(0, -17 * .625f);
                return;
            }
        });
    }
    
    
    private static void ModKomodos()
    {
        var raceTyped = (RaceData<OverSizeParameters>) Komodos.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(30 * 30));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.Komodos2[29]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.Komodos2[28]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 26)
                {
                    output.Sprite(input.Sprites.Komodos2[27]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(30 * 30));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.Komodos2[59]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.Komodos2[58]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 26)
                {
                    output.Sprite(input.Sprites.Komodos2[57]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(26, 0.7f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 26)
                {
                    output.Sprite(input.Sprites.Komodos2[90]).AddOffset(0, -12 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 26)
                {
                    output.Sprite(input.Sprites.Komodos2[89]).AddOffset(0, -12 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 25)
                {
                    output.Sprite(input.Sprites.Komodos2[88]).AddOffset(0, -12 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 24)
                {
                    output.Sprite(input.Sprites.Komodos2[87]).AddOffset(0, -12 * .625f);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }
            
            int offset = input.Actor.GetBallSize(27, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.Komodos2[127]).AddOffset(0, -27 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.Komodos2[126]).AddOffset(0, -22 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 26)
            {
                output.Sprite(input.Sprites.Komodos2[125]).AddOffset(0, -18 * .625f);
                return;
            }
        });
    }
    
    
    private static void ModPuca()
    {
        var raceTyped = (RaceData<IParameters>) Puca.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(9) == 9)
                {
                    output.Sprite(input.Sprites.Puca[50]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(9) == 9)
                {
                    output.Sprite(input.Sprites.Puca[49]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            output.AddOffset(0, -21 * .625f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 21)
            {
                output.Sprite(input.Sprites.Balls[24]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 21)
            {
                output.Sprite(input.Sprites.Balls[23]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 20)
            {
                output.Sprite(input.Sprites.Balls[22]).AddOffset(0, -15 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 19)
            {
                output.Sprite(input.Sprites.Balls[21]).AddOffset(0, -14 * .625f);
                return;
            }
        });
    }
    
    
    
    private static void ModSuccubi()
    {
        var raceTyped = (RaceData<IParameters>) Succubi.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                {
                    output.Sprite(input.Sprites.Succubi[88]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                {
                    if (input.Actor.GetStomachSize(15, 0.7f) == 15)
                    {
                        output.Sprite(input.Sprites.Succubi[91]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(15, 0.8f) == 15)
                    {
                        output.Sprite(input.Sprites.Succubi[90]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(15, 0.9f) == 15)
                    {
                        output.Sprite(input.Sprites.Succubi[89]);
                        return;
                    }
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 21)
            {
                output.Sprite(input.Sprites.Balls[24]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 21)
            {
                output.Sprite(input.Sprites.Balls[23]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 20)
            {
                output.Sprite(input.Sprites.Balls[22]).AddOffset(0, -15 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 19)
            {
                output.Sprite(input.Sprites.Balls[21]).AddOffset(0, -14 * .625f);
                return;
            }
        });
    }
    
    private static void ModVargul()
    {
        var raceTyped = (RaceData<OverSizeParameters>) Vargul.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            input.Params.Oversize = false;
            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(30 * 30));
                if (leftSize > input.Actor.Unit.DefaultBreastSize)
                {
                    input.Params.Oversize = true;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.Vargul3[29]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.Vargul3[28]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 26)
                {
                    output.Sprite(input.Sprites.Vargul3[27]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(30 * 30));
                if (rightSize > input.Actor.Unit.DefaultBreastSize)
                {
                    input.Params.Oversize = true;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.Vargul3[59]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.Vargul3[58]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 26)
                {
                    output.Sprite(input.Sprites.Vargul3[57]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(26, 0.7f);

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 26)
                {
                    output.Sprite(input.Sprites.Vargul3[90]).AddOffset(0, -22 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 26)
                {
                    output.Sprite(input.Sprites.Vargul3[89]).AddOffset(0, -22 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 25)
                {
                    output.Sprite(input.Sprites.Vargul3[88]).AddOffset(0, -22 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 24)
                {
                    output.Sprite(input.Sprites.Vargul3[87]).AddOffset(0, -22 * .625f);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            int offset = input.Actor.GetBallSize(27, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.Vargul3[127]).AddOffset(0, -24 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.Vargul3[126]).AddOffset(0, -19 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 26)
            {
                output.Sprite(input.Sprites.Vargul3[125]).AddOffset(0, -15 * .625f);
                return;
            }
        });
    }
    
    private static void ModVipers()
    {
        var raceTyped = (RaceData<OverSizeParameters>) Vipers.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.BodyAccent2, ModdingMode.After, (input, output) =>
        {
                        
            if (input.Actor.Unit.Predator == false)
            {
                return;
            }

            int size2;
            if (Config.LamiaUseTailAsSecondBelly && (input.Actor.PredatorComponent.Stomach2ndFullness > 0 || input.Actor.PredatorComponent.TailFullness > 0))
            {
                size2 = Math.Min(input.Actor.GetStomach2Size(19, 0.9f) + input.Actor.GetTailSize(19, 0.9f), 19);
            }
            else if (input.Actor.PredatorComponent.TailFullness > 0)
            {
                size2 = input.Actor.GetTailSize(19, 0.9f);
            }
            else
            {
                return;
            }

            if (input.Actor.Unit.TailType == 0)
            {
                if (size2 == 19 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach2, PreyLocation.tail))
                {
                    output.Sprite(input.Sprites.Vipers3[1]);
                }
                else if (size2 == 19 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach2, PreyLocation.tail))
                {
                    output.Sprite(input.Sprites.Vipers3[0]);
                }
            }
            else
            {
                if (size2 == 19 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach2, PreyLocation.tail))
                {
                    output.Sprite(input.Sprites.Vipers4[47]);
                }
                else if (size2 == 19 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach2, PreyLocation.tail))
                {
                    output.Sprite(input.Sprites.Vipers4[46]);
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.BodyAccent7, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.TailType == 0)
            {
                if (input.Actor.GetStomachSize() == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                {
                    output.Sprite(input.Sprites.Vipers1[99]);
                }
                else if (input.Actor.GetStomachSize() == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    output.Sprite(input.Sprites.Vipers1[98]);
                }
            }
            else
            {
                if (input.Actor.GetStomachSize() == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                {
                    output.Sprite(input.Sprites.Vipers4[27]);
                }
                else if (input.Actor.GetStomachSize() == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    output.Sprite(input.Sprites.Vipers4[26]);
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(28 * 28));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.Vipers2[27]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 26)
                {
                    output.Sprite(input.Sprites.Vipers2[26]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 24)
                {
                    output.Sprite(input.Sprites.Vipers2[25]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(28 * 28));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.Vipers2[55]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 26)
                {
                    output.Sprite(input.Sprites.Vipers2[54]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 24)
                {
                    output.Sprite(input.Sprites.Vipers2[53]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
                        
            if (!Config.LamiaUseTailAsSecondBelly)
            {
                if (input.Actor.HasBelly)
                {
                    int size0 = input.Actor.GetCombinedStomachSize();

                    if (input.Actor.Unit.TailType == 0)
                    {
                        if (size0 == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true,
                                PreyLocation.stomach, PreyLocation.stomach2, PreyLocation.womb))
                        {
                            output.Sprite(input.Sprites.Vipers1[95]);
                            return;
                        }

                        if (size0 == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false,
                                PreyLocation.stomach, PreyLocation.stomach2, PreyLocation.womb))
                        {
                            output.Sprite(input.Sprites.Vipers1[94]);
                            return;
                        }
                    }

                    if (size0 == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true,
                            PreyLocation.stomach, PreyLocation.stomach2, PreyLocation.womb))
                    {
                        output.Sprite(input.Sprites.Vipers4[45]);
                        return;
                    }

                    if (size0 == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false,
                            PreyLocation.stomach, PreyLocation.stomach2, PreyLocation.womb))
                    {
                        output.Sprite(input.Sprites.Vipers4[44]);
                        return;
                    }
                }

                return;
            }

            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize();

                if (input.Actor.Unit.TailType == 0)
                {
                    if (size == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true,
                            PreyLocation.stomach, PreyLocation.womb))
                    {
                        output.Sprite(input.Sprites.Vipers1[95]);
                    }
                    else if (size == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false,
                                 PreyLocation.stomach, PreyLocation.womb))
                    {
                        output.Sprite(input.Sprites.Vipers1[94]);
                    }
                }
                else
                {
                    if (size == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true,
                            PreyLocation.stomach, PreyLocation.womb))
                    {
                        output.Sprite(input.Sprites.Vipers4[45]);
                    }
                    else if (size == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false,
                                 PreyLocation.stomach, PreyLocation.womb))
                    {
                        output.Sprite(input.Sprites.Vipers4[44]);
                    }
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(20, 1.5f) == 20)
            {
                output.Sprite(input.Sprites.Vipers2[79]);
            }
            else if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(20, 1.2f) == 20)
            {
                output.Sprite(input.Sprites.Vipers2[78]);
            }
            else if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(20, 1.35f) == 20)
            {
                output.Sprite(input.Sprites.Vipers2[77]);
            }
        });
    }
    
    
    private static void ModCatfish()
    {
        var raceTyped = (RaceData<IParameters>) Catfish.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Body, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                output.Sprite(input.Sprites.Catfish[0]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.Catfish[80]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                if (input.Actor.GetStomachSize(20, .8f) == 20)
                {
                    output.Sprite(input.Sprites.Catfish[80]);
                    return;
                }

                if (input.Actor.GetStomachSize(20, .9f) == 20)
                {
                    output.Sprite(input.Sprites.Catfish[80]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.Catfish[59]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                if (input.Actor.GetStomachSize(20, .8f) == 20)
                {
                    output.Sprite(input.Sprites.Catfish[58]);
                    return;
                }

                if (input.Actor.GetStomachSize(20, .9f) == 20)
                {
                    output.Sprite(input.Sprites.Catfish[57]);
                    return;
                }
            }
        });
    }
    
    
    private static void ModCollectors()
    {
        var raceTyped = (RaceData<IParameters>) Collectors.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false)
            {
                if (input.Actor.PredatorComponent.VisibleFullness > 4)
                {
                    output.Sprite(input.Sprites.Collector[19]);
                    return;
                }
            }
        });
    }

    
    
    private static void ModCompy()
    {
        var raceTyped = (RaceData<IParameters>) Compy.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.GetBallSize(30) == 0)
            {
                return;
            }

            int size = input.Actor.GetBallSize(30);

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && size >= 30)
            {
                output.Sprite(input.Sprites.Compy[30]);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 28)
            {
                output.Sprite(input.Sprites.Compy[29]);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 26)
            {
                output.Sprite(input.Sprites.Compy[28]);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 24)
            {
                output.Sprite(input.Sprites.Compy[27]);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 22)
            {
                output.Sprite(input.Sprites.Compy[26]);
                return;
            }
        });
    }
    
    
    private static void ModCoralSlugs()
    {
        var raceTyped = (RaceData<IParameters>) CoralSlugs.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.CoralSlug[20]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.CoralSlug[10 + input.Actor.GetStomachSize(9)]);
                return;
            }
        });
    }
    
    
    private static void ModDarkSwallower()
    {
        var raceTyped = (RaceData<IParameters>) DarkSwallower.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                output.Sprite(input.Sprites.DarkSwallower[19]);
                return;
            }

            int size = input.Actor.GetStomachSize(29);

            if (size >= 28 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
            {
                output.Sprite(input.Sprites.DarkSwallower[44]);
                return;
            }

            if (size >= 26 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
            {
                output.Sprite(input.Sprites.DarkSwallower[43]);
                return;
            }

            if (size >= 24 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
            {
                output.Sprite(input.Sprites.DarkSwallower[42]);
                return;
            }

            if (size >= 22 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
            {
                output.Sprite(input.Sprites.DarkSwallower[41]);
                return;
            }
        });
    }
    
    
    private static void ModDragon()
    {
        var raceTyped = (RaceData<DragonParameters>) Dragon.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.Predator == false || input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Params.Position == Position.Standing || input.Params.Position == Position.StandingCrouch)
            {
                output.Layer(16);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                {
                    output.Sprite(input.Sprites.Dragon[69]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    if (input.Actor.GetStomachSize(17, 1.4f) == 17)
                    {
                        output.Sprite(input.Sprites.Dragon[68]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(17, 1.6f) == 17)
                    {
                        output.Sprite(input.Sprites.Dragon[67]);
                        return;
                    }
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false || input.Params.Position == Position.Down)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                output.AddOffset(0, 1 * .625f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                {
                    output.Sprite(input.Sprites.Dragon[91]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    if (input.Actor.GetBallSize(14, 1.4f) == 14)
                    {
                        output.Sprite(input.Sprites.Dragon[90]);
                        return;
                    }

                    if (input.Actor.GetBallSize(14, 1.6f) == 14)
                    {
                        output.Sprite(input.Sprites.Dragon[89]);
                        return;
                    }
                }
            }
        });
    }
    
    
    private static void ModDragonfly()
    {
        var raceTyped = (RaceData<IParameters>) Dragonfly.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.Predator == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                if (input.Actor.PredatorComponent.VisibleFullness > 3)
                {
                    output.Sprite(input.Sprites.Dragonfly[27]);
                    return;
                }
            }
        });
    }
    
    
    private static void ModDratopyr()
    {
        var raceTyped = (RaceData<IParameters>) Dratopyr.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            int bellySize = input.Actor.GetStomachSize(23, 0.7f);
            int shake = Dratopyr.FrameListShake.Frames[input.Actor.AnimationController.frameLists[2].currentFrame];

            if (!input.Actor.Targetable)
            {
                shake = 0;
            }

            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false)
            {
                output.Sprite(input.Sprites.Dratopyr[168 + shake]);
                return;
            }

            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false)
            {
                if (bellySize > 22)
                {
                    output.Sprite(input.Sprites.Dratopyr[165 + shake]);
                    return;
                }

                if (bellySize > 21)
                {
                    output.Sprite(input.Sprites.Dratopyr[162 + shake]);
                    return;
                }

                if (bellySize > 20)
                {
                    output.Sprite(input.Sprites.Dratopyr[159 + shake]);
                    return;
                }

                if (bellySize > 19)
                {
                    output.Sprite(input.Sprites.Dratopyr[156 + shake]);
                    return;
                }
            }

            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.womb) ?? false)
            {
                output.Sprite(input.Sprites.Dratopyr[168 + shake]);
                return;
            }

            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.womb) ?? false)
            {
                if (bellySize > 22)
                {
                    output.Sprite(input.Sprites.Dratopyr[165 + shake]);
                    return;
                }

                if (bellySize > 21)
                {
                    output.Sprite(input.Sprites.Dratopyr[162 + shake]);
                    return;
                }

                if (bellySize > 20)
                {
                    output.Sprite(input.Sprites.Dratopyr[159 + shake]);
                    return;
                }

                if (bellySize > 19)
                {
                    output.Sprite(input.Sprites.Dratopyr[156 + shake]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.DickSize == -1)
            {
                return;
            }

            if (Config.HideCocks)
            {
                return;
            }

            int shake = Dratopyr.FrameListShake.Frames[input.Actor.AnimationController.frameLists[2].currentFrame];
            int ballSize = input.Actor.GetBallSize(21, 0.6f);

            if (!input.Actor.Targetable)
            {
                shake = 0;
            }

            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false)
            {
                if (ballSize > 19)
                {
                    output.Sprite(input.Sprites.Dratopyr[96 + shake]);
                    return;
                }

                if (ballSize > 17)
                {
                    output.Sprite(input.Sprites.Dratopyr[93 + shake]);
                    return;
                }

                if (ballSize > 15)
                {
                    output.Sprite(input.Sprites.Dratopyr[90 + shake]);
                    return;
                }

                if (ballSize > 13)
                {
                    output.Sprite(input.Sprites.Dratopyr[87 + shake]);
                    return;
                }
            }
        });
    }
    
    
    private static void ModEarthworms()
    {
        var raceTyped = (RaceData<Earthworms.EarthWormParameters>) Earthworms.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Params.Position == Earthworms.Position.Aboveground)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
                {
                    output.Sprite(input.Sprites.Earthworms[43]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
                {
                    if (input.Actor.GetStomachSize(21, .76f) == 21)
                    {
                        output.Sprite(input.Sprites.Earthworms[42]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(21, .84f) == 21)
                    {
                        output.Sprite(input.Sprites.Earthworms[41]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(21, .92f) == 21)
                    {
                        output.Sprite(input.Sprites.Earthworms[40]);
                        return;
                    }
                }
            }
        });
    }
    
    
    private static void ModEasternDragon()
    {
        var raceTyped = (RaceData<IParameters>) EasternDragon.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.BodyAccent4, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.GetWombSize(17) > 0)
            {
                int sprite = input.Actor.GetWombSize(17, 0.8f);

                if (sprite == 17 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.womb) ?? false))
                {
                    output.Sprite(input.Sprites.EasternDragon[86]);
                    return;
                }

                if (sprite == 17 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.womb) ?? false))
                {
                    output.Sprite(input.Sprites.EasternDragon[85]);
                    return;
                }

                if (sprite == 16 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.womb) ?? false))
                {
                    output.Sprite(input.Sprites.EasternDragon[84]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.PredatorComponent?.ExclusiveStomachFullness > 0)
            {
                int sprite = input.Actor.GetExclusiveStomachSize(16, 0.8f);

                if (sprite == 16 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false))
                {
                    output.Sprite(input.Sprites.EasternDragon[37]).AddOffset(0, 0 * .625f);
                    return;
                }

                if (sprite == 16 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
                {
                    output.Sprite(input.Sprites.EasternDragon[36]).AddOffset(0, 0 * .625f);
                    return;
                }

                if (sprite == 15 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
                {
                    output.Sprite(input.Sprites.EasternDragon[35]).AddOffset(0, 0 * .625f);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.DickSize < 0)
            {
                return;
            }

            // TODO is this necessary? 
            if (Config.HideCocks)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.BallsFullness > 0 || input.Actor.IsCockVoring)
            {
                int sprite = input.Actor.GetBallSize(24, 0.8f);

                if (sprite == 24 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false))
                {
                    output.Sprite(input.Sprites.EasternDragon[66]);
                    return;
                }

                if (sprite == 24 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
                {
                    output.Sprite(input.Sprites.EasternDragon[65]);
                    return;
                }

                if (sprite == 23 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
                {
                    output.Sprite(input.Sprites.EasternDragon[64]);
                    return;
                }
            }
        });
    }
    
    
    private static void ModFairies()
    {
        var raceTyped = (RaceData<Fairies.FairyParameters>) Fairies.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.BodyAccent2, ModdingMode.After, (input, output) =>
        {
            if (input.Params.VeryEncumbered && input.Actor.IsEating)
            {
                if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false)
                {
                    output.Sprite(input.Sprites.Fairy[207]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.GetLeftBreastSize(21 * 21, Fairies.GeneralSizeMod));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast))
                {
                    output.Sprite(input.Sprites.Fairy240[8]).AddOffset(-34 * .625f, -57 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize == 21)
                {
                    output.Sprite(input.Sprites.Fairy240[7]).AddOffset(-34 * .625f, -57 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize > 20)
                {
                    output.Sprite(input.Sprites.Fairy240[6]).AddOffset(-34 * .625f, -57 * .625f);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.GetRightBreastSize(21 * 21, Fairies.GeneralSizeMod));
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast))
                {
                    output.Sprite(input.Sprites.Fairy240[13]).AddOffset(34 * .625f, -57 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize == 21)
                {
                    output.Sprite(input.Sprites.Fairy240[12]).AddOffset(34 * .625f, -57 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize > 20)
                {
                    output.Sprite(input.Sprites.Fairy240[11]).AddOffset(34 * .625f, -57 * .625f);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int bellySprite = input.Actor.GetRootedStomachSize(18, Fairies.GeneralSizeMod);

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                {
                    output.Sprite(input.Sprites.Fairy240[3]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && bellySprite == 18)
                {
                    output.Sprite(input.Sprites.Fairy240[2]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && bellySprite > 17)
                {
                    output.Sprite(input.Sprites.Fairy240[1]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                int ballSize = input.Actor.GetBallSize(17, Fairies.GeneralSizeMod);
                //AddOffset(Balls, 0, -10 * .625f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                {
                    output.Sprite(input.Sprites.Fairy240[17]).AddOffset(0, -10 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) && ballSize == 17)
                {
                    output.Sprite(input.Sprites.Fairy240[16]).AddOffset(0, -10 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) && ballSize == 16)
                {
                    output.Sprite(input.Sprites.Fairy240[15]).AddOffset(0, -10 * .625f);
                    return;
                }
            }
        });
    }
    
    
    private static void ModFeralAnts()
    {
        var raceTyped = (RaceData<IParameters>) FeralAnts.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.Predator == false)
            {
                return;
            }

            int size = input.Actor.GetStomachSize(16, 0.75f);

            if (size == 16 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.Ant[19]);
                return;
            }

            if (size == 16 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.Ant[18]);
                return;
            }
        });
    }
    
    
    private static void ModFeralBats()
    {
        var raceTyped = (RaceData<IParameters>) FeralBats.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.Predator == false)
            {
                return;
            }

            if (input.Actor.HasBelly)
            {
                int sprite = input.Actor.GetStomachSize(22);

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) || input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.womb))
                {
                    if (sprite >= 21)
                    {
                        output.Sprite(input.Sprites.Bat[27]);
                        return;
                    }
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) || input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.womb))
                {
                    if (sprite >= 19)
                    {
                        output.Sprite(input.Sprites.Bat[26]);
                        return;
                    }

                    if (sprite >= 17)
                    {
                        output.Sprite(input.Sprites.Bat[25]);
                        return;
                    }

                    if (sprite >= 15)
                    {
                        output.Sprite(input.Sprites.Bat[24]);
                        return;
                    }
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick)
            {
                if (input.Actor.Unit.Predator == false)
                {
                    output.Sprite(input.Sprites.Bat[31]);
                    return;
                }

                if (input.Actor.PredatorComponent.BallsFullness <= 0)
                {
                    output.Sprite(input.Sprites.Bat[28]);
                    return;
                }

                int sprite = input.Actor.GetBallSize(21);

                if (sprite >= 20 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false))
                {
                    output.Sprite(input.Sprites.Bat[49]);
                    return;
                }

                if (sprite >= 18 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
                {
                    output.Sprite(input.Sprites.Bat[48]);
                    return;
                }

                if (sprite >= 16 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
                {
                    output.Sprite(input.Sprites.Bat[47]);
                    return;
                }
            }
        });
    }
    
    
    private static void ModFeralLions()
    {
        var raceTyped = (RaceData<FeralLions.HindViewParameters>) FeralLions.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
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
        });
    }
    
    
    private static void ModFeralLizards()
    {
        var raceTyped = (RaceData<IParameters>) FeralLizards.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
            {
                output.Sprite(input.Sprites.FeralLizards[54]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
            {
                if (input.Actor.GetStomachSize(26, .8f) == 26)
                {
                    output.Sprite(input.Sprites.FeralLizards[53]);
                    return;
                }

                if (input.Actor.GetStomachSize(26, .9f) == 26)
                {
                    output.Sprite(input.Sprites.FeralLizards[52]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                {
                    output.Sprite(input.Sprites.FeralLizards[84]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    if (input.Actor.GetBallSize(26, .8f) == 26)
                    {
                        output.Sprite(input.Sprites.FeralLizards[83]);
                        return;
                    }

                    if (input.Actor.GetBallSize(26, .9f) == 26)
                    {
                        output.Sprite(input.Sprites.FeralLizards[82]);
                        return;
                    }
                }
            }
        });
    }
    
    private static void ModFeralSharks()
    {
        var raceTyped = (RaceData<IParameters>) FeralSharks.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            int size = input.Actor.GetStomachSize(22);

            if (size >= 21 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
            {
                output.Sprite(input.Sprites.Shark[30]);
                return;
            }

            if (size >= 19 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
            {
                output.Sprite(input.Sprites.Shark[31]);
                return;
            }

            if (size >= 17 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
            {
                output.Sprite(input.Sprites.Shark[32]);
                return;
            }
        });
    }
    
    
    
    private static void ModFeralWolves()
    {
        var raceTyped = (RaceData<IParameters>) FeralWolves.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.BodyAccessory, ModdingMode.After, (input, output) =>
        {
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false) && input.Actor.GetStomachSize() == 15)
            {
                return;
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false) && input.Actor.GetStomachSize() == 15)
            {
                output.Sprite(input.Sprites.FeralWolf[16]);
                return;
            }
        });
    }
    
    
    private static void ModGazelle()
    {
        var raceTyped = (RaceData<IParameters>) Gazelle.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
            {
                output.Sprite(input.Sprites.Gazelle2[30]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
            {
                if (input.Actor.GetStomachSize(27, .8f) == 27)
                {
                    output.Sprite(input.Sprites.Gazelle2[29]);
                    return;
                }

                if (input.Actor.GetStomachSize(27, .9f) == 27)
                {
                    output.Sprite(input.Sprites.Gazelle2[28]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                {
                    output.Sprite(input.Sprites.Gazelle2[66]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    if (input.Actor.GetBallSize(30, .8f) == 30)
                    {
                        output.Sprite(input.Sprites.Gazelle2[65]);
                        return;
                    }

                    if (input.Actor.GetBallSize(30, .9f) == 30)
                    {
                        output.Sprite(input.Sprites.Gazelle2[64]);
                        return;
                    }
                }
            }
        });
    }
    
    
    
    private static void ModGryphons()
    {
        var raceTyped = (RaceData<Gryphons.PositionParameters>) Gryphons.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.BodyAccent6, ModdingMode.After, (input, output) =>
        {
                        
            if (input.Actor.Unit.HasDick == false || input.Params.Position == Gryphons.Position.Standing)
            {
                return;
            }

            if (input.Actor.GetBallSize(10, 1.5f) > 5)
            {
                output.Layer(1);
                if (input.Actor.PredatorComponent?.BallsFullness > 0)
                {
                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                    {
                        output.Sprite(input.Sprites.Gryphon[47]);
                        return;
                    }

                    output.Sprite(input.Sprites.Gryphon[36 + input.Actor.GetBallSize(10, 1.5f)]);
                    return;
                }
                return;
            }

            if (input.Actor.GetStomachSize(16) < 3)
            {
                output.Layer(10);
                if (input.Actor.PredatorComponent?.BallsFullness > 0)
                {
                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                    {
                        output.Sprite(input.Sprites.Gryphon[47]);
                        return;
                    }

                    output.Sprite(input.Sprites.Gryphon[36 + input.Actor.GetBallSize(10, 1.5f)]);
                    return;
                }
                return;
            }

            if (input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                output.Layer(5);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                {
                    output.Sprite(input.Sprites.Gryphon[47]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.Predator == false || input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Params.Position == Gryphons.Position.Sitting)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(16) == 16)
                {
                    output.Sprite(input.Sprites.Gryphon[35]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    if (input.Actor.GetStomachSize(16, .8f) == 16)
                    {
                        output.Sprite(input.Sprites.Gryphon[61]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(16, .9f) == 16)
                    {
                        output.Sprite(input.Sprites.Gryphon[60]);
                        return;
                    }
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            int sz = input.Actor.GetStomachSize(16);
            int bz = input.Actor.GetBallSize(10, 1.5f);
            if (input.Actor.Unit.HasDick == false || input.Params.Position == Gryphons.Position.Standing)
            {
                return;
            }

            if (input.Actor.GetStomachSize(16) < 12 || sz < bz * 2)
            {
                output.Layer(13);
                if (input.Actor.PredatorComponent?.BallsFullness > 0)
                {
                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                    {
                        output.Sprite(input.Sprites.Gryphon[47]);
                        return;
                    }
                    return;
                }
                return;
            }

            if (input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                output.Layer(8);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                {
                    output.Sprite(input.Sprites.Gryphon[47]);
                    return;
                }
                return;
            }
        });
    }
    
    
    
    private static void ModHarvesters()
    {
        var raceTyped = (RaceData<IParameters>) Harvesters.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (!input.Actor.HasBelly)
            {
                return;
            }

            int size = input.Actor.GetStomachSize(26);

            if (size >= 26 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
            {
                output.Sprite(input.Sprites.Harvester[38]);
                return;
            }

            if (size >= 24 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
            {
                output.Sprite(input.Sprites.Harvester[37]);
                return;
            }

            if (size >= 22 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
            {
                output.Sprite(input.Sprites.Harvester[36]);
                return;
            }

            if (size >= 20 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
            {
                output.Sprite(input.Sprites.Harvester[35]);
                return;
            }
        });
    }
    
    
    private static void ModKobolds()
    {
        var raceTyped = (RaceData<FacingFrontParameters>) Kobolds.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
                        
            if (input.Actor.Unit.Predator == false || input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Params.FacingFront)
            {
                output.Layer(15);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(12) == 12)
                {
                    output.Sprite(input.Sprites.Kobolds[84]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    if (input.Actor.GetStomachSize(12, 0.7f) == 12)
                    {
                        output.Sprite(input.Sprites.Kobolds[110]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(12, 0.8f) == 12)
                    {
                        output.Sprite(input.Sprites.Kobolds[109]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(12, 0.9f) == 12)
                    {
                        output.Sprite(input.Sprites.Kobolds[108]);
                        return;
                    }
                }

                output.Sprite(input.Sprites.Kobolds[71 + input.Actor.GetStomachSize(12)]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(12) == 12)
            {
                output.Layer(2);
                output.Sprite(input.Sprites.Kobolds[102]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
            {
                output.Layer(2);
                if (input.Actor.GetStomachSize(12, 0.7f) == 12)
                {
                    output.Sprite(input.Sprites.Kobolds[116]);
                    return;
                }

                if (input.Actor.GetStomachSize(12, 0.8f) == 12)
                {
                    output.Sprite(input.Sprites.Kobolds[115]);
                    return;
                }

                if (input.Actor.GetStomachSize(12, 0.9f) == 12)
                {
                    output.Sprite(input.Sprites.Kobolds[114]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }
            
            if (input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) && input.Actor.GetBallSize(16) == 16)
                {
                    output.Sprite(input.Sprites.Kobolds[62]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    if (input.Actor.GetBallSize(16, 0.7f) == 16)
                    {
                        output.Sprite(input.Sprites.Kobolds[113]);
                        return;
                    }

                    if (input.Actor.GetBallSize(16, 0.8f) == 16)
                    {
                        output.Sprite(input.Sprites.Kobolds[112]);
                        return;
                    }

                    if (input.Actor.GetBallSize(16, 0.9f) == 16)
                    {
                        output.Sprite(input.Sprites.Kobolds[111]);
                        return;
                    }
                }
            }
        });
    }
    
    
    private static void ModMantis()
    {
        var raceTyped = (RaceData<Mantis.MantisParameters>) Mantis.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.Predator == false || input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Params.Position == Mantis.Position.Eating)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(17) == 17)
                {
                    output.Sprite(input.Sprites.Mantis[107]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    if (input.Actor.GetStomachSize(17, .8f) == 17)
                    {
                        output.Sprite(input.Sprites.Mantis[106]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(17, .9f) == 17)
                    {
                        output.Sprite(input.Sprites.Mantis[105]);
                        return;
                    }
                }

                output.Sprite(input.Sprites.Mantis[87 + input.Actor.GetStomachSize(17)]);
            }
        });
    }
    
    
    private static void ModMonitors()
    {
        var raceTyped = (RaceData<IParameters>) Monitors.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(29, 0.7f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.Monitors[153]).AddOffset(0, -13 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.Monitors[152]).AddOffset(0, -13 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 28)
                {
                    output.Sprite(input.Sprites.Monitors[151]).AddOffset(0, -13 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 27)
                {
                    output.Sprite(input.Sprites.Monitors[150]).AddOffset(0, -13 * .625f);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            int offset = input.Actor.GetBallSize(28, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Monitors[119]).AddOffset(0, -27 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Monitors[118]).AddOffset(0, -27 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.Monitors[117]).AddOffset(0, -27 * .625f);
                return;
            }
        });
    }
    
    
    private static void ModRaptor()
    {
        var raceTyped = (RaceData<IParameters>) Raptor.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.BodyAccent6, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.GetBallSize(24, 2) == 0 && Config.HideCocks == false)
            {
                return;
            }

            int size = input.Actor.GetBallSize(24, 2);

            if (size == 24 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false))
            {
                output.Sprite(input.Sprites.Raptor[75]);
                return;
            }

            if (size >= 23 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
            {
                output.Sprite(input.Sprites.Raptor[74]);
                return;
            }

            if (size == 22 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
            {
                output.Sprite(input.Sprites.Raptor[73]);
                return;
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            int size = input.Actor.GetStomachSize(24, 2);

            if (size == 24 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false))
            {
                output.Sprite(input.Sprites.Raptor[47]);
                return;
            }

            if (size >= 23 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
            {
                output.Sprite(input.Sprites.Raptor[46]);
                return;
            }

            if (size == 22 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
            {
                output.Sprite(input.Sprites.Raptor[45]);
                return;
            }
        });
    }
    
    
    private static void ModRockSlugs()
    {
        var raceTyped = (RaceData<IParameters>) RockSlugs.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.RockSlug[18]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.RockSlug[6 + input.Actor.GetStomachSize(11)]);
                return;
            }
        });
    }
    
    
    private static void ModSalamanders()
    {
        var raceTyped = (RaceData<IParameters>) Salamanders.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.Salamanders[72]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                if (input.Actor.GetStomachSize(16, .8f) == 16)
                {
                    output.Sprite(input.Sprites.Salamanders[71]);
                    return;
                }

                if (input.Actor.GetStomachSize(16, .9f) == 16)
                {
                    output.Sprite(input.Sprites.Salamanders[70]);
                    return;
                }
            }
        });
    }
    
    
    private static void ModSchiwardez()
    {
        var raceTyped = (RaceData<IParameters>) Schiwardez.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.GetBallSize(24) == 0 && Config.HideCocks == false)
            {
                return;
            }

            int size = input.Actor.GetBallSize(24);

            if (size == 24 &&
                (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true,
                    PreyLocation.balls) ?? false))
            {
                output.Sprite(input.Sprites.Schiwardez[32]);
                return;
            }

            if (size >= 23 &&
                (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false,
                    PreyLocation.balls) ?? false))
            {
                output.Sprite(input.Sprites.Schiwardez[31]);
                return;
            }

            if (size >= 21 &&
                (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false,
                    PreyLocation.balls) ?? false))
            {
                output.Sprite(input.Sprites.Schiwardez[30]);
                return;
            }

            if (size >= 19 &&
                (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false,
                    PreyLocation.balls) ?? false))
            {
                output.Sprite(input.Sprites.Schiwardez[29]);
                return;
            }
        });
    }
    
    
    private static void ModSpitterSlugs()
    {
        var raceTyped = (RaceData<IParameters>) SpitterSlugs.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.SpitterSlug[21]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.SpitterSlug[11 + input.Actor.GetStomachSize(9)]);
                return;
            }
        });
    }
    
    
    private static void ModSpringSlugs()
    {
        var raceTyped = (RaceData<IParameters>) SpringSlugs.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Body, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.SpringSlug[15]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.SpringSlug[5 + input.Actor.GetStomachSize(9)]);
                return;
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.SpringSlug[26]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.SpringSlug[16 + input.Actor.GetStomachSize(9)]);
                return;
            }
        });
    }
    
    
    private static void ModTerrorbird()
    {
        var raceTyped = (RaceData<IParameters>) Terrorbird.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
            {
                if (input.Actor.GetStomachSize(29, .75f) == 29)
                {
                    output.Sprite(input.Sprites.Terrorbird[64]);
                    return;
                }

                if (input.Actor.GetStomachSize(29, .8275f) == 29)
                {
                    output.Sprite(input.Sprites.Terrorbird[63]);
                    return;
                }

                if (input.Actor.GetStomachSize(29, .9f) == 29)
                {
                    output.Sprite(input.Sprites.Terrorbird[62]);
                    return;
                }
            }
        });
    }
    
    
    private static void ModTwistedVines()
    {
        var raceTyped = (RaceData<IParameters>) TwistedVines.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false) && input.Actor.GetStomachSize() == 15)
            {
                output.Sprite(input.Sprites.Plant[15]);
                return;
            }
        });
    }
    
    
    private static void ModVagrants()
    {
        var raceTyped = (RaceData<Vagrants.VargantParameters>) Vagrants.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
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
            }
        });
    }
    
    
    private static void ModVoilin()
    {
        var raceTyped = (RaceData<IParameters>) Voilin.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
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
        });
    }
    
    
    private static void ModWarriorAnts()
    {
        var raceTyped = (RaceData<IParameters>) WarriorAnts.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.WarriorAnt[36]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                if (input.Actor.GetStomachSize(16, .8f) == 20)
                {
                    output.Sprite(input.Sprites.WarriorAnt[35]);
                    return;
                }

                if (input.Actor.GetStomachSize(16, .9f) == 20)
                {
                    output.Sprite(input.Sprites.WarriorAnt[34]);
                    return;
                }
            }
        });
    }
    
    
    private static void ModWyvern()
    {
        var raceTyped = (RaceData<IParameters>) Wyvern.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false)
            {
                if (input.Actor.PredatorComponent.VisibleFullness > 3)
                {
                    output.Sprite(input.Sprites.Wyvern[50]);
                    return;
                }
            }
        });
    }
    
    
    private static void ModYoungWyvern()
    {
        var raceTyped = (RaceData<IParameters>) YoungWyvern.Instance;

        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            output.Layer(8);
            if (input.Actor.PredatorComponent.VisibleFullness < 0.2 / YoungWyvern.StomachGainDivisor)
            {
                return;
            }

            if (input.Actor.PredatorComponent.VisibleFullness < 0.5 / YoungWyvern.StomachGainDivisor)
            {
                return;
            }

            if (input.Actor.PredatorComponent.VisibleFullness < 1.2 / YoungWyvern.StomachGainDivisor)
            {
                return;
            }

            if (input.Actor.PredatorComponent.VisibleFullness < 1.75 / YoungWyvern.StomachGainDivisor)
            {
                return;
            }

            if (input.Actor.PredatorComponent.VisibleFullness < 2.5 / YoungWyvern.StomachGainDivisor)
            {
                return;
            }

            if (input.Actor.PredatorComponent.VisibleFullness < 3 / YoungWyvern.StomachGainDivisor)
            {
                return;
            }

            if (input.Actor.PredatorComponent.VisibleFullness < 3.5 / YoungWyvern.StomachGainDivisor)
            {
                return;
            }

            output.Layer(2);
            if (input.Actor.PredatorComponent.VisibleFullness > 5 / YoungWyvern.StomachGainDivisor && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true))
            {
                output.Sprite(input.Sprites.YoungWyvern[31]);
                return;
            }

            if (input.Actor.PredatorComponent.VisibleFullness > 5 / YoungWyvern.StomachGainDivisor && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false))
            {
                output.Sprite(input.Sprites.YoungWyvern[30]);
                return;
            }
        });
    }
    
    private static void ModAbakhanskya()
    {
        var raceTyped = (RaceData<IParameters>) Abakhanskya.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.BodyAccent4, ModdingMode.After, (input, output) =>
        {
            var sprite = 7 + input.Actor.GetStomachSize(5);
            if (sprite == 12 && (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
            {
                sprite = 13;
                output.Sprite(input.Sprites.Abakhanskya[sprite]);
            }
        });
    }
    
    private static void ModAsura()
    {
        var raceTyped = (RaceData<IParameters>) Asura.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
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
            }
        });
    }
    
    private static void ModAuri()
    {
        var raceTyped = (RaceData<OverSizeParameters>) Auri.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));
                if (leftSize > input.Actor.Unit.DefaultBreastSize)
                {
                    input.Params.Oversize = true;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.AuriVore[31]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.AuriVore[30]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.AuriVore[29]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));
                
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.AuriVore[66]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.AuriVore[65]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.AuriVore[64]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(32, Auri.StomachMult);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.AuriVore[105]).AddOffset(0, -34 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.AuriVore[104]).AddOffset(0, -34 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.AuriVore[103]).AddOffset(0, -34 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                {
                    output.Sprite(input.Sprites.AuriVore[102]).AddOffset(0, -34 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.AuriVore[101]).AddOffset(0, -33 * .625f);
                    return;
                }
            }
        });
    }
    
    private static void ModDRACO()
    {
        var raceTyped = (RaceData<IParameters>) DRACO.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false)
            {
                if (input.Actor.PredatorComponent.VisibleFullness > 3)
                {
                    output.Sprite(input.Sprites.DRACO[10]);
                    return;
                }
            }
        });
    }
    
    private static void ModKi()
    {
        var raceTyped = (RaceData<IParameters>) Ki.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.GetBallSize(9, 0.48f) <= 0)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) &&
                input.Actor.GetBallSize(9, .48f) == 9)
            {
                output.Sprite(input.Sprites.Ki[47]);
                return;
            }
        });
    }
    
    private static void ModSalix()
    {
        var raceTyped = (RaceData<OverSizeParameters>) Salix.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.SalixVore[31]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.SalixVore[30]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.SalixVore[29]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.SalixVore[66]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.SalixVore[65]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.SalixVore[64]);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(32);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.SalixVore[105]).AddOffset(0, -34 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.SalixVore[104]).AddOffset(0, -34 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.SalixVore[103]).AddOffset(0, -34 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                {
                    output.Sprite(input.Sprites.SalixVore[102]).AddOffset(0, -34 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.SalixVore[101]).AddOffset(0, -33 * .625f);
                    return;
                }
            }
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }
            
            int offset = input.Actor.GetBallSize(28, 0.8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.SalixGen[83]).AddOffset(0, -22 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.SalixGen[82]).AddOffset(0, -22 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.SalixGen[81]).AddOffset(0, -22 * .625f);
                return;
            }
        });
    }
    
    private static void ModScorch()
    {
        var raceTyped = (RaceData<IParameters>) Scorch.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {            
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false)
            {
                if (input.Actor.PredatorComponent.VisibleFullness > 4)
                {
                    output.Sprite(input.Sprites.Scorch[7]);
                    return;
                }
            }
        });
    }
    
    private static void ModVision()
    {
        var raceTyped = (RaceData<IParameters>) Vision.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false)
            {
                if (input.Actor.PredatorComponent.VisibleFullness > 3)
                {
                    output.Sprite(input.Sprites.Vision[9]);
                    return;
                }
            }
        });
    }
    
    private static void ModZera()
    {
        var raceTyped = (RaceData<OverSizeParameters>) Zera.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Breasts, ModdingMode.After, (input, output) =>
        {
            
        });
        
        raceTyped.ModifySingleRender(SpriteType.SecondaryBreasts, ModdingMode.After, (input, output) =>
        {
            
        });
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            
        });
        
        raceTyped.ModifySingleRender(SpriteType.Balls, ModdingMode.After, (input, output) =>
        {
            
        });
    }
    
    private static void ModZoey()
    {
        var raceTyped = (RaceData<Zoey.ZoeyParams>) Zoey.Instance;
        
        raceTyped.ModifySingleRender(SpriteType.Belly, ModdingMode.After, (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                switch (input.Params.BodyState)
                {
                    case Zoey.BodyState.SpinAttack:
                    case Zoey.BodyState.SideBelly:
                        if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(19) == 19)
                        {
                            output.Sprite(input.Sprites.Zoey[72]);
                            return;
                        }

                        if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                        {
                            if (input.Actor.GetStomachSize(19, 0.7f) == 19)
                            {
                                output.Sprite(input.Sprites.Zoey[78]);
                                return;
                            }

                            if (input.Actor.GetStomachSize(19, 0.8f) == 19)
                            {
                                output.Sprite(input.Sprites.Zoey[77]);
                                return;
                            }

                            if (input.Actor.GetStomachSize(19, 0.9f) == 19)
                            {
                                output.Sprite(input.Sprites.Zoey[76]);
                                return;
                            }
                        }
                        return;
                    default:
                        if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(19) == 19)
                        {
                            output.Sprite(input.Sprites.Zoey[51]);
                            return;
                        }

                        if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                        {
                            if (input.Actor.GetStomachSize(19, 0.7f) == 19)
                            {
                                output.Sprite(input.Sprites.Zoey[75]);
                                return;
                            }

                            if (input.Actor.GetStomachSize(19, 0.8f) == 19)
                            {
                                output.Sprite(input.Sprites.Zoey[74]);
                                return;
                            }

                            if (input.Actor.GetStomachSize(19, 0.9f) == 19)
                            {
                                output.Sprite(input.Sprites.Zoey[73]);
                                return;
                            }
                        }
                        return;
                }
            }
        });
    }
    

    
    
    
    
    
    
    
    
    private static bool SelLeftBreast( IRaceRenderInput<IParameters> input, IRaceRenderOutput output, int rightSize, Sprite spr1, Sprite spr2, Sprite spr3, int size1, int size2,  int size3)
    {
        if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && rightSize >= size1)
        {
            output.Sprite(spr1);
            return true;
        }

        if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && rightSize >= size2)
        {
            output.Sprite(spr2);
            return true;
        }

        if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && rightSize >= size3)
        {
            output.Sprite(spr3);
            return true;
        }
        
        return false;
    }

    private static bool SelRightBreast( IRaceRenderInput<IParameters> input, IRaceRenderOutput output, int leftSize, Sprite spr1, Sprite spr2, Sprite spr3, int size1, int size2,  int size3)
    {

        if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && leftSize >= size1)
        {
            output.Sprite(spr1);
            return true;
        }

        if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && leftSize >= size2)
        {
            output.Sprite(spr2);
            return true;
        }

        if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && leftSize >= size3)
        {
            output.Sprite(spr3);
            return true;
        }
        
        return false;
    }

    private static bool SelBelly(IRaceRenderInput input, IRaceRenderOutput output, int size, int size1, int size2, int size3, int size4, Vector2 offset1, Vector2 offset2, Vector2 offset3, Vector2 offset4, Sprite spr1, Sprite spr2, Sprite spr3, Sprite spr4)
    {
        if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == size1)
        {
            output.Sprite(spr1).AddOffset(offset1);
            return true;
        }

        if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == size2)
        {
            output.Sprite(spr2).AddOffset(offset2);
            return true;
        }

        if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == size3)
        {
            output.Sprite(spr3).AddOffset(offset3);
            return true;
        }

        if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == size4)
        {
            output.Sprite(spr4).AddOffset(offset4);
            return true;
        }
        
        return false;
    }
    

    private static bool SelBalls(IRaceRenderInput input, IRaceRenderOutput output, int offset, int size1, int size2, int size3, Vector2 offset1, Vector2 offset2, Vector2 offset3, Sprite spr1, Sprite spr2, Sprite spr3)
    {
        if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == size1)
        {
            output.Sprite(spr1).AddOffset(offset1);
            return true;
        }

        if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == size2)
        {
            output.Sprite(spr2).AddOffset(offset2);
            return true;
        }

        if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == size3)
        {
            output.Sprite(spr3).AddOffset(offset3);
            return true;
        }
        
        return false;
    }

    private static bool SelBalls(IRaceRenderInput input, IRaceRenderOutput output, int offset, int size1, int size2, int size3, Sprite spr1, Sprite spr2, Sprite spr3)
    {
        if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == size1)
        {
            output.Sprite(spr1);
            return true;
        }

        if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == size2)
        {
            output.Sprite(spr2);
            return true;
        }

        if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == size3)
        {
            output.Sprite(spr3);
            return true;
        }
        
        return false;
    }
    
    
    
}