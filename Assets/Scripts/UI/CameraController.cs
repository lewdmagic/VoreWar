﻿using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector2 _strategicPosition;
    private float _strategicZoom;
    private Camera _cam;

    private Vector2 _lastMousePos;
    public Vector2 ZoomRange;
    public float ScrollSpeed = .2f;
    private float _maxX;
    private float _maxY;

    private void Start()
    {
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (State.GameManager?.CurrentScene == null || _cam == null || State.GameManager.ActiveInput) return;
        if ((State.GameManager.CurrentScene != State.GameManager.StrategyMode && State.GameManager.CurrentScene != State.GameManager.TacticalMode && State.GameManager.CurrentScene != State.GameManager.MapEditor) || State.GameManager.Menu.UIPanel.activeSelf) return;
        if (State.GameManager.CurrentScene == State.GameManager.TacticalMode)
        {
            ZoomRange.y = Mathf.Max(Config.TacticalSizeX * .7f, Config.TacticalSizeY * .7f);
            _maxX = Config.TacticalSizeX;
            _maxY = Config.TacticalSizeY;
        }
        else if (State.GameManager.CurrentScene == State.GameManager.StrategyMode || State.GameManager.CurrentScene == State.GameManager.MapEditor)
        {
            ZoomRange.y = Mathf.Max(Config.StrategicWorldSizeX * .7f, Config.StrategicWorldSizeY * .7f);
            _maxX = Config.StrategicWorldSizeX;
            _maxY = Config.StrategicWorldSizeY;
        }

        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false) //Don't zoom if you're scrolling or otherwise
        {
            _cam.orthographicSize = Mathf.Clamp(_cam.orthographicSize * (1f - Input.GetAxis("Mouse ScrollWheel")), ZoomRange.x, ZoomRange.y);
            if (Input.GetKey(KeyCode.RightAlt))
            {
                if (State.GameManager.CurrentScene == State.GameManager.StrategyMode)
                {
                    _cam.orthographicSize = 8f;
                }
                else
                {
                    _cam.orthographicSize = 4f;
                }
            }
        }

        _cam.orthographicSize = Mathf.Clamp(_cam.orthographicSize * (1f - Input.GetAxis("Camera Zoom")), ZoomRange.x, ZoomRange.y);

        Vector2 currentMousePos = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            // Mouse was clicked this frame
        }
        else if (Input.GetMouseButton(0))
        {
            if (State.GameManager.CurrentScene != State.GameManager.MapEditor || State.GameManager.MapEditor.ActiveAnything == false)
            {
                transform.Translate(_lastMousePos - currentMousePos);
                currentMousePos = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else if (Input.GetMouseButtonDown(2))
        {
            // Mouse was clicked this frame
        }
        else if (Input.GetMouseButton(2))
        {
            transform.Translate(_lastMousePos - currentMousePos);
            currentMousePos = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal != 0 || vertical != 0) transform.Translate(ScrollSpeed * horizontal, ScrollSpeed * vertical, 0);
        }

        _lastMousePos = currentMousePos;

        if (Config.EdgeScrolling)
        {
            int edgeWidth = 10;
            if (Input.mousePosition.x >= Screen.width - edgeWidth)
            {
                transform.Translate(ScrollSpeed, 0, 0);
            }

            if (Input.mousePosition.x <= 0 + edgeWidth)
            {
                transform.Translate(-ScrollSpeed, 0, 0);
            }

            if (Input.mousePosition.y >= Screen.height - edgeWidth)
            {
                transform.Translate(0, ScrollSpeed, 0);
            }

            if (Input.mousePosition.y <= 0 + edgeWidth)
            {
                transform.Translate(0, -ScrollSpeed, 0);
            }
        }

        Vector3 clampedLoc = new Vector3(transform.position.x, transform.position.y, -10f);

        clampedLoc.x = Mathf.Clamp(clampedLoc.x, 0, _maxX);
        clampedLoc.y = Mathf.Clamp(clampedLoc.y, 0, _maxY);
        transform.position = clampedLoc;
    }

    public void SetZoom(float size)
    {
        _cam.orthographicSize = size;
    }

    public void SaveStrategicCamera()
    {
        if (State.World.SavedCameraState == null) State.World.SavedCameraState = new SavedCameraState();
        State.World.SavedCameraState.StrategicPosition = transform.position;
        State.World.SavedCameraState.StrategicZoom = _cam.orthographicSize;
    }

    public void SaveTacticalCamera()
    {
        if (State.World.SavedCameraState == null) State.World.SavedCameraState = new SavedCameraState();
        State.World.SavedCameraState.TacticalPosition = transform.position;
        State.World.SavedCameraState.TacticalZoom = _cam.orthographicSize;
    }

    public void LoadStrategicCamera()
    {
        if (State.World.SavedCameraState == null || State.World.SavedCameraState.StrategicPosition == Vector2.zero) return;
        transform.position = State.World.SavedCameraState.StrategicPosition;
        _cam.orthographicSize = State.World.SavedCameraState.StrategicZoom;
    }

    public void LoadTacticalCamera()
    {
        if (State.World.SavedCameraState == null || State.World.SavedCameraState.TacticalPosition == Vector2.zero) return;
        transform.position = State.World.SavedCameraState.TacticalPosition;
        _cam.orthographicSize = State.World.SavedCameraState.TacticalZoom;
    }
}