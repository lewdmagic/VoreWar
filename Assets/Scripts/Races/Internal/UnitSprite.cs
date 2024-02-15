#region

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class UnitSprite : MonoBehaviour
{
    public TextMeshProUGUI HitPercentage;
    public TextMeshProUGUI DamageIndicator;
    public TextMeshProUGUI LevelText;

    public Slider HealthBar;
    public RectTransform HealthBarOrange;

    public SpriteRenderer FlexibleSquare;

    public AudioSource[] SfxSources;
    public AudioSource LoopSource;

    public Transform GraphicsFolder;
    public Transform OtherFolder;
    public readonly float PitchMax = 1.08f;
    public readonly float PitchMin = 0.92f;

    public readonly int SfxSourcesCount = 5;

    private Animator _animator;
    private Animator _ballsAnimator;
    internal bool BlueColored;
    private Animator _boobsAnimator;

    private int _lastHealth;

    private float _remainingTimeForDamage;
    private Animator _secondBoobsAnimator;
    private Color _startingColor;
    private float _startingTimeForDamage = .75f;

    private float _timeUntilHealthBarReset = -1;

    internal RaceRenderer RaceRenderer { get; set; }

    public void Awake()
    {
        SfxSources = new AudioSource[5];

        for (var i = 0; i < SfxSourcesCount; i++)
        {
            SfxSources[i] = gameObject.AddComponent<AudioSource>();
            SfxSources[i].pitch = PitchMin + (PitchMax - PitchMin) / SfxSourcesCount * i;
        }

        LoopSource = gameObject.AddComponent<AudioSource>();
    }

    public void UpdateHealthBar(ActorUnit unit)
    {
        if (State.GameManager.TacticalMode.TurboMode)
        {
            return;
        }

        HealthBarOrange.gameObject.SetActive(false);
        float healthFraction = unit.Unit.HealthPct;
        if (healthFraction > .99f || healthFraction <= 0)
        {
            HealthBar.gameObject.SetActive(false);
        }
        else
        {
            HealthBar.gameObject.SetActive(true);
            HealthBar.value = healthFraction;
        }

        _lastHealth = unit.Unit.Health;
    }

    public void ShowDamagedHealthBar(ActorUnit unit, int damage)
    {
        _timeUntilHealthBarReset = .1f;
        HealthBarOrange.gameObject.SetActive(true);
        float orangeLevel = 0.5f + (Mathf.Abs(Time.time % 1 - .5f) - 0.25f);
        HealthBarOrange.GetComponent<Image>().color = new Color(1, orangeLevel, 0);
        float baseHealthFraction = unit.Unit.HealthPct;
        float newhealthFraction = (float)(unit.Unit.Health - damage) / unit.Unit.MaxHealth;
        HealthBarOrange.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 150 * baseHealthFraction);
        HealthBar.gameObject.SetActive(true);
        HealthBar.value = newhealthFraction;
    }

    public void HitPercentagesDisplayed(bool displayed)
    {
        HitPercentage.gameObject.SetActive(displayed);
    }

    public void DisplayHitPercentage(float chance, Color color, int damage = 0)
    {
        HitPercentagesDisplayed(true);
        HitPercentage.text = Mathf.Round(chance * 100) + "%";
        if (damage > 0)
        {
            HitPercentage.text += "\n-" + damage;
        }

        HitPercentage.faceColor = color;
    }

    public void DisplayResist()
    {
        DamageIndicator.faceColor = Color.red;
        DamageIndicator.text = "Resist";
        FinishDisplayedTextSetup();
    }

    public void DisplayCharm()
    {
        DamageIndicator.faceColor = Color.magenta;
        DamageIndicator.text = "Charmed!";
        FinishDisplayedTextSetup();
    }

    public void DisplayHypno()
    {
        DamageIndicator.faceColor = Color.green;
        DamageIndicator.text = "Hypnotized!";
        FinishDisplayedTextSetup();
    }

    public void DisplayDazzle()
    {
        DamageIndicator.faceColor = Color.red;
        DamageIndicator.text = "Dazzled!";
        FinishDisplayedTextSetup();
    }

    public void DisplaySummoned()
    {
        DamageIndicator.faceColor = Color.white;
        DamageIndicator.text = "Summoned!";
        FinishDisplayedTextSetup();
    }

    public void DisplaySeduce()
    {
        DamageIndicator.faceColor = Color.magenta;
        DamageIndicator.text = "Seduced!";
        FinishDisplayedTextSetup();
    }

    public void DisplayDistract()
    {
        DamageIndicator.faceColor = Color.magenta;
        DamageIndicator.text = "Distracted!";
        FinishDisplayedTextSetup();
    }

    public void DisplayEscape()
    {
        DamageIndicator.faceColor = Color.blue;
        DamageIndicator.text = "Escaped";
        FinishDisplayedTextSetup();
    }

    public void DisplayCrit()
    {
        DamageIndicator.faceColor = Color.red;
        DamageIndicator.text = "CRITICAL!";
        FinishDisplayedTextSetup();
    }

    public void DisplayGraze()
    {
        DamageIndicator.faceColor = Color.green;
        DamageIndicator.text = "Graze";
        FinishDisplayedTextSetup();
    }

    public void DisplayDamage(int damage, bool spellDamage = false, bool expGain = false)
    {
        if (State.GameManager.TacticalMode.TurboMode)
        {
            return;
        }

        if (Config.DamageNumbers == false && damage != 0)
        {
            return;
        }

        if (damage > 0)
        {
            if (expGain)
            {
                DamageIndicator.faceColor = Color.yellow;
                DamageIndicator.text = $"+{damage}";
            }
            else
            {
                DamageIndicator.faceColor = Color.red;
                DamageIndicator.text = $"-{damage}";
            }
        }
        else if (damage < 0)
        {
            DamageIndicator.faceColor = Color.blue;
            DamageIndicator.text = $"+{Mathf.Abs(damage)}";
        }
        else
        {
            DamageIndicator.faceColor = Color.red;
            if (spellDamage)
            {
                DamageIndicator.text = "No Effect";
            }
            else
            {
                DamageIndicator.text = "Miss";
            }
        }

        FinishDisplayedTextSetup();
    }

    private void FinishDisplayedTextSetup()
    {
        _remainingTimeForDamage = _startingTimeForDamage;
        _startingColor = DamageIndicator.faceColor;
        DamageIndicator.gameObject.SetActive(true);
    }

    private void UpdateDisplayDamage()
    {
        _remainingTimeForDamage -= Time.deltaTime;
        if (_remainingTimeForDamage < 0)
        {
            DamageIndicator.gameObject.SetActive(false);
        }
        else
        {
            DamageIndicator.faceColor = new Color(_startingColor.r, _startingColor.g, _startingColor.b, 1.5f * _remainingTimeForDamage / _startingTimeForDamage);
            DamageIndicator.outlineColor = new Color(0, 0, 0, 1.5f * _remainingTimeForDamage / _startingTimeForDamage);
        }
    }

    public void UpdateSprites(ActorUnit actor, bool activeTurn)
    {
        if (State.GameManager.TacticalMode.TurboMode)
        {
            return;
        }

        Vector3 goalScale = new Vector3(1, 1, 1);

        goalScale *= actor.Unit.GetScale();

        if (_lastHealth != actor.Unit.Health)
        {
            UpdateHealthBar(actor);
        }


        if (goalScale.x > GraphicsFolder.localScale.x)
        {
            float newScale = Mathf.Min(GraphicsFolder.localScale.x + .03f, goalScale.x);
            GraphicsFolder.localScale = new Vector3(newScale, newScale, newScale);
        }
        else if (goalScale.x < GraphicsFolder.localScale.x)
        {
            float newScale = Mathf.Max(GraphicsFolder.localScale.x - .03f, goalScale.x);
            GraphicsFolder.localScale = new Vector3(newScale, newScale, newScale);
        }

        if (_timeUntilHealthBarReset > 0)
        {
            _timeUntilHealthBarReset -= Time.deltaTime;
            if (_timeUntilHealthBarReset <= 0)
            {
                UpdateHealthBar(actor);
            }
        }

        UpdateFlexibleSquare(actor, activeTurn);

        if (_remainingTimeForDamage >= 0)
        {
            UpdateDisplayDamage();
        }

        if (RaceRenderer == null)
        {
            CreateCompleteSprite(actor);
        }

        //CompleteSprite.SetActor(actor);
        RaceRenderer.UpdateSprite();
        UpdateLevelText(actor);
        ApplyTinting(actor);
    }

    private void ApplyTinting(ActorUnit actor)
    {
        if (actor.Visible && actor.Targetable == false)
        {
            ApplyDeadEffect();
        }
        else if (actor.Surrendered || actor.DamagedColors)
        {
            float tint = .4f;
            if (actor.DamagedColors)
            {
                tint = .8f;
            }

            RaceRenderer.RedifySprite(tint);
        }
        else if (actor.Unit.GetStatusEffect(StatusEffectType.Petrify) != null)
        {
            RaceRenderer.DarkenSprites();
        }
    }

    private void UpdateLevelText(ActorUnit actor)
    {
        LevelText.gameObject.SetActive(Config.ShowLevelText);
        if (Config.ShowLevelText)
        {
            LevelText.text = $"Level: {actor.Unit.Level}";
        }
    }

    private void CreateCompleteSprite(ActorUnit actor)
    {
        if (Config.AnimatedBellies)
        {
            RaceRenderer = new RaceRenderer(State.GameManager.SpriteRendererPrefab, State.GameManager.SpriteRenderAnimatedPrefab, GraphicsFolder, actor);
            _animator = RaceRenderer.GetSpriteOfType(SpriteType.Belly)?.GameObject.GetComponentInParent<Animator>();
            if (_animator != null)
            {
                var raceData = RaceFuncs.GetRace(actor.Unit);
                if (raceData.SetupOutput.GentleAnimation)
                {
                    _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/ActorsGentle");
                }
                else
                {
                    _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Actors");
                }

                _animator.enabled = true;
            }

            _ballsAnimator = RaceRenderer.GetSpriteOfType(SpriteType.Balls)?.GameObject.GetComponentInParent<Animator>();
            if (_ballsAnimator != null)
            {
                var raceData = RaceFuncs.GetRace(actor.Unit);
                if (raceData.SetupOutput.GentleAnimation)
                {
                    _ballsAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/ActorsGentle");
                }
                else
                {
                    _ballsAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Actors");
                }

                _ballsAnimator.enabled = true;
            }

            _boobsAnimator = RaceRenderer.GetSpriteOfType(SpriteType.Breasts)?.GameObject.GetComponentInParent<Animator>();
            if (_boobsAnimator != null)
            {
                var raceData = RaceFuncs.GetRace(actor.Unit);
                if (raceData.SetupOutput.GentleAnimation)
                {
                    _boobsAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/ActorsGentle");
                }
                else
                {
                    _boobsAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Actors");
                }

                _boobsAnimator.enabled = true;
            }

            _secondBoobsAnimator = RaceRenderer.GetSpriteOfType(SpriteType.SecondaryBreasts)?.GameObject.GetComponentInParent<Animator>();
            if (_secondBoobsAnimator != null)
            {
                var raceData = RaceFuncs.GetRace(actor.Unit);
                if (raceData.SetupOutput.GentleAnimation)
                {
                    _secondBoobsAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/ActorsGentle");
                }
                else
                {
                    _secondBoobsAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Actors");
                }

                _secondBoobsAnimator.enabled = true;
            }
        }
        else
        {
            RaceRenderer = new RaceRenderer(State.GameManager.SpriteRendererPrefab, null, GraphicsFolder, actor);
        }
    }

    private void UpdateFlexibleSquare(ActorUnit actor, bool activeTurn)
    {
        float alpha = 1;
        if (Config.AllianceSquaresDarkness == 2)
        {
            alpha = activeTurn && actor.Movement > 0 ? .9f : .5f;
        }
        else if (Config.AllianceSquaresDarkness == 1)
        {
            alpha = activeTurn && actor.Movement > 0 ? .5f : .2f;
        }
        else
        {
            alpha = 0;
        }

        if (!Equals(actor.Unit.FixedSide, actor.Unit.Side) && TacticalUtilities.PlayerCanSeeTrueSide(actor.Unit))
        {
            if (BlueColored)
            {
                {
                    if (Config.AllianceSquaresDarkness == 3)
                    {
                        if (activeTurn && actor.Movement > 0)
                        {
                            FlexibleSquare.color = new Color(1f, 0.6f, 0, 1);
                        }
                        else
                        {
                            FlexibleSquare.color = new Color(0.75f, 0.40f, 0, 1);
                        }
                    }
                    else
                    {
                        FlexibleSquare.color = new Color(0.8f, 0.5f, 0f, alpha);
                    }
                }
            }
            else
            {
                if (Config.AllianceSquaresDarkness == 3)
                {
                    if (activeTurn && actor.Movement > 0)
                    {
                        FlexibleSquare.color = new Color(0.65f, 0, 0.9f, 1);
                    }
                    else
                    {
                        FlexibleSquare.color = new Color(0.35f, 0, 0.40f, 1);
                    }
                }
                else
                {
                    FlexibleSquare.color = new Color(0.45f, 0f, 0.75f, alpha);
                }
            }
        }
        else if (BlueColored)
        {
            if (Config.AltFriendlyColor)
            {
                if (Config.AllianceSquaresDarkness == 3)
                {
                    if (activeTurn && actor.Movement > 0)
                    {
                        FlexibleSquare.color = new Color(0, 1, 0, 1);
                    }
                    else
                    {
                        FlexibleSquare.color = new Color(0, 0.4f, 0, 1);
                    }
                }
                else
                {
                    FlexibleSquare.color = new Color(0f, 0.8f, 0, alpha);
                }
            }
            else
            {
                if (Config.AllianceSquaresDarkness == 3)
                {
                    if (activeTurn && actor.Movement > 0)
                    {
                        FlexibleSquare.color = new Color(0, 0, 1, 1);
                    }
                    else
                    {
                        FlexibleSquare.color = new Color(0, 0, 0.4f, 1);
                    }
                }
                else
                {
                    FlexibleSquare.color = new Color(0f, 0f, 0.8f, alpha);
                }
            }
        }
        else
        {
            if (Config.AllianceSquaresDarkness == 3)
            {
                if (activeTurn && actor.Movement > 0)
                {
                    FlexibleSquare.color = new Color(1, 0, 0, 1);
                }
                else
                {
                    FlexibleSquare.color = new Color(.4f, 0, 0, 1);
                }
            }
            else
            {
                FlexibleSquare.color = new Color(0.8f, 0f, 0, alpha);
            }
        }
    }

    public void AnimateBalls(float odds)
    {
        if (_ballsAnimator == null)
        {
            return;
        }

        if (Random.value > odds)
        {
            return;
        }

        if (!_ballsAnimator.GetCurrentAnimatorStateInfo(0).IsName("none"))
        {
            return;
        }

        int ran = Random.Range(0, 3); // 0 up to 3
        if (ran == 0)
        {
            _ballsAnimator.SetTrigger("wriggle");
        }

        if (ran == 1)
        {
            _ballsAnimator.SetTrigger("wriggle2");
        }

        if (ran == 2)
        {
            _ballsAnimator.SetTrigger("wriggle3");
        }
    }

    public void AnimateBelly(float odds)
    {
        if (_animator == null)
        {
            return;
        }

        if (Random.value > odds)
        {
            return;
        }

        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("none"))
        {
            return;
        }

        int ran = Random.Range(0, 4); // 0 up to 3
        if (ran == 1)
        {
            _animator.SetTrigger("wriggle");
        }

        if (ran == 2)
        {
            _animator.SetTrigger("wriggle2");
        }

        if (ran == 3)
        {
            _animator.SetTrigger("wriggle3");
        }
    }

    public void AnimateBoobs(float odds)
    {
        if (_boobsAnimator == null)
        {
            return;
        }

        if (Random.value > odds)
        {
            return;
        }

        if (!_boobsAnimator.GetCurrentAnimatorStateInfo(0).IsName("none"))
        {
            return;
        }

        int ran = Random.Range(0, 3); // 0 up to 3
        if (ran == 1)
        {
            _boobsAnimator.SetTrigger("wriggle");
        }

        if (ran == 2)
        {
            _boobsAnimator.SetTrigger("wriggle2");
        }

        if (ran == 3)
        {
            _boobsAnimator.SetTrigger("wriggle3");
        }
    }

    public void AnimateSecondBoobs(float odds)
    {
        if (_secondBoobsAnimator == null)
        {
            return;
        }

        if (Random.value > odds)
        {
            return;
        }

        if (!_secondBoobsAnimator.GetCurrentAnimatorStateInfo(0).IsName("none"))
        {
            return;
        }

        int ran = Random.Range(0, 3); // 0 up to 3
        if (ran == 1)
        {
            _secondBoobsAnimator.SetTrigger("wriggle");
        }

        if (ran == 2)
        {
            _secondBoobsAnimator.SetTrigger("wriggle2");
        }

        if (ran == 3)
        {
            _secondBoobsAnimator.SetTrigger("wriggle3");
        }
    }

    public void AnimateBellyEnter()
    {
        if (_animator == null)
        {
            return;
        }

        _animator.SetTrigger("enter");
    }

    private void ApplyDeadEffect()
    {
        LevelText.gameObject.SetActive(false);
        FlexibleSquare.gameObject.SetActive(false);
        HealthBar.gameObject.SetActive(false);
        RaceRenderer.ApplyDeadEffect();
    }
}