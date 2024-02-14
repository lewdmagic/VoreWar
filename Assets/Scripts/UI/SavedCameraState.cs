using OdinSerializer;
using UnityEngine;


internal class SavedCameraState
{
    [OdinSerialize]
    private Vector2 _strategicPosition;
    internal Vector2 StrategicPosition { get => _strategicPosition; set => _strategicPosition = value; }
    [OdinSerialize]
    private float _strategicZoom;
    internal float StrategicZoom { get => _strategicZoom; set => _strategicZoom = value; }
    [OdinSerialize]
    private Vector2 _tacticalPosition;
    internal Vector2 TacticalPosition { get => _tacticalPosition; set => _tacticalPosition = value; }
    [OdinSerialize]
    private float _tacticalZoom;
    internal float TacticalZoom { get => _tacticalZoom; set => _tacticalZoom = value; }
}

