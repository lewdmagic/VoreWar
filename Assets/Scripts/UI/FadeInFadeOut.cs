using UnityEngine;


public class FadeInFadeOut : MonoBehaviour
{
    public float FadeInTime = .2f;
    public float HoldTime = .2f;
    public float FadeOutTime = .8f;

    public SpriteRenderer SpriteRenderer;

    private bool _fadingIn = true;
    private bool _fadingOut = false;

    private float _currentTime = 0;

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_fadingIn)
        {
            SpriteRenderer.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, _currentTime / FadeInTime));
            if (_currentTime > FadeInTime)
            {
                _fadingIn = false;
                _currentTime = 0;
            }
        }
        else if (_fadingOut)
        {
            SpriteRenderer.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, _currentTime / FadeOutTime));
            if (_currentTime > FadeOutTime)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (_currentTime > HoldTime)
            {
                _fadingOut = true;
                _currentTime = 0;
            }
        }
    }
}