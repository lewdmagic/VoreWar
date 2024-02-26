using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemExchangeControl : MonoBehaviour
{
    internal ItemType Type;
    public Button TransferToRightButton;
    public Button TransferToLeftButton;
    public TextMeshProUGUI LeftText;
    public TextMeshProUGUI RightText;
    public TextMeshProUGUI ItemText;

    public void UpdateValues(int left, int right)
    {
        LeftText.text = left.ToString();
        RightText.text = right.ToString();
        ItemText.text = Type.ToString();
        TransferToRightButton.interactable = left > 0;
        TransferToLeftButton.interactable = right > 0;
    }

    public void TransferRight()
    {
        State.GameManager.StrategyMode.ExchangerUI.TransferItemToRight(Type);
    }

    public void TransferLeft()
    {
        State.GameManager.StrategyMode.ExchangerUI.TransferItemToLeft(Type);
    }
}