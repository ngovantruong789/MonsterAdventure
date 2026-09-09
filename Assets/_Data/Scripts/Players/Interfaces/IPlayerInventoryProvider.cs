using System;
using UniRx;

public interface IPlayerInventoryProvider
{
    IObservable<Unit> OnPlayerInventoryChanged { get; }
    InventoryModel RestoreInventoryModel { get; }
    InventoryModel CaptureInventoryModel { get; }
    void UpdateQuantityPlayerInventoryModel();
    void InventoryConstructor();
    void SetPlayerData(IPlayerData playerData);
}