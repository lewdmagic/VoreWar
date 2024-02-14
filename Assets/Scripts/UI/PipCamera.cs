using UnityEngine;

public class PipCamera : MonoBehaviour
{
    private Camera _cam;

    private void Start()
    {
        _cam = GetComponent<Camera>();
    }

    internal void SetLocation(int x, int y, int zoom)
    {
        _cam.transform.position = new Vector3(x, y, _cam.transform.position.z);
        _cam.orthographicSize = zoom;
    }

    internal void SetLocation(Vector3 position, int zoom)
    {
        _cam.transform.position = new Vector3(position.x, position.y, _cam.transform.position.z);
        _cam.orthographicSize = zoom;
    }
}