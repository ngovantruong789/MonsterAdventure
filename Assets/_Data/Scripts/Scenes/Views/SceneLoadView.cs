using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoadView : LifetimeScope, IStartInit
{
    [Header("Scene Load Images")]
    [SerializeField] private RectTransform _sceneLoadContainer;
    [SerializeField] private float _stripeHeight = 80f;
    [SerializeField] private bool _isOpen;
    private List<ScripeData> _stripes = new List<ScripeData>();

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        SpawnLoadSceneImages(_isOpen);
    }

    public void ToggleOpenCloseLoadScene(Action onComplete = null)
    {
        if (_isOpen)
        {
            CloseLoadScene(onComplete);
        }
        else
        {
            OpenLoadScene(onComplete);
        }
    }

    public void OpenLoadScene(Action onComplete = null)
    {
        _isOpen = true;
        MoveStripes(onComplete);
    }

    public void CloseLoadScene(Action onComplete = null)
    {
        _isOpen = false;
        MoveStripes(onComplete);
    }

    private void SpawnLoadSceneImages(bool isOpen)
    {
        float screenW = _sceneLoadContainer.rect.width;
        float screenH = _sceneLoadContainer.rect.height;

        int count = Mathf.CeilToInt(screenH / _stripeHeight) + 1;
        float bottom = -screenH / 2f;

        for (int i = 0; i < count; i++)
        {
            GameObject obj = new GameObject($"Stripe_{i}", typeof(RectTransform), typeof(Image));
            obj.transform.SetParent(_sceneLoadContainer, false);

            RectTransform rect = obj.GetComponent<RectTransform>();
            Image image = obj.GetComponent<Image>();

            image.sprite = null;
            image.type = Image.Type.Sliced;
            image.color = Color.black;

            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);

            rect.sizeDelta = new Vector2(screenW, _stripeHeight);

            float y = bottom + _stripeHeight * 0.5f + i * _stripeHeight;
            float startX = i % 2 == 0 ? screenW + 100 : -screenW - 100;

            if (isOpen)
            {
                rect.anchoredPosition = new Vector2(startX, y);
            }
            else
            {
                rect.anchoredPosition = new Vector2(0, y);
            }
            
            _stripes.Add(new ScripeData
            {
                RectTransform = rect,
                openX = startX,
                closeX = 0,
            });
        }
    }

    private void MoveStripes(Action onComplete = null)
    {
        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < _stripes.Count; i++)
        {
            float posX = _isOpen ? _stripes[i].openX : _stripes[i].closeX;
            seq.Join(
                _stripes[i].RectTransform
                    .DOAnchorPosX(posX, 1f)
                    .SetEase(Ease.Linear)
            );
        }

        seq.OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }
}

public class ScripeData
{
    public RectTransform RectTransform;
    public float openX;
    public float closeX;
}