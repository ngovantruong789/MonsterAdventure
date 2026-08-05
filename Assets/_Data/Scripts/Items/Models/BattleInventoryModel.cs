public class BattleInventoryModel
{
    public RestoreInventoryModel RestoreInventory { get; set; } = new();
    public CaptureInventoryModel CaptureInventory { get; set; } = new();
}