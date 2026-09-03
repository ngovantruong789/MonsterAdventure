using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMapBattleView : BaseMonoBehaviour, IStartInit
{
    [SerializeField] private List<BattleHudButtonInfor> _battleHudButtonInfors = new();
    [SerializeField] private List<BattleButtonSkillInfor> _battleButtonSkillInfors = new();
    [SerializeField] private MonsterBattleInforUI _playerBattleInforUI;
    [SerializeField] private MonsterBattleInforUI _opponentBattleInforUI;
    [SerializeField] private TextMeshProUGUI _notificationText;
    [SerializeField] private Image _imgNotification;
    [SerializeField] private Image _imgBg;

    private UIMapBattleViewData _uIMapBattleViewData;

    public void SetData(UIMapBattleViewData uIMapBattleViewData)
    {
        _uIMapBattleViewData = uIMapBattleViewData;
    }

    public void Initialize()
    {
        
    }

    public void UpdateUIBattleWithMap()
    {
        //Button hud
        _battleHudButtonInfors.ForEach(infor =>
        {
            UpdateHUDButton(infor.Button);
            UpdateHUDText(infor.BtnText, _uIMapBattleViewData.TextBtnBattleHud);
        });

        //Button skill
        _battleButtonSkillInfors.ForEach(infor =>
        {
            UpdateHUDButton(infor.BtnSkill);
            UpdateHUDText(infor.SkillNameText, _uIMapBattleViewData.TextBtnBattleHud);
        });

        //Notification
        _imgNotification.sprite = _uIMapBattleViewData.SpriteNotification;
        UpdateHUDText(_notificationText, _uIMapBattleViewData.TextNotification);

        //Hud battle monster
        _playerBattleInforUI.ImgBg.sprite = _uIMapBattleViewData.SpriteHudMonsterBattle;
        UpdateHUDText(_playerBattleInforUI.HealthValueText, _uIMapBattleViewData.HudMonsterBattleHealthValueTextViewData);
        UpdateHUDText(_playerBattleInforUI.LevelText, _uIMapBattleViewData.HudMonsterBattleLevelTextViewData);
        UpdateHUDText(_playerBattleInforUI.HealthText, _uIMapBattleViewData.HudMonsterBattleHealthTextViewData);
        UpdateHUDText(_playerBattleInforUI.MonsterNameText, _uIMapBattleViewData.HudMonsterBattleNameTextViewData);

        _opponentBattleInforUI.ImgBg.sprite = _uIMapBattleViewData.SpriteHudMonsterBattle;
        UpdateHUDText(_opponentBattleInforUI.HealthValueText, _uIMapBattleViewData.HudMonsterBattleHealthValueTextViewData);
        UpdateHUDText(_opponentBattleInforUI.LevelText, _uIMapBattleViewData.HudMonsterBattleLevelTextViewData);
        UpdateHUDText(_opponentBattleInforUI.HealthText, _uIMapBattleViewData.HudMonsterBattleHealthTextViewData);
        UpdateHUDText(_opponentBattleInforUI.MonsterNameText, _uIMapBattleViewData.HudMonsterBattleNameTextViewData);

        //Img bg
        _imgBg.sprite = _uIMapBattleViewData.ImgBg;
    }

    private void UpdateHUDButton(Button btn)
    {
        btn.image.sprite = _uIMapBattleViewData.SpriteBtnBattleHud;
    }

    private void UpdateHUDText(TextMeshProUGUI btnText, UIBattleTextViewData viewData)
    {
        btnText.color = viewData.Color;
        btnText.fontSize = viewData.FontSize;
        btnText.rectTransform.anchoredPosition = new Vector2(viewData.HudSize.x, viewData.HudSize.y);
        btnText.rectTransform.sizeDelta = new Vector2(viewData.HudSize.z, viewData.HudSize.w);
    }
}