﻿using UnityEngine;


public class Translator
{
    bool playerMove;
    Vec2i startPos;
    Vec2i endPos;
    float remainingTime;
    float totalTime;
    Transform transform;

    public bool IsActive { get; private set; }

    public void UpdateLocation()
    {

        if (IsActive)
        {
            if (transform == null)
            {
                IsActive = false;
                return;
            }

            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0)
            {
                IsActive = false;
                transform.position = new Vector2(endPos.X, endPos.Y);
                return;
            }

            float t = (totalTime - remainingTime) / totalTime;
            float newX = Mathf.Lerp(startPos.X, endPos.X, t);
            float newY = Mathf.Lerp(startPos.Y, endPos.Y, t);
            transform.position = new Vector2(newX, newY);
            if (State.GameManager.CurrentScene == State.GameManager.TacticalMode)
            {
                if (State.GameManager.TacticalMode.IsPlayerInControl == false)
                    State.GameManager.CameraCall(transform.position);
            }
            else
            {
                if (State.GameManager.StrategyMode.IsPlayerTurn == false)
                    State.GameManager.CameraCall(transform.position);
            }


        }

    }

    public void SetTranslator(Transform trans, Vec2i start, Vec2i end, float AIMoveRate, bool PlayerMove)
    {
        if (trans == null)
            return;
        if (IsActive)
        {
            transform.position = new Vector2(endPos.X, endPos.Y);
        }
        playerMove = PlayerMove;
        totalTime = Mathf.Min(AIMoveRate);
        transform = trans;
        startPos = start;
        endPos = end;
        IsActive = true;
        remainingTime = totalTime;
    }

    internal void ClearTranslator()
    {
        IsActive = false;
    }

}

