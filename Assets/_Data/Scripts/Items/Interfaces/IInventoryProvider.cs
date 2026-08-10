using System.Collections.Generic;

public interface IInventoryProvider
{
    List<ItemModel> CloneInventoryModel(List<ItemModel> items);
    InventoryModel InventoryModel { get; }
}