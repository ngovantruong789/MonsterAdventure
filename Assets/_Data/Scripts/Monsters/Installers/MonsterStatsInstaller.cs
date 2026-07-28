using System;
using UnityEngine;

[Serializable]
public class MonsterStatsInstaller : BaseInstaller, IMonsterModelProvider
{
    [SerializeField] private MapManager _mapManager;
    [SerializeField] private MonsterSO _monsterSO;

    private MonsterModel _currentModel;
    public MonsterModel CurrentMonsterModel => _currentModel;

    public override void Initialize()
    {
        base.Initialize();
        Vector2 originLevelRange = GetOriginLevelRange(_monsterSO.Map);
        //_currentModel = MonsterModelFactory.Create(_monsterSO, (int)UnityEngine.Random.Range(originLevelRange.x, originLevelRange.y));
        _currentModel = MonsterModelFactory.Create(_monsterSO, 27);
        //Debug.Log("Level: " + _currentModel.Level + "; Health: " + _currentModel.Health);
    }

    public MonsterModel CloneCurrentMonsterModel()
    {
        return MonsterModelFactory.Create(_currentModel);
    }

    private Vector2 GetOriginLevelRange(MonsterMapConfig[] monsterMapConfigs)
    {
        foreach(MonsterMapConfig monsterMapConfig in monsterMapConfigs)
        {
            if(monsterMapConfig.MapType == _mapManager.MapType)
            {
                return monsterMapConfig.LevelOriginRange;
            }
        }
        return Vector2.zero;
    }
}
