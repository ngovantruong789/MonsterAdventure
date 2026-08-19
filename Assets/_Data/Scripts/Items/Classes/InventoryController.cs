using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class InventoryController : IInventoryProvider, IStartable
{
    private InventoryModel _inventoryModel;
    public RestoreInventoryModel RestoreInventoryModel => _inventoryModel.RestoreInventory;
    public CaptureInventoryModel CaptureInventoryModel => _inventoryModel.CaptureInventory;

    private IReadOnlyList<ItemSO> _items;

    [Inject] private ItemDatabaseSO _itemDatabaseSO;
    
    public InventoryController(InventoryModel inventoryModel, IReadOnlyList<ItemSO> items)
    {
        _items = items;
        _inventoryModel = inventoryModel;
        //_itemDatabaseSO = itemDatabaseSO;
        Debug.Log("InventoryController Initialized");
    }

    public void Start()
    {
        foreach (ItemSO item in _items)
        {
            AddItem(ItemModelFactory.Create(item, 1));
        }
    }

    public void AddItem(ItemModel item)
    {
        if (item.EffectItem == EItemEffect.RestoreHp ||
           item.EffectItem == EItemEffect.BurnHeal ||
           item.EffectItem == EItemEffect.FreezeHeal ||
           item.EffectItem == EItemEffect.BuffHealth ||
           item.EffectItem == EItemEffect.BuffDefense ||
           item.EffectItem == EItemEffect.BuffAttack ||
           item.EffectItem == EItemEffect.BuffSpeed ||
           item.EffectItem == EItemEffect.Awakening ||
           item.EffectItem == EItemEffect.ParalyzeHeal )
        {
            OnAddItem(_inventoryModel.RestoreInventory.Items, item);
        }
        else if (item.EffectItem == EItemEffect.Capture)
        {
            OnAddItem(_inventoryModel.CaptureInventory.Items, item);
        }
        else if (item.EffectItem == EItemEffect.Lucky)
        {
            OnAddItem(_inventoryModel.PlayerEquipment.Items, item);
        }
        /*else //Chưa có Item cho monster
        {
           OnAddItem(_inventoryModel.MonsterEquipment.Items, item);
        }*/
    }

    private void OnAddItem(List<ItemModel> itemModels, ItemModel item)
    {
        bool isItemExist = false;

        foreach (ItemModel itemModel in itemModels)
        {
            if (item.Id == itemModel.Id)
            {
                itemModel.Quantity += item.Quantity;
                isItemExist = true;
                break;
            }
        }

        if (!isItemExist) itemModels.Add(item);
    }
}