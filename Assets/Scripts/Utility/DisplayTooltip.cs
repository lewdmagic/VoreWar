using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int Value;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (State.GameManager.Menu.ContentSettings.gameObject.activeSelf)
            State.GameManager.Menu.ContentSettings.ChangeToolTip(Value);
        else if (State.GameManager.Menu.Options.gameObject.activeSelf)
            State.GameManager.Menu.Options.ChangeToolTip(Value);
        else if (State.GameManager.StartMode.CreateStrategicGame.gameObject.activeSelf)
            State.GameManager.StartMode.CreateStrategicGame.ChangeToolTip(Value);
        else if (State.GameManager.StartMode.CreateTacticalGame.gameObject.activeSelf)
            State.GameManager.StartMode.CreateTacticalGame.ChangeToolTip(Value);
        else if (State.GameManager.Menu.RaceEditor.gameObject.activeSelf)
        {
            State.GameManager.HoveringTooltip.UpdateInformationDefaultTooltip(Value);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (State.GameManager.Menu.ContentSettings.gameObject.activeSelf)
            State.GameManager.Menu.ContentSettings.ChangeToolTip(0);
        else if (State.GameManager.Menu.Options.gameObject.activeSelf)
            State.GameManager.Menu.Options.ChangeToolTip(0);
        else if (State.GameManager.StartMode.CreateStrategicGame.gameObject.activeSelf)
            State.GameManager.StartMode.CreateStrategicGame.ChangeToolTip(0);
        else if (State.GameManager.StartMode.CreateTacticalGame.gameObject.activeSelf)
            State.GameManager.StartMode.CreateTacticalGame.ChangeToolTip(0);
        else if (State.GameManager.Menu.RaceEditor.gameObject.activeSelf)
        {
            State.GameManager.HoveringTooltip.gameObject.SetActive(false);
        }
    }
}