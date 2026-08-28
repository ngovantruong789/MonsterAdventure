using System.Collections.Generic;

public static class ItemModelFactory
{
    public static ItemModel Create(ItemSO itemSO, int quantity)
    {
        return new ItemModel
        {
            Id = itemSO.Id,
            Name = itemSO.Name,
            Prefab = itemSO.Prefab,
            EffectItem = itemSO.EffectItem,
            ItemType = itemSO.ItemType,
            Quantity = quantity,
            Image = itemSO.Image,
            Description = itemSO.Description,
            BuyPrice = itemSO.BuyPrice,
            Value = itemSO.Value,
            SellPrice = itemSO.SellPirce,
        };
    }

    public static ItemViewData ConvertItemModelToItemViewData(ItemModel itemModel)
    {
        return new ItemViewData
        {
            Id = itemModel.Id,
            Name = itemModel.Name,
            EffectItem = itemModel.EffectItem,
            ItemType = itemModel.ItemType,
            Quantity = itemModel.Quantity,
            Image = itemModel.Image,
            Description = itemModel.Description,
            BuyPrice = itemModel.BuyPrice,
            Value = itemModel.Value,
            SellPrice = itemModel.SellPrice,
        };
    }

    public static List<ItemViewData> ConvertListItemViewModelToItemViewData(List<ItemModel> items)
    {
        List<ItemViewData> itemViewData = new();
        foreach (ItemModel item in items)
        {
            itemViewData.Add(ConvertItemModelToItemViewData(item));
        }
        return itemViewData;
    }
}