using System.Collections.Generic;

public static class InventoryDataFactory
{
    public static InventoryDataModel Create(IPlayerInventoryProvider inventoryProvider)
    {
        return new InventoryDataModel
        {
            CaptureItems = ConvertInventoryToInventoryDataModel(inventoryProvider.CaptureInventoryModel.Items),
            RestoreItems = ConvertInventoryToInventoryDataModel(inventoryProvider.RestoreInventoryModel.Items),
        };
    }

    private static List<ItemDataModel> ConvertInventoryToInventoryDataModel(List<ItemModel> items)
    {
        List<ItemDataModel> itemDataModels = new();
        foreach (ItemModel itemModel in items)
        {
            ItemDataModel item = new ItemDataModel
            {
                ItemID = itemModel.Id,
                ItemType = (int)itemModel.ItemType,
                Quantity = itemModel.Quantity,
            };
            itemDataModels.Add(item);
        }

        return itemDataModels;
    }
}
