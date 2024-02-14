using UnityEngine;
using UnityEngine.EventSystems;

public class MapEditorTile : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public StrategicTileType Type;

    public void OnPointerClick(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetTileType(Type, transform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetTileTooltip(Type);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetBlankTooltip();
    }
}