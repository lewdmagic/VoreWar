using System;
using UnityEngine;
using UnityEngine.UI;

public class EditStatButton : MonoBehaviour
{
    public Text Label;
    public Stat Stat;
    public Unit Unit;
    private UnitEditorPanel _parent;
    public string DefaultText;
    public Button Increase;
    public Button IncreaseLevel;
    public Button Decrease;
    public Button DecreaseLevel;

    internal void SetData(Stat stat, int value, Action<Stat, int> statAction, Action<Stat, int> levelAction, Action<Stat, int> manualSetAction, Unit unit, UnitEditorPanel parent)
    {
        Label.text = $"{stat}\n{value.ToString()}";
        DefaultText = $"{stat}\n{value.ToString()}";
        Stat = stat;
        Unit = unit;
        _parent = parent;
        Increase.onClick.AddListener(() => statAction(stat, 1));
        IncreaseLevel.onClick.AddListener(() => levelAction(stat, 1));
        Decrease.onClick.AddListener(() => statAction(stat, -1));
        DecreaseLevel.onClick.AddListener(() => levelAction(stat, -1));
        var button = Label.gameObject.AddComponent<Button>();
        button.onClick.AddListener(() => manualSetAction(stat, 0));
        if (Stat == Stat.Leadership && Unit.Type != UnitType.Leader)
        {
            Label.text = $"{Stat}\nN/A";
            DefaultText = $"{Stat}\nN/A";
        }
    }

    public void UpdateLabel()
    {
        Label.text = $"{Stat}\n{Unit.GetStat(Stat)}";
        DefaultText = $"{Stat}\n{Unit.GetStat(Stat)}";
        if (Stat == Stat.Leadership && Unit.Type != UnitType.Leader)
        {
            Label.text = $"{Stat}\nN/A";
            DefaultText = $"{Stat}\nN/A";
        }
    }

    public void UpdateStat(int baseNum)
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) baseNum *= 5;
        Unit.ModifyStat(Stat.GetHashCode(), baseNum);
        _parent.UpdateButtons();
    }

    public void ChangeLevel(int change)
    {
        if (change > 0)
        {
            Unit.SetExp(Unit.ExperienceRequiredForNextLevel);
            Unit.LevelUp(Stat);
        }
        else
        {
            Unit.LevelDown(Stat);
            Unit.SetExp(Unit.GetExperienceRequiredForLevel(Unit.Level - 1));
        }

        _parent.UpdateButtons();
    }
}