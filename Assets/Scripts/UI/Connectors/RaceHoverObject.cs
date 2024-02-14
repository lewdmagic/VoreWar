using UnityEngine;
using UnityEngine.EventSystems;

public class RaceHoverObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool _hovering;
    internal Race Race;


    private void Update()
    {
        if (_hovering == false) return;
        State.GameManager.HoveringRacePicture.UpdateInformation(Race);
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