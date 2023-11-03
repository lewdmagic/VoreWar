#region

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#endregion

public class UIUnitSprite : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Text Name;

    private CompleteSprite _completeSprite;

    private Actor_Unit _lastActor;
    private Race _lastRace;

    [HideInInspector] private int index = -1;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (index >= 0)
        {
            if (State.GameManager.CurrentScene == State.GameManager.Recruit_Mode)
            {
                State.GameManager.Recruit_Mode.Select(index);
            }
            else
            {
                State.GameManager.StrategyMode.ExchangerUI.Select(index);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (index >= 0)
        {
            if (State.GameManager.CurrentScene != State.GameManager.Recruit_Mode)
            {
                State.GameManager.StrategyMode.ExchangerUI.UpdateInfo(index);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (index >= 0)
        {
            if (State.GameManager.CurrentScene != State.GameManager.Recruit_Mode)
            {
                State.GameManager.StrategyMode.ExchangerUI.UpdateInfo(-20);
            }
        }
    }

    public void SetIndex(int num)
    {
        index = num;
    }

    public void ResetBellyScale(Actor_Unit actor)
    {
        if (_completeSprite != null)
        {
            _completeSprite.ResetBellyScale();
        }
    }

    public void UpdateSprites(Actor_Unit actor, bool locked = true)
    {
        if (actor != _lastActor || _lastRace == null || !Equals(actor.Unit.Race, _lastRace))
        {
            if (_completeSprite != null)
            {
                _completeSprite.Destroy();
            }

            _completeSprite = new CompleteSprite(State.GameManager.ImagePrefab, null, transform, actor);
        }

        //CompleteSprite.SetActor(actor);
        _completeSprite.UpdateSprite();
        if (locked)
        {
            if (actor.AnimationController?.frameLists != null)
            {
                for (int i = 0; i < actor.AnimationController.frameLists.Length; i++)
                {
                    actor.AnimationController.frameLists[i].currentFrame = 0;
                }
            }
        }

        _completeSprite.UpdateSprite();
        //The second one is designed to fix the squishbreasts flag not applying correctly on the first round.  I could do a lot of complicated stuff to fix, or just this

        Name.color = actor.Unit.HasEnoughExpToLevelUp() ? new Color(1, .6f, 0) : new Color(.196f, .196f, .196f);
        _lastActor = actor;
        _lastRace = actor.Unit.Race;
    }
}