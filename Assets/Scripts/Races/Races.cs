using System;
using System.Collections.Generic;
using UnityEngine;

static class Races
{

    private static Dictionary<Race, IRaceData> raceMap;

    private static void RegisterRace(Race race, IRaceData raceData)
    {
        raceMap.Add(race, raceData);
    }

    private static IRaceData SlimeQueenInstance;
    private static IRaceData AntQueenInstance;
    
    private static void LoadRaces()
    {
        raceMap = new Dictionary<Race, IRaceData>();
        SlimeQueenInstance = SlimeQueen.Instance;
        AntQueenInstance = AntQueen.Instance;
        
        RegisterRace(Race.Cats, Cats.Instance);
        RegisterRace(Race.Dogs, Dogs.Instance);
        RegisterRace(Race.Foxes, Foxes.Instance);
        RegisterRace(Race.Wolves, Wolves.Instance);
        RegisterRace(Race.Bunnies, Bunnies.Instance);
        RegisterRace(Race.Lizards, Lizards.Instance);
        RegisterRace(Race.Slimes, Slimes.Instance);
        RegisterRace(Race.Scylla, Scylla.Instance);
        RegisterRace(Race.Harpies, Harpies.Instance);
        RegisterRace(Race.Imps, Imps.Instance);
        RegisterRace(Race.Humans, Humans.Instance);
        RegisterRace(Race.Crypters, Crypters.Instance);
        RegisterRace(Race.Lamia, Lamia.Instance);
        RegisterRace(Race.Kangaroos, Kangaroos.Instance);
        RegisterRace(Race.Taurus, Taurus.Instance);
        
        RegisterRace(Race.Crux, Crux.Instance);
        
        //RegisterRace(Race.Equines, EquinesLua.Instance);
        RegisterRace(Race.Equines, EquinesImrpoved.Instance);
        
        RegisterRace(Race.Sergal, Sergal.Instance);
        RegisterRace(Race.Bees, Bees.Instance);
        RegisterRace(Race.Driders, Driders.Instance);
        RegisterRace(Race.Alraune, Alraune.Instance);
        RegisterRace(Race.DemiBats, Bats.Instance);
        RegisterRace(Race.Panthers, Panthers.Instance);
        RegisterRace(Race.Merfolk, Merfolk.Instance);
        RegisterRace(Race.Avians, Avians.Instance);
        RegisterRace(Race.Ants, Ants.Instance);
        RegisterRace(Race.Demifrogs, Demifrogs.Instance);
        RegisterRace(Race.Demisharks, Demisharks.Instance);
        RegisterRace(Race.Deer, Deer.Instance);
        RegisterRace(Race.Aabayx, Aabayx.Instance);

        RegisterRace(Race.Succubi, Succubi.Instance);
        RegisterRace(Race.Tigers, Tigers.Instance);
        RegisterRace(Race.Goblins, Goblins.Instance);
        RegisterRace(Race.Alligators, Alligators.Instance);
        RegisterRace(Race.Puca, Puca.Instance);
        RegisterRace(Race.Kobolds, Kobolds.Instance);
        RegisterRace(Race.DewSprites, DewSprites.Instance);
        RegisterRace(Race.Hippos, Hippos.Instance);
        RegisterRace(Race.Vipers, Vipers.Instance);
        RegisterRace(Race.Komodos, Komodos.Instance);
        RegisterRace(Race.Cockatrice, Cockatrice.Instance);
        RegisterRace(Race.Vargul, Vargul.Instance);
        RegisterRace(Race.Youko, Youko.Instance);

        RegisterRace(Race.Vagrants, Vagrants.Instance);
        RegisterRace(Race.Serpents, Serpents.Instance);
        RegisterRace(Race.Wyvern, Wyvern.Instance);
        RegisterRace(Race.YoungWyvern, YoungWyvern.Instance);
        RegisterRace(Race.Compy, Compy.Instance);
        RegisterRace(Race.FeralSharks, FeralSharks.Instance);
        RegisterRace(Race.FeralWolves, FeralWolves.Instance);
        RegisterRace(Race.DarkSwallower, DarkSwallower.Instance);
        RegisterRace(Race.Cake, Cake.Instance);
        RegisterRace(Race.Harvesters, Harvesters.Instance);
        RegisterRace(Race.Collectors, Collectors.Instance);
        RegisterRace(Race.Voilin, Voilin.Instance);
        RegisterRace(Race.FeralBats, FeralBats.Instance);
        RegisterRace(Race.FeralFrogs, FeralFrogs.Instance);
        RegisterRace(Race.Dragon, Dragon.Instance);
        RegisterRace(Race.Dragonfly, Dragonfly.Instance);
        RegisterRace(Race.TwistedVines, TwistedVines.Instance);
        RegisterRace(Race.Fairies, Fairies.Instance);
        RegisterRace(Race.FeralAnts, FeralAnts.Instance);
        RegisterRace(Race.Gryphons, Gryphons.Instance);
        RegisterRace(Race.SpitterSlugs, SpitterSlugs.Instance);
        RegisterRace(Race.SpringSlugs, SpringSlugs.Instance);
        RegisterRace(Race.RockSlugs, RockSlugs.Instance);
        RegisterRace(Race.CoralSlugs, CoralSlugs.Instance);
        RegisterRace(Race.Salamanders, Salamanders.Instance);
        RegisterRace(Race.Mantis, Mantis.Instance);
        RegisterRace(Race.EasternDragon, EasternDragon.Instance);
        RegisterRace(Race.Catfish, Catfish.Instance);
        RegisterRace(Race.Raptor, Raptor.Instance);
        RegisterRace(Race.WarriorAnts, WarriorAnts.Instance);
        RegisterRace(Race.Gazelle, Gazelle.Instance);
        RegisterRace(Race.Earthworms, Earthworms.Instance);
        RegisterRace(Race.FeralLizards, FeralLizards.Instance);
        RegisterRace(Race.Monitors, Monitors.Instance);
        RegisterRace(Race.Schiwardez, Schiwardez.Instance);
        RegisterRace(Race.Terrorbird, Terrorbird.Instance);
        RegisterRace(Race.Dratopyr, Dratopyr.Instance);
        RegisterRace(Race.FeralLions, FeralLions.Instance);
        RegisterRace(Race.Goodra, Goodra.Instance);
        RegisterRace(Race.Whisp, Whisp.Instance);

        RegisterRace(Race.Selicia, Selicia.Instance);
        RegisterRace(Race.Vision, Vision.Instance);
        RegisterRace(Race.Ki, Ki.Instance);
        RegisterRace(Race.Scorch, Scorch.Instance);
        RegisterRace(Race.Asura, Asura.Instance);
        RegisterRace(Race.DRACO, DRACO.Instance);
        RegisterRace(Race.Zoey, Zoey.Instance);
        RegisterRace(Race.Abakhanskya, Abakhanskya.Instance);
        RegisterRace(Race.Zera, Zera.Instance);
        RegisterRace(Race.Auri, Auri.Instance);
        RegisterRace(Race.Erin, Erin.Instance);
        RegisterRace(Race.Salix, Salix.Instance);
    }

    static internal IRaceData GetRace(Unit unit)
    {
        if (Equals(unit.Race, Race.Slimes) && unit.Type == UnitType.Leader)
        {
            return SlimeQueenInstance;
        }
        if (Equals(unit.Race, Race.Ants) && unit.Type == UnitType.Leader)
        {
            return AntQueenInstance;
        }
        return GetRace(unit.Race);
    }

    /// <summary>
    /// This version can't do the slime queen check, but is fine anywhere else
    /// </summary>    
    static internal IRaceData GetRace(Race race)
    {
        if (raceMap == null) LoadRaces();


        // TODO needs to be fixed eventually, probably with "Side" rework
        // Maybe make a No-OP race implementation
        // if (Equals(race, Race.Rebels) ||
        //     Equals(race, Race.Bandits) ||
        //     Equals(race, Race.none) //||
        //     //Equals(race, Race.Empire30) ||
        //     //Equals(race, Race.Empire31) ||
        //     //Equals(race, Race.Empire32) ||
        //     //Equals(race, Race.Empire33) ||
        //     //Equals(race, Race.Empire34
        //     )
        // {
        //     Debug.Log("Found try to GetRace");
        //     Debug.Log(race);
        //     return Whisp.Instance;
        // }
        
        IRaceData raceData;
        if (raceMap.TryGetValue(race, out raceData))
        {
            return raceData;
        }
        else
        {
            Debug.Log("Null race");
            return null;
            //throw new ApplicationException("No race registered for " + race.ToString());
        }
    }

}

