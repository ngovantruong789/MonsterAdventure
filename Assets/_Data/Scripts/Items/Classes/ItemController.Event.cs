using System;
using UniRx;

public partial class ItemController
{
    private Subject<ActiveItemControllerEventData> _onActiveItem = new();
    public IObservable<ActiveItemControllerEventData> OnActiveItem => _onActiveItem;
}