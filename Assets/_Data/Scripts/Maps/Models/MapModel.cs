using System;
using System.Collections.Generic;
using UnityEngine;

public class MapModel
{
    public EMapType MapType { get; set; }
    public List<MonsterMapModel> MonsterMaps { get; set; } = new();
    public UIBattleMapModel UIBattleMapModel { get; set; }
}

public class MonsterMapModel
{
    public MonsterSO MonsterSO { get; set; }
    public Vector2 LevelOriginRange { get; set; }
    public Vector2 SpawnRate { get; set; }
}

public class UIBattleMapModel
{
    //Hud button
    public Sprite ImgBg { get; set; }
    public Sprite SpriteBtnBattleHud { get; set; }
    public UIBattleTextModel TextBtnBattleHudModel { get; set; }

    //Notification
    public Sprite SpriteNotification { get; set; }
    public UIBattleTextModel TextNotificationModel { get; set; }

    //Hud battle monster
    public Sprite SpriteHudMonsterBattle {  get; set; }
    public UIBattleTextModel HudMonsterBattleNameTextModel { get; set; }
    public UIBattleTextModel HudMonsterBattleHealthTextModel { get; set; }
    public UIBattleTextModel HudMonsterBattleHealthValueTextModel { get; set; }
    public UIBattleTextModel HudMonsterBattleLevelTextModel { get; set; }
}

public class BaseUIBattleMapModel
{
    public Vector4 HudSize { get; set; }
}

public class UIBattleTextModel : BaseUIBattleMapModel
{
    public Color Color { get; set; }
    public float FontSize { get; set; }
}