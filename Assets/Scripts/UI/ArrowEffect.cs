using System;
using UnityEngine;

public class ArrowEffect : MonoBehaviour
{
    public GameObject Arrow;

    internal Vector2 StartLocation;
    internal Vector2 EndLocation;
    internal float TotalTime;
    internal float CurrentTime;

    internal float ExtraTime;

    private Action _playHitSound;
    private Action _createHitEffect;

    public void Setup(Vec2I startLocation, Vec2I endLocation, ActorUnit target)
    {
        GeneralSetup(startLocation, endLocation);

        _playHitSound = () => State.GameManager.SoundManager.PlayArrowHit(target);
        _createHitEffect = () => State.GameManager.TacticalMode.CreateBloodHitEffect(EndLocation);
    }

    public void Setup(Vec2I startLocation, Vec2I endLocation, ActorUnit target, Action hitSound, Action hitEffect)
    {
        GeneralSetup(startLocation, endLocation);

        _playHitSound = hitSound;
        _createHitEffect = hitEffect;
    }

    private void GeneralSetup(Vec2I startLocation, Vec2I endLocation)
    {
        StartLocation = startLocation;
        EndLocation = endLocation;
        Arrow.transform.position = StartLocation;

        CurrentTime = 0;
        TotalTime = startLocation.GetNumberOfMovesDistance(endLocation) * 0.05f;

        float angle = 90 + (float)(Math.Atan2(startLocation.Y - endLocation.Y, startLocation.X - endLocation.X) * 180 / Math.PI);
        Arrow.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }


    private void Update()
    {
        if (State.GameManager.TacticalMode.PausedText.activeSelf) return;
        if (State.GameManager.CurrentScene != State.GameManager.TacticalMode)
        {
            Destroy(gameObject);
            return;
        }

        CurrentTime += Time.deltaTime;
        Arrow.transform.position = Vector2.Lerp(StartLocation, EndLocation, CurrentTime / TotalTime);
        if (CurrentTime > TotalTime)
        {
            _playHitSound?.Invoke();
            _createHitEffect?.Invoke();
        }

        if (CurrentTime > TotalTime + ExtraTime)
        {
            Destroy(gameObject);
        }
    }
}