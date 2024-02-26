using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class VillageView
{
    private Village _village;
    private VillageViewPanel _ui;

    private Empire _buyingEmpire;

    private Button[] _buttons;

    public VillageView(Village village, VillageViewPanel villageUI)
    {
        _buttons = new Button[(int)VillageBuilding.LastIndex + 1];
        _ui = villageUI;
    }

    internal void Open(Village village, Empire activatingEmpire)
    {
        _buyingEmpire = activatingEmpire;
        _ui.BuyForAllToggle.isOn = false;
        this._village = village;
        Refresh();
        _ui.gameObject.SetActive(true);
    }

    internal void Refresh()
    {
        if (_ui.BuyForAllToggle.isOn)
            CreateOrUpdateBuyEachButtons();
        else
            CreateOrUpdateButtons();
        GenerateText();
    }

    private void GenerateText()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Village: {_village.Name}");
        sb.AppendLine($"Population: {_village.GetTotalPop()}");
        sb.AppendLine($"Maximum Population: {_village.Maxpop}");
        sb.AppendLine($"Garrison: {_village.Garrison}");
        sb.AppendLine($"Income: {_village.GetIncome()}");
        sb.AppendLine($"Current Money: {_buyingEmpire.Gold}");
        sb.AppendLine($"Starting Experience: {_village.GetStartingXp()}");
        if (_village.NetBoosts.FarmsEquivalent > 0) sb.AppendLine($"Extra Farms: {_village.NetBoosts.FarmsEquivalent}");
        if (_village.NetBoosts.WealthMult > 1) sb.AppendLine($"Gold Income: {100 * _village.NetBoosts.WealthMult}%");
        if (_village.NetBoosts.WealthAdd > 0) sb.AppendLine($"Gold Income: +{_village.NetBoosts.WealthAdd}");
        if (_village.NetBoosts.PopulationMaxMult > 1) sb.AppendLine($"Population Max: {100 * _village.NetBoosts.PopulationMaxMult}%");
        if (_village.NetBoosts.PopulationMaxAdd > 0) sb.AppendLine($"Population Max: +{_village.NetBoosts.PopulationMaxAdd}");
        if (_village.NetBoosts.GarrisonMaxMult > 1) sb.AppendLine($"Max Garrison: {100 * _village.NetBoosts.GarrisonMaxMult}%");
        if (_village.NetBoosts.GarrisonMaxAdd > 0) sb.AppendLine($"Max Garrison: +{_village.NetBoosts.GarrisonMaxAdd}");

        if (_village.buildings.Any())
        {
            sb.AppendLine($"Buildings already built:");
            foreach (var buildingId in _village.buildings)
            {
                var buildingDef = VillageBuildingList.GetBuildingDefinition(buildingId);
                if (buildingDef != null) sb.AppendLine($"{buildingDef.Name} : {buildingDef.Description}");
            }
        }

        _ui.VillageInfo.text = sb.ToString();
    }

    private void CreateOrUpdateButtons()
    {
        ClearAllButtons();
        int nextOpenPos = 0;

        foreach (var building in VillageBuildingList.GetListOfBuildingEnum())
        {
            var buildingDef = VillageBuildingList.GetBuildingDefinition(building);
            var added = SetButton(buildingDef, nextOpenPos);
            if (added) nextOpenPos++;
        }

        SetBuyAllButton(nextOpenPos);
    }

    private bool SetButton(VillageBuildingDefinition buildingDef, int buttonPosition)
    {
        if (buildingDef.CanBeBuilt == false)
        {
            return false;
        }

        if (buildingDef.CanEverBuild(_village) == false)
        {
            return false;
        }

        string text = buildingDef.Name + " - " + buildingDef.Description + " - Cost: " + GetCostText(buildingDef.Cost);
        Button button = ResetButton(buttonPosition);

        if (buildingDef.HasAllPrerequisites(_village) == false)
        {
            button.interactable = false;
            button.GetComponentInChildren<Text>().text = "Requires " + buildingDef.GetFirstUnmetPrerequisiteName(_village.buildings) + " to be built first. " + text;
        }
        else if (buildingDef.CanAfford(_buyingEmpire) == false)
        {
            button.interactable = false;
            button.GetComponentInChildren<Text>().text = "Cannot afford " + text;
        }
        else if (buildingDef.CanBuild(_village) == false)
        {
            button.interactable = false;
            button.GetComponentInChildren<Text>().text = "ERROR: Cannot build " + text;
        }
        else
        {
            button.interactable = true;
            button.onClick.AddListener(() => Build(buildingDef.Id));
            button.GetComponentInChildren<Text>().text = "Build " + text;
        }

        return true;
    }

    private void CreateOrUpdateBuyEachButtons()
    {
        ClearAllButtons();
        int nextOpenPos = 0;

        Village[] villages = State.World.Villages.Where(s => Equals(s.Side, _village.Side)).ToArray();

        foreach (var building in VillageBuildingList.GetListOfBuildingEnum())
        {
            var buildingDef = VillageBuildingList.GetBuildingDefinition(building);
            var anyVillageStillNeeds = false;
            var anyVillageCanBuild = false;
            foreach (var villageToCheck in villages)
            {
                if (villageToCheck.buildings.Contains(building) == false)
                {
                    anyVillageStillNeeds = true;
                }

                if (buildingDef.CanBuild(villageToCheck))
                {
                    anyVillageCanBuild = true;
                }

                if (anyVillageStillNeeds && anyVillageCanBuild)
                {
                    break;
                }
            }

            if (anyVillageStillNeeds && anyVillageCanBuild)
            {
                var added = SetButtonForEachVillage(buildingDef.Name + " - " + buildingDef.Description + " - Cost: " + GetCostText(VillageBuildingDefinition.GetCost(building)), building, nextOpenPos, villages);
                if (added) nextOpenPos++;
            }
        }

        SetAllBuyAllButton(nextOpenPos, villages);
    }

    private bool SetButtonForEachVillage(string text, VillageBuilding building, int buttonPosition, Village[] villages)
    {
        if (_village.Empire != _buyingEmpire)
        {
            State.GameManager.CreateMessageBox("Can't use buy for each village for allied empires, to avoid confusion about what the correct behavior should be");
            return false;
        }

        var buildingDef = VillageBuildingList.GetBuildingDefinition(building);
        Button button = ResetButton(buttonPosition);
        int needed = 0;
        foreach (Village villageToCheck in villages)
        {
            if (villageToCheck.buildings.Contains(building) == false)
            {
                if (buildingDef.CanBuild(villageToCheck))
                {
                    needed++;
                }
            }
        }

        if (needed > 0)
        {
            var cost = VillageBuildingDefinition.GetCost(building, needed);
            if (buildingDef.CanAfford(_buyingEmpire) == false)
            {
                button.interactable = false;
                button.GetComponentInChildren<Text>().text = $"{text} * {needed} = {GetCostText(cost)} -- Cannot afford ";
            }
            else
            {
                button.interactable = true;
                button.onClick.AddListener(() => BuildForEachVillage(building));
                button.GetComponentInChildren<Text>().text = $"Build {text} * {needed} = {GetCostText(cost)}";
            }
        }
        else
        {
            return false;
        }

        return true;
    }

    private void ClearAllButtons()
    {
        for (var i = 0; i < _buttons.Length; i++)
        {
            var button = ResetButton(i);
            button.GetComponent<Button>().interactable = false;
            button.GetComponentInChildren<Text>().text = "...";
            button.gameObject.SetActive(false);
        }
    }

    private Button ResetButton(int position)
    {
        int id = position;
        Button button;
        if (_buttons[id] == null)
        {
            button = Object.Instantiate(_ui.ButtonPrefab, _ui.ButtonPanel.transform).GetComponent<Button>();
            _buttons[id] = button;
        }
        else
        {
            button = _buttons[id];
            button.onClick.RemoveAllListeners();
        }

        button.gameObject.SetActive(true);

        return button;
    }

    private void SetBuyAllButton(int buttonPosition)
    {
        Button button = ResetButton(buttonPosition);
        button.gameObject.SetActive(true);

        button.onClick.AddListener(() => BuildAll());

        var cost = _village.GetCostAllBuildings();
        if (_buyingEmpire == null || cost == null)
        {
            Debug.Log("This shouldn't have happened");
            button.interactable = false;
            return;
        }

        if (cost.Wealth == 0 && cost.LeaderExperience == 0)
        {
            button.interactable = false;
            button.GetComponentInChildren<Text>().text = "This village already has all of the buildings";
        }
        else if (_buyingEmpire.Gold < cost.Wealth || _buyingEmpire.Leader?.Experience < cost.LeaderExperience)
        {
            button.interactable = false;
            button.GetComponentInChildren<Text>().text = $"Cannot afford to buy all buildings - {GetCostText(cost)}";
        }
        else
        {
            button.interactable = true;
            button.GetComponentInChildren<Text>().text = $"Build All buildings - {GetCostText(cost)}";
        }
    }

    private void SetAllBuyAllButton(int buttonPosition, Village[] villages)
    {
        Button button = ResetButton(buttonPosition);
        button.gameObject.SetActive(true);

        button.onClick.AddListener(() => BuildAllForAll());


        BuildingCost cost = _village.GetCostAllBuildings();
        foreach (Village village in villages)
        {
            var localcost = village.GetCostAllBuildings();
            cost.LeaderExperience += localcost.LeaderExperience;
            cost.Wealth += localcost.Wealth;
        }

        if (_buyingEmpire == null || cost == null)
        {
            Debug.Log("This shouldn't have happened");
            button.interactable = false;
            return;
        }

        if (cost.Wealth == 0 && cost.LeaderExperience == 0)
        {
            button.interactable = false;
            button.GetComponentInChildren<Text>().text = "No village needs a building";
        }
        else if (_buyingEmpire.Gold < cost.Wealth || _buyingEmpire.Leader?.Experience < cost.LeaderExperience)
        {
            button.interactable = false;
            button.GetComponentInChildren<Text>().text = $"Cannot afford to buy all buildings for all villages {GetCostText(cost)}";
        }
        else
        {
            button.interactable = true;
            button.GetComponentInChildren<Text>().text = $"Build all buildings for all villages - {GetCostText(cost)}";
        }
    }

    private string GetCostText(BuildingCost cost)
    {
        var textResult = "";
        if (cost.Wealth > 0) textResult += "" + cost.Wealth + " Gold";
        if (cost.LeaderExperience > 0.0f)
        {
            if (textResult != "") textResult += " and ";
            textResult += "" + (int)cost.LeaderExperience + " Leader XP";
        }

        return textResult;
    }

    private void BuildAll()
    {
        _village.BuyAllBuildings(_buyingEmpire);
        Refresh();
    }

    private void BuildAllForAll()
    {
        foreach (Village vill in State.World.Villages.Where(s => Equals(s.Side, _village.Side)))
        {
            vill.BuyAllBuildings(_buyingEmpire);
        }

        Refresh();
    }

    private void BuildForEachVillage(VillageBuilding building)
    {
        foreach (Village vill in State.World.Villages.Where(s => Equals(s.Side, _village.Side) && s.buildings.Contains(building) == false))
        {
            vill.Build(building, _buyingEmpire);
        }

        Refresh();
    }

    private void Build(VillageBuilding building)
    {
        _village.Build(building, _buyingEmpire);
        Refresh();
    }
}