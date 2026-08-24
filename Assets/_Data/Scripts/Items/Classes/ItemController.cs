using System.Collections.Generic;
using UnityEngine;

public partial class ItemController : IItemController
{
    private IInventoryProvider _inventoryProvider;

    public ItemController(IInventoryProvider inventoryProvider)
    {
        _inventoryProvider = inventoryProvider;
    }

    public void UseItem(int id, EItemType itemType, MonsterModel opponentMonster, MonsterModel player)
    {
        List<ItemModel> items = itemType == EItemType.Capture ? _inventoryProvider.CaptureInventoryModel.Items : 
            _inventoryProvider.RestoreInventoryModel.Items;
        if (!CheckQuantityItem(id, items)) return;

        ItemModel itemModel = GetItemModel(id, items);
        itemModel.Quantity -= 1;
        if(itemType == EItemType.Capture)
        {
            ActiveCapture(id, itemType, itemModel, opponentMonster);
        }
        else
        {
            ActiveRestore(id, itemModel, player);
        }
    }

    private bool CheckQuantityItem(int id, List<ItemModel> items)
    {
        if(items == null || items.Count == 0) return false;

        foreach(ItemModel item in items)
        {
            if(item.Id != id) continue;
            if (item.Quantity <= 0) return false;

            return true;
        }
        return false;
    }

    private ItemModel GetItemModel(int id, List<ItemModel> items)
    {
        foreach (ItemModel item in items)
        {
            if (item.Id != id) continue;
            return item;
        }

        return null;
    }

    private void ActiveCapture(int id, EItemType itemType, ItemModel itemCapture, MonsterModel opponentMonster)
    {
        int percentRateCapture = (int)(itemCapture.Value * 3.5 / (opponentMonster.DifficultCapture * 0.3f));
        percentRateCapture = Mathf.Clamp(percentRateCapture, 1, 100);
        int rand = Random.Range(1, 101);
        bool isCaptureComplete = rand <= percentRateCapture;
        Debug.Log("ItemCapture: " + itemCapture.Value + "; DifficultMonster: " + opponentMonster.DifficultCapture 
            + "; Rate: " + percentRateCapture + "; IsComplete: " +  isCaptureComplete);
        _onActiveItem.OnNext(new ActiveItemControllerEventData(id, itemCapture.Prefab, itemType, isCaptureComplete));
    }
    private void ActiveRestore(int id, ItemModel itemModel, MonsterModel player)
    {
        if (itemModel.EffectItem == EItemEffect.RestoreHp && player != null)
        {
            player.Health += (int)itemModel.Value;
            if (player.Health > player.MaxHealth)
            {
                player.Health = player.MaxHealth;
            }
        }
        _onActiveItem.OnNext(new ActiveItemControllerEventData (id, itemModel.Prefab, itemModel.ItemType, true));
    }
}