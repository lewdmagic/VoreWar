using System.Collections.Generic;


// public interface IButtonChangeData
// {
//     IButtonChangeData Active(bool active);
//     IButtonChangeData Text(string text);
// }

public class ButtonCustomizer
{
    private class ChangeData// : IButtonChangeData
    {
        internal bool? Active;
        internal string Text;
        
        // public IButtonChangeData Active(bool active)
        // {
        //     SetActiveField = active;
        //     return this;
        // }
        //
        // public IButtonChangeData Text(string text)
        // {
        //     LabelTextField = text;
        //     return this;
        // }
    }

    private readonly Dictionary<ButtonType, ChangeData> data = new Dictionary<ButtonType, ChangeData>();

    private ChangeData GetOrSet(ButtonType type) => data.GetOrSet(type, () => new ChangeData());
    
    // public IButtonChangeData ChangeButton(ButtonType type)
    // {
    //     ChangeData changeData = data.GetOrSet(type, () => new ChangeData());
    //     return changeData;
    // }

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
        foreach (var pair in data)
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