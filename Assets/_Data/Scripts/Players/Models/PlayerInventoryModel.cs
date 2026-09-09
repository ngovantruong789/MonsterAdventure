public class PlayerInventoryModel
{
    public InventoryModel RestoreInventory { get; set; } = new();
    public InventoryModel CaptureInventory { get; set; } = new();
    public InventoryModel PlayerEquipment { get; set; } = new();
    public InventoryModel MonsterEquipment { get; set; } = new();
}