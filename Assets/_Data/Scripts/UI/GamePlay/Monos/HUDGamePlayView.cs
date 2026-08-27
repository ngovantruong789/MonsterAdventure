using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HUDGamePlayView : BaseMonoBehaviour, IStartInit
{
    [Header("Menu")]
    [SerializeField] private Button _btnOpenMenu;
    [SerializeField] private RectTransform _menuCanvas;

    [Header("Inventory")]

    [Header("Team")]

    [Header("Move")]

    public bool IsInteract { get; set; } = true;

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        _btnOpenMenu.onClick.AddListener(OpenMenuCanvas);
    }

    private void OpenMenuCanvas()
    {
        IsInteract = false;
        _menuCanvas.gameObject.SetActive(true);
        _menuCanvas.DOAnchorPosX(0, 0.2f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => IsInteract = true);
    }
}