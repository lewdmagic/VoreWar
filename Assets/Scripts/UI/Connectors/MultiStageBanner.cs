﻿using System.Linq;
using UnityEngine;

public class MultiStageBanner : MonoBehaviour
{
    public SpriteRenderer LeaderLayer;
    public SpriteRenderer FullBannerLayer;
    public SpriteRenderer InsigniaLayer;
    public SpriteRenderer SelectedBackLayer;
    public SpriteRenderer SelectedColorLayer;
    private ParticleSystem system;

    public void Refresh(Army army, bool selected)
    {
        transform.localScale = new Vector3(Config.BannerScale, Config.BannerScale, 1);
        Empire empire = army.Empire;
        LeaderLayer.enabled = army.LeaderIfInArmy() != null;
        if (empire.UnitySecondaryColor != default)
        {
            FullBannerLayer.color = empire.UnitySecondaryColor;
        }
        else
        {
            FullBannerLayer.color = Color.white;
        }

        InsigniaLayer.color = empire.UnityColor;
        // TODONE what is 25?
        // 25 is where the race banners start
        if (army.BannerStyle == 0)
        {
            InsigniaLayer.sprite = RaceFuncs.BannerSprite(empire.Race);
        }
        else
        {
            InsigniaLayer.sprite = State.GameManager.StrategyMode.Banners[2 + army.BannerStyle];
        }
        //Color TransparentPrimaryColor = new Color(empire.UnityColor.r, empire.UnityColor.g, empire.UnityColor.b, 0.5f);
        //Color TransparentSecondaryColor = new Color(empire.UnitySecondaryColor.r, empire.UnitySecondaryColor.g, empire.UnitySecondaryColor.b, 0.5f);

        if (army.Units.All(u => u.HasTrait(TraitType.Infiltrator) && !u.IsInfiltratingSide(u.Side)))
        {
            FullBannerLayer.color = new Color(FullBannerLayer.color.r, FullBannerLayer.color.g, FullBannerLayer.color.b, 0.5f);
            InsigniaLayer.color = new Color(InsigniaLayer.color.r, InsigniaLayer.color.g, InsigniaLayer.color.b, 0.5f);
            LeaderLayer.color = new Color(LeaderLayer.color.r, LeaderLayer.color.g, LeaderLayer.color.b, 0.5f);
        }

        SelectedBackLayer.enabled = selected;
        SelectedColorLayer.enabled = selected;
        if (selected)
        {
            SelectedColorLayer.color = empire.UnityColor;
        }

        if (empire.StrategicAI == null && State.World.ActingEmpire == empire && army.Units.Where(s => s.HasEnoughExpToLevelUp()).Any())
        {
            if (system == null)
            {
                system = Instantiate(State.GameManager.ParticleSystem, gameObject.transform).GetComponent<ParticleSystem>();
                var main = system.main;
                main.startColor = new ParticleSystem.MinMaxGradient(empire.UnityColor);
            }
        }
        else
        {
            if (system != null)
            {
                system.gameObject.SetActive(false);
            }
        }
    }
}