using System;
using UnityEngine;

[Serializable]
public class UIBattleMapConfig
{
    [Header("Image background")]
    [SerializeField] private Sprite _imgBg;
    public Sprite ImgBg => _imgBg;

    [Header("Button HUD")]
    [SerializeField] private Sprite _spriteBtnBattleHud;
    public Sprite SpriteBtnBattleHud => _spriteBtnBattleHud;

    [SerializeField] private UIBattleTextSetting _textBtnBattleHudSetting;
    public UIBattleTextSetting TextBtnBattleHudSetting => _textBtnBattleHudSetting;

    [Header("Notification")]
    [SerializeField] private Sprite _spriteNotification;
    public Sprite SpriteNotification => _spriteNotification;

    [SerializeField] private UIBattleTextSetting _notificationTextSetting;
    public UIBattleTextSetting NotificationTextSetting => _notificationTextSetting;

    [Header("HUD monster battle")]
    [SerializeField] private Sprite _spriteHudMonsterBattle;
    public Sprite SpriteHudMonsterBattle => _spriteHudMonsterBattle;

    [SerializeField] private UIBattleTextSetting _hudMonsterBattleNameTextSetting;
    public UIBattleTextSetting HudMonsterBattleNameTextSetting => _hudMonsterBattleNameTextSetting;

    [SerializeField] private UIBattleTextSetting _hudMonsterBattleHealthTextSetting;
    public UIBattleTextSetting HudMonsterBattleHealthTextSetting => _hudMonsterBattleHealthTextSetting;

    [SerializeField] private UIBattleTextSetting _hudMonsterBattleHealthValueTextSetting;
    public UIBattleTextSetting HudMonsterBattleHealthValueTextSetting => _hudMonsterBattleHealthValueTextSetting;

    [SerializeField] private UIBattleTextSetting _hudMonsterBattleLevelTextSetting;
    public UIBattleTextSetting HudMonsterBattleLevelTextSetting => _hudMonsterBattleLevelTextSetting;
}

[Serializable]
public class BaseUIBattleMapSetting
{
    [SerializeField] private Vector4 _hudSize;
    public Vector4 HudSize => _hudSize;
}

[Serializable]
public class UIBattleTextSetting : BaseUIBattleMapSetting
{
    [SerializeField] private Color _color;
    public Color Color => _color;

    [SerializeField] private float _fontSize;
    public float FontSize => _fontSize;
}