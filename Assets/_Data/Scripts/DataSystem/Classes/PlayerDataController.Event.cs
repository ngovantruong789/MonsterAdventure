using System;
using UniRx;

public partial class PlayerDataController
{
    private readonly Subject<PlayerSaveDataModel> _onLoadData = new();
    public IObservable<PlayerSaveDataModel> OnLoadData => _onLoadData;
}