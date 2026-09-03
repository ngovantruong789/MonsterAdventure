using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapSO", menuName = "ScriptableObjects/Map")]
public class MapSO : ScriptableObject
{
    [SerializeField] private EMapType _mapType;
    public EMapType MapType => _mapType;

    [SerializeField] private List<MonsterMapConfig> _mapConfigs = new();
    public List<MonsterMapConfig> MapConfigs => _mapConfigs;

    [SerializeField] private UIBattleMapConfig _uIBattleMapConfig;
    public UIBattleMapConfig UIBattleMapConfig => _uIBattleMapConfig;
}