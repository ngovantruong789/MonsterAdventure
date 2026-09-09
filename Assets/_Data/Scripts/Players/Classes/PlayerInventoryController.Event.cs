using System;
using UniRx;

public partial class PlayerInventoryController
{
    private readonly Subject<Unit> _onPlayerInventoryChanged = new();
    public IObservable<Unit> OnPlayerInventoryChanged => _onPlayerInventoryChanged;
}