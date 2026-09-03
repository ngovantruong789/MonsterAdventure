public static class MapFactory
{
    public static MapModel Create(MapSO mapSO)
    {
        MapModel mapModel = new MapModel();

        mapModel.MapType = mapSO.MapType;
        for (int i = 0; i < mapSO.MapConfigs.Count; i++)
        {
            MonsterMapModel monsterMapModel = new MonsterMapModel();
            monsterMapModel.MonsterSO = mapSO.MapConfigs[i].MonsterSO;
            monsterMapModel.LevelOriginRange = mapSO.MapConfigs[i].LevelOriginRange;
            monsterMapModel.SpawnRate = mapSO.MapConfigs[i].SpawnRate;

            mapModel.MonsterMaps.Add(monsterMapModel);
        }

        mapModel.UIBattleMapModel = new UIBattleMapModel
        {
            SpriteBtnBattleHud = mapSO.UIBattleMapConfig.SpriteBtnBattleHud,
            TextBtnBattleHudModel = CreateUIBattleTextModel(mapSO.UIBattleMapConfig.TextBtnBattleHudSetting),

            SpriteNotification = mapSO.UIBattleMapConfig.SpriteNotification,
            TextNotificationModel = CreateUIBattleTextModel(mapSO.UIBattleMapConfig.NotificationTextSetting),

            SpriteHudMonsterBattle = mapSO.UIBattleMapConfig.SpriteHudMonsterBattle,
            HudMonsterBattleHealthTextModel = CreateUIBattleTextModel(mapSO.UIBattleMapConfig.HudMonsterBattleHealthTextSetting),
            HudMonsterBattleHealthValueTextModel = CreateUIBattleTextModel(mapSO.UIBattleMapConfig.HudMonsterBattleHealthValueTextSetting),
            HudMonsterBattleLevelTextModel = CreateUIBattleTextModel(mapSO.UIBattleMapConfig.HudMonsterBattleLevelTextSetting),
            HudMonsterBattleNameTextModel = CreateUIBattleTextModel(mapSO.UIBattleMapConfig.HudMonsterBattleNameTextSetting),

            ImgBg = mapSO.UIBattleMapConfig.ImgBg,
        };

        return mapModel;
    }

    public static UIMapBattleViewData ConvertUIBattleMapModelToUIMapBattleViewData(UIBattleMapModel data)
    {
        return new UIMapBattleViewData
        {
            SpriteBtnBattleHud = data.SpriteBtnBattleHud,
            TextBtnBattleHud = CreateUIBattleTextViewData(data.TextBtnBattleHudModel),

            SpriteNotification = data.SpriteNotification,
            TextNotification = CreateUIBattleTextViewData(data.TextNotificationModel),

            SpriteHudMonsterBattle = data.SpriteHudMonsterBattle,
            HudMonsterBattleHealthTextViewData = CreateUIBattleTextViewData(data.HudMonsterBattleHealthTextModel),
            HudMonsterBattleNameTextViewData = CreateUIBattleTextViewData(data.HudMonsterBattleNameTextModel),
            HudMonsterBattleHealthValueTextViewData = CreateUIBattleTextViewData(data.HudMonsterBattleHealthValueTextModel),
            HudMonsterBattleLevelTextViewData = CreateUIBattleTextViewData(data.HudMonsterBattleLevelTextModel),

            ImgBg = data.ImgBg,
        };
    }

    private static UIBattleTextModel CreateUIBattleTextModel(UIBattleTextSetting uIBattleTextSetting)
    {
        return new UIBattleTextModel
        {
            HudSize = uIBattleTextSetting.HudSize,
            Color = uIBattleTextSetting.Color,
            FontSize = uIBattleTextSetting.FontSize,
        };
    }

    private static UIBattleTextViewData CreateUIBattleTextViewData(UIBattleTextModel uIBattleTextModel)
    {
        return new UIBattleTextViewData
        {
            HudSize = uIBattleTextModel.HudSize,
            Color = uIBattleTextModel.Color,
            FontSize = uIBattleTextModel.FontSize,
        };
    }
}