using System;

public interface IBattleMonsterPresenter
{
    int CurrentPlayerMonsterBattleIndex { get; set; }
    public Action<EBattlePhase> TurnEvt {  get; set; }
    Action<EMonsterSide, EStatePhase, int, bool> StatePhaseChangeEvt { get; set; }
    void ActiveAttack(EMonsterSide eMonsterSide, int skillIndex);
    void NotifyStateCompleted(EStatePhase eStatePhase);
}
