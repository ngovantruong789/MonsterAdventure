using System;

public interface IBattleManager
{
    IMapManager MapManager { get; }
    IObservable<bool> OnBattleStatus { get; }
    void EnterBattle();
    void EndBattle();
    void SetMapManager(IMapManager mapManager);
}