
public class InventoryModel
{
    public RestoreInventoryModel RestoreInventory { get; set; } = new();
    public CaptureInventoryModel CaptureInventory { get; set; } = new();
    public PlayerEquipmentModel PlayerEquipment { get; set; } = new();
    public MonsterEquipmentModel MonsterEquipment { get; set; } = new();
}