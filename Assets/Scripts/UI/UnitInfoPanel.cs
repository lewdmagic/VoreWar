using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitInfoPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool _hovering;

    private int _prevNames = 0;

    public UIUnitSprite Sprite;
    public TextMeshProUGUI BasicInfo;
    public TextMeshProUGUI InfoText;
    public GameObject StatBlock;
    public Slider ExpBar;
    public Slider HealthBar;
    public Slider ManaBar;
    internal Unit Unit;
    internal ActorUnit Actor;

    private int _nameInstances;

    public string HoveringName;

    private void Update()
    {
        if (_hovering == false) return;

        if (Unit == null) return;
        TextMeshProUGUI hoverBox;
        if (Input.mousePosition.y > InfoText.transform.parent.position.y && BasicInfo)
            hoverBox = BasicInfo;
        else
            hoverBox = InfoText;
        
        if (StatBlock)
        {
            // Actually, I changed my mind. I love coding.
            if (IsMousingOver(StatBlock))
            {
                GameObject STRLabel = StatBlock.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
                GameObject DEXLabel = StatBlock.transform.GetChild(0).GetChild(1).GetChild(0).gameObject;
                GameObject MNDLabel = StatBlock.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
                GameObject WLLLabel = StatBlock.transform.GetChild(1).GetChild(1).GetChild(0).gameObject;
                GameObject ENDLabel = StatBlock.transform.GetChild(2).GetChild(0).GetChild(0).gameObject;
                GameObject AGILabel = StatBlock.transform.GetChild(2).GetChild(1).GetChild(0).gameObject;
                GameObject VORLabel = StatBlock.transform.GetChild(3).GetChild(0).GetChild(0).gameObject;
                GameObject STMLabel = StatBlock.transform.GetChild(3).GetChild(1).GetChild(0).gameObject;
                GameObject LDRLabel = StatBlock.transform.GetChild(4).GetChild(0).GetChild(0).gameObject;
                GameObject E1Label = StatBlock.transform.GetChild(5).GetChild(0).GetChild(0).gameObject;
                GameObject E2Label = StatBlock.transform.GetChild(5).GetChild(1).GetChild(0).gameObject;
                GameObject E3Label = StatBlock.transform.GetChild(5).GetChild(2).GetChild(0).gameObject;

                foreach (var item in new GameObject[] {STRLabel, DEXLabel, MNDLabel, WLLLabel, ENDLabel, AGILabel, VORLabel, STMLabel, LDRLabel, E1Label, E2Label, E3Label})
                {
                    if (IsMousingOver(item))
                        hoverBox = item.GetComponent<TextMeshProUGUI>();
                }
            }
        }



        
        int wordIndex = TMP_TextUtilities.FindIntersectingWord(hoverBox, Input.mousePosition, null);
        //if (wordIndex <= -1 && BasicInfo)
        //{
        //    wordIndex = TMP_TextUtilities.FindIntersectingWord(BasicInfo, Input.mousePosition, null);
        //    HoverBox = BasicInfo;
        //}
        if (wordIndex > -1)
        {
            string[] words = new string[5];
            for (int i = 0; i < 5; i++)
            {
                if (wordIndex - 2 + i < 0 || wordIndex - 2 + i >= hoverBox.textInfo.wordCount || hoverBox.textInfo.wordInfo[wordIndex - 2 + i].characterCount < 1)
                {
                    words[i] = string.Empty;
                    continue;
                }

                words[i] = hoverBox.textInfo.wordInfo[wordIndex - 2 + i].GetWord();
            }

            State.GameManager.HoveringTooltip.UpdateInformation(words, Unit, Actor);
            if (Input.GetMouseButtonDown(0))
            {
                if (words[2] == "UnitEditor")
                {
                    if (Actor == null)
                        State.GameManager.UnitEditor.Open(Unit);
                    else
                        State.GameManager.UnitEditor.Open(Actor);
                    return;
                }

                if (_nameInstances <= 1)
                    DisplayInfoFor(HoveringName);
                else
                {
                    _prevNames = 0;
                    for (int i = 2; i < wordIndex; i++) //Don't use your own name as the first name
                    {
                        if (hoverBox.textInfo.wordInfo[i].GetWord() == words[2])
                            _prevNames += 1;
                        else if (wordIndex + 1 >= hoverBox.textInfo.wordCount && HoveringName == $"{hoverBox.textInfo.wordInfo[i].GetWord()} {hoverBox.textInfo.wordInfo[i + 1].GetWord()}")
                            _prevNames += 1;
                        else if (wordIndex + 2 >= hoverBox.textInfo.wordCount && HoveringName == $"{hoverBox.textInfo.wordInfo[i].GetWord()} {hoverBox.textInfo.wordInfo[i + 1].GetWord()} {hoverBox.textInfo.wordInfo[i + 2].GetWord()}") _prevNames += 1;
                    }

                    DisplayInfoFor(HoveringName, _prevNames);
                }
            }
            else
            {
                _nameInstances = 0;
                if (Actor?.Unit != null && (bool)Actor?.Unit.Predator)
                {
                    foreach (var prey in Actor.PredatorComponent.GetAllPrey())
                    {
                        if (prey.Unit.Name == words[2])
                        {
                            State.GameManager.HoveringTooltip.HoveringValidName();
                            HoveringName = words[2];
                            _nameInstances += 1;
                        }
                        else if (prey.Unit.Name == $"{words[2]} {words[3]}")
                        {
                            State.GameManager.HoveringTooltip.HoveringValidName();
                            HoveringName = $"{words[2]} {words[3]}";
                            _nameInstances += 1;
                        }
                        else if (prey.Unit.Name == $"{words[2]} {words[3]} {words[4]}")
                        {
                            State.GameManager.HoveringTooltip.HoveringValidName();
                            HoveringName = $"{words[2]} {words[3]} {words[4]}";
                            _nameInstances += 1;
                        }
                    }
                }

                if (_nameInstances == 0) HoveringName = "RAREASFARARA";
            }
        }
    }

    public bool IsMousingOver(GameObject thing)
    {
        Vector3[] v = new Vector3[4];
        thing.GetComponent<RectTransform>().GetWorldCorners(v);
        Rect rect = new Rect(v[0].x, v[0].y, v[2].x - v[0].x, v[2].y - v[0].y);
        return rect.Contains(Input.mousePosition);
    }

    
    private void DisplayInfoFor(string name)
    {
        if (Actor?.Unit.Predator == false || Actor?.PredatorComponent == null) return;
        foreach (var prey in Actor.PredatorComponent.GetAllPrey())
        {
            if (prey.Unit.Name == name)
            {
                State.GameManager.TacticalMode.InfoPanel.RefreshTacticalUnitInfo(prey.Actor);
            }
        }
    }

    private void DisplayInfoFor(string name, int instance)
    {
        int count = 0;
        if (Actor?.Unit.Predator == false) return;
        foreach (var prey in Actor.PredatorComponent.GetAllPrey())
        {
            if (prey.Unit.Name == name)
            {
                if (count == _prevNames)
                {
                    State.GameManager.TacticalMode.InfoPanel.RefreshTacticalUnitInfo(prey.Actor);
                    break;
                }
                else
                    count++;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hovering = false;
    }
}