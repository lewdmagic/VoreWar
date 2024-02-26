using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderRevealer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Slider _slider;

    private bool _hovering;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hovering = false;
    }

    private void TrySetValue(string fl)
    {
        if (float.TryParse(fl, out float result)) _slider.value = result;
    }

    // Use this for initialization
    private void Start()
    {
        _slider = GetComponentInParent<Slider>();
        if (_slider == null) Debug.LogWarning("SliderRevealer has no slider!");
    }

    // Update is called once per frame
    private void Update()
    {
        if (_hovering && _slider.interactable)
        {
            State.GameManager.HoveringTooltip.UpdateInformation(_slider);

            if (Input.GetMouseButton(1))
            {
                var box = Instantiate(State.GameManager.InputBoxPrefab).GetComponentInChildren<InputBox>();
                box.SetData(TrySetValue, "Set Value", "Cancel", $"Change the value of this slider?  Allowed Range {_slider.minValue} - {_slider.maxValue}", 4);
            }
        }
    }
}