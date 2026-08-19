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
}