using System.Collections.Generic;
using UnityEngine;

public class MapModel
{
    public EMapType MapType { get; set; }
    public List<MonsterMapModel> MonsterMaps { get; set; } = new();
}

public class MonsterMapModel
{
    public MonsterSO MonsterSO { get; set; }
    public Vector2 LevelOriginRange { get; set; }
    public Vector2 SpawnRate { get; set; }
}