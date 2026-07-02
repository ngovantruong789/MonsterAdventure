using UnityEngine;

[CreateAssetMenu(fileName = "MapSO", menuName = "ScripableObjects/Map")]
public class MapSO : ScriptableObject
{
    [SerializeField] private int _id;
    public int Id => _id;

    [SerializeField] private EMapType _mapType;
    public EMapType MapType => _mapType;

    [SerializeField] private string _name;
    public string Name => _name;
}