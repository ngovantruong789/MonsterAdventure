using System;

public interface IBattleMonsterTurn
{
    Action NextTurnEvt { get; set; }
    bool IsEndBattle { get; set; }
    void ChangeTurn(EBattlePhase eBattlePhase);
}
