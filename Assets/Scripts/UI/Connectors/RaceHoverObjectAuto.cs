using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaceHoverObjectAuto : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool _hovering;

    private Race _race;

    private void Start()
    {
        var comp = GetComponentInChildren<TextMeshProUGUI>();
        if (comp == null)
        {
            Destroy(this);
            return;
        }

        if (RaceFuncs.TryParse(GetComponentInChildren<TextMeshProUGUI>().text, out Race result))
            _race = result;
        else
            Destroy(this);
    }

    private void Update()
    {
        if (_hovering == false) return;
        State.GameManager.HoveringRacePicture.UpdateInformation(_race);
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