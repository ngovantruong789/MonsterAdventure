using System;
using UniRx;

public interface IBattleMonsterTurn
{
    IObservable<Unit> OnNextTurn { get;}
    IObservable<bool> OnEndBattle { get;}
    void ChangeTurn(EBattlePhase eBattlePhase);
}
