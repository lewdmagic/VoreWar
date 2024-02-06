using System.Collections.Generic;

public class ButtonCustomizer : IButtonCustomizer
{
    private class ChangeData
    {
        internal bool? Active;
        internal string Text;
    }

    private readonly Dictionary<ButtonType, ChangeData> _data = new Dictionary<ButtonType, ChangeData>();

    private ChangeData GetOrSet(ButtonType type) => _data.GetOrSet(type, () => new ChangeData());

    public void SetText(ButtonType type, string text)
    {
        GetOrSet(type).Text = text;
    }

    public void SetActive(ButtonType type, bool active)
    {
        GetOrSet(type).Active = active;
    }

    internal void ApplyValues(EnumIndexedArray<ButtonType, CustomizerButton> buttons)
    {
        foreach (var pair in _data)
        {
            ButtonType key = pair.Key;
            ChangeData value = pair.Value;
            CustomizerButton customizerButton = buttons[key];

            if (value.Active.HasValue)
            {
                customizerButton.gameObject.SetActive(value.Active.Value);
            }
            
            if (value.Text != null)
            {
                customizerButton.Label.text = value.Text;
            }
        }
    }
}