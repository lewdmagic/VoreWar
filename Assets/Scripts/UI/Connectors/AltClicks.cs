﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AltClicks : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent LeftClick;
    public UnityEvent MiddleClick;
    public UnityEvent RightClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            LeftClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Middle)
            MiddleClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right) RightClick.Invoke();
    }
}