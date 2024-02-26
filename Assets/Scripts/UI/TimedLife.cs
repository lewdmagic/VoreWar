using UnityEngine;

public class TimedLife : MonoBehaviour
{
    public float Life;
    private float _currentLife;

    private void Update()
    {
        _currentLife += Time.deltaTime;
        if (_currentLife > Life) Destroy(gameObject);
    }
}