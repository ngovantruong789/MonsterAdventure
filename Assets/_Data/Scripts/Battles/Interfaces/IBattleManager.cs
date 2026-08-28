using System;

public interface IBattleManager
{
    IObservable<bool> OnBattleStatus { get; }
    void EnterBattle();
    void EndBattle();
    void SetMapManager(IMapManager mapManager);
}
