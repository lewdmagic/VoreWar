using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    private Action _yesAction;
    private Action _noAction;
    public Button Yes;
    public Button No;
    public Text Text;

    public void SetData(Action action, string yesText, string noText, string mainText, Action noAction = null)
    {
        _yesAction = action;
        Yes.GetComponentInChildren<Text>().text = yesText;
        No.GetComponentInChildren<Text>().text = noText;
        Text.text = mainText;
        _noAction = noAction;
    }

    public void YesClicked()
    {
        _yesAction?.Invoke();
        Destroy(gameObject);
    }

    public void NoClicked()
    {
        _noAction?.Invoke();
        Destroy(gameObject);
    }
}