using System.Collections.Generic;
using UnityEngine;

public partial class ItemController : IItemController
{
    private IPlayerInventoryProvider _inventoryProvider;

    public ItemController(IPlayerInventoryProvider inventoryProvider)
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
        float percentRateCapture = (float)(itemCapture.Value * 2.7f / (opponentMonster.DifficultCapture * 0.45f));
        float effectPercentBonnus = CheckEffectMonster(opponentMonster) ? 0.5f : 0f;
        percentRateCapture += percentRateCapture * (GetLowHpPercent(opponentMonster) + effectPercentBonnus);
        percentRateCapture = Mathf.Clamp(percentRateCapture, 0, 101);

        float random = Random.Range(0f, 100f);
        bool isCaptureComplete = random <= percentRateCapture;
        Debug.Log("ItemCapture: " + itemCapture.Value + "; DifficultMonster: " + opponentMonster.DifficultCapture
            + "; Rate: " + percentRateCapture + "; Rand: " + random + "; IsComplete: " + isCaptureComplete);
        _onActiveItem.OnNext(new ActiveItemControllerEventData(id, itemCapture.Prefab, itemType, isCaptureComplete));
    }

    private float GetLowHpPercent(MonsterModel opponentMonster)
    {
        float currentHealth = (float)opponentMonster.Health / opponentMonster.MaxHealth;
        if (currentHealth <= 0.2f) return 1;
        else if (currentHealth <= 0.5f) return 0.5f;
        else return 0;
    }

    private bool CheckEffectMonster(MonsterModel opponentMonster)
    {
        return opponentMonster.EffectType == EEffectType.Paralyze
            || opponentMonster.EffectType == EEffectType.Freeze
            || opponentMonster.EffectType == EEffectType.Burn
            || opponentMonster.EffectType == EEffectType.SpeedDown;
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