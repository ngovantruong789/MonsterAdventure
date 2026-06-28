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
         _inputManager.MovePressedEvent += OnKeyboardMove;    
    }

    private void OnKeyboardMove(Vector2 keyboardDirection)
    {
        if (keyboardDirection == Vector2.zero)
        {
            _point.anchoredPosition = Vector2.zero;
            directionTouchPos = Vector2.zero;
            this.MoveEvent?.Invoke(Vector2.zero, 0f);
        }
        else
        {
            Vector2 normalizedDir = keyboardDirection.normalized;
            _point.anchoredPosition = normalizedDir * _radius;
            directionTouchPos = normalizedDir * _radius;
            this.MoveEvent?.Invoke(directionTouchPos, 1f);
        }
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
        HandleTransformPoint(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startTouchPos = eventData.position;
        PlayTweenClick(0.9f, 0.1f);
    }

    private void HandleTransformPoint(Vector2 tounchPos)
    {
        directionTouchPos = tounchPos - startTouchPos;//Tính hướng chạm  từ điểm đầu tới cuối
        Vector2 newPointPos = Vector2.ClampMagnitude(directionTouchPos, _radius);//Tính nó vào bán kính, cao quá thì = r
        _point.anchoredPosition = newPointPos;

        float speedIntensity = Mathf.Clamp01(newPointPos.magnitude / _radius);
        this.MoveEvent?.Invoke(directionTouchPos, speedIntensity);
    }

    private void PlayTweenClick(float scale, float duration)
    {
        _container.DOScale(scale, duration);
    }
}
