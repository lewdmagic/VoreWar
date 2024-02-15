using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using DaVikingCode.AssetPacker;
using UnityEngine;

// Todo evil global is evil
public static class State
{
    private static int _saveErrors = 0;
    public const string Version = "42";
    public static World World;
    public static Rand Rand = new Rand();
    public static NameGenerator NameGen;
    public static GameManager GameManager;
    public static AssimilateList AssimilateList;
    public static List<RandomizeList> RandomizeLists;

    internal static EventList EventList;

    internal static RaceSettings RaceSettings;

    public static bool TutorialMode;
    public static bool Warned = false;

    public static string SaveDirectory;
    public static string StorageDirectory;
    public static string MapDirectory;

    public static SpriteManager SpriteManager;

    public static int RaceSlot;
    public static string RaceSaveDataName;

    /// <summary>
    ///     Initiate that's called from a thread in a controlled manner.
    ///     Static initiation can (and did) lead to circular refference in static initialization
    ///     which creates a hard to diagnose exception such as TypeInitializationException
    ///     and overall unpredictable and inconsistent order of execution
    ///     https://stackoverflow.com/questions/47160605/why-would-my-initialized-static-property-be-null-when-i-access-it
    ///     As the stackoverflow answer nicely put it: Nuke it from orbit
    /// </summary>
    internal static void CarefulIntatntiate()
    {
        NameGen = new NameGenerator();
    }

    static State()
    {
        if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            SaveDirectory = Application.persistentDataPath + $"Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = Application.persistentDataPath + Path.DirectorySeparatorChar;
            MapDirectory = Application.persistentDataPath + $"Maps{Path.DirectorySeparatorChar}";
        }
        else
        {
            SaveDirectory = $"UserData{Path.DirectorySeparatorChar}Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = $"UserData{Path.DirectorySeparatorChar}";
            MapDirectory = $"UserData{Path.DirectorySeparatorChar}Maps{Path.DirectorySeparatorChar}";
        }

        try
        {
            Directory.CreateDirectory(StorageDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(MapDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(SaveDirectory.TrimEnd(new char[] { '\\', '/' }));
        }
        catch
        {
            SaveDirectory = Application.persistentDataPath + $"Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = Application.persistentDataPath + Path.DirectorySeparatorChar;
            MapDirectory = Application.persistentDataPath + $"Maps{Path.DirectorySeparatorChar}";
            Directory.CreateDirectory(StorageDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(MapDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(SaveDirectory.TrimEnd(new char[] { '\\', '/' }));
        }


        try
        {
            if (File.Exists($"{StorageDirectory}males.txt") == false) File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}males.txt", $"{StorageDirectory}males.txt");
            if (File.Exists($"{StorageDirectory}females.txt") == false) File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}females.txt", $"{StorageDirectory}females.txt");
            if (File.Exists($"{StorageDirectory}monsters.txt") == false) File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}monsters.txt", $"{StorageDirectory}monsters.txt");
            if (File.Exists($"{StorageDirectory}events.txt") == false) File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}events.txt", $"{StorageDirectory}events.txt");
            if (File.Exists($"{StorageDirectory}armyNames.txt") == false) File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}armyNames.txt", $"{StorageDirectory}armyNames.txt");
            if (File.Exists($"{StorageDirectory}femaleFeralLions.txt") == false) File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleFeralLions.txt", $"{StorageDirectory}femaleFeralLions.txt");
            if (File.Exists($"{StorageDirectory}maleFeralLions.txt") == false) File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleFeralLions.txt", $"{StorageDirectory}maleFeralLions.txt");
            if (File.Exists($"{StorageDirectory}customTraits.txt") == false) File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}customTraits.txt", $"{StorageDirectory}customTraits.txt");
            if (File.Exists($"{StorageDirectory}femaleAabayx.txt") == false) File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleAabayx.txt", $"{StorageDirectory}femaleAabayx.txt");
            if (File.Exists($"{StorageDirectory}maleAabayx.txt") == false) File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleAabayx.txt", $"{StorageDirectory}maleAabayx.txt");
        }
        catch
        {
            Debug.Log("Initial setup failed!");
        }

        FlagLoader.FlagLoader flagLoader = new FlagLoader.FlagLoader();
        flagLoader.LoadFlags();
        EventList = new EventList();
        AssimilateList = new AssimilateList();

        Encoding encoding = Encoding.GetEncoding("iso-8859-1");
        List<string> lines;
        RandomizeLists = new List<RandomizeList>();
        if (File.Exists($"{StorageDirectory}customTraits.txt"))
        {
            var logFile = File.ReadAllLines($"{StorageDirectory}customTraits.txt", encoding);
            if (logFile.Any())
            {
                lines = new List<string>(logFile);
                int count = 0;
                lines.ForEach(line =>
                {
                    count++;
                    RandomizeList custom = new RandomizeList();
                    line = new string(line
                        .Where(c => !char.IsWhiteSpace(c)).ToArray());
                    string[] strings = line.Split(',');
                    if (strings.Length == 4)
                    {
                        custom.ID = int.Parse(strings[0]);
                        custom.Name = strings[1];
                        custom.Chance = float.Parse(strings[2], new CultureInfo("en-US"));
                        custom.RandomTraits = strings[3].Split('|').ToList().ConvertAll(s => (TraitType)int.Parse(s));
                        RandomizeLists.Add(custom);
                    }
                });
            }
        }
    }

    public static void SaveEditedRaces()
    {
        try
        {
            byte[] bytes = SerializationUtility.SerializeValue(RaceSettings, DataFormat.Binary);
            File.WriteAllBytes(RaceSaveDataName, bytes);
        }
        catch
        {
            Debug.LogWarning("Failed to properly save edited races!");
        }
    }

    public static void LoadRaceData()
    {
        RaceSlot = PlayerPrefs.GetInt("RaceEditorSlot", 1);
        ChangeRaceSlotUsed(RaceSlot);
    }

    public static void ChangeRaceSlotUsed(int num)
    {
        RaceSlot = num;
        PlayerPrefs.SetInt("RaceEditorSlot", num);
        if (RaceSlot <= 1)
            RaceSaveDataName = $"{StorageDirectory}EditedRaces.dat";
        else if (RaceSlot == 2)
            RaceSaveDataName = $"{StorageDirectory}EditedRaces2.dat";
        else if (RaceSlot == 3) RaceSaveDataName = $"{StorageDirectory}EditedRaces3.dat";
        LoadEditedRaces();
    }

    public static void LoadEditedRaces()
    {
        try
        {
            if (File.Exists(RaceSaveDataName))
            {
                byte[] bytes = File.ReadAllBytes(RaceSaveDataName);
                RaceSettings = SerializationUtility.DeserializeValue<RaceSettings>(bytes, DataFormat.Binary);
                GameManager.StartMode.miscText.text = "Successfully read race settings";
                RaceSettings.Sanitize();
            }
            else
            {
                RaceSettings = new RaceSettings();
                GameManager.StartMode.miscText.text = "No modified race settings found, using default";
            }
        }
        catch
        {
            RaceSettings = new RaceSettings();
            GameManager.StartMode.miscText.text = "Failed to properly read race settings";
        }
    }


    public static void Save(string filename)
    {
        try
        {
            for (int i = 0; i < 3; i++)
            {
                if (filename.EndsWith("/") || filename.EndsWith("\\"))
                    filename = filename.Remove(filename.Length - 1, 1);
                else
                    break;
            }

            World.SaveVersion = Version;
            if (GameManager.CurrentScene == GameManager.TacticalMode)
            {
                GameManager.CameraController.SaveTacticalCamera();
                World.TacticalData = GameManager.TacticalMode.Export();
            }
            else
                World.TacticalData = null;


            byte[] bytes = SerializationUtility.SerializeValue(World, DataFormat.Binary);
            File.WriteAllBytes(filename, bytes);

            //byte[] readable = SerializationUtility.SerializeValue(World, DataFormat.JSON);
            //File.WriteAllBytes(filename, readable);
        }
        catch
        {
            _saveErrors++;
            if (_saveErrors < 3)
            {
                GameManager.CreateMessageBox($"Unable to save properly, {filename} didn't work (will only warn 3 times in a single session)");
            }
            else if (_saveErrors == 3)
            {
                GameManager.CreateMessageBox($"Unable to save properly, {filename} didn't work (will no longer warn you this session)");
            }
        }
    }

    public static World PreviewSave(string filename)
    {
        if (filename.EndsWith("/") || filename.EndsWith("\\")) filename = filename.Remove(filename.Length - 1, 1);
        if (!File.Exists(filename))
        {
            return null;
        }

        World tempWorld;
        try
        {
            byte[] bytes = File.ReadAllBytes(filename);
            tempWorld = SerializationUtility.DeserializeValue<World>(bytes, DataFormat.Binary);
            return tempWorld;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static void Load(string filename, bool tutorial = false)
    {
        if (filename.EndsWith("/") || filename.EndsWith("\\")) filename = filename.Remove(filename.Length - 1, 1);
        if (!File.Exists(filename))
        {
            GameManager.CreateMessageBox("Couldn't find the saved file");
            return;
        }

        try
        {
            GameManager.StrategyMode.ClearData();
            GameManager.StrategyMode.CleanUpLingeringWindows();
            if (tutorial == false) GameManager.SwitchToMainMenu();
            byte[] bytes = File.ReadAllBytes(filename);
            World = SerializationUtility.DeserializeValue<World>(bytes, DataFormat.Binary);

            if (World.Empires != null)
            {
                World.MainEmpiresWritable = World.Empires.ToList();
                World.RefreshEmpires();
            }

            if (tutorial)
            {
                var catEmp = World.GetEmpireOfRace(Race.Cat);
                var impEmp = World.GetEmpireOfRace(Race.Imp);

                catEmp.Armies[0].SetEmpire(catEmp);
                impEmp.Armies[0].SetEmpire(impEmp);
                TutorialMode = true;
            }
            else
            {
                TutorialMode = false;
            }

            // New version check. Initially considered making an array of applicable versions to bridge gaps, but just grabbing the version number should be plenty
            string versionStr = System.Text.RegularExpressions.Regex.Match(World.SaveVersion, @"\d+").Value;
            int version = int.Parse(versionStr);


            VillageBuildingList.SetBuildings(World.CrazyBuildings);
            if (version < 12)
            {
                World = null;
                GameManager.CreateMessageBox("This save file is from before version 12.  I took the liberty of doing a clean sweep when I added the new garrisons to improve the code quality. Sorry.  You can still load .map files from before version 12 though.");
                return;
            }

            Config.World = World.ConfigStorage;
            GameManager.Menu.Options.LoadFromStored();
            GameManager.Menu.CheatMenu.LoadFromStored();

            if (World.MercenaryHouses == null) World.MercenaryHouses = new MercenaryHouse[0];

            foreach (MercenaryHouse house in World.MercenaryHouses)
            {
                if (house.Mercenaries != null)
                {
                    foreach (var merc in house.Mercenaries)
                    {
                        merc.Unit.InitializeTraits();
                    }
                }
            }


            if (World.Claimables == null) World.Claimables = new ClaimableBuilding[0];

            //Always runs for new versions           
            if (World.SaveVersion != Version && World.AllActiveEmpires != null)
            {
                if (World.GetEmpireOfSide(Side.RebelSide) == null)
                {
                    World.MainEmpiresWritable.Add(new Empire(new Empire.ConstructionArgs(null, Side.RebelSide, Color.red, new Color(.6f, 0, 0), 5, StrategyAIType.Basic, TacticalAIType.Full, 700, 16, 16)));
                    World.RefreshEmpires();
                }
                else
                {
                    World.GetEmpireOfSide(Side.RebelSide).Name = "Rebels";
                    if (World.EmpireOrder.Where(s => Equals(s.Side, Side.RebelSide)).Any() == false) World.EmpireOrder.Add(World.GetEmpireOfSide(Side.RebelSide));
                }

                if (World.GetEmpireOfSide(Side.BanditSide) == null)
                {
                    World.MainEmpiresWritable.Add(new Empire(new Empire.ConstructionArgs(null, Side.BanditSide, Color.red, new Color(.6f, 0, 0), 7, StrategyAIType.Basic, TacticalAIType.Full, 701, 16, 16)));
                    World.RefreshEmpires();
                }
                else
                {
                    World.GetEmpireOfSide(Side.BanditSide).Name = "Bandits";
                }

                /*         if (World.GetEmpireOfSide(702) == null)
                        {
                            World.MainEmpires.Add(new Empire(new Empire.ConstructionArgs(702, Color.red, new Color(.6f, 0, 0), 5, StrategyAIType.Basic, TacticalAIType.Full, 702, 16, 16)));
                            World.RefreshEmpires();
                        }
                        else
                        {
                            World.GetEmpireOfSide(702).Name = "Outcasts";
                            if (World.EmpireOrder.Where(s => s.Side == 702).Any() == false)
                                World.EmpireOrder.Add(World.GetEmpireOfSide(702));
                        } */
                if (version < 30 + 1)
                {
                    if (World.AllActiveEmpires != null)
                    {
                        foreach (Village village in World.Villages)
                        {
                            village.ConvertToMultiRace();
                        }
                    }
                }

                ItemRepository newRepo = new ItemRepository();
                World.ItemRepository = newRepo;

                foreach (var unit in StrategicUtilities.GetAllUnits())
                {
                    unit.UpdateItems(newRepo);
                    unit.ReloadTraits();
                }

                foreach (Empire empire in World.AllActiveEmpires)
                {
                    foreach (Army army in empire.Armies)
                    {
                        foreach (Unit unit in army.Units)
                        {
                            if (!Equals(unit.Side, army.Side)) unit.Side = army.Side;
                            if (unit.BodySize < 0) //Can take this out later, was a fix for 14H
                                unit.BodySize = 0;
                        }
                    }
                }


                if (Config.MaxSpellLevelDrop == 0) Config.World.MaxSpellLevelDrop = 4;
            }

            if (version < 18 + 1)
            {
                if (Config.LeaderLossLevels == 0) Config.World.LeaderLossLevels = 1;
                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        if (Equals(unit.Race, Race.Goblin)) //Re-randomize because the number of options has dropped
                            unit.EyeType = Rand.Next(3);
                    }
                }
            }

            if (version < 20 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        int oldSpeed = unit.GetStatBase(Stat.Mind);
                        unit.ModifyStat(Stat.Agility, Math.Max(oldSpeed - 10, 0));
                        unit.ModifyStat(Stat.Mind, unit.Level + 10 - oldSpeed);
                    }
                }
            }

            if (version < 21 + 1)
            {
                if (World.Villages != null)
                {
                    foreach (Village village in World.Villages)
                    {
                        if (village.buildings.Contains(VillageBuilding.empty)) //Removes Sub pens
                        {
                            village.buildings.Remove(VillageBuilding.empty);
                        }
                    }
                }

                if (World.Relations != null)
                {
                    RelationsManager.ResetRelations();
                }
            }

            if (version < 21D + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        if (empire.StrategicAI == null) continue;
                        foreach (Army army in empire.Armies)
                        {
                            foreach (Unit unit in army.Units)
                            {
                                StrategicUtilities.SetAIClass(unit);
                            }
                        }
                    }

                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        foreach (Army army in empire.Armies)
                        {
                            foreach (Unit unit in army.Units)
                            {
                                if (Equals(unit.Race, Race.Lizard)) //Adjustment for the added clothing item
                                    if (unit.ClothingType == 4)
                                        unit.ClothingType = 5;
                                    else if (unit.ClothingType == 5) unit.ClothingType = 6;
                            }
                        }
                    }
                }
            }

            if (version < 22 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire emp in World.AllActiveEmpires)
                    {
                        emp.Name = emp.Race.ToString();
                    }
                }
            }

            if (version < 26 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        foreach (Army army in empire.Armies)
                        {
                            foreach (Unit unit in army.Units)
                            {
                                if (Equals(unit.Race, Race.Lizard)) //Adjustment for the added clothing item
                                    if (unit.ClothingType >= 5)
                                        unit.ClothingType++;
                            }
                        }
                    }
                }
            }

            if (version < 26 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        foreach (Army army in empire.Armies)
                        {
                            foreach (Unit unit in army.Units)
                            {
                                if (Equals(unit.Race, Race.Abakhanskya)) //Adjustment for the added clothing item
                                {
                                    unit.FixedGear = true;
                                    unit.Items[0] = World.ItemRepository.GetSpecialItem(SpecialItems.AbakWeapon);
                                }
                            }
                        }
                    }
                }
            }

            if (version < 26D + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        if (Equals(empire.Leader?.Race, Race.Bee)) empire.Leader.ClothingType = 6;
                    }
                }
            }

            if (version < 27 + 1)
            {
                Config.World.Toggles["Defections"] = true;
            }

            if (version < 28 + 1)
            {
                Config.World.OralWeight = 40;
                Config.World.BreastWeight = 40;
                Config.World.CockWeight = 40;
                Config.World.TailWeight = 40;
                Config.World.UnbirthWeight = 40;
                Config.World.AnalWeight = 40;
            }

            if (version < 28 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        foreach (Army army in empire.Armies)
                        {
                            foreach (Unit unit in army.Units)
                            {
                                if (Equals(unit.Race, Race.Succubus))
                                {
                                    if (unit.ClothingType2 == 3) unit.ClothingType2 = 2;
                                }
                            }
                        }
                    }
                }
            }

            if (version < 28 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        foreach (Army army in empire.Armies)
                        {
                            army.NameArmy(empire);
                        }
                    }
                }
            }

            if (version < 29 + 1)
            {
                World.ConfigStorage.OverallMonsterCapModifier = 1;
                World.ConfigStorage.OverallMonsterSpawnRateModifier = 1;
            }

            if (version < 30 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    var raceData = RaceFuncs.GetRace(Race.Bee);
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        if (Equals(unit.Race, Race.Bee)) raceData.RandomCustomCall(unit);
                    }
                }
            }

            if (version < 30 + 1)
            {
                Config.World.AutoSurrenderChance = 1;
            }

            if (version < 31 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        if (empire.CapitalCity != null)
                            empire.ReplacedRace = empire.CapitalCity.OriginalRace;
                        else
                            empire.ReplacedRace = empire.Race;
                    }
                }
            }

            if (version < 32 + 1)
            {
                World.ConfigStorage.StartingPopulation = 99999;
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        if (empire.StrategicAI != null && empire.StrategicAI is StrategicAI ai)
                        {
#pragma warning disable CS0612 // Type or member is obsolete
                            if (ai.StrongerAI)
#pragma warning restore CS0612 // Type or member is obsolete
                                ai.CheatLevel = 1;
                        }

                        if (empire.CapitalCity != null)
                            empire.ReplacedRace = empire.CapitalCity.OriginalRace;
                        else
                            empire.ReplacedRace = empire.Race;
                    }
                }
            }

            if (version < 34 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        if (Equals(unit.Race, Race.Bat) || Equals(unit.Race, Race.Equine))
                        {
                            unit.TotalRandomizeAppearance();
                        }
                    }
                }
            }


            if (version < 34 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        if (empire.CapitalCity != null)
                            empire.ReplacedRace = empire.CapitalCity.OriginalRace;
                        else
                            empire.ReplacedRace = empire.Race;
                    }
                }
            }

            if (version < 35 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    World.UpdateBanditLimits();
                }
            }

            if (version < 37 + 1)
            {
                foreach (var unit in StrategicUtilities.GetAllUnits())
                {
                    if (unit.HasVagina == false)
                    {
                        if (unit.HasBreasts && !unit.HasDick)
                            unit.HasVagina = true;
                        else if (!unit.HasBreasts && unit.HasDick)
                            unit.HasVagina = false;
                        else if (Config.World.GetValue("HermsCanUB"))
                            unit.HasVagina = true;
                        else
                            unit.HasVagina = false;
                    }
                }
            }

            if (version < 38 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        if (unit.Pronouns == null)
                        {
                            unit.GeneratePronouns();
                        }
                    }
                }
                else
                {
                    foreach (var unit in World.TacticalData.Units)
                    {
                        if (unit.Unit.Pronouns == null)
                        {
                            unit.Unit.GeneratePronouns();
                        }
                    }
                }
            }


            if (version < 38 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        if (unit.GetGender() == Gender.Hermaphrodite || unit.GetGender() == Gender.Gynomorph)
                        {
                            unit.HasVagina = Config.HermsCanUb;
                        }
                    }
                }
            }

            if (version < 39 + 1)
            {
                World.ConfigStorage.FogDistance = 2;

                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        if (Equals(unit.Race, Race.Human))
                        {
                            unit.RandomizeAppearance();
                        }
                    }
                }
            }

            if (version < 40 + 1)
            {
                if (World.TacticalData != null)
                {
                    foreach (var unit in World.TacticalData.Units)
                    {
                        unit.ModeQueue = new List<KeyValuePair<int, float>>();
                        unit.Unit.FixedSide = Side.TrueNoneSide;
                    }
                }

                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        unit.FixedSide = Side.TrueNoneSide;
                    }
                }
            }


            if (version < 41 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire emp in World.AllActiveEmpires)
                    {
                        foreach (Army army in emp.Armies)
                        {
                            army.Impassables = new List<StrategicTileType>()
                                { StrategicTileType.Mountain, StrategicTileType.SnowMountain, StrategicTileType.Water, StrategicTileType.Lava, StrategicTileType.Ocean, StrategicTileType.BrokenCliffs };
                        }
                    }
                }
            }

            if (World.TacticalData != null)
            {
                foreach (var unit in World.TacticalData.Units)
                {
                    if (unit.ModeQueue == null) unit.ModeQueue = new List<KeyValuePair<int, float>>();
                }
            }

            if (World.AllActiveEmpires != null)
            {
                foreach (Empire emp in World.AllActiveEmpires)
                {
                    if (RaceFuncs.IsRebelOrBandit4(emp.Side)) continue;
                    var raceFlags = RaceSettings.GetRaceTraits(emp.Race);
                    if (raceFlags != null)
                    {
                        if (raceFlags.Contains(TraitType.Prey)) emp.CanVore = false;
                    }
                }

                foreach (Empire emp in World.MainEmpires)
                {
                    if (RaceFuncs.IsRebelOrBandit4(emp.Side)) continue;
                    if (RaceSettings.Exists(emp.Race))
                    {
                        emp.BannerType = RaceSettings.Get(emp.Race).BannerType;
                    }
                    else
                        emp.BannerType = 0;
                }

                foreach (var unit in StrategicUtilities.GetAllUnits())
                {
                    unit.InitializeTraits();
                }
            }

            if (World.Villages != null)
            {
                foreach (var village in World.Villages)
                {
                    if (village.FarmCount <= 0) village.UpdateFarms(8);
                    village.UpdateNetBoosts();
                }
            }


            if (Config.World.ArmyMp == 0) Config.World.ArmyMp = 3;

            if (Config.World.MaxArmies == 0) Config.World.MaxArmies = 12;

            if (Config.World.VillagersPerFarm == 0) Config.World.VillagersPerFarm = 6;

            if (Config.World.SoftLevelCap == 0) Config.World.SoftLevelCap = 999999;

            if (Config.World.HardLevelCap == 0) Config.World.HardLevelCap = 999999;

            if (Config.World.GoldMineIncome == 0) Config.World.GoldMineIncome = 40;

            if (Config.World.TacticalTerrainFrequency == 0) Config.World.TacticalTerrainFrequency = 10;

            if (Config.World.TacticalWaterValue == 0) Config.World.TacticalWaterValue = .29f;

            World.ItemRepository = new ItemRepository();

            if (version < 41 + 1)
            {
                if (Config.World.BaseCritChance == 0) Config.World.BaseCritChance = .05f;

                if (Config.World.CritDamageMod == 0) Config.World.CritDamageMod = 1.5f;

                if (Config.World.BaseGrazeChance == 0) Config.World.BaseGrazeChance = .05f;

                if (Config.World.GrazeDamageMod == 0) Config.World.GrazeDamageMod = .3f;
            }

            bool pureTactical = false;
            if (World.MainEmpires != null) //Is the detector for a pure tactical game.
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        empire.LoadFix(); //Compatibility Temporary fix to bridge the gap between versions
                    }
                }
                else
                {
                    foreach (Empire empire in World.MainEmpires)
                    {
                        empire.LoadFix(); //Compatibility Temporary fix to bridge the gap between versions
                    }
                }

                foreach (Empire empire in World.AllActiveEmpires)
                {
                    foreach (Army army in empire.Armies)
                    {
                        foreach (Unit unit in army.Units)
                        {
                            unit.ReloadTraits();
                        }
                    }

                    /*
                    foreach (Army army in empire.Armies)
                    {
                        var toRemove = new List<Unit>();
                        foreach (Unit unit in army.Units)
                        {
                            if (unit.Race == null)
                            {
                                toRemove.Add(unit);
                            }
                            else
                            {
                                unit.ReloadTraits();
                            }
                        }

                        foreach (var oneToRemove in toRemove)
                        {
                            army.Units.Remove(oneToRemove);
                        }
                    }
                     */
                }

                if (World.Relations == null) RelationsManager.ResetRelations();
                GameManager.ClearPureTactical();
                GameManager.SwitchToStrategyMode(true);
                GameManager.StrategyMode.GenericSetup();
                GameManager.StrategyMode.CheckIfOnlyAIPlayers();

                MercenaryHouse.UpdateStaticStock();
            }
            else //If Pure Tactical
            {
                //These two lines are there to catch a pure tactical game with the summon spell.
                ItemRepository newRepo = new ItemRepository();
                World.ItemRepository = newRepo;

                Config.WatchAIBattles = true;
                pureTactical = true;
            }

            if (World.TacticalData != null)
            {
                GameManager.SwitchToTacticalOnLoadedGame();
                GameManager.TacticalMode.LoadData(World.TacticalData);
                if (pureTactical)
                {
                    GameManager.TacticalMode.RefreshPureTacticalTraits();
                    GameManager.TacticalMode.ForceUpdate();
                }
            }


            //var units = StrategicUtilities.GetAllUnits();
            //Debug.Log("LOADED");
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            GameManager.CreateMessageBox("Encountered an error when trying to load the save");
            return;
        }
    }
}