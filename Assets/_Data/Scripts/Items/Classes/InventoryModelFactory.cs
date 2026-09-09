using System.Collections.Generic;

public static class InventoryModelFactory
{
    public static PlayerInventoryModel ConvertPlayerInventoryDataToPlayerInventoryModel(InventoryDataModel inventoryDataModel, ItemDatabaseSO database)
    {
        return new PlayerInventoryModel
        {
            RestoreInventory = new InventoryModel
            {
                Items = ConvertInventoryDataToInventoryModel(inventoryDataModel.RestoreItems, database.Restores),
            },
            CaptureInventory = new InventoryModel
            {
                Items = ConvertInventoryDataToInventoryModel(inventoryDataModel.CaptureItems, database.Captures),
            }
        };
    }

    private static List<ItemModel> ConvertInventoryDataToInventoryModel(List<ItemDataModel> itemsData, ItemSO[] database)
    {
        List<ItemModel> itemModels = new();
        foreach(ItemDataModel itemData in itemsData)
        {
            ItemSO itemSO = GetItemSOFromItemDataModel(itemData, database);
            if (itemSO == null) continue;

            itemModels.Add(ItemModelFactory.Create(itemSO, itemData.Quantity));
        }

        return itemModels;
    }

    private static ItemSO GetItemSOFromItemDataModel(ItemDataModel itemDataModel, ItemSO[] database)
    {
        foreach (ItemSO itemSO in database)
        {
            if (itemDataModel.ItemID == itemSO.Id && itemDataModel.ItemType == (int)itemSO.ItemType)
            {
                return itemSO;
            }
        }
        return null;
    }
}