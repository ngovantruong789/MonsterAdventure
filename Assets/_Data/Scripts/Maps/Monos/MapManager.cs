using UnityEngine;

public class MapManager : BaseMonoBehaviour
{
    [SerializeField] private EMapType _mapType;
    public EMapType MapType => _mapType;
}
