using System;

public interface IBattleMonsterPresenter
{
    public Action<EBattlePhase> TurnEvt {  get; set; }
    Action<bool, EStatePhase, int> StatePhaseChangeEvt { get; set; }
    void ActiveAttack(bool isPlayer, int playerMonsterIndex, int skillIndex);
    void NotifyStateCompleted(EStatePhase eStatePhase);
}
