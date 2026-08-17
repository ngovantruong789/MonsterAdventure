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

        return mapModel;
    }
}
