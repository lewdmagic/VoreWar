using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoveringRacePicture : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private RectTransform _rect;
    private int _remainingFrames = 0;
    private Race _lastRace = Race.Selicia;
    public UIUnitSprite ActorSprite;
    private ActorUnit _actor;
    private float _lastUpdate;

    private void Start()
    {
        _rect = GetComponent<RectTransform>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (_remainingFrames > 0)
            _remainingFrames--;
        else
            gameObject.SetActive(false);
    }

    public void UpdateInformation(Race race)
    {
        if (_actor == null) _actor = new ActorUnit(new Unit(Race.Cat));
        if (!Equals(_lastRace, race))
        {
            _actor.Unit.Race = race;
            _actor.Unit.TotalRandomizeAppearance();
            ActorSprite.UpdateSprites(_actor);
            ActorSprite.Name.text = race.ToString();
            var images = ActorSprite.GetComponentsInChildren<Image>();
            foreach (Image image in images)
            {
                image.raycastTarget = false;
            }

            _lastRace = race;
        }

        gameObject.SetActive(true);
        _remainingFrames = 3;
        _text.text = "";
        float xAdjust = 10;
        float exceeded = Input.mousePosition.x + _rect.rect.width * Screen.width / 1920 - Screen.width;
        if (exceeded > 0) xAdjust = -exceeded;
        float yAdjust = 0;
        exceeded = Input.mousePosition.y - _rect.rect.height * Screen.height / 1080;
        if (exceeded < 0) yAdjust = -exceeded;
        transform.position = Input.mousePosition + new Vector3(xAdjust, yAdjust, 0);
    }
}