using UnityEngine;

public class UIMapBattleViewData
{
    //Hud button
    public Sprite ImgBg { get; set; }
    public Sprite SpriteBtnBattleHud { get; set; }
    public UIBattleTextViewData TextBtnBattleHud { get; set; }

    //Notification
    public Sprite SpriteNotification { get; set; }
    public UIBattleTextViewData TextNotification { get; set; }

    //Hud battle monster
    public Sprite SpriteHudMonsterBattle { get; set; }
    public UIBattleTextViewData HudMonsterBattleNameTextViewData { get; set; }
    public UIBattleTextViewData HudMonsterBattleHealthTextViewData { get; set; }
    public UIBattleTextViewData HudMonsterBattleHealthValueTextViewData { get; set; }
    public UIBattleTextViewData HudMonsterBattleLevelTextViewData { get; set; }
}

public class BaseUIBattleMapViewData
{
    public Vector4 HudSize { get; set; }
}

public class UIBattleTextViewData : BaseUIBattleMapViewData
{
    public Color Color { get; set; }
    public float FontSize { get; set; }
}