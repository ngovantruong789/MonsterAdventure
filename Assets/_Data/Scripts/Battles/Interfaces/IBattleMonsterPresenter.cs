using System;

public interface IBattleMonsterPresenter
{
    IObservable<EBattlePhase> OnTurnChanged { get; }
    IObservable<StatePhaseChangedControllerData> OnStatePhaseChanged { get; }
    int CurrentPlayerMonsterBattleIndex { get; set; }
    void ActiveAttack(EMonsterSide eMonsterSide, int skillIndex);
    void NotifyStateCompleted(EStatePhase eStatePhase);
}
