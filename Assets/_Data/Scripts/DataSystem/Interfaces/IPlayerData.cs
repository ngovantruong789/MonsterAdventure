using System;

public interface IPlayerData
{
    IObservable<PlayerSaveDataModel> OnLoadData { get; }
}