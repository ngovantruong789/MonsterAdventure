using UnityEngine;

public readonly struct ActiveItemControllerEventData
{
    public GameObject Prefab { get; }
    public EItemType ItemType { get; }
    public bool IsComplete { get; }

    public ActiveItemControllerEventData(GameObject prefab, EItemType itemType, bool isComplete)
    {
        Prefab = prefab;
        ItemType = itemType;
        IsComplete = isComplete;
    }
}