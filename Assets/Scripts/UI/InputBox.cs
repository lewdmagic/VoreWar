using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputBox : MonoBehaviour
{
    private bool _stringMode = false;
    private Action<int> _yesAction;
    private Action<string> _yesActionString;
    private Action _noAction;
    public TMP_InputField InputField;
    public Button Yes;
    public Button No;
    public Text Text;

    public void SetData(Action<int> action, string yesText, string noText, string mainText, int characterLimit, Action noAction = null)
    {
        State.GameManager.ActiveInput = true;
        _stringMode = false;
        _yesAction = action;
        Yes.GetComponentInChildren<Text>().text = yesText;
        No.GetComponentInChildren<Text>().text = noText;
        Text.text = mainText;
        _noAction = noAction;
        InputField.characterLimit = characterLimit;
        InputField.ActivateInputField();
    }

    public void SetData(Action<string> action, string yesText, string noText, string mainText, int characterLimit, Action noAction = null)
    {
        State.GameManager.ActiveInput = true;
        _stringMode = true;
        InputField.contentType = TMP_InputField.ContentType.Standard;
        _yesActionString = action;
        Yes.GetComponentInChildren<Text>().text = yesText;
        No.GetComponentInChildren<Text>().text = noText;
        Text.text = mainText;
        _noAction = noAction;
        InputField.characterLimit = characterLimit;
        InputField.ActivateInputField();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Menu") && _noAction == null) NoClicked();
    }

    public void ActivateTypeMethod(Func<string, string> getYesValue)
    {
        InputField.onValueChanged.AddListener((s) => Yes.GetComponentInChildren<Text>().text = getYesValue(InputField.text));
    }

    public void YesClicked()
    {
        if (_stringMode == false && int.TryParse(InputField.text, out int result))
        {
            _yesAction?.Invoke(result);
        }
        else if (_stringMode)
        {
            _yesActionString?.Invoke(InputField.text);
        }
        else
        {
            State.GameManager.CreateMessageBox("Invalid value");
        }

        State.GameManager.ActiveInput = false;
        Destroy(gameObject);
    }

    public void NoClicked()
    {
        _noAction?.Invoke();
        State.GameManager.ActiveInput = false;
        Destroy(gameObject);
    }
}