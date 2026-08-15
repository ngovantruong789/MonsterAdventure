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
        for (int i=0; i < _items.Count; i++)
        {
            AddItem(new ItemModel
            {
                Id = _items[i].Id,
                Name = _items[i].Name,
                EffectItem = _items[i].EffectItem,
                Quantity = 1,
                Image = _items[i].Image,
                Description = _items[i].Description,
                BuyPrice = _items[i].BuyPrice,
                Value = _items[i].Value,
                SellPrice = _items[i].SellPirce,
            });
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

        if (isItemExist != true) itemModels.Add(item);
    }
}