using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageLogPanel : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Button IncreaseSize;
    public Button DecreaseSize;

    public Button Restore;

    public Toggle ShowOdds;
    public Toggle ShowHealing;
    public Toggle ShowSpells;
    public Toggle ShowMisses;
    public Toggle ShowInformational;
    public Toggle ShowWeaponCombat;
    public Toggle ShowFluff;

    private RectTransform _rect;

    private int _size = 1;

    private bool _initialized = false;

    public void SetBase()
    {
        _initialized = false;
        TacticalUtilities.Log.ShowWeaponCombat = PlayerPrefs.GetInt("LogShowWeaponCombat", 1) == 1;
        TacticalUtilities.Log.ShowOdds = PlayerPrefs.GetInt("LogShowOdds", 0) == 1;
        TacticalUtilities.Log.ShowHealing = PlayerPrefs.GetInt("LogShowHealing", 1) == 1;
        TacticalUtilities.Log.ShowSpells = PlayerPrefs.GetInt("LogShowSpells", 1) == 1;
        TacticalUtilities.Log.ShowMisses = PlayerPrefs.GetInt("LogShowMisses", 1) == 1;
        TacticalUtilities.Log.ShowInformational = PlayerPrefs.GetInt("LogShowInformational", 1) == 1;
        TacticalUtilities.Log.ShowPureFluff = PlayerPrefs.GetInt("LogShowPureFluff", 1) == 1;
        ShowWeaponCombat.isOn = TacticalUtilities.Log.ShowWeaponCombat;
        ShowOdds.isOn = TacticalUtilities.Log.ShowOdds;
        ShowHealing.isOn = TacticalUtilities.Log.ShowHealing;
        ShowSpells.isOn = TacticalUtilities.Log.ShowSpells;
        ShowMisses.isOn = TacticalUtilities.Log.ShowMisses;
        ShowInformational.isOn = TacticalUtilities.Log.ShowInformational;
        ShowFluff.isOn = TacticalUtilities.Log.ShowPureFluff;
        _initialized = true;
    }

    public void Refresh()
    {
        if (_initialized == false) return;
        TacticalUtilities.Log.ShowWeaponCombat = ShowWeaponCombat.isOn;
        TacticalUtilities.Log.ShowOdds = ShowOdds.isOn;
        TacticalUtilities.Log.ShowHealing = ShowHealing.isOn;
        TacticalUtilities.Log.ShowSpells = ShowSpells.isOn;
        TacticalUtilities.Log.ShowMisses = ShowMisses.isOn;
        TacticalUtilities.Log.ShowInformational = ShowInformational.isOn;
        TacticalUtilities.Log.ShowPureFluff = ShowFluff.isOn;
        PlayerPrefs.SetInt("LogShowWeaponCombat", ShowWeaponCombat.isOn ? 1 : 0);
        PlayerPrefs.SetInt("LogShowOdds", ShowOdds.isOn ? 1 : 0);
        PlayerPrefs.SetInt("LogShowHealing", ShowHealing.isOn ? 1 : 0);
        PlayerPrefs.SetInt("LogShowSpells", ShowSpells.isOn ? 1 : 0);
        PlayerPrefs.SetInt("LogShowMisses", ShowMisses.isOn ? 1 : 0);
        PlayerPrefs.SetInt("LogShowInformational", ShowInformational.isOn ? 1 : 0);
        PlayerPrefs.SetInt("LogShowPureFluff", ShowFluff.isOn ? 1 : 0);
        PlayerPrefs.Save();
        TacticalUtilities.Log.RefreshListing();
    }


    public void Expand()
    {
        if (_rect == null) _rect = GetComponent<RectTransform>();
        if (_size == 0)
        {
            Restore.gameObject.SetActive(false);
            gameObject.SetActive(true);
            //rect.sizeDelta = new Vector2(1920, 160);
            //rect.anchoredPosition = new Vector3(0, 118, 0);            
            _size = 1;
        }
        else if (_size == 1)
        {
            _rect.anchorMax = new Vector2(1, 1);
            _rect.pivot = new Vector2(.5f, .5f);
            _rect.sizeDelta = new Vector2(1920, Screen.height);
            transform.position = Vector3.zero;
            _rect.offsetMax = Vector2.zero;
            _rect.offsetMin = Vector2.zero;
            _size = 2;
            IncreaseSize.interactable = false;
            ShowHealing.gameObject.SetActive(true);
            ShowMisses.gameObject.SetActive(true);
            ShowInformational.gameObject.SetActive(true);
            ShowOdds.gameObject.SetActive(true);
            ShowWeaponCombat.gameObject.SetActive(true);
            ShowFluff.gameObject.SetActive(true);
            ShowSpells.gameObject.SetActive(true);
        }
    }

    public void Shrink()
    {
        if (_rect == null) _rect = GetComponent<RectTransform>();
        if (_size == 2)
        {
            _rect.anchorMax = new Vector2(0, 0);
            _rect.pivot = Vector2.zero;
            _rect.sizeDelta = new Vector2(1920, 160);
            _rect.anchoredPosition = new Vector3(0, 98, 0);
            _size = 1;
            IncreaseSize.interactable = true;
            ShowHealing.gameObject.SetActive(false);
            ShowMisses.gameObject.SetActive(false);
            ShowInformational.gameObject.SetActive(false);
            ShowOdds.gameObject.SetActive(false);
            ShowWeaponCombat.gameObject.SetActive(false);
            ShowFluff.gameObject.SetActive(false);
            ShowSpells.gameObject.SetActive(false);
        }
        else if (_size == 1)
        {
            gameObject.SetActive(false);
            _size = 0;
            Restore.gameObject.SetActive(true);
        }
    }
}