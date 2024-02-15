using System;
using System.Collections.Generic;
using BidirectionalMap;
using OdinSerializer;

public enum RaceTag
{
    MainRace,
    Merc,
    Monster,
    UniqueMerc,
    Humanoid,
}


public static class Race2
{
    internal static int NextRaceNumber = 0;
    internal static List<Race> RaceIdList = new List<Race>();
    internal static Dictionary<string, Race> StringMap = new Dictionary<string, Race>();
    public static BiMap<Race, Side> RaceSideMap = new BiMap<Race, Side>();

    internal static readonly Dictionary<Race, VeryBasicRaceData> BasicDataMap = new Dictionary<Race, VeryBasicRaceData>();

    internal static VeryBasicRaceData GetBasic(Race race)
    {
        return BasicDataMap[race];
    }

    public static void LoadHardcodedRaces()
    {
        foreach (Race race in RaceIdList)
        {
            race.Init();
        }
    }
}

public class VeryBasicRaceData
{
    internal RaceDataMaker RaceDataMaker;
    internal IRaceData RaceData;
    internal readonly HashSet<RaceTag> Tags = new HashSet<RaceTag>();

    /// <summary>
    ///     THIS IS NOT AN ID.
    ///     This is just an incremental number assigned to each Race.
    ///     IT IS ABSOLUTELY NOT GUARANTEED TO STAY THE SAME ACROSS VERSIONS
    ///     OR EVEN GAME LAUNCHES. NOT TO BE USED AS AN ID.
    /// </summary>
    public int RaceNumber;
}


public class Race : IComparable<Race>
{
    [OdinSerialize]
    internal readonly string Id;

    internal IRaceData RaceData => Race2.GetBasic(this).RaceData;

    internal static Race RegisterRace(string id, RaceDataMaker raceDataMaker, RaceTag[] tags)
    {
        Race race = new Race(id);

        VeryBasicRaceData basicRaceData = new VeryBasicRaceData();

        if (tags != null)
        {
            foreach (RaceTag tag in tags)
            {
                basicRaceData.Tags.Add(tag);
            }
        }

        basicRaceData.RaceDataMaker = raceDataMaker;
        basicRaceData.RaceNumber = Race2.NextRaceNumber++;

        Race2.BasicDataMap[race] = basicRaceData;

        Race2.RaceIdList.Add(race);
        Race2.StringMap[id] = race;
        Race2.RaceSideMap.Add(race, new Side(id));

        return race;
    }

    public Side ToSide()
    {
        if (Race2.RaceSideMap.Forward.ContainsKey(this))
        {
            return Race2.RaceSideMap.Forward[this];
        }
        else
        {
            throw new Exception();
        }
    }

    public override bool Equals(object obj)
    {
        Race otherRace = obj as Race;
        if (otherRace == null)
        {
            return false;
        }

        return Id.Equals(otherRace.Id);
    }
    
    public static bool operator == (Race b1, Race b2)
    {
        if ((object)b1 == null)
            return (object)b2 == null;

        return b1.Equals(b2);
    }

    public static bool operator != (Race b1, Race b2)
    {
        return !(b1 == b2);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Id;
    }

    public int CompareTo(Race other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (ReferenceEquals(null, other))
        {
            return 1;
        }

        return string.Compare(Id, other.Id, StringComparison.Ordinal);
    }


    internal Race(string id)
    {
        Id = id;
    }

    internal void Init()
    {
        Race2.GetBasic(this).RaceData = Race2.GetBasic(this).RaceDataMaker.Create();
    }

    internal static void CreateRace(string id, RaceDataMaker raceDataMaker, RaceTag[] tags = null)
    {
        Race race = RegisterRace(id, raceDataMaker, tags);
        race.Init();
    }

    public bool HasTag(RaceTag tag)
    {
        return Race2.GetBasic(this).Tags.Contains(tag);
    }

    public static Race TrueNone = null;

    // FIX Circular referrence static initialization
    public static Race Cat = RegisterRace("cat", Races.Graphics.Implementations.MainRaces.Cats.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Dog = RegisterRace("dog", Races.Graphics.Implementations.MainRaces.Dogs.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Fox = RegisterRace("fox", Races.Graphics.Implementations.MainRaces.Foxes.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Wolf = RegisterRace("wolf", Races.Graphics.Implementations.MainRaces.Wolves.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Bunny = RegisterRace("bunny", Races.Graphics.Implementations.MainRaces.Bunnies.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Lizard = RegisterRace("lizard", Races.Graphics.Implementations.MainRaces.Lizards.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Slime = RegisterRace("slime", Races.Graphics.Implementations.MainRaces.Slimes.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Scylla = RegisterRace("scylla", Races.Graphics.Implementations.MainRaces.Scylla.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Harpy = RegisterRace("harpy", Races.Graphics.Implementations.MainRaces.Harpies.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Imp = RegisterRace("imp", Races.Graphics.Implementations.MainRaces.Imps.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Human = RegisterRace("human", Races.Graphics.Implementations.MainRaces.Humans.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Crypter = RegisterRace("crypter", Races.Graphics.Implementations.MainRaces.Crypters.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Lamia = RegisterRace("lamia", Races.Graphics.Implementations.MainRaces.Lamia.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Kangaroo = RegisterRace("kangaroo", Races.Graphics.Implementations.MainRaces.Kangaroos.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Taurus = RegisterRace("taurus", Races.Graphics.Implementations.MainRaces.Taurus.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Crux = RegisterRace("crux", Races.Graphics.Implementations.MainRaces.Crux.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Equine = null; // TODO RegisterRace("equine", Races.Graphics.Implementations.MainRaces.EquinesImrpoved.Instance,    new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Sergal = RegisterRace("sergal", Races.Graphics.Implementations.MainRaces.Sergal.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Bee = RegisterRace("bee", Races.Graphics.Implementations.MainRaces.Bees.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Drider = RegisterRace("drider", Races.Graphics.Implementations.MainRaces.Driders.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Alraune = RegisterRace("alraune", Races.Graphics.Implementations.MainRaces.Alraune.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Bat = RegisterRace("bat", Races.Graphics.Implementations.MainRaces.DemiBats.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Panther = RegisterRace("panther", Races.Graphics.Implementations.MainRaces.Panthers.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Merfolk = RegisterRace("merfolk", Races.Graphics.Implementations.MainRaces.Merfolk.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Avian = RegisterRace("avian", Races.Graphics.Implementations.MainRaces.Avians.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Ant = RegisterRace("ant", Races.Graphics.Implementations.MainRaces.Ants.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Frog = RegisterRace("frog", Races.Graphics.Implementations.MainRaces.Demifrogs.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Shark = RegisterRace("shark", Races.Graphics.Implementations.MainRaces.Demisharks.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Deer = RegisterRace("deer", Races.Graphics.Implementations.MainRaces.Deer.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Aabayx = RegisterRace("aabayx", Races.Graphics.Implementations.MainRaces.Aabayx.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });

    public static Race Succubus = RegisterRace("succubu", Races.Graphics.Implementations.Mercs.Succubi.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Tiger = RegisterRace("tiger", Races.Graphics.Implementations.Mercs.Tigers.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Goblin = RegisterRace("goblin", Races.Graphics.Implementations.Mercs.Goblins.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Alligator = RegisterRace("alligator", Races.Graphics.Implementations.Mercs.Alligators.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Puca = RegisterRace("puca", Races.Graphics.Implementations.Mercs.Puca.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Kobold = RegisterRace("Kobold", Races.Graphics.Implementations.Mercs.Kobolds.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race DewSprite = RegisterRace("dewSprite", Races.Graphics.Implementations.Mercs.DewSprites.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Hippo = RegisterRace("hippo", Races.Graphics.Implementations.Mercs.Hippos.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Viper = RegisterRace("viper", Races.Graphics.Implementations.Mercs.Vipers.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Komodo = RegisterRace("komodo", Races.Graphics.Implementations.Mercs.Komodos.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Cockatrice = RegisterRace("cockatrice", Races.Graphics.Implementations.Mercs.Cockatrice.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Vargul = RegisterRace("vargul", Races.Graphics.Implementations.Mercs.Vargul.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Youko = RegisterRace("youko", Races.Graphics.Implementations.Mercs.Youko.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });

    public static Race Vagrant = RegisterRace("vagrant", Races.Graphics.Implementations.Monsters.Vagrants.Instance, new[] { RaceTag.Monster });
    public static Race Serpent = RegisterRace("serpent", Races.Graphics.Implementations.Monsters.Serpents.Instance, new[] { RaceTag.Monster });
    public static Race Wyvern = RegisterRace("wyvern", Races.Graphics.Implementations.Monsters.Wyvern.Instance, new[] { RaceTag.Monster });
    public static Race YoungWyvern = RegisterRace("youngWyvern", Races.Graphics.Implementations.Monsters.YoungWyvern.Instance, new[] { RaceTag.Monster });
    public static Race Compy = RegisterRace("compy", Races.Graphics.Implementations.Monsters.Compy.Instance, new[] { RaceTag.Monster });
    public static Race FeralShark = RegisterRace("feralShark", Races.Graphics.Implementations.Monsters.FeralSharks.Instance, new[] { RaceTag.Monster });
    public static Race FeralWolve = RegisterRace("feralWolve", Races.Graphics.Implementations.Monsters.FeralWolves.Instance, new[] { RaceTag.Monster });
    public static Race DarkSwallower = RegisterRace("darkSwallower", Races.Graphics.Implementations.Monsters.DarkSwallower.Instance, new[] { RaceTag.Monster });
    public static Race Cake = RegisterRace("cake", Races.Graphics.Implementations.Monsters.Cake.Instance, new[] { RaceTag.Monster });
    public static Race Harvester = RegisterRace("harvester", Races.Graphics.Implementations.Monsters.Harvesters.Instance, new[] { RaceTag.Monster });
    public static Race Collector = RegisterRace("collector", Races.Graphics.Implementations.Monsters.Collectors.Instance, new[] { RaceTag.Monster });
    public static Race Voilin = RegisterRace("voilin", Races.Graphics.Implementations.Monsters.Voilin.Instance, new[] { RaceTag.Monster });
    public static Race FeralBat = RegisterRace("feralBat", Races.Graphics.Implementations.Monsters.FeralBats.Instance, new[] { RaceTag.Monster });
    public static Race FeralFrog = RegisterRace("feralFrog", Races.Graphics.Implementations.Monsters.FeralFrogs.Instance, new[] { RaceTag.Monster });
    public static Race Dragon = RegisterRace("dragon", Races.Graphics.Implementations.Monsters.Dragon.Instance, new[] { RaceTag.Monster });
    public static Race Dragonfly = RegisterRace("dragonfly", Races.Graphics.Implementations.Monsters.Dragonfly.Instance, new[] { RaceTag.Monster });
    public static Race TwistedVine = RegisterRace("twistedVine", Races.Graphics.Implementations.Monsters.TwistedVines.Instance, new[] { RaceTag.Monster });
    public static Race Fairy = RegisterRace("fairy", Races.Graphics.Implementations.Monsters.Fairies.Instance, new[] { RaceTag.Monster });
    public static Race FeralAnt = RegisterRace("feralAnt", Races.Graphics.Implementations.Monsters.FeralAnts.Instance, new[] { RaceTag.Monster });
    public static Race Gryphon = RegisterRace("gryphon", Races.Graphics.Implementations.Monsters.Gryphons.Instance, new[] { RaceTag.Monster });
    public static Race SpitterSlug = RegisterRace("spitterSlug", Races.Graphics.Implementations.Monsters.SpitterSlugs.Instance, new[] { RaceTag.Monster });
    public static Race SpringSlug = RegisterRace("springSlug", Races.Graphics.Implementations.Monsters.SpringSlugs.Instance, new[] { RaceTag.Monster });
    public static Race RockSlug = RegisterRace("rockSlug", Races.Graphics.Implementations.Monsters.RockSlugs.Instance, new[] { RaceTag.Monster });
    public static Race CoralSlug = RegisterRace("coralSlug", Races.Graphics.Implementations.Monsters.CoralSlugs.Instance, new[] { RaceTag.Monster });
    public static Race Salamander = RegisterRace("salamander", Races.Graphics.Implementations.Monsters.Salamanders.Instance, new[] { RaceTag.Monster });
    public static Race Manti = RegisterRace("mantis", Races.Graphics.Implementations.Monsters.Mantis.Instance, new[] { RaceTag.Monster });
    public static Race EasternDragon = RegisterRace("easternDragon", Races.Graphics.Implementations.Monsters.EasternDragon.Instance, new[] { RaceTag.Monster });
    public static Race Catfish = RegisterRace("catfish", Races.Graphics.Implementations.Monsters.Catfish.Instance, new[] { RaceTag.Monster });
    public static Race Raptor = RegisterRace("raptor", Races.Graphics.Implementations.Monsters.Raptor.Instance, new[] { RaceTag.Monster });
    public static Race WarriorAnt = RegisterRace("warriorAnt", Races.Graphics.Implementations.Monsters.WarriorAnts.Instance, new[] { RaceTag.Monster });
    public static Race Gazelle = RegisterRace("gazelle", Races.Graphics.Implementations.Monsters.Gazelle.Instance, new[] { RaceTag.Monster });
    public static Race Earthworms = RegisterRace("earthworm", Races.Graphics.Implementations.Monsters.Earthworms.Instance, new[] { RaceTag.Monster });
    public static Race FeralLizard = RegisterRace("feralLizard", Races.Graphics.Implementations.Monsters.FeralLizards.Instance, new[] { RaceTag.Monster });
    public static Race Monitor = RegisterRace("monitor", Races.Graphics.Implementations.Monsters.Monitors.Instance, new[] { RaceTag.Monster });
    public static Race Schiwardez = RegisterRace("schiwardez", Races.Graphics.Implementations.Monsters.Schiwardez.Instance, new[] { RaceTag.Monster });
    public static Race Terrorbird = RegisterRace("terrorbird", Races.Graphics.Implementations.Monsters.Terrorbird.Instance, new[] { RaceTag.Monster });
    public static Race Dratopyr = RegisterRace("dratopyr", Races.Graphics.Implementations.Monsters.Dratopyr.Instance, new[] { RaceTag.Monster });
    public static Race FeralLion = RegisterRace("feralLion", Races.Graphics.Implementations.Monsters.FeralLions.Instance, new[] { RaceTag.Monster });
    public static Race Goodra = RegisterRace("goodra", Races.Graphics.Implementations.Monsters.Goodra.Instance, new[] { RaceTag.Monster });
    public static Race Whisp = RegisterRace("whisp", Races.Graphics.Implementations.Monsters.Whisp.Instance, new[] { RaceTag.Monster });

    public static Race Selicia = RegisterRace("selicia", Races.Graphics.Implementations.UniqueMercs.Selicia.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Vision = RegisterRace("vision", Races.Graphics.Implementations.UniqueMercs.Vision.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Ki = RegisterRace("ki", Races.Graphics.Implementations.UniqueMercs.Ki.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Scorch = RegisterRace("scorch", Races.Graphics.Implementations.UniqueMercs.Scorch.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Asura = RegisterRace("asura", Races.Graphics.Implementations.UniqueMercs.Asura.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Draco = RegisterRace("draco", Races.Graphics.Implementations.UniqueMercs.Draco.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Zoey = RegisterRace("zoey", Races.Graphics.Implementations.UniqueMercs.Zoey.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Abakhanskya = RegisterRace("abakhanskya", Races.Graphics.Implementations.UniqueMercs.Abakhanskya.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Zera = RegisterRace("zera", Races.Graphics.Implementations.UniqueMercs.Zera.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Auri = RegisterRace("auri", Races.Graphics.Implementations.UniqueMercs.Auri.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Erin = RegisterRace("erin", Races.Graphics.Implementations.UniqueMercs.Erin.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Salix = RegisterRace("salix", Races.Graphics.Implementations.UniqueMercs.Salix.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
}