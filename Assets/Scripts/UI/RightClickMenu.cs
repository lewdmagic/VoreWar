using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightClickMenu : MonoBehaviour
{
    private Button[] _buttons;
    public Transform ButtonFolder;
    public GameObject ButtonPrefab;
    private RectTransform _rect;

    private Button[] _pounceButtons;
    public Transform PouncePanel;
    private RectTransform _pounceRect;

    private bool _activeWait;
    private bool _pounceNeedsRefresh;

    private const int MaxButtons = 30;

    private struct CommandData
    {
        internal ActorUnit Actor;
        internal ActorUnit Target;
        internal int Range;
        internal float DevourChance;
    }

    public void CloseAll()
    {
        gameObject.SetActive(false);
        PouncePanel.gameObject.SetActive(false);
    }

    public void Open(ActorUnit actor, ActorUnit target)
    {
        PouncePanel.gameObject.SetActive(false);
        _pounceNeedsRefresh = true;
        if (State.TutorialMode && State.GameManager.TutorialScript.Step < 6)
        {
            State.GameManager.CreateMessageBox("Can't use the right click action menu for the first battle of the tutorial");
            return;
        }

        if (_rect == null) _rect = GetComponent<RectTransform>();
        if (!target.Hidden)
        {
            gameObject.SetActive(true);
            CreateButtons(actor, target);
        }
        else
        {
            State.GameManager.TacticalMode.OrderSelectedUnitToMoveTo(target.Position.X, target.Position.Y);
            return;
        }

        float xAdjust = 10;
        float exceeded = Input.mousePosition.x + _rect.rect.width * Screen.width / 1920 - Screen.width;
        if (exceeded > 0) xAdjust = -exceeded;
        transform.position = Input.mousePosition + new Vector3(xAdjust, 0, 0);
    }

    public void OpenWithNoTarget(ActorUnit actor, Vec2I location)
    {
        PouncePanel.gameObject.SetActive(false);

        if (_rect == null) _rect = GetComponent<RectTransform>();
        gameObject.SetActive(true);
        CreateButtonsForNoTarget(actor, location);
        float xAdjust = 10;
        float exceeded = Input.mousePosition.x + _rect.rect.width * Screen.width / 1920 - Screen.width;
        if (exceeded > 0) xAdjust = -exceeded;
        transform.position = Input.mousePosition + new Vector3(xAdjust, 0, 0);
    }

    public void CreateButtonsForNoTarget(ActorUnit actor, Vec2I location)
    {
        int currentButton = 0;
        const int buttonCount = MaxButtons;
        if (_buttons == null)
        {
            _buttons = new Button[buttonCount];
            for (int i = 0; i < buttonCount; i++)
            {
                _buttons[i] = Instantiate(ButtonPrefab, ButtonFolder).GetComponent<Button>();
            }
        }

        for (int i = 0; i < buttonCount; i++)
        {
            _buttons[i].gameObject.SetActive(false);
            _buttons[i].interactable = true;
            _buttons[i].onClick.RemoveAllListeners();
            Destroy(_buttons[i].gameObject.GetComponent<EventTrigger>());
        }

        if (TacticalUtilities.OpenTile(location, actor))
        {
            _buttons[currentButton].GetComponentInChildren<Text>().text = "Move to location";
            _buttons[currentButton].onClick.AddListener(() => State.GameManager.TacticalMode.OrderSelectedUnitToMoveTo(location.X, location.Y));
            _buttons[currentButton].onClick.AddListener(FinishMoveAction);
            currentButton++;
        }

        int range = actor.Position.GetNumberOfMovesDistance(location);
        foreach (Spell spell in actor.Unit.UseableSpells)
        {
            if (spell.AcceptibleTargets.Contains(AbilityTargets.Tile))
            {
                currentButton = AddSpellLocation(spell, actor, location, currentButton, range, 1);
            }
        }

        foreach (TargetedTacticalAction action in TacticalActionList.TargetedActions.Where(s => s.OnExecuteLocation != null && s.AppearConditional(actor)))
        {
            _buttons[currentButton].onClick.AddListener(() => action.OnExecuteLocation(actor, location));
            _buttons[currentButton].onClick.AddListener(FinishAction);
            _buttons[currentButton].GetComponentInChildren<Text>().text = action.Name;
            currentButton++;
        }

        if (currentButton == 0)
        {
            CloseAll();
        }
        else
            ActivateButtons(currentButton);
    }

    public void CreateButtons(ActorUnit actor, ActorUnit target)
    {
        bool sneakAttack = false;
        bool rubCreated = false;
        if (TacticalUtilities.SneakAttackCheck(actor.Unit, target.Unit))
        {
            sneakAttack = true;
        }

        //var racePar = RaceParameters.GetTraitData(actor.Unit.Race);
        int currentButton = 0;
        const int buttonCount = MaxButtons;
        if (_buttons == null)
        {
            _buttons = new Button[buttonCount];
            for (int i = 0; i < buttonCount; i++)
            {
                _buttons[i] = Instantiate(ButtonPrefab, ButtonFolder).GetComponent<Button>();
            }
        }

        for (int i = 0; i < buttonCount; i++)
        {
            _buttons[i].gameObject.SetActive(false);
            _buttons[i].interactable = true;
            _buttons[i].onClick.RemoveAllListeners();
            Destroy(_buttons[i].gameObject.GetComponent<EventTrigger>());
        }

        int range = actor.Position.GetNumberOfMovesDistance(target.Position);

        if (actor == target)
        {
            foreach (Spell spell in actor.Unit.UseableSpells)
            {
                if (spell.AcceptibleTargets.Contains(AbilityTargets.Ally) || spell.AcceptibleTargets.Contains(AbilityTargets.Self))
                {
                    currentButton = AddSpell(spell, actor, target, currentButton, range, 1);
                }
            }

            _buttons[currentButton].onClick.AddListener(() => actor.BellyRub(target));
            _buttons[currentButton].onClick.AddListener(FinishAction);
            if (target.ReceivedRub)
            {
                _buttons[currentButton].interactable = false;
                _buttons[currentButton].GetComponentInChildren<Text>().text = "Belly Rub\nAlready rubbed";
            }
            else
                _buttons[currentButton].GetComponentInChildren<Text>().text = "Belly Rub";

            currentButton++;

            foreach (var action in TacticalActionList.UntargetedActions.Where(a => a.AppearConditional(actor)))
            {
                _buttons[currentButton].onClick.AddListener(() => action.OnClicked());
                _buttons[currentButton].onClick.AddListener(FinishAction);
                _buttons[currentButton].GetComponentInChildren<Text>().text = action.Name;
                currentButton++;
            }

            ActivateButtons(currentButton);
            return;
        }


        if (TacticalUtilities.IsUnitControlledByPlayer(target.Unit) || Equals(target.Unit.Side, actor.Unit.Side))
        {
            foreach (Spell spell in actor.Unit.UseableSpells)
            {
                if (spell.AcceptibleTargets.Contains(AbilityTargets.Ally))
                {
                    currentButton = AddSpell(spell, actor, target, currentButton, range, 1);
                }
            }

            _buttons[currentButton].onClick.AddListener(() => actor.BellyRub(target));
            _buttons[currentButton].onClick.AddListener(FinishAction);
            if (target.ReceivedRub)
            {
                _buttons[currentButton].interactable = false;
                _buttons[currentButton].GetComponentInChildren<Text>().text = "Belly Rub\nAlready rubbed";
            }
            else
                _buttons[currentButton].GetComponentInChildren<Text>().text = "Belly Rub";

            if (range != 1 || target.PredatorComponent?.Fullness <= 0) _buttons[currentButton].interactable = false;
            rubCreated = true;
            currentButton++;

            if (target.Surrendered == false && actor.Unit.HasTrait(TraitType.Cruel) == false && Config.AllowInfighting == false)
            {
                if (actor.Unit.HasTrait(TraitType.Endosoma))
                {
                    float devChance;
                    if (actor.Unit.Predator)
                        devChance = Mathf.Round(100 * target.GetDevourChance(actor, true));
                    else
                        devChance = 0;

                    CommandData data2 = new CommandData()
                    {
                        Actor = actor,
                        Target = target,
                        Range = range,
                        DevourChance = devChance
                    };
                    currentButton = AddVore(actor, currentButton, data2);
                }

                ActivateButtons(currentButton);
            }
        }

        float devourChance;
        if (actor.Unit.Predator)
            devourChance = Mathf.Round(100 * target.GetDevourChance(actor, true));
        else
            devourChance = 0;
        CommandData data = new CommandData()
        {
            Actor = actor,
            Target = target,
            Range = range,
            DevourChance = devourChance
        };
        int damage = actor.WeaponDamageAgainstTarget(target, false);
        if (!TacticalUtilities.IsUnitControlledByPlayer(target.Unit) || Config.AllowInfighting || (!State.GameManager.TacticalMode.AIDefender && !State.GameManager.TacticalMode.AIAttacker))
        {
            _buttons[currentButton].onClick.AddListener(() => State.GameManager.TacticalMode.MeleeAttack(actor, target));
            _buttons[currentButton].onClick.AddListener(FinishAction);
            _buttons[currentButton].GetComponentInChildren<Text>().text = $"Melee Attack {Math.Round(100 * target.GetAttackChance(actor, false, true))}% {(damage >= target.Unit.Health ? "Kill" : $"{damage} dmg")} ";
            if (range != 1) _buttons[currentButton].interactable = false;
            currentButton++;


            if (actor.BestRanged != null)
            {
                _buttons[currentButton].onClick.AddListener(() => State.GameManager.TacticalMode.RangedAttack(actor, target));
                _buttons[currentButton].onClick.AddListener(FinishAction);
                damage = actor.WeaponDamageAgainstTarget(target, true);
                _buttons[currentButton].GetComponentInChildren<Text>().text = $"Ranged Attack {Math.Round(100 * target.GetAttackChance(actor, true, true))}% {(damage >= target.Unit.Health ? "Kill" : $"{damage} dmg")} ";
                if (actor.BestRanged.Omni == false && (range < 2 || range > actor.BestRanged.Range)) _buttons[currentButton].interactable = false;
                currentButton++;
            }


            if (actor.Unit.UseableSpells != null)
            {
                foreach (Spell spell in actor.Unit.UseableSpells)
                {
                    if (spell.AcceptibleTargets.Contains(AbilityTargets.Enemy))
                    {
                        if (spell == SpellList.Maw)
                            currentButton = AddSpell(spell, actor, target, currentButton, range, target.GetMagicChance(actor, spell) * target.GetDevourChance(actor, skillBoost: actor.Unit.GetStat(Stat.Mind)));
                        else if (spell == SpellList.Bind && target.Unit.Type != UnitType.Summon)
                            AddSpell(spell, actor, target, currentButton, range, 0);
                        else
                            currentButton = AddSpell(spell, actor, target, currentButton, range, target.GetMagicChance(actor, spell));
                    }
                }
            }


            if (actor.Unit.HasTrait(TraitType.Pounce))
            {
                _buttons[currentButton].onClick.AddListener(() => CreatePounceButtons(actor, target));
                if (actor.Movement > 1)
                {
                    _buttons[currentButton].GetComponentInChildren<Text>().text = "Pounces =>";
                    var trigger = _buttons[currentButton].gameObject.AddComponent<EventTrigger>();
                    EventTrigger.Entry entry = new EventTrigger.Entry
                    {
                        eventID = EventTriggerType.PointerEnter
                    };
                    entry.callback.AddListener((s) => { CreatePounceButtons(actor, target); });
                    trigger.triggers.Add(entry);
                    entry = new EventTrigger.Entry
                    {
                        eventID = EventTriggerType.PointerExit
                    };
                    entry.callback.AddListener((s) => { Invoke("QueueCloseLoop", .25f); });
                    trigger.triggers.Add(entry);
                    if (range < 2 || range > 4) _buttons[currentButton].interactable = false;
                    currentButton++;
                }
                else
                {
                    _buttons[currentButton].GetComponentInChildren<Text>().text = "Pounces (No AP)";
                    _buttons[currentButton].interactable = false;
                    currentButton++;
                }
            }
        }

        if (!Equals(target.Unit.GetApparentSide(actor.Unit), actor.Unit.GetApparentSide()) && !Equals(target.Unit.GetApparentSide(actor.Unit), actor.Unit.FixedSide) &&
            !rubCreated &&
            (Config.CanUseStomachRubOnEnemies || actor.Unit.HasTrait(TraitType.SeductiveTouch)))
        {
            _buttons[currentButton].onClick.AddListener(() => actor.BellyRub(target));
            _buttons[currentButton].onClick.AddListener(FinishAction);
            if (target.ReceivedRub)
            {
                _buttons[currentButton].interactable = false;
                _buttons[currentButton].GetComponentInChildren<Text>().text = "Belly Rub\nAlready rubbed";
            }
            else
                _buttons[currentButton].GetComponentInChildren<Text>().text = "Belly Rub" + (actor.Unit.HasTrait(TraitType.SeductiveTouch) ? " (Seduce " + Math.Round(100 * target.GetPureStatClashChance(actor.Unit.GetStat(Stat.Dexterity), target.Unit.GetStat(Stat.Will), .1f)) + "%)" : "");

            if (range != 1 || !(target.PredatorComponent?.Fullness > 0)) // Still can't rub empty bellies
                _buttons[currentButton].interactable = false;
            currentButton++;
        }

        currentButton = AddVore(actor, currentButton, data);

        if (actor.Unit.HasTrait(TraitType.ShunGokuSatsu))
        {
            if (TacticalActionList.TargetedDictionary.TryGetValue(SpecialAction.ShunGokuSatsu, out var targetedAction))
            {
                if (targetedAction.AppearConditional(data.Actor))
                {
                    _buttons[currentButton].onClick.AddListener(() => targetedAction.OnExecute(data.Actor, data.Target));
                    _buttons[currentButton].onClick.AddListener(FinishAction);
                    damage = 2 * actor.WeaponDamageAgainstTarget(target, false);
                    _buttons[currentButton].GetComponentInChildren<Text>().text = $"Shun Goku Satsu {Math.Round(100 * target.GetAttackChance(actor, false, true))}% {(damage >= target.Unit.Health ? "Kill" : $"{damage} dmg")} ";
                    if (data.Range != 1) _buttons[currentButton].interactable = false;
                    currentButton++;
                }
            }
        }


        if (actor.Unit.Predator)
        {
            if (data.Target.Unit.Predator) data.DevourChance = Mathf.Round(100 * data.Target.PredatorComponent.GetVoreStealChance(data.Actor));
            currentButton = AddKtCommands(actor, currentButton, data);
        }

        ActivateButtons(currentButton);
    }

    private int AddVore(ActorUnit actor, int currentButton, CommandData data)
    {
        if (actor.Unit.Predator)
        {
            var voreTypes = State.RaceSettings.GetVoreTypes(actor.Unit.Race);
            if (voreTypes.Contains(VoreType.Oral))
            {
                _buttons[currentButton].onClick.AddListener(() => State.GameManager.TacticalMode.VoreAttack(data.Actor, data.Target));
                _buttons[currentButton].onClick.AddListener(FinishAction);
                _buttons[currentButton].GetComponentInChildren<Text>().text = $"Oral Vore {data.DevourChance}%";
                if (actor.Unit.HasTrait(TraitType.RangedVore))
                {
                    if (data.Range > 4) _buttons[currentButton].interactable = false;
                }
                else
                {
                    if (data.Range != 1) _buttons[currentButton].interactable = false;
                }

                if (data.Actor.PredatorComponent.FreeCap() < data.Target.Bulk())
                {
                    _buttons[currentButton].GetComponentInChildren<Text>().text = $"Too bulky to vore";
                    _buttons[currentButton].interactable = false;
                }

                currentButton++;
            }

            currentButton = AltVore(actor, currentButton, SpecialAction.BreastVore, data);
            currentButton = AltVore(actor, currentButton, SpecialAction.CockVore, data);
            currentButton = AltVore(actor, currentButton, SpecialAction.Unbirth, data);
            currentButton = AltVore(actor, currentButton, SpecialAction.AnalVore, data);
            currentButton = AltVore(actor, currentButton, SpecialAction.TailVore, data);
        }

        return currentButton;
    }

    private int AltVore(ActorUnit actor, int currentButton, SpecialAction actionType, CommandData data)
    {
        if (TacticalActionList.TargetedDictionary.TryGetValue(actionType, out var targetedAction))
        {
            if (targetedAction.AppearConditional(data.Actor) && (targetedAction.RequiresPred == false || data.Actor.Unit.Predator))
            {
                _buttons[currentButton].onClick.AddListener(() => targetedAction.OnExecute(data.Actor, data.Target));
                _buttons[currentButton].onClick.AddListener(FinishAction);
                if (actionType == SpecialAction.TailVore && Equals(actor.Unit.Race, Race.Terrorbird))
                    _buttons[currentButton].GetComponentInChildren<Text>().text = $"Crop Vore {data.DevourChance}%";
                else if (actionType == SpecialAction.BreastVore && Equals(actor.Unit.Race, Race.Kangaroo))
                    _buttons[currentButton].GetComponentInChildren<Text>().text = $"Pouch Vore {data.DevourChance}%";
                else
                    _buttons[currentButton].GetComponentInChildren<Text>().text = $"{targetedAction.Name} {data.DevourChance}%";
                if (actor.Unit.HasTrait(TraitType.RangedVore))
                {
                    if (data.Range > 4) _buttons[currentButton].interactable = false;
                }
                else
                {
                    if (data.Range != 1) _buttons[currentButton].interactable = false;
                }

                if (data.Actor.PredatorComponent.FreeCap() < data.Target.Bulk())
                {
                    _buttons[currentButton].GetComponentInChildren<Text>().text = $"Too bulky to {targetedAction.Name}";
                    _buttons[currentButton].interactable = false;
                }
                else if (data.Actor.BodySize() < data.Target.BodySize() * 3 && actor.Unit.HasTrait(TraitType.TightNethers) && (actionType == SpecialAction.CockVore || actionType == SpecialAction.Unbirth))
                {
                    _buttons[currentButton].GetComponentInChildren<Text>().text = $"Too large to {targetedAction.Name}";
                    _buttons[currentButton].interactable = false;
                }

                currentButton++;
                return currentButton;
            }
        }

        return currentButton;
    }

    private int AddSpell(Spell spell, ActorUnit actor, ActorUnit target, int currentButton, int range, float spellChance)
    {
        int modifiedManaCost = spell.ManaCost +
                               spell.ManaCost * (actor.Unit.GetStatusEffect(StatusEffectType.SpellForce) != null ? actor.Unit.GetStatusEffect(StatusEffectType.SpellForce).Duration / 10 : 0);
        if (actor.Unit.Mana >= modifiedManaCost || spell.IsFree)
            _buttons[currentButton].GetComponentInChildren<Text>().text = $"{spell.Name} {(spell.Resistable ? Mathf.Round(100 * spellChance).ToString() : "100")}%";
        else
            _buttons[currentButton].GetComponentInChildren<Text>().text = $"{spell.Name} (no mana)";
        _buttons[currentButton].onClick.AddListener(() => spell.TryCast(actor, target));
        if ((range < spell.Range.Min || range > spell.Range.Max || actor.Unit.Mana < modifiedManaCost) && !spell.IsFree) _buttons[currentButton].interactable = false;
        _buttons[currentButton].onClick.AddListener(FinishAction);
        currentButton++;
        return currentButton;
    }

    private int AddSpellLocation(Spell spell, ActorUnit actor, Vec2I location, int currentButton, int range, float spellChance)
    {
        int modifiedManaCost = spell.ManaCost +
                               spell.ManaCost * (actor.Unit.GetStatusEffect(StatusEffectType.SpellForce) != null ? actor.Unit.GetStatusEffect(StatusEffectType.SpellForce).Duration / 10 : 0);

        if (actor.Unit.Mana >= modifiedManaCost || spell.IsFree)
            _buttons[currentButton].GetComponentInChildren<Text>().text = $"{spell.Name}";
        else
            _buttons[currentButton].GetComponentInChildren<Text>().text = $"{spell.Name} (no mana)";
        _buttons[currentButton].onClick.AddListener(() => spell.TryCast(actor, location));
        if ((range < spell.Range.Min || range > spell.Range.Max || actor.Unit.Mana < modifiedManaCost) && !spell.IsFree) _buttons[currentButton].interactable = false;
        _buttons[currentButton].onClick.AddListener(FinishAction);
        currentButton++;
        return currentButton;
    }

    public void CreatePounceButtons(ActorUnit actor, ActorUnit target)
    {
        if (_pounceNeedsRefresh == false)
        {
            PouncePanel.gameObject.SetActive(true);
            _activeWait = false;
        }

        int currentButton = 0;
        const int buttonCount = 7;
        if (_pounceButtons == null)
        {
            _pounceButtons = new Button[buttonCount];
            for (int i = 0; i < buttonCount; i++)
            {
                _pounceButtons[i] = Instantiate(ButtonPrefab, PouncePanel).GetComponent<Button>();
            }
        }

        for (int i = 0; i < buttonCount; i++)
        {
            _pounceButtons[i].gameObject.SetActive(false);
            _pounceButtons[i].interactable = true;
            _pounceButtons[i].onClick.RemoveAllListeners();
        }

        int range = actor.Position.GetNumberOfMovesDistance(target.Position);


        if (_pounceRect == null) _pounceRect = PouncePanel.GetComponent<RectTransform>();
        PouncePanel.gameObject.SetActive(true);
        float xAdjust = 60;
        float exceeded = Input.mousePosition.x + _pounceRect.rect.width * Screen.width / 1920 - Screen.width;
        if (exceeded > 0) xAdjust = -exceeded;
        PouncePanel.position = Input.mousePosition + new Vector3(xAdjust, 0, 0);


        float devourChance;
        if (actor.Unit.Predator)
            devourChance = Mathf.Round(100 * target.GetDevourChance(actor, true));
        else
            devourChance = 0;

        CommandData data = new CommandData()
        {
            Actor = actor,
            Target = target,
            Range = range,
            DevourChance = devourChance
        };

        _pounceButtons[currentButton].onClick.AddListener(() => actor.MeleePounce(target));
        _pounceButtons[currentButton].onClick.AddListener(FinishAction);
        int damage = actor.WeaponDamageAgainstTarget(target, false);
        _pounceButtons[currentButton].GetComponentInChildren<Text>().text = $"Melee Pounce {Math.Round(100 * target.GetAttackChance(actor, false, true))}% {(damage >= target.Unit.Health ? "Kill" : $"{damage} dmg")}";
        if (range < 2 || range > 4) _pounceButtons[currentButton].interactable = false;
        currentButton++;
        if (actor.Unit.Predator)
        {
            var voreTypes = State.RaceSettings.GetVoreTypes(actor.Unit.Race);
            if (voreTypes.Contains(VoreType.Oral))
            {
                _pounceButtons[currentButton].onClick.AddListener(() => actor.VorePounce(target));
                _pounceButtons[currentButton].onClick.AddListener(FinishAction);
                if (data.Actor.PredatorComponent.FreeCap() < data.Target.Bulk())
                {
                    _pounceButtons[currentButton].GetComponentInChildren<Text>().text = $"Too bulky to vore";
                    _pounceButtons[currentButton].interactable = false;
                }
                else
                    _pounceButtons[currentButton].GetComponentInChildren<Text>().text = $"Oral Vore Pounce {devourChance}%";

                if (range < 2 || range > 4) _pounceButtons[currentButton].interactable = false;
                currentButton++;
            }

            currentButton = AltVorePounce(data, SpecialAction.BreastVore, currentButton);
            currentButton = AltVorePounce(data, SpecialAction.CockVore, currentButton);
            currentButton = AltVorePounce(data, SpecialAction.AnalVore, currentButton);
            currentButton = AltVorePounce(data, SpecialAction.Unbirth, currentButton);
            currentButton = AltVorePounce(data, SpecialAction.TailVore, currentButton);
        }

        _pounceNeedsRefresh = false;
        ActivatePounceButtons(currentButton);
    }

    private int AltVorePounce(CommandData data, SpecialAction type, int currentButton)
    {
        if (TacticalActionList.TargetedDictionary.TryGetValue(type, out var targetedAction))
        {
            if (targetedAction.AppearConditional(data.Actor) && (targetedAction.RequiresPred == false || data.Actor.Unit.Predator))
            {
                _pounceButtons[currentButton].onClick.AddListener(() => data.Actor.VorePounce(data.Target, type));
                _pounceButtons[currentButton].onClick.AddListener(FinishAction);
                if (data.Actor.PredatorComponent.FreeCap() < data.Target.Bulk())
                {
                    _pounceButtons[currentButton].GetComponentInChildren<Text>().text = $"Too bulky to {targetedAction.Name}";
                    _pounceButtons[currentButton].interactable = false;
                }
                else if (data.Actor.BodySize() < data.Target.BodySize() * 3 && data.Actor.Unit.HasTrait(TraitType.TightNethers) && (type == SpecialAction.CockVore || type == SpecialAction.Unbirth))
                {
                    _pounceButtons[currentButton].GetComponentInChildren<Text>().text = $"Too large to {targetedAction.Name}";
                    _pounceButtons[currentButton].interactable = false;
                }
                else
                    _pounceButtons[currentButton].GetComponentInChildren<Text>().text = $"{targetedAction.Name} Pounce {data.DevourChance}%";

                if (data.Range < 2 || data.Range > 4) _pounceButtons[currentButton].interactable = false;
                currentButton++;
                return currentButton;
            }
        }

        return currentButton;
    }

    private int AddKtCommands(ActorUnit actor, int currentButton, CommandData data)
    {
        if (Config.KuroTenkoEnabled)
        {
            if (Equals(data.Actor.Unit.Side, data.Target.Unit.Side) && data.Actor.Unit != data.Target.Unit)
            {
                if (actor.PredatorComponent.CanFeed())
                {
                    _buttons[currentButton].onClick.AddListener(() => data.Actor.PredatorComponent.Feed(data.Target));
                    _buttons[currentButton].onClick.AddListener(FinishAction);
                    _buttons[currentButton].GetComponentInChildren<Text>().text = $"Breastfeed";
                    currentButton++;
                }

                if (actor.PredatorComponent.CanFeedCum())
                {
                    _buttons[currentButton].onClick.AddListener(() => data.Actor.PredatorComponent.FeedCum(data.Target));
                    _buttons[currentButton].onClick.AddListener(FinishAction);
                    _buttons[currentButton].GetComponentInChildren<Text>().text = $"Feed Cum";
                    currentButton++;
                }

                if (actor.PredatorComponent.CanTransfer() && data.Target.Unit.Predator)
                {
                    _buttons[currentButton].onClick.AddListener(() => data.Actor.PredatorComponent.TransferAttempt(data.Target));
                    _buttons[currentButton].onClick.AddListener(FinishAction);
                    _buttons[currentButton].GetComponentInChildren<Text>().text = $"Transfer";
                    if (data.Target.PredatorComponent.FreeCap() < actor.PredatorComponent.GetTransferBulk())
                    {
                        _buttons[currentButton].GetComponentInChildren<Text>().text = $"Too bulky to Transfer";
                        _buttons[currentButton].interactable = false;
                    }

                    currentButton++;
                }
            }
            else if (data.Actor.Unit != data.Target.Unit && data.Target.Unit.Predator)
            {
                if (actor.PredatorComponent.CanVoreSteal(data.Target))
                {
                    _buttons[currentButton].onClick.AddListener(() => data.Actor.PredatorComponent.VoreStealAttempt(data.Target));
                    _buttons[currentButton].onClick.AddListener(FinishAction);
                    _buttons[currentButton].GetComponentInChildren<Text>().text = $"Vore Steal {data.DevourChance}%";
                    currentButton++;
                }
            }

            if (actor.PredatorComponent.CanSuckle() && actor.PredatorComponent.GetSuckle(data.Target)[0] + actor.PredatorComponent.GetSuckle(data.Target)[1] != 0)
            {
                _buttons[currentButton].onClick.AddListener(() => data.Actor.PredatorComponent.Suckle(data.Target));
                _buttons[currentButton].onClick.AddListener(FinishAction);
                _buttons[currentButton].GetComponentInChildren<Text>().text = $"Suckle";
                currentButton++;
            }
        }

        return currentButton;
    }

    private void ActivateButtons(int currentButton)
    {
        for (int i = 0; i < currentButton; i++)
        {
            _buttons[i].gameObject.SetActive(true);
        }

        _rect.sizeDelta = new Vector2(_rect.sizeDelta.x, currentButton * 40);
    }

    private void ActivatePounceButtons(int currentButton)
    {
        for (int i = 0; i < currentButton; i++)
        {
            _pounceButtons[i].gameObject.SetActive(true);
        }

        _pounceRect.sizeDelta = new Vector2(_pounceRect.sizeDelta.x, currentButton * 40);
    }

    private void FinishAction()
    {
        State.GameManager.TacticalMode.ActionDone();
        gameObject.SetActive(false);
        PouncePanel.gameObject.SetActive(false);
    }

    private void FinishMoveAction()
    {
        gameObject.SetActive(false);
        PouncePanel.gameObject.SetActive(false);
    }

    private void QueueCloseLoop()
    {
        if (_activeWait == false)
        {
            _activeWait = true;
            CloseSecond();
        }
    }

    private void CloseSecond()
    {
        if (_activeWait == false) return;
        Vector2 localMousePosition = _pounceRect.InverseTransformPoint(Input.mousePosition);
        if (_pounceRect.rect.Contains(localMousePosition))
        {
            MiscUtilities.DelayedInvoke(CloseSecond, .25f);
        }
        else
        {
            PouncePanel.gameObject.SetActive(false);
            _activeWait = false;
        }
    }
}