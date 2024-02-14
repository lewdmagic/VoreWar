using Assets.Scripts.Utility.Stored;
using MapObjects;
using OdinSerializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TacticalAIType
{
    None,
    Legacy,
    Full,
}

public enum StrategyAIType
{
    None,
    Passive,
    Legacy,
    Basic,
    Advanced,
    Cheating1,
    Cheating2,
    Cheating3,
    Monster = 100,
    Goblin,
}

public class StrategicCreationArgs
{
    internal Dictionary<Race, Empire.ConstructionArgs> empireArgs;
    internal Dictionary<Race, bool> CanVore;
    internal Dictionary<Race, int> TurnOrder;
    internal Dictionary<Race, int> Team;
    internal bool crazyBuildings;
    internal int MercCamps;
    internal int GoldMines;

    internal WorldGenerator.MapGenArgs MapGen;

    public StrategicCreationArgs(int length)
    {
        empireArgs = new Dictionary<Race, Empire.ConstructionArgs>();
        CanVore = new Dictionary<Race, bool>();
        TurnOrder = new Dictionary<Race, int>();
        Team = new Dictionary<Race, int>();
        crazyBuildings = false;
        MercCamps = 0;
        GoldMines = 0;

        MapGen = new WorldGenerator.MapGenArgs();
    }
}

public class CreateStrategicGame : MonoBehaviour
{
    public Transform ScrollViewContent;
    public StartEmpireUI BasicEmpire;

    public StartEmpireUI AllEmpires;

    private Dictionary<Race, StartEmpireUI> Empires = new Dictionary<Race, StartEmpireUI>();

    public Dropdown StrategicAutoSize;
    public InputField StrategicX;
    public InputField StrategicY;
    public InputField TacticalX;
    public InputField TacticalY;
    public Toggle AutoScaleTactical;
    public Toggle CrazyBuildings;
    public InputField BaseExpRequired;
    public InputField ExpIncreaseRate;
    public InputField VillageIncomeRate;
    public InputField VillagersPerFarm;
    public InputField SoftLevelCap;
    public InputField HardLevelCap;
    public Toggle FactionLeaders;
    public Toggle LeadersAutoGainLeadership;
    public Dropdown VictoryCondition;
    public InputField StartingVillagePopulation;

    public InputField LeaderLossLevels;
    public Slider LeaderLossExpPct;

    public InputField GoldMines;
    public InputField MercenaryHouses;

    public Toggle SpawnTeamsTogether;
    public Toggle FirstTurnArmiesIdle;
    public Toggle CapitalGarrisonCapped;

    public Button ClearPickedMap;

    public Button AddRaces;

    public InputField StartingGold;

    public InputField GoldMineIncome;

    public GameObject EmpiresTab;
    public GameObject GameSettingsTab;
    public GameObject MapGenTab;

    public Dropdown MapGenType;
    public Toggle MapGenPoles;
    public Toggle MapGenExcessBridges;
    public Slider MapGenWaterPct;
    public Slider MapGenTemperature;
    public Slider MapGenForests;
    public Slider MapGenSwamps;
    public Slider MapGenHills;

    public InputField ArmyUpkeep;

    public InputField AbandonedVillages;


    public RacePanel RaceUI;

    public Text TooltipText;

    internal Map map;

    private string mapString;

    public void ClearState()
    {
        State.World = null;
        foreach (var emp in Empires)
        {
            emp.Value.PrimaryColor.GetComponent<Image>().color = ColorFromIndex(emp.Value.PrimaryColor.value);
            emp.Value.SecondaryColor.GetComponent<Image>().color = GetDarkerColor(ColorFromIndex(emp.Value.SecondaryColor.value));
        }
    }

    private void Awake()
    {
        int i = 0;
        foreach (Race race in RaceFuncs.MainRaceEnumerable())
        {
            IRaceData raceData = Races2.GetRace(race);
            StartEmpireUI empire = Instantiate(BasicEmpire, ScrollViewContent);
            empire.name = raceData.SingularName(Gender.Male);
            empire.Text.text = raceData.SingularName(Gender.Male);
            empire.gameObject.SetActive(false);
            empire.RemoveButton.onClick.AddListener(() => RemoveRace(empire));
            empire.Team.text = i.ToString();
            empire.TurnOrder.text = i.ToString();
            empire.PrimaryColor.value = i;
            empire.SecondaryColor.value = i;
            empire.VillageCount.text = "0";

            Empires[race] = empire;
            i++;
        }

        // for (int i = 0; i < 30; i++)
        // {
        //     Race race = RaceFuncs.IntToRace(i);
        //     IRaceData raceData = Races.GetRace(race);
        //     StartEmpireUI empire = Instantiate(BasicEmpire, ScrollViewContent);
        //     empire.name = raceData.SingularName(Gender.Male);
        //     empire.Text.text = raceData.SingularName(Gender.Male);
        //     empire.gameObject.SetActive(false);
        //     empire.RemoveButton.onClick.AddListener(() => RemoveRace(empire));
        //     empire.Team.text = i.ToString();
        //     empire.TurnOrder.text = i.ToString();
        //     empire.PrimaryColor.value = i;
        //     empire.SecondaryColor.value = i;
        //     empire.VillageCount.text = "0";
        //     
        //     Empires[race] = empire;
        // }

        // int i = 0;
        // foreach (Race race in RaceFuncs.RaceEnumerable())
        // {
        //     //Race race = RaceFuncs.IntToRace(i);
        //     IRaceData raceData = Races.GetRace(race);
        //     StartEmpireUI empire = Instantiate(BasicEmpire, ScrollViewContent);
        //     empire.name = raceData.SingularName(Gender.Male);
        //     empire.Text.text = raceData.SingularName(Gender.Male);
        //     empire.gameObject.SetActive(false);
        //     empire.RemoveButton.onClick.AddListener(() => RemoveRace(empire));
        //     empire.Team.text = i.ToString();
        //     empire.TurnOrder.text = i.ToString();
        //     empire.PrimaryColor.value = i;
        //     empire.SecondaryColor.value = i;
        //     
        //     Empires[race] = empire;
        //
        //     if (i == 29) break;
        //     
        //     i++;
        // }
    }

    public void SaveSettings(int slot)
    {
        CreateStrategicStored stored = new CreateStrategicStored();
        FieldInfo[] fields = GetType().GetFields();
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(InputField))
            {
                stored.InputFields[field.Name] = ((InputField)field.GetValue(this)).text;
            }

            if (field.FieldType == typeof(Dropdown))
            {
                stored.Dropdowns[field.Name] = ((Dropdown)field.GetValue(this)).value;
            }

            if (field.FieldType == typeof(Toggle))
            {
                stored.Toggles[field.Name] = ((Toggle)field.GetValue(this)).isOn;
            }

            if (field.FieldType == typeof(Slider))
            {
                stored.Sliders[field.Name] = ((Slider)field.GetValue(this)).value;
            }
        }

        foreach (var entry in Empires)
        {
            Race race = entry.Key;
            StartEmpireUI empire = entry.Value;

            var data = new EmpireData
            {
                AIPlayer = empire.AIPlayer.isOn,
                CanVore = empire.CanVore.isOn,
                MaxArmySize = empire.MaxArmySize.value,
                MaxGarrisonSize = empire.MaxGarrisonSize.value,
                PrimaryColor = empire.PrimaryColor.value,
                SecondaryColor = empire.SecondaryColor.value,
                Team = empire.Team.text,
                TurnOrder = empire.TurnOrder.text,
                VillageCount = empire.VillageCount.text,
                StrategicAI = empire.StrategicAI.value,
                TacticalAI = empire.TacticalAI.value,
            };

            stored.Empires[race] = data;
        }

        byte[] bytes = SerializationUtility.SerializeValue(stored, DataFormat.JSON);
        File.WriteAllBytes($"{State.StorageDirectory}createsettings{slot}.txt", bytes);
    }

    public void LoadSettings(int slot)
    {
        if (File.Exists($"{State.StorageDirectory}createsettings{slot}.txt") == false) return;
        byte[] bytes = File.ReadAllBytes($"{State.StorageDirectory}createsettings{slot}.txt");
        CreateStrategicStored stored = SerializationUtility.DeserializeValue<CreateStrategicStored>(bytes, DataFormat.JSON);
        FieldInfo[] fields = GetType().GetFields();
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(InputField))
            {
                if (stored.InputFields.TryGetValue(field.Name, out var value)) ((InputField)field.GetValue(this)).text = value;
            }

            if (field.FieldType == typeof(Dropdown))
            {
                if (stored.Dropdowns.TryGetValue(field.Name, out var value)) ((Dropdown)field.GetValue(this)).value = value;
            }

            if (field.FieldType == typeof(Toggle))
            {
                if (stored.Toggles.TryGetValue(field.Name, out var value)) ((Toggle)field.GetValue(this)).isOn = value;
            }

            if (field.FieldType == typeof(Slider))
            {
                if (stored.Sliders.TryGetValue(field.Name, out var value)) ((Slider)field.GetValue(this)).value = value;
            }
        }

        foreach (var entry in Empires)
        {
            Race race = entry.Key;
            StartEmpireUI empire = entry.Value;

            if (stored.Empires.TryGetValue(race, out var value))
            {
                empire.AIPlayer.isOn = value.AIPlayer;
                empire.CanVore.isOn = value.CanVore;
                empire.MaxArmySize.value = value.MaxArmySize;
                empire.MaxGarrisonSize.value = value.MaxGarrisonSize;
                empire.PrimaryColor.value = value.PrimaryColor;
                empire.SecondaryColor.value = value.SecondaryColor;
                empire.Team.text = value.Team;
                empire.TurnOrder.text = value.TurnOrder;
                empire.VillageCount.text = value.VillageCount;
                empire.StrategicAI.value = value.StrategicAI;
                empire.TacticalAI.value = value.TacticalAI;
                if (int.TryParse(value.VillageCount, out int result))
                {
                    empire.gameObject.SetActive(result > 0);
                }
                else
                    empire.gameObject.SetActive(false);
            }
        }

        if (map != null) PickMap(mapString);
    }

    public static Color GetDarkerColor(Color color)
    {
        color.r *= .6f;
        color.g *= .6f;
        color.b *= .6f;
        return color;
    }

    public void MapGenTypeChanged()
    {
        MapGenPoles.interactable = MapGenType.value == 1;
        MapGenTemperature.interactable = MapGenType.value == 1;
        MapGenWaterPct.interactable = MapGenType.value == 1;
    }

    public void VillageCountUpdated()
    {
        foreach (var entry in Empires)
        {
            Race race = entry.Key;
            StartEmpireUI empire = entry.Value;
            if (empire.gameObject.activeSelf == false) continue;
            empire.VillageCount.text = AllEmpires.VillageCount.text;
        }
    }

    public void StrategicAIUpdated()
    {
        foreach (var entry in Empires)
        {
            Race race = entry.Key;
            StartEmpireUI empire = entry.Value;
            if (empire.gameObject.activeSelf == false) continue;
            empire.StrategicAI.value = AllEmpires.StrategicAI.value;
        }
    }

    public void TacticalAIUpdated()
    {
        foreach (var entry in Empires)
        {
            Race race = entry.Key;
            StartEmpireUI empire = entry.Value;
            if (empire.gameObject.activeSelf == false) continue;
            empire.TacticalAI.value = AllEmpires.TacticalAI.value;
        }
    }

    public void UpdateAIBoxes()
    {
        foreach (var entry in Empires)
        {
            Race race = entry.Key;
            StartEmpireUI empire = entry.Value;
            empire.StrategicAI.interactable = empire.AIPlayer.isOn;
            empire.TacticalAI.interactable = empire.AIPlayer.isOn;
        }
    }

    public void MaxGarrisonSizeUpdated()
    {
        foreach (var entry in Empires)
        {
            Race race = entry.Key;
            StartEmpireUI empire = entry.Value;
            if (empire.gameObject.activeSelf == false) continue;
            empire.MaxGarrisonSize.value = AllEmpires.MaxGarrisonSize.value;
        }
    }

    public void MaxArmySizeUpdated()
    {
        foreach (var entry in Empires)
        {
            Race race = entry.Key;
            StartEmpireUI empire = entry.Value;
            if (empire.gameObject.activeSelf == false) continue;
            empire.MaxArmySize.value = AllEmpires.MaxArmySize.value;
        }
    }

    public void StrategicAutoSizeChanged()
    {
        StrategicX.transform.parent.gameObject.SetActive(StrategicAutoSize.value == 0);
        StrategicY.transform.parent.gameObject.SetActive(StrategicAutoSize.value == 0);
    }

    public void SwitchToEmpires()
    {
        EmpiresTab.SetActive(true);
        GameSettingsTab.SetActive(false);
        MapGenTab.SetActive(false);
    }

    public void SwitchToGameSettings()
    {
        EmpiresTab.SetActive(false);
        GameSettingsTab.SetActive(true);
        MapGenTab.SetActive(false);
    }

    public void SwitchToMapGenSettings()
    {
        EmpiresTab.SetActive(false);
        GameSettingsTab.SetActive(false);
        MapGenTab.SetActive(true);
    }

    public void PickSaveForContentSettingsDialog()
    {
        var ui = Instantiate(State.GameManager.LoadPicker).GetComponent<FileLoaderUI>();
        new SimpleFileLoader(State.SaveDirectory, "sav", ui, false, SimpleFileLoader.LoaderType.PickSaveForContentSettings);
    }

    public void PickMapDialog()
    {
        var ui = Instantiate(State.GameManager.LoadPicker).GetComponent<FileLoaderUI>();
        new SimpleFileLoader(State.MapDirectory, "map", ui, false, SimpleFileLoader.LoaderType.PickMap);
    }

    internal void PickSaveForContentData(string filename)
    {
        try
        {
            byte[] bytes = File.ReadAllBytes(filename);
            var tempWorld = SerializationUtility.DeserializeValue<World>(bytes, DataFormat.Binary);
            Config.World = tempWorld.ConfigStorage;
        }
        catch
        {
            State.GameManager.CreateMessageBox("Couldn't read the content settings correctly");
        }
    }

    internal void PickMap(string filename)
    {
        mapString = filename;
        foreach (var entry in Empires)
        {
            Race race = entry.Key;
            StartEmpireUI empire = entry.Value;
            empire.gameObject.SetActive(false);
        }

        map = Map.Get(filename);
        foreach (var entry in Empires)
        {
            Race race = entry.Key;
            StartEmpireUI empire = entry.Value;
            empire.VillageCount.interactable = false;
            int count = map.storedVillages.Where(s => Equals(s.Race, race)).Count(); // Fix mess
            empire.VillageCount.text = count.ToString();
            if (count > 0)
            {
                empire.gameObject.SetActive(true);
                //Empires[i].TurnOrder.text = "1";
            }
        }

        AllEmpires.VillageCount.interactable = false;
        StrategicX.interactable = false;
        StrategicX.text = map.Tiles.GetLength(0).ToString();
        StrategicY.interactable = false;
        StrategicY.text = map.Tiles.GetLength(1).ToString();
        ClearPickedMap.interactable = true;
        AddRaces.interactable = false;
    }

    public void ClearMap()
    {
        map = null;
        foreach (var entry in Empires)
        {
            Race race = entry.Key;
            StartEmpireUI empire = entry.Value;
            empire.VillageCount.interactable = true;
        }

        AllEmpires.VillageCount.interactable = true;
        StrategicX.interactable = true;
        StrategicY.interactable = true;
        ClearPickedMap.interactable = false;
        AddRaces.interactable = true;
    }

    internal void UpdateColor(StartEmpireUI emp)
    {
        emp.PrimaryColor.GetComponent<Image>().color = ColorFromIndex(emp.PrimaryColor.value);
    }

    internal void UpdateSecondaryColor(StartEmpireUI emp)
    {
        emp.SecondaryColor.GetComponent<Image>().color = GetDarkerColor(ColorFromIndex(emp.SecondaryColor.value));
    }

    private string TestTacticalSize()
    {
        int x = Convert.ToInt32(TacticalX.text);
        int y = Convert.ToInt32(TacticalY.text);

        int MaxUnitsOnHalf = 50;

        if (x < 8 || y < 8) return "Can't have a tactical dimension less than 8";

        if (MaxUnitsOnHalf > x * y / 14) return "Not enough space to comfortably fit a full army";

        return "";
    }

    private string TestStrategicSize()
    {
        int x = Convert.ToInt32(StrategicX.text);
        int y = Convert.ToInt32(StrategicY.text);

        int villageCount = 0;
        int empireCount = 0;
        foreach (var entry in Empires)
        {
            StartEmpireUI empire = entry.Value;
            if (Convert.ToInt32(empire.VillageCount.text) < 0) return "Can't have negative villages";
            villageCount += Convert.ToInt32(empire.VillageCount.text);
            if (Convert.ToInt32(empire.VillageCount.text) > 0) empireCount++;
        }

        if (empireCount < 1 || (empireCount < 2 && VictoryCondition.value != 3)) return "Need at least 2 empires with villages (or 1 with no victory condition)";

        if (x < 7 || y < 7) return "Can't have a tactical dimension less than 7";

        int useableSpace = (x - 2) * (y - 2);

        if (useableSpace < 32 * villageCount) return "Not enough space to comfortably fit all the villages";

        return "";
    }

    private string SetStrategicSizeAutomatically()
    {
        int villageCount = 0;
        int empireCount = 0;
        foreach (var entry in Empires)
        {
            Race race = entry.Key;
            StartEmpireUI empire = entry.Value;
            if (Convert.ToInt32(empire.VillageCount.text) < 0) return "Can't have negative villages";
            villageCount += Convert.ToInt32(empire.VillageCount.text);
            //Debug.Log(empire.VillageCount.text);
            if (Convert.ToInt32(empire.VillageCount.text) > 0) empireCount++;
        }
        //Debug.Log(Empires.Count);
        //Debug.Log(empireCount);

        villageCount += Convert.ToInt32(AbandonedVillages.text);

        if (empireCount < 1 || (empireCount < 2 && VictoryCondition.value != 3)) return "Need at least 2 empires with villages (or 1 with no victory condition)";

        int minimumSpace = 64;
        if (StrategicAutoSize.value == 1)
            minimumSpace = 32 * villageCount;
        else if (StrategicAutoSize.value == 2)
            minimumSpace = 64 * villageCount;
        else if (StrategicAutoSize.value == 3) minimumSpace = 128 * villageCount;
        StrategicX.text = (1 + (int)Math.Sqrt(minimumSpace)).ToString();
        StrategicY.text = (1 + (int)Math.Sqrt(minimumSpace)).ToString();

        return "";
    }

    private void AssignUnusedTurnOrders()
    {
        // TODO what does this even do
        // int lastIndex = Empires.Length - 1;
        //
        // for (int i = Empires.Length - 1; i >= 0; i--)
        // {
        //     if (int.TryParse(Empires[i].VillageCount.text, out int result))
        //     {
        //         if (result <= 0)
        //         {
        //             lastIndex--;
        //         }
        //     }
        //
        // }
    }


    public void CreateWorld()
    {
        State.GameManager.Menu.Options.LoadFromStored();
        State.GameManager.Menu.CheatMenu.LoadFromStored();
        Config.World.resetVillagesPerEmpire();
        // try
        // {
        foreach (var entry in Empires)
        {
            Race race = entry.Key;
            StartEmpireUI empire = entry.Value;
            //Debug.Log(empire.VillageCount.text);
            //Debug.Log(race.Id);
            int intValue = Convert.ToInt32(empire.VillageCount.text);
            Config.World.VillagesPerEmpire[race] = intValue;
        }

        Config.World.SoftLevelCap = Convert.ToInt32(SoftLevelCap.text);
        Config.World.HardLevelCap = Convert.ToInt32(HardLevelCap.text);

        // }
        // catch
        // {
        //     State.GameManager.CreateMessageBox("There's a blank textbox, if you want it to be 0 it should say 0");
        //     return;
        // }

        // TODO evil try catch lies to you. Remove and replace. 
        try
        {
            string errorText = "";
            if (AutoScaleTactical.isOn == false) errorText = TestTacticalSize();
            if (errorText != "")
            {
                State.GameManager.CreateMessageBox(errorText);
                return;
            }

            if (map == null && StrategicAutoSize.value == 0)
            {
                errorText = TestStrategicSize();
                if (errorText != "")
                {
                    State.GameManager.CreateMessageBox(errorText);
                    return;
                }
            }
            else if (map == null && StrategicAutoSize.value > 0)
            {
                errorText = SetStrategicSizeAutomatically();
                if (errorText != "")
                {
                    State.GameManager.CreateMessageBox(errorText);
                    return;
                }
            }

            Config.World.ExperiencePerLevel = Convert.ToInt32(BaseExpRequired.text);
            Config.World.AdditionalExperiencePerLevel = Convert.ToInt32(ExpIncreaseRate.text);
            Config.StartingGold = Convert.ToInt32(StartingGold.text);
            if (Config.ExperiencePerLevel < 1)
            {
                State.GameManager.CreateMessageBox("Levels should require at least 1 exp");
                return;
            }

            if (Config.AdditionalExperiencePerLevel < 0)
            {
                State.GameManager.CreateMessageBox("Additonal Levels should be at least 0 (new levels shouldn't be cheaper)");
                return;
            }
        }
        catch
        {
            State.GameManager.CreateMessageBox("At least one of the textboxes is blank, and needs to be filled in");
            return;
        }

        Config.World.ItemSlots = Config.NewItemSlots;

        StrategicCreationArgs args = new StrategicCreationArgs(Empires.Count);
        Config.ResetCenteredEmpire();

        // TODO evil try catch lies to you. Remove and replace. 
        try
        {
            foreach (var entry in Empires)
            {
                Race race = entry.Key;
                StartEmpireUI empire = entry.Value;
                StrategyAIType strategyAIType;
                TacticalAIType tacticalAIType;
                if (empire.AIPlayer.isOn)
                {
                    strategyAIType = (StrategyAIType)empire.StrategicAI.value + 1;
                    tacticalAIType = (TacticalAIType)empire.TacticalAI.value + 1;
                    Config.CenteredEmpire[race] = (StrategyAIType)empire.StrategicAI.value + 1 == StrategyAIType.Passive;
                }
                else
                {
                    strategyAIType = 0;
                    tacticalAIType = 0;
                    Config.CenteredEmpire[race] = false;
                }

                args.CanVore[race] = empire.CanVore.isOn;
                args.Team[race] = Convert.ToInt32(empire.Team.text);
                args.TurnOrder[race] = Convert.ToInt32(empire.TurnOrder.text);
                //args.empireArgs[i].bannerType = (i % 2 == 1) ? 1 : 3;

                Empire.ConstructionArgs constructionArgs = new Empire.ConstructionArgs(
                    race: race,
                    side: race.ToSide(),
                    color: ColorFromIndex(empire.PrimaryColor.value),
                    secColor: GetDarkerColor(ColorFromIndex(empire.SecondaryColor.value)),
                    bannerType: 0,
                    strategicAI: strategyAIType,
                    tacticalAI: tacticalAIType,
                    team: Convert.ToInt32(empire.Team.text),
                    maxArmySize: (int)empire.MaxArmySize.value,
                    maxGarrisonSize: (int)empire.MaxGarrisonSize.value
                );

                args.empireArgs[race] = constructionArgs;
            }

            args.MercCamps = Convert.ToInt32(MercenaryHouses.text);
            args.GoldMines = Convert.ToInt32(GoldMines.text);
            args.crazyBuildings = CrazyBuildings.isOn;

            args.MapGen.UsingNewGenerator = MapGenType.value == 1;
            args.MapGen.ExcessBridges = MapGenExcessBridges.isOn;
            args.MapGen.Poles = MapGenPoles.isOn;
            args.MapGen.Temperature = MapGenTemperature.value;
            args.MapGen.WaterPct = MapGenWaterPct.value;
            args.MapGen.Hilliness = MapGenHills.value;
            args.MapGen.Swampiness = MapGenSwamps.value;
            args.MapGen.ForestPct = MapGenForests.value;
            args.MapGen.AbandonedVillages = Convert.ToInt32(AbandonedVillages.text);

            Config.World.StartingPopulation = Convert.ToInt32(StartingVillagePopulation.text);
            Config.World.LeaderLossExpPct = LeaderLossExpPct.value;
            Config.World.LeaderLossLevels = Convert.ToInt32(LeaderLossLevels.text);
            Config.World.GoldMineIncome = Convert.ToInt32(GoldMineIncome.text);
            Config.World.StrategicWorldSizeX = Convert.ToInt32(StrategicX.text);
            Config.World.StrategicWorldSizeY = Convert.ToInt32(StrategicY.text);
            Config.World.TacticalSizeX = Convert.ToInt32(TacticalX.text);
            Config.World.TacticalSizeY = Convert.ToInt32(TacticalY.text);
            Config.World.FactionLeaders = FactionLeaders.isOn;
            Config.World.VictoryCondition = (Config.VictoryType)VictoryCondition.value;
            Config.World.VillageIncomePercent = Convert.ToInt32(VillageIncomeRate.text);
            Config.World.VillagersPerFarm = Convert.ToInt32(VillagersPerFarm.text);
            Config.World.ArmyUpkeep = Convert.ToInt32(ArmyUpkeep.text);
            Config.World.CapMaxGarrisonIncrease = CapitalGarrisonCapped.isOn;
            Config.World.Toggles["FirstTurnArmiesIdle"] = FirstTurnArmiesIdle.isOn;
            Config.World.Toggles["LeadersAutoGainLeadership"] = LeadersAutoGainLeadership.isOn;


            Config.PutTeamsTogether = SpawnTeamsTogether.isOn;
        }
        catch
        {
            State.GameManager.CreateMessageBox("At least one of the textboxes is blank, and needs to be filled in");
            return;
        }

        State.World = new World(args, map);
        Config.World.AutoScaleTactical = AutoScaleTactical.isOn;

        State.GameManager.SwitchToStrategyMode();
        gameObject.SetActive(false);
    }

    public void AutoScaleChanged()
    {
        TacticalX.interactable = !AutoScaleTactical.isOn;
        TacticalY.interactable = !AutoScaleTactical.isOn;
    }

    public void ClearRaces()
    {
        foreach (var entry in Empires)
        {
            Race race = entry.Key;
            StartEmpireUI empire = entry.Value;
            RemoveRace(empire);
        }

        BuildRaceDisplay();
    }

    internal void RemoveRace(StartEmpireUI empire)
    {
        empire.VillageCount.text = 0.ToString();
        empire.gameObject.SetActive(false);
    }

    public void AllAddRaces()
    {
        foreach (var entry in Empires)
        {
            Race race = entry.Key;
            StartEmpireUI empire = entry.Value;
            if (empire.gameObject.activeSelf == false)
            {
                empire.gameObject.SetActive(true);
                empire.VillageCount.text = AllEmpires.VillageCount.text;
                empire.StrategicAI.value = AllEmpires.StrategicAI.value;
                empire.TacticalAI.value = AllEmpires.TacticalAI.value;
                empire.MaxArmySize.value = AllEmpires.MaxArmySize.value;
                empire.MaxGarrisonSize.value = AllEmpires.MaxGarrisonSize.value;
                empire.TurnOrder.text = "1";
            }
        }

        RaceUI.gameObject.SetActive(false);
    }


    public void BuildRaceDisplay()
    {
        int children = RaceUI.RaceFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(RaceUI.RaceFolder.transform.GetChild(i).gameObject);
        }

        foreach (var entry in Empires)
        {
            Race race = entry.Key;
            StartEmpireUI empire = entry.Value;
            if (empire.gameObject.activeSelf) continue;
            GameObject obj = Instantiate(RaceUI.RaceUnitPanel, RaceUI.RaceFolder);
            UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
            // Side was 1 for Unit
            Actor_Unit actor = new Actor_Unit(new Vec2i(0, 0), new Unit(Race.Dog.ToSide(), race, 0, true));
            TextMeshProUGUI text = obj.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            obj.GetComponentInChildren<UnitInfoPanel>().Unit = actor.Unit;
            var racePar = RaceParameters.GetTraitData(actor.Unit);
            text.text = $"{race}\nBody Size: {State.RaceSettings.GetBodySize(actor.Unit.Race)}\nBase Stomach Size: {State.RaceSettings.GetStomachSize(actor.Unit.Race)}\nFavored Stat: {State.RaceSettings.GetFavoredStat(actor.Unit.Race)}\nDefault Traits:\n{State.RaceSettings.ListTraits(actor.Unit.Race)}";
            sprite.UpdateSprites(actor);
            Button button = obj.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => AddRace(race));
            button.onClick.AddListener(() => Destroy(obj));
        }

        RaceUI.gameObject.SetActive(true);
    }

    private void AddRace(Race race)
    {
        Empires[race].gameObject.SetActive(true);
        Empires[race].VillageCount.text = AllEmpires.VillageCount.text;
        Empires[race].StrategicAI.value = AllEmpires.StrategicAI.value;
        Empires[race].TacticalAI.value = AllEmpires.TacticalAI.value;
        Empires[race].MaxArmySize.value = AllEmpires.MaxArmySize.value;
        Empires[race].MaxGarrisonSize.value = AllEmpires.MaxGarrisonSize.value;
        Empires[race].TurnOrder.text = "1";
        AssignUnusedTurnOrders();
    }


    private static Color[] allColors = new Color[]
    {
        new Color(0.502f, 0.502f, 0.502f),
        new Color(0.133f, 0.545f, 0.133f),
        new Color(0.498f, 0.000f, 0.000f),
        new Color(0.502f, 0.502f, 0.000f),
        new Color(0.282f, 0.239f, 0.545f),
        new Color(0.000f, 0.545f, 0.545f),
        new Color(0.275f, 0.510f, 0.706f),
        new Color(0.824f, 0.412f, 0.118f),
        new Color(0.604f, 0.804f, 0.196f),
        new Color(0.000f, 0.000f, 0.545f),
        new Color(0.855f, 0.647f, 0.125f),
        new Color(0.498f, 0.000f, 0.498f),
        new Color(0.561f, 0.737f, 0.561f),
        new Color(0.690f, 0.188f, 0.376f),
        new Color(1.000f, 0.271f, 0.000f),
        new Color(1.000f, 1.000f, 0.000f),
        new Color(0.251f, 0.878f, 0.816f),
        new Color(0.498f, 1.000f, 0.000f),
        new Color(0.580f, 0.000f, 0.827f),
        new Color(0.000f, 1.000f, 0.498f),
        new Color(0.863f, 0.078f, 0.235f),
        new Color(0.000f, 0.000f, 1.000f),
        new Color(1.000f, 0.000f, 1.000f),
        new Color(0.118f, 0.565f, 1.000f),
        new Color(0.941f, 0.902f, 0.549f),
        new Color(0.980f, 0.502f, 0.447f),
        new Color(0.565f, 0.933f, 0.565f),
        new Color(0.678f, 0.847f, 0.902f),
        new Color(1.000f, 0.078f, 0.576f),
        new Color(0.482f, 0.408f, 0.933f),
        new Color(0.933f, 0.510f, 0.933f),
        new Color(1.000f, 0.714f, 0.757f)
    };

    internal static Color ColorFromRace(Race race)
    {
        int index = RaceFuncs.RaceToTempNumber(race) % 31;
        return ColorFromIndex(index);
    }

    internal static Color ColorFromIndex(int index)
    {
        if (index < allColors.Length)
        {
            return allColors[index];
        }
        else
        {
            return new Color(0f, 0f, 0f);
        }
    }

    internal static int IndexFromColor(Color color)
    {
        for (int i = 0; i < allColors.Length; i++)
        {
            if (color == allColors[i]) return i;
        }

        return 0;
    }

    public void ChangeToolTip(int type)
    {
        TooltipText.text = DefaultTooltips.Tooltip(type);
    }

    public void UpdateLeaderExpLoss()
    {
        LeaderLossExpPct.GetComponentInChildren<Text>().text = $"Leader Exp lost on Death: {Math.Round(LeaderLossExpPct.value * 100, 2)}%";
    }
}