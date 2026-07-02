using System;
using UnityEngine;

[Serializable]
public class MonsterMapConfig
{
    [SerializeField] private EMapType _mapType;
    public EMapType MapType => _mapType;

    [SerializeField] private Vector2 _levelOriginRange;
    public Vector2 LevelOriginRange => _levelOriginRange;
}
