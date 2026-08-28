using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDMenuView : BaseMonoBehaviour, IStartInit
{
    [SerializeField] private RectTransform _menuCanvas;
    [SerializeField] private Button _btnCloseMenu;
    [SerializeField] private List<BtnMenuInfor> _btnMenuList = new();
    [SerializeField] private BtnMenuInfor _currentMenuSelected;

    public bool IsInteract { get; set; } = true;

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        _btnCloseMenu.onClick.AddListener(CloseMenuCanvas);
        _btnMenuList.ForEach(btn => btn.Button.onClick.AddListener(() => SelectButtonMenu(btn)));
    }

    private void CloseMenuCanvas()
    {
        IsInteract = false;
        _menuCanvas.DOAnchorPosX(-960, 0.2f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                _menuCanvas.gameObject.SetActive(false);
                ResetValue();
                IsInteract = true;
            });
    }

    private void SelectButtonMenu(BtnMenuInfor btnMenuInfor)
    {
        if(_currentMenuSelected == null)
        {
            _currentMenuSelected = btnMenuInfor;
            HandleTweenSelectMenu(_currentMenuSelected, -100);
            return;
        }

        if(_currentMenuSelected != btnMenuInfor)
        {
            HandleTweenSelectMenu(_currentMenuSelected, 0);
            HandleTweenSelectMenu(btnMenuInfor, -100)
                .OnComplete(() => _currentMenuSelected = btnMenuInfor);
        }
        else
        {
            _currentMenuSelected.MenuCanvas.gameObject.SetActive(true);
        }
    }

    private Tween HandleTweenSelectMenu(BtnMenuInfor btnMenuInfor, float offsetX)
    {
        return DOTween.To(
            () => btnMenuInfor.MenuRect.offsetMax,//Giá trị tween dùng
            value => btnMenuInfor.MenuRect.offsetMax = value,//Chạy mỗi frame sẽ có value, gán nó vào offset
            new Vector2(offsetX, btnMenuInfor.MenuRect.offsetMax.y),//Data mong muốn
            0.1f)
            .SetEase(Ease.InOutQuad);
    }

    private void ResetValue()
    {
        if(_currentMenuSelected != null)
        {
            HandleTweenSelectMenu(_currentMenuSelected, 0f);
            _currentMenuSelected = null;
        }
    }
}
