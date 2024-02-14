using UnityEngine;
using UnityEngine.UI;

public class CustomizerSaver : MonoBehaviour
{
    public InputField Text;
    private bool _openedFromEditor;

    public void Open(bool openedFromEditor)
    {
        _openedFromEditor = openedFromEditor;
        if (_openedFromEditor)
            Text.text = State.GameManager.UnitEditor.UnitEditor.Unit.Name;
        else
            Text.text = State.GameManager.RecruitMode.Customizer.Unit.Name;
        gameObject.SetActive(true);
    }

    public void Save()
    {
        CustomizerData custom = new CustomizerData();
        if (_openedFromEditor)
            custom.CopyFromUnit(State.GameManager.UnitEditor.UnitEditor.Unit);
        else
            custom.CopyFromUnit(State.GameManager.RecruitMode.Customizer.Unit);
        custom.Name = Text.text;
        CustomizationDataStorer.Add(custom);
    }
}