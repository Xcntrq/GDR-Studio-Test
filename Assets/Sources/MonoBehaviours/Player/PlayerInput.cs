using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(LineRenderer))]
public class PlayerInput : MonoBehaviour, ITargetPositionGenerator, IPlayerInput
{
    private LineRenderer _lineRenderer;
    private Camera _camera;

    public event Action<Vector2> OnTargetPositionGenerated;

    public void Disable()
    {
        enabled = false;
    }

    private void Awake()
    {
        _camera = Camera.main;

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, Vector3.zero);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsOverGameObject())
        {
            Vector2 newTargetPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            OnTargetPositionGenerated?.Invoke(newTargetPosition);

            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, newTargetPosition);
        }
    }

    private bool IsOverGameObject()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return true;

        foreach (Touch touch in Input.touches)
        {
            bool isTouchOverGameObject = (touch.phase == TouchPhase.Began) && EventSystem.current.IsPointerOverGameObject(touch.fingerId);

            if (isTouchOverGameObject)
                return true;
        }

        return false;
    }
}
