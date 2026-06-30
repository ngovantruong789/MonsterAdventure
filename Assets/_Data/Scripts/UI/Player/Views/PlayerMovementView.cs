using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementView : LifetimeScope, IPointerUpHandler, IDragHandler, IPointerDownHandler, IStartInit
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private RectTransform _container;
    [SerializeField] private RectTransform _joystickBg;
    [SerializeField] private RectTransform _point;
    [SerializeField] private float _radius;
    
    private Vector2 startTouchPos;
    private Vector2 directionTouchPos;
    public Action<Vector2, float> MoveEvent { get; set; }

    protected override void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
         _inputManager.MovePressedEvent += HandleMovePointByKey;
    }

    private void ProcessMovement(Vector2 rawOffset)
    {
        directionTouchPos = Vector2.ClampMagnitude(rawOffset, _radius);
        _point.anchoredPosition = directionTouchPos;

        float speedIntensity = Mathf.Clamp01(directionTouchPos.magnitude / _radius);
        this.MoveEvent?.Invoke(directionTouchPos, speedIntensity);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _point.anchoredPosition = Vector2.zero;
        startTouchPos = Vector2.zero;
        PlayTweenClick(1f, 0.1f);
        this.MoveEvent?.Invoke(directionTouchPos, 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        HandleMovePointByJoystick(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startTouchPos = eventData.position;
        PlayTweenClick(0.9f, 0.1f);
    }

    private void HandleMovePointByKey(Vector2 keyboardDirection)
    {
        ProcessMovement(keyboardDirection.normalized * _radius);
    }

    private void HandleMovePointByJoystick(Vector2 touchPos)
    {
        ProcessMovement(touchPos - startTouchPos);
    }

    private void PlayTweenClick(float scale, float duration)
    {
        _container.DOScale(scale, duration);
    }
}
