public interface IInventoryProvider
{
    RestoreInventoryModel RestoreInventoryModel { get; }
    CaptureInventoryModel CaptureInventoryModel { get; }
    void UpdateInventoryModel();
}