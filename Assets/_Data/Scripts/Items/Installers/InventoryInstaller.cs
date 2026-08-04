using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryInstaller : BaseInstaller, IInventoryModelProvider
{
    [SerializeField] private ItemDatabaseSO _itemDatabaseSO;

    private InventoryController _inventoryController;
    public InventoryController InventoryController => _inventoryController;

    public InventoryModel InventoryModel => _inventoryController.InventoryModel;

    public override void Initialize()
    {
        base.Initialize();
        _inventoryController = new InventoryController(new InventoryModel(), _itemDatabaseSO);
    }

    public List<ItemModel> CloneInventoryModel(List<ItemModel> items)
    {
        return InventoryModelFactory.Create(items);
    }
}