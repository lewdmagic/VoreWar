using UnityEngine;
using UnityEngine.UI;

public class UniformSaver : MonoBehaviour
{
    public InputField Text;

    public Toggle IncludeHat;
    public Toggle IncludeClothingAccesory;

    private bool _openedFromEditor;

    public void Open(bool openedFromEditor)
    {
        _openedFromEditor = openedFromEditor;
        Unit unit;
        if (_openedFromEditor)
            unit = State.GameManager.UnitEditor.UnitEditor.Unit;
        else
            unit = State.GameManager.RecruitMode.Customizer.Unit;

        Text.text = unit.Name;
        var raceData = Races2.GetRace(unit.Race);
        if (raceData.SetupOutput.AllowedClothingHatTypes.Count > 0)
        {
            IncludeHat.interactable = true;
        }
        else
        {
            IncludeHat.isOn = false;
            IncludeHat.interactable = false;
        }

        if (raceData.SetupOutput.AllowedClothingAccessoryTypes.Count > 0)
        {
            IncludeClothingAccesory.interactable = true;
        }
        else
        {
            IncludeClothingAccesory.isOn = false;
            IncludeClothingAccesory.interactable = false;
        }

        gameObject.SetActive(true);
    }

    public void Save()
    {
        UniformData uniform = new UniformData();
        if (_openedFromEditor)
            uniform.CopyFromUnit(State.GameManager.UnitEditor.UnitEditor.Unit, IncludeHat.isOn, IncludeClothingAccesory.isOn);
        else
            uniform.CopyFromUnit(State.GameManager.RecruitMode.Customizer.Unit, IncludeHat.isOn, IncludeClothingAccesory.isOn);
        uniform.Name = Text.text;
        UniformDataStorer.Add(uniform);
    }
}