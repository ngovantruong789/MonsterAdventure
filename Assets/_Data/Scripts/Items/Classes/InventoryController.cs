using System.Collections.Generic;

public class InventoryController
{
    private InventoryModel _inventoryModel;
    public InventoryModel InventoryModel => _inventoryModel;

    private BattleInventoryModel _battleInventoryModel;
    public BattleInventoryModel BattleInventoryModel => _battleInventoryModel;

    private ItemDatabaseSO _itemDatabaseSO;
    

    public InventoryController(InventoryModel inventoryModel, ItemDatabaseSO itemDatabaseSO)
    {
        _inventoryModel = inventoryModel;
        _itemDatabaseSO = itemDatabaseSO;
        AddItem(new ItemModel
        {
            Id = 2301,
            Name = "Red Candy",
            EffectItem = EItemEffect.RestoreHp,
            Quantity = 1,
        });
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
        if(itemModels.Count == 0)
        {
            itemModels.Add(item);
            return;
        }

        foreach(ItemModel itemModel in itemModels)
        {
            if(item.Id == itemModel.Id)
            {
                itemModel.Quantity++;
                break;
            }
        }

    }
}