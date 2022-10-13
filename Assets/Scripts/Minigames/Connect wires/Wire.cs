using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Wire : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Color customColor;
    public bool IsLeft;
    public bool IsSuccess = false;

    private PlayerInputActions playerInputActions;

    private Image _image;
    private LineRenderer _lineRenderer;
    private Canvas _canvas;
    private bool _isDragStarted = false;

    private WireTask _wireTask;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        _image = GetComponent<Image>();
        _lineRenderer = GetComponent<LineRenderer>();
        _canvas = GetComponentInParent<Canvas>();
        _wireTask = GetComponentInParent<WireTask>();
    }

    private void Update()
    {
        if (_isDragStarted)
        {
            Vector2 movePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                playerInputActions.UI.MousePosition.ReadValue<Vector2>(),
                _canvas.worldCamera,
                out movePos);

            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _canvas.transform.TransformPoint(movePos));
        }
        else if (!IsSuccess)
        {
            _lineRenderer.SetPosition(0, Vector2.zero);
            _lineRenderer.SetPosition(1, Vector2.zero);
        }

        bool isHovered = RectTransformUtility.RectangleContainsScreenPoint(
            transform as RectTransform,
            playerInputActions.UI.MousePosition.ReadValue<Vector2>(),
            _canvas.worldCamera);

        if (isHovered)
        {
            _wireTask.currentHoveredWire = this;
        }
    }

    public void SetColor(Color color)
    {
        _image.color = color;
        _lineRenderer.startColor = color;
        _lineRenderer.endColor = color;
        customColor = color;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // unused
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsLeft && !IsSuccess)
        {
            _isDragStarted = true;
            _wireTask.currentDraggedWire = this;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_wireTask.currentHoveredWire != null)
        {
            if (_wireTask.currentHoveredWire.customColor == customColor && !_wireTask.currentHoveredWire.IsLeft)
            {
                IsSuccess = true;

                _wireTask.currentHoveredWire.IsSuccess = true;
            }
        }

        _isDragStarted = false;
        _wireTask.currentDraggedWire = null;
    }
}