﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoveringRacePicture : MonoBehaviour
{
    private TextMeshProUGUI text;
    private RectTransform rect;
    private int remainingFrames = 0;
    private Race LastRace = Race.Selicia;
    public UIUnitSprite ActorSprite;
    private Actor_Unit Actor;
    private float lastUpdate;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (remainingFrames > 0)
            remainingFrames--;
        else
            gameObject.SetActive(false);
    }

    public void UpdateInformation(Race race)
    {
        if (Actor == null) Actor = new Actor_Unit(new Unit(Race.Cat));
        if (!Equals(LastRace, race))
        {
            Actor.Unit.Race = race;
            Actor.Unit.TotalRandomizeAppearance();
            ActorSprite.UpdateSprites(Actor);
            ActorSprite.Name.text = race.ToString();
            var images = ActorSprite.GetComponentsInChildren<Image>();
            foreach (Image image in images)
            {
                image.raycastTarget = false;
            }

            LastRace = race;
        }

        gameObject.SetActive(true);
        remainingFrames = 3;
        text.text = "";
        float xAdjust = 10;
        float exceeded = Input.mousePosition.x + rect.rect.width * Screen.width / 1920 - Screen.width;
        if (exceeded > 0) xAdjust = -exceeded;
        float yAdjust = 0;
        exceeded = Input.mousePosition.y - rect.rect.height * Screen.height / 1080;
        if (exceeded < 0) yAdjust = -exceeded;
        transform.position = Input.mousePosition + new Vector3(xAdjust, yAdjust, 0);
    }
}