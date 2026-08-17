using UnityEngine;
using VContainer.Unity;

public class MonsterEntity : CharacterEntity, IStartable
{
    [SerializeField] private MapManager _mapManager;
    [SerializeField] private MonsterSO _monsterSO;

    private MonsterModel _currentModel;
    public MonsterModel CurrentMonsterModel => _currentModel;

    public void Start()
    {
        //Vector2 originLevelRange = GetOriginLevelRange(_monsterSO.Map);
        _currentModel = MonsterModelFactory.Create(_monsterSO, 20);
    }

    private Vector2 GetOriginLevelRange(MonsterMapConfig[] monsterMapConfigs)
    {
        foreach (MonsterMapConfig monsterMapConfig in monsterMapConfigs)
        {
            /*if (monsterMapConfig.MapType == _mapManager.MapType)
            {
                return monsterMapConfig.LevelOriginRange;
            }*/
        }
        return Vector2.zero;
    }
}