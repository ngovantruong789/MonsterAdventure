using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterMapConfig
{
    [SerializeField] private MonsterSO _monsterSO;
    public MonsterSO MonsterSO => _monsterSO;

    [SerializeField] private Vector2 _levelOriginRange;
    public Vector2 LevelOriginRange => _levelOriginRange;

    [SerializeField] private Vector2 _spawnRate;
    public Vector2 SpawnRate => _spawnRate;
}