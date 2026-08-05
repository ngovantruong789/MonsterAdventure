using System.Collections.Generic;

public interface IInventoryModelProvider
{
    List<ItemModel> CloneInventoryModel(List<ItemModel> items);
    InventoryModel InventoryModel { get; }
}