using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeEndOfTurnModeUI : MonoBehaviour
{
    public TMP_Dropdown Dropdown;

    public Toggle Display;


    public void Open()
    {
        gameObject.SetActive(true);
        Dropdown.value = PlayerPrefs.GetInt("AutoAdvance", 1);
        Display.isOn = PlayerPrefs.GetInt("DisplayEndOfTurnText", 0) == 1;
        Dropdown.RefreshShownValue();
    }

    public void CloseAndApply()
    {
        gameObject.SetActive(false);
        PlayerPrefs.SetInt("AutoAdvance", Dropdown.value);
        Config.AutoAdvance = (Config.AutoAdvanceType)PlayerPrefs.GetInt("AutoAdvance", 1);

        PlayerPrefs.SetInt("DisplayEndOfTurnText", Display.isOn ? 1 : 0);
        Config.DisplayEndOfTurnText = PlayerPrefs.GetInt("DisplayEndOfTurnText", 0) == 1;
        State.GameManager.TacticalMode.UpdateEndTurnButtonText();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}