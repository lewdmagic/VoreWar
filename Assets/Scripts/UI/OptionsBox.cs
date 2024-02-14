using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsBox : MonoBehaviour
{
    private Action _aAction;
    private Action _bAction;
    private Action _cAction;
    private Action _dAction;
    private Action _eAction;
    public Button A;
    public Button B;
    public Button C;
    public Button D;
    public Button E;
    public Text Text;

    public void SetData(string mainText, string aText, Action aAction, string bText, Action bAction, string cText = null, Action cAction = null, string dText = null, Action dAction = null, string eText = null, Action eAction = null)
    {
        _aAction = aAction;
        A.GetComponentInChildren<Text>().text = aText;
        _bAction = bAction;
        B.GetComponentInChildren<Text>().text = bText;
        _cAction = cAction;
        if (cText != null)
            C.GetComponentInChildren<Text>().text = cText;
        else
            C.gameObject.SetActive(false);
        _dAction = dAction;
        if (dText != null)
            D.GetComponentInChildren<Text>().text = dText;
        else
            D.gameObject.SetActive(false);
        _eAction = eAction;
        if (eText != null)
            E.GetComponentInChildren<Text>().text = eText;
        else
            E.gameObject.SetActive(false);
        Text.text = mainText;
    }

    public void AClicked()
    {
        _aAction?.Invoke();
        Destroy(gameObject);
    }

    public void BClicked()
    {
        _bAction?.Invoke();
        Destroy(gameObject);
    }

    public void CClicked()
    {
        _cAction?.Invoke();
        Destroy(gameObject);
    }

    public void DClicked()
    {
        _dAction?.Invoke();
        Destroy(gameObject);
    }

    public void EClicked()
    {
        _eAction?.Invoke();
        Destroy(gameObject);
    }
}