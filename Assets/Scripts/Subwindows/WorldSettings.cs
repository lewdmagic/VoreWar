using LegacyAI;
using System;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class WorldSettings : MonoBehaviour
{
    public GameObject EditEmpirePrefab;
    public Transform Folder;
    private EditEmpireUI[] _empires;

    public Text RightText;

    public void Open()
    {
        ShowSettings();
        if (_empires != null)
        {
            foreach (var item in _empires)
            {
                Destroy(item.gameObject);
            }
        }

        _empires = new EditEmpireUI[State.World.MainEmpires.Count];
        for (int i = 0; i < _empires.Length; i++)
        {
            _empires[i] = Instantiate(EditEmpirePrefab, Folder).GetComponent<EditEmpireUI>();
            _empires[i].Name.text = State.World.MainEmpires[i].Name.ToString();
            _empires[i].AIPlayer.isOn = State.World.MainEmpires[i].StrategicAI != null;
            if (State.World.MainEmpires[i].StrategicAI is PassiveAI)
            {
                _empires[i].StrategicAI.value = 0;
            }
            else if (State.World.MainEmpires[i].StrategicAI is LegacyStrategicAI)
            {
                _empires[i].StrategicAI.value = 1;
            }
            else if (State.World.MainEmpires[i].StrategicAI is StrategicAI)
            {
                StrategicAI ai = (StrategicAI)State.World.MainEmpires[i].StrategicAI;
                if (ai.CheatLevel > 0)
                {
                    _empires[i].StrategicAI.value = 3 + ai.CheatLevel;
                }
                else if (ai.SmarterAI)
                {
                    _empires[i].StrategicAI.value = 3;
                }
                else
                {
                    _empires[i].StrategicAI.value = 2;
                }
            }

            if (_empires[i].AIPlayer.isOn)
            {
                _empires[i].TacticalAI.value = (int)State.World.MainEmpires[i].TacticalAIType - 1;
            }
            else
            {
                _empires[i].TacticalAI.value = 1;
            }

            _empires[i].CanVore.isOn = State.World.MainEmpires[i].CanVore;
            _empires[i].Team.text = State.World.MainEmpires[i].Team.ToString();
            _empires[i].PrimaryColor.value = CreateStrategicGame.IndexFromColor(State.World.MainEmpires[i].UnityColor);
            Color secColor = State.World.MainEmpires[i].UnitySecondaryColor;
            _empires[i].SecondaryColor.value = CreateStrategicGame.IndexFromColor(GetLighterColor(State.World.MainEmpires[i].UnitySecondaryColor));
            _empires[i].PrimaryColor.onValueChanged.AddListener((s) => UpdateColors());
            _empires[i].SecondaryColor.onValueChanged.AddListener((s) => UpdateColors());
            _empires[i].MaxArmySize.value = State.World.MainEmpires[i].MaxArmySize;
            _empires[i].MaxGarrisonSize.value = State.World.MainEmpires[i].MaxGarrisonSize;
            _empires[i].MaxArmySize.GetComponentInChildren<SetMeToValue>().Set(_empires[i].MaxArmySize);
            _empires[i].MaxGarrisonSize.GetComponentInChildren<SetMeToValue>().Set(_empires[i].MaxGarrisonSize);
            _empires[i].TurnOrder.text = State.World.MainEmpires[i].TurnOrder.ToString();
            if (State.World.MainEmpires[i].KnockedOut || RaceFuncs.IsRebelOrBandit3(State.World.MainEmpires[i].Side))
            {
                _empires[i].gameObject.SetActive(false);
            }
        }

        UpdateColors();
        RelationsManager.ResetMonsterRelations();
    }

    public static Color GetLighterColor(Color color)
    {
        color.r /= .6f;
        color.g /= .6f;
        color.b /= .6f;
        return color;
    }

    public void EditWorldSettings()
    {
        State.GameManager.VariableEditor.Open(Config.World);
    }

    public void ExitAndSave()
    {
        for (int i = 0; i < _empires.Length; i++)
        {
            if (RaceFuncs.IsRebelOrBandit2(State.World.MainEmpires[i].Side)) continue;
            if (_empires[i].AIPlayer.isOn)
            {
                StrategyAIType strat = (StrategyAIType)(_empires[i].StrategicAI.value + 1);
                if (strat == StrategyAIType.Passive)
                    State.World.MainEmpires[i].StrategicAI = new PassiveAI(State.World.MainEmpires[i].Side);
                else if (strat == StrategyAIType.Basic)
                    State.World.MainEmpires[i].StrategicAI = new StrategicAI(State.World.MainEmpires[i], 0, false);
                else if (strat == StrategyAIType.Advanced)
                    State.World.MainEmpires[i].StrategicAI = new StrategicAI(State.World.MainEmpires[i], 0, true);
                else if (strat == StrategyAIType.Cheating1)
                    State.World.MainEmpires[i].StrategicAI = new StrategicAI(State.World.MainEmpires[i], 1, true);
                else if (strat == StrategyAIType.Cheating2)
                    State.World.MainEmpires[i].StrategicAI = new StrategicAI(State.World.MainEmpires[i], 2, true);
                else if (strat == StrategyAIType.Cheating3)
                    State.World.MainEmpires[i].StrategicAI = new StrategicAI(State.World.MainEmpires[i], 3, true);
                else if (strat == StrategyAIType.Legacy) State.World.MainEmpires[i].StrategicAI = new LegacyStrategicAI(State.World.MainEmpires[i].Side);
                State.World.MainEmpires[i].TacticalAIType = (TacticalAIType)_empires[i].TacticalAI.value + 1;
            }
            else
            {
                State.World.MainEmpires[i].StrategicAI = null;
                State.World.MainEmpires[i].TacticalAIType = TacticalAIType.None;
            }

            if (State.World.MainEmpires[i].CanVore != _empires[i].CanVore.isOn)
            {
                State.World.MainEmpires[i].CanVore = _empires[i].CanVore.isOn;
                foreach (Unit unit in StrategicUtilities.GetAllUnits().Where(s => Equals(s.Race, State.World.MainEmpires[i].Race)))
                {
                    if (unit.Type == UnitType.Soldier)
                    {
                        if (unit.FixedPredator == false)
                        {
                            unit.Predator = State.World.MainEmpires[i].CanVore;
                            unit.ReloadTraits(); //To make sure it takes into account gender traits as well
                        }
                    }
                }
            }

            if (State.World.MainEmpires[i].Team != Convert.ToInt32(_empires[i].Team.text))
            {
                State.World.MainEmpires[i].Team = Convert.ToInt32(_empires[i].Team.text);
                RelationsManager.TeamUpdated(State.World.MainEmpires[i]);
            }

            State.World.MainEmpires[i].UnityColor = CreateStrategicGame.ColorFromIndex(_empires[i].PrimaryColor.value);
            State.World.MainEmpires[i].UnitySecondaryColor = CreateStrategicGame.GetDarkerColor(CreateStrategicGame.ColorFromIndex(_empires[i].SecondaryColor.value));
            State.World.MainEmpires[i].MaxArmySize = (int)_empires[i].MaxArmySize.value;
            State.World.MainEmpires[i].MaxGarrisonSize = (int)_empires[i].MaxGarrisonSize.value;
            State.World.MainEmpires[i].TurnOrder = Convert.ToInt32(_empires[i].TurnOrder.text);
        }

        if (Config.Diplomacy == false) RelationsManager.ResetRelationTypes();
        State.World.RefreshTurnOrder();
        State.World.UpdateBanditLimits();
        State.GameManager.StrategyMode?.CheckIfOnlyAIPlayers();
        gameObject.SetActive(false);
    }

    public void ExitWithoutSaving()
    {
        gameObject.SetActive(false);
    }

    public void ChangeAllArmySizes()
    {
        var box = State.GameManager.CreateInputBox();
        box.SetData(ChangeArmySizes, "Change them", "Cancel", "Change all army max sizes? (1-48)", 2);
    }

    private void ChangeArmySizes(int size)
    {
        foreach (var empire in _empires)
        {
            empire.MaxArmySize.value = size;
        }
    }

    public void ChangeAllGarrisonSizes()
    {
        var box = State.GameManager.CreateInputBox();
        box.SetData(ChangeGarrisonSizes, "Change them", "Cancel", "Change all max garrison sizes? (1-48)", 2);
    }

    private void ChangeGarrisonSizes(int size)
    {
        foreach (var empire in _empires)
        {
            empire.MaxGarrisonSize.value = size;
        }
    }

    public void ShowSettings()
    {
        StringBuilder right = new StringBuilder();

        right.AppendLine($"Max Item Slots: {Config.ItemSlots}");
        //right.AppendLine($"Regurgitate friendly units: {(Config.FriendlyRegurgitation ? "Yes" : "No")}");
        right.AppendLine($"Exp required per level (base): {Config.ExperiencePerLevel}");
        right.AppendLine($"Additional exp required per level: {Config.AdditionalExperiencePerLevel}");
        right.AppendLine($"Level Soft Cap: {Config.SoftLevelCap}");
        right.AppendLine($"Level Hard Cap: {Config.HardLevelCap}");
        right.AppendLine($"Victory Type: {Config.VictoryCondition}");
        right.AppendLine($"Gold Mine Income : {Config.GoldMineIncome}");
        right.AppendLine($"Leader Exp % lost on Death : {Math.Round(Config.LeaderLossExpPct * 100, 2)}");
        right.AppendLine($"Leader Levels lost on Death : {Config.LeaderLossLevels}");
        if (State.World.CrazyBuildings) right.AppendLine($"Annoynimouse's crazy buildings are on...");

        RightText.text = right.ToString();
    }

    public void UpdateColors()
    {
        foreach (var emp in _empires)
        {
            emp.PrimaryColor.GetComponent<Image>().color = CreateStrategicGame.ColorFromIndex(emp.PrimaryColor.value);
            emp.SecondaryColor.GetComponent<Image>().color = CreateStrategicGame.GetDarkerColor(CreateStrategicGame.ColorFromIndex(emp.SecondaryColor.value));
        }
    }
}