using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecruitCheatsPanel : MonoBehaviour
{
    public TMP_Dropdown ArmyPicker;
    public TMP_Dropdown TraitPicker;

    public Button SwapArmyButton;
    public Button AddTraitButton;
    public Button RemoveTraitButton;

    public Button RefreshButton;

    private bool _init = false;

    private Army _army;

    internal void Setup(Army army)
    {
        _army = army;
        if (_init == false)
        {
            foreach (TraitType traitId in ((TraitType[])Enum.GetValues(typeof(TraitType))).OrderBy(s => { return s >= TraitType.LightningSpeed ? "ZZZ" + s.ToString() : s.ToString(); }))
            {
                TraitPicker.options.Add(new TMP_Dropdown.OptionData(traitId.ToString()));
            }

            TraitPicker.RefreshShownValue();
            SwapArmyButton.onClick.AddListener(MoveToAnotherEmpire);
            AddTraitButton.onClick.AddListener(AddTrait);
            RemoveTraitButton.onClick.AddListener(RemoveTrait);
            RefreshButton.onClick.AddListener(Refresh);
            _init = true;
        }


        ArmyPicker.ClearOptions();

        foreach (Empire empire in State.World.MainEmpires.Where(s => s.KnockedOut == false))
        {
            ArmyPicker.options.Add(new TMP_Dropdown.OptionData(empire.Name));
        }

        ArmyPicker.RefreshShownValue();
    }

    private void Refresh()
    {
        _army.Refresh();
    }

    private void MoveToAnotherEmpire()
    {
        if (StrategicUtilities.GetVillageAt(_army.Position) != null)
        {
            State.GameManager.CreateMessageBox("Can't switch sides in villages, it generates bugs.");
            return;
        }

        if (State.World.MainEmpires.Where(s => s.Name == ArmyPicker.captionText.text).Any() == false)
        {
            State.GameManager.CreateMessageBox("Invalid Empire, try repicking from the dropdown.");
            return;
        }

        var emp = State.World.MainEmpires.Where(s => s.Name == ArmyPicker.captionText.text).First();
        if (_army.Units.Where(s => s.Type == UnitType.Leader).Any())
        {
            State.GameManager.CreateMessageBox("That army had a leader in it, unexpected behavior may occur when the leader dies.");
        }

        State.GameManager.RecruitMode.ButtonCallback(86);
        State.GameManager.SwitchToStrategyMode();
        _army.Units.ForEach(unit => { unit.Side = emp.Side; });
        State.GameManager.StrategyMode.RedrawArmies();
    }

    private void AddTrait()
    {
        if (Enum.TryParse(TraitPicker.captionText.text, out TraitType trait))
        {
            foreach (var unit in _army.Units)
            {
                unit.AddPermanentTrait(trait);
            }
        }
    }

    private void RemoveTrait()
    {
        if (Enum.TryParse(TraitPicker.captionText.text, out TraitType trait))
        {
            foreach (var unit in _army.Units)
            {
                unit.RemoveTrait(trait);
            }
        }
    }
}