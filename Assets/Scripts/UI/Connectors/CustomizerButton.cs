using System;
using UnityEngine;
using UnityEngine.UI;

public class CustomizerButton : MonoBehaviour
{
    public Text Label;
    public string DefaultText;
    public Button Increase;
    public Button Decrease;

    internal void SetData(string text, Action<int> action)
    {
        Label.text = text;
        DefaultText = text;
        Increase.onClick.AddListener(() => action(1));
        Decrease.onClick.AddListener(() => action(-1));
    }
}