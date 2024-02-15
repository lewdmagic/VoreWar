#region

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#endregion

public class UIUnitSprite : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Text Name;

    private RaceRenderer _raceRenderer;

    private ActorUnit _lastActor;
    private Race _lastRace;

    [HideInInspector]
    private int _index = -1;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_index >= 0)
        {
            if (State.GameManager.CurrentScene == State.GameManager.RecruitMode)
            {
                State.GameManager.RecruitMode.Select(_index);
            }
            else
            {
                State.GameManager.StrategyMode.ExchangerUI.Select(_index);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_index >= 0)
        {
            if (State.GameManager.CurrentScene != State.GameManager.RecruitMode)
            {
                State.GameManager.StrategyMode.ExchangerUI.UpdateInfo(_index);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_index >= 0)
        {
            if (State.GameManager.CurrentScene != State.GameManager.RecruitMode)
            {
                State.GameManager.StrategyMode.ExchangerUI.UpdateInfo(-20);
            }
        }
    }

    public void SetIndex(int num)
    {
        _index = num;
    }

    public void ResetBellyScale(ActorUnit actor)
    {
        if (_raceRenderer != null)
        {
            _raceRenderer.ResetBellyScale();
        }
    }

    public void UpdateSprites(ActorUnit actor, bool locked = true)
    {
        if (actor != _lastActor || _lastRace == null || !Equals(actor.Unit.Race, _lastRace))
        {
            if (_raceRenderer != null)
            {
                _raceRenderer.Destroy();
            }

            _raceRenderer = new RaceRenderer(State.GameManager.ImagePrefab, null, transform, actor);
        }

        //CompleteSprite.SetActor(actor);
        _raceRenderer.UpdateSprite();
        if (locked)
        {
            if (actor.AnimationController?.FrameLists != null)
            {
                for (int i = 0; i < actor.AnimationController.FrameLists.Length; i++)
                {
                    actor.AnimationController.FrameLists[i].CurrentFrame = 0;
                }
            }
        }

        _raceRenderer.UpdateSprite();
        //The second one is designed to fix the squishbreasts flag not applying correctly on the first round.  I could do a lot of complicated stuff to fix, or just this

        Name.color = actor.Unit.HasEnoughExpToLevelUp() ? new Color(1, .6f, 0) : new Color(.196f, .196f, .196f);
        _lastActor = actor;
        _lastRace = actor.Unit.Race;
    }
}