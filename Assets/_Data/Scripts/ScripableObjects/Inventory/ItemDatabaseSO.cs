using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabaseSO", menuName = "ScriptableObjects/ItemDatabase")]
public class ItemDatabaseSO : ScriptableObject
{
    [SerializeField] private ItemSO[] _captures;
    public ItemSO[] Captures => _captures;

    [SerializeField] private ItemSO[] _playerEquipments;
    public ItemSO[] PlayerEquipments => _playerEquipments;

    [SerializeField] private ItemSO[] _monsterEquipments;
    public ItemSO[] MonsterEquipments => _monsterEquipments;

    [SerializeField] private ItemSO[] _restores;
    public ItemSO[] Restores => _restores;
}