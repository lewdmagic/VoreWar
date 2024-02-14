using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomizerTrait : MonoBehaviour
{
    internal int ID;
    public InputField Name;
    public InputField Chance;
    public Button PickTagsBtn;
    public Button CloneBtn;
    public Button RemoveBtn;


    [AllowEditing]
    internal Dictionary<TraitType, bool> TraitDictionary;

    public void OpenTraitsDict()
    {
        State.GameManager.VariableEditor.Open(this);
    }
}