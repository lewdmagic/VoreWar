using UnityEngine;
using UnityEngine.EventSystems;

public class MapEditorVillage : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string RaceName;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (RaceFuncs.TryParse(RaceName, out var race))
        {
            State.GameManager.MapEditor.SetVillageType(race, transform);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (RaceFuncs.TryParse(RaceName, out var race))
        {
            State.GameManager.MapEditor.SetVillageTooltip(race);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetBlankTooltip();
    }
}