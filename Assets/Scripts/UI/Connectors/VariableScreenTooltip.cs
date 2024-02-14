using UnityEngine;
using UnityEngine.EventSystems;

public class VariableScreenTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (State.GameManager.VariableEditor.gameObject.activeSelf) State.GameManager.VariableEditor.ChangeToolTip(Text);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (State.GameManager.VariableEditor.gameObject.activeSelf) State.GameManager.VariableEditor.ChangeToolTip("");
    }
}