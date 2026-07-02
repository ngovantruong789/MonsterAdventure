using UnityEngine;

public class MapManager : LifetimeScope
{
    [SerializeField] private EMapType _mapType;
    public EMapType MapType => _mapType;
}
