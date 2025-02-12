﻿using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitEditorPanel : CustomizerPanel
{
    public TMP_Dropdown RaceDropdown;
    public TMP_Dropdown TraitDropdown;
    public TMP_Dropdown[] ItemDropdown;
    public TMP_Dropdown[] SpellDropdown;
    public TMP_Dropdown AlignmentDropdown;
    public Toggle HiddenToggle;
    public UnitInfoPanel InfoPanel;
    public TextMeshProUGUI TraitList;
    public Slider ExpBar;
    public Slider HealthBar;
    public Slider ManaBar;
    public EditStatButton EditStatButtonPrefab;
    public GameObject StatButtonPanel;
    internal UnitEditor UnitEditor;


    private Dictionary<Race, int> _raceDict;
    private Dictionary<TraitType, int> _traitDict;
    private Dictionary<int, string> _itemDict;
    private Dictionary<string, int> _itemReverseDict;
    private Dictionary<int, Empire> _empireDict;

    public TMP_InputField TraitsText;

    public Button SwapAlignment;


    protected EditStatButton[] Buttons;


    private void SetUpRaces()
    {
        _raceDict = new Dictionary<Race, int>();
        int val = 0;
        foreach (Race race in RaceFuncs.RaceEnumerable().OrderBy((s) => s.ToString()))
        {
            _raceDict[race] = val;
            val++;
            RaceDropdown.options.Add(new TMP_Dropdown.OptionData(race.ToString()));
        }

        Buttons = new EditStatButton[10];
        foreach (Stat stat in (Stat[])Enum.GetValues(typeof(Stat)))
        {
            if (stat == Stat.None) break;
            Buttons[(int)stat] = CreateNewButton(stat, UnitEditor.ChangeStat, UnitEditor.ChangeLevel, ManualChangeStat);
        }

        _traitDict = new Dictionary<TraitType, int>();
        int val2 = 0;
        foreach (RandomizeList rl in State.RandomizeLists)
        {
            _traitDict[(TraitType)rl.ID] = val2;
            val2++;
            TraitDropdown.options.Add(new TMP_Dropdown.OptionData(rl.Name.ToString()));
        }

        foreach (TraitType traitId in ((TraitType[])Enum.GetValues(typeof(TraitType))).OrderBy(s => { return s >= TraitType.LightningSpeed ? "ZZZ" + s.ToString() : s.ToString(); }))
        {
            _traitDict[traitId] = val2;
            val2++;
            TraitDropdown.options.Add(new TMP_Dropdown.OptionData(traitId.ToString()));
        }

        _itemDict = new Dictionary<int, string>();
        _itemReverseDict = new Dictionary<string, int>();
        _itemDict[0] = "Empty";
        var allItems = State.World.ItemRepository.GetAllItems();
        for (int j = 0; j < ItemDropdown.Length; j++)
        {
            ItemDropdown[j].options.Add(new TMP_Dropdown.OptionData("Empty"));
        }

        for (int i = 0; i < allItems.Count; i++)
        {
            for (int j = 0; j < ItemDropdown.Length; j++)
            {
                ItemDropdown[j].options.Add(new TMP_Dropdown.OptionData(allItems[i].Name));
            }

            _itemDict[i + 1] = allItems[i].Name;
            _itemReverseDict[allItems[i].Name] = 1 + i;
        }

        for (int i = 0; i < SpellDropdown.Length; i++)
        {
            foreach (SpellType type in ((SpellType[])Enum.GetValues(typeof(SpellType))).Where(s => (int)s < 100))
            {
                SpellDropdown[i].options.Add(new TMP_Dropdown.OptionData(type.ToString()));
            }

            SpellDropdown[i].RefreshShownValue();
        }

        SetupAllignment();
    }

    private void SetupAllignment()
    {
        _empireDict = new Dictionary<int, Empire>();
        AlignmentDropdown.options.Add(new TMP_Dropdown.OptionData("Default"));
        if (State.World?.MainEmpires != null)
        {
            var mainEmps = State.World.MainEmpires;
            for (int i = 0; i < mainEmps.Count; i++)
            {
                if (RaceFuncs.IsRebelOrBandit5(mainEmps[i].Side)) continue;
                AlignmentDropdown.options.Add(new TMP_Dropdown.OptionData(mainEmps[i].Name));
                _empireDict[i + 1] = mainEmps[i];
            }

            if (State.World.MonsterEmpires != null)
            {
                var monsterEmps = State.World.MonsterEmpires;
                for (int i = 0; i < monsterEmps.Count(); i++)
                {
                    if (RaceFuncs.IsRebelOrBandit5(monsterEmps[i].Side)) continue;
                    AlignmentDropdown.options.Add(new TMP_Dropdown.OptionData(monsterEmps[i].Name));
                    _empireDict[i + mainEmps.Count - 1] = monsterEmps[i];
                }
            }
        }
        else
        {
            AlignmentDropdown.options.Add(new TMP_Dropdown.OptionData("Defender"));
            AlignmentDropdown.options.Add(new TMP_Dropdown.OptionData("Attacker"));
        }

        AlignmentDropdown.RefreshShownValue();
    }

    private EditStatButton CreateNewButton(Stat stat, Action<Stat, int> statAction, Action<Stat, int> levelAction, Action<Stat, int> manualSetAction)
    {
        EditStatButton button = Instantiate(EditStatButtonPrefab, StatButtonPanel.transform).GetComponent<EditStatButton>();
        button.SetData(stat, UnitEditor.Unit.GetStat(stat), statAction, levelAction, manualSetAction, UnitEditor.Unit, this);
        return button;
    }

    public void ChangeUnitButtons(Unit unit)
    {
        foreach (EditStatButton button in Buttons)
        {
            //This if condition serves to fix a bug where using the stat change buttons would cause an exception were a button in-code was not assigned to a GameObject.
            if (button) button.Unit = unit;
        }
    }

    public void UpdateButtons()
    {
        UnitEditor.RefreshStats();
        foreach (EditStatButton button in Buttons)
        {
            //This if condition serves to fix a bug where using the stat change buttons would cause an exception were a button in-code was not assigned to a GameObject.
            if (button) button.UpdateLabel();
        }
    }

    public void Open(ActorUnit actor)
    {
        gameObject.SetActive(true);
        if (UnitEditor == null)
        {
            InfoPanel.ExpBar = ExpBar;
            InfoPanel.HealthBar = HealthBar;
            InfoPanel.ManaBar = ManaBar;
            UnitEditor = new UnitEditor(actor, this, InfoPanel);
            SetUpRaces();
        }
        else
        {
            UnitEditor.SetActor(actor);
            UnitEditor.RefreshStats();
        }

        if (_raceDict.TryGetValue(actor.Unit.Race, out int race))
        {
            RaceDropdown.value = race;
        }
        else
            RaceDropdown.value = 0;

        RaceDropdown.captionText.text = actor.Unit.Race.ToString();
        AlignmentDropdown.captionText.text = DetermineAllignment(actor.Unit);
        HiddenToggle.isOn = actor.Unit.HiddenFixedSide;
        PopulateItems();
        TraitList.text = UnitEditor.Unit.ListTraits();
        SwapAlignment.gameObject.SetActive(State.GameManager.CurrentScene == State.GameManager.TacticalMode);
        ChangeUnitButtons(actor.Unit);
        UpdateButtons();
    }

    private string DetermineAllignment(Unit unit)
    {
        if (!unit.HasFixedSide()) return "Default";
        if (State.World?.MainEmpires != null)
        {
            return State.World.GetEmpireOfSide(unit.FixedSide)?.Name ?? unit.Race.ToString();
        }
        else
            return Equals(unit.FixedSide, State.GameManager.TacticalMode.GetDefenderSide()) ? "Defender" : "Attacker";
    }

    public void Open(Unit unit)
    {
        gameObject.SetActive(true);
        if (UnitEditor == null)
        {
            InfoPanel.ExpBar = ExpBar;
            InfoPanel.HealthBar = HealthBar;
            InfoPanel.ManaBar = ManaBar;
            UnitEditor = new UnitEditor(unit, this, InfoPanel);
            SetUpRaces();
        }
        else
        {
            UnitEditor.SetUnit(unit);
            UnitEditor.RefreshStats();
        }

        if (_raceDict.TryGetValue(unit.Race, out int race))
        {
            RaceDropdown.value = race;
        }
        else
            RaceDropdown.value = 0;

        AlignmentDropdown.captionText.text = DetermineAllignment(unit);
        HiddenToggle.isOn = unit.HiddenFixedSide;
        PopulateItems();
        TraitList.text = UnitEditor.Unit.ListTraits();
        SwapAlignment.gameObject.SetActive(State.GameManager.CurrentScene == State.GameManager.TacticalMode);
        ChangeUnitButtons(unit);
        UpdateButtons();
    }

    public void ClearStatus()
    {
        UnitEditor.ClearStatus();
    }

    public void Close()
    {
        for (int i = 0; i < SpellDropdown.Length; i++)
        {
            int spellIndex = SpellDropdown[i].value;
            SpellType spell = (SpellType)Enum.GetValues(typeof(SpellType)).GetValue(spellIndex);
            //if (spell > SpellTypes.Resurrection)
            //    spell = spell - SpellTypes.Resurrection + SpellTypes.AlraunePuff - 1;
            if (spell != SpellType.None)
            {
                if (UnitEditor.Unit.InnateSpells.Count > i)
                    UnitEditor.Unit.InnateSpells[i] = spell;
                else if (!UnitEditor.Unit.InnateSpells.Contains(spell)) UnitEditor.Unit.InnateSpells.Add(spell);
            }
            else if (UnitEditor.Unit.InnateSpells.Count > i)
            {
                UnitEditor.Unit.InnateSpells.RemoveAt(i);
            }
        }

        UnitEditor.Unit.UpdateSpells();
        gameObject.SetActive(false);
        if (State.GameManager.CurrentScene == State.GameManager.RecruitMode)
        {
            State.GameManager.RecruitMode.ButtonCallback(10);
        }
        else if (State.GameManager.CurrentScene == State.GameManager.TacticalMode)
        {
            State.GameManager.TacticalMode.RebuildInfo();
            State.GameManager.TacticalMode.UpdateHealthBars();
        }

        var ownerEmp = State.World.GetEmpireOfSide(UnitEditor.Unit.Side);
        if (ownerEmp != null && ownerEmp.StrategicAI != null)
        {
            StrategicUtilities.SetAIClass(UnitEditor.Unit);
        }
    }

    public override void ChangeGender()
    {
        UnitEditor?.ChangeGender();
    }

    public override void ChangePronouns()
    {
        UnitEditor?.ChangePronouns();
    }

    public void ChangeRace()
    {
        if (UnitEditor.Unit == null) return;
        if (RaceFuncs.TryParse(RaceDropdown.options[RaceDropdown.value].text, out Race race))
        {
            if (Equals(UnitEditor.Unit.Race, race)) return;
            UnitEditor.Unit.Race = race;
            UnitEditor.Unit.RandomizeGender(race, RaceFuncs.GetRace(UnitEditor.Unit));
            UnitEditor.Unit.SetGear(race);
            UnitEditor.ClearAnimations();
            RandomizeUnit();
            UnitEditor.Unit.ReloadTraits();
            UnitEditor.Unit.InitializeTraits();
            UnitEditor.RefreshActor();
            PopulateItems();
            TraitList.text = UnitEditor.Unit.ListTraits();
        }
    }

    public void ChangeSide()
    {
        UnitEditor.ChangeSide();
    }

    public void PopulateItems()
    {
        var allItems = State.World.ItemRepository.GetAllItems();
        int maxIndex = Math.Min(ItemDropdown.Length, UnitEditor.Unit.Items.Length);
        for (int i = 0; i < ItemDropdown.Length; i++)
        {
            ItemDropdown[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < maxIndex; i++)
        {
            ItemDropdown[i].gameObject.SetActive(true);
            if (UnitEditor.Unit.Items[i] == null)
            {
                ItemDropdown[i].value = 0;
                ItemDropdown[i].captionText.text = "Empty";
                continue;
            }

            int value = 0;
            for (int j = 0; j < allItems.Count; j++)
            {
                if (allItems[j].Name == UnitEditor.Unit.Items[i].Name)
                {
                    value = j;
                    break;
                }
            }

            ItemDropdown[i].value = value + 1;
        }

        for (int i = 0; i < SpellDropdown.Length; i++)
        {
            if (UnitEditor.Unit.InnateSpells == null) UnitEditor.Unit.InnateSpells = new List<SpellType>();
            if (UnitEditor.Unit.InnateSpells.Count > i)
            {
                var value = Array.IndexOf(Enum.GetValues(typeof(SpellType)), UnitEditor.Unit.InnateSpells[i]);
                //if (value > SpellTypes.Resurrection)
                //    value = value - SpellTypes.AlraunePuff + SpellTypes.Resurrection + 1;
                SpellDropdown[i].value = (int)value;
            }
            else
                SpellDropdown[i].value = 0;

            SpellDropdown[i].RefreshShownValue();
        }
    }

    public void ChangeItem(int slot)
    {
        if (UnitEditor.Unit == null) return;
        Item newItem = null;

        if (_itemDict.TryGetValue(ItemDropdown[slot].value, out string value))
        {
            newItem = State.World.ItemRepository.GetAllItems().Where(s => s.Name == value).FirstOrDefault();
        }


        UnitEditor.ChangeItem(slot, newItem);
        UnitEditor.RefreshView();
    }

    public void ChangeAlignment()
    {
        if (UnitEditor.Unit == null) return;

        if (AlignmentDropdown.options[AlignmentDropdown.value].text == "Default")
            UnitEditor.Unit.FixedSide = Side.TrueNoneSide;
        else if (AlignmentDropdown.options[AlignmentDropdown.value].text == "Defender")
            UnitEditor.Unit.FixedSide = State.GameManager.TacticalMode.GetDefenderSide();
        else if (AlignmentDropdown.options[AlignmentDropdown.value].text == "Attacker")
            UnitEditor.Unit.FixedSide = State.GameManager.TacticalMode.GetAttackerSide();
        else if (State.World?.MainEmpires != null)
        {
            UnitEditor.Unit.FixedSide = _empireDict[AlignmentDropdown.value].Side;
        }

        ToggleHidden();
    }

    public void ToggleHidden()
    {
        if (UnitEditor.Unit == null) return;
        UnitEditor.Unit.HiddenFixedSide = HiddenToggle.isOn;
        UnitEditor.RefreshView();
    }

    public void RandomizeUnit()
    {
        RaceFuncs.GetRace(UnitEditor.Unit).RandomCustomCall(UnitEditor.Unit);
        UnitEditor.RefreshView();
        UnitEditor.RefreshGenderSelector();
    }

    public void AddTrait()
    {
        if (UnitEditor.Unit == null) return;
        if (State.RandomizeLists.Any(rl => rl.Name == TraitDropdown.options[TraitDropdown.value].text))
        {
            RandomizeList randomizeList = State.RandomizeLists.Single(rl => rl.Name == TraitDropdown.options[TraitDropdown.value].text);
            var resTraits = UnitEditor.Unit.RandomizeOne(randomizeList);
            foreach (TraitType resTrait in resTraits)
            {
                UnitEditor.AddTrait(resTrait);
                if (resTrait == TraitType.Resourceful || resTrait == TraitType.BookWormI || resTrait == TraitType.BookWormIi || resTrait == TraitType.BookWormIii)
                {
                    UnitEditor.Unit.SetMaxItems();
                    PopulateItems();
                }
            }

            UnitEditor.RefreshActor();
            TraitList.text = UnitEditor.Unit.ListTraits();
        }

        if (Enum.TryParse(TraitDropdown.options[TraitDropdown.value].text, out TraitType trait))
        {
            UnitEditor.AddTrait(trait);
            if (trait == TraitType.Resourceful || trait == TraitType.BookWormI || trait == TraitType.BookWormIi || trait == TraitType.BookWormIii)
            {
                UnitEditor.Unit.SetMaxItems();
                PopulateItems();
            }

            UnitEditor.RefreshActor();
            TraitList.text = UnitEditor.Unit.ListTraits();
        }
    }

    public void ManualChangeStat(Stat stat, int dummy)
    {
        if (UnitEditor.Unit == null) return;
        var input = Instantiate(State.GameManager.InputBoxPrefab).GetComponent<InputBox>();
        input.SetData((s) =>
            {
                UnitEditor.Unit.ModifyStat(stat, s - UnitEditor.Unit.GetStatBase(stat));
                UnitEditor.RefreshStats();
                UpdateButtons();
            }, "Change", "Cancel", $"Modify {stat}?", 6);
    }

    public void AddTraitsText()
    {
        if (UnitEditor.Unit == null) return;
        foreach (RandomizeList rl in State.RandomizeLists)
        {
            if (TraitsText.text.ToLower().Contains(rl.Name.ToString().ToLower()))
            {
                var resTraits = UnitEditor.Unit.RandomizeOne(rl);
                foreach (TraitType resTrait in resTraits)
                {
                    UnitEditor.AddTrait(resTrait);
                    if (resTrait == TraitType.Resourceful || resTrait == TraitType.BookWormI || resTrait == TraitType.BookWormIi || resTrait == TraitType.BookWormIii)
                    {
                        UnitEditor.Unit.SetMaxItems();
                        PopulateItems();
                    }
                }

                UnitEditor.RefreshActor();
                TraitList.text = UnitEditor.Unit.ListTraits();
            }
        }

        foreach (TraitType trait in (Stat[])Enum.GetValues(typeof(TraitType)))
        {
            if (TraitsText.text.ToLower().Contains(trait.ToString().ToLower()))
            {
                UnitEditor.AddTrait(trait);
                if (trait == TraitType.Resourceful || trait == TraitType.BookWormI || trait == TraitType.BookWormIi || trait == TraitType.BookWormIii)
                {
                    UnitEditor.Unit.SetMaxItems();
                    PopulateItems();
                }

                UnitEditor.RefreshActor();
                TraitList.text = UnitEditor.Unit.ListTraits();
            }
        }
    }

    public void RemoveTrait()
    {
        if (UnitEditor.Unit == null) return;

        if (Enum.TryParse(TraitDropdown.options[TraitDropdown.value].text, out TraitType trait))
        {
            UnitEditor.RemoveTrait(trait);
            UnitEditor.RefreshActor();
            TraitList.text = UnitEditor.Unit.ListTraits();
            if (trait == TraitType.Resourceful)
            {
                UnitEditor.Unit.SetMaxItems();
                PopulateItems();
            }
        }
    }

    public void RestoreHealth()
    {
        if (UnitEditor.Unit == null) return;
        UnitEditor.RestoreHealth();
    }

    public void RestoreMana()
    {
        if (UnitEditor.Unit == null) return;
        UnitEditor.RestoreMana();
    }

    public void RestoreMovement()
    {
        if (UnitEditor.Unit == null) return;
        UnitEditor.RestoreMovement();
    }

    public void ShowLevelSetter()
    {
        var inputBox = Instantiate(State.GameManager.InputBoxPrefab).GetComponent<InputBox>();

        inputBox.SetData(UnitEditor.SetLevelTo, "Update Level", "Cancel", "Use this to set the units current level, automatically applying level ups or downs", 5);
    }
}