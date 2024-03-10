using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    internal bool SoundEnabled = true;

    private readonly int _sourceCount = 9;
    private AudioSource[] _efxSources;
    private int _sourceIndex = 0;

    private readonly float _lowPitchRange = 0.92f;
    private readonly float _highPitchRange = 1.08f;

    private float _voreVolume = 1f;
    private float _combatVolume = 1f;
    private float _passiveVoreSoundVolume = 1f;

    private float _sfxFloor = 0.7f;
    private float _loopFloor = 0f;

    private AudioClip[] _swings;
    private AudioClip[] _arrowHits;
    private AudioClip[] _meleeHits;
    private AudioClip[] _armorHits;
    private AudioClip[] _burps;
    private AudioClip[] _farts;

    private readonly Dictionary<PreyLocation, AudioClip[]> _swallows = new Dictionary<PreyLocation, AudioClip[]>();
    private readonly Dictionary<PreyLocation, AudioClip[]> _failedSwallows = new Dictionary<PreyLocation, AudioClip[]>();
    private readonly Dictionary<PreyLocation, AudioClip[]> _digests = new Dictionary<PreyLocation, AudioClip[]>();
    private readonly Dictionary<PreyLocation, AudioClip[]> _digestLoops = new Dictionary<PreyLocation, AudioClip[]>();
    private readonly Dictionary<PreyLocation, AudioClip[]> _absorbs = new Dictionary<PreyLocation, AudioClip[]>();
    private readonly Dictionary<PreyLocation, AudioClip[]> _absorbLoops = new Dictionary<PreyLocation, AudioClip[]>();

    private readonly Dictionary<string, AudioClip[]> _spellCasts = new Dictionary<string, AudioClip[]>();
    private readonly Dictionary<string, AudioClip[]> _spellHits = new Dictionary<string, AudioClip[]>();

    private readonly Dictionary<string, AudioClip[]> _miscSounds = new Dictionary<string, AudioClip[]>();

    internal void PlaySwing(ActorUnit actor) => RandomizeSfx(_swings, actor, _combatVolume);
    internal void PlayArrowHit(ActorUnit actor) => RandomizeSfx(_arrowHits, actor, _combatVolume);
    internal void PlayMeleeHit(ActorUnit actor) => RandomizeSfx(_meleeHits, actor, _combatVolume);
    internal void PlayArmorHit(ActorUnit actor) => RandomizeSfx(_armorHits, actor, _combatVolume);

    internal void PlayBurp(ActorUnit actor) => RandomizeSfx(_burps, actor, _voreVolume);
    internal void PlayFart(ActorUnit actor) => RandomizeSfx(_farts, actor, _voreVolume);

    internal void PlaySwallow(PreyLocation location, ActorUnit actor) => RandomizeSfx(_swallows[location], actor, _voreVolume);
    internal void PlayFailedSwallow(PreyLocation location, ActorUnit actor) => RandomizeSfx(_failedSwallows[location], actor, _voreVolume);

    internal void PlayDigest(PreyLocation location, ActorUnit actor) => RandomizeSfx(_digests[location], actor, _voreVolume);
    internal void PlayDigestLoop(PreyLocation location, ActorUnit actor) => RandomizeLoop(_digestLoops[location], actor, _passiveVoreSoundVolume);

    internal void PlayAbsorb(PreyLocation location, ActorUnit actor) => RandomizeSfx(_absorbs[location], actor, _voreVolume);
    internal void PlayAbsorbLoop(PreyLocation location, ActorUnit actor) => RandomizeLoop(_absorbLoops[location], actor, _passiveVoreSoundVolume);

    internal void PlaySpellCast(Spell spell, ActorUnit actor) => RandomizeSfx(_spellCasts[spell.Id], actor, _combatVolume);

    // todo locate the spell correctly
    internal void PlaySpellHit(Spell spell, Vector2 location) => RandomizeSfxGlobal(_spellHits[spell.Id], location, _combatVolume);

    private void PopulateVoreClips(Dictionary<PreyLocation, AudioClip[]> dict, string name)
    {
        char sep = Path.DirectorySeparatorChar;
        dict[PreyLocation.Stomach] = Resources.LoadAll<AudioClip>($"audio{sep}vore{sep}{name}{sep}oral");
        dict[PreyLocation.Stomach2] = dict[PreyLocation.Stomach];
        dict[PreyLocation.Anal] = dict[PreyLocation.Stomach];
        dict[PreyLocation.Tail] = dict[PreyLocation.Stomach];
        dict[PreyLocation.Balls] = Resources.LoadAll<AudioClip>($"audio{sep}vore{sep}{name}{sep}cock");
        dict[PreyLocation.Breasts] = Resources.LoadAll<AudioClip>($"audio{sep}vore{sep}{name}{sep}breast");
        dict[PreyLocation.LeftBreast] = dict[PreyLocation.Breasts];
        dict[PreyLocation.RightBreast] = dict[PreyLocation.Breasts];
        dict[PreyLocation.Womb] = Resources.LoadAll<AudioClip>($"audio{sep}vore{sep}{name}{sep}unbirth");
    }

    private void PopulateClips(ref AudioClip[] array, string name)
    {
        char sep = Path.DirectorySeparatorChar;
        array = Resources.LoadAll<AudioClip>($"audio{sep}{name}");
    }

    private void InitSources()
    {
        _efxSources = new AudioSource[_sourceCount];

        for (int x = 0; x < _sourceCount; x++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.pitch = (_highPitchRange - _lowPitchRange) * x / _sourceCount + _lowPitchRange;
            _efxSources[x] = source;
        }
    }

    public void SetVolume(float combatVolume, float voreVolume, float passiveVoreSoundVolume)
    {
        this._voreVolume = voreVolume;
        this._combatVolume = combatVolume;
        this._passiveVoreSoundVolume = passiveVoreSoundVolume;

        //foreach(var source in efxSources)
        //{
        //    source.volume = volume;
        //}
    }

    private void Awake()
    {
        char sep = Path.DirectorySeparatorChar;

        PopulateClips(ref _swings, $"combat{sep}swings");
        PopulateClips(ref _arrowHits, $"combat{sep}arrow-hits");
        PopulateClips(ref _meleeHits, $"combat{sep}melee-hits");
        PopulateClips(ref _armorHits, $"combat{sep}armor-hits");

        PopulateClips(ref _burps, $"vore{sep}burps");
        PopulateClips(ref _farts, $"vore{sep}farts");

        PopulateVoreClips(_swallows, "swallow");
        PopulateVoreClips(_failedSwallows, "fail");
        PopulateVoreClips(_digests, "digest");
        PopulateVoreClips(_digestLoops, "digest-loop");
        PopulateVoreClips(_absorbs, "absorb");
        PopulateVoreClips(_absorbLoops, "absorb-loop");

        foreach (var spell in SpellList.SpellDict.Values)
        {
            string id = spell.Id;

            _spellCasts[id] = Resources.LoadAll<AudioClip>($"audio{sep}spell{sep}{id}{sep}cast");
            _spellHits[id] = Resources.LoadAll<AudioClip>($"audio{sep}spell{sep}{id}{sep}hit");
        }

        _miscSounds["unbound"] = Resources.LoadAll<AudioClip>($"audio{sep}spell{sep}unbound");

        InitSources();
    }

    private void PlayLoop(AudioClip clip, AudioSource source)
    {
        // Debug.Log(clip);
        source.clip = clip;
        source.Play();
    }

    private void PlaySfx(AudioClip clip, AudioSource source, float volume)
    {
        source.PlayOneShot(clip, volume); //Allows sounds to play over each other without using multiple Audio Sources
    }

    private float PositionSound(AudioSource source, Vector2 position, float minVolume)
    {
        Camera camera = State.GameManager.Camera;

        float deltaX = camera.transform.position.x - position.x;
        float deltaY = camera.transform.position.y - position.y;
        float height = camera.orthographicSize / 4;

        float distance = Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY + height * height);

        float volume = 1.2f / (deltaX * deltaX + deltaY * deltaY + height * height * height * height);

        // todo: do volume properly oops
        // I don't like this formula very much

        float pan = -Mathf.Atan(deltaX / distance / 5) / Mathf.PI * 2;

        source.panStereo = pan;

        // since we might need to pass the volume along, we just return it

        return Mathf.Clamp(volume, minVolume, 1f);
    }

    // Plays a sound that isn't attached to a unit
    // If the location is null, the sound has no pan or volume adjustment
    // Otherwise, it sounds like it came from that location on the tactical map

    private void RandomizeSfxGlobal(AudioClip[] clips, Vector2? location, float volume)
    {
        if (SoundEnabled == false) return;
        if (clips == null || clips.Length == 0) return;
        if (State.GameManager.TacticalMode.TacticalSoundBlocked()) return;

        AudioSource source = _efxSources[_sourceIndex];

        _sourceIndex = (_sourceIndex + 1) % _sourceCount;

        AudioClip clip = clips[Random.Range(0, clips.Length)];

        if (location != null)
        {
            volume *= PositionSound(source, (Vector2)location, 0.7f);
        }
        else
        {
            source.panStereo = 0;
        }

        PlaySfx(clip, source, volume);
    }

    private void RandomizeLoop(AudioClip[] clips, ActorUnit actor, float volume)
    {
        if (SoundEnabled == false) return;
        if (clips == null || clips.Length == 0) return;
        if (actor == null)
        {
            return;
        }

        if (State.GameManager.TacticalMode.TacticalSoundBlocked()) return;

        AudioSource source = actor.UnitSprite.LoopSource;

        volume *= PositionSound(source, actor.UnitSprite.transform.position, _loopFloor);

        source.volume = volume;

        // Don't interrupt an existing source

        if (source.isPlaying) return;

        source.pitch = Random.Range(_lowPitchRange, _highPitchRange);

        AudioClip clip = clips[Random.Range(0, clips.Length)];

        PlayLoop(clip, source);
    }

    private void RandomizeSfx(AudioClip[] clips, ActorUnit actor, float volume)
    {
        if (SoundEnabled == false) return;
        if (clips == null || clips.Length == 0) return;

        if (actor == null || actor.UnitSprite == null)
        {
            RandomizeSfxGlobal(clips, null, volume);
            return;
        }

        if (State.GameManager.TacticalMode.TacticalSoundBlocked()) return;

        AudioSource source = actor.UnitSprite.SfxSources[Random.Range(0, actor.UnitSprite.SfxSourcesCount)];

        AudioClip clip = clips[Random.Range(0, clips.Length)];

        volume *= PositionSound(source, actor.UnitSprite.transform.position, _sfxFloor);

        if (actor.UnitSprite.isActiveAndEnabled) //Keeps a Unity warning from popping up
            PlaySfx(clip, source, volume);
    }

    internal void PlayMisc(string name, ActorUnit actor)
    {
        var volume = _combatVolume;

        AudioClip[] clips = _miscSounds[name];

        if (SoundEnabled == false) return;
        if (clips == null || clips.Length == 0) return;

        if (actor == null || actor.UnitSprite == null)
        {
            RandomizeSfxGlobal(clips, null, volume);
            return;
        }

        if (State.GameManager.TacticalMode.TacticalSoundBlocked()) return;

        AudioSource source = actor.UnitSprite.SfxSources[Random.Range(0, actor.UnitSprite.SfxSourcesCount)];

        AudioClip clip = clips[Random.Range(0, clips.Length)];

        volume *= PositionSound(source, actor.UnitSprite.transform.position, _sfxFloor);

        if (actor.UnitSprite.isActiveAndEnabled) //Keeps a Unity warning from popping up
            PlaySfx(clip, source, volume);
    }
}