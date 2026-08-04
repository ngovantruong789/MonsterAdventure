using System.Collections.Generic;

public static class InventoryModelFactory
{
    public static List<ItemModel> Create(List<ItemModel> items)
    {
        List<ItemModel> itemsList = new List<ItemModel>();
        if (items.Count <= 0 ) return itemsList;

        foreach(ItemModel item in items) 
        {
            ItemModel newItem = new ItemModel
            {
                Id = item.Id,
                Name = item.Name,
                EffectItem = item.EffectItem,
                Value = item.Value,
                BuyPrice = item.BuyPrice,
                SellPrice = item.SellPrice,
                Quantity = item.Quantity,
                Description = item.Description,
            };
            itemsList.Add(newItem);
        }
        return itemsList;
    }
}