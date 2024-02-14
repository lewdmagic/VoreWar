using UnityEngine;

public class SkullDisplay : MonoBehaviour
{
    private Vector3 _velocity = new Vector3(0.0f, 0.04f, 0.0f);
    private SpriteRenderer _spriteR;

    private double _alpha = 1;

    // Use this for initialization
    private void Start()
    {
        _spriteR = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += _velocity * Time.fixedDeltaTime;
        _spriteR.color = new Color(1f, 1f, 1f, (float)_alpha);
        _alpha -= 0.02;
        if (_alpha < 0) Destroy(gameObject);
    }
}