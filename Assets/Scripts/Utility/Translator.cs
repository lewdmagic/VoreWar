using UnityEngine;


public class Translator
{
    private bool _playerMove;
    private Vec2I _startPos;
    private Vec2I _endPos;
    private float _remainingTime;
    private float _totalTime;
    private Transform _transform;

    public bool IsActive { get; private set; }

    public void UpdateLocation()
    {
        if (IsActive)
        {
            if (_transform == null)
            {
                IsActive = false;
                return;
            }

            _remainingTime -= Time.deltaTime;
            if (_remainingTime <= 0)
            {
                IsActive = false;
                _transform.position = new Vector2(_endPos.X, _endPos.Y);
                return;
            }

            float t = (_totalTime - _remainingTime) / _totalTime;
            float newX = Mathf.Lerp(_startPos.X, _endPos.X, t);
            float newY = Mathf.Lerp(_startPos.Y, _endPos.Y, t);
            _transform.position = new Vector2(newX, newY);
            if (State.GameManager.CurrentScene == State.GameManager.TacticalMode)
            {
                if (State.GameManager.TacticalMode.IsPlayerInControl == false) State.GameManager.CameraCall(_transform.position);
            }
            else
            {
                if (State.GameManager.StrategyMode.IsPlayerTurn == false) State.GameManager.CameraCall(_transform.position);
            }
        }
    }

    public void SetTranslator(Transform trans, Vec2I start, Vec2I end, float aiMoveRate, bool playerMove)
    {
        if (trans == null) return;
        if (IsActive)
        {
            _transform.position = new Vector2(_endPos.X, _endPos.Y);
        }

        _playerMove = playerMove;
        _totalTime = Mathf.Min(aiMoveRate);
        _transform = trans;
        _startPos = start;
        _endPos = end;
        IsActive = true;
        _remainingTime = _totalTime;
    }

    internal void ClearTranslator()
    {
        IsActive = false;
    }
}