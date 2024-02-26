using UnityEngine;
using UnityEngine.EventSystems;

public class MapEditorDoodad : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public StrategicDoodadType Type;

    public void OnPointerClick(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetDoodadType(Type, transform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetDoodadTooltip(Type);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetBlankTooltip();
    }
}