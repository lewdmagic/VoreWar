using UnityEngine;

public class AnimationEffectComponent : MonoBehaviour
{
    public float[] FrameTime;
    public Sprite[] Frame;
    public bool Repeat;

    private new SpriteRenderer _renderer;

    private int _currentFrame = 0;
    private float _currentTime = 0;

    private void Start()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (_renderer == null) _renderer = GetComponentInChildren<SpriteRenderer>();
        _currentTime += Time.deltaTime;
        if (_currentTime > FrameTime[_currentFrame])
        {
            if (_currentFrame + 1 > FrameTime.GetUpperBound(0))
            {
                if (Repeat)
                {
                    _currentTime = 0;
                    _currentFrame = 0;
                    _renderer.sprite = Frame[0];
                    return;
                }

                Destroy(gameObject);
                return;
            }
            else
            {
                _currentTime = 0;
                _currentFrame++;
                _renderer.sprite = Frame[_currentFrame];
            }
        }
    }
}