using UnityEngine;

public readonly struct ActiveItemControllerEventData
{
    public int Id { get; }
    public GameObject Prefab { get; }
    public EItemType ItemType { get; }
    public bool IsComplete { get; }

    public ActiveItemControllerEventData(int id, GameObject prefab, EItemType itemType, bool isComplete)
    {
        Id = id;
        Prefab = prefab;
        ItemType = itemType;
        IsComplete = isComplete;
    }
}