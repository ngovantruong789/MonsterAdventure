using System;

public interface IBattleMonsterPresenter
{
    int CurrentPlayerMonsterBattleIndex { get; set; }
    public Action<EBattlePhase> TurnEvt {  get; set; }
    Action<bool, EStatePhase, int> StatePhaseChangeEvt { get; set; }
    void ActiveAttack(bool isPlayer, int skillIndex);
    void NotifyStateCompleted(EStatePhase eStatePhase);
}
