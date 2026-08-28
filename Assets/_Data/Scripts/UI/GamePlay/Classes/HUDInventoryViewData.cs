using UnityEngine;

public class HUDInventoryViewData
{
    public InventoryViewData RestoreInventory { get; set; } = new();
    public InventoryViewData CaptureInventory { get; set; } = new();
    public InventoryViewData PlayerEquipment { get; set; } = new();
    public InventoryViewData MonsterEquipment { get; set; } = new();
}