using UnityEngine;

public class MapManager : BaseMonoBehaviour, IMapManager, IStartInit
{
    [SerializeField] private MapSO _mapSO;
    [SerializeField] private EMapType _mapType;
    public EMapType MapType => _mapType;

    private MapModel mapModel;
    public MapModel MapModel => mapModel;

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        mapModel = MapFactory.Create(_mapSO);
        FindAnyObjectByType<BattleManager>().SetMapManager(this);
    }
}