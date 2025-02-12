using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ItemType
{
    Mace,
    Axe,
    Bow,
    CompoundBow,
    Helmet,
    BodyArmor,
    Gauntlet,
    Gloves,
    Shoes,

    FireBall,
    PowerBolt,
    LightningBolt,
    Shield,
    Mending,
    Speed,
    Valor,
    Predation,
    IceBlast,
    Pyre,

    //Warp
    //Magic Wall
    Poison,

    //Quicksand
    PreysCurse,
    Maw,
    Enlarge,
    Charm,
    Summon,

    //Raze,
    Diminishment,
    GateMaw,
    Resurrection,
}


public enum SpecialItems
{
    SeliciaWeapon,
    SuccubusWeapon,
    VisionWeapon,
    KiWeapon,
    ScorchWeapon,
    DracoWeapon,
    ZoeyWeapon,
    ZeraWeapon,
    AbakWeapon,
    AurilikaWeapon,
    SalixWeapon,
    ErinWeapon,
    ErinWings,
}


public class ItemRepository
{
    [OdinSerialize]
    private List<Item> _items;

    private List<Item> Items { get => _items; set => _items = value; }

    [OdinSerialize]
    private List<Item> _specialItems;

    private List<Item> SpecialItems { get => _specialItems; set => _specialItems = value; }

    [OdinSerialize]
    private Dictionary<Race,Item> _monsterItems;
    public Dictionary<Race,Item> MonsterItems { get => _monsterItems; set => _monsterItems = value; }

    [OdinSerialize]
    private Weapon _claws;

    public Weapon Claws { get => _claws; set => _claws = value; }

    [OdinSerialize]
    private Weapon _bite;

    public Weapon Bite { get => _bite; set => _bite = value; }

    [OdinSerialize]
    private List<Item> _allItems;

    private List<Item> AllItems { get => _allItems; set => _allItems = value; }

    public ItemRepository()
    {
        Claws = new Weapon("Claw", "innate claws", 0, 0, 2, 1);
        Bite = new Weapon("Bite", "bite attack", 0, 0, 4, 1);
        Items = new List<Item>
        {
            new Weapon(name: "Mace", description: "Moderate melee weapon", cost: 4, graphic: 0, damage: 4, range: 1, accuracyModifier: 1.25f),
            new Weapon(name: "Axe", description: "Strong melee weapon", cost: 12, graphic: 2, damage: 8, range: 1, accuracyModifier: 1),
            new Weapon(name: "Simple Bow", description: "Ranged weapon", cost: 6, graphic: 4, damage: 4, range: 5, accuracyModifier: 1.25f),
            new Weapon(name: "Compound Bow", description: "Advanced Ranged weapon", cost: 12, graphic: 6, damage: 6, range: 7, accuracyModifier: 1),
            new Accessory(name: "Helmet", description: "+8 agility", cost: 6, changedStat: (int)Stat.Agility, statBonus: 8),
            new Accessory(name: "Body Armor", description: "+6 endurance", cost: 6, changedStat: (int)Stat.Endurance, statBonus: 6),
            new Accessory(name: "Gauntlet", description: "+6 strength", cost: 8, changedStat: (int)Stat.Strength, statBonus: 6),
            new Accessory(name: "Gloves", description: "+6 dexterity", cost: 10, changedStat: (int)Stat.Dexterity, statBonus: 6),
            new Accessory(name: "Shoes", description: "+2 agility, +1 movement tile", cost: 6, changedStat: (int)Stat.Agility, statBonus: 2),


            new SpellBook("Fireball Book", "Allows the casting of Fireball", 30, 1, SpellType.Fireball),
            new SpellBook("Power Bolt Book", "Allows the casting of Power Bolt", 30, 1, SpellType.PowerBolt),
            new SpellBook("Lightning Bolt Book", "Allows the casting of Lightning Bolt", 30, 1, SpellType.LightningBolt),
            new SpellBook("Shield Book", "Allows the casting of Shield", 30, 1, SpellType.Shield),
            new SpellBook("Mending Book", "Allows the casting of Mending", 30, 1, SpellType.Mending),
            new SpellBook("Speed Book", "Allows the casting of Speed", 30, 1, SpellType.Speed),
            new SpellBook("Valor Book", "Allows the casting of Valor", 30, 1, SpellType.Valor),
            new SpellBook("Predation Book", "Allows the casting of Predation", 30, 1, SpellType.Predation),
            new SpellBook("Ice Blast Book", "Allows the casting of Ice Blast", 60, 2, SpellType.IceBlast),
            new SpellBook("Pyre Book", "Allows the casting of Pyre", 60, 2, SpellType.Pyre),
            //new SpellBook("Warp Book", "Allows the casting of Warp", 60, 2, SpellTypes.Warp),
            //new SpellBook("Magic Wall Book", "Allows the casting of Magic Wall", 60, 2, SpellTypes.MagicWall),           
            new SpellBook("Poison Book", "Allows the casting of Poison", 60, 2, SpellType.Poison),
            //new SpellBook("Quicksand Book", "Allows the casting of Quicksand", 90, 3, SpellTypes.Quicksand),
            new SpellBook("Prey's Curse Book", "Allows the casting of Prey's Curse", 90, 3, SpellType.PreysCurse),
            new SpellBook("Maw Book", "Allows the casting of Maw", 90, 3, SpellType.Maw),
            new SpellBook("Enlarge Book", "Allows the casting of Enlarge", 90, 3, SpellType.Enlarge),
            new SpellBook("Charm Book", "Allows the casting of Charm", 90, 3, SpellType.Charm),
            new SpellBook("Summon Book", "Allows the casting of Summon", 90, 3, SpellType.Summon),
            //new SpellBook("Raze Book", "Allows the casting of Raze", 150, 4, SpellTypes.Raze),
            new SpellBook("Diminishment Book", "Allows the casting of Diminishment", 150, 4, SpellType.Diminishment),
            new SpellBook("Gatemaw Book", "Allows the casting of Gatemaw", 150, 4, SpellType.GateMaw),
            new SpellBook("Resurrection Book", "Allows the casting of Resurrection", 150, 4, SpellType.Resurrection),
            new SpellBook("Reanimate Book", "Allows the casting of Reanimate", 150, 4, SpellType.Reanimate),
        };
        MonsterItems = new Dictionary<Race, Item>()
        {
            { Race.Vagrant, new Weapon(name: "Vagrant Stinger", description: "Jellyfish stinger", cost: 4, graphic: 0, damage: 3, range: 1) },
            { Race.Serpent, new Weapon(name: "Serpent Fangs", description: "Fangs", cost: 4, graphic: 0, damage: 3, range: 1) },
            { Race.Wyvern, new Weapon(name: "Wyvern Claws", description: "Claws", cost: 4, graphic: 0, damage: 5, range: 1) },
            { Race.YoungWyvern, new Weapon(name: "Young Wyvern Claws", description: "Claws", cost: 4, graphic: 0, damage: 4, range: 1) },
            { Race.Compy, new Weapon(name: "Puny Claws", description: "Puny Claws", cost: 4, graphic: 0, damage: 2, range: 1) },
            { Race.FeralShark, new Weapon(name: "Shark Jaws", description: "Shark Jaws", cost: 4, graphic: 0, damage: 4, range: 1) },
            { Race.FeralWolve, new Weapon(name: "Wolf Claws", description: "Wolf Claws", cost: 4, graphic: 0, damage: 4, range: 1) },
            { Race.DarkSwallower, new Weapon(name: "Dark Swallower Jaws", description: "Dark Swallower Jaws", cost: 4, graphic: 0, damage: 2, range: 1) },
            { Race.Cake, new Weapon(name: "Pointy Teeth", description: "Cake Jaws", cost: 4, graphic: 0, damage: 6, range: 1) },
            { Race.Harvester, new Weapon(name: "Harvester Scythes", description: "Scythes", cost: 4, graphic: 0, damage: 6, range: 1) },
            { Race.Collector, new Weapon(name: "Collector Maw", description: "Maw", cost: 4, graphic: 0, damage: 4, range: 1) },
            { Race.Voilin, new Weapon(name: "Voilin Jaws", description: "Jaws", cost: 4, graphic: 0, damage: 3, range: 1) },
            { Race.FeralBat, new Weapon(name: "Bat Jaws", description: "Jaws", cost: 4, graphic: 0, damage: 3, range: 1) },
            { Race.FeralFrog, new Weapon(name: "Frog Tongue", description: "Tongue", cost: 4, graphic: 0, damage: 6, range: 1) },
            { Race.Dragon, new Weapon(name: "Dragon Claws", description: "Claws", cost: 4, graphic: 0, damage: 6, range: 1) },
            { Race.Dragonfly, new Weapon(name: "Dragonfly Mandibles", description: "Mandibles", cost: 4, graphic: 0, damage: 3, range: 1) },
            { Race.TwistedVine, new Weapon(name: "Plant Bite", description: "Bite", cost: 4, graphic: 0, damage: 5, range: 1) },
            { Race.Fairy, new Weapon(name: "Fairy Spark", description: "Magical Attack", cost: 4, graphic: 0, damage: 5, range: 5, omniWeapon: true, magicWeapon: true) },
            { Race.FeralAnt, new Weapon(name: "Ant Mandibles", description: "Mandibles", cost: 4, graphic: 0, damage: 3, range: 1) },
            { Race.Gryphon, new Weapon(name: "Gryphon Claws", description: "Gryphon Claws", cost: 4, graphic: 0, damage: 6, range: 1) },
            { Race.SpitterSlug, new Weapon(name: "Slug Slime", description: "Slug Slime", cost: 4, graphic: 0, damage: 4, range: 5, omniWeapon: true) },
            { Race.SpringSlug, new Weapon(name: "Slug Headbash", description: "Headbash", cost: 4, graphic: 0, damage: 3, range: 1) },
            { Race.RockSlug, new Weapon(name: "Slug Body Slam", description: "Body Slam", cost: 4, graphic: 0, damage: 5, range: 1) },
            { Race.CoralSlug, new Weapon(name: "Slug Stinger", description: "Venomous Stinger", cost: 4, graphic: 0, damage: 4, range: 1) },
            { Race.Salamander, new Weapon(name: "Salamander Jaws", description: "Salamander Jaws", cost: 4, graphic: 0, damage: 4, range: 1) },
            { Race.Catfish, new Weapon(name: "Catfish Jaws", description: "Catfish Jaws", cost: 4, graphic: 0, damage: 3, range: 1) },
            { Race.Raptor, new Weapon(name: "Raptor Jaws", description: "Raptor Jaws", cost: 4, graphic: 0, damage: 3, range: 1) },
            { Race.WarriorAnt, new Weapon(name: "Warrior Ant Mandibles", description: "Warrior Ant Mandibles", cost: 4, graphic: 0, damage: 3, range: 1) },
            { Race.Gazelle, new Weapon(name: "Gazelle Headbash", description: "Gazelle Headbash", cost: 4, graphic: 0, damage: 4, range: 1) },
            { Race.Earthworms, new Weapon(name: "Earthworm Maw", description: "Earthworm Maw", cost: 4, graphic: 0, damage: 4, range: 1) },
            { Race.FeralLizard, new Weapon(name: "Lizard Jaws", description: "Lizard Jaws", cost: 4, graphic: 0, damage: 6, range: 1) },
            { Race.Monitor, new Weapon(name: "Monitor Lizard Claws", description: "Monitor Lizard Claws", cost: 4, graphic: 0, damage: 5, range: 1) },
            { Race.Schiwardez, new Weapon(name: "Schiwardez Jaws", description: "Schiwardez Jaws", cost: 4, graphic: 0, damage: 4, range: 1) },
            { Race.Terrorbird, new Weapon(name: "Terrorbird Beak", description: "Terrorbird Beak", cost: 4, graphic: 0, damage: 5, range: 1) },
            { Race.Dratopyr, new Weapon(name: "Dratopyr Jaws", description: "Dratopyr Jaws", cost: 4, graphic: 0, damage: 4, range: 1) },
            { Race.FeralLion, new Weapon(name: "Lion Fangs", description: "Serrated and pointy (Feline) Canines", cost: 4, graphic: 0, damage: 6, range: 1) },
            { Race.Goodra, new Weapon(name: "Goodra Slug Antenna", description: "Goodra's Power Whip", cost: 4, graphic: 0, damage: 5, range: 1) },
            { Race.Whisp, new Weapon(name: "Whisp fire", description: "Whisp's FoxFire", cost: 4, graphic: 0, damage: 5, range: 5, omniWeapon: true, magicWeapon: true) },
        };

        SpecialItems = new List<Item>()
        {
            new Weapon(name: "Selicia's Bite", description: "Bite attack", cost: 4, graphic: 0, damage: 10, range: 1),
            new Weapon(name: "Summoned Sword", description: "Imp that drops a sword on target", cost: 4, graphic: 0, damage: 4, range: 3, omniWeapon: true, lockedItem: true),
            new Weapon(name: "Vision's Bite", description: "Bite Attack", cost: 4, graphic: 0, damage: 8, range: 1),
            new Weapon(name: "Ki's Bite", description: "Bite Attack", cost: 4, graphic: 0, damage: 8, range: 1),
            new Weapon(name: "Scorch's Bite", description: "Bite Attack", cost: 4, graphic: 0, damage: 8, range: 1),
            new Weapon(name: "DRACO's Bite", description: "Bite Attack", cost: 4, graphic: 0, damage: 8, range: 1),
            new Weapon(name: "Zoey's Limbs", description: "Fist / Tail Attack", cost: 4, graphic: 0, damage: 6, range: 1),
            new Weapon(name: "Zera's Claws", description: "Claw Attack", cost: 4, graphic: 0, damage: 8, range: 1),
            new Weapon(name: "Abakhanskya's Bite", description: "Bite Attack", cost: 4, graphic: 0, damage: 8, range: 1),
            new Weapon(name: "Aurilika's Gohei", description: "A sacred talisman", cost: 4, graphic: 0, damage: 4, range: 1, accuracyModifier: 5f, lockedItem: true),
            new Weapon(name: "Salix's Staff", description: "A weighty magic staff", cost: 4, graphic: 0, damage: 6, range: 1, lockedItem: true),
            new Weapon(name: "Nyangel Claws", description: "Near-useless cat scratch!", cost: 4, graphic: 0, damage: 8, range: 1),
            new Accessory(name: "Nyangel Wings", description: "The softest, most delicious looking pair of wings you ever did see!\n+5 Willpower", cost: 6, changedStat: (int)Stat.Will, statBonus: 5),
        };


        AllItems = new List<Item>();
        AllItems.AddRange(Items);
        AllItems.AddRange(MonsterItems.Values);
        AllItems.AddRange(SpecialItems);
    }

    public int NumItems => Items.Count;

    public Item GetItem(int i) => Items[i];

    public Item GetMonsterItem(Race i)
    {
        Item item = MonsterItems.GetOrNull(i);
        if (item == null) Debug.Log(i);
        return item;
    }

    public Item GetSpecialItem(SpecialItems i) => SpecialItems[(int)i];

    public Item GetItem(ItemType i) => Items[(int)i];

    public ItemType GetItemType(Item item)
    {
        return (ItemType)Items.IndexOf(Items.Where(s => s.Name == item.Name).FirstOrDefault());
    }

    public Item GetRandomBook(int minTier = 1, int maxTier = 4, bool ignoreLimit = false)
    {
        return GetItem(GetRandomBookType(minTier, maxTier, ignoreLimit));
    }

    public int GetRandomBookType(int minTier = 1, int maxTier = 4, bool ignoreLimit = false)
    {
        if (ignoreLimit == false) maxTier = Mathf.Clamp(maxTier, 1, Config.MaxSpellLevelDrop);
        minTier = Mathf.Clamp(minTier, 1, maxTier);
        int min = (int)ItemType.FireBall;
        int max = (int)ItemType.Resurrection;
        if (minTier == 1) min = (int)ItemType.FireBall;
        if (minTier == 2) min = (int)ItemType.IceBlast;
        if (minTier == 3) min = (int)ItemType.PreysCurse;
        if (minTier == 4) min = (int)ItemType.Diminishment;
        if (maxTier == 1) max = (int)ItemType.Predation;
        if (maxTier == 2) max = (int)ItemType.Poison;
        if (maxTier == 3) max = (int)ItemType.Summon;
        if (maxTier >= 4) max = (int)ItemType.Resurrection;

        return State.Rand.Next(min, max + 1);
    }

    internal bool ItemIsUnique(Item item)
    {
        return MonsterItems.ContainsValue(item) || SpecialItems.Contains(item);
    }

    public bool ItemIsRangedWeapon(int i)
    {
        if (Items[i] is Weapon weapon)
        {
            if (weapon.Range > 1) return true;
        }

        return false;
    }

    public bool ItemIsRangedWeapon(Item item)
    {
        if (item is Weapon weapon)
        {
            if (weapon.Range > 1) return true;
        }

        return false;
    }

    public Item GetUpgrade(Item item)
    {
        if (item == GetItem(ItemType.Mace)) return GetItem(ItemType.Axe);
        if (item == GetItem(ItemType.Bow)) return GetItem(ItemType.CompoundBow);
        return null;
    }

    public Item GetNewItemType(Item item)
    {
        var ret = Items.Where(s => s.Name == item.Name).FirstOrDefault();
        if (ret == null) ret = MonsterItems.Values.Where(s => s.Name == item.Name).FirstOrDefault();
        if (ret == null) ret = SpecialItems.Where(s => s.Name == item.Name).FirstOrDefault();
        if (item.Name.Contains("Frog ???")) ret = MonsterItems.Values.Where(s => s.Name.Contains("Frog Tongue")).FirstOrDefault();
        if (ret == null)
        {
            if (item is Weapon weap)
            {
                if (weap.AccuracyModifier == 0) weap.ResetAccuracy();
            }

            return item;
        }

        return ret;
    }

    //public Weapon[] GetWeapons()
    //{
    //    return items.Append(Claws).Where(s => s is Weapon).Select(s => s as Weapon).ToArray();
    //}

    //public Weapon[] GetMonsterWeapons()
    //{
    //    return monsterItems.Where(s => s is Weapon).Select(s => s as Weapon).ToArray();
    //}

    public List<Item> GetAllItems()
    {
        return AllItems;
    }
}